using FridgeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace FridgeApp.Converters
{
    public class ExpiredProductsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(String))
            {
                return null;
            }

            int expirationDays = int.Parse(value.ToString());
            
            string text;
            if (expirationDays <= 0)
            {
                text = "Already EXPIRED";
            }
            else
            {
                text = "Expiring after:";
            }

            return text;

            //var obj = value as ProductsViewModel;
            //if (obj == null)
            //{
            //    return null;
            //}

            //return null;//string.Format("{4} : KCals: {0}, Proteins: {1}, Carbohydrates: {2}, Fats: {3}",
            //                        //obj.KCal, obj.Protein, obj.Carbohydrates, obj.Fats, obj.Name);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
