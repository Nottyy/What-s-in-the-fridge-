using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridgeApp.Models
{
    [ParseClassName("EatenProducts")]
    public class EatenProduct : ParseObject
    {
        [ParseFieldName("expirationDays")]
        public int ExpirationDays
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        [ParseFieldName("name")]
        public string Name
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("quantity")]
        public double Quantity
        {
            get { return GetProperty<double>(); }
            set { SetProperty<double>(value); }
        }

        [ParseFieldName("datePurchased")]
        public DateTime DatePurchased
        {
            get { return GetProperty<DateTime>(); }
            set { SetProperty<DateTime>(value); }
        }

        [ParseFieldName("description")]
        public string Description
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("fridgeCategory")]
        public FridgeCategories FridgeCategory
        {
            get { return GetProperty<FridgeCategories>(); }
            set { SetProperty<FridgeCategories>(value); }
        }
    }
}
