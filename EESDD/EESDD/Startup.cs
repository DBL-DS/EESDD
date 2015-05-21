using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EESDD
{
    class Startup
    {
        [STAThread]
        public static void Main() {
            Application app = new Application();
            MainWindow mainWindow = new MainWindow();

            app.Run(mainWindow);
        }
    }
}
