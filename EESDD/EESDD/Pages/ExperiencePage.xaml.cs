﻿using EESDD.Widgets.Menu;
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
    /// ExperiencePage.xaml 的交互逻辑
    /// </summary>
    public partial class ExperiencePage : Page
    {
        public ExperiencePage()
        {
            InitializeComponent();
            Tabs.setActived(TabsTitle.ExperienceTab);
        }

    }
}
