using FridgeApp.Common;
using FridgeApp.Helpers;
using FridgeApp.Models;
using FridgeApp.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class FridgeProductsPage : Page
    {
        public const string DBNAME_ALL_PRODUCTS = "allProductsDB";

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public FridgeProductsPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            this.DataContext = new ProductsViewModel();
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
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

            //Create DB if not exists
            bool dbExistsAllProducts = await CheckDbAsync(DBNAME_ALL_PRODUCTS);
            if (!dbExistsAllProducts)
            {
                await CreateDBAsync();
                await AddProductsAsync();
            }

            var parameters = e.Parameter as List<object>;

            if (parameters != null)
            {
                var dataContext = parameters[0] as ProductsViewModel;
                this.DataContext = dataContext;
            }

            //Get fridge products
            if (((ProductsViewModel)this.DataContext).FirstShelveProducts.Count() == 0)
            {
                ((ProductsViewModel)this.DataContext).AllProducts = await GetProductsByFridgeCategoryAsync(FridgeCategories.AllProducts);
                ((ProductsViewModel)this.DataContext).FirstShelveProducts = await GetProductsByFridgeCategoryAsync(FridgeCategories.FirstShelve);
                ((ProductsViewModel)this.DataContext).VegAndFruitShelveProducts = await GetProductsByFridgeCategoryAsync(FridgeCategories.VegAndFruitShelve);
                ((ProductsViewModel)this.DataContext).DrinksShelveProducts = await GetProductsByFridgeCategoryAsync(FridgeCategories.DrinksShelve);
                ((ProductsViewModel)this.DataContext).DairyShelveProducts = await GetProductsByFridgeCategoryAsync(FridgeCategories.DairyShelve);
            }

            this.listViewFirstShelve.ItemsSource = ((ProductsViewModel)this.DataContext).FirstShelveProducts;
            this.listViewVegAndFruitShelve.ItemsSource = ((ProductsViewModel)this.DataContext).VegAndFruitShelveProducts;
            this.listViewDrinksShelve.ItemsSource = ((ProductsViewModel)this.DataContext).DrinksShelveProducts;
            this.listViewDairyProductsShelve.ItemsSource = ((ProductsViewModel)this.DataContext).DairyShelveProducts;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async Task<bool> CheckDbAsync(string dbNameIdentification)
        {
            bool dbExists = true;
            try
            {
                await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(dbNameIdentification);
            }
            catch (Exception)
            {
                dbExists = false;
            }

            return dbExists;
        }

        private async Task CreateDBAsync()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DBNAME_ALL_PRODUCTS);
            await conn.CreateTableAsync<Product>();
        }

        private async Task AddProductsAsync()
        {
            // Create a Articles list
            var list = new List<Product>()
            {
                new Product("BobChorba", "../Images/dairy.jpg", DateTime.Now,DateTime.Now.AddDays(150),"Bobeca e strashen", 1, FridgeCategories.FirstShelve),
                new Product("Banani", "../Images/dairy.jpg", DateTime.Now,DateTime.Now.AddDays(10),"hapvam banan", 1, FridgeCategories.VegAndFruitShelve),
                new Product("Whiskey", "../Images/dairy.jpg", DateTime.Now,DateTime.Now.AddDays(2),"Pih si", 1, FridgeCategories.DrinksShelve),
                new Product("BobChorba", "../Images/dairy.jpg", DateTime.Now,DateTime.Now.AddDays(150),"Bobeca e strashen", 1, FridgeCategories.FirstShelve),
                new Product("Banani", "../Images/dairy.jpg", DateTime.Now,DateTime.Now.AddDays(10),"hapvam banan", 1, FridgeCategories.VegAndFruitShelve),
                new Product("Whiskey", "../Images/dairy.jpg", DateTime.Now,DateTime.Now.AddDays(2),"Pih si", 1, FridgeCategories.DrinksShelve)
                
            };

            // Add rows to the Article Table
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DBNAME_ALL_PRODUCTS);
            await conn.InsertAllAsync(list);
        }

        private async Task<List<Product>> GetProductsByFridgeCategoryAsync(FridgeCategories fridgeCategory)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DBNAME_ALL_PRODUCTS);

            AsyncTableQuery<Product> query = conn.Table<Product>().Where(x => x.FridgeCategory == fridgeCategory);
            List<Product> result = await query.ToListAsync();

            return result;
        }

        private async Task DeleteProductAsync(Product product)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DBNAME_ALL_PRODUCTS);

            // Delete record
            await conn.DeleteAsync(product);
        }

        private void NavigateToAddProductView(object sender, RoutedEventArgs e)
        {
            List<object> parameters = new List<object>() { this.DataContext };
            ((Frame)Window.Current.Content).Navigate(typeof(AddProductPage), parameters);
        }

        private async void ListViewElementHolding(object sender, HoldingRoutedEventArgs e)
        {
            Task<bool> isConnected = CheckInternetConnection.CheckForInternetConnection();

            if (!(await (isConnected)))
            {
                await new MessageDialog("You won't be able to mark the selected product as eaten! Check your internet connetion!").ShowAsync();
                return;
            }

            var frameworkElement = (FrameworkElement)e.OriginalSource;
            var holdedItem = ((Product)frameworkElement.DataContext);

            await this.CheckProductCategoryAndRemoveEntry(holdedItem);
            this.soundEatenProduct.Play();

            //logic for parse
            EatenProduct eatenProduct = new EatenProduct
            {
                Name = holdedItem.Name,
                Description = holdedItem.Description,
                ExpirationDays = holdedItem.ExpirationDays,
                Quantity = holdedItem.Quantity,
                DatePurchased = holdedItem.DateAdded
            };
            
            await eatenProduct.SaveAsync();

            MessageDialog dialogSuccess = new MessageDialog(string.Format("You have eaten the \"{0}\"!", holdedItem.Name));
            await dialogSuccess.ShowAsync();
        }

        private async void ListViewDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            
            var frameworkElement = (FrameworkElement)e.OriginalSource;
            var holdedItem = ((Product)frameworkElement.DataContext);

            await this.CheckProductCategoryAndRemoveEntry(holdedItem);
            this.soundDeleteProduct.Play();

            MessageDialog dialogSuccess = new MessageDialog(string.Format("You have successfully removed \"{0}\" from the fridge!", holdedItem.Name));
            await dialogSuccess.ShowAsync();
        }

        private async Task CheckProductCategoryAndRemoveEntry(Product product)
        {
            if (product != null)
            {
                switch (product.FridgeCategory)
                {
                    case FridgeCategories.FirstShelve:
                        ((ProductsViewModel)this.DataContext).FirstShelveProducts.Remove(product);
                        break;
                    case FridgeCategories.VegAndFruitShelve:
                        ((ProductsViewModel)this.DataContext).VegAndFruitShelveProducts.Remove(product);
                        break;
                    case FridgeCategories.DrinksShelve:
                        ((ProductsViewModel)this.DataContext).DrinksShelveProducts.Remove(product);
                        break;
                    case FridgeCategories.AllProducts:
                        ((ProductsViewModel)this.DataContext).AllProducts.Remove(product);
                        break;
                    case FridgeCategories.DairyShelve:
                        ((ProductsViewModel)this.DataContext).DairyShelveProducts.Remove(product);
                        break;
                    default:
                        break;
                }

                await this.DeleteProductAsync(product);
            }
        }
    }
}
