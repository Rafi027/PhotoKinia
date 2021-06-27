using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.Contracts
{
    interface IDialogHostWrapper
    {
        Task ShowAsync(object o, string dialogID, DialogOpenedEventHandler openedEventHandler, DialogClosingEventHandler closingEventHandler);
    }
}
