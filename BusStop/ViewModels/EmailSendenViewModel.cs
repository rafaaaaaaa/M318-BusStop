using Prism.Commands;
using ProjectTemplate.Business;
using ProjectTemplate.Views;
using SwissTransport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ProjectTemplate.ViewModels
{
    class EmailSendenViewModel: INotifyPropertyChanged
    {
        //Membervariablen
        private string _absenderEmailAdresse;
        private string _empfängerEmailAdresse;
        private List<StationBoard> _stationItems;
        private string _passwort;
        private Action _close;
        private string _station;

        //Properties
        public ICommand SendenCommand { get; set; }
        public string AbsenderEmailAdresse
        {
            get
            {
                return _absenderEmailAdresse;
            }

            set
            {
                _absenderEmailAdresse = value;
                OnPropertyChanged("AbsenderEmailAdresse");
            }
        }
        public string Passwort
        {
            get
            {
                return _passwort;
            }

            set
            {
                _passwort = value;
                OnPropertyChanged("Passwort");
            }
        }
        public string EmpfängerEmailAdresse
        {
            get
            {
                return _empfängerEmailAdresse;
            }

            set
            {
                _empfängerEmailAdresse = value;
                OnPropertyChanged("EmpfängerEmailAdresse");
            }
        }

        /// <summary>
        /// Instanziert neues FahrplanViewModelObjekt
        /// </summary>
        /// <param name="stationboarditems"></param>
        /// <param name="station"></param>
        /// <param name="close"></param>
        public EmailSendenViewModel(List<StationBoard> stationboarditems, string station, Action close)
        {
            _station = station;
            _close = close;
            SendenCommand = new DelegateCommand(senden);
            _stationItems = stationboarditems;
        }

        //Methoden
        private void senden ()
        {
            bool isAbsenderAdresseOk = false;
            bool isEmpfängerAdresseOk = false;
            bool isPasswordOk = false;

            if (Passwort != null)
            {
                isPasswordOk = true;
            }


            if (isRealEmailAddress(AbsenderEmailAdresse))
            {
                isAbsenderAdresseOk = true;
            }

            if (isRealEmailAddress(EmpfängerEmailAdresse))
            {
                isEmpfängerAdresseOk = true;
            }

            if (isAbsenderAdresseOk && isEmpfängerAdresseOk)
            {
                EmailProvider provider = new EmailProvider();
                string data = GetData();
                provider.send(AbsenderEmailAdresse, EmpfängerEmailAdresse, _passwort, data);
            }

            else
            {
                if (isAbsenderAdresseOk == false || isPasswordOk == false)
                {
                    EmpfängerEmailAdresse = "";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    System.Windows.Forms.MessageBox.Show("Die EmpfängerEmailAdresse oder das Passwort ist falsch", "Validierungsfehler", buttons);
                }

                else
                {
                    AbsenderEmailAdresse = "";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    System.Windows.Forms.MessageBox.Show("Die AbsenderEmailAdresse ist falsch", "Validierungsfehler", buttons);
                }

                
            }
            
            _close.Invoke();
        }
        private string GetData()
        {
            string data = "Station: " + _station;

            foreach (var item in _stationItems)
            {
                data += "\n" + item.Name + "    " + " Abfahrt: " + item.Stop.Departure; 
            }

            return data;
        }
        private bool isRealEmailAddress(string inputEmail)
        {
            bool isReal = false;
            try
            {
                string[] host = (inputEmail.Split('@'));
                string hostname = host[1];

                IPHostEntry IPhst = Dns.GetHostEntry(hostname);
                IPEndPoint endPt = new IPEndPoint(IPhst.AddressList[0], 25);
                Socket s = new Socket(endPt.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);
                s.Connect(endPt);
                s.Close();
                isReal = true;
            }
            catch (Exception ex)
            {
                
                isReal = false;
            }

            return isReal;
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
