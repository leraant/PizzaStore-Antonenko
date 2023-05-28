using PizzaStore.Logic.Contexts;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaStore.Presentation.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminProfile : Page
    {
        public AdminProfile()
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
        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LogIn), database);
        }
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            LogInUser.InAccount = false;
            NameTextBox.Text = " ";
            EmailTextBox.Text = " ";
            Frame.Navigate(typeof(Profile), database);
        }
    }
}
