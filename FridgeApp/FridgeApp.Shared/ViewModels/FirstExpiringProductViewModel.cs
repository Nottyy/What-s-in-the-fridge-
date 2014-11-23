using FridgeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridgeApp.ViewModels
{
    public class FirstExpiringProductViewModel : BaseViewModel
    {
        public int ExpiringAfter { get; set; }
        public DateTime PurchasedOn { get; set; }
        public string Name { get; set; }
        public string Fridge { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }

        public FirstExpiringProductViewModel(string name, string description, double quantity, DateTime purchasedOn,
            int expiringAfter, string fridgeCategory)
        {
            this.Name = name;
            this.Description = description;
            this.Quantity = quantity;
            this.PurchasedOn = purchasedOn;
            this.ExpiringAfter = expiringAfter;
            this.Fridge = fridgeCategory;
        }
    }
}
