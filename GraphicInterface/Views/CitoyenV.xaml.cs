using Covid19Track;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Citoyens.xaml
    /// </summary>
    public partial class CitoyenV : UserControl
    {
        private ComboBox cb;
        public CitoyenV()
        {
            InitializeComponent();

            cb = (ComboBox)this.FindName("CINList");
            cb.ItemsSource = Citoyen.citoyens.Select(c => c.CIN).ToList();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            
        }

        private void CINList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb.SelectedItem != null)
            {
              
                FillData(Citoyen.citoyens.First(c => c.CIN.Equals(cb.SelectedItem)));

            }
            else
            {
                RemoveData();


            }

        }
        private void FillData(Citoyen citoyen)
        {
            ((TextBox)this.FindName("NomBox")).Text = citoyen.nom;
            ((TextBox)this.FindName("PrenomBox")).Text = citoyen.prenom;
            ((TextBox)this.FindName("DosesBox")).Text = citoyen.DosesInjectee.ToString();
            ((DatePicker)this.FindName("DateBox")).Text = citoyen.dateDeNaissance.ToShortDateString();
            ((ComboBox)this.FindName("RegionBox")).SelectedIndex = (int)citoyen.Region;
            ((ComboBox)this.FindName("EtatBox")).SelectedIndex = (int)citoyen.Etat;
            ((ComboBox)this.FindName("RegionBox")).SelectedIndex = (int)citoyen.Region;

            ((Button)this.FindName("O1Btn")).IsEnabled = true;
            ((Button)this.FindName("O2Btn")).IsEnabled = true;
            ((Button)this.FindName("O3Btn")).IsEnabled = true;
            ((Button)this.FindName("O4Btn")).IsEnabled = true;

        }
        private void RemoveData()
        {
            ((TextBox)this.FindName("NomBox")).Text = String.Empty;
            ((TextBox)this.FindName("PrenomBox")).Text = String.Empty;
            ((TextBox)this.FindName("DosesBox")).Text = String.Empty;
            ((DatePicker)this.FindName("DateBox")).Text = String.Empty;
            ((ComboBox)this.FindName("RegionBox")).SelectedIndex = -1;
            ((ComboBox)this.FindName("EtatBox")).SelectedIndex = -1;
            ((ComboBox)this.FindName("RegionBox")).SelectedIndex = -1;

            ((Button)this.FindName("O1Btn")).IsEnabled = false;
            ((Button)this.FindName("O2Btn")).IsEnabled = false;
            ((Button)this.FindName("O3Btn")).IsEnabled = false;
            ((Button)this.FindName("O4Btn")).IsEnabled = false;

        }
    }
}
