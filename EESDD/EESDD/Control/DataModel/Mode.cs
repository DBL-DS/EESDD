using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.DataModel
{
    class Mode
    {
        public Mode(string modeName, int modeValue)
        {
            this.ModeName = modeName;
            this.ModeValue = modeValue;
        }

        public string ModeName { get; set; }
        public int ModeValue { get; set; }
    }
}
