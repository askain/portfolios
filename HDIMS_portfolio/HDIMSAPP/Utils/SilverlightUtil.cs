using System;
using System.Windows;
using System.Windows.Media;

namespace HDIMSAPP.Utils
{
    public class SilverlightUtil
    {
        private static string HostUri = string.Empty;
        public static string GetHostUri() 
        {
            if(HostUri == string.Empty) {
                Uri uri = Application.Current.Host.Source;
                UriBuilder Host = new UriBuilder(uri.Scheme, uri.Host, uri.Port);
                HostUri = Host.ToString();
            }

            return HostUri;
        }

        public static FrameworkElement GetParent(FrameworkElement child, Type targetType)
        {
            object parent = child.Parent;
            if (parent != null)
            {
                if (parent.GetType() == targetType)
                {
                    return (FrameworkElement)parent;
                }
                else
                {
                    return GetParent((FrameworkElement)parent, targetType);
                }
            }
            return null;
        }


        /// <summary>
        /// 이름으로 자식찾기
        /// 찾을때까지 recursive 파고들긔 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FrameworkElement GetChildFromName(DependencyObject parent, string name)
        {
            FrameworkElement result = null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                if (VisualTreeHelper.GetChild(parent, i) is FrameworkElement
                    && (VisualTreeHelper.GetChild(parent, i) as FrameworkElement).Name.Equals(name))
                {
                    return VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                }
                else
                {
                    result = GetChildFromName(VisualTreeHelper.GetChild(parent, i), name);

                    if (result != null)
                    {
                        break;
                    }
                }
            }
            return result;
        }

    }
}
