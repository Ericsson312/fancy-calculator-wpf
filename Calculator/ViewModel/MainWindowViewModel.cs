using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Calculator.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private string lowerText = "0";
        private string apperText = "";
        private const int lenghtOfLowerText = 13;
        private const int lengthOfApperText = 22;
        private int fontSize = 52;
        private bool pointClickKey = false;
        private double previousOperationResult = 0;

        public MainWindowViewModel()
        {
            ClickOnNumber = new Command(AddNumber, null);
            ClickOnPoint = new Command(AddPoint, null);
            ClickOnCB = new Command(ChangeTextLength, null);
            ClickOnArithmeticOperation = new Command(ArithmeticOperation, null);
        }

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

        public ICommand ClickOnCB
        {
            get;
            set;
        }

        public ICommand ClickOnArithmeticOperation
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
                if(Text == "0")
                {
                    Text = "";
                }

                if(lenghtOfLowerText > Text.Length)
                {
                    Text += number;
                    FontSizeManagement();
                }
            }
        }

        public void AddPoint(object parameter)
        {
            if(!pointClickKey && Text.Length < 12)
            {
                Text += ".";
                pointClickKey = true;
                FontSizeManagement();
            }
        }

        public void ChangeTextLength(object parameter)
        {
            string param = parameter as string;
            if(param == "B" && Text != "0")
            {
                if(Text.Length == 1)
                    Text = "0";
                else
                {
                    if(Text.Substring(Text.Length - 1, 1) == ".")
                        pointClickKey = false;
                    Text = Text.Substring(0, Text.Length - 1);
                    FontSizeManagement();
                }
            }
            else if(param == "C")
            {
                Text = "0";
                pointClickKey = false;
                FontSizeManagement();
            }
        }

        public void FontSizeManagement()
        {
            if(Text.Length > 7 && Text.Length <= 9)
                FontSize = 45;
            else if(Text.Length > 9 && Text.Length <= 11)
                FontSize = 38;
            else if(Text.Length > 11)
                FontSize = 32;
            else
                FontSize = 52;

        }

        public void ArithmeticOperation(object parameter)
        {
            string operationType = parameter as string;

            if(ApperText == "")
            {
                ApperText += (Text + " " + operationType);
                previousOperationResult = Double.Parse(Text);
            }
            else if(operationType == "+")
            {
                ApperText += Text + " + ";
                previousOperationResult+= Double.Parse(Text);
                Text = "0";
            }
        }
    }
}
