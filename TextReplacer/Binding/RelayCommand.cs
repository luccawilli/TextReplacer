﻿using System;
using System.Windows.Input;

namespace TextReplacer.Binding {
  /// <summary>
  /// A command whose sole purpose is to 
  /// relay its functionality to other
  /// objects by invoking delegates. The
  /// default return value for the CanExecute
  /// method is 'true'.
  /// </summary>
  // Source: https://stackoverflow.com/questions/3531772/binding-button-click-to-a-method
  public class RelayCommand : ICommand {
    #region Fields

    readonly Action<object> _execute;
    readonly Predicate<object> _canExecute;

    #endregion // Fields

    #region Constructors

    /// <summary>
    /// Creates a new command that can always execute.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    public RelayCommand(Action<object> execute)
        : this(execute, null) {
    }

    /// <summary>
    /// Creates a new command.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    /// <param name="canExecute">The execution status logic.</param>
    public RelayCommand(Action<object> execute, Predicate<object> canExecute) {
      _execute = execute ?? throw new ArgumentNullException("execute");
      _canExecute = canExecute;
    }

    #endregion // Constructors

    #region ICommand Members

    public bool CanExecute(object parameters) {
      return _canExecute == null || _canExecute(parameters);
    }

    public event EventHandler CanExecuteChanged {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(object parameters) {
      _execute(parameters);
    }

    #endregion // ICommand Members
  }
}
