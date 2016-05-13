using Windows.Phone.UI.Input;
using Windows.UI.Core;
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
                HardwareButtons.BackPressed += Page_HardwareButtonsBackPressed;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().BackRequested += Page_BackRequested;
            }
        }

        protected virtual void Page_HardwareButtonsBackPressed(object sender, BackPressedEventArgs e)
        {
            if (e.Handled)
            {
                return;
            }
            if (IsActiveSessionPage)
            {
                if (RootFrame != null)
                {
                    var pageInSessionHookHardwareBackButton = RootFrame.Content as IPageInSessionHookBackRequest;
                    if (pageInSessionHookHardwareBackButton != null)
                    {
                        var handled = pageInSessionHookHardwareBackButton.RootPage_BackRequested(e.Handled);
                        if (handled)
                        {
                            e.Handled = true;
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

        protected virtual void Page_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (e.Handled)
            {
                return;
            }
            if (IsActiveSessionPage)
            {
                if (RootFrame != null)
                {
                    var pageInSessionHookHardwareBackButton = RootFrame.Content as IPageInSessionHookBackRequest;
                    if (pageInSessionHookHardwareBackButton != null)
                    {
                        var handled = pageInSessionHookHardwareBackButton.RootPage_BackRequested(e.Handled);
                        if (handled)
                        {
                            e.Handled = true;
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
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
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
