using Newtonsoft.Json.Linq;
using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
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
    public sealed partial class AdminEditProduct : Page
    {
        public AdminEditProduct()
        {
            this.InitializeComponent();
        }
        public string path_img { get; set; }
        private Database database;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter is Database)
            {
                database = (Database)e.Parameter;
            }
                ProductImage.Source = new BitmapImage(new Uri(ProductM.Img));
                NameTextbox.Text = ProductM.NameOfproduct;
                IngridientTextbox.Text = ProductM.Ingredient_description;
                IngridientTextbox.TextWrapping = TextWrapping.Wrap;
                PriceTextbox.Text = ProductM.Price.ToString();
                ProductId.Text = ProductM.Id.ToString();
                path_img = ProductM.Img;
                string productTypeString = Enum.GetName(typeof(ProductType), ProductM.TypeOfproduct);
                ComboBoxTypeOfProduct.SelectedItem = ComboBoxTypeOfProduct.Items.Cast<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == productTypeString);
            
        }
        private async void ButtonAddPhoto_Click_1(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                path_img = file.Path;
                ProductImage.Source = new BitmapImage(new Uri(path_img));
            }
        }

        private void ButtonEditConfirm_Click(object sender, RoutedEventArgs e)
        {
            foreach ( var item in database.MenuList )
            {
                if(item.Id==int.Parse(ProductId.Text))
                {
                    item.NameOfproduct = NameTextbox.Text;
                    item.Price = double.Parse(PriceTextbox.Text);
                    item.Ingredient_description = IngridientTextbox.Text;
                    item.TypeOfproduct= (ProductType)Enum.Parse(typeof(ProductType), ((ComboBoxItem)ComboBoxTypeOfProduct.SelectedItem).Content.ToString());
                    item.Img = path_img;
                }
            }
            ProductImage.Source = new BitmapImage(new Uri(path_img));
            Frame.Navigate(typeof(AdminMenu), database);
        }
    }
}
