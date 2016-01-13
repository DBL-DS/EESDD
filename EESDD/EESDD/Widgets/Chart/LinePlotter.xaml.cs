using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Threading;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using EESDD.Control.Operation;
using System;
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
        private Dispatcher dispatcher;

        private float gradientNormal;
        private float gradientDistractA;
        private float gradientDistractB;
        private float gradientDistractC;
        private float gradientDistractD;

        private Point lastNormal;
        private Point lastDistractA;
        private Point lastDistractB;
        private Point lastDistractC;
        private Point lastDistractD;

        private const float minChange = (float)0.1;
        private const double opacity = 2 / 3;

        public LinePlotter()
        {
            InitializeComponent();
            drawLine();
            init();
            dispatcher = Application.Current.Dispatcher;
        }

        private ObservableDataSource<Point> initData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> normalData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractAData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractBData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractCData = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractDData = new ObservableDataSource<Point>();

        public void init()
        {
            gradientNormal = 0;
            gradientDistractA = 0;
            gradientDistractB = 0;
            gradientDistractC = 0;
            gradientDistractD = 0;

            lastNormal = new Point(0, 0);
            lastDistractA = new Point(0, 0);
            lastDistractB = new Point(0, 0);
            lastDistractC = new Point(0, 0);
            lastDistractD = new Point(0, 0);
        }
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

            init();
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

        public void addRealTimePoint(int mode, Point point)
        {
            switch (mode)
            {
                case UserSelections.NormalMode: normalData.AppendAsync(dispatcher, point); break;
                case UserSelections.DistractAMode: distractAData.AppendAsync(dispatcher, point); break;
                case UserSelections.DistractBMode: distractBData.AppendAsync(dispatcher, point); break;
                case UserSelections.DistractCMode: distractCData.AppendAsync(dispatcher, point); break;
                case UserSelections.DistractDMode: distractDData.AppendAsync(dispatcher, point); break;
            }
        }

        public void addPoint(int mode, Point point)
        {
            float gradient;
            switch (mode)
            {
                case UserSelections.NormalMode:
                    gradient = (float)Math.Abs((point.Y - lastNormal.Y) / (point.X - lastNormal.X));
                    if (gradient - gradientNormal >= minChange)
                    {
                        normalData.AppendAsync(dispatcher, point);
                        lastNormal = point;
                        gradientNormal = gradient;
                    }
                    break;
                case UserSelections.DistractAMode:
                    gradient = (float)Math.Abs((point.Y - lastDistractA.Y) / (point.X - lastDistractA.X));
                    if (gradient - gradientDistractA >= minChange)
                    {
                        distractAData.AppendAsync(dispatcher, point);
                        lastDistractA = point;
                        gradientDistractA = gradient;
                    }
                    break;
                case UserSelections.DistractBMode:
                    gradient = (float)Math.Abs((point.Y - lastDistractB.Y) / (point.X - lastDistractB.X));
                    if (gradient - gradientDistractB >= minChange)
                    {
                        distractBData.AppendAsync(dispatcher, point);
                        lastDistractB = point;
                        gradientDistractB = gradient;
                    }
                    break;
                case UserSelections.DistractCMode:
                    gradient = (float)Math.Abs((point.Y - lastDistractC.Y) / (point.X - lastDistractC.X));
                    if (gradient - gradientDistractC >= minChange)
                    {
                        distractCData.AppendAsync(dispatcher, point);
                        lastDistractC = point;
                        gradientDistractC = gradient;
                    }
                    break;
                case UserSelections.DistractDMode:
                    gradient = (float)Math.Abs((point.Y - lastDistractD.Y) / (point.X - lastDistractD.X));
                    if (gradient - gradientDistractD >= minChange)
                    {
                        distractDData.AppendAsync(dispatcher, point);
                        lastDistractD = point;
                        gradientDistractD = gradient;
                    }
                    break;
                default: initData.AppendAsync(dispatcher, point); break;
            }
        }
    }
    
}
