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
    public partial class CitoyenDialogV : Window
    {
        private string rfr ;
        public CitoyenDialogV()
        {
            InitializeComponent();
            Citoyens.ItemsSource = (from c in Citoyen.citoyens
                                   select new
                                   {
                                       Name = c.nom + " " + c.prenom,
                                       CIN = c.CIN
                                   }).ToList();

            Citoyens.DisplayMemberPath = "Name";
            Citoyens.SelectedValuePath = "CIN";
            
   
        }

        private void Centres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Citoyens.SelectedItem != null)
            {
                ValiderBtn.IsEnabled = true;
                rfr = Citoyens.SelectedValue.ToString();
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
