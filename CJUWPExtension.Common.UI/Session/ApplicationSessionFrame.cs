using Windows.UI.Xaml.Controls;

namespace CJUWPExtension.Common.UI.Session
{

    public class ApplicationSessionFrame : Frame
    {

        public bool IsActiveSessionFrame { get; private set; }

        internal ApplicationSessionPage RootPage;

        internal void BecomeActiveFrame()
        {
            IsActiveSessionFrame = true;
        }

        internal void ResignActiveFrame()
        {
            IsActiveSessionFrame = false;
        }

    }

}
