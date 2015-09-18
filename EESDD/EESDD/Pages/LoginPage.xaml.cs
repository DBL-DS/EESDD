using EESDD.Control.User;
using EESDD.Data.Database;
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
        private static string experiencesFileNameFormat = "yyyyMMddHHmmss";
        private static string loginTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginForm_Login();
        }
        private void LoginForm_Login()
        {
            string loginName = LoginName.Text.Trim();
            if (loginName != "")
            {
                if (UserDB.isUserExist(loginName))
                {
                    User user = UserDB.getUserByLoginName(loginName);

                    user.newLogin();
                    UserDB.updateLoginInfo(user);

                    doWhenLoginSuccess(user);
                }
                else
                {
                    login_name.Text = loginName;
                    LoginForm.Visibility = System.Windows.Visibility.Hidden;
                    RegisterForm.Visibility = System.Windows.Visibility.Visible;
                }          
            }
            else
            {
                CustomMessageBox.Show("提示","用户名不能为空！");
            }
        }

        private void doWhenLoginSuccess(User user)
        {
            PageList.Main.User = user;
            PageList.Main.setPage(PageList.SceneSelect);
            PageList.Main.setDefaultChosen();

            PageList.SceneSelect.setTitle(user.LoginName);
            PageList.Evaluation.setTitle(user.LoginName);
        }
        private void RegisterForm_Login()
        {
            if (validateRegisterForm())
            {
                User user = new User();
                user.LoginName = login_name.Text;
                user.RealName = real_name.Text;
                user.Gender = male.IsChecked == true ? "男" : "女";
                user.Height = float.Parse(height.Text);
                user.Weight = float.Parse(weight.Text);
                user.Age = int.Parse(age.Text);
                user.DrivingAge = int.Parse(driving_age.Text);
                user.Career = career.Text;
                user.Contact = contact.Text;

                user.UserClass = 0;
                user.LoginCount = 1;
                user.LastLoginDate = user.RegisterDate = DateTime.Now.ToString(loginTimeFormat);

                UserDB.saveNewUser(user);

                doWhenLoginSuccess(user);
            }
        }
        private bool validateRegisterForm()
        {
            return true;
        }
        private void LoginName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LoginForm_Login();
        }

        public void ToDefault()
        {
            LoginName.Text = "";
            LoginName.Focus();
            login_name.Text = real_name.Text = height.Text = weight.Text = age.Text
                = driving_age.Text = career.Text = contact.Text = "";
            male.IsChecked = true;
            LoginForm.Visibility = System.Windows.Visibility.Visible;
            RegisterForm.Visibility = System.Windows.Visibility.Hidden;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterForm.Visibility = System.Windows.Visibility.Hidden;
            LoginForm.Visibility = System.Windows.Visibility.Visible;
        }

        private void RegisterFormLogin_Click(object sender, RoutedEventArgs e)
        {
            RegisterForm_Login();
        }
    }
}
