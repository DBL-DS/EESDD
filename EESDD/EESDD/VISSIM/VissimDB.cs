using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using EESDD.Control.User;
namespace EESDD.VISSIM
{
    class VissimDB
    {
        OleDbConnection mycon = null;
        OleDbDataReader myReader = null;
        private string path = 
            System.IO.Directory.GetCurrentDirectory() + "\\vissim\\map\\vissim.accdb";
        public VissimDB()
        {
            string strcon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + path;
            mycon = new OleDbConnection(strcon);
            mycon.Open();
        }
       
        public double getAvgDelayTime()
        {
            string sql = "SELECT AVG(Delay) FROM [DELAYTIMES]";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            myReader = mycom.ExecuteReader();
            double type = -1;
            if (myReader.HasRows)
            {
                myReader.Read();
                type = myReader.GetDouble(0);
            }
            return type;
        }
        public double getAvgQueueLengh()
        {
            string sql = "SELECT AVG(Avg_) FROM [QUEUELENGTH]";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            myReader = mycom.ExecuteReader();
            double  type = -1;
            if (myReader.HasRows)
            {
                myReader.Read();
                type = myReader.GetDouble(0);
            }
            return type;
        }
        public double getAvgSpeed()
        {
            string sql = "SELECT AVG(v) FROM [VEH_RECORD]";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            myReader = mycom.ExecuteReader();
            double type = -1;
            if (myReader.HasRows)
            {
                myReader.Read();
                type = myReader.GetDouble(0);
            }
            return type;
        }
        public void close()
        {
            myReader.Close();
            mycon.Close();
        }
    }
}
