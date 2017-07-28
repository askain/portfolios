using System.Windows.Input;
using Infragistics.Controls;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Grids.Primitives;

namespace HDIMSAPP.Common.Commands
{
    public class CustomPrevPageCommandSource : CommandSource
    {
        private CustomPrevPageCommand Command;
        protected override ICommand ResolveCommand()
        {
            if (Command == null) Command = new CustomPrevPageCommand();
            return Command;
        }
    }

    public class CustomPrevPageCommand : PagingBaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        protected override void ExecutePaging(RowsManager rowsBase)
        {
            // Your Button Click code goes here: 
            XamGrid grid = rowsBase.ColumnLayout.Grid;

            if (1 > rowsBase.CurrentPageIndex - 10)
            {
                grid.PagerSettings.CurrentPageIndex = 1;
            }
            else
            {
                grid.PagerSettings.CurrentPageIndex = rowsBase.CurrentPageIndex - 10;
            }
        }
    }
}
