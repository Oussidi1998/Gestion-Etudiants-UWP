using SQLitePCL;
using System;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App2
{
    public sealed partial class MainPage : Page
    {
        private SQLiteConnection con = new SQLiteConnection("database_uwp.db");
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string query = @"Select * from Users where login='" + userInput.Text + "' and password='" + mtpInput.Password + "'";
            ISQLiteStatement stmt = con.Prepare(query);
            int nbr = 0;
            while (stmt.Step() == SQLiteResult.ROW) nbr++;
                
            if (nbr <= 0){
               var messageDialog = new MessageDialog("Le nom d'utilisateur ou  mot de passe incorrect");
               await messageDialog.ShowAsync();
             }else{
                if (remember_me.IsChecked == true)
                {
                    localSettings.Values["remember"] = DateTime.Today.ToString();
                }

                Frame.Navigate(typeof(Main));

              }
            
        }

    }
}
