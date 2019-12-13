using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class StudentsView : Page
    {
        public List<Customer> ListCustomers { get; set; }
        public StudentsView()
        {
            this.InitializeComponent();
            ListCustomers = Customer.Customers();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder localCache = ApplicationData.Current.LocalCacheFolder;
            await Launcher.LaunchFolderAsync(localCache);
        }
    }
}
