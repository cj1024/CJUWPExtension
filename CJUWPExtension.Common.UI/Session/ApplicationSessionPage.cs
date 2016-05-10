using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace CJUWPExtension.Common.UI.Session
{
    
    public abstract class ApplicationSessionPage : Page
    {

        public bool IsClosedSessionPage { get; private set; }

        private NavigationTransitionInfo CloseSessionTransitionInfoOverride { get; set; }

        public bool IsActiveSessionPage { get; private set; }

        public bool IsSessionPageGoBacked { get; private set; }

        public abstract ApplicationSessionFrame RootFrame { get; }

        protected ApplicationSessionPage()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += Page_HardwareButtonsBackPressed;
            }
        }

        protected virtual void Page_HardwareButtonsBackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (e.Handled)
            {
                return;
            }
            if (IsActiveSessionPage)
            {
                if (RootFrame != null)
                {
                    var pageInSessionHookHardwareBackButton = RootFrame.Content as IPageInSessionHookHardwareBackButton;
                    if (pageInSessionHookHardwareBackButton != null)
                    {
                        pageInSessionHookHardwareBackButton.RootPage_HardwareButtonsBackPressed(sender, e);
                        if (e.Handled)
                        {
                            return;
                        }
                    }
                    if (RootFrame.CanGoBack)
                    {
                        e.Handled = true;
                        RootFrame.GoBack();
                    }
                    else if (Frame != null)
                    {
                        if (Frame.CanGoBack)
                        {
                            e.Handled = true;
                            Frame.GoBack();
                        }
                    }
                }
                else if (Frame != null)
                {
                    if (Frame.CanGoBack)
                    {
                        e.Handled = true;
                        Frame.GoBack();
                    }
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (RootFrame != null)
            {
                RootFrame.RootPage = this;
                if (RootFrame.Content == null)
                {
                    DoNavigationToRootPage(e.Parameter);
                }
            }
            IsSessionPageGoBacked = false;
            BecomeActivePage();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            IsSessionPageGoBacked = e.NavigationMode == NavigationMode.Back;
            ResignActivePage();
        }

        protected abstract void DoNavigationToRootPage(object parameter);

        protected virtual void BecomeActivePage()
        {
            IsActiveSessionPage = true;
            RootFrame?.BecomeActiveFrame();
            if (IsClosedSessionPage)
            {
                TryCloseSession(CloseSessionTransitionInfoOverride);
            }
        }

        protected virtual void ResignActivePage()
        {
            IsActiveSessionPage = false;
            RootFrame?.ResignActiveFrame();
        }

        public void TryCloseSession()
        {
            TryCloseSession(null);
        }

        public void TryCloseSession(NavigationTransitionInfo transitionInfoOverride)
        {
            IsClosedSessionPage = true;
            CloseSessionTransitionInfoOverride = transitionInfoOverride;
            if (IsActiveSessionPage && Frame.Content == this && Frame.CanGoBack)
            {
                Frame.GoBack(CloseSessionTransitionInfoOverride);
            }
        }

    }

}
