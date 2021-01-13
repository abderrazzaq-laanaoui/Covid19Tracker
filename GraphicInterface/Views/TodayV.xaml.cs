using GraphicInterface.ViewModeles;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphicInterface.Views
{
    /// <summary>
    /// Interaction logic for TodayV.xaml
    /// </summary>
    public partial class TodayV : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public TodayV()
        {
            InitializeComponent();
            RegionsdData(out int[] InfectedData, out int[] GueriData);
            SeriesCollection = new SeriesCollection
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



            Labels = new[] { "Tangier_Tetouan_AlHociema", "Oriental", "Fez_Meknes", "Rabat_Sale_Kenitra", "BeniMellal_Khenifra",
                "CasaBlanca_Settat", "Marrakesh_Safi", "Draa_Tafilalt", "Sous_Massa","Guelmim_OuedNoun","Laayoune_SakiaElHamra","Dakhla_OuedEddahab" };


        }

        private void RegionsdData(out int[] infectedData, out int[] gueriData)
        {
            throw new NotImplementedException();
        }

        private void GlobalBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.DataContext = new StatestiquesVM();

        }
    }
}
