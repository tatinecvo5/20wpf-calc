using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double firstNumber, secondNumber, resultNumber = 0;
        bool calcDone = false;
        Operations operation = Operations.None;
        string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public MainWindow()
        {
            InitializeComponent();

            //Assign to the decimal button the separator from the current culture
            dec.Content = separator;
        }

        //List the possible numeric operations
        private enum Operations
        {
            None,
            Division,
            Multiplication,
            Subtraction,
            Sum
        }

        //Manage number buttons input
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (calcDone) //calculation already done
            {
                result.Content = $"{button.Content}";
                calcDone = false;
            }
            else //calculation not yet done
            {
                if (result.Content.ToString() == "0")
                {
                    result.Content = $"{button.Content}";
                }
                else
                {
                    result.Content = $"{result.Content}{button.Content}";
                }
            }

        }

        //Кнопка ввода
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            //Кнопка сброса
            switch (button.Content.ToString())
            {
                case "AC":
                    result.Content = "0";
                    break;
                case "+/-":
                    if (!(result.Content.ToString() == "0"))
                    {
                        result.Content = Convert.ToDouble(result.Content) * -1;
                    }
                    break;
                case "%":
                    if (!(result.Content.ToString() == "0"))
                    {
                        result.Content = Convert.ToDouble(result.Content) / 100;
                    }
                    break;
                case "÷":
                    firstNumber = Convert.ToDouble(result.Content);
                    operation = Operations.Division;
                    result.Content = "0";
                    break;
                case "×":
                    firstNumber = Convert.ToDouble(result.Content);
                    operation = Operations.Multiplication;
                    result.Content = "0";
                    break;
                case "-":
                    firstNumber = Convert.ToDouble(result.Content);
                    operation = Operations.Subtraction;
                    result.Content = "0";
                    break;
                case "+":
                    firstNumber = Convert.ToDouble(result.Content);
                    operation = Operations.Sum;
                    result.Content = "0";
                    break;
                case "=":
                    switch (operation)
                    {
                        case Operations.Division:
                            if (calcDone)
                            {
                                if (!(result.Content.ToString() == "ERROR"))
                                {
                                    result.Content = Convert.ToDouble(result.Content) / secondNumber;
                                }
                            }
                            else
                            {
                                //Проверка деления на ноль
                                if (result.Content.ToString() == "0")
                                {
                                    result.Content = "ERROR";
                                    calcDone = true;
                                }
                                else
                                {
                                    secondNumber = Convert.ToDouble(result.Content);
                                    resultNumber = firstNumber / secondNumber;
                                    result.Content = resultNumber;
                                    calcDone = true;
                                }
                            }
                            break;
                        case Operations.Multiplication:
                            if (calcDone)
                            {
                                result.Content = Convert.ToDouble(result.Content) * secondNumber;
                            }
                            else
                            {
                                secondNumber = Convert.ToDouble(result.Content);
                                resultNumber = firstNumber * secondNumber;
                                result.Content = resultNumber;
                                calcDone = true;
                            }
                            break;
                        case Operations.Subtraction:
                            if (calcDone)
                            {
                                result.Content = Convert.ToDouble(result.Content) - secondNumber;
                            }
                            else
                            {
                                secondNumber = Convert.ToDouble(result.Content);
                                resultNumber = firstNumber - secondNumber;
                                result.Content = resultNumber;
                                calcDone = true;
                            }
                            break;
                        case Operations.Sum:
                            if (calcDone)
                            {
                                result.Content = Convert.ToDouble(result.Content) + secondNumber;
                            }
                            else
                            {
                                secondNumber = Convert.ToDouble(result.Content);
                                MessageBox.Show($"{firstNumber} + {secondNumber}");
                                resultNumber = firstNumber + secondNumber;
                                result.Content = resultNumber;
                                calcDone = true;
                            }
                            break;
                    }
                    break;
                default:
                    if (!result.Content.ToString().Contains(separator))
                    {
                        result.Content = $"{result.Content}{button.Content}";
                    }
                    break;
            }
        }
    }
}