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
        public void insertData(String name,int type,String path,String reg,String login,int count)
        {
            string sql = "insert into [User] (usrName,usrType,experiencesPath,registerDate,loginDate,accessCount) values('" + name + "'," + type + ",'" + path + "','"+reg+"','"+login+"',"+count+")";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            mycom.ExecuteReader();
        }

        public void updateData(String name,String path,String reg,String login,int count)
        {
            string sql = "UPDATE [User] SET experiencesPath ='" + path + "',registerDate='" + reg + "',loginDate='" + login + "',accessCount=" + count + " WHERE usrName='" + name + "'";
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
        public int getUserClass(string name)
        {
            string sql = "SELECT usrType FROM [User] WHERE usrName = '"+name+"'";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            myReader = mycom.ExecuteReader();
            int type=-1;
            if (myReader.HasRows)
            {
                myReader.Read();
                if (myReader.Depth != 0)
                    type = myReader.GetInt32(0);
            }
            return type;
        }
        public String getExperienceFileName(string name)
        {
            string sql = "SELECT experiencesPath FROM [User] WHERE usrName = '" + name + "'";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            myReader = mycom.ExecuteReader();
            String path = null;
            if (myReader.HasRows)
            {
                myReader.Read();
                if (myReader.Depth != 0)
                    path = myReader.GetString(0);
            }
            return path;
        }
        public String getRegisterDate(string name)
        {
            string sql = "SELECT registerDate FROM [User] WHERE usrName = '" + name + "'";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            myReader = mycom.ExecuteReader();
            String path = null;
            if (myReader.HasRows)
            {
                myReader.Read();
                if (myReader.Depth != 0)
                    path = myReader.GetString(0);
            }
            return path;
        }
        public String getLastLoginDate(string name)
        {
            string sql = "SELECT loginDate FROM [User] WHERE usrName = '" + name + "'";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            myReader = mycom.ExecuteReader();
            String path = null;
            if (myReader.HasRows)
            {
                myReader.Read();
                if (myReader.Depth != 0)
                    path = myReader.GetString(0);
            }
            return path;
        }
        public int getLoginTime(string name)
        {
            string sql = "SELECT accessCount FROM [User] WHERE usrName = '" + name + "'";
            OleDbCommand mycom = new OleDbCommand(sql, mycon);
            myReader = mycom.ExecuteReader();
            int type = -1;
            if (myReader.HasRows)
            {
                myReader.Read();
                if (myReader.Depth != 0)
                    type = myReader.GetInt32(0);
            }
            return type;
        }
        public void Close()
        {
            if (myReader != null) {         
                myReader.Close();
            }
            mycon.Close();
        }
    }
}
