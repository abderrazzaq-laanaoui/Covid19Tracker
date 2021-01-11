using Covid19Track;
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
using System.Windows.Shapes;

namespace GraphicInterface.Views
{
    /// <summary>
    /// Interaction logic for LaboDialogV.xaml
    /// </summary>
    public partial class CentreDialogV : Window
    {
        private string rfr ;
        public CentreDialogV()
        {
            InitializeComponent();
            Centres.ItemsSource = CentreDeVaccination.centres;
            Centres.DisplayMemberPath = "nom";
            Centres.SelectedValuePath = "reference";
   
        }

        private void Centres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Centres.SelectedItem != null)
            {
                ValiderBtn.IsEnabled = true;
                rfr = Centres.SelectedValue.ToString();
            }
            else
            {
                ValiderBtn.IsEnabled = false;
            }
        }
        public string Result()
        {
            return rfr;
        }

        private void ValiderBtn_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DialogResult = true;
            Window.GetWindow(this).Close();
        }
    }
}
