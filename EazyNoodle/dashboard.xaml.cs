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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EazyNoodle
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class dashboard : Page
    {
        public dashboard()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(order));
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "ยืนยัน?",
                Content = "ต้องการเรียกเก็บเงินใช่หรือไม่",
                PrimaryButtonText = "ใช่",
                CloseButtonText = "ไม่"
            };
            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            { ConfirmDialog(); ; }
             
        }
        private async void ConfirmDialog()
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "เรียกเก็บเงินเรียบร้อย",
                Content = "ขอบคุณที่ใช้บริการ",
                PrimaryButtonText = "ตกลง"
            };
            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            { Frame.Navigate(typeof(welcome)); ; }
        }
    }
}
