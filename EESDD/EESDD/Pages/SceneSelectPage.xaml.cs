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
        private SelectorButton chosenButton;
        public SceneSelectPage()
        {
            InitializeComponent();
            Tabs.setActived(TabsTitle.ExperienceTab);
            setSelectionValue();
            setDefaultChosen();
        }
        private void setDefaultChosen() {
            chosenButton = practice;
            chosenButton.changeState(true);
        }

        private void NextButton_BtnClick(object sender, EventArgs e)
        {
            PageList.Main.Selection.SceneSelect = chosenButton.SelectionValue;
            PageList.Main.setPage(PageList.ModeSelect);
        }
        private void setSelectionValue()
        {
            practice.SelectionValue = UserSelections.ScenePractice;
            secure_one.SelectionValue = UserSelections.SceneSecurityOne;
            secure_two.SelectionValue = UserSelections.SceneSecurityTwo;
            smooth_one.SelectionValue = UserSelections.SceneSmoothOne;
        }

        private void SelectorButton_BtnClick(object sender, EventArgs e)
        {
            chosenButton.changeState(false);
            chosenButton= ((SelectorButton)sender);
            chosenButton.changeState(true);
        }
    }
}
