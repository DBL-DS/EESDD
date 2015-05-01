using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using EESDD.CSV;
using System.Threading;
namespace EESDD.Widgets.Chart
{
    /// <summary>
    /// Interaction logic for LinePlotter.xaml
    /// </summary>
    public partial class LinePlotter : UserControl
    {
        public LinePlotter()
        {
            InitializeComponent();
        }
        private ObservableDataSource<Point> normalData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> abnormalData = new ObservableDataSource<Point>();
        private PerformanceCounter cpuPerformance = new PerformanceCounter();
        private DispatcherTimer timer = new DispatcherTimer();

        public void drawNormalLine(ObservableDataSource<Point> normal)
        {
            plotter.AddLineGraph(normal, Color.FromRgb(124, 255, 124));
            plotter.LegendVisible = false;
        }
        public void drawDistractedLine(ObservableDataSource<Point> distracted)
        {
            plotter.AddLineGraph(distracted, Color.FromRgb(255, 124, 124), 1, "分心 Distracted");
        }

        public void addLine(ObservableDataSource<Point> points, Color lineColor, double lineThickness, string description)
        {
            plotter.AddLineGraph(points, lineColor, lineThickness, description);
        }
    }
    
}
