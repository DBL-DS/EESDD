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
        VehicleUDP testUDP;

        public UDPTest(int port) {
            this.port = port;
            connected = false;
        }
        public void test() {
            testUDP = new VehicleUDP(port);
            testUDP.getData();
            connected = true;
        }
        public bool Connected
        {
            get { return connected; }
        }
        public void close()
        {
            if (testUDP != null)
                testUDP.close();
        }
    }
}
