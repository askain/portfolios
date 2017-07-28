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

namespace HDIMSMAIN.Models
{
    public class KeyValue<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }
        public bool Selected { get; set; }
    }
}
