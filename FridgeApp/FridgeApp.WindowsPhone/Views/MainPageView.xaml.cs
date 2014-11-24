using FridgeApp.Pages;
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
using FridgeApp.Helpers;
using System.Threading.Tasks;
using Windows.UI.Popups;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace FridgeApp.Views
{
    public sealed partial class MainPageView : UserControl
    {
        public MainPageView()
        {
            this.InitializeComponent();
        }

        private void NavigateToFridgeProductsPage(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(FridgeProductsPage));
        }

        private async void NavigateToEatenProductsPage(object sender, RoutedEventArgs e)
        {
            bool isConnected = await CheckInternetConnection.CheckForInternetConnection();
            

            if (!isConnected)
            {
                await new MessageDialog("You won't be able to proceed to the Eaten Products page! Check your internet connetion!").ShowAsync();
                return;
            }
            ((Frame)Window.Current.Content).Navigate(typeof(EatenProductsPage));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(OnShakeGetFirstExpiringProductPage));
        }

        private void NavigateToExpiringProductsPage(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(ExpiringProductsPage));
        }
    }
}
