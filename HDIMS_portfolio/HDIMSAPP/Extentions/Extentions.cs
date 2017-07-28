using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace HDIMSAPP.Extentions
{
    public static class Extensions
    {
        /// <summary>
        /// http://forums.silverlight.net/t/122515.aspx/1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T FindParentOfType<T>(this FrameworkElement element)
        {
            var parent = VisualTreeHelper.GetParent(element) as FrameworkElement;


            while (parent != null)
            {
                if (parent is T)
                    return (T)(object)parent;


                parent = VisualTreeHelper.GetParent(parent) as FrameworkElement;
            }
            return default(T);
        }
    }
}
