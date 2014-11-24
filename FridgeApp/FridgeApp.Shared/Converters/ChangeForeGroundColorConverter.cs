using FridgeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace FridgeApp.Converters
{
    public class ChangeForeGroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(Brush))
            {
                return null;
            }

            int expirationDays = int.Parse(value.ToString());
            Brush brush;
            if (expirationDays <= 0 )
            {
                brush = new SolidColorBrush { Color = Colors.Red };
            }
            else
            {
                brush = new SolidColorBrush { Color = Colors.Black };
            }

            return brush;
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
