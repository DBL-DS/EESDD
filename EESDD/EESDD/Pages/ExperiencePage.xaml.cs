using EESDD.Widgets.Chart;
using EESDD.Widgets.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// ExperiencePage.xaml 的交互逻辑
    /// </summary>
    public partial class ExperiencePage : Page
    {
        LinePlotter speedLine;
        public ExperiencePage()
        {
            InitializeComponent();
            Tabs.setActived(TabsTitle.ExperienceTab);
        }
        public void bindDataSource()
        {
            speedLine = null;
            speedLine = new LinePlotter();
            speed_data.Children.Clear();
            speed_data.Children.Add(speedLine);
            speedLine.drawNormalLine(PageList.Main.Player.Speed);
        }
        public void startRefresh()
        {
            Thread refresh = new Thread(PageList.Main.refreshDataSource);
            refresh.Start();
        }
        public void refreshTime(string t)
        {
            this.Dispatcher.BeginInvoke((Action)delegate() {
                Data_Time.Text = t;
            });
        }
        private void OverButton_BtnClick(object sender, EventArgs e)
        {
            PageList.Main.endRefresh(true);
            PageList.Main.setPage(PageList.SceneSelect);
        }

    }
}
