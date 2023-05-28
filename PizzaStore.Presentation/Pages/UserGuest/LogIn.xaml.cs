using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Services;
using PizzaStore.Presentation.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using System.Text.Json;
using System.Threading.Tasks;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaStore.Presentation.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LogIn : Page
    {
       
        public LogIn()
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
        private void ButtonRegistaration_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Registration), database);
        }

        private async void ButtonLogIn_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = EmailTextBox.Text;
                string password = PasswordBox.Password;
                string path_admin = Path.Combine(Path.GetTempPath(), "administrator.json");
                Administrator administrator = Administrator.ReadFromJson(path_admin);
                if (administrator.Email == email && administrator.Password == password)
                {
                    Frame rootFrame = Window.Current.Content as Frame;
                    rootFrame.Navigate(typeof(AdminMainPage), database);
                    
                    LogInUser.InAccount = true;
                }
                else
                {
                    Users user = database.UserList.FirstOrDefault(u => u.Email == email && u.Password == password);
                    if (user != null)
                    {
                        SaveUserInAccountToJson(user);
                        LogInUser.InAccount = true;
                        Frame rootFrame = Window.Current.Content as Frame;
                        rootFrame.Navigate(typeof(MainPage), database);
                    }
                    else
                    {
                        await new MessageDialog("Invalid email or password.").ShowAsync();
                    }
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

        private void SaveUserInAccountToJson(Users user)
        {
            LogInUser.Id = user.Id; 
            LogInUser.FirstName=user.FirstName;
            LogInUser.LastName=user.LastName;
            LogInUser.Email=user.Email;
            LogInUser.Password=user.Password;
            LogInUser.location = user.City;
        }
        
    }
}
