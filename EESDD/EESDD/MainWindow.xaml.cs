using System;
using System.Windows;
using System.Windows.Controls;
using EESDD.Pages;
using EESDD.Control.Player;
using EESDD.UDP;
using System.Threading;
using EESDD.Control.Operation;
using EESDD.Control.User;
using System.Windows.Input;
using EESDD.Widgets.Buttons;
using EESDD.Data.Database;

namespace EESDD
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        UDPController udpControl;
        VehicleUDP udp;
        UserSelections selection;
        User user;
        Player player;
        bool refreshing;

        public MainWindow()
        {
            InitializeComponent();
            init();
            setPage(PageList.Login);
        }
        
        internal Player Player
        {
            get { return player; }
            set { player = value; }
        }
        internal User User
        {
            get { return user; }
            set { 
                user = value;
                if (user != null)
                {
                    LogOutButtonVisiable();
                }
                else
                {
                    LogOutButtonInvisiable();
                }
            }
        }

        internal UserSelections Selection
        {
            get { return selection; }
            set { selection = value; }
        }

        internal UDPController UdpControl
        {
            get { return udpControl; }
            set { udpControl = value; }
        }

        void init() {
            udpControl = new UDPController();
            selection = new UserSelections();
            player = new Player();
            User = null;

            LogOutButtonInvisiable();
        }

        public void setPage(Page page) {
            if (page.Equals(PageList.SceneSelect) || page.Equals(PageList.Experience)){
                PageList.CurrentExperience = page;
            }
            
            MainFrame.Content = page;
        }

        public bool testConnection() {
            UDPTest test = new UDPTest(udpControl.Port);
            Thread thread = new Thread(test.test);
            thread.Start();
            if (thread.Join(TimeSpan.FromSeconds(2))) {
                thread.Abort();
            }
            test.close();
            return test.Connected;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            shutdownApp();
        }
        private bool isExperiencing() {
            if (MainFrame.Content.Equals(PageList.Experience))
                return true;
            else
                return false;
        }
        private void shutdownApp()
        {
            if (isExperiencing()) {
                CustomMessageBox.Show("Warning", "Can't quit before the end of experience!");
                return;
            }

            if (CustomMessageBox.Show("Confirmation", "Do you want to close this window?")
                == true)
            {
                this.Close();
            }
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
            base.OnClosing(e);
        }

        private void click() {
            exp.Chosen = true;
        }

        private void exp_BtnClick(object sender, EventArgs e)
        {
            if (user != null && user.LoginName != null)
            {            
                setChosen((TabsButton)sender);
                PageList.Main.setPage(PageList.CurrentExperience);
            }
            else
            {
                CustomMessageBox.Show("提示","请登录后查看！");
            }
        }

        private void eva_BtnClick(object sender, EventArgs e)
        {
            if (user != null && user.LoginName != null)
            {
                setChosen((TabsButton)sender);
                PageList.Main.setPage(PageList.CurrentEvaluation);
                PageList.Evaluation.refreshCurrentChart();
            }
            else
            {
                CustomMessageBox.Show("提示", "请登录后查看！");
            }
        }

        private void data_BtnClick(object sender, EventArgs e)
        {
            if (user != null && user.LoginName != null)
            {
                setChosen((TabsButton)sender);
                PageList.Main.setPage(PageList.CurrentData);
            }
            else
            {
                CustomMessageBox.Show("提示", "请登录后查看！");
            }
        }

        private void setChosen(TabsButton t)
        {
            t.Chosen = true;

            if (!t.Equals(exp))
                exp.Chosen = false;
            if (!t.Equals(eva))
                eva.Chosen = false;
            if (!t.Equals(data))
                data.Chosen = false;
        }
        public void setDefaultChosen()
        {
            setChosen(exp);
        }
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (isExperiencing())
            {
                CustomMessageBox.Show("Warning", "Can't logout before the end of experience!");
                return;
            }
            this.setPage(PageList.Login);
            this.init();
        }
        public void LogOutButtonVisiable()
        {
            LogOutBtn.Visibility = System.Windows.Visibility.Visible;
        }

        public void LogOutButtonInvisiable()
        {
            LogOutBtn.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Resize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void WindowStateChange(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                ResizeBtn.Content = "Resize";
                ResizeBtn.ToolTip = "Resize the window";
            }
            else
            {
                ResizeBtn.Content = "Full Screen";
                ResizeBtn.ToolTip = "Maximum the window";
            }           
        }
        
    }

    public static class PageList
    {
        static MainWindow main = (MainWindow)Application.Current.MainWindow;
        static WelcomePage welcome;
        static LoginPage login;
        static EvaluationPage evaluation;
        static SceneSelectPage sceneSelect;
        static ExperiencePage experience;
        static DataExportAuthenticationPage authentication;
        static DataExportPage dataExport;
        static AboutPage about;

        static Page currentExperience;
        static Page currentEvaluation;
        static Page currentData;
        static Page currentAbout;

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
        public static LoginPage Login
        {
            get {
                if (login == null)
                {
                    login = new LoginPage();
                }
                login.ToDefault();
                return login; 
            }
        }
        public static EvaluationPage Evaluation
        {
            get {
                if (evaluation == null)
                {
                    evaluation = new EvaluationPage();
                } 
                return evaluation;
            }
        }
        public static SceneSelectPage SceneSelect
        {
            get {
                if (sceneSelect == null)
                {
                    sceneSelect = new SceneSelectPage();
                }
                return sceneSelect;
            }
        }
        public static ExperiencePage Experience
        {
            get
            {
                if (experience == null)
                {
                    experience = new ExperiencePage();
                }
                return experience;
            }
        }

        public static DataExportAuthenticationPage Authentication
        {
            get
            {
                if (authentication == null)
                {
                    authentication = new DataExportAuthenticationPage();
                }
                return authentication;
            }
        }
        public static DataExportPage DataExport
        {
            get
            {
                if (dataExport == null)
                {
                    dataExport = new DataExportPage();
                }
                return dataExport;
            }
        }
        public static AboutPage About
        {
            get
            {
                if (about == null)
                {
                    about = new AboutPage();
                }
                return about;
            }
        }

        public static Page CurrentExperience
        {
            get {
                if (currentExperience == null)
                {
                    currentExperience = PageList.SceneSelect;
                }
                return PageList.currentExperience; 
            }
            set { PageList.currentExperience = value; 
            }
        }

        public static Page CurrentEvaluation
        {
            get {
                if (currentEvaluation == null)
                {
                    currentEvaluation = Evaluation;
                }
                return PageList.currentEvaluation; 
            }
            set { PageList.currentEvaluation = value;
            }
        }
        
        public static Page CurrentData
        {
            get {
                if (currentData == null) {
                    currentData = Authentication;
                } 
                return PageList.currentData;
            }
            set { PageList.currentData = value; 
            }
        }

        public static Page CurrentAbout
        {
            get { 
                if (currentAbout == null) {
                    currentAbout = About;
                }
                return PageList.currentAbout;
            }
            set { PageList.currentAbout = value; }
        }
    }
}
