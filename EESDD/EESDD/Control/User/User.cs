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
        public void setNewUser(string name) {
            newUser = true;
            this.name = name;
            this.userClass = 0;
            experiences = new List<ExperienceUnit>();
        }

        public void setOldUser(string name)
        {
            this.name = name;
            string fileName = "";
            getExperienceListFromFile(fileName);
            newUser = false;
        }
        public void saveUser()
        {
            string fileName = saveExperienceListToFile();
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
