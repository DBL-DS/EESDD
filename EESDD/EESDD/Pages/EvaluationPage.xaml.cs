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
    /// EvaluationPage.xaml 的交互逻辑
    /// </summary>
    public partial class EvaluationPage : Page
    {
        public EvaluationPage()
        {
            InitializeComponent();
            Tabs.setActived(TabsTitle.EvaluationTab);
            setWelcomeTitle();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageList.Main.setPage(PageList.SceneSelect);
        }

        private void setWelcomeTitle()
        {
            StringBuilder title = new StringBuilder();
            title.Append(PageList.Main.User.Name+",");

            if (PageList.Main.User.NewUser)
            {
                title.Append("欢迎您体验！");
            }
            else
            {
                title.Append("欢迎您再度体验！");
            }
            WelcomeTitle.Text = title.ToString();
        }
    }
}
