using EESDD.Control.DataModel;
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
        List<ExperienceUnit> experiences;
        bool newUser;

        const string rootName = "/Data/";

        public User()
        {

        }


        /// <summary>
        /// 将用户信息存入数据库，experiences序列化后存入文件，文件名存入数据库
        /// </summary>
        public void logIn(string name)
        {
            this.name = name;

            if (userExist())
                setOldUser();
            else
                setNewUser();
        }
        private bool userExist()
        {
            return true;
        }
        /// <summary>
        /// 将用户信息存入数据库，experiences序列化后存入文件，文件名存入数据库
        /// </summary>
        public void logOut()
        {
            if (experiences.Count != 0)
            {
                string fileName = saveExperienceListToFile();
            }
        }

        public void setNewUser()
        {
            newUser = true;
            this.userClass = 0;
            experiences = new List<ExperienceUnit>();
        }

        public void setOldUser()
        {
            string fileName = "";
            getExperienceListFromFile(fileName);
            newUser = false;
        }
        private void loadUser()
        {
            
        }
        private void saveUser()
        {

        }
        public string saveExperienceListToFile()
        {
            string fileName = DateTime.Now.ToFileTimeUtc().ToString();
            string filePath = rootName + fileName;

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
            string filePath = rootName + fileName;

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
