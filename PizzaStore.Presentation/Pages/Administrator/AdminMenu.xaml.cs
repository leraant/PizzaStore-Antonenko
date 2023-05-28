using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Models;
using PizzaStore.Presentation.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaStore.Presentation.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminMenu : Page
    {
        public AdminMenu()
        {
            this.InitializeComponent();
            
        }
        private Database database;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter is Database)
            {
                database = (Database)e.Parameter;
            }
            MenuListView.ItemsSource = database.MenuList;
        }
        private void ButtonAdd_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminAddProduct), database);
        }
        private void ViewMoreButton_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as PizzaStore.Logic.Models.Menu;
            if (product != null)
            {
                Frame.Navigate(typeof(AdminProduct), product);
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as PizzaStore.Logic.Models.Menu;
            if (product != null)
            {
                ProductM.Img = product.Img;
                ProductM.NameOfproduct = product.NameOfproduct;
                ProductM.Ingredient_description = product.Ingredient_description;
                ProductM.Price = product.Price;
                ProductM.Id = product.Id;
                ProductM.TypeOfproduct = product.TypeOfproduct;
                Frame.Navigate(typeof(AdminEditProduct), database);
            }
        }
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as PizzaStore.Logic.Models.Menu;
            if (product != null)
            {
                if (database.MenuList.Count != 1)
                {
                    var confirmDialog = new ContentDialog
                    {
                        Title = "Confirm Deletion",
                        Content = "Are you sure you want to delete this product?",
                        PrimaryButtonText = "Yes",
                        CloseButtonText = "No"
                    };

                    ContentDialogResult result = await confirmDialog.ShowAsync();

                    if (result == ContentDialogResult.Primary)
                    {
                        MenuRepository mr = new MenuRepository(database);
                        mr.Delete(product.Id);
                        SalesRepository sr = new SalesRepository(database);
                        sr.Delete(product.Id);
                        Favorites favorite = database.FavoritesList.Find(s => s.ProductId == product.Id);
                        if (favorite != null)
                        {
                            database.FavoritesList.Remove(favorite);
                        }
                        MenuListView.ItemsSource = database.MenuList;

                        string path_menu = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "menu.json");
                        mr.SaveDataToJson(database.MenuList, path_menu);
                        string path_sales = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "sales.json");
                        sr.SaveDataToJson(database.SalesList, path_sales);
                        FavoriteSavingData fsd =new FavoriteSavingData(database);
                        string path_favorites = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "favorite.json");
                        fsd.SaveDataToJson(database.FavoritesList, path_favorites);
                        Frame.Navigate(typeof(AdminMenu), database);
                    }
                }
                else
                {
                    await new MessageDialog("You can`t delete all produts!").ShowAsync();
                }
            }
        }
        private void MenuPizzasButton_Click(object sender, RoutedEventArgs e)
        {
            List<PizzaStore.Logic.Models.Menu> pizzas = new List<PizzaStore.Logic.Models.Menu>();
            foreach (var item in database.MenuList)
            {
                if (item.TypeOfproduct == ProductType.Pizza)
                {
                    pizzas.Add(item);
                }
            }
            MenuListView.ItemsSource = pizzas;
        }
        private void MenuDrinksButton_Click(object sender, RoutedEventArgs e)
        {
            List<PizzaStore.Logic.Models.Menu> drinks = new List<PizzaStore.Logic.Models.Menu>();
            foreach (var item in database.MenuList)
            {
                if (item.TypeOfproduct == ProductType.Drinks)
                {
                    drinks.Add(item);
                }
            }
            MenuListView.ItemsSource = drinks;
        }
        private void MenuDessertsButton_Click(object sender, RoutedEventArgs e)
        {
            List<PizzaStore.Logic.Models.Menu> desserts = new List<PizzaStore.Logic.Models.Menu>();
            foreach (var item in database.MenuList)
            {
                if (item.TypeOfproduct == ProductType.Desserts)
                {
                    desserts.Add(item);
                }
            }
            MenuListView.ItemsSource = desserts;
        }
    }
}
