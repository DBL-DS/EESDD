using EESDD.Control.Operation;
using EESDD.Control.User;
using EESDD.Public;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EESDD.Widgets.Chart
{
    /// <summary>
    /// BarChart.xaml 的交互逻辑
    /// </summary>
    public partial class BarChart : UserControl
    {
        public static readonly DependencyProperty BarChartTitleProperty =
            DependencyProperty.Register("BarChartTitle", typeof(string), typeof(BarChart));
        private float normalValue;
        private float distractAValue;
        private float distractBValue;
        private float distractCValue;
        private float distractDValue;
        public BarChart()
        {
            InitializeComponent();
            GUISet();
        }

        private void GUISet()
        {
            normalBar.Fill = new SolidColorBrush(ColorDef.Normal);
            distractABar.Fill = new SolidColorBrush(ColorDef.DistractA);
            distractBBar.Fill = new SolidColorBrush(ColorDef.DistractB);
            distractCBar.Fill = new SolidColorBrush(ColorDef.DistractC);
            distractDBar.Fill = new SolidColorBrush(ColorDef.DistractD);
        }
        public void setValue(float normalValue, float distractAValue, float distractBValue, float distractCValue, float distractDValue)
        {
            this.normalValue = normalValue;
            this.distractAValue = distractAValue;
            this.distractBValue = distractBValue;
            this.distractCValue = distractCValue;
            this.distractDValue = distractDValue;
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

        public void setBarFromBarDetail(BarDetail detail)
        {
            setTitle(detail.BarTtitle);
        }

        public void setBarVisible(bool _normal, bool _distractA, bool _distractB, bool _distractC, bool _distractD) 
        {
            setVisibility(normal, _normal);
            setVisibility(distractA, _distractA);
            setVisibility(distractB, _distractB);
            setVisibility(distractC, _distractC);
            setVisibility(distractD, _distractD);
        }

        public void setBarVisible(int mode, bool visibility)
        {
            Grid grid = null;
            switch (mode)
            {
                case UserSelections.NormalMode:
                    grid = normal;
                    break;
                case UserSelections.DistractAMode:
                    grid = distractA;
                    break;
                case UserSelections.DistractBMode:
                    grid = distractB;
                    break;
                case UserSelections.DistractCMode:
                    grid = distractC;
                    break;
                case UserSelections.DistractDMode:
                    grid = distractD;
                    break;
            }

            if (grid != null)
            {
                setVisibility(grid, visibility);   
            }
        }

        private void setVisibility(Grid grid, bool visibility)
        {
            grid.Visibility = visibility ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            replotChart();
        }

        private void RePlot(object sender, SizeChangedEventArgs e)
        {
            replotChart();
        }

        private void replotChart()
        {
            double height = barsContainer.ActualHeight;

            float max = getMax();

            //max value occupy 85% height
            double maxScale = 0.85;
            double fullValue = max / maxScale;

            // all three values are zero
            if (fullValue == 0)
            {
                normalBar.Height = distractABar.Height = distractBBar.Height = 1;
                normalText.Text = distractAText.Text = distractBText.Text = "0";
            }
            else
            {
                normalBar.Height = normalValue / fullValue * height + 1;
                distractABar.Height = distractAValue / fullValue * height + 1;
                distractBBar.Height = distractBValue / fullValue * height + 1;
                distractCBar.Height = distractCValue / fullValue * height + 1;
                distractDBar.Height = distractDValue / fullValue * height + 1;

                normalText.Text = normalValue.ToString("0.00");
                distractAText.Text = distractAValue.ToString("0.00");
                distractBText.Text = distractBValue.ToString("0.00");
                distractCText.Text = distractCValue.ToString("0.00");
                distractDText.Text = distractDValue.ToString("0.00");
            }
        }


        private float getMax()
        {
            float max = 0;

            if (normal.Visibility == System.Windows.Visibility.Visible)
                max = Math.Max(max, normalValue);

            if (distractA.Visibility == System.Windows.Visibility.Visible)
                max = Math.Max(max, distractAValue);

            if (distractB.Visibility == System.Windows.Visibility.Visible)
                max = Math.Max(max, distractBValue);

            if (distractC.Visibility == System.Windows.Visibility.Visible)
                max = Math.Max(max, distractCValue);

            if (distractD.Visibility == System.Windows.Visibility.Visible)
                max = Math.Max(max, distractDValue);

            return max;
        }

    }
}
