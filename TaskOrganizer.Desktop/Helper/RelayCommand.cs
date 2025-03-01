using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskOrganizer.Desktop.Helper
{
  public class RelayCommand(Action execute, Func<bool>? canExecute = null) : ICommand
  {
    private readonly Action execute = execute ?? throw new ArgumentNullException(nameof(execute));
    private readonly Func<bool>? canExecute = canExecute;

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
      return canExecute == null || canExecute();
    }

    public void Execute(object? parameter)
    {
      execute();
    }

    public void RaiseCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}
