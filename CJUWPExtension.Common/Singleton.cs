using System;

namespace CJUWPExtension.Common
{

    public abstract class Singleton<T>
        where T : new ()
    {

        private static readonly Lazy<T> _sharedInstance = new Lazy<T>();

        public static T SharedInstance => _sharedInstance.Value;

    }

}
