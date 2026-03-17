using System.Globalization;
using System.Windows;
using System.Windows.Controls;

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
                    if (double.TryParse(MainDisplay.Text, out double pct))
                    {
                        MainDisplay.Text = (pct / 100).ToString();
                    }
                    break;
                case "+":
                case "-":
                case "×":
                case "÷":
                    _firstNumber = double.Parse(MainDisplay.Text);
                    _operator = input;
                    ExpressionDisplay.Text = $"{_firstNumber} {_operator}";
                    _newInput = true;
                    break;
                case "=":
                    if (_operator == "")
                    break;
                    double second = double.Parse(MainDisplay.Text, CultureInfo. InvariantCulture);
                    ExpressionDisplay.Text = $"{_firstNumber} {second} {_operator}=";
                    double result = Calculate(_firstNumber, second, _operator);
                    MainDisplay.Text = result.ToString(CultureInfo.InvariantCulture);
                    _operator = "";
                    _newInput = true;
                    break;
                case ".":
                    if(_newInput) { MainDisplay.Text = "0.";_newInput = false;}
                    else if (!MainDisplay.Text.Contains("."))
                        MainDisplay.Text += ".";
                    break;
                default:
                    if (_newInput) { MainDisplay.Text = input; _newInput = false; }
                    else MainDisplay.Text = MainDisplay.Text == "0" ? input : MainDisplay.Text + input;
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