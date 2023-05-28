using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Models;
using PizzaStore.Logic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
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
    public sealed partial class Favorite : Page
    {
        public Favorite()
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
                List<PizzaStore.Logic.Models.Favorites> userfavorites = new List<PizzaStore.Logic.Models.Favorites>();
                foreach (var item in database.FavoritesList)
                {
                    if (item.UserId == LogInUser.Id)
                    {
                       userfavorites.Add(item);
                    }
                }
                MenuListView.ItemsSource = userfavorites;
            }
        }
       
        private void ViewMoreButton_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as PizzaStore.Logic.Models.Favorites;
            if (product != null)
            {
                Frame.Navigate(typeof(FavoriteProduct), product);
            }
        }
        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            var productfavorite = (sender as Button).DataContext as PizzaStore.Logic.Models.Favorites;
            MenuRepository menuRepository = new MenuRepository(database);
            var product = menuRepository.Find(productfavorite.ProductId);
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
            var product = (sender as Button).DataContext as PizzaStore.Logic.Models.Favorites;
            if (product != null)
            {
                if (LogInUser.InAccount)
                {
                    
                    if (product != null)
                    {
                        favoriteButtonImage.Source = new BitmapImage(new Uri("ms-appx:///Pages/Assets/heart.png"));
                        database.FavoritesList.Remove(product);
                        List<PizzaStore.Logic.Models.Favorites> userfavorites = new List<PizzaStore.Logic.Models.Favorites>();
                        foreach (var item in database.FavoritesList)
                        {
                            if (item.UserId == LogInUser.Id)
                            {
                                userfavorites.Add(item);
                            }
                        }
                        MenuListView.ItemsSource = userfavorites;
                    }
                    else
                    {
                        favoriteButtonImage.Source = new BitmapImage(new Uri("ms-appx:///Pages/Assets/heart_filled.png"));
                        Favorites favorites = new Favorites(product.NameOfproduct, product.TypeOfproduct, LogInUser.Id, product.ProductId, product.Price, product.Ingredient_description, product.Img);
                        database.FavoritesList.Add(favorites);
                    }
                }
            }

        }

        private void ButtonCart_Click(object sender, RoutedEventArgs e)
        {
           Frame.Navigate(typeof(Cart), database);
        }
    }
}
