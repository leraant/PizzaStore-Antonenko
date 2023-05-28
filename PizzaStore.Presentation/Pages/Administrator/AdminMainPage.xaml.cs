using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PizzaStore.Presentation.Pages;
using PizzaStore.Logic.Contexts;

namespace PizzaStore.Presentation.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminMainPage : Page
    {
        public AdminMainPage()
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
        }
        private void Home_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Home.Background = new SolidColorBrush(Color.FromArgb(45, 0, 205, 0));
        }
        private void Home_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Home.Background = null;
        }
        private void Home_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Home),database);
        }
        private void Menu_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Menu.Background = new SolidColorBrush(Color.FromArgb(45, 0, 205, 0));
        }
        private void Menu_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Menu.Background = null;
        }
        private void Menu_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(AdminMenu), database);
        }
        private void Sales_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Sales.Background = new SolidColorBrush(Color.FromArgb(45, 0, 205, 0));
        }
        private void Sales_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Sales.Background = null;
        }
        private void Sales_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(AdminSales), database);
        }
        private void Favorite_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Favorite.Background = new SolidColorBrush(Color.FromArgb(45, 0, 205, 0));
        }
        private void Favorite_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Favorite.Background = null;
        }
        private void Favorite_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Feedbacks), database);
        }
        private void AboutUs_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            AboutUs.Background = new SolidColorBrush(Color.FromArgb(45, 0, 205, 0));
        }
        private void AboutUs_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            AboutUs.Background = null;
        }
        private void AboutUs_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(AboutUs), database);
        }
        private void Orders_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Orders.Background = new SolidColorBrush(Color.FromArgb(45, 0, 205, 0));
        }
        private void Orders_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Orders.Background = null;
        }
        private void Orders_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(AdminOrder), database);
        }
        private void Profile_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Profile.Background = new SolidColorBrush(Color.FromArgb(45, 0, 205, 0));
        }
        private void Profile_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Profile.Background = null;
        }
        private void Profile_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(AdminProfile), database);
        }
        public void ButtonLocation_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Location), database);
        }

        private void ButtonFeedback_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Feedbacks), database);
        }
    }
}
