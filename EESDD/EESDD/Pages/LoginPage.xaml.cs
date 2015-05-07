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
        string nickName;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void NewLoginButton_BtnClick(object sender, EventArgs e)
        {
            NewLogin();
        }

        private void NewLogin()
        {
            nickName = newNick.Text.Trim();
            if (!CheckNickName())
            {
                PageList.Main.User.setNewUser(nickName);
                PageList.Main.setPage(PageList.Evaluation);
            }
        }

        /// <summary>
        /// 数据库中查找昵称，若存在，返回true，否则返回false
        /// </summary>
        /// <returns></returns>
        private bool CheckNickName()
        {

            return false;
        }

        private void newNick_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NewLogin();
            }
        }
    }
}
