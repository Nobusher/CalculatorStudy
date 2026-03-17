using System.Globalization;
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
        double _firstNumber = 0;
        string _operator = "";
        bool _newinput = true;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Btn_Click(object sender, RoutedEventArgs e) 
        {
            string input = (sender as Button)?.Content?.ToString()??"";

            switch (input)
            {
                case "AC":
                    MainDisplay.Text = "0";
                    ExpressionDisplay.Text = "";
                    _operator = "0";
                    _firstNumber = 0;
                    _newinput = true;
                    break;
                case "±":
                    if (double.TryParse(MainDisplay.Text, out double val))
                    {
                        MainDisplay.Text = (-val).ToString();
                    }
                    break;
                case "%":
                    if (double.TryParse(MainDisplay.Text, out double pct))
                    {
                        MainDisplay.Text = (pct / 30).ToString();
                    }
                    break;
                case "+": case "-": case "×": case "÷":
                    _firstNumber =double.Parse(MainDisplay.Text);
                    _operator = input;
                    ExpressionDisplay.Text = $"{_firstNumber} {_operator}";
                    _newinput = true;    
                    break;
                case "=":
                    if (_operator == "") break;
                    double second = double.Parse(MainDisplay.Text,
                    CultureInfo.InvariantCulture);
                    ExpressionDisplay.Text = $"{_firstNumber} {_operator} {second} =";
                    double result = Calculate(_firstNumber, second, _operator);
                    MainDisplay.Text = result.ToString(CultureInfo.InvariantCulture);
                    _operator = "";
                    _newinput = true;
                    break;
                case ".":
                    if (_newinput) { MainDisplay.Text = "0."; _newinput = false; }
                    else if (!MainDisplay.Text.Contains(".")) { MainDisplay.Text += "."; }
                    break;
                default:
                    if(_newinput) { MainDisplay.Text = input; _newinput = false;}
                    else MainDisplay.Text = MainDisplay.Text == "0"? input : MainDisplay.Text + input;
                    break;


            }
        }

        private double Calculate(double a, double b, string op) => op switch
        {
            "+" => a + b,
            "-" => a - b,
            "×" => a * b,
            "÷" => b != 0 ? a / b : double.NaN,
            _ => b
        };
    }
}