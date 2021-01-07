using System;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using System.Linq;

namespace GraphicInterface.Views
{
    /// <summary>
    /// Interaction logic for Statestiques.xaml
    /// </summary>
    public partial class Statestiques : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        //---
        public SeriesCollection SeriesCollection2 { get; set; }
        public string[] Labels2 { get; set; }
        public Func<double, string> Formatter2 { get; set; }
        public Statestiques()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 },
                    PointGeometry = DefaultGeometries.Diamond,
                    PointGeometrySize = 10
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    PointGeometry = DefaultGeometries.Square,
                },
                new LineSeries
                {
                    Title = "Series 3",
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 1
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
             
            //________

            SeriesCollection2 = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Guerés",
                    Fill = Brushes.Green,
                    Values = new ChartValues<double> { 12, 30, 49, 60 , 123, 210, 160, 80,92, 71, 160, 94}
                },
                new ColumnSeries
                {
                    Title = "Infectés",
                    Fill = Brushes.Red,
                    Values = new ChartValues<double> { 10, 50, 39, 50 , 223, 120, 60, 98,112, 100, 60, 54}
                }
                
            };


            //also adding values updates and animates the chart automatically
            /**/
            Labels2 = new[] { "Tangier_Tetouan_AlHociema", "Oriental", "Fez_Meknes", "Rabat_Sale_Kenitra", "BeniMellal_Khenifra",
                "CasaBlanca_Settat", "Marrakesh_Safi", "Draa_Tafilalt", "Sous_Massa","Guelmim_OuedNoun","Laayoune_SakiaElHamra","Dakhla_OuedEddahab" };


            DataContext = this;
        }
    }
}
