using Covid19Track;
using QRCoder;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
namespace GraphicInterface.Views
{
    /// <summary>
    /// Interaction logic for Citoyens.xaml
    /// </summary>
    public partial class CitoyenV : UserControl
    {
        public CitoyenV()
        {
            InitializeComponent();
            CINList.ItemsSource = Citoyen.citoyens.Select(c => c.CIN).ToList();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }

        private void CINList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CINList.SelectedItem != null)
            {
                FillData(Citoyen.citoyens.First(c => c.CIN.Equals(CINList.SelectedItem)));
            }
            else
            {
                RemoveData();
            }

        }
        private void FillData(Citoyen citoyen)
        {
            DataBox.IsEnabled = true;
            DataBox.SelectedIndex = -1;
            DataViewer.ItemsSource = null;
            NomBox.Text = citoyen.nom;
            PrenomBox.Text = citoyen.prenom;
            DosesBox.Text = citoyen.DosesInjectee.ToString();
            DateBox.Text = citoyen.dateDeNaissance.ToShortDateString();
            RegionBox.SelectedIndex = (int)citoyen.Region;
            EtatBox.SelectedIndex = (int)citoyen.Etat;

            O1Btn.IsEnabled = true;
            O2Btn.IsEnabled = true;
            O3Btn.IsEnabled = true;
            O4Btn.IsEnabled = true;

            SetQRCode(citoyen);
        }
        private void RemoveData()
        {
            DataBox.IsEnabled = false;
            NomBox.Text = string.Empty;
            PrenomBox.Text = string.Empty;
            DosesBox.Text = string.Empty;
            DateBox.Text = string.Empty;
            RegionBox.SelectedIndex = -1;
            EtatBox.SelectedIndex = -1;

            O1Btn.IsEnabled = false;
            O2Btn.IsEnabled = false;
            O3Btn.IsEnabled = false;
            O4Btn.IsEnabled = false;
            DataBox.SelectedIndex = -1;
            DataBox.IsEnabled = false;

        }
        private void SetQRCode(Citoyen citoyen)
        {
            Color color;
            if (citoyen.Etat == Etats.Infecte)
                color = Color.Red;
            else if (citoyen.Etat == Etats.Vaccine)
                color = Color.Green;
            else if (citoyen.Etat == Etats.Sain)
                color = Color.Lime;
            else if (citoyen.Etat == Etats.Soupconne)
                color = Color.Orange;
            else if (citoyen.Etat == Etats.Inconnu)
                color = Color.Gray;
            else
                color = Color.Black;


            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(citoyen.ConsultationEtat(), QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(qRCodeData);
            Bitmap qrCodeImg = qRCode.GetGraphic(20, color, Color.White, true);
            QRImage.Source = Bitmap2imageSource(qrCodeImg);

        }


        private ImageSource Bitmap2imageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private void O1Btn_Click(object sender, RoutedEventArgs e)
        {
            var tmpC = Citoyen.citoyens.First(c => c.CIN == CINList.SelectedItem.ToString());
            var centreDialog = new CentreDialogV();
            centreDialog.Owner = (Window)this.Parent;
            string r = string.Empty;
            if (centreDialog.ShowDialog() == true)
                r = centreDialog.Result();
             
            if (!string.IsNullOrEmpty(r))
            {
                CentreDeVaccination.centres.First(l => l.reference == r).InjecteDose(tmpC);
                string message;
                if (tmpC.DosesInjectee == 1)
                {
                    message = "1er dose de vaccine bien injectée ";
                }
                else
                {
                    message = "Citoyen Bien Vaccinner ";
                }
                MessageBox.Show(message);
                FillData(tmpC);
            }
        }

        private void O2Btn_Click(object sender, RoutedEventArgs e)
        {
            var tmpC = Citoyen.citoyens.First(c => c.CIN == CINList.SelectedItem.ToString());
            tmpC.SeConfiner();
            MessageBox.Show("Le Confinement à été commancer , Il sera terminer dans 10 jours");

        }

        private void O3Btn_Click(object sender, RoutedEventArgs e)
        {
            var This = Citoyen.citoyens.First(c => c.CIN == CINList.SelectedItem.ToString());
            var citoyenDialog = new CitoyenDialogV();
            citoyenDialog.Owner = (Window)this.Parent;
            string r = string.Empty;
            if (citoyenDialog.ShowDialog() == true)
                r = citoyenDialog.Result();
            if (!string.IsNullOrEmpty(r))
            {
                var Other = Citoyen.citoyens.First(c => c.CIN == r);
                This.Contacter(Other);
                MessageBox.Show($"{This.nom} {This.prenom} à bien contacter {Other.nom} {Other.prenom}");
                FillData(Citoyen.citoyens.First(c => c.CIN == CINList.SelectedItem.ToString()));
            }
        }
        private void O4Btn_Click(object sender, RoutedEventArgs e)
        {
            var tmpC = Citoyen.citoyens.First(c => c.CIN == CINList.SelectedItem.ToString());
            var laboDialog = new LaboDialogV();
            laboDialog.Owner = (Window)this.Parent;
            string r = string.Empty;
            if (laboDialog.ShowDialog() == true)
                r = laboDialog.Result();

            if (!string.IsNullOrEmpty(r))
            {
                bool resultatTest = Laboratoire.laboratoires.First(l => l.reference == r).TestPCR(tmpC);
                string resultatText;
                if (resultatTest)
                    resultatText = "Postive";
                else
                    resultatText = "Negative";

                MessageBox.Show("Resultat de test est " + resultatText, "Information !", MessageBoxButton.OK);
                FillData(tmpC);

            }
        }

        private void DataViewer_AutoGeneratedColumns(object sender, EventArgs e)
        {

        }

        private void DataViewer_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CINList.SelectedItem == null || DataBox.SelectedItem == null)
                return;

            Citoyen citoyen;

            citoyen= Citoyen.citoyens.First(c => c.CIN.Equals(CINList.SelectedItem.ToString()));
            string selected = (string)((ComboBoxItem)DataBox.SelectedItem).Content;
            if (selected == "Isolations")
            {
                DataViewer.ItemsSource = citoyen.Isolations;

            }
            else if (selected == "Records")
            {
                DataViewer.ItemsSource = citoyen.Records;

            }
            else if (selected == "Rencontres")
            {
                DataViewer.ItemsSource = citoyen.Rencontres;

            }
            else if (selected == "Testes")
            {
                DataViewer.ItemsSource = citoyen.Tests;

            }
            else
                return;
        }
    }
}
