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

            if (clickBtn.Name.Equals("LittleOne"))
            {
                DistractAMode.Visibility = System.Windows.Visibility.Hidden;
                DistractBMode.Visibility = System.Windows.Visibility.Hidden;
                DistractCMode.Visibility = System.Windows.Visibility.Hidden;
                DistractDMode.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                NormalMode.Visibility = System.Windows.Visibility.Visible;
                DistractAMode.Visibility = System.Windows.Visibility.Visible;
                DistractBMode.Visibility = System.Windows.Visibility.Visible;
                DistractCMode.Visibility = System.Windows.Visibility.Visible;
                DistractDMode.Visibility = System.Windows.Visibility.Visible;
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
                DIstractACheck.Visibility = System.Windows.Visibility.Hidden;
                DistractBCheck.Visibility = System.Windows.Visibility.Hidden;
                DistractCCheck.Visibility = System.Windows.Visibility.Hidden;
                DistractDCheck.Visibility = System.Windows.Visibility.Hidden;
                switch (((Button)sender).Name)
                {
                    case "NormalMode":
                        NormalCheck.Visibility = System.Windows.Visibility.Visible;
                        PageList.Main.Selection.ModeSelect = UserSelections.NormalMode;
                        break;
                    case "DistractAMode":
                        DIstractACheck.Visibility = System.Windows.Visibility.Visible;
                        PageList.Main.Selection.ModeSelect = UserSelections.DistractAMode;
                        break;
                    case "DistractBMode":
                        DistractBCheck.Visibility = System.Windows.Visibility.Visible;
                        PageList.Main.Selection.ModeSelect = UserSelections.DistractBMode;
                        break;
                    case "DistractCMode":
                        DistractCCheck.Visibility = System.Windows.Visibility.Visible;
                        PageList.Main.Selection.ModeSelect = UserSelections.DistractCMode;
                        break;
                    case "DistractDMode":
                        DistractDCheck.Visibility = System.Windows.Visibility.Visible;
                        PageList.Main.Selection.ModeSelect = UserSelections.DistractDMode;
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
