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
    /// GetReadyPage.xaml 的交互逻辑
    /// </summary>
    public partial class GetReadyPage : Page
    {
        public GetReadyPage()
        {
            InitializeComponent();
            Tabs.setActived(TabsTitle.ExperienceTab);
        }

        private void BackButton_BtnClick(object sender, EventArgs e)
        {
            PageList.Main.setPage(PageList.ModeSelect); 
        }
        private void NextButton_BtnClick(object sender, EventArgs e)
        {
            PageList.Main.setPage(PageList.Experience);
        }
    }
}
