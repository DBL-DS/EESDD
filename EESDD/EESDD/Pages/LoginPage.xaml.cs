using EESDD.Control.User;
using EESDD.Data.Database;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                User user = UserInfoManger.loadUserInfo(loginName);
                if (user != null)
                {
                    //user.Experiences.Remove(user.Experiences[user.Experiences.Count - 1]);
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

            string titleName = System.Threading.Thread
                .CurrentThread.CurrentCulture.TextInfo.ToTitleCase(user.LoginName);
            PageList.SceneSelect.setTitle(titleName);
            PageList.Evaluation.setTitle(titleName);
            PageList.Evaluation.init();
            PageList.Experience.UserName.Text = titleName;
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

                UserInfoManger.saveUserInfo(user);

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
