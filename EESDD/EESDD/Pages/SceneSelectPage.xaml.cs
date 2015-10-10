using System.Windows;
using System.Windows.Controls;
using EESDD.Control.Operation;

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

        public void setTitle(string name)
        {
            titleTip.Text = "欢迎您，" + name + "！请选择一个场景，开始体验。";
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
            else if (clickBtn.Name.Equals("LittleFive"))
            {
                changeDetail(SceneFiveDetail);
                PageList.Main.Selection.SceneSelect = UserSelections.SceneNavigator;  
            }

            if (clickBtn.Name.Equals("LittleFive"))
            {
                NormalMode.Visibility = System.Windows.Visibility.Hidden;
                ModeSelect_Click(LowDistractedMode, new RoutedEventArgs());
            }
            else
            {
                NormalMode.Visibility = System.Windows.Visibility.Visible;
                ModeSelect_Click(NormalMode, new RoutedEventArgs());
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
                        PageList.Main.Selection.ModeSelect = UserSelections.DistractAMode;
                        break;
                    case "HighDistractedMode":
                        HighDistractedCheck.Visibility = System.Windows.Visibility.Visible;
                        PageList.Main.Selection.ModeSelect = UserSelections.DistractBMode;
                        break;
                }
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            PageList.Main.setPage(PageList.Experience);
            PageList.Experience.startRefresh();
        }
    }
}
