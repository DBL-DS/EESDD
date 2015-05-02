using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using EESDD.Control.User;
namespace EESDDTEST.DB
{
    class AccessDB
    {
        OleDbConnection mycon = null;
        OleDbDataReader myReader = null;
        public AccessDB(string path)
        {
            string strcon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + @path;
            mycon = new OleDbConnection(strcon);
            mycon.Open();
        }
        public void insertData(User save)
        {
            string sql = "insert into [User] (usrName,usrType,experiencePath) values('" + save.Name + "'," + save.UserClass + ",'" + save.Experiences + "')";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            mycom.ExecuteReader();
        }

        public User getData(string name)
        {
            string sql = "SELECT * FROM [User] WHERE usrName = '"+name+"'";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            myReader = mycom.ExecuteReader();
            User outData = new User();
            outData.Name = name;
            outData.UserClass = myReader.GetInt32(2);
            outData.Experiences = myReader.GetString(3);
            return outData;
        }
    }
}
