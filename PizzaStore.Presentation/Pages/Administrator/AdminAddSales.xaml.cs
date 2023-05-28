using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
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
    public sealed partial class AdminAddSales : Page
    {
        public AdminAddSales()
        {
            this.InitializeComponent();
        }
        private Database database;
        public static string Img { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter is Database)
            {
                database = (Database)e.Parameter;
            }
            ProductImage.Source = new BitmapImage(new Uri(ProductM.Img));
            Img =ProductM.Img;
            NameTextbox.Text = ProductM.NameOfproduct;
            PriceTextbox.Text = ProductM.Price.ToString();
            ProductId.Text = ProductM.Id.ToString();
            ProductImage.Source = new BitmapImage(new Uri(ProductM.Img)); 
        }
        private async void ButtonEditConfirm_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in database.MenuList)
            {
                if (item.Id == int.Parse(ProductId.Text))
                {
                    if (int.Parse(PercentDiscountTextbox.Text) > 100)
                    {
                        await new MessageDialog("Invalid percent of sale.").ShowAsync();
                    }
                    else
                    {
                        item.PercentDiscount = int.Parse(PercentDiscountTextbox.Text);
                        PizzaStore.Logic.Models.Sales sale = new PizzaStore.Logic.Models.Sales(NameTextbox.Text, item.TypeOfproduct, int.Parse(PercentDiscountTextbox.Text), double.Parse(PriceTextbox.Text),
                        int.Parse(ProductId.Text), Img);
                        SalesRepository sr = new SalesRepository(database);
                        sr.Add(sale);
                        MenuRepository mr = new MenuRepository(database);
                        string path_menu = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "menu.json");
                        mr.SaveDataToJson(database.MenuList, path_menu);
                        string path_sales = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "sales.json");
                        sr.SaveDataToJson(database.SalesList, path_sales);
                    }
                }
            }
            Frame.Navigate(typeof(AdminSales), database);
        }
    }
}
