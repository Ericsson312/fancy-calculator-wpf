using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using org.mariuszgromada.math.mxparser;
using Expression = org.mariuszgromada.math.mxparser.Expression;

namespace Calculator.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private string lowerText = "0";
        private string apperText = "";
        private const int lenghtOfLowerText = 13;
        private const int lengthOfApperText = 22;
        private int fontSize = 52;
        private bool pointPressed = false;
        private bool operatorPressed;
        private double result = 0;
        private Expression expression;
        private bool firstOperationComplete;
        private string previousOpearation = "";

        public MainWindowViewModel()
        {
            ClickOnNumber = new Command(AddNumber, null);
            ClickOnPoint = new Command(AddPoint, null);
            ClickOnCE = new Command(RemoveInput, null);
            ClickOnB = new Command(RemoveLastNumber, null);
            ClickOnC = new Command(Reset, null);
            ClickOnArithmeticalOperation = new Command(ArithmeticalOperation, null);
        }

        // previous user input 
        public string ApperText
        {
            get
            {
                return apperText;
            }
            set
            {
                apperText = value;
                OnProtertyChanged("ApperText");
            }
        }

        // current user input number
        public string Text
        {
            get
            {
                return lowerText;
            }
            set
            {
                lowerText = value;
                OnProtertyChanged("Text");
            }

        }

        public int FontSize
        {
            get
            {
                return fontSize;
            }
            set
            {
                fontSize = value;
                OnProtertyChanged("FontSize");
            }
        }

        public ICommand ClickOnNumber
        {
            get;
            set;
        }

        public ICommand ClickOnPoint
        {
            get;
            set;
        }

        public ICommand ClickOnCE
        {
            get;
            set;
        }

        public ICommand ClickOnB
        {
            get;
            set;
        }

        public ICommand ClickOnC
        {
            get;
            set;
        }

        public ICommand ClickOnArithmeticalOperation
        {
            get;
            set;
        }

        public void AddNumber(object parameter)
        {
            string number = parameter as string;

            if(number == "0" && Text == "0")
            {
                Text = "0";
            }
            else
            {
                // remove default 0 before adding a number
                if(Text == "0" || operatorPressed)
                {
                    Text = "";
                }

                if(lenghtOfLowerText > Text.Length)
                {
                    Text += number;
                    FontSizeManagement();
                }

                operatorPressed = false;
            }
        }

        public void AddPoint(object parameter)
        {
            if(!pointPressed && Text.Length < 12)
            {
                if (operatorPressed)
                {
                    Text = "";
                }

                if (Text.EndsWith("-") || Text == "")
                {
                    Text += "0";
                }
                Text += ".";
                pointPressed = true;
                operatorPressed = false;
                FontSizeManagement();
            }
        }

        public void ArithmeticalOperation(object parameter)
        {
            string operationType = parameter as string;

            if (operationType == "=")
            {
                if (firstOperationComplete)
                {
                    expression = new Expression(ApperText + Text);
                }
                else
                {
                    expression = new Expression(result + previousOpearation + Text);
                }
                ApperText = "";
                Text = expression.calculate().ToString();
                result = 0;
                return;
            }

            if (ApperText == "" && operationType == "-" && Text.Length == 1)
            {
                Text = "-";
                return;
            }
            else
            {
                if (ApperText == "")
                {
                    firstOperationComplete = true;
                    ApperText += Text + " " + operationType + " ";
                }
                else
                {
                    ApperText += Text + " ";

                    if (firstOperationComplete)
                    {
                        expression = new Expression(ApperText);
                        result = expression.calculate();
                        previousOpearation = operationType;
                        firstOperationComplete = false;
                    }
                    else
                    {
                        expression = new Expression(result + previousOpearation + Text);
                        result = expression.calculate();
                    }

                    Text = result.ToString();
                    previousOpearation = operationType;
                    ApperText += operationType + " ";
                }

                operatorPressed = true;
            }

            pointPressed = false;
        }

        public void RemoveInput(object parameter)
        {
            Text = "0";
            pointPressed = false;
        }

        public void RemoveLastNumber(object parameter)
        {
            if (Text != "0")
            {
                if (Text.Length == 1)
                {
                    Text = "0";
                }
                else
                {
                    if (Text.Substring(Text.Length - 1, 1) == ".")
                    {
                        pointPressed = false;
                    }
                        
                    Text = Text.Substring(0, Text.Length - 1);
                    FontSizeManagement();
                }
            }
        }

        public void Reset(object parameter)
        {
            Text = "0";
            ApperText = "";
            pointPressed = false;
            FontSizeManagement();
        }

        public void FontSizeManagement()
        {
            if (Text.Length > 7 && Text.Length <= 9)
                FontSize = 45;
            else if (Text.Length > 9 && Text.Length <= 11)
                FontSize = 38;
            else if (Text.Length > 11)
                FontSize = 32;
            else
                FontSize = 52;

        }
    }
}
