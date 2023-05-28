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
    public sealed partial class AdminSales : Page
    {
        public AdminSales()
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
        private void ViewMoreButton_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as PizzaStore.Logic.Models.Menu;
            if (product != null)
            {
                Frame.Navigate(typeof(AdminProduct), product);
            }
        }
        private void AddSalesButton_Click(object sender, RoutedEventArgs e)
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
                Frame.Navigate(typeof(AdminAddSales), database);
            }
        }
        private async void RemoveSalesButton_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as PizzaStore.Logic.Models.Menu;
            if (product != null)
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
                  MenuRepository menuReposirory = new MenuRepository(database);
                  PizzaStore.Logic.Models.Menu m = menuReposirory.Find(product.Id);
                  m.PercentDiscount = 0;

                  SalesRepository sr = new SalesRepository(database);
                  sr.Delete(product.Id);

                  string path_sales = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "sales.json");
                  sr.SaveDataToJson(database.SalesList, path_sales);

                  MenuListView.ItemsSource = database.MenuList;
                  UpdateButtons();

                    MenuRepository mr = new MenuRepository(database);
                    string path_menu = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "menu.json");
                    mr.SaveDataToJson(database.MenuList, path_menu);
                    Frame.Navigate(typeof(AdminSales), database);
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
                                var product = item as PizzaStore.Logic.Models.Menu;
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
                                var product = item as PizzaStore.Logic.Models.Menu;
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MenuListView.ItemsSource = database.MenuList;
            UpdateButtons();
        }
    }
}
