using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Public
{
    class DirectoryDef
    {
        public static string dataExportPath = System.IO.Directory.GetCurrentDirectory() + "\\data\\export";

        public static string KaiFontPath = System.IO.Directory.GetCurrentDirectory() + "\\font\\simkai.ttf";

        public static string PictureTempPath = System.IO.Directory.GetCurrentDirectory() + "\\report\\tmp\\tmp.png";
    }
}
