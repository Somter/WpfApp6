using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp6
{
    public class RelayCommand : ICommand
    {
     
        private readonly Func<bool> canExecuteFunc;
        private readonly Action<object> executeAction;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
            : this(_ => execute(), canExecute)
        {
        }

        public RelayCommand(Action<object> execute, Func<bool> canExecute = null)
        {
            this.executeAction = execute;
            canExecuteFunc = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteFunc?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
