using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Threading;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using EESDD.Control.Operation;
namespace EESDD.Widgets.Chart
{
    /// <summary>
    /// Interaction logic for LinePlotter.xaml
    /// </summary>
    public partial class LinePlotter : UserControl
    {
        private double lineThickness = 2.0;
        public LinePlotter()
        {
            InitializeComponent();
            drawLine();
        }

        private ObservableDataSource<Point> initData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> normalData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractAData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractBData = new ObservableDataSource<Point>();

        public void drawLine()
        {
            plotter.AddLineGraph(initData, Color.FromArgb(0, 255, 255, 255), lineThickness);
            plotter.AddLineGraph(normalData, Color.FromArgb(170, 8, 255, 0), lineThickness);
            plotter.AddLineGraph(distractAData, Color.FromArgb(170, 255, 117, 29), lineThickness);
            plotter.AddLineGraph(distractBData, Color.FromArgb(170, 51, 170, 255), lineThickness);
            plotter.LegendVisible = false;
        }

        public void drawLine(double thickness)
        {
            plotter.AddLineGraph(initData, Color.FromArgb(0, 255, 255, 255), thickness);
            plotter.AddLineGraph(normalData, Color.FromArgb(170, 8, 255, 0), thickness);
            plotter.AddLineGraph(distractAData, Color.FromArgb(170, 255, 117, 29), thickness);
            plotter.AddLineGraph(distractBData, Color.FromArgb(170, 51, 170, 255), thickness);
            plotter.LegendVisible = false;
        }

        public ObservableDataSource<Point> Normal
        {
            get { return normalData; }
            set { normalData = value; }
        }
        public ObservableDataSource<Point> DistractA
        {
            get { return distractAData; }
            set { distractAData = value; }
        }
        public ObservableDataSource<Point> DistractB
        {
            get { return distractBData; }
            set { distractBData = value; }
        }
        public ObservableDataSource<Point> Init
        {
            get { return initData; }
            set { initData = value; }
        }

        public void clearData() {
            normalData.Collection.Clear();
            distractAData.Collection.Clear();
            distractBData.Collection.Clear();
        }

        public void clearLine()
        {
            plotter.RemoveUserElements();
        }
    }
    
}
