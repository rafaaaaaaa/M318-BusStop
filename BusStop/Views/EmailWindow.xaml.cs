using ProjectTemplate.ViewModels;
using SwissTransport;
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

namespace ProjectTemplate.Views
{
    /// <summary>
    /// Interaction logic for EmailWindow.xaml
    /// </summary>
    public partial class EmailWindow : Window
    {
        private EmailSendenViewModel viewmodel;

        public EmailWindow(List<StationBoard> AbfahrtStationItems, string station)
        {
            InitializeComponent();
            viewmodel = new EmailSendenViewModel(AbfahrtStationItems,station, close);
            DataContext = viewmodel;
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            AbsaenderInputBox.Focus();
        }

  

        private void close()
        {
            this.Close();
        }


        private void PassWordInput(object sender, RoutedEventArgs e)
        {
            viewmodel.Passwort = Passwordbox.Password.ToString();
        }
    }
}
