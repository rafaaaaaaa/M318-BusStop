using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SwissTransport;
using System.ComponentModel;
using System.Collections;
using Prism.Commands;
using ProjectTemplate.Business;
using System.Windows.Forms;
using ProjectTemplate.Views;

namespace ProjectTemplate.ViewModels
{
    class FahrplanViewModel :  INotifyPropertyChanged
    {
        //Membervariablen
        private string _inputText;
        private List<StationBoard> _stationBoardItems;
        private List<string> _comboboxItems;
        private Transport _transport;

        //Properties
        public ICommand SuchenCommand { get; set; }
        public ICommand SendDataPerEmailCommand { get; set; }
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
                    OnPropertyChanged("ComboboxItems");

                }
                catch { }
            }
        }
        public List<StationBoard> StationBoardItems
        {
            get
            {
                return _stationBoardItems;
            }

            set
            {
                _stationBoardItems = value;

                OnPropertyChanged("StationBoardItems");               
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
        public DateTime SelectedTime { get; set; }

        //Konstruktor
        /// <summary>
        /// Instanziert neues FahrplanViewModel
        /// </summary>
        public FahrplanViewModel()
        {     
            SuchenCommand = new DelegateCommand(suchen);
            SendDataPerEmailCommand = new DelegateCommand(sendDataPerEmail);
            ComboboxItems = new List<string>();
            SelectedTime = DateTime.Now;
            _transport = new Transport();
        }

        //Methoden   
        private void suchen()
        {
            SelectedTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second);

            if (SelectedTime > DateTime.Now.AddHours(1))
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                System.Windows.Forms.MessageBox.Show("Zu diesem Zeitpunkt kann kein Fahrplan generiert werden", "Validierungsfehler", buttons);
                SelectedTime = DateTime.Now;
                OnPropertyChanged("SelectedTime");
                return;
            }

            try
            {
                Station station = _transport.GetStations(InputText).StationList.First<Station>();
                StationBoardItems = _transport.GetStationBoard(station.Name, station.Id).Entries.Where(item => item.Stop.Departure >= SelectedTime).Take(6).ToList();
                OnPropertyChanged("StationBoardItems");
            }

            catch
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                System.Windows.Forms.MessageBox.Show("Diese Station ist nicht verfügbar", "Validierungsfehler", buttons);
                InputText = "";
                OnPropertyChanged("InputText");
                return;
            }

        }
        private void sendDataPerEmail()
        {
            try
            {
                string stationname = ComboboxItems.First();
                var emailwindow = new EmailWindow(StationBoardItems, stationname);
                emailwindow.ShowDialog();
                emailwindow.Close();
            }

            catch
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                System.Windows.Forms.MessageBox.Show("Export ist nicht möglich, bitte geben sie eine Station ein", "Validierungsfehler", buttons);
            }

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
