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

namespace EESDD.Widgets.Menu
{
    /// <summary>
    /// TabsTitle.xaml 的交互逻辑
    /// </summary>
    public partial class TabsTitle : UserControl
    {
        private Button currentTab;
        public const int ExperienceTab = 1;
        public const int EvaluationTab = 2;
        public const int DataTab = 3;
        public const int AboutTab = 4;
        public TabsTitle()
        {
            InitializeComponent();
            currentTab = Evaluation;
        }

        private void ExperienceButton_Click(object sender, RoutedEventArgs e)
        {
            PageList.Main.setPage(PageList.CurrentExperience);
        }

        private void EvaluationButton_Click(object sender, RoutedEventArgs e)
        {
            PageList.Main.setPage(PageList.CurrentEvaluation);    
        }

        private void DataButton_Click(object sender, RoutedEventArgs e)
        {
            PageList.Main.setPage(PageList.CurrentData);            
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            PageList.Main.setPage(PageList.CurrentAbout);            
        }

        public void setActived(int tab) {
            SolidColorBrush activeBackground = new SolidColorBrush(Color.FromRgb(165, 165, 165));
            currentTab.Background = Brushes.Transparent;
            switch (tab) { 
                case ExperienceTab:
                    currentTab = Experience;
                    break;
                case EvaluationTab:
                    currentTab = Evaluation;
                    break;
                case DataTab:
                    currentTab = Data;
                    break;
                case AboutTab:
                    currentTab = About;
                    break;
            }
            currentTab.Background = activeBackground;
        }
    }
}
