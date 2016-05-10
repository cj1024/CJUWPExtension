using Windows.UI;

namespace CJUWPExtension.Common
{

    public class ColorExtensions
    {

        public static Color ColorFromArgb(uint hex)
        {
            return Color.FromArgb((byte)((hex & 0xFF000000) >> 24), (byte)((hex & 0x00FF0000) >> 16), (byte)((hex & 0x0000FF00) >> 8), (byte)((hex & 0x000000FF) >> 0));
        }

        public static Color ColorFromRgb(uint hex)
        {
            return Color.FromArgb(0xFF, (byte)((hex & 0x00FF0000) >> 16), (byte)((hex & 0x0000FF00) >> 8), (byte)((hex & 0x000000FF) >> 0));
        }

    }

}
