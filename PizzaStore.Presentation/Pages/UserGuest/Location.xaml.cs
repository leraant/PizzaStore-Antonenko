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
using PizzaStore.Logic.Models;
using System.Security.Cryptography;
using Windows.Storage;
using System.Text.Json;
using PizzaStore.Logic.Services;
using Windows.UI.Popups;
using PizzaStore.Logic.Contexts;
using System.Threading.Tasks;

namespace PizzaStore.Presentation.Pages
{
    public sealed partial class Location : Page
    {
        public Location()
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
            RadioButton selectedRadioButton = FindName(LogInUser.location.ToString() + "R") as RadioButton;
            if (selectedRadioButton != null)
            {
                    LvivR.IsChecked = false;
                    KyivR.IsChecked = false;
                    KharkivR.IsChecked = false;
                    OdesaR.IsChecked = false;
                    DniproR.IsChecked = false;
                    PoltavaR.IsChecked = false;
                    TernopilR.IsChecked = false;
                    UmanR.IsChecked = false;
                    selectedRadioButton.IsChecked = true;
            }
        }

        public void RadioButtonCheck()
        {
          
            if (LvivR.IsChecked == true)
            {
                LogInUser.location = PizzaStore.Logic.Models.Location.Lviv;
            }
            else if (KyivR.IsChecked == true)
            {
                LogInUser.location = PizzaStore.Logic.Models.Location.Kyiv;
            }
            else if (KharkivR.IsChecked == true)
            {
                LogInUser.location = PizzaStore.Logic.Models.Location.Kharkiv;
            }
            else if (OdesaR.IsChecked == true)
            {
                LogInUser.location = PizzaStore.Logic.Models.Location.Odesa;
            }
            else if (DniproR.IsChecked == true)
            {
                LogInUser.location = PizzaStore.Logic.Models.Location.Dnipro;
            }
            else if (PoltavaR.IsChecked == true)
            {
                LogInUser.location = PizzaStore.Logic.Models.Location.Poltava;
            }
            else if (TernopilR.IsChecked == true)
            {
                LogInUser.location = PizzaStore.Logic.Models.Location.Ternopil;
            }
            else if (UmanR.IsChecked == true)
            {
                LogInUser.location = PizzaStore.Logic.Models.Location.Uman;
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            RadioButtonCheck();
            if (LogInUser.InAccount)
            {
               Users userToUpdate = database.UserList.Find(u => u.Email == LogInUser.Email);
                if (userToUpdate != null)
                {
                    userToUpdate.City = LogInUser.location;
                }
            }
        }
    }
}
