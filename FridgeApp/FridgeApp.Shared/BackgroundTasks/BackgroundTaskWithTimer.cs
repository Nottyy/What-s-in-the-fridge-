using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Background;
using Windows.UI.Popups;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using SQLite;
using FridgeApp.ViewModels;

namespace FridgeApp.BackgroundTasks
{
    public sealed class BackgroundTaskWithTimer : IBackgroundTask
    {
        public static string Name { get { return "MyBackgroundTaskName"; } }
        public const string DBNAME_ALL_PRODUCTS = "allProductsDB";

        public async static void Register()
        {
            await Task.Delay(1000);
            await BackgroundExecutionManager.RequestAccessAsync();
            var access = BackgroundExecutionManager.GetAccessStatus();

            var dialog = new MessageDialog(access.ToString());
            await dialog.ShowAsync();

            switch (access)
            {
                case BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity:
                case BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity:
                    break;
                case BackgroundAccessStatus.Denied:
                case BackgroundAccessStatus.Unspecified:
                    return;
                default:
                    break;
            }

            var existing = BackgroundTaskRegistration.AllTasks
                .Where(x => x.Value.Name.Equals(Name))
                .Select(x => x.Value)
                .FirstOrDefault();

            if (existing != null)
            {
                existing.Unregister(false);
            }

            var builder = new BackgroundTaskBuilder
            {
              Name = Name,
              CancelOnConditionLoss = false,
              TaskEntryPoint = "FridgeApp.BackgroundTasks.BackgroundTaskWithTimer"
            };

            var trigger = new TimeTrigger(15, false);
            builder.SetTrigger(trigger);

            var cond1 = new SystemCondition(SystemConditionType.InternetAvailable);
            builder.AddCondition(cond1);

            BackgroundTaskRegistration task = builder.Register();
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            DateTime currentDateTime = DateTime.Now;

            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DBNAME_ALL_PRODUCTS);

            // Retrieve Article
            AsyncTableQuery<Product> query = conn.Table<Product>().Where(x => x.ExpirationDays > 0).OrderBy(x => x.ExpirationDays);
            List<Product> result = await query.ToListAsync();

            List<string> expiredProductsNames = new List<string>();
            foreach (var pr in result)
            {
                
                if ((pr.ExpirationDate - currentDateTime).TotalDays <= 0)
                {
                    expiredProductsNames.Add(pr);
                }
            }

            if (expiredProductsNames.Count > 0)
            {
                string show = string.Format("Expired products: {0}", string.Join(", ", expiredProductsNames));
                ShowToast(, true);
            }

            
        }

        public static ToastNotification ShowToast(string content, bool show)
        {
            var template = ToastTemplateType.ToastText01;
            var xml = ToastNotificationManager.GetTemplateContent(template);
            var text = xml.CreateTextNode(content);
            var elements = xml.GetElementsByTagName("text");
            elements[0].AppendChild(text);

            var toast = new ToastNotification(xml);
            var notifier = ToastNotificationManager.CreateToastNotifier();
            if (show)
            {
                notifier.Show(toast);
            }

            return toast;
        }
    }
}
