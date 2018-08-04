using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GeekFlixClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        List<OutputItem> items;
        public MainPage()
        {
            this.InitializeComponent();
        }
        OutputItem thisItem;
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            object value = localSettings.Values["LastIndex"] ?? 0;
            var lastandis = (int)value;
            items = Rest.getListAsync();
            SetItem(lastandis);
        }

        private void Nxt_Click(object sender, RoutedEventArgs e)
        {
            GoToNxt();
        }
        void GoToNxt()
        {
            var index = items.IndexOf(thisItem);
            if (index >= items.Count() - 1)
            {
                AppendNewItems();
            }
            SetItem(items[index + 1]);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Rest.DeleteItem(thisItem);
            GoToNxt();
            items.Remove(thisItem);
        }
        void AppendNewItems()
        {
            var a = Rest.getListAsync(30, items.Count());
            if (a.Count() == 0)
            {
                "Done!".ShowMessage("No Items to see");
                return;
            }
            items.AddRange(a);
        }
        void SetItem(OutputItem itm)
        {
            mediaPlyr.Source = MediaSource.CreateFromUri(itm.GetDownloadLink());
            subtitle.Text = itm.Lines;
            thisItem = itm;
            crntindx.Text = items.IndexOf(thisItem).ToString();
            localSettings.Values["LastIndex"] = items.IndexOf(thisItem);

        }
        void SetItem(int index)
        {
            if (items.Count() <= index + 1)
            {
                items.AddRange(Rest.getListAsync(((index + 1) - items.Count()) + 10, items.Count()));
            }
            var item = items[index];
            SetItem(item);
        }
        private void Go_Click(object sender, RoutedEventArgs e)
        {
            var index = int.Parse(indx.Text);
            SetItem(index);
        }
    }
}
