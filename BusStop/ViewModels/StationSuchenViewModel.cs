using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using SwissTransport;
using System.ComponentModel;
using System.Windows.Input;
using Prism.Commands;
using ProjectTemplate.Business;
using ProjectTemplate.Views;

namespace ProjectTemplate.ViewModels
{   
    class StationSuchenViewModel: INotifyPropertyChanged
    {
        //Membervariablen        
        private string _inputText1;
        private string _inputText2;
        private Transport _transport;

        //Properties
        public List<Station> AnfahrtStationItems { get; set; }
        public List<Station> AbfahrtStationItems { get; set; }
        public string Inpputtext1
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
                    AnfahrtStationItems = _transport.GetStations(value).StationList;



                    if (AnfahrtStationItems.Count == 0)
                    {
                        AnfahrtStationItems.Add(new Station() { Name = "keine Anfahrtstation gefunden" });
                    }

                    OnPropertyChanged("Inpputtext1");
                    OnPropertyChanged("AnfahrtStationItems");

                }
                catch { }
            }
        }
        public string Inpputtext2
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
                    AbfahrtStationItems = _transport.GetStations(value).StationList;

                    if (AbfahrtStationItems.Count == 0)
                    {
                        AbfahrtStationItems.Add(new Station() { Name = "keine Abfahrtstation gefunden" });
                    }

                    OnPropertyChanged("Inpputtext2");
                    OnPropertyChanged("AbfahrtStationItems");

                }
                catch { }
            }
        }

       /// <summary>
        /// Instanziert neues StationSuchenViewModel
       /// </summary>
        public StationSuchenViewModel()
        {
            AnfahrtStationItems = new List<Station>();
            AbfahrtStationItems = new List<Station>();
            _transport = new Transport();
        }

        //Methoden
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
