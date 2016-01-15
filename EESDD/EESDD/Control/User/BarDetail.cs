using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.User
{
    public class BarDetail
    {
        private string dataName;
        private string barTtitle;
        private bool normal;
        private bool distractA;
        private bool distractB;
        private bool distractC;
        private bool distractD;

        public string DataName { get { return dataName; } set { dataName = value; } }
        public string BarTtitle { get { return barTtitle; } set { barTtitle = value; } }
        public bool Normal { get { return normal; } set { normal = value; } }
        public bool DstractA { get { return distractA; } set { distractA = value; } }
        public bool DstractB { get { return distractB; } set { distractB = value; } }
        public bool DstractC { get { return distractA; } set { distractA = value; } }
        public bool DstractD { get { return distractB; } set { distractB = value; } }

        public BarDetail(string dataName, string barTtitle, bool normal, bool distractA, bool distractB, bool distractC, bool distractD)
        {
            this.dataName = dataName;
            this.barTtitle = barTtitle;
            this.normal = normal;
            this.distractA = distractA;
            this.distractB = distractB;
            this.distractA = distractC;
            this.distractB = distractD;
        }

        public BarDetail(string dataName, string barTtitle)
        {
            this.dataName = dataName;
            this.barTtitle = barTtitle;
            this.normal = true;
            this.distractA = true;
            this.distractB = true;
            this.distractA = true;
            this.distractB = true;
        }

        public BarDetail ChangeTitle(string title)
        {
            this.barTtitle = title;
            return this;
        }
    }
}
