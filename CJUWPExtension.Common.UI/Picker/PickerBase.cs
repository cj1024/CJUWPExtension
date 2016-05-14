using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CJUWPExtension.Common.UI.Session;

namespace CJUWPExtension.Common.UI.Picker
{

    public abstract class PickerBase : Control
    {

        protected abstract PickerPageNavigationInfo PickerPageInfo();

        internal ApplicationSessionPage SessionPage { get; set; }
        
        public void Open()
        {
            WillOpen();
            var pickerPageInfo = PickerPageInfo();
            pickerPageInfo.Picker = this;
            (Window.Current.Content as Frame)?.Navigate(typeof(PickerRootPage), pickerPageInfo, pickerPageInfo.NavigationTransitionInfo);
        }

        protected virtual void WillOpen()
        {
            
        }

        public void Close()
        {
            WillClose();
            SessionPage.TryCloseSession();
        }

        protected virtual void WillClose()
        {

        }

    }

}
