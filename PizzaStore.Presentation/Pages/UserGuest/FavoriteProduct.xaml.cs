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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaStore.Presentation.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FavoriteProduct : Page
    {
        public FavoriteProduct()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var selectedProduct = e.Parameter as PizzaStore.Logic.Models.Favorites;
            if (selectedProduct != null)
            {
                ProductImage.Source = new BitmapImage(new Uri(selectedProduct.Img));
                ProductName.Text = selectedProduct.NameOfproduct;
                ProductDescription.Text = selectedProduct.Ingredient_description;
                ProductDescription.TextWrapping = TextWrapping.Wrap;
                ProductPrice.Text = $"Price: {selectedProduct.Price} €";
            }
        }

    }
}
