using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Data.SqlClient;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EznManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MobileServiceCollection<Order_Details, Order_Details> items;
        IMobileServiceTable<Order_Details> Order_DetailsObj = App.MobileService.GetTable<Order_Details>();
        System.Threading.Timer _timer;
        public MainPage()
        {
            this.InitializeComponent();
            _timer = new System.Threading.Timer(new System.Threading.TimerCallback((obj) => RefreshOrder()), null, 0, 3000);
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
           ButtonRefresh_Click(this, null);
        }
        private async Task RefreshOrder()
        {
            Order_Details or = new Order_Details();
            items = await Order_DetailsObj.Where(order_Details => (order_Details.Status == "Ordered")|| (order_Details.Status == "Delivered")).ToCollectionAsync();
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
          () =>
          {
              ListItems.ItemsSource = items;
          });
            
            
        }
        private async void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            await RefreshOrder();
        }

        private async void Proceed_Click(object sender, RoutedEventArgs e)
        {
            Button cb = (Button)sender;
            Order_Details item = cb.DataContext as Order_Details;
            item.Status = "Delivered";
            await Order_DetailsObj.UpdateAsync(item);
            await RefreshOrder();
        }
    }
}
