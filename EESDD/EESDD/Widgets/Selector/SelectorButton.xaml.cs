using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EESDD.Widgets.Selector
{
    /// <summary>
    /// OperationButton.xaml 的交互逻辑
    /// </summary>
    public partial class SelectorButton : UserControl
    {
        public static readonly DependencyProperty ButtonTextProperty =
             DependencyProperty.Register("ImageSource", typeof(string), typeof(SelectorButton));
        public event EventHandler BtnClick;
        public static readonly DependencyProperty OperationNameProperty =
            DependencyProperty.Register("SelectionName", typeof(string), typeof(SelectorButton));
        private string selectionName;
        private int selectionValue;
        private bool chosen;

        public SelectorButton()
        {
            InitializeComponent();
            chosen = false;
        }
        public bool Chosen
        {
            get { return chosen; }
        }
        public string ImageSource
        {
            set
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(value);
                img.EndInit();
                image.Source = img;
            }
        }

        public string SelectionName
        {
            get { return selectionName; }
            set { selectionName = value; }
        }
        public int SelectionValue
        {
            get { return selectionValue; }
            set { selectionValue = value; }
        }

        public void changeState()
        {
            chosen = !chosen;
            changeBorderColor();
        }
        public void changeState(bool state)
        {
            chosen = state;
            changeBorderColor();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (BtnClick != null)
            {
                BtnClick(this, new EventArgs());
            }
        }

        private void changeBorderColor()
        {
            if (chosen)
                border.Background = new SolidColorBrush(Color.FromRgb(0, 151, 194));
            else
                border.Background = Brushes.Transparent;
        }
    }
}
