
using SQLitePCL;
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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Main : Page
    {
        private int id_user;
        public Main()
        {
            this.InitializeComponent();
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            FrameNavigationOptions navOptions = new FrameNavigationOptions();
            navOptions.TransitionInfoOverride = args.RecommendedNavigationTransitionInfo;

            TextBlock ItemContent = args.InvokedItem as TextBlock;
            if (ItemContent != null)
            {
                switch (ItemContent.Tag)
                {
                    case "etudiants":
                        contentFrame.Navigate(typeof(StudentsView));
                        break;

                    case "filieres":
                        contentFrame.Navigate(typeof(FilieresView));
                        break;

                    case "cours":
                        contentFrame.Navigate(typeof(CoursView));
                        break;

                    case "absences":
                        contentFrame.Navigate(typeof(AbsencesView));
                        break;

                    default:
                        contentFrame.Navigate(typeof(Main));
                        break;
                }
            }
        }

        private void MainMenu_Loaded(object sender, RoutedEventArgs e)
        {
            /*// set the initial SelectedItem
            foreach (NavigationViewItemBase item in MainMenu.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "etudiants")
                {
                    MainMenu.SelectedItem = item;
                    break;
                }
            }*/
            contentFrame.Navigate(typeof(StudentsView));
        }


    }
}
