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

namespace EESDD.Widgets.Chart
{
    /// <summary>
    /// BarChart.xaml 的交互逻辑
    /// </summary>
    public partial class BarChart : UserControl
    {
        public static readonly DependencyProperty BarChartTitleProperty =
            DependencyProperty.Register("BarChartTitle", typeof(string), typeof(BarChart));
        public BarChart()
        {
            InitializeComponent();
            //setValue(7, 7, 2);
        }

        public void setValue(float normalValue, float distractAValue, float distractBValue)
        {
            double height = normal.ActualHeight;

            float max = normalValue;
            if (max < distractAValue)
                max = distractAValue;
            if (max < distractBValue)
                max = distractBValue;

            //max value occupy 85% height
            double maxScale = 0.85;
            double fullValue = max / maxScale;

            normalBar.Height = normalValue / fullValue * height;
            distractABar.Height = distractAValue / fullValue * height;
            distractBBar.Height = distractBValue / fullValue * height;

            normalText.Text = normalValue.ToString();
            distractAText.Text = distractAValue.ToString();
            distractBText.Text = distractBValue.ToString();
        }

        public void setTitle(string title)
        {
            BarChartTitle = title;
        }

        public string BarChartTitle
        {
            get { return (string)GetValue(BarChartTitleProperty); }
            set { SetValue(BarChartTitleProperty, value); }
        }

    }
}
