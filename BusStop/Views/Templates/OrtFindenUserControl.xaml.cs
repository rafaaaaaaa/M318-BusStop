using ProjectTemplate.ViewModels;
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
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Prism.Commands;
using ProjectTemplate.Business;
using Microsoft.Maps.MapControl.WPF;
using Microsoft;
using System.Globalization;


namespace ProjectTemplate.Views.Templates
{
    /// <summary>
    /// Interaction logic for OrtFindenUserControl.xaml
    /// </summary>
    public partial class OrtFindenUserControl : UserControl
    {

        OrtFindenViewModel vm;
        bool isfirst;
        Location loc;
        string stationname;

        public OrtFindenUserControl()
        {
            isfirst = true;
            InitializeComponent();
            vm = new OrtFindenViewModel(load);
            DataContext = vm;
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            InputCombobox.Focus();

            if (isfirst)
            {
                var x = new Locator();
                Location standartlocation = x.SearchCurrentLocation();
                myMap.SetView(vm.Coordinates, 16);
                isfirst = false;
                return;
            }       
            myMap.SetView(loc, 16);
            myMap.Children.Add(new Pushpin(){Location = loc, ToolTip = stationname});
        }

        private void load(Location l, string stationnamme)
        {           
            stationname = stationnamme;
            loc = l;
            object s = new Object();
            RoutedEventArgs e = new RoutedEventArgs();
            Load(s,e);
        }


    } 
}
