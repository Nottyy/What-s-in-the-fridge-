using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using FridgeApp.Models;

namespace FridgeApp.ViewModels
{
    [Table("FridgeProducts")]
    public class Product: BaseViewModel
    {
        private double quantity;

        public Product()
        {

        }

        public Product(string name, string imagePath, DateTime dateAdded, DateTime expirationDate,
            string description, double quantity, FridgeCategories fridgeCategory)
        {
            this.Name = name;
            this.ImagePath = imagePath;
            this.DateAdded = dateAdded;
            this.ExpirationDate = expirationDate;
            this.Description = description;
            this.Quantity = quantity;
            this.FridgeCategory = fridgeCategory;

            this.ExpirationDays = (int)((this.ExpirationDate - this.DateAdded).TotalDays);
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int ExpirationDays { get; set; }

        public string Description { get; set; }

        public double Quantity
        {
            get
            {
                return this.quantity;
            }
            set
            {
                if (value < 0)
                {
                    return;
                }
                this.quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public FridgeCategories FridgeCategory { get; set; }
    }
}
