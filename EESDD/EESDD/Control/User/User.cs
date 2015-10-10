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
    [Serializable]
    class User
    {
        private const string timeFormat = "yyyy-MM-dd HH:mm:ss";
        private const int minPoints = 100;

        public User()
        {
            registerDate = lastLoginDate = DateTime.Now.ToString(timeFormat);
            loginCount++;
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

        private int loginCount = 0;

        public int LoginCount
        {
            get { return loginCount; }
            set { loginCount = value; }
        }

        private string registerDate;

        public string RegisterDate
        {
            get { return registerDate; }
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

        private int[] index = new int[20] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                                            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
        public int[] Index { get { return index; } }
    }
}
