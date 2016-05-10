using System;
using System.Linq;
using Windows.UI.Xaml;

namespace CJUWPExtension.Common
{

    public class ResourceDictionaryResolver
    {

        public static T GetResource<T>(string name)
        {
            if (Application.Current.Resources.ContainsKey(name) && Application.Current.Resources[name] is T)
                return (T)Application.Current.Resources[name];
            foreach (var resource in Application.Current.Resources.MergedDictionaries.Where(resource => resource.ContainsKey(name)).Where(resource => resource[name] is T))
            {
                return (T)resource[name];
            }
#if DEBUG
            throw new ArgumentOutOfRangeException(nameof(name), string.Format("There is no resource of name {0} with type {1} in this application", name, typeof(T).Name));
#else
            return default(T);
#endif
        }
    }

}