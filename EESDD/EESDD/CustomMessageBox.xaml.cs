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
using System.Windows.Shapes;

namespace EESDD
{
    /// <summary>
    /// CustomMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox()
        {
            InitializeComponent();
        }
        public new string Title
        {
            get { return this.lblTitle.Text; }
            set { this.lblTitle.Text = value; }
        }

        public string Message
        {
            get { return this.lblMsg.Text; }
            set { this.lblMsg.Text = value; }
        }

        /// <summary>
        /// 静态方法 模拟MESSAGEBOX.Show方法
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static bool? Show(string title, string msg)
        {
            var msgBox = new CustomMessageBox();
            msgBox.Title = title;
            msgBox.Message = msg;
            return msgBox.ShowDialog();
        }

        private void Yes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }


        private void No_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Foreground = Brushes.White;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Foreground = Brushes.Black;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //((Border)sender).BorderThickness = 
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

    }
}
