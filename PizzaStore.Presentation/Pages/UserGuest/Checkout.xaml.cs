using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Services;
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
    public sealed partial class Checkout : Page
    {
        public Checkout()
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
                CartSavingData сrep = new CartSavingData(database);
                string path_cart = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "cart.json");
                database.CartList = сrep.ReadFromJson(path_cart);
                OrdersSavingData or = new OrdersSavingData(database);
                string path_order = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "orders.json");
                database.OrdersList= or.ReadFromJson(path_order);   
            }
        }

        private async void PayButton_Click(object sender, RoutedEventArgs e)
        {
            if (CreditCardRadioButton.IsChecked == true)
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
                string cardNumber = CardNumberTextbox.Text.Replace(" ", ""); // Видаляємо пробіли з номеру картки
                string cardHolderName = CardholderNameTextbox.Text;
                string Street = StreetTextbox.Text;
                int houseNumber;
                if (!int.TryParse(HousenumberTextbox.Text, out houseNumber))
                {
                    await new MessageDialog("House number should be a valid integer.").ShowAsync();
                    return; // Зупиняємо виконання функції
                }
                string CVVCVC = CVVCVCTextbox.Text;

                // Перевірка кількості цифр у полі CVVCVC
                if (CVVCVC.Length != 3 || !CVVCVC.All(char.IsDigit))
                {
                    await new MessageDialog("CVV/CVC should be a 3-digit number.").ShowAsync();
                    return; // Зупиняємо виконання функції
                }

                // Перевірка кількості цифр у полі cardNumber
                if (cardNumber.Length != 16 || !cardNumber.All(char.IsDigit))
                {
                    await new MessageDialog("Card number should contain 16 digits.").ShowAsync();
                    return; // Зупиняємо виконання функції
                }

                if (string.IsNullOrEmpty(cardHolderName) || string.IsNullOrEmpty(Street))
                {
                    await new MessageDialog("Please fill in all the required fields.").ShowAsync();
                    return; // Зупиняємо виконання функції
                }

                UserAction ua = new UserAction(database);
                foreach (var item in usercart)
                {
                    ua.MakeOrderWithCard(item.NameOfProduct, item.TypeOfProduct, DateTime.Now, LogInUser.location, usercart.Count, cardNumber, cardHolderName, Street, houseNumber, CVVCVC, true, LogInUser.Id);
                }
                OrdersSavingData or = new OrdersSavingData(database);
                string path_order = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "orders.json");
                or.SaveDataToJson(database.OrdersList, path_order);
                Frame.Navigate(typeof(Home), database);
            }
            else if (CashOnDeliveryRadioButton.IsChecked == true)
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
                string Street = StreetTextbox.Text;
                int houseNumber;
                if (!int.TryParse(HousenumberTextbox.Text, out houseNumber))
                {
                    await new MessageDialog("House number should be a valid integer.").ShowAsync();
                    return; // Зупиняємо виконання функції
                }

                if (string.IsNullOrEmpty(Street))
                {
                    await new MessageDialog("Please fill in all the required fields.").ShowAsync();
                    return; // Зупиняємо виконання функції
                }

                UserAction ua = new UserAction(database);
                foreach (var item in usercart)
                {
                    ua.MakeOrder(item.NameOfProduct, item.TypeOfProduct, DateTime.Now, LogInUser.location, usercart.Count, Street, houseNumber, false, LogInUser.Id);
                }
                OrdersSavingData or = new OrdersSavingData(database);
                string path_order = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "orders.json");
                or.SaveDataToJson(database.OrdersList, path_order);
                Frame.Navigate(typeof(Home), database);
            }
            else
            {
                await new MessageDialog("You should choose a payment type.").ShowAsync();
            }
        }



        private void CashOnDeliveryRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            CardNumber.Visibility= Visibility.Collapsed;
            CardNumberTextbox.Visibility = Visibility.Collapsed;
            CardholderName.Visibility= Visibility.Collapsed;
            CardholderNameTextbox.Visibility= Visibility.Collapsed;
            CVVCVC.Visibility= Visibility.Collapsed;
            CVVCVCTextbox.Visibility= Visibility.Collapsed;
        }

        private void CreditCardRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            CardNumber.Visibility= Visibility.Visible;
            CardNumberTextbox.Visibility = Visibility.Visible;
            CardholderName.Visibility= Visibility.Visible;
            CardholderNameTextbox.Visibility = Visibility.Visible;
            CVVCVC.Visibility= Visibility.Visible;  
            CVVCVCTextbox.Visibility = Visibility.Visible;
        }
    }
}
