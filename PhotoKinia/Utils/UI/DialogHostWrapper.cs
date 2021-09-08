using MaterialDesignThemes.Wpf;
using PhotoKinia.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.Utils.UI
{
    class DialogHostWrapper : IDialogHostWrapper
    {
        public async Task ShowAsync(object o, string dialogID, DialogOpenedEventHandler openedEventHandler, DialogClosingEventHandler closingEventHandler)
        {
            await DialogHost.Show(o, dialogID, openedEventHandler, closingEventHandler);
        }
    }
}
