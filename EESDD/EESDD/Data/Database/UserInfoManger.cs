using EESDD.Control.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Data.Database
{
    class UserInfoManger
    {
        private static string userInfoPath = System.IO.Directory.GetCurrentDirectory() + "\\data\\";
        private const string timeFormat = "yyyy-MM-dd HH:mm:ss";

        public static bool saveUserInfo(User user)
        {          
            string filePath = userInfoPath + user.LoginName.ToLower() + ".eesdd";

            BinaryFormatter bf = new BinaryFormatter();
            if (user != null)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    bf.Serialize(fs, user);
                }

                return true;
            }
            return false;
        }

        //从文件加载用户信息，信息存在返回一个User对象，不存在返回null
        public static User loadUserInfo(string userName)
        {
            string filePath = userInfoPath + userName.ToLower() + ".eesdd";
            User user = null;

            if (File.Exists(filePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        user = (User)bf.Deserialize(fs);
                        loginRecord(user);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }

            return user;
        }

        private static void loginRecord(User user)
        {
            user.LoginCount++;
            user.LastLoginDate = DateTime.Now.ToString(timeFormat);
        }
    }
}
