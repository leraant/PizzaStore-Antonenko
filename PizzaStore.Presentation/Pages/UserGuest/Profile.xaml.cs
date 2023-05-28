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
using Windows.UI.Xaml.Navigation;
using PizzaStore.Presentation.Pages;
using PizzaStore.Presentation.View;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Windows.UI.ViewManagement;
using PizzaStore.Logic.Services;
using Windows.Storage;
using Windows.UI.Popups;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using PizzaStore.Logic.Contexts;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaStore.Presentation.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Profile : Page
    {
        private Database database;
        public Profile()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter is Database)
            {
                database = (Database)e.Parameter;
            }
            if (LogInUser.InAccount)
            {
               NameTextBox.Text = LogInUser.FirstName + " " + LogInUser.LastName;
               EmailTextBox.Text = LogInUser.Email;
            }
        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LogIn), database);
        }

        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Registration), database);
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            LogInUser.InAccount = false;
            NameTextBox.Text = " ";
            EmailTextBox.Text = " ";
        }
    }
}
