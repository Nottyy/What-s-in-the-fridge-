using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FridgeApp.ViewModels
{
    public class ExpiringProductsViewModel : BaseViewModel
    {
        private ObservableCollection<Product> expiringProducts;

        public ExpiringProductsViewModel()
        {
            this.ExpiringProducts = new List<Product>();
        }

        public IList<Product> ExpiringProducts
        {
            get
            {
                return this.expiringProducts;
            }

            set
            {
                if (this.expiringProducts == null)
                {
                    this.expiringProducts = new ObservableCollection<Product>();
                }
                this.expiringProducts.Clear();

                foreach (var product in value)
                {
                    this.expiringProducts.Add(product);
                }
                this.OnPropertyChanged("ExpiringProducts");
            }
        }
    }
}
