using EESDD.Control.User;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace EESDD.Pages
{
    /// <summary>
    /// DataExportPage.xaml 的交互逻辑
    /// </summary>
    public partial class DataExportPage : Page
    {
        public DataExportPage()
        {
            InitializeComponent();
            initData();
        }

        private void DoneButton_BtnClick(object sender, EventArgs e)
        {
            PageList.Main.setPage(PageList.Authentication);
            initData();
        }
        private void initData()
        {
            experienceDataGrid.Items.Add(new ExperienceRecord() { exportChoice = false, number = 1, loginName = "Hugh",
                realName = "洪鑫", gender = "男", lastSaveTime = "2015-08-01", one = true, two = true, four = true});
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageList.Main.setPage(PageList.Authentication);
        }

        private void DeleteLastExperience(object sender, RoutedEventArgs e)
        {
            List<ExperienceUnit> units = PageList.Main.User.Experiences;
            if (units != null && units.Count != 0)
            {
                if (CustomMessageBox.Show("确认", "确定删除上一个历史体验记录？") == true)
                {
                    units.Remove(units[units.Count - 1]);
                    CustomMessageBox.Show("提示", "已成功删除！");
                }
            }
            else
            {
                CustomMessageBox.Show("提示", "没有历史体验记录！");
            }
        }
    }

    public class ExperienceRecord
    {
        public bool exportChoice { set; get; }
        public int number { set; get; }
        public string loginName { set; get; }
        public string realName { set; get; }
        public string gender { set; get; }
        public string lastSaveTime { set; get; }
        public bool one { set; get; }
        public bool two { set; get; }
        public bool three { set; get; }
        public bool four { set; get; }
        public bool five { set; get; }
        public bool six { set; get; }
        public bool seven { set; get; }
        public bool eight { set; get; }
        public bool nine { set; get; }
    }
}
