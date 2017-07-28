using System;
using System.Linq;
using System.Windows.Input;
using Infragistics.Controls;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Grids.Primitives;

namespace HDIMSAPP.Common.Commands
{
    public class CustomNextPageCommandSource : CommandSource
    {
        private CustomNextPageCommand Command;
        protected override ICommand ResolveCommand()
        {
            if (Command == null) Command = new CustomNextPageCommand();
            return Command;
        }
    }

    public class CustomNextPageCommand : PagingBaseCommand
    {
        int LastPageIndex = -1;
        public override bool CanExecute(object parameter)
        {
            //XamGridRowsManager p = parameter as XamGridRowsManager;

            //if (LastPageIndex == -1) CalcLastPageIndex(p.ColumnLayout.Grid);

            //if (p.CurrentPageIndex == LastPageIndex) return false;
            return true;
        }

        protected override void ExecutePaging(RowsManager rowsBase)
        {
            // Your Button Click code goes here: 
            XamGrid grid = rowsBase.ColumnLayout.Grid;

            if (LastPageIndex == -1) CalcLastPageIndex(grid);
            
            //MessageBox.Show("grid.ItemsSource.Cast<object>().Count():" + grid.ItemsSource.Cast<object>().Count().ToString() + " / grid.PagerSettings.PageSize : " + grid.PagerSettings.PageSize.ToString() + " / rowsBase.CurrentPageIndex + 10 :" + (rowsBase.CurrentPageIndex + 10).ToString() + " / LastPageIndex : " + LastPageIndex.ToString());

            if (LastPageIndex < rowsBase.CurrentPageIndex + 10)
            {
                grid.PagerSettings.CurrentPageIndex = LastPageIndex;
            }
            else
            {
                grid.PagerSettings.CurrentPageIndex = rowsBase.CurrentPageIndex + 10;
            }
        }

        private void CalcLastPageIndex(XamGrid grid)
        {
            double d = (double)grid.ItemsSource.Cast<object>().Count() / (double)grid.PagerSettings.PageSize;
            LastPageIndex = int.Parse(Math.Ceiling(d).ToString());
        }
    }
}
