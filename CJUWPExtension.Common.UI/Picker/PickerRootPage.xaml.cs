using System;
using Windows.UI.Xaml.Media.Animation;
using CJUWPExtension.Common.UI.Session;

namespace CJUWPExtension.Common.UI.Picker
{

    public class PickerPageNavigationInfo
    {

        public Type PickerPageType { get; set; }

        public object PickerPageParameter { get; set; }

        public NavigationTransitionInfo NavigationTransitionInfo { get; set; }

        internal PickerBase Picker { get; set; }

    }

    public sealed partial class PickerRootPage
    {

        public PickerPageNavigationInfo PickerPageNavigationInfo { get; private set; }

        public PickerRootPage()
        {
            InitializeComponent();
        }

        public override ApplicationSessionFrame RootFrame => RootFrameInstance;

        protected override void DoNavigationToRootPage(object parameter)
        {
            PickerPageNavigationInfo = parameter as PickerPageNavigationInfo;
            if (PickerPageNavigationInfo?.Picker != null)
            {
                PickerPageNavigationInfo.Picker.SessionPage = this;
            }
            RootFrame.Navigate(PickerPageNavigationInfo?.PickerPageType, this, PickerPageNavigationInfo?.NavigationTransitionInfo);
        }

    }

}
