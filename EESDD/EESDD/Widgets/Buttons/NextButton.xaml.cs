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

namespace EESDD.Widgets.Buttons
{
    /// <summary>
    /// NextButton.xaml 的交互逻辑
    /// </summary>
    public partial class NextButton : UserControl
    {
        int thisPage;

        public NextButton()
        {
            InitializeComponent();
        }

        public void Next()
        {
            switch (thisPage)
            {
                case Navigation.SceneSelect:
                    PageList.Main.setPage(PageList.ModeSelect);
                    break;
                case Navigation.ModeSelect:
                    PageList.Main.setPage(PageList.GetReady);
                    break;
                case Navigation.GetReady:
                    PageList.Main.setPage(PageList.Experience);
                    break;
                case Navigation.Authentication:
                    PageList.Main.setPage(PageList.DataExport);
                    break;
            }
        }

        public int ThisPage
        {
            get { return thisPage; }
            set { thisPage = value; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Next();
        }
    }
}
