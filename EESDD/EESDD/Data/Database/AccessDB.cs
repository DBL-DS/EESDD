using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using EESDD.Control.User;
namespace EESDD.Data.DataBase
{
    class AccessDB
    {
        OleDbConnection mycon = null;
        OleDbDataReader myReader = null;
        public AccessDB(string path)
        {
            string strcon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + path;
            mycon = new OleDbConnection(strcon);
            mycon.Open();
        }
        public void insertData(String name,int type,String path)
        {
            string sql = "insert into [User] (usrName,usrType,experiencePath) values('" + name + "'," + type + ",'" + path + "')";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            mycom.ExecuteReader();
        }
        public Boolean isExisted(String name)
        {
            string sql = "select * FROM [User] WHERE usrName = '"+name+"'";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            myReader = mycom.ExecuteReader();
            return myReader.HasRows;
        }
        public int getType(string name)
        {
            string sql = "SELECT usrType FROM [User] WHERE usrName = '"+name+"'";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            myReader = mycom.ExecuteReader();
            int type=-1;
            if (myReader.HasRows)
            {
                myReader.Read();
                type = myReader.GetInt32(0);
            }
            return type;
        }
        public String getPath(string name)
        {
            string sql = "SELECT experiencePath FROM [User] WHERE usrName = '" + name + "'";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            myReader = mycom.ExecuteReader();
            String path = null;
            if (myReader.HasRows)
            {
                myReader.Read();
                path = myReader.GetString(0);
            }
            return path;
        }

        public void close()
        {
            myReader.Close();
            mycon.Close();
        }
    }
}
