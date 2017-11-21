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
using System.Device.Location;
using SwissTransport;
using Microsoft.Maps.MapControl.WPF;

namespace ProjectTemplate.Business
{
    class Locator
    {
        private GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);

        /// <summary>
        /// Liefert die Koordinaten eines Clients heraus 
        /// </summary>
        /// <returns>Location</returns>
        public Location SearchCurrentLocation()
        {
            if (watcher.TryStart(false, TimeSpan.FromSeconds(1)))
            {
                var whereat = watcher.Position.Location;
                return new Location() { Latitude = whereat.Latitude, Longitude = whereat.Longitude };
            }

            return null;
        }
    }
}
