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
using ProjectTemplate.ViewModels;

namespace ProjectTemplate.Views.Templates
{
    /// <summary>
    /// Interaction logic for FahrPlanUserControl.xaml
    /// </summary>
    public partial class FahrPlanUserControl : UserControl
    {
        /// <summary>
        /// Instanziert 
        /// </summary>
        public FahrPlanUserControl()
        {
            InitializeComponent();
            DataContext = new FahrplanViewModel();
        }
        
        private void Load(object sender, RoutedEventArgs e)
        {
            InputCombobox.Focus();    
        }
    }
}
