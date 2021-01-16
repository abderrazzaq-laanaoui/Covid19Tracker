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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphicInterface.Views
{
    /// <summary>
    /// Interaction logic for Etablisement.xaml
    /// </summary>
    public partial class EtablisementV : UserControl
    {
        public EtablisementV()
        {
            InitializeComponent();

            Centres.ItemsSource = CentreDeVaccination.centres;
            Labos.ItemsSource = Laboratoire.laboratoires;
            
        }
    }
}
