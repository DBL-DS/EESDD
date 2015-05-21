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
using EESDD.Widgets.Menu;
using EESDD.Widgets.Buttons;
using EESDD.Control.Operation;
using EESDD.Widgets.Selector;

namespace EESDD.Pages
{
    /// <summary>
    /// SceneSelectPage.xaml 的交互逻辑
    /// </summary>
    public partial class SceneSelectPage : Page
    {
        private Grid currentDetail;
        private Button currentMode;
        public SceneSelectPage()
        {
            InitializeComponent();
            init();
        }

        void init()
        {
            currentDetail = SceneOneDetail;
            currentMode = NormalMode;
            PageList.Main.Selection.SceneSelect = UserSelections.ScenePractice;
            PageList.Main.Selection.ModeSelect = UserSelections.NormalMode;
        }

        private void Little_Enter(object sender, RoutedEventArgs e)
        {
            Button clickBtn = (Button)sender;
            if (clickBtn.Name.Equals("LittleOne"))
            {
                changeDetail(SceneOneDetail);
                PageList.Main.Selection.SceneSelect = UserSelections.ScenePractice;
            }
            else if (clickBtn.Name.Equals("LittleTwo"))
            {
                changeDetail(SceneTwoDetail);
                PageList.Main.Selection.SceneSelect = UserSelections.SceneBrake;
            }
            else if (clickBtn.Name.Equals("LittleThree"))
            {
                changeDetail(SceneThreeDetail);
                PageList.Main.Selection.SceneSelect = UserSelections.SceneLaneChange;
            }
            else if (clickBtn.Name.Equals("LittleFour"))
            {
                changeDetail(SceneFourDetail);
                PageList.Main.Selection.SceneSelect = UserSelections.SceneIntersection;
            }
        }

        private void changeDetail(Grid toChange)
        {
            if (!toChange.Equals(currentDetail))
            {
                currentDetail.Visibility = System.Windows.Visibility.Hidden;
                currentDetail = toChange;
                currentDetail.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void ModeSelect_Click(object sender, RoutedEventArgs e)
        {
            if (!((Button)sender).Equals(currentMode))
            {
                currentMode = (Button)sender;
                NormalCheck.Visibility = System.Windows.Visibility.Hidden;
                LowDistractedCheck.Visibility = System.Windows.Visibility.Hidden;
                HighDistractedCheck.Visibility = System.Windows.Visibility.Hidden;
                switch (((Button)sender).Name)
                {
                    case "NormalMode":
                        NormalCheck.Visibility = System.Windows.Visibility.Visible;
                        PageList.Main.Selection.ModeSelect = UserSelections.NormalMode;
                        break;
                    case "LowDistractedMode":
                        LowDistractedCheck.Visibility = System.Windows.Visibility.Visible;
                        PageList.Main.Selection.ModeSelect = UserSelections.LowDistractedMode;
                        break;
                    case "HighDistractedMode":
                        HighDistractedCheck.Visibility = System.Windows.Visibility.Visible;
                        PageList.Main.Selection.ModeSelect = UserSelections.HighDistractedMode;
                        break;
                }
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            PageList.Main.setPage(PageList.NewExperience);
            PageList.Experience.startRefresh();
        }
    }
}
