using FridgeApp.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace FridgeApp.ViewModels
{
    public class AppEatenProductsViewModel : BaseViewModel
    {
        private ObservableCollection<EatenProductsViewModel> eatenProducts;
        //private ICommand refreshCommand;

        public IEnumerable<EatenProductsViewModel> EatenProducts
        {
            get
            {
                if (this.eatenProducts == null)
                {
                    this.EatenProducts = new ObservableCollection<EatenProductsViewModel>();
                }
                return this.eatenProducts;
            }
            set
            {
                if (this.eatenProducts == null)
                {
                    this.eatenProducts = new ObservableCollection<EatenProductsViewModel>();
                }
                this.eatenProducts.Clear();
                foreach (var item in value)
                {
                    this.eatenProducts.Add(item);
                }
            }
        }

        //public ICommand Refresh
        //{
        //    get
        //    {
        //        if (this.refreshCommand == null)
        //        {
        //            this.refreshCommand = new RelayCommand(this.PerformRefresh);
        //        }
        //        return this.refreshCommand;
        //    }
        //}

        //private void PerformRefresh()
        //{
        //    this.LoadPhones();
        //}

        public AppEatenProductsViewModel()
        {
            //this.LoadEatenProducts();
        }

        public async Task LoadEatenProducts()
        {
            //var phoneLinq = 
            //    from phone in new ParseQuery<Phone>()
            //    where phone.Model.StartsWith("L")
            //    orderby phone.Model
            //    select phone;

            var eatenProductsQuery = await new ParseQuery<EatenProduct>()
                .FindAsync(CancellationToken.None);

            //var sds = await ParseObject.GetQuery("EatenProducts").FindAsync();
            //DateTime b = (DateTime)(sds.AsQueryable().First()["datePurchased"]);
            
            this.EatenProducts = eatenProductsQuery.AsQueryable()
                .Select(EatenProductsViewModel.FromModel);

            var s = 5;
            //var phones = await ParseObject.GetQuery("Phones")
            //        .FindAsync();
            //this.Phones = phones.AsQueryable()
            //    .Select(PhoneViewModel.FromParseObject);
        }
    }
}
