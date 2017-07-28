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
using System.Collections.Generic;
using Infragistics.Collections;

namespace HDIMSAPP.Controls
{
    public class CRUDCollection<T> : VirtualCollection<T>
    {
        
        //VirtualCollection<T> vc = VirtualCollection<T>();
        IList<T> AddedItems = new List<T>();
        IList<T> RemovedItems = new List<T>();
        public void test()
        {
            Infragistics.Controls.Grids.XamGrid
            base.RemoveItem += new EventHandler<RemoveItemEventArgs<T>>(CRUDCollection_RemoveItem);
            base.AddNewItem += new EventHandler<AddNewItemEventArgs<T>>(CRUDCollection_AddNewItem);
            base.edit
        }

        void CRUDCollection_AddNewItem(object sender, AddNewItemEventArgs<T> e)
        {
            base.Add(e.DefaultValue);
            AddedItems.Add(e.DefaultValue);
        }

        void CRUDCollection_RemoveItem(object sender, RemoveItemEventArgs<T> e)
        {
            RemovedItems.Add(e.RemovedItem);
        }
    }
}
