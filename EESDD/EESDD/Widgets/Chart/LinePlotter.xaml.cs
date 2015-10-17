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
        private LineGraph initGraph;
        private LineGraph normalGraph;
        private LineGraph distractAGraph;
        private LineGraph distractBGraph;
        private LineGraph distractCGraph;
        private LineGraph distractDGraph;
        public LinePlotter()
        {
            InitializeComponent();
            drawLine();
        }

        private ObservableDataSource<Point> initData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> normalData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractAData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractBData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractCData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractDData = new ObservableDataSource<Point>();

        public void drawLine()
        {
            initGraph = plotter.AddLineGraph(initData, Color.FromArgb(0, 255, 255, 255), lineThickness);
            normalGraph = plotter.AddLineGraph(normalData, Color.FromArgb(170, 8, 255, 0), lineThickness);
            distractAGraph = plotter.AddLineGraph(distractAData, Color.FromArgb(170, 255, 117, 29), lineThickness);
            distractBGraph = plotter.AddLineGraph(distractBData, Color.FromArgb(170, 51, 170, 255), lineThickness);
            distractCGraph = plotter.AddLineGraph(distractCData, Color.FromArgb(170, 0, 140, 136), lineThickness);
            distractDGraph = plotter.AddLineGraph(distractDData, Color.FromArgb(170, 0, 140, 0), lineThickness);
            plotter.LegendVisible = false;
        }

        public void drawLine(double thickness)
        {
            initGraph = plotter.AddLineGraph(initData, Color.FromArgb(0, 255, 255, 255), thickness);
            normalGraph = plotter.AddLineGraph(normalData, Color.FromArgb(170, 8, 255, 0), thickness);
            distractAGraph = plotter.AddLineGraph(distractAData, Color.FromArgb(170, 255, 117, 29), thickness);
            distractBGraph = plotter.AddLineGraph(distractBData, Color.FromArgb(170, 51, 170, 255), thickness);
            distractCGraph = plotter.AddLineGraph(distractCData, Color.FromArgb(170, 0, 140, 136), thickness);
            distractDGraph = plotter.AddLineGraph(distractDData, Color.FromArgb(170, 0, 140, 0), thickness);
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
        public ObservableDataSource<Point> DistractC
        {
            get { return distractCData; }
            set { distractBData = value; }
        }
        public ObservableDataSource<Point> DistractD
        {
            get { return distractDData; }
            set { distractBData = value; }
        }
        public ObservableDataSource<Point> Init
        {
            get { return initData; }
            set { initData = value; }
        }

        public void clearData() {
            initData.Collection.Clear();
            normalData.Collection.Clear();
            distractAData.Collection.Clear();
            distractBData.Collection.Clear();
            distractCData.Collection.Clear();
            distractDData.Collection.Clear();
        }

        public void clearLine()
        {
            plotter.RemoveUserElements();
        }

        public void saveSnapShot()
        {
            
        }

        public void hideLine(int selection)
        {
            switch (selection)
            {
                case UserSelections.NormalMode:
                    normalGraph.LinePen.Brush.Opacity = 0;
                    break;
                case UserSelections.DistractAMode:
                    distractAGraph.LinePen.Brush.Opacity = 0;
                    break;
                case UserSelections.DistractBMode:
                    distractBGraph.LinePen.Brush.Opacity = 0;
                    break;
                case UserSelections.DistractCMode:
                    distractCGraph.LinePen.Brush.Opacity = 0;
                    break;
                case UserSelections.DistractDMode:
                    distractDGraph.LinePen.Brush.Opacity = 0;
                    break;
            }
        }

        public void showLine(int selection)
        {
            double opacity = 2 / 3;
            switch (selection)
            {
                case UserSelections.NormalMode:
                    normalGraph.LinePen.Brush.Opacity = opacity;
                    break;
                case UserSelections.DistractAMode:
                    distractAGraph.LinePen.Brush.Opacity = opacity;
                    break;
                case UserSelections.DistractBMode:
                    distractBGraph.LinePen.Brush.Opacity = opacity;
                    break;
                case UserSelections.DistractCMode:
                    distractCGraph.LinePen.Brush.Opacity = opacity;
                    break;
                case UserSelections.DistractDMode:
                    distractDGraph.LinePen.Brush.Opacity = opacity;
                    break;
            }
        }

        public void setLines(LinePlotter plotter)
        {
            this.initData = plotter.initData;
            this.normalData = plotter.normalData;
            this.distractAData = plotter.distractAData;
            this.distractBData = plotter.distractBData;
            this.distractCData = plotter.distractCData;
            this.distractDData = plotter.distractDData;
        }
    }
    
}
