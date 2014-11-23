using FridgeApp.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace FridgeApp.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private ObservableCollection<Product> allProducts;
        private ObservableCollection<Product> firstShelveProducts;
        private ObservableCollection<Product> vegAndFruitShelveProducts;
        private ObservableCollection<Product> drinkShelveProducts;
        private ObservableCollection<Product> dairyShelveProducts;
        private ICommand saveProduct;

        public ProductsViewModel()
        {
            this.AllProducts = new List<Product>();
            this.FirstShelveProducts = new List<Product>();
            this.VegAndFruitShelveProducts = new List<Product>();
            this.DrinksShelveProducts = new List<Product>();
            this.saveProduct = new RelayCommand(execute: this.SaveProductToFridge);
        }

        public IList<Product> AllProducts
        {
            get
            {
                return this.allProducts;
            }

            set
            {
                if (this.allProducts == null)
                {
                    this.allProducts = new ObservableCollection<Product>();
                }
                this.allProducts.Clear();

                foreach (var product in value)
                {
                    this.allProducts.Add(product);
                }
                this.OnPropertyChanged("AllProducts");
            }
        }

        public IList<Product> DairyShelveProducts
        {
            get
            {
                return this.dairyShelveProducts;
            }

            set
            {
                if (this.dairyShelveProducts == null)
                {
                    this.dairyShelveProducts = new ObservableCollection<Product>();
                }
                this.dairyShelveProducts.Clear();

                foreach (var product in value)
                {
                    this.dairyShelveProducts.Add(product);
                }
                this.OnPropertyChanged("DairyShelveProducts");
            }
        }

        public IList<Product> FirstShelveProducts
        {
            get
            {
                return this.firstShelveProducts;
            }

            set
            {
                if (this.firstShelveProducts == null)
                {
                    this.firstShelveProducts = new ObservableCollection<Product>();
                }
                this.firstShelveProducts.Clear();

                foreach (var product in value)
                {
                    this.firstShelveProducts.Add(product);
                }
                this.OnPropertyChanged("FirstShelveProducts");
            }
        }

        public IList<Product> VegAndFruitShelveProducts
        {
            get
            {
                return this.vegAndFruitShelveProducts;
            }

            set
            {
                if (this.vegAndFruitShelveProducts == null)
                {
                    this.vegAndFruitShelveProducts = new ObservableCollection<Product>();
                }
                this.vegAndFruitShelveProducts.Clear();

                foreach (var product in value)
                {
                    this.vegAndFruitShelveProducts.Add(product);
                }
                this.OnPropertyChanged("VegAndFruitShelveProducts");
            }
        }

        public IList<Product> DrinksShelveProducts
        {
            get
            {
                return this.drinkShelveProducts;
            }

            set
            {
                if (this.drinkShelveProducts == null)
                {
                    this.drinkShelveProducts = new ObservableCollection<Product>();
                }
                this.drinkShelveProducts.Clear();

                foreach (var product in value)
                {
                    this.drinkShelveProducts.Add(product);
                }
                this.OnPropertyChanged("DrinkShelveProducts");
            }
        }

        public ICommand SaveProduct
        {
            get
            {
                return this.saveProduct;
            }
        }
        
        private void SaveProductToFridge()
        {
            var b = 5;
        }
    }
}
