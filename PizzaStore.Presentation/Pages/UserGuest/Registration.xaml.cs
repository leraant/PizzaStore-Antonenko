using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Services;
using PizzaStore.Presentation.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading;
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
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaStore.Presentation.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Registration : Page
    {
        public Registration()
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

        private async void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guest guestAction = new Guest(database);
                int id = database.UserList.Count + 100;
                string firstName = FirstNameTextbox.Text;
                string lastName = LastNameTextbox.Text;
                string email = EmailTextbox.Text;
                string password = PasswordBox.Password;
                string confirmPassword = ConfirmPasswordBox.Password;

                // Перевірка, чи не є поля порожніми
                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                    string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(confirmPassword))
                {
                    throw new ArgumentException("All fields must be filled.");
                }

                bool reg = guestAction.Register(id, firstName, lastName, email, password, confirmPassword, LogInUser.location);
                if (reg)
                {
                    await new MessageDialog("You have successfully registered.").ShowAsync();
                    UsersSavingData ur = new UsersSavingData(database);
                    string path_user = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "users.json");
                    ur.SaveDataToJson(database.UserList, path_user);
                    Frame rootFrame = Window.Current.Content as Frame;
                    rootFrame.Navigate(typeof(MainPage), database);
                }
                else
                {
                    await new MessageDialog("Passwords do not match").ShowAsync();
                }
            }
            catch (ArgumentException ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            catch (Exception ex)
            {
                await new MessageDialog("An error occurred: " + ex.Message).ShowAsync();
            }
        }

        public void Reset(Database database)
        {
            UsersSavingData ur = new UsersSavingData(database);
            database.UserList.Clear();
        }
       
    }
}
