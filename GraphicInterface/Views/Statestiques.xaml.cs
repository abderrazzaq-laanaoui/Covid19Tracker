using System;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using Covid19Track;
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
            RegionsdData(out int[] InfectedData, out int[] GueriData);
            SeriesCollection2 = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Guéris",
                    Fill = Brushes.Green,
                    Values = new ChartValues<double> { GueriData[0], GueriData[1], GueriData[2], GueriData[3], GueriData[4], GueriData[5], GueriData[6], GueriData[7], GueriData[8], GueriData[9], GueriData[10], GueriData[11] }
                },
                new ColumnSeries
                {
                    Title = "Infectés",
                    Fill = Brushes.Red,
                    Values = new ChartValues<int> { InfectedData[0], InfectedData[1], InfectedData[2], InfectedData[3], InfectedData[4], InfectedData[5], InfectedData[6], InfectedData[7], InfectedData[8], InfectedData[9], InfectedData[10], InfectedData[11]}
                } 
            };


            //also adding values updates and animates the chart automatically
            /**/
            Labels2 = new[] { "Tangier_Tetouan_AlHociema", "Oriental", "Fez_Meknes", "Rabat_Sale_Kenitra", "BeniMellal_Khenifra",
                "CasaBlanca_Settat", "Marrakesh_Safi", "Draa_Tafilalt", "Sous_Massa","Guelmim_OuedNoun","Laayoune_SakiaElHamra","Dakhla_OuedEddahab" };


            DataContext = this;
        }

        private void RegionsdData(out int[] RegionsInfectedData, out int[] RegionsGueriData)
        {
            RegionsInfectedData = new int[12];
            RegionsGueriData = new int[12];
            for(int i = 0; i < RegionsInfectedData.Length; i++)
            {
                RegionsInfectedData[i] =  Covid19Track.Citoyen.citoyens.Where(c => c.Region.Equals((Regions)i))
                                                                       .Where(c => c.Etat.Equals(Etats.Infecte))
                                                                       .Count();
                /*-|        -|*/
                RegionsGueriData[i] =  Covid19Track.Citoyen.citoyens.Where(c => c.Region.Equals((Regions)i))
                                                                    .Where(c => c.Etat.Equals(Etats.Saint))
                                                                    .Where(c => c.Records.ElementAt(c.Records.Count - 2).Equals(Etats.Infecte))
                                                                    .Count();
            }
        }
    }
}
