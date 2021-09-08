using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.ViewModels
{
    public class AsyncCommand : AsyncCommandBase
    {
        private readonly Func<object, Task> command;
        private readonly Predicate<object> canExecute;

        public AsyncCommand(Func<object, Task> command)
            : this(command, null)
        {
            this.command = command;
        }

        public AsyncCommand(Func<object, Task> command, Predicate<object> canExecute)
        {
            this.command = command;
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }
        public override async Task ExecuteAsync(object parameter)
        {
            await command(parameter);
        }
    }
}
