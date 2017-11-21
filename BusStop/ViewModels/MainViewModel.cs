using Prism.Commands;
using ProjectTemplate.Views.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectTemplate.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        //Membervariablen
        private UserControl _aktuellesTab;

        //Properties
        public UserControl AktuellesTab 
        {
            get
            {
                return _aktuellesTab;
            }
            set
            {
                _aktuellesTab = value;
                OnPropertyChanged("AktuellesTab");
            }
        }   
        public ICommand NavigateToFahrplanCommand { get; set; }
        public ICommand NavigateToVerbindungenCommand { get; set; }
        public ICommand NavigateToOrtFindenCommand { get; set; }
        public ICommand NavigateToStationSuchenCommand { get; set; }

        /// <summary>
        /// Instaniziert neues MainViewModel
        /// </summary>
        public MainViewModel()
        {
            NavigateToFahrplanCommand = new DelegateCommand(navigateToFahrplan);
            NavigateToVerbindungenCommand = new DelegateCommand(navigateToVerbindungen);
            NavigateToOrtFindenCommand = new DelegateCommand(navigateToOrtFinden);
            NavigateToStationSuchenCommand = new DelegateCommand(navigateToStationSuchen);
       
        }

        //Methoden
        private void navigateToFahrplan ()
        {
            AktuellesTab = new FahrPlanUserControl();            
        }
        private void navigateToVerbindungen()
        {
            AktuellesTab = new VerbindungenUserControl();
         }
        private void navigateToOrtFinden()
        {
            AktuellesTab = new OrtFindenUserControl();
        }
        private void navigateToStationSuchen()
        {
            AktuellesTab = new StationSuchenUserControl();
        }
        /// <summary>
        /// Stosst ein Event an, welches das UI aktualisiert (nach bestimmten Propertie)
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        //Events
        /// <summary>
        /// Event, welches eine Änderung an einem Property registriert
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
