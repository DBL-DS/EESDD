using EESDD.Control.User;
using System.Windows;
using System.Windows.Controls;

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
        public BarChart()
        {
            InitializeComponent();
        }

        public void setValue(float normalValue, float distractAValue, float distractBValue)
        {
            this.normalValue = normalValue;
            this.distractAValue = distractAValue;
            this.distractBValue = distractBValue;

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

            if (!detail.Normal)
            {
                normal.Visibility = System.Windows.Visibility.Hidden;
                distractA.SetValue(Grid.ColumnProperty, 1);
                distractB.SetValue(Grid.ColumnProperty, 2);
            }

            if (!detail.DstractA)
                distractA.Visibility = System.Windows.Visibility.Hidden;
            if (!detail.DstractB)
                distractB.Visibility = System.Windows.Visibility.Hidden;
        }

        private void RePlot(object sender, SizeChangedEventArgs e)
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

                normalText.Text = normalValue.ToString("0.00");
                distractAText.Text = distractAValue.ToString("0.00");
                distractBText.Text = distractBValue.ToString("0.00");
            }
        }

    }
}
