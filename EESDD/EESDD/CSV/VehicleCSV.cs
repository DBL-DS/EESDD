using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EESDD.Control.DataModel;

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
                    s.SteeringWheel = Convert.ToSingle(reader.get(CSVCloumnName.SteeringWheel));
                    s.Offset = Convert.ToSingle(reader.get(CSVCloumnName.Offset));
                    s.BrakePedal = Convert.ToSingle(reader.get(CSVCloumnName.BrakePedal));
                    s.TotalDistance = Convert.ToSingle(reader.get(CSVCloumnName.TotalDistance));
                    s.Braking = Convert.ToSingle(reader.get(CSVCloumnName.Braking));
                    s.Reacting = Convert.ToSingle(reader.get(CSVCloumnName.Reacting));
                    s.Area = Convert.ToSingle(reader.get(CSVCloumnName.Area));

                    dataList.Add(s);
                    s = null;
                }
                
                return dataList;
            }
            return null;
        }
    }
}
