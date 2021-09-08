using MaterialDesignThemes.Wpf;
using PhotoKinia.Contracts;
using PhotoKinia.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoKiniaTests.Mocks
{
    class DialogHostSpy : IDialogHostWrapper
    {
        EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
        public object PassedObject { get; private set; }
        public string DialogID { get; private set; }
        public int NumberOfCalls { get; private set; }

        public Task ShowAsync(object o, string dialogID, DialogOpenedEventHandler openedEventHandler, DialogClosingEventHandler closingEventHandler)
        {
            PassedObject = o;
            DialogID = dialogID;
            NumberOfCalls++;

            var viewModel = o as ProgressControlViewModel;
            viewModel.OnCompleted += ViewModel_OnCompleted;
            openedEventHandler.Invoke(this, null);
            waitHandle.WaitOne();
            return Task.CompletedTask;
        }

        private void ViewModel_OnCompleted(object sender, System.EventArgs e)
        {
            waitHandle.Set();
        }
    }
}
