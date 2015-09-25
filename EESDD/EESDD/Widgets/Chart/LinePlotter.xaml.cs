using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Threading;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
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
        private ObservableDataSource<Point> distractAData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractBData = new ObservableDataSource<Point>();

        public void drawNormalLine(ObservableDataSource<Point> normal)
        {
            plotter.AddLineGraph(normal, Color.FromRgb(124, 255, 124));
            plotter.LegendVisible = false;   
            
        }
        public void drawDistractedLineA(ObservableDataSource<Point> distracted)
        {
            plotter.AddLineGraph(distracted, Color.FromRgb(255, 124, 124), 1, "分心 Distracted A");
            plotter.LegendVisible = false;
        }
        public void drawDistractedLineB(ObservableDataSource<Point> distracted)
        {
            plotter.AddLineGraph(distracted, Color.FromRgb(255, 124, 124), 1, "分心 Distracted B");
            plotter.LegendVisible = false;
        }

        public void addLine(ObservableDataSource<Point> points, Color lineColor, double lineThickness, string description)
        {
            plotter.AddLineGraph(points, lineColor, lineThickness, description);
            plotter.LegendVisible = false;
        }

        public void clear() {
            plotter.RemoveUserElements();
        }
    }
    
}
