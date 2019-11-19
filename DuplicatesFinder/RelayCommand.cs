using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace DuplicatesFinder.ViewModel
{
    /// <summary>
    /// A command whose sole purpose is to 
    /// relay its functionality to other
    /// objects by invoking delegates. The
    /// default return value for the CanExecute
    /// method is 'true'.
    /// </summary>
    public class RelayCommand : BindingItems, ICommand
    {
        Action execute;
        Func<object, bool> canExecute;


        #region Constructors
        public RelayCommand(Action execute)
        {
            this.execute = execute;
        }

        public RelayCommand(Action execute, string commandName) : this(execute)
        {
            this.commandName = commandName;
        }

        public RelayCommand(Action execute, string commandName, bool enabledModificate) : this(execute, commandName)
        {
            IsEnabledModificate = enabledModificate;
        }

        public RelayCommand(Action execute, Func<object, bool> canExecute = null) : this(execute)
        {
            this.canExecute = canExecute;
        }
        #endregion


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute();
        }

        bool isEnabledModificate = true;
        public bool IsEnabledModificate
        {
            get { return isEnabledModificate; }
            set { SetField(ref isEnabledModificate, value); }
        }

        string commandName;
        public string CommandName
        {
            get { return commandName; }
            set { SetField(ref commandName, value); }
        }
    }
}
