using EESDD.Control.DataModel;
using EESDD.Control.Operation;
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
        private const string experiencesFileNameFormat = "yyyyMMddHHmmss";
        private const string loginTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private string expFilesRoot = System.IO.Directory.GetCurrentDirectory() + "\\data\\";
        private const int minPoints = 100;

        public User()
        {
        }

        private string loginName;

        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }

        private string realName;

        public string RealName
        {
            get { return realName; }
            set { realName = value; }
        }

        private string gender;

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private float height;

        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        private float weight;

        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        private int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        private int drivingAge;

        public int DrivingAge
        {
            get { return drivingAge; }
            set { drivingAge = value; }
        }

        private string career;

        public string Career
        {
            get { return career; }
            set { career = value; }
        }

        private string contact;

        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }

        private int userClass;

        public int UserClass
        {
            get { return userClass; }
            set { userClass = value; }
        }

        private int loginCount;

        public int LoginCount
        {
            get { return loginCount; }
            set { loginCount = value; }
        }

        private string registerDate;

        public string RegisterDate
        {
            get { return registerDate; }
            set { registerDate = value; }
        }

        private string lastLoginDate;

        public string LastLoginDate
        {
            get { return lastLoginDate; }
            set { lastLoginDate = value; }
        }

        private string experiencesFileName;

        public string ExperiencesFileName
        {
            get {
                return experiencesFileName; 
            }
            set { 
                experiencesFileName = value;
            }
        }

        private List<ExperienceUnit> experiences;

        public List<ExperienceUnit> Experiences
        {
            get { return experiences; }
            set { experiences = value; }
        }
        public ExperienceUnit NewExperience
        {
            set
            {
                if (experiences == null)
                {
                    experiences = new List<ExperienceUnit>();                 
                }
                experiences.Add(value);
                if (value.Vehicles.Count > minPoints)
                {
                    index[PageList.Main.Selection.Index] = experiences.Count - 1;
                }
            }
        }

        private int[] index = new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
        public int[] Index { get { return index; } }

        /// <summary>
        /// 1. count of login times plus
        /// 2. update last login time
        /// 3. set experiences depend on experiencesFileName
        /// </summary>
        public void newLogin()
        {
            loginCount++;
            lastLoginDate = DateTime.Now.ToString(loginTimeFormat);
            setExperienceListFromFile();
        }

        public bool saveExperienceListToFile()
        {
            string fileName = loginName.ToLower();
            string filePath = expFilesRoot + fileName;

            BinaryFormatter bf = new BinaryFormatter();
            if (experiences != null && experiences.Count != 0)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    bf.Serialize(fs, experiences);
                }

                experiencesFileName = fileName;             //if success, update experiencesFileName

                return true;
            }
            return false;
        }
        private bool setExperienceListFromFile()
        {
            string filePath = expFilesRoot + loginName.ToLower();
            if (File.Exists(filePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    experiences = (List<ExperienceUnit>)bf.Deserialize(fs);
                }

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
