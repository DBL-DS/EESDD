using EESDD.Control.DataModel;
using EESDD.Data.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.User
{
    class User
    {
        string name;
        int userClass;
        int loginCount;
        List<ExperienceUnit> experiences;
        bool newUser;
        bool experienceChanged;

        string databaseFilePath;
        string expFilesRoot;

        public User()
        {
            init();
        }

        private void initRoot() {
            databaseFilePath = System.IO.Directory.GetCurrentDirectory() + "\\data\\database\\EESDD.accdb";
            expFilesRoot = System.IO.Directory.GetCurrentDirectory() + "\\data\\";
        }
        private void init()
        {
            initRoot();
            experienceChanged = false;
            loginCount = 0;
        }
        /// <summary>
        /// 将用户信息存入数据库，experiences序列化后存入文件，文件名存入数据库
        /// </summary>
        public void logIn(string name)
        {
            this.name = name;

            if (userExist(name))
                setOldUser();
            else
                setNewUser();
        }
        private bool userExist(string name)
        {
            AccessDB database = new AccessDB(databaseFilePath);
            bool result = database.isExistted(name);
            database.close();

            return result;
        }
        /// <summary>
        /// 将用户信息存入数据库，experiences序列化后存入文件，文件名存入数据库
        /// </summary>
        public void logOut()
        {
            string fileName = "";
            if (experiences.Count != 0 && experienceChanged)
            {
                fileName = saveExperienceListToFile();
            }
            AccessDB database = new AccessDB(databaseFilePath);

            if (newUser)
            {
                string time = DateTime.Now.ToShortDateString();
                database.insertData(name, userClass, fileName, time, time, 1);
            }
            else
            {
                string time = DateTime.Now.ToShortDateString();
                database.insertData(name, userClass, fileName, database.getRegister(name), time, loginCount);
            }

            database.close();
        }

        public void setNewUser()
        {
            newUser = true;
            loginCount ++;
            this.userClass = 0;
            experiences = new List<ExperienceUnit>();
        }

        public void setOldUser()
        {
            string fileName = "";
            getExperienceListFromFile(fileName);
            newUser = false;

            AccessDB database = new AccessDB(databaseFilePath);
            loginCount = database.getAccessCount(name) + 1;
            database.close();
        }
        private void loadUser()
        {
            
        }
        private void saveUser()
        {
            AccessDB database = new AccessDB(databaseFilePath);

            if (newUser)
            {
                //database.insertData(name,userClass,)
            }

            database.close();
        }
        private void saveUser(string fileName)
        {

        }
        public string saveExperienceListToFile()
        {
            string fileName = DateTime.Now.ToFileTimeUtc().ToString();
            string filePath = expFilesRoot + fileName;

            BinaryFormatter bf = new BinaryFormatter();
            if (experiences != null && experiences.Count != 0)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    bf.Serialize(fs, experiences);
                }
                return fileName;
            }
            return null;
        }

        public void getExperienceListFromFile(string fileName)
        {
            string filePath = expFilesRoot + fileName;

            experiences = null;

            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                experiences = (List<ExperienceUnit>)bf.Deserialize(fs);
            }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int UserClass
        {
            get { return userClass; }
            set { userClass = value; }
        }

        public void addExpUnit(ExperienceUnit unit)
        {
            experienceChanged = true;
            experiences.Add(unit);
        }
        public int UnitSize
        {
            get { return experiences.Count; }
        }

        public bool NewUser
        {
            get { return newUser; }
            set { newUser = value; }
        }
    }
}
