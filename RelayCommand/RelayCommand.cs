﻿using System.Windows.Input;

namespace RelayCommand
{
    internal class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action<object> execute)
            : base(execute) { }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
            :base(execute, canExecute) { }
    }

    internal class RelayCommand<T> : ICommand
    {
        private Action<T> execute;

        private Func<T, bool> canExecute;

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

        public RelayCommand(Action<T> execute)
            : this(execute, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public void Execute(object parameter) => Execute(parameter);
        public bool CanExecute(object parameter) => CanExecute(parameter);

        private bool CanExecute(T parameter)
        {
            return canExecute != null && canExecute(parameter);
        }

        private void Execute(T parameter)
        {
            execute(parameter);
        }

        private static bool DefaultCanExecute(T param)
        {
            return true;
        }
    }
}