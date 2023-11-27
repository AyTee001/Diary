using System.Windows.Input;

namespace Diary.Commands
{
    public class RelayCommand(Action<object?> executeAction, Func<object?, bool> canExecuteFunc) : ICommand
    {
        private readonly Action<object?> _executeAction = executeAction;
        private readonly Func<object?, bool> _canExecuteFunc = canExecuteFunc;

        public bool CanExecute(object? parameter) => _canExecuteFunc(parameter);

        public void Execute(object? parameter) => _executeAction(parameter);

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public static void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
