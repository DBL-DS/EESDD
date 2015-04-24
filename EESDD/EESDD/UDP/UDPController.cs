using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.UDP
{
    class UDPController
    {
        int port;
        IPAddress ip;

        public UDPController() { 
        
        }

        void initDefault() {
            port = 6000;
            ip = VehicleUDP.getIPV4Address();
        }
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        public void refreshIP() {
            ip = VehicleUDP.getIPV4Address();
        }

        public IPAddress IP
        {
            get {
                return ip;
            }
        }
    }
}
