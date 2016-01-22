using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Threading;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using EESDD.Control.Operation;
using System;
using EESDD.Public;
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

        public LinePlotter()
        {
            InitializeComponent();
            drawLine();
            init();
            dispatcher = Application.Current.Dispatcher;
        }

        private ObservableDataSource<Point> initData        = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> normalData      = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractAData   = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractBData   = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractCData   = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> distractDData   = new ObservableDataSource<Point>();

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
            initGraph = plotter.AddLineGraph(initData, ColorDef.Init, lineThickness);
            normalGraph = plotter.AddLineGraph(normalData, ColorDef.Normal, lineThickness);
            distractAGraph = plotter.AddLineGraph(distractAData, ColorDef.DistractA, lineThickness);
            distractBGraph = plotter.AddLineGraph(distractBData, ColorDef.DistractB, lineThickness);
            distractCGraph = plotter.AddLineGraph(distractCData, ColorDef.DistractC, lineThickness);
            distractDGraph = plotter.AddLineGraph(distractDData, ColorDef.DistractD, lineThickness);
            plotter.LegendVisible = false;
        }

        public void drawLine(double thickness)
        {
            initGraph = plotter.AddLineGraph(initData, ColorDef.Init, thickness);
            normalGraph = plotter.AddLineGraph(normalData, ColorDef.Normal, thickness);
            distractAGraph = plotter.AddLineGraph(distractAData, ColorDef.DistractA, thickness);
            distractBGraph = plotter.AddLineGraph(distractBData, ColorDef.DistractB, thickness);
            distractCGraph = plotter.AddLineGraph(distractCData, ColorDef.DistractC, thickness);
            distractDGraph = plotter.AddLineGraph(distractDData, ColorDef.DistractD, thickness);
            plotter.LegendVisible = false;
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

        private void hideLine(int selection)
        {
            double hideOpacity = 0;
            switch (selection)
            {
                case UserSelections.NormalMode:
                    normalGraph.LinePen.Brush.Opacity = hideOpacity;
                    break;
                case UserSelections.DistractAMode:
                    distractAGraph.LinePen.Brush.Opacity = hideOpacity;
                    break;
                case UserSelections.DistractBMode:
                    distractBGraph.LinePen.Brush.Opacity = hideOpacity;
                    break;
                case UserSelections.DistractCMode:
                    distractCGraph.LinePen.Brush.Opacity = hideOpacity;
                    break;
                case UserSelections.DistractDMode:
                    distractDGraph.LinePen.Brush.Opacity = hideOpacity;
                    break;
            }
        }

        private void showLine(int selection)
        {
            double visibleOpacity = 1;
            switch (selection)
            {
                case UserSelections.NormalMode:
                    normalGraph.LinePen.Brush.Opacity = visibleOpacity;
                    break;
                case UserSelections.DistractAMode:
                    distractAGraph.LinePen.Brush.Opacity = visibleOpacity;
                    break;
                case UserSelections.DistractBMode:
                    distractBGraph.LinePen.Brush.Opacity = visibleOpacity;
                    break;
                case UserSelections.DistractCMode:
                    distractCGraph.LinePen.Brush.Opacity = visibleOpacity;
                    break;
                case UserSelections.DistractDMode:
                    distractDGraph.LinePen.Brush.Opacity = visibleOpacity;
                    break;
            }
        }

        public void setLineVisible(int mode, bool visible)
        {
            if (visible == true)
                showLine(mode);
            else
                hideLine(mode);
        }

        public void setLinesData(LinePlotter plotter)
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
            float minChange = (float)0.1;
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

        // ------------- Get & Set ------------>
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
        // <------------- Get & Set--------------
    }
    
}
