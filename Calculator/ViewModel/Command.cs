using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModel
{
    class Command : ICommand
    {
        public Action<object> execute;
        public Predicate<object> canExecute;

        public Command(Action<object> execute, Predicate<object> canChange)
        {
            this.execute = execute;
            this.canExecute = canChange;
        }

        public Command(Action<object> execute)
            :this(execute, null)
        {

        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            execute.Invoke(parameter);
        }
    }
}
