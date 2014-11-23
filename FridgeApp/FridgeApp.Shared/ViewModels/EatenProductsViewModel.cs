using FridgeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridgeApp.ViewModels
{
    public class EatenProductsViewModel
    {
        public static Func<EatenProduct, EatenProductsViewModel> FromModel
        {
            get
            {
                return model => new EatenProductsViewModel()
                {
                    Name = (model.Name).ToString(),
                    ExpiringAfter = (int)(model.ExpirationDays),
                    Description = model.Description,
                    PurchasedDate = model.DatePurchased,
                    Quantity = model.Quantity,
                    FridgeCategory = model.FridgeCategory
                };
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PurchasedDate { get; set; }
        public int ExpiringAfter { get; set; }
        public double Quantity { get; set; }
        public FridgeCategories FridgeCategory { get; set; }
    }
}
