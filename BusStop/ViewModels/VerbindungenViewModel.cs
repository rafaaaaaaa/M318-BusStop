using Prism.Commands;
using SwissTransport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Dragablz;
using System.Windows;
using System.Windows.Forms;
using System.Globalization;

namespace ProjectTemplate.ViewModels
{
    class VerbindungenViewModel : INotifyPropertyChanged
    {
        //Membervariablen
        private string _inputText1;
        private string _inputText2;
        private List<string> _comboboxItems1;
        private List<string> _comboboxItems2;
        private Transport _transport;
        private List<Connection> _connectionsItems;

        //Properties
        public ICommand SuchenCommand { get; set; }
        public string InputText1
        {
            get
            {
                return _inputText1;
            }

            set
            {
                _inputText1 = value;
                try
                {
                    ComboboxItems1 = _transport.GetStations(value).StationList.Select(x => x.Name).ToList();
                    OnPropertyChanged("ComboboxItems1");

                }
                catch { }
            }
        }
        public string InputText2
        {
            get
            {
                return _inputText2;
            }

            set
            {
                _inputText2 = value;
                try
                {
                    ComboboxItems2 = _transport.GetStations(value).StationList.Select(x => x.Name).ToList();
                    OnPropertyChanged("ComboboxItems2");

                }
                catch { }
            }
        }
        public List<string> ComboboxItems1
        {
            get
            {
                return _comboboxItems1;
            }

            set
            {
                _comboboxItems1 = value;
                OnPropertyChanged("ComboboxItems1");
            }
        }
        public List<string> ComboboxItems2
        {
            get
            {
                return _comboboxItems2;
            }

            set
            {
                _comboboxItems2 = value;
                OnPropertyChanged("ComboboxItems2");
            }
        }
        public DateTime SelectedTime { get; set; }
        public DateTime SelectedDate { get; set; }
        public List<Connection> ConnectionsItems
        {
            get
            {
                return _connectionsItems;
            }

            set
            {
                _connectionsItems = value;
                OnPropertyChanged("ConnectionsItems");
            }
        }

        /// <summary>
        /// Instanziert neues VerbindungenViewModel
        /// </summary>
        public VerbindungenViewModel()
        {
            SuchenCommand = new DelegateCommand(suchen);
            ComboboxItems1 = new List<string>();
            ComboboxItems2 = new List<string>();
            _transport = new Transport();
            SelectedTime = DateTime.Now;
            SelectedDate = DateTime.Now;
        }

        //Methoden
        private void suchen()
        {
            SelectedTime = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second);
            Station station1;
            Station station2;

            try
            {
                station1 = _transport.GetStations(InputText1).StationList.First<Station>();   
            }

            catch
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                System.Windows.Forms.MessageBox.Show("Diese Abfahrtstation ist nicht verfügbar", "Validierungsfehler", buttons);
                InputText1 = "";
                OnPropertyChanged("InputText1");
                return;
            }

            try
            {
                station2 = _transport.GetStations(InputText2).StationList.First<Station>();
            }

            catch
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                System.Windows.Forms.MessageBox.Show("Diese Ankunftsstation ist nicht verfügbar", "Validierungsfehler", buttons);
                InputText2 = "";
                OnPropertyChanged("InputText2");
                return;
            }

            ConnectionsItems = _transport.GetConnections(station1.Name, station2.Name, SelectedTime).ConnectionList; 

            
            foreach (var item in ConnectionsItems)
            {
                item.From.Departure = Convert.ToDateTime(item.From.Departure).Hour.ToString() + ":" + Convert.ToDateTime(item.From.Departure).Minute.ToString();

                String[] substrings1 = item.From.Departure.Split(':');
                if (substrings1[0].Length == 1)
                {
                    substrings1[0] = substrings1[0] + "0";
                }

                if (substrings1[1].Length == 1)
                {
                    substrings1[1] =  substrings1[1] + "0";
                }

                if (Int32.Parse(substrings1[0]) > 24)
                {
                    char[] digits = substrings1[0].ToCharArray();
                    char firstDigit = digits[0];
                    digits[0] = digits[digits.Length - 1];
                    digits[digits.Length - 1] = firstDigit;
                    Console.WriteLine(new string(digits));

                    substrings1[0] = digits[0].ToString() + digits[1].ToString();
                }

                item.From.Departure = substrings1[0] + ":" + substrings1[1];

               String[] substrings2 = item.Duration.Split(':');
               item.Duration = substrings2[0].Substring(substrings2[0].Length - 1, 1) + " h " + substrings2[1] + " m";           
            }

            OnPropertyChanged("ConnectionsItems");

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
