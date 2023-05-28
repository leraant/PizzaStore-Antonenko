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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaStore.Presentation.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Order : Page
    {
        public Order()
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
                OrdersSavingData or = new OrdersSavingData(database);
                string path_order = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "orders.json");
                database.OrdersList=or.ReadFromJson(path_order);
                List<PizzaStore.Logic.Models.Orders> userorders = new List<PizzaStore.Logic.Models.Orders>();
                if (LogInUser.InAccount)
                {
                    OrdersEnumerator enumerator = new OrdersEnumerator(database.OrdersList);
                    foreach (var item in enumerator)
                    {
                        if (item.Id == LogInUser.Id)
                        {
                            userorders.Add(item);
                        }
                    }
                }
                OrderListView.ItemsSource = userorders;
            }
        }

        private void ButtonFeedbacks_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Feedbacks), database);
        }
    }
}
