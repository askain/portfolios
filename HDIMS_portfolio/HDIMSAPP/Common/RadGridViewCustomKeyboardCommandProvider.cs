using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using HDIMSAPP.Controls;
using HDIMSAPP.Models;
using HDIMSAPP.Utils;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace HDIMSAPP.Common
{
    #region == SetDirtyColorCommand ==
    public class SetDirtyColorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private GridViewDataControl gridview;


        public SetDirtyColorCommand(GridViewDataControl gridview) 
        {
            this.gridview = gridview;
        }

        public bool CanExecute(object parameter)
        {
            return (gridview.SelectedCells != null && gridview.SelectedCells.Count > 0);
        }

        public void Execute(object parameter)
        {
            //gridview.IsEnabled = false;
            //색깔 바꾸어주기
            foreach (GridViewCellInfo cellInfo in gridview.SelectedCells)
            {
                if (cellInfo.Column.UniqueName.Contains("WL_") == true || cellInfo.Column.UniqueName.Contains("ACURF_") == true || cellInfo.Column.UniqueName.Contains("RF_") == true)
                {
                    //Deployment.Current.Dispatcher.BeginInvoke(() => gridview.GetRowForItem(cellInfo.Item).Cells[cellInfo.Column.DisplayIndex].Background = Constants.DirtyColor);
                    gridview.GetRowForItem(cellInfo.Item).Cells[cellInfo.Column.DisplayIndex].Background = Constants.DirtyColor;
                    //System.Threading.Thread.Sleep(500);
                       
                }
            }

            //gridview.IsEnabled = false; //UI freeze 버그생김 생김
            //gridview.IsEnabled = true; //UI freeze 버그생김 생김
        }
    }
    #endregion

    #region == RevertClipboardTextCommand ==
    public class RevertClipboardTextCommand : RoutedUICommand
    {
        public event EventHandler CanExecuteChanged;

        public string clipboardText = string.Empty;

        public RevertClipboardTextCommand(string clipboardText, string text, string name, Type ownerType) : base(text, name, ownerType)
        {
            this.clipboardText = clipboardText;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MessageBox.Show(clipboardText);
            System.Windows.Clipboard.SetText(clipboardText);
        }
    }
    #endregion

    #region == CustomKeyboardCommandProvider ==
    class CustomKeyboardCommandProvider : DefaultKeyboardCommandProvider
    {
        private GridViewDataControl parentGrid;
        //private DefaultKeyboardCommandProvider defaultKeyboardProvider;
        //private CustomKeyboardCommandProvider customKeyboardProvider;
        public CustomKeyboardCommandProvider(GridViewDataControl grid) : base(grid)
        {
            this.parentGrid = grid;
        }


        public string stringExtendsBy(string ori, int rowTime, int colTime)
        {
            //List<List<string>> values = ClipboardHelper.ParseStringToTabSeparatedValues(ori);


            string temp = ConcatHorizon(ori, colTime);
            string result = ConcatVertical(temp, rowTime);

            char[] a = result.ToArray();

            return result.Trim();
        }

        public string ConcatVertical(string t, int times)
        {
            StringBuilder sb = new StringBuilder(t);
            for (int i = 1; i < times; i++)
            {
                if (sb.ToString().Last().Equals(Environment.NewLine) == false && sb.ToString().Last().Equals('\n') == false) sb.Append(Environment.NewLine);
                sb.Append(t);
            }
            return sb.ToString();
        }

        public string ConcatHorizon(string t, int times) 
        {
            StringBuilder result = new StringBuilder();
            string[] lines = t.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            IList<string> temp = new List<string>();
            foreach (string s in lines) 
            {
                StringBuilder sb = new StringBuilder(s);
                for (int i = 1; i < times; i++)
                {
                    sb.Append("\t");
                    sb.Append(s);
                }
                temp.Add(sb.ToString());
            }

            foreach (string s in temp)
            {
                if(result.Length > 0) result.Append(Environment.NewLine);
                result.Append(s);
            }


            return result.ToString();
        }


        public override IEnumerable<ICommand> ProvideCommandsForKey(Key key)
        {
            List<ICommand> commandsToExecute = base.ProvideCommandsForKey(key).ToList();

            try
            {
                if (commandsToExecute.Contains(RadGridViewCommands.Paste) == true)
                {

                    //RevertClipboardTextCommand lastCommand = new RevertClipboardTextCommand(Telerik.Windows.Controls.Clipboard.GetText(), "Text", "RevertClipBoard", typeof(GridViewDataControl));
                    //commandsToExecute.Add(lastCommand);
                    //commandsToExecute.Insert(0, lastCommand);

                    List<List<string>> values = ClipboardHelper.ParseStringToTabSeparatedValues(Telerik.Windows.Controls.Clipboard.GetText());
                    int clipBoardColCnt = values[0].Count;
                    int clipBoardRowCnt = values.Count;
                    int selColCnt = (from cell in parentGrid.SelectedCells select cell.Column).Distinct<GridViewColumn>().Count();
                    int selRowCnt = (from cell in parentGrid.SelectedCells select cell.Item).Distinct<object>().Count();

                    if (parentGrid.SelectedCells.Count > 1 &&
                        (
                            ((selRowCnt % clipBoardRowCnt) == 0 && selColCnt == 1) ||
                            ((selColCnt % clipBoardColCnt) == 0 && selRowCnt == 1) ||
                            (((selColCnt % clipBoardColCnt) == 0 && selColCnt > 1 && (selRowCnt >= clipBoardRowCnt)) && ((selRowCnt % clipBoardRowCnt) == 0 && selRowCnt > 1 && (selColCnt >= clipBoardColCnt)))
                        )
                    )
                    {
                        //MessageBox.Show("selRowCnt:" + selRowCnt + ", clipBoardRowCnt:" + clipBoardRowCnt);
                        //MessageBox.Show("selColCnt:" + selColCnt + ", clipBoardColCnt:" + clipBoardColCnt);

                        //---------------
                        int TotalCountToPasteHorizon = Math.Abs(selColCnt / clipBoardColCnt);
                        int TotalCountToPasteVertical = Math.Abs(selRowCnt / clipBoardRowCnt);
                        if (TotalCountToPasteHorizon == 0) TotalCountToPasteHorizon = 1;
                        if (TotalCountToPasteVertical == 0) TotalCountToPasteVertical = 1;

                        string result = stringExtendsBy(Telerik.Windows.Controls.Clipboard.GetText(), TotalCountToPasteVertical, TotalCountToPasteHorizon);
                        System.Windows.Clipboard.SetText(result);
                        //Telerik.Windows.Controls.Clipboard.SetText(result);
                    }
                }
            }
            catch (Exception ex) 
            {
                commandsToExecute = base.ProvideCommandsForKey(key).ToList();
            }

            //MessageBox.Show(Telerik.Windows.Controls.Clipboard.GetText());
            /*
            if (commandsToExecute.Contains(RadGridViewCommands.Paste) == true)
            {
                commandsToExecute.Clear();

                //MessageBox.Show(Telerik.Windows.Controls.Clipboard.GetText());
                if (parentGrid.ClipboardPasteMode.Equals(GridViewClipboardPasteMode.None)) return commandsToExecute;

                List<List<string>> values = ClipboardHelper.ParseStringToTabSeparatedValues(Telerik.Windows.Controls.Clipboard.GetText());
                int clipBoardColCnt = values[0].Count;
                int clipBoardRowCnt = values.Count;
                int selColCnt = (from cell in parentGrid.SelectedCells select cell.Column).Distinct<GridViewColumn>().Count();
                int selRowCnt = (from cell in parentGrid.SelectedCells select cell.Item).Distinct<object>().Count();

                if (parentGrid.SelectedCells.Count > 1 &&
                    (
                        ((selRowCnt % clipBoardRowCnt) == 0 && selColCnt == 1) ||
                        ((selColCnt % clipBoardColCnt) == 0 && selRowCnt == 1) ||
                        (((selColCnt % clipBoardColCnt) == 0 && selColCnt > 1 && (selRowCnt >= clipBoardRowCnt)) && ((selRowCnt % clipBoardRowCnt) == 0 && selRowCnt > 1 && (selColCnt >= clipBoardColCnt)))
                    )
                )
                {
                    //MessageBox.Show("selRowCnt:" + selRowCnt + ", clipBoardRowCnt:" + clipBoardRowCnt);
                    //MessageBox.Show("selColCnt:" + selColCnt + ", clipBoardColCnt:" + clipBoardColCnt);

                    GridViewCellInfo firstCellInfo = parentGrid.SelectedCells.First();
                    GridViewRow firsRow = parentGrid.GetRowForItem(firstCellInfo.Item);
                    GridViewColumn firsCol = firstCellInfo.Column;
                    GridViewCell firstCell = firsRow.Cells[firstCellInfo.Column.DisplayIndex] as GridViewCell;
                    int ColumnCnt = parentGrid.Columns.Count;
                    int RowCnt = parentGrid.Items.Count;
                    GridViewCell currentCell = firstCell;
                    parentGrid.SelectedCells.Clear();

                    //---------------
                    int TotalCountToPasteHorizon = Math.Abs(selColCnt / clipBoardColCnt);
                    int TotalCountToPasteVertical = Math.Abs(selRowCnt / clipBoardRowCnt);
                    if (TotalCountToPasteHorizon == 0) TotalCountToPasteHorizon = 1;
                    if (TotalCountToPasteVertical == 0) TotalCountToPasteVertical = 1;
                    //MessageBox.Show("TotalCountToPasteHorizon:" + TotalCountToPasteHorizon.ToString() + ", TotalCountToPasteVertical:" + TotalCountToPasteVertical.ToString());
                    for (int VerticalCnt = 0; VerticalCnt < TotalCountToPasteVertical; VerticalCnt++)
                    {
                        for (int HorizonCnt = 0; HorizonCnt < TotalCountToPasteHorizon; HorizonCnt++)
                        {
                            PasteAction(currentCell, values);
                            //------------- 다음 셀(열)로 넘어감 ----------------
                            int nextColumnIndex = currentCell.Column.DisplayIndex + values[0].Count;
                            if (ColumnCnt > nextColumnIndex)
                            {
                                currentCell = currentCell.ParentRow.Cells[nextColumnIndex] as GridViewCell;
                            }
                            else
                            {
                                break;//다음열이 없는 경우 다음줄로 넘어감
                            }
                            //--------------------------------------------------
                        }

                        //------------- 다음 셀(열)로 넘어감 ----------------
                        int nextRowIndex = parentGrid.Items.IndexOf(currentCell.ParentRow.Item) + values.Count;
                        if (RowCnt > nextRowIndex)
                        {
                            currentCell = parentGrid.GetRowForItem(parentGrid.Items[nextRowIndex]).Cells[firsCol.DisplayIndex] as GridViewCell;
                        }
                        else
                        {
                            break;//다음줄이 없는 경우 끝냄
                        }
                        //--------------------------------------------------

                    }
                }
                else
                {

                    GridViewCellInfo firstCellInfo = parentGrid.SelectedCells.First();
                    GridViewRow firsRow = parentGrid.GetRowForItem(firstCellInfo.Item);
                    GridViewCell firstCell = firsRow.Cells[firstCellInfo.Column.DisplayIndex] as GridViewCell;
                    parentGrid.SelectedCells.Clear();
                    PasteAction(firstCell, values);

                }
                //commandsToExecute.Add(new SetDirtyColorCommand(parentGrid));
            }
            */
            return commandsToExecute;
        }
        
        private void PasteAction(GridViewCell cell, List<List<string>> values)
        {
            //GridViewCellInfo firstCellInfo = parentGrid.SelectedCells.First();
            GridViewRow firsRow = parentGrid.GetRowForItem(cell.DataContext);
            GridViewColumn firsCol = cell.Column;
            GridViewCell firstCell = firsRow.Cells[cell.Column.DisplayIndex] as GridViewCell;
            int ColumnCnt = parentGrid.Columns.Count;
            int RowCnt = parentGrid.Items.Count;
            GridViewCell currentCell = firstCell;
            //GridViewRow currentRow = firsRow;

            foreach (IList<string> row in values)
            {
                //MessageBox.Show(row.Count.ToString());
                foreach (string value in row)
                {
                    if (currentCell.Column.UniqueName.Contains("WL_") == true || currentCell.Column.UniqueName.Contains("ACURF_") == true || currentCell.Column.UniqueName.Contains("RF_") == true)
                    {
                        //MessageBox.Show(currentCell.DataContext.ToString() + " / " + currentCell.Column.UniqueName + " / " + value);
                        //Deployment.Current.Dispatcher.BeginInvoke(() => CommonUtil.SetValue(currentCell.DataContext, currentCell.Column.UniqueName, value));
                        CommonUtil.SetValue(currentCell.DataContext, currentCell.Column.UniqueName, value);
                        //System.Threading.Thread.Sleep(500);
                        //IEnumerable<TextBlock> e = currentCell.ChildrenOfType<TextBlock>();
                        //foreach (TextBlock t in e)
                        //{
                        //    t.Text = value;
                        //}
                    }

                    GridViewCellInfo cellinfo = new GridViewCellInfo(currentCell);
                    parentGrid.SelectedCells.Add(cellinfo);
                    //CommonUtil.SetValue(currentCell..Item, currentCell.Column.UniqueName, value);

                    //------------- 다음 셀(열)로 넘어감 ----------------
                    int nextColumnIndex = currentCell.Column.DisplayIndex + 1;
                    if (ColumnCnt > nextColumnIndex)
                    {
                        currentCell = currentCell.ParentRow.Cells[nextColumnIndex] as GridViewCell;
                    }
                    else
                    {
                        break;//다음열이 없는 경우 다음줄로 넘어감
                    }
                    //--------------------------------------------------
                }

                //------------- 다음 셀(열)로 넘어감 ----------------
                int nextRowIndex = parentGrid.Items.IndexOf(currentCell.ParentRow.Item) + 1;

                if (RowCnt > nextRowIndex)
                {
                    currentCell = parentGrid.GetRowForItem(parentGrid.Items[nextRowIndex]).Cells[firsCol.DisplayIndex] as GridViewCell;
                }
                else
                {
                    break;//다음줄이 없는 경우 끝냄
                }
                //--------------------------------------------------

            }
        }
    }
    #endregion
}
