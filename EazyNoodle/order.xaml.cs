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
using Windows.UI.Popups;
using System.Data.SqlClient;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EazyNoodle
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class order : Page
    {
        public order()
        {
            this.InitializeComponent();

        }


        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
            Frame.GoBack();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            if (orderList.Text == string.Empty)
                ListmissingDialog();
           else ConfirmDialog();
        }

        

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            if (
                ((id_311.IsChecked == true || id_312.IsChecked == true || id_313.IsChecked == true || id_314.IsChecked == true || id_315.IsChecked == true) || 
                (id_321.IsChecked == true || id_322.IsChecked == true || id_323.IsChecked == true || id_324.IsChecked == true || id_325.IsChecked == true)) &&
                (id_11.IsChecked == true || id_12.IsChecked == true || id_13.IsChecked == true || id_14.IsChecked == true || id_15.IsChecked == true) && 
                (id_51.IsChecked == true || id_52.IsChecked == true || id_53.IsChecked == true))
                {
                OrderAssignment1();
                OrderAssignment2();
                OrderAssignment3();
                OrderAssignment4();

            }
            else missingDialog();

            

        }
        private void OrderAssignment1()
        {
            orderList.Text += "ก๋วยเตี๋ยว";
            if ((id_311.IsChecked == true || id_312.IsChecked == true || id_313.IsChecked == true || id_314.IsChecked == true || id_315.IsChecked == true) &&
                    (id_321.IsChecked == true || id_322.IsChecked == true || id_323.IsChecked == true || id_324.IsChecked == true || id_325.IsChecked == true))
                orderList.Text += "หมู+เนื้อ";
            else if (id_311.IsChecked == true || id_312.IsChecked == true || id_313.IsChecked == true || id_314.IsChecked == true || id_315.IsChecked == true)
                orderList.Text += "หมู";
            else if (id_321.IsChecked == true || id_322.IsChecked == true || id_323.IsChecked == true || id_324.IsChecked == true || id_325.IsChecked == true)
                orderList.Text += "เนื้อ";
            
        }
        private void OrderAssignment2()
        {
            if (id_11.IsChecked == true)
                orderList.Text += "น้ำใส";
            else if (id_12.IsChecked == true)
                orderList.Text += "ต้มยำใส";
            else if (id_13.IsChecked == true)
                orderList.Text += "ต้มยำข้น";
            else if (id_14.IsChecked == true)
                orderList.Text += "น้ำตก";

        }
        private void OrderAssignment3()
        {
            if (id_41.IsChecked == false && id_42.IsChecked == false)
                orderList.Text += "ไม่ผัก";
        }

        private void OrderAssignment4()
        {
            if (id_51.IsChecked == true)
                orderList.Text += " เล็ก";
            else if (id_52.IsChecked == true)
                orderList.Text += " กลาง";
            else if (id_53.IsChecked == true)
                orderList.Text += " ใหญ่";
            orderList.Text += " " + Qty.Value.ToString() + " ชาม  \n";
        }

        private async void missingDialog()
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "ผิดพลาด",
                Content = "กรุณาเลือกน้ำซุป เส้น เนื้อสัตว์ และ ขนาดให้ครบก่อนสั่งครับ",
                CloseButtonText = "เข้าใจแล้ว",
            };
            ContentDialogResult result = await dialog.ShowAsync();

        }
        private async void ListmissingDialog()
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "ผิดพลาด",
                Content = "กรุณาเพิ่มรายการก่อนทำการสั่งอาหาร",
                CloseButtonText = "เข้าใจแล้ว",
            };
            ContentDialogResult result = await dialog.ShowAsync();

        }

        private async void ConfirmDialog()
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "ยืนยันการสั่งอาหารใช่หรือไม่ ?",
                Content = "หลังจากกดปุมยืนยัน จะไม่สามารถยกเลิกหรือแก้ไขได้",
                CloseButtonText = "ยกเลิก",
                PrimaryButtonText = "ยืนยัน"
            };
            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            { DBAdd(); Frame.Navigate(typeof(dashboard)); }
        }


        private void Qty_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {

            QtyLabel.Text = Qty.Value.ToString();


        }

        private void clearOrder_Click(object sender, RoutedEventArgs e)
        {
            DBDel();
            orderList.Text = string.Empty;
        }

        private MobileServiceCollection<Order_Details, Order_Details> items;
        IMobileServiceTable<Order_Details> Order_DetailsObj = App.MobileService.GetTable<Order_Details>();
        int ID ;
        public async void DBAdd()
        {
            await RefreshOrder();
            Order_Details obj = new Order_Details();
            obj.MachineID = "Virtual";
            obj.Status = "Ordered";
            //obj.Status = "0";
            obj.OrderID = ID;
            //obj.OrderID = 0;
            obj.OrderDescription = orderList.Text;
            await Order_DetailsObj.InsertAsync(obj);
        }

        private async Task RefreshOrder()
        {
            
            items = await Order_DetailsObj.Where(order_Details => (order_Details.Status == "Ordered") ||
            (order_Details.Status == "Delivered")|| (order_Details.Status == "0")).ToCollectionAsync();
            ID = items.Last().OrderID + 1;
            
        }

        public void DBDel()
        {
           
        }
    }
}
