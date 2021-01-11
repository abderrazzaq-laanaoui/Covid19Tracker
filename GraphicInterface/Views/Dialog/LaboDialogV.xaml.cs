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
    public partial class LaboDialogV : Window
    {
        private string rfr ;
        public LaboDialogV()
        {
            InitializeComponent();
            Labos.ItemsSource = Laboratoire.laboratoires;
            Labos.DisplayMemberPath = "nom";
            Labos.SelectedValuePath = "reference";
   
        }

        private void Centres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Labos.SelectedItem != null)
            {
                ValiderBtn.IsEnabled = true;
                rfr = Labos.SelectedValue.ToString();
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
