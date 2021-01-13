using Covid19Track;
using GraphicInterface.ViewModeles;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphicInterface.Views
{
    /// <summary>
    /// Interaction logic for Statestiques.xaml
    /// </summary>
    public partial class StatestiquesV : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        //---
        public SeriesCollection SeriesCollection2 { get; set; }
        public string[] Labels2 { get; set; }
        public Func<double, string> Formatter2 { get; set; }
        public StatestiquesV()
        {
            InitializeComponent();
            var recordes = RecordDAO.SelectAll();
            Labels = recordes.Select(r => r.date.ToShortDateString()).Distinct().ToList();
            ChartValues<int> inf = new ChartValues<int>(), vac = new ChartValues<int>(), sai = new ChartValues<int>();
            for (int i = 0; i < Labels.Count; i++)
            {
                inf.Add(recordes.Where(r => r.date.ToShortDateString() == Labels[i]
                                       && r.etat == Etats.Infecte).Count());
                vac.Add(recordes.Where(r => r.date.ToShortDateString() == Labels[i]
                                       && r.etat == Etats.Vaccine).Count());
                sai.Add(recordes.Where(r => r.date.ToShortDateString() == Labels[i]
                                       && r.etat == Etats.Sain).Count());
                ;
            }
            SeriesCollection = new SeriesCollection
            {

                new LineSeries
                {
                    Title = "\nCas\nInfectés\n",
                    Values = inf,
                    Stroke = Brushes.Red,
                    PointGeometry = DefaultGeometries.Diamond,
                    PointGeometrySize = 10
                },
                new LineSeries
                {
                    Title = "\nCas\nGuéris\n",
                    Values = sai,
                    Stroke = Brushes.Lime,
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 10

                },
                new LineSeries
                {
                    Title = "\nCas\nVaccinés\n",
                    Values = vac,Stroke = Brushes.Green,
                    PointGeometry = DefaultGeometries.Triangle,
                    PointGeometrySize = 10
                }
            };

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



            Labels2 = new[] { "Tangier_Tetouan_AlHociema", "Oriental", "Fez_Meknes", "Rabat_Sale_Kenitra", "BeniMellal_Khenifra",
                "CasaBlanca_Settat", "Marrakesh_Safi", "Draa_Tafilalt", "Sous_Massa","Guelmim_OuedNoun","Laayoune_SakiaElHamra","Dakhla_OuedEddahab" };


            DataContext = this;
        }

        private void RegionsdData(out int[] RegionsInfectedData, out int[] RegionsGueriData)
        {
            RegionsInfectedData = new int[12];
            RegionsGueriData = new int[12];
            for (int i = 0; i < RegionsInfectedData.Length; i++)
            {
                RegionsInfectedData[i] = 0;
                RegionsGueriData[i] = 0;
                for (int j = 0; j < Citoyen.citoyens.Count; j++)
                {
                    var tmpC = Citoyen.citoyens[j];
                    if (tmpC.Region == (Regions)i)
                    {
                        //I
                        RegionsInfectedData[i] += tmpC.Records.Where(r => r.etat == Etats.Infecte).Count();

                        //G
                        for (int k = 0; k < tmpC.Records.Count - 1; k++)
                        {
                            if (tmpC.Records[k].etat == Etats.Infecte && tmpC.Records[k + 1].etat == Etats.Sain)
                            {
                                RegionsGueriData[i]++;
                            }
                        }
                    }
                }

            }
        }

        private void TodayBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Application.Current.MainWindow.DataContext = new TodayVM();

        }
    }
}
