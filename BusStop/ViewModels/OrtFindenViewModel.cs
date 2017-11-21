using Prism.Commands;
using SwissTransport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Device.Location;
using ProjectTemplate.Business;
using Microsoft.Maps.MapControl.WPF;
using System.Windows.Forms;


namespace ProjectTemplate.ViewModels
{
    class OrtFindenViewModel : INotifyPropertyChanged
    {
        //Membervariablen
        private List<string> _comboboxItems;
        private string _inputText;
        private bool _isDropDownOpen;
        private Transport _transport;
        private Location _coordinates;
        private Locator _locator;
        private Action<Location, string> _load;

        //Properties             
        public string InputText
        {
            get
            {
                return _inputText;
            }

            set
            {
                _inputText = value;
                try
                {
                    ComboboxItems = _transport.GetStations(value).StationList.Select(x => x.Name).ToList();
                    OnPropertyChanged("ComboboxItems1");

                }
                catch { }
            }
        }
        public List<string> ComboboxItems
        {
            get
            {
                return _comboboxItems;
            }

            set
            {
                _comboboxItems = value;
                OnPropertyChanged("ComboboxItems");
            }
        }
        public bool IsDropDownOpen
        {
            get
            {
                return _isDropDownOpen;
            }

            set
            {
                _isDropDownOpen = value;
                OnPropertyChanged("IsDropDownOpen");
            }
        }
        public ICommand Suchen { get; set; }
        public ICommand FindeOrteCommand { get; set; }
        public Location Coordinates
        {
            get
            {
                return _coordinates;
            }
            set
            {
                _coordinates = value;
                OnPropertyChanged("Coordinates");
            }
        }


       /// <summary>
       ///  Instanziert neues OrtFindenViewModel
       /// </summary>
       /// <param name="Load"></param>
        public OrtFindenViewModel(Action<Location, string> Load)
        {
            ComboboxItems = new List<string>();
            Suchen = new DelegateCommand(suchen);
            FindeOrteCommand = new DelegateCommand(findeort);
            _transport = new Transport();
            _locator = new Locator();
            _load = Load;
            Coordinates = _locator.SearchCurrentLocation();
        }

        //Methoden
        private void suchen()
        {            
                Stations station = _transport.GetStations(InputText);
                    if (station.StationList.Count == 0)
                    {
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        System.Windows.Forms.MessageBox.Show("Diese Station exisitert nicht", "Validierungsfehler", buttons);
                        InputText = "";
                        OnPropertyChanged("InputText");
                        return;
                    }
                Station currentStation = station.StationList.Where(x => x.Name == InputText).First<Station>();
            
           
            double xcoord = currentStation.Coordinate.XCoordinate;
            double ycoord = currentStation.Coordinate.YCoordinate;

            Coordinates.Latitude = xcoord;
            Coordinates.Longitude = ycoord;
            OnPropertyChanged("Coordinates");
            _load.Invoke(Coordinates, currentStation.Name);
        }
        private void findeort()        
        {
            Location standartlocation = _locator.SearchCurrentLocation();
            _load.Invoke(standartlocation, "ihr Standort");
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
