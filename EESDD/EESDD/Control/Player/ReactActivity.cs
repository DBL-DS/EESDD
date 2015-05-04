using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.Player
{
    class ReactActivity
    {
        float reactTime;
        StringBuilder reactStart;
        StringBuilder reactEnd;
        float timeStart;
        float timeEnd;

        public ReactActivity()
        {
            reactTime = 0;
            reactStart = new StringBuilder("00:00:00");
            reactEnd = new StringBuilder("00:00:00");
            timeStart = 0;
        }

        public float ReactTime
        {
            get { return reactTime; }
            set { reactTime = value; }
        }

        public string ReactStart
        {
            get { return reactStart.ToString(); }
            set {
                reactStart.Clear();
                reactStart.Append(value);
            }
        }

        public string ReactEnd
        {
            get { return reactEnd.ToString(); }
            set {
                reactEnd.Clear();
                reactEnd.Append(value);
            }
        }

        public float TimeStart
        {
            get { return timeStart; }
            set { timeStart = value; }
        }

        public float TimeEnd
        {
            get { return timeEnd; }
            set { timeEnd = value; }
        }
    }
}
