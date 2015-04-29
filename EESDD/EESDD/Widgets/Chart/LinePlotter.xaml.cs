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
namespace TestChart
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
		
		
		
		
        public void drawLine(List<ObservableDataSource<Point>> inData)
        {
            normalData = inData[0];
            abnormalData = inData[1];
            plotter.AddLineGraph(normalData, Colors.Red, 2, "分心");
            plotter.AddLineGraph(abnormalData, Colors.Green, 2, "正常");
            timer.IsEnabled = true;
            plotter.Viewport.FitToView();
        }
    }
    
}
