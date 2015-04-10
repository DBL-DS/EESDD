using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EESDD.DataModel;

namespace EESDD.CSV
{
    class VehicleCSV
    {
        List<SimulatedVehicle> dataList;

        public VehicleCSV() { 
            dataList = new List<SimulatedVehicle>();
        }

        public List<SimulatedVehicle> getList(String filePath) {
            CSVReader reader = new CSVReader(filePath);
            if (reader!=null)
            {
                reader.readHeader();

                while (reader.readRecord())
                {
                    SimulatedVehicle s = new SimulatedVehicle();
                    s.SimulationTime = Convert.ToSingle(reader.get(CSVCloumnName.SimulationTime));
                    s.PositionX = Convert.ToSingle(reader.get(CSVCloumnName.PositionX));
                    s.PositionY = Convert.ToSingle(reader.get(CSVCloumnName.PositionY));
                    s.Speed = Convert.ToSingle(reader.get(CSVCloumnName.Speed));
                    s.Acceleration = Convert.ToSingle(reader.get(CSVCloumnName.Acceleration));
                    s.DriftAngle = Convert.ToSingle(reader.get(CSVCloumnName.DriftAngle));
                    s.Offset = Convert.ToSingle(reader.get(CSVCloumnName.Offset));
                    s.WheelSpeed = Convert.ToSingle(reader.get(CSVCloumnName.WheelSpeed));
                    s.GasPedal = Convert.ToSingle(reader.get(CSVCloumnName.GasPedal));
                    s.BrakePedal = Convert.ToSingle(reader.get(CSVCloumnName.BrakePedal));
                    s.Gear = Convert.ToSingle(reader.get(CSVCloumnName.Gear));
                    s.Rpm = Convert.ToSingle(reader.get(CSVCloumnName.Rpm));

                    dataList.Add(s);
                    s = null;
                }
                
                return dataList;
            }
            return null;
        }
    }
}
