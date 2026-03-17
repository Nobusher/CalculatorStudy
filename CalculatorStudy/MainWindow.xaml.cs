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

namespace CalculatorStudy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _firstNumber = 0;
        private string _operator = "";
        private bool _newInput = true;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Btn_Click(object sender, RoutedEventArgs e) 
        {
            string input = (sender as Button)?.Content?.ToString() ?? "";

            switch (input)
            {
                case "AC":
                    MainDisplay.Text = "0";
                    ExpressionDisplay.Text = "";
                    _operator = "";
                    _firstNumber = 0;
                    _newInput = true;
                    break;
                case "±":
                    if (double.TryParse(MainDisplay.Text, out double val))
                    {
                        MainDisplay.Text = (-val).ToString();
                    }
                        break;
                case "%":
                    if(double.TryParse(MainDisplay.Text, out double pct))
                    {
                        MainDisplay.Text = (pct/100).ToString();
                    }
                    break;
                case "+":
                case "-":
                case "*":
                case "÷":
                    _firstNumber = double.Parse(MainDisplay.Text);
                    _operator = input;
                    ExpressionDisplay.Text = $"{_firstNumber} {_operator}";
                    _newInput = true ;
                    break;

            }
        }
    }
}