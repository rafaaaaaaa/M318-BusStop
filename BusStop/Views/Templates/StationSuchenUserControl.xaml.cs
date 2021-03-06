﻿using ProjectTemplate.ViewModels;
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

namespace ProjectTemplate.Views.Templates
{
    /// <summary>
    /// Interaction logic for StationSuchenUserControl.xaml
    /// </summary>
    public partial class StationSuchenUserControl : UserControl
    {
        public StationSuchenUserControl()
        {
            InitializeComponent();
            DataContext = new StationSuchenViewModel();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            InputCombobox.Focus();
        }
    }
}
