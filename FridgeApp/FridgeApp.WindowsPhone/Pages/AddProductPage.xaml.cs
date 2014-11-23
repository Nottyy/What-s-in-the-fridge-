using FridgeApp.Common;
using FridgeApp.Models;
using FridgeApp.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FridgeApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddProductPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public const string DBNAME_ALL_PRODUCTS = "allProductsDB";

        public AddProductPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            this.FridgeCategoriesCombobox.ItemsSource = Enum.GetValues(typeof(FridgeCategories));
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

            var parameters = e.Parameter as List<object>;

            var dataContext = parameters[0] as ProductsViewModel;
            this.DataContext = dataContext;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //var product = new Product("Sapun", "../Images/domat.jpg", DateTime.Now, DateTime.Now.AddDays(199), "hubav sapun", 12.0, FridgeCategories.FirstShelve);
           
            if (this.FridgeCategoriesCombobox.SelectedItem == null)
            {
                MessageDialog dialog = new MessageDialog("Please input fridge location!");
                await dialog.ShowAsync();
                return;
            }
            FridgeCategories productCategory = (FridgeCategories)(this.FridgeCategoriesCombobox.SelectedItem);

            var productName = this.ProductName.Text;
            if (productName == null || productName.Length < 2 || productName.Length > 7)
            {
                MessageDialog dialog = new MessageDialog("Wrong product name!");
                await dialog.ShowAsync();
                return;
            }

            var productDescription = this.ProductDescription.Text;
            if (productDescription == null)
            {
                MessageDialog dialog = new MessageDialog("Please input product description!");
                await dialog.ShowAsync();
                return;
            }

            var productQuantity = this.ProductQuantity.Text;
            double productQuantityDouble;

            if (productQuantity == null || !(double.TryParse(productQuantity, out productQuantityDouble)))
            {
                MessageDialog dialog = new MessageDialog("The product quantity is either null or not a decimal number!");
                await dialog.ShowAsync();
                return;
            }

            DateTime purchaseDateTime = this.purchaseDatePicker.Date.DateTime;
            DateTime expirationDateTime = this.expirationDatePicker.Date.DateTime;
            if (purchaseDateTime > expirationDateTime)
            {
                MessageDialog dialog = new MessageDialog("The expiration date must be exceeding the purchase date!");
                await dialog.ShowAsync();
                return;
            }

            var product = new Product(productName, null, purchaseDateTime, expirationDateTime, productDescription, productQuantityDouble, productCategory);
            ((ProductsViewModel)this.DataContext).AllProducts.Add(product);

            switch (productCategory)
            {
                case FridgeCategories.FirstShelve:
                    product.ImagePath = "../Images/first-shelve.jpg";
                    ((ProductsViewModel)this.DataContext).FirstShelveProducts.Add(product);
                    break;
                case FridgeCategories.VegAndFruitShelve:
                    product.ImagePath = "../Images/first-shelve.jpg";
                    ((ProductsViewModel)this.DataContext).VegAndFruitShelveProducts.Add(product);
                    break;
                case FridgeCategories.DrinksShelve:
                    product.ImagePath = "../Images/first-shelve.jpg";
                    ((ProductsViewModel)this.DataContext).DrinksShelveProducts.Add(product);
                    break;
                case FridgeCategories.DairyShelve:
                    product.ImagePath = "../Images/first-shelve.jpg";
                    ((ProductsViewModel)this.DataContext).DairyShelveProducts.Add(product);
                    break;
                default:
                    break;
            }

            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DBNAME_ALL_PRODUCTS);
            await conn.InsertAsync(product);

            this.soundAddProduct.Play();

            List<object> parameters = new List<object>() { this.DataContext };
            ((Frame)Window.Current.Content).Navigate(typeof(FridgeProductsPage), parameters);
        }
    }
}
