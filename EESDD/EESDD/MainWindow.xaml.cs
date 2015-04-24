using System;
using System.Windows;
using System.Windows.Controls;
using EESDD.Pages;
using EESDD.Control.Player;
using EESDD.UDP;
using System.Threading;

namespace EESDD
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        UDPController udp;
        public MainWindow()
        {
            InitializeComponent();
            init();
            setPage(PageList.Welcome);
        }
        void init() {
            udp = new UDPController();
        }
        public void setPage(Page page) {
            MainFrame.Content = page;
        }

        public bool testConnection() {
            UDPTest test = new UDPTest(udp.Port);
            Thread thread = new Thread(test.test);
            thread.Start();

            int i = 0;
            while (!test.Connected) {
                if (i++ > 100000)
                    break;
            }

            return test.Connected;
        }
    }

    public static class PageList
    {
        static MainWindow main = (MainWindow)Application.Current.MainWindow;
        static WelcomePage welcome;
        static LoginPage login;
        static EvaluationPage evaluation;
        static SceneSelectPage sceneSelect;
        static ModeSelectPage modeSelect;
        static GetReadyPage getReady;
        static ExperiencePage experience;
        static DataExportAuthenticationPage authentication;
        static DataExportPage dataExport;
        static AboutPage about;

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
        public static ModeSelectPage ModeSelect
        {
            get
            {
                if (modeSelect == null)
                {
                    modeSelect = new ModeSelectPage();
                }
                return modeSelect;
            }
        }
        public static GetReadyPage GetReady
        {
            get
            {
                if (getReady == null)
                {
                    getReady = new GetReadyPage();
                }
                return getReady;
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
    }

}
