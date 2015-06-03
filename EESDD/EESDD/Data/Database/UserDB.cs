using EESDD.Control.User;
using EESDD.Data.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Data.Database
{
    class UserDB
    {
        
        private static string databasePath = System.IO.Directory.GetCurrentDirectory() + "\\data\\database\\EESDD.accdb";
        public static bool isUserExist(string userLoginName)
        {
            AccessDB db = new AccessDB(databasePath);
            string sql = "SELECT * FROM [User] WHERE LoginName = '" + userLoginName + "'";
            bool exist = db.executeWithDataReader(sql).HasRows;
            db.closeConnection();
            return exist;
        }

        public static User getUserByLoginName(string loginName)
        {
            AccessDB db = new AccessDB(databasePath);
            string sql = "SELECT * FROM [User] WHERE LoginName = '" + loginName + "'";
            DataTable table = db.executeWithDataTable(sql);

            User user = null;
            
            if (table.Rows.Count != 0)
            {
                DataRow row = table.Rows[0];
                user = getUserFromRow(row);
            }

            return user;
        }

        public static List<User> getUsers()
        {
            AccessDB db = new AccessDB(databasePath);
            string sql = "SELECT * FROM [User]";
            DataTable table = db.executeWithDataTable(sql);

            List<User> users = new List<User>();

            if (table.Rows.Count != 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    users.Add(getUserFromRow(row));
                }
            }
            return users;
        }

        public static void saveNewUser(User user)
        {
            string experiencesFileName = "";
            user.ExperiencesFileName = experiencesFileName;
            AccessDB db = new AccessDB(databasePath);
            string sql = "INSERT INTO [User] (LoginName,RealName,Gender,Height,Weight,"
                +"Age,DrivingAge,Career,Contact,UserClass,LoginCount,RegisterDate,LastLoginDate,ExperiencesFileName)"
                + " VALUES('" + user.LoginName + "','" + user.RealName + "','" + user.Gender
                + "'," + user.Height + "," + user.Weight + "," + user.Age + "," + user.DrivingAge
                + ",'" + user.Career + "','" + user.Contact + "'," + user.UserClass + ","
                + user.LoginCount + ",'" + user.RegisterDate + "','" + user.LastLoginDate
                + "','" + user.ExperiencesFileName + "')";
            db.execute(sql);
        }

        public static void updateUser(User user)
        {
            
        }

        public static void updateLoginInfo(User user)
        {
            AccessDB db = new AccessDB(databasePath);
            string sql = "UPDATE [USER] SET LoginCount=" + user.LoginCount + ",LastLoginDate='" + user.LastLoginDate
                + "' WHERE LoginName='" + user.LoginName+"'";
            db.execute(sql);
        }

        public static bool updateExperiencesInfo(User user)
        {
            string oldFileName = user.ExperiencesFileName;

            if (user.saveExperienceListToFile())
            {
                AccessDB db = new AccessDB(databasePath);
                string sql = "UPDATE [USER] SET ExperiencesFileName=" + user.ExperiencesFileName
                    + " WHERE LoginName='" + user.LoginName + "'";
                db.execute(sql);

                /*
                 * whether delete old files?
                 */
                //string oldFileRoot = expFilesRoot + oldFileName;
                //if (File.Exists(oldFileRoot))
                //{
                //    File.Delete(oldFileRoot);
                //}

                return true;
            }

            return false;
        }



        private static User getUserFromRow(DataRow row)
        {
            User user = new User();
            user.LoginName = row["LoginName"] as string;
            user.RealName = row["RealName"] as string;
            user.Gender = row["Gender"] as string;
            user.Height = (float)row["Height"];
            user.Weight = (float)row["Weight"];
            user.Age = (int)row["Age"];
            user.DrivingAge = (int)row["DrivingAge"];
            user.Career = row["Career"] as string;
            user.Contact = row["Contact"] as string;
            user.UserClass = (int)row["UserClass"];
            user.LoginCount = (int)row["LoginCount"];
            user.RegisterDate = row["RegisterDate"] as string;
            user.LastLoginDate = row["LastLoginDate"] as string;
            user.ExperiencesFileName = row["ExperiencesFileName"] as string;
            return user;
        }


    }
}
