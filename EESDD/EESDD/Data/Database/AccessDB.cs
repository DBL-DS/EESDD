using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using EESDD.Control.User;
using System.Data;
namespace EESDD.Data.DataBase
{
    //连接数据库通用类
    class AccessDB
    {
        private OleDbConnection connection;
        private OleDbCommand command;
        private string strcon;


        public AccessDB(string path)
        {
            init(path);
        }


        private void init(string path)
        {
            connection = new OleDbConnection();
            command = new OleDbCommand();
            strcon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + path;
        }



        private void openConnection() {           
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.ConnectionString = strcon;
                command.Connection = connection;
                try 
	            {	        
		            connection.Open();
	            }
	            catch (Exception e)
	            {
		            throw new Exception(e.Message);
	            }
            }   
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    connection.Dispose();
                    command.Dispose();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public void execute(string sql)
        {
            try
            {
                openConnection();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
            finally
            {
                closeConnection();
            }
        }

        public OleDbDataReader executeWithDataReader(string sql)
        {
            OleDbDataReader reader = null;

            openConnection();
            command.CommandType = CommandType.Text;
            command.CommandText = sql;
            reader = command.ExecuteReader();
            
            return reader;
        }
        public DataTable executeWithDataTable(string sql)
        {
            DataTable table = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter();
            try
            {
                openConnection();
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                da.SelectCommand = command;
                da.Fill(table);
            }
            finally
            {
                closeConnection();
            }
            return table;
        }
    }
}
