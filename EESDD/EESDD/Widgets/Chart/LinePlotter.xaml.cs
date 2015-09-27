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
            drawLine();
        }
        private ObservableDataSource<Point> normalData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractAData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractBData = new ObservableDataSource<Point>();

        public void drawLine()
        {
            plotter.AddLineGraph(normalData, Color.FromRgb(124, 255, 124));
            plotter.AddLineGraph(distractAData, Color.FromRgb(124, 255, 124));
            plotter.AddLineGraph(distractBData, Color.FromRgb(0, 140, 255));
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

        public void clear() {
            plotter.RemoveUserElements();
        }
    }
    
}
