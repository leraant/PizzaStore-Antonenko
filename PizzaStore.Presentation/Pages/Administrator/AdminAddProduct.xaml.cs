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
    public sealed partial class AdminAddProduct : Page
    {
        public AdminAddProduct()
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
        public string path_img { get; set; }
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

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            int id = 100;
            if (database.MenuList.Count > 0)
            {
                id = database.MenuList.Max(f => f.Id) + 1;
            }
            string product_name = NameTextbox.Text;
            double price= double.Parse(PriceTextbox.Text);
            string ingredient_description = IngridientTextbox.Text;
            ProductType productType = (ProductType)Enum.Parse(typeof(ProductType), ((ComboBoxItem)ComboBoxTypeOfProduct.SelectedItem).Content.ToString());
            PizzaStore.Logic.Models.Menu menu = new PizzaStore.Logic.Models.Menu(id, product_name, price, ingredient_description, productType, path_img, 0);
            ProductImage.Source = new BitmapImage(new Uri(menu.Img));
            MenuRepository mr = new MenuRepository(database);
            mr.Add(menu);
            string path_menu = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "menu.json");
            mr.SaveDataToJson(database.MenuList, path_menu);
            Frame.Navigate(typeof(AdminMenu), database);
           
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UsersSavingData ur = new UsersSavingData(database);
            MenuRepository mr = new MenuRepository(database);
            FavoriteSavingData fr = new FavoriteSavingData(database);

            string path_user = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "users.json");
            string path_menu = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "menu.json");
            string path_favorite = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "favorite.json");

            database.UserList = ur.ReadFromJson(path_user);
            database.MenuList = mr.ReadFromJson(path_menu);
            database.FavoritesList = fr.ReadFromJson(path_favorite);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            UsersSavingData ur = new UsersSavingData(database);
            MenuRepository mr = new MenuRepository(database);
            FavoriteSavingData fr = new FavoriteSavingData(database);

            string path_user = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "users.json");
            string path_menu = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "menu.json");
            string path_favorite = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "favorite.json");

            ur.SaveDataToJson(database.UserList, path_user);
            mr.SaveDataToJson(database.MenuList, path_menu);
            fr.SaveDataToJson(database.FavoritesList, path_favorite);
        }
    }
}
