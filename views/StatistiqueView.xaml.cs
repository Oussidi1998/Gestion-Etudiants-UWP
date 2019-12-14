using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class StatistiqueView : Page
    {
        List<Data> data ;
        public StatistiqueView()
        {
            this.InitializeComponent();
            data = new List<Data>();
            data.Add(new Data() { Category = "Apples", Value = 5 });
            data.Add(new Data() { Category = "Oranges", Value = 9 });
            data.Add(new Data() { Category = "Pineaples", Value = 8 });

            this.barSeries.DataContext = data;
        }


    }

    public class Data
    {
        public string Category { get; set; }

        public double Value { get; set; }
    }
}
