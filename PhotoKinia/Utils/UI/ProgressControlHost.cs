using MaterialDesignThemes.Wpf;
using PhotoKinia.Contracts;
using PhotoKinia.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.Utils.UI
{
    internal class ProgressControlHost : IDialogHostWrapper
    {
        public async Task ShowAsync(object o, string dialogID, DialogOpenedEventHandler openedEventHandler, DialogClosingEventHandler closingEventHandler)
        {
            var control = new OperationProcessingControl();
            control.DataContext = o;
            await DialogHost.Show(control, dialogID, openedEventHandler, closingEventHandler);
        }
    }
}
