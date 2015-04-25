using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using EESDD.Control.DataModel;

namespace EESDD.UDP
{
    class VehicleUDP
    {
        IPAddress address;
        int port;
        Socket socket;
        EndPoint EP;
        byte[] buffer; 
        const int BufferSize = 184;

        public VehicleUDP(int port) {
            this.port = port;
            initSocket();
            buffer = new byte[BufferSize];
        }

        void initSocket(){
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            address = getIPV4Address();
            EP = new IPEndPoint(address,port);
            socket.Bind(EP);
        }

        public static IPAddress getIPV4Address(){
            string hostName = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);
            foreach (IPAddress address in addresses)
	        {
		        if (address.AddressFamily == AddressFamily.InterNetwork)
	            {
		            return address;
	            }
	        }
            return null;
        }
        /// <summary>
        /// 通过UDP，获取一个byte数组，并转化为一个SimulatedVehicle对象
        /// </summary>
        /// <returns>一个SimulatedVehicle对象</returns>
        public SimulatedVehicle getData() {
            try
            {
                socket.Receive(buffer);
            }
            catch (SocketException e)
            {
                return null;
            }
            return byteToSimulatedVehicle(buffer);
        }
        /// <summary>
        /// 将每四个字节组合为一个单精度浮点数
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index">偏移量</param>
        /// <returns></returns>
        float getFloat(byte[] b, int index)
        {
            return BitConverter.ToSingle(b, index);
        }
        /// <summary>
        /// 传入一个字节数组，传出一个SimulatedVehicle对象
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        SimulatedVehicle byteToSimulatedVehicle(byte[] buffer) {
            SimulatedVehicle s = new SimulatedVehicle();

            s.SimulationTime = getFloat(buffer, UDPOffset.SimulationTime);
            s.PositionX = getFloat(buffer, UDPOffset.PositionX);
            s.PositionY = getFloat(buffer, UDPOffset.PositionY);
            s.Speed = getFloat(buffer, UDPOffset.Speed);
            s.Acceleration = getFloat(buffer, UDPOffset.Acceleration);
            s.DriftAngle = getFloat(buffer, UDPOffset.DriftAngle);
            s.Offset = getFloat(buffer, UDPOffset.Offset);
            s.WheelSpeed = getFloat(buffer, UDPOffset.RotationalSpeed);
            s.GasPedal = getFloat(buffer, UDPOffset.GasPedal);
            s.BrakePedal = getFloat(buffer, UDPOffset.BrakePedal);
            s.Gear = getFloat(buffer, UDPOffset.Gear);
            s.Rpm = getFloat(buffer, UDPOffset.Rpm);

            return s;
        }

        public void close() {
            socket.Close();
        }
    }
}
