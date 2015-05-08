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
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ToLogin();
        }
        private void ToLogin()
        {
            string nickName = LoginName.Text.Trim();
            PageList.Main.User.logIn(nickName);
            PageList.Main.setPage(PageList.SceneSelect);
            PageList.Main.setDefaultChosen();
            //string path = System.IO.Directory.GetCurrentDirectory();
            //MessageBox.Show(path);
        }
        private void LoginName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ToLogin();
        }
    }
}
