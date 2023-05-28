using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Models;
using PizzaStore.Logic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Cart : Page
    {
        public Cart()
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
                if(!LogInUser.InAccount)
                {
                    BuyButton.Visibility = Visibility.Collapsed;  
                }
                CartSavingData сrep = new CartSavingData(database);
                string path_cart = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "cart.json");
                database.CartList = сrep.ReadFromJson(path_cart);
                UserCart();
            }
        }
        public void UserCart ()
        {
            List<PizzaStore.Logic.Models.Cart> usercart = new List<PizzaStore.Logic.Models.Cart>();
            if (LogInUser.InAccount)
            {
                foreach (var item in database.CartList)
                {
                    if (item.UserId == LogInUser.Id)
                    {
                        usercart.Add(item);
                    }
                }
            }
            MenuListView.ItemsSource = usercart;
        }
        private void ViewMoreButton_Click(object sender, RoutedEventArgs e)
        {
            var productcart = (sender as Button).DataContext as PizzaStore.Logic.Models.Cart;
            var product = database.MenuList.FirstOrDefault(f => f.Id == productcart.Id);
            if (product != null)
            {
                Frame.Navigate(typeof(Product), product);
            }
        }

        private void RemoveProductButton_Click(object sender, RoutedEventArgs e)
        {
            UserAction ua = new UserAction(database);
            var product = (sender as Button).DataContext as PizzaStore.Logic.Models.Cart;
            if (product != null)
            {
                ua.OrderRemovedNotify += ua.DisplayMessage;
                ua.RemoveOrderFromCard(product.Id, product.OrderNumber);
            }
            CartSavingData сrep = new CartSavingData(database);
            string path_cart = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "cart.json");
            сrep.SaveDataToJson(database.CartList, path_cart);
            UserCart();
        }
       
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Checkout), database);
        }
    }
}
