﻿using System;
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

namespace EESDD.Widgets.Buttons
{
    /// <summary>
    /// NavigationButton.xaml 的交互逻辑
    /// </summary>
    public partial class NavigationButton : UserControl
    {
        public event EventHandler BtnClick;
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("BtnText", typeof(string), typeof(NavigationButton));
        public NavigationButton()
        {
            InitializeComponent();
        }

        public string BtnText
        {
            get { return btn.Content.ToString(); }
            set { btn.Content = BtnText;}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (BtnClick != null)
            {
                BtnClick(this, new EventArgs());
            }
        }
    }
}
