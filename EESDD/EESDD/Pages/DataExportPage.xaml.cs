using EESDD.Widgets.Buttons;
using EESDD.Widgets.Menu;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EESDD.Pages
{
    /// <summary>
    /// DataExportPage.xaml 的交互逻辑
    /// </summary>
    public partial class DataExportPage : Page
    {
        public DataExportPage()
        {
            InitializeComponent();
            initData();
        }

        private void DoneButton_BtnClick(object sender, EventArgs e)
        {
            PageList.Main.setPage(PageList.Authentication);
            initData();
        }
        private void initData()
        {
            experienceDataGrid.Items.Add(new ExperienceRecord() { exportChoice = false, number = 1, loginName = "Hugh",
                realName = "洪鑫", gender = "男", lastSaveTime = "2015-08-01", one = true, two = true, four = true});
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageList.Main.setPage(PageList.Authentication);
        }
    }

    public class ExperienceRecord
    {
        public bool exportChoice { set; get; }
        public int number { set; get; }
        public string loginName { set; get; }
        public string realName { set; get; }
        public string gender { set; get; }
        public string lastSaveTime { set; get; }
        public bool one { set; get; }
        public bool two { set; get; }
        public bool three { set; get; }
        public bool four { set; get; }
        public bool five { set; get; }
        public bool six { set; get; }
        public bool seven { set; get; }
        public bool eight { set; get; }
        public bool nine { set; get; }
    }
}
