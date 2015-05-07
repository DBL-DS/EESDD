using EESDD.Widgets.Selector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EESDD.Pages
{
    /// <summary>
    /// ModeSelect.xaml 的交互逻辑
    /// </summary>
    public partial class ModeSelect : Window
    {
        public ModeSelect()
        {
            InitializeComponent();
        }

        public static void show() {
            var msgBox = new ModeSelect();
            msgBox.ShowDialog();
        }

        private void SelectorButton_BtnClick(object sender, EventArgs e)
        {
            ((SelectorButton)sender).changeState(true);
        }
    }
}
