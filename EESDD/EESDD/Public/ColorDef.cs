using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EESDD.Public
{
    class ColorDef
    {
        public static Color Init = Color.FromArgb(0, 255, 255, 255);
        //public static Color Normal = Color.FromArgb(170, 8, 255, 0);
        //public static Color DistractA = Color.FromArgb(170, 255, 129, 10);
        //public static Color DistractB = Color.FromArgb(170, 255, 0, 0);
        //public static Color DistractC = Color.FromArgb(170, 217, 255, 0);
        //public static Color DistractD = Color.FromArgb(170, 10, 116, 255);
        //public static Color Marker = Color.FromArgb(170, 10, 116, 255); 
        
        public static Color Normal = Color.FromArgb(255, 0, 234, 255);
        public static Color DistractA = Color.FromArgb(255, 12, 232, 101);
        public static Color DistractB = Color.FromArgb(255, 87, 255, 0);
        public static Color DistractC = Color.FromArgb(255, 232, 220, 12);
        public static Color DistractD = Color.FromArgb(255, 255, 183, 11);
        public static Color Marker = Color.FromArgb(255, 255, 35, 0);

        public static Color SceneButtonBorderNormal = Color.FromArgb(51, 255, 255, 255);
        public static Color SceneButtonBorderSelected = Color.FromArgb(51, 0, 0, 0);
    }
}
