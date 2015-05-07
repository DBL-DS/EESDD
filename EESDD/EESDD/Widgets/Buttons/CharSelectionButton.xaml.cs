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

namespace EESDD.Widgets.Buttons
{
    /// <summary>
    /// CharSelectionButton.xaml 的交互逻辑
    /// </summary>
    public partial class ChartSelectionButton : UserControl
    {
        public ChartSelectionButton()
        {
            InitializeComponent();
        }
                
        public event EventHandler BtnClick;
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("BtnText", typeof(string), typeof(ChartSelectionButton));
        private bool chosen;
        public bool Chosen
        {
            get { return chosen; }
            set { 
                chosen = value;

                if (chosen)
                {

                    Style borderStyle = this.FindResource("SelectionBorderChosenStyle") as Style;
                    Style btnStyle = this.FindResource("SelectionChosenStyle") as Style;
                    border.Style = borderStyle;
                    btn.Style = btnStyle;
                }
                else
                {
                    Style borderStyle = this.FindResource("SelectionBorderNormalStyle") as Style;
                    Style btnStyle = this.FindResource("SelectionNormalStyle") as Style;
                    border.Style = borderStyle;
                    btn.Style = btnStyle;
                }

            }
        }
        public string BtnText
        {
            get { return btn.Content.ToString(); }
            set { btn.Content = BtnText; }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (BtnClick != null)
            {
                BtnClick(this, new EventArgs());
            }
        }
    }
}
