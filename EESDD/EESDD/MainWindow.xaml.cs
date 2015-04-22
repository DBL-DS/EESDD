using System;
using System.Windows;
using System.Windows.Controls;
using EESDD.Pages;

namespace EESDD
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            init();
            setPage(PageList.Welcome);
        }
        void init() {
        }
        public void setPage(Page page) {
            MainFrame.Content = page;
        }
    }

    public static class PageList
    {
        static MainWindow main = (MainWindow)Application.Current.MainWindow;
        static WelcomePage welcome;
        static LoginPage login;

        public static LoginPage Login
        {
            get {
                if (login == null)
                {
                    login = new LoginPage();
                }
                return login; 
            }
        }

        public static MainWindow Main
        {
            get { return main; }
        }

        public static Page Welcome
        {
            get {
                if (welcome == null) {
                    welcome = new WelcomePage();
                }
                return welcome;
            }
        }
    }

}
