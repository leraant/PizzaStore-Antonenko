using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Models;
using PizzaStore.Logic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaStore.Presentation.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Sales : Page
    {
        public Sales()
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
                SalesRepository sr = new SalesRepository(database);
                string path_sales = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "sales.json");
                database.SalesList=sr.ReadFromJson(path_sales);
                MenuListView.ItemsSource = database.SalesList;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MenuListView.ItemsSource = database.SalesList;
            UpdateButtons();
        }
        private void ViewMoreButton_Click(object sender, RoutedEventArgs e)
        {
            var productsales = (sender as Button).DataContext as PizzaStore.Logic.Models.Sales;
            var product = database.MenuList.FirstOrDefault(f => f.Id == productsales.Id);
            if (product != null)
            {
                Frame.Navigate(typeof(Product), product);
            }
        }
        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            var productsales = (sender as Button).DataContext as PizzaStore.Logic.Models.Sales;
            MenuRepository menuRepository = new MenuRepository(database);
            var product = menuRepository.Find(productsales.Id);
            if (product != null)
            {
                CartSavingData сrep = new CartSavingData(database);
                string path_cart = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "cart.json");
                database.CartList = сrep.ReadFromJson(path_cart);
                UserAction ua = new UserAction(database);
                if (LogInUser.InAccount)
                {
                    double price_with_discount = PizzaStore.Logic.Models.Sales.CalculatePriceWithDiscount(product.Price, product.PercentDiscount);
                    ua.MakeOrderNotify += ua.DisplayMessage;
                    ua.AddToCart(product.NameOfproduct, product.TypeOfproduct, product.Id, price_with_discount, product.PercentDiscount, LogInUser.Id, database.CartList.Count + 100, product.Img);
                }
                сrep.SaveDataToJson(database.CartList, path_cart);
            }
        }

        private void FavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            var favoriteButton = (Button)sender;
            var button = (Button)sender;
            Windows.UI.Xaml.Controls.Image favoriteButtonImage = (Windows.UI.Xaml.Controls.Image)button.Content;

            var product = (sender as Button).DataContext as PizzaStore.Logic.Models.Sales;
            if (product != null)
            {
                if (LogInUser.InAccount)
                {
                    var favoritesToDelete = database.FavoritesList.FirstOrDefault(f => f.ProductId == product.Id && f.UserId == LogInUser.Id);
                    if (favoritesToDelete != null)
                    {
                        favoriteButtonImage.Source = new BitmapImage(new Uri("ms-appx:///Pages/Assets/heart.png"));
                        database.FavoritesList.Remove(favoritesToDelete);
                        FavoriteSavingData fr = new FavoriteSavingData(database);
                        string path_favorite = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "favorite.json");
                        fr.SaveDataToJson(database.FavoritesList, path_favorite);
                    }
                    else
                    {
                        favoriteButtonImage.Source = new BitmapImage(new Uri("ms-appx:///Pages/Assets/heart_filled.png"));
                        var productmenu = database.MenuList.FirstOrDefault(f => f.Id == product.Id);
                        Favorites favorites = new Favorites(product.NameOfproduct, product.TypeOfproduct, LogInUser.Id, product.Id, product.Price, productmenu.Ingredient_description, product.Img);
                        database.FavoritesList.Add(favorites);
                        FavoriteSavingData fr = new FavoriteSavingData(database);
                        string path_favorite = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "favorite.json");
                        fr.SaveDataToJson(database.FavoritesList, path_favorite);
                    }
                }
            }

        }

        public void UpdateButtons()
        {
            ListView listView = FindName("MenuListView") as ListView;
            if (listView != null)
            {
                foreach (var item in listView.Items)
                {
                    ListViewItem listViewItem = listView.ItemContainerGenerator.ContainerFromItem(item) as ListViewItem;
                    if (listViewItem != null)
                    {
                        Grid grid = FindVisualChild<Grid>(listViewItem);
                        if (grid != null)
                        {
                            Button favoriteButton = grid.FindName("FavoriteButton") as Button;
                            Button salesButton = grid.FindName("PercentButton") as Button;
                            if (salesButton != null)
                            {
                                var product = item as PizzaStore.Logic.Models.Sales;
                                if (product.PercentDiscount == 0)
                                {
                                    salesButton.Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    salesButton.Visibility = Visibility.Visible;
                                    salesButton.Content = product.PercentDiscount + "%";
                                }
                            }
                            if (favoriteButton != null && (LogInUser.InAccount))
                            {
                                var product = item as PizzaStore.Logic.Models.Sales;
                                var favorites = database.FavoritesList.FirstOrDefault(f => f.ProductId == product.Id && f.UserId == LogInUser.Id);
                                Windows.UI.Xaml.Controls.Image favoriteButtonImage = (Windows.UI.Xaml.Controls.Image)favoriteButton.Content;

                                if (favorites != null)
                                {
                                    favoriteButtonImage.Source = new BitmapImage(new Uri("ms-appx:///Pages/Assets/heart_filled.png"));
                                }
                            }
                        }
                    }
                }
            }
        }
        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    return (T)child;
                }
                else
                {
                    T foundChild = FindVisualChild<T>(child);
                    if (foundChild != null)
                        return foundChild;
                }
            }
            return null;
        }

        private void ButtonCart_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Cart), database);
        }
    }
}
