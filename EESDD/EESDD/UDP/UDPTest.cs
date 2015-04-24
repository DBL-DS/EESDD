using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.UDP
{
    class UDPTest
    {
        int port;
        bool connected;

        public UDPTest(int port) {
            this.port = port;
            connected = false;
        }
        public void test() {
            VehicleUDP testUDP = new VehicleUDP(port);
            testUDP.getData();
        }
        public bool Connected
        {
            get { return connected; }
        }
    }
}
