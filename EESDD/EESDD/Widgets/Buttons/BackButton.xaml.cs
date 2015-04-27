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
    /// BackButton.xaml 的交互逻辑
    /// </summary>
    public partial class BackButton : UserControl
    {
        int thisPage;

        public BackButton()
        {
            InitializeComponent();
        }

        public void Back()
        {
            switch (thisPage)
            {
                case Navigation.ModeSelect:
                    PageList.Main.setPage(PageList.SceneSelect);
                    break;
                case Navigation.GetReady:
                    PageList.Main.setPage(PageList.ModeSelect);
                    break;
                case Navigation.DataExport:
                    PageList.Main.setPage(PageList.Authentication);
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
            Back();
        }
    }
}
