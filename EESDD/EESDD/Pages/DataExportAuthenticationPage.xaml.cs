using System.Windows;
using System.Windows.Controls;

namespace EESDD.Pages
{
    /// <summary>
    /// DataExportAuthenticationPage.xaml 的交互逻辑
    /// </summary>
    public partial class DataExportAuthenticationPage : Page
    {
        public DataExportAuthenticationPage()
        {
            InitializeComponent();
        }

        private void ValidatePassword(object sender, RoutedEventArgs e)
        {
            PageList.Main.setPage(PageList.DataExport);
        }
    }

}
