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

        public string DataName { get { return dataName; } set { dataName = value; } }
        public string BarTtitle { get { return barTtitle; } set { barTtitle = value; } }
        public bool Normal { get { return normal; } set { normal = value; } }
        public bool DstractA { get { return distractA; } set { distractA = value; } }
        public bool DstractB { get { return distractB; } set { distractB = value; } }

        public BarDetail(string dataName, string barTtitle, bool normal, bool distractA, bool distractB)
        {
            this.dataName = dataName;
            this.barTtitle = barTtitle;
            this.normal = normal;
            this.distractA = distractA;
            this.distractB = distractB;
        }

        public BarDetail ChangeTitle(string title)
        {
            this.barTtitle = title;
            return this;
        }
    }
}
