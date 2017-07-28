using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using HDIMSAPP.Common;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Controls
{

    /// <summary>
    /// http://www.codeproject.com/Articles/115405/Double-Click-DataGrid-in-Silverlight
    /// </summary>
    public partial class DoubleClickDataGrid : DataGrid
    {
        public bool AllowPaste { get; set; }
 
        public event RowClickedHandler RowClicked;
        public event RowDoubleClickedHandler RowDoubleClicked;

        public event CellClickedHandler CellClicked;
        public event CellDoubleClickedHandler CellDoubleClicked;

        public delegate void RowClickedHandler(object sender, DataGridRowClickedArgs e);
        public delegate void RowDoubleClickedHandler(object sender, DataGridRowClickedArgs e);

        public delegate void CellClickedHandler(object sender, DataGridCellClickedArgs e);
        public delegate void CellDoubleClickedHandler(object sender, DataGridCellClickedArgs e);

        #region == Copy & Paste 추가 ==
        public event BeforePasteHandler BeforePaste;
        public event AfterPasteHandler AfterPaste;
        public delegate void BeforePasteHandler(object sender, PasteArgs e);
        public delegate void AfterPasteHandler(object sender, PasteArgs e);
        #endregion

        private DataGridRow _LastDataGridRow = null;
        private DataGridColumn _LastDataGridColumn = null;
        private DataGridCell _LastDataGridCell = null;
        private object _LastObject = null;
        private DateTime _LastClick = DateTime.MinValue;

        #region == Copy & Paste 추가 ==
        public bool IsPasteEnable = false;
        #endregion

        #region == 셀 선택기능 ==
        private SelectAction selectAction = null;
        public CellSelection cellSelection;
        #endregion


        private double _DoubleClickTime = 500;
        public double DoubleClickTime
        {
            get
            {
                return _DoubleClickTime;
            }
            set
            {
                _DoubleClickTime = value;
            }
        }

        public DoubleClickDataGrid()
        {
            //InitializeComponent();

            this.MouseLeftButtonUp += new MouseButtonEventHandler(DoubleClickDataGridClick);

            cellSelection = new CellSelection(this);
            #region == Copy & Paste 추가 ==
            //this.KeyUp +=new KeyEventHandler(DoubleClickDataGrid_KeyUp);
            this.KeyDown += new KeyEventHandler(DoubleClickDataGrid_KeyDown);
            #endregion 
            #region == 셀 선택기능 ==
            this.AddHandler(FrameworkElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(DoubleClickDataGrid_MouseLeftButtonDown), true);
            base.MouseLeftButtonUp += new MouseButtonEventHandler(DoubleClickDataGrid_MouseLeftButtonUp);
            base.MouseMove += new MouseEventHandler(DoubleClickDataGrid_MouseMove);
            selectAction = new SelectAction(this);
            #endregion
        }
        #region == 셀 선택기능 ==
        private void DoubleClickDataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.SelectionMode != System.Windows.Controls.DataGridSelectionMode.Extended) return;

            ModifierKeys keys = Keyboard.Modifiers;

            bool shiftKey = (keys & ModifierKeys.Shift) != 0; 
            bool altKey = (keys & ModifierKeys.Alt) != 0; 
            bool appleKey = (keys & ModifierKeys.Apple) != 0; 
            bool controlKey = (keys & ModifierKeys.Control) != 0; 
            bool windowsKey = (keys & ModifierKeys.Windows) != 0;


            DataGridCell CellUnderMouse = this.GetDataGridCellByPosition(e.GetPosition(null));
            if(CellUnderMouse != null)   {
                if (shiftKey == true)
                {   //simple select
                    selectAction.selectionShiftClick.ShiftClickHere(e.GetPosition(null));
                }
                else
                {
                    selectAction.selectionShiftClick.ClickHere(e.GetPosition(null));
                    //draging
                    //cellSelection.UnSelectCells();
                    selectAction.selectionDrag.PrepareForCellDragging(e.GetPosition(null));
                    selectAction.selectionDrag.TryBeginDragging(e.GetPosition(null));
                }
            }
            base.OnMouseLeftButtonDown(e);
        }

        private void DoubleClickDataGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.SelectionMode != System.Windows.Controls.DataGridSelectionMode.Extended) return;

            #region == 셀 선택기능 ==
            selectAction.selectionDrag.ProcessMouseMove(e.GetPosition(null));
            #endregion
            base.OnMouseMove(e);
        }

        private void DoubleClickDataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.SelectionMode != System.Windows.Controls.DataGridSelectionMode.Extended) return;

            #region == 셀 선택기능 ==
            selectAction.selectionDrag.End();
            #endregion
            base.OnMouseLeftButtonUp(e);
        }
        #endregion

        #region == Copy & Paste 추가 ==
        //void DoubleClickDataGrid_KeyUp(object sender, KeyEventArgs e)
        //{
        //    //MessageBox.Show(e.Key.ToString());
        //    //check for the specific 'v' key, then check modifiers 
        //    if (e.Key == Key.C && ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
        //    {
        //        CopyCells();
        //    } 
        //    else if (e.Key == Key.V && ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
        //    {
        //        if (AllowPaste == true)
        //        {
        //            PasteCells();
        //        }
        //    } // else ignore the keystroke 
        //    else
        //    {
        //        base.OnKeyDown(e);
        //    }
        //}
        void DoubleClickDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.Key.ToString());
            //check for the specific 'v' key, then check modifiers 
            if (e.Key == Key.C && ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
            {
                CopyCells();
            }
            else if (e.Key == Key.V && ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
            {
                if (AllowPaste == true)
                {
                    PasteCells();
                }
            } // else ignore the keystroke 
            else
            {
                base.OnKeyDown(e);
            }
        }

        protected void CopyCells()
        {
            //MessageBox.Show("Ctrl + c : " + Clipboard.GetText());
            if (_LastDataGridCell == null || Clipboard.ContainsText() == false) return;

            ICollection<DataGridCell> cells = this.cellSelection.GetSelectedCells();
            StringBuilder sb = new StringBuilder();

            for(int i = 0 ; i < cells.Count ; i++) 
            {
                string value = cells.ElementAt(i).GetCellContent<TextBlock>().Text;
                if(i > 0) {
                    
                    if (cells.ElementAt(i).DataContext.Equals(cells.ElementAt(i - 1).DataContext))
                    {
                        //같은 행일 경우
                        sb.Append("\t");
                    }
                    else
                    {
                        //다른 행일 경우
                        sb.Append(ClipboardHelper.CurrentPlatformNewLine);
                    }

                }
                
                sb.Append(value);
                
            }

            //MessageBox.Show("value=" + sb.ToString());
            Clipboard.SetText(sb.ToString());
        }

        private Brush DirtyColor = new SolidColorBrush(Infragistics.ColorConverter.FromString("#FFA500"));
        protected void PasteCells()
        {
            //MessageBox.Show("Ctrl + v");
            if (IsPasteEnable == false || _LastDataGridCell == null || Clipboard.ContainsText() == false) return;

            MessageBox.Show("Clipboard.GetText()=" + Clipboard.GetText());
            ICollection<DataGridCell> selectedCells = cellSelection.GetSelectedCells();

            if(selectedCells.Count == 0) return;

            if (selectedCells.Count == 1)
            {
                //선택한 셀이 하나일경우 - 클립보드 기준으로 붙여넣기

                DataGridCell firstCell = selectedCells.First();
                DataGridRow firstRow = DataGridRow.GetRowContainingElement(firstCell);
                DataGridColumn firstCol = DataGridColumn.GetColumnContainingElement(firstCell);

                //MessageBox.Show(" currentCell's parent : " + VisualTreeHelper.GetParent(currentCell).ToString());
                //DataGridCellsPresenter p = VisualTreeHelper.GetParent(currentCell) as DataGridCellsPresenter;
                DataGridRowsPresenter rowPresenter = VisualTreeHelper.GetParent(firstRow) as DataGridRowsPresenter;
                IEnumerable<DataGridRow> rows = rowPresenter.Children.OfType<DataGridRow>();
                
                //MessageBox.Show(" row's parent : " + VisualTreeHelper.GetParent(row).ToString());
                ////MessageBox.Show(" col's parent : " + VisualTreeHelper.GetParent(col).ToString());   //에러
                int ColumnCnt = this.Columns.Count;
                //int RowCnt = this.ItemsSource.Cast<object>().Count();
                int RowCnt = rows.Count();
                DataGridCell currentCell = firstCell;
                DataGridRow currentRow = firstRow;
                DataGridColumn currentCol = firstCol;
                object dataModel = firstCell.DataContext;
                List<List<string>> PasteDatas = ClipboardHelper.ParseStringToTabSeparatedValues(Clipboard.GetText());
                foreach (IList<string> row in PasteDatas)
                {
                    foreach (string value in row)
                    {
                        if (AfterPaste != null)
                            AfterPaste(this, new PasteArgs(_LastDataGridRow, _LastDataGridColumn, _LastDataGridCell, _LastObject, Utils.CommonUtil.GetValue(this.SelectedItem, currentCol.SortMemberPath), Clipboard.GetText()));

                        currentCell.GetCellContent<TextBlock>().Text = value;
                        currentCell.Background = DirtyColor;

                        if (BeforePaste != null)
                            BeforePaste(this, new PasteArgs(_LastDataGridRow, _LastDataGridColumn, _LastDataGridCell, _LastObject, Utils.CommonUtil.GetValue(this.SelectedItem, currentCol.SortMemberPath), Clipboard.GetText()));

                        //------------- 다음 셀(열)로 넘어감 ----------------
                        int nextColumnIndex = currentCol.DisplayIndex + 1;
                        if (ColumnCnt > nextColumnIndex)
                        {
                            currentCol = this.Columns[nextColumnIndex];
                            currentCell = currentCol.GetCellContent(currentRow).Parent as DataGridCell;;
                        }
                        else
                        {
                            //다음열이 없는 경우 다음줄로 넘어감
                            break;
                        }
                        //--------------------------------------------------
                    }

                    IEnumerable d = this.ItemsSource;
                    //------------- 다음 셀(열)로 넘어감 ----------------
                    int nextRowIndex = currentRow.GetIndex() + 1;
                    if (RowCnt > nextRowIndex)
                    {
                        currentRow = rows.ElementAt(nextRowIndex);
                        currentCol = firstCol;
                        currentCell = currentCol.GetCellContent(currentRow).Parent as DataGridCell; ;
                    }
                    else
                    {
                        //다음줄이 없는 경우 끝냄
                        break;
                    }
                    //--------------------------------------------------
                }
            }
            else
            {
                //선택한 셀이 복수일경우 - 선택한 셀 중심으로 붙여넣기
                //DataGridCell firstCell = selectedCells.First();
                List<List<string>> PasteDatas = ClipboardHelper.ParseStringToTabSeparatedValues(Clipboard.GetText());
                int rowIndex = 0;
                int colIndex = 0;
                int rowCnt = PasteDatas.Count();
                int colCnt = PasteDatas[0].Count;
                DataGridCell previousCell = null;
                foreach (DataGridCell cell in selectedCells)
                {
                    DataGridColumn currentCol = DataGridColumn.GetColumnContainingElement(cell);
                    string value = PasteDatas[rowIndex][colIndex];

                    if (AfterPaste != null)
                        AfterPaste(this, new PasteArgs(_LastDataGridRow, _LastDataGridColumn, _LastDataGridCell, _LastObject, Utils.CommonUtil.GetValue(this.SelectedItem, currentCol.SortMemberPath), Clipboard.GetText()));

                    cell.GetCellContent<TextBlock>().Text = value;
                    cell.Background = DirtyColor;

                    if (BeforePaste != null)
                        BeforePaste(this, new PasteArgs(_LastDataGridRow, _LastDataGridColumn, _LastDataGridCell, _LastObject, Utils.CommonUtil.GetValue(this.SelectedItem, currentCol.SortMemberPath), Clipboard.GetText()));

                    if (previousCell != null)
                    {
                        if (cell.DataContext.Equals(previousCell.DataContext))
                        {
                            //이전 셀이랑 같은 Row 일 경우
                            colIndex += 1; if (colIndex >= colCnt) colIndex = 0;
                        }
                        else
                        {
                            //이전 셀이랑 다른 Row 일 경우
                            rowIndex += 1; if (rowIndex >= rowCnt) rowIndex = 0;
                            colIndex = 0;
                        }
                    }
                    previousCell = cell;
                }

            }

            
            //this.CurrentColumn.Header
            //Dispatcher.BeginInvoke(() => MessageBox.Show(this.CurrentColumn.Header.ToString()));    //00:10
            
            //MessageBox.Show(_LastObject.ToString());
            //DataGridTextColumn col = this.CurrentColumn as DataGridTextColumn;
            //if (col == null) MessageBox.Show("coll is null");
            //MessageBox.Show(col.Binding.Path.Path);

            //MessageBox.Show("orig data:" + Utils.CommonUtil.GetValue(this.SelectedItem, col.Binding.Path.Path).ToString());

            //값변경
            //((TextBlock)this.CurrentColumn.GetCellContent(this.SelectedItem)).Text = Clipboard.GetText();   //셀
            //Utils.CommonUtil.SetValue(this.SelectedItem, col.Binding.Path.Path, Clipboard.GetText());       //데이터
            //색변경


            //Dispatcher.BeginInvoke(() => MessageBox.Show(ClipBoardText));
        }
        public IEnumerable<DataGridCell> getDirtyCells()
        {
            IEnumerable<DataGridCell> dirtys = from a in this.GetCells()
                                               where DirtyColor.Equals(((Control)a).Background) 
                                               select a;

            //MessageBox.Show("dirtys" + dirtys.Count().ToString());
             //UIElement element = this as UIElement;
            return dirtys;
        }

        protected IEnumerable<object> GetChild()
        {
            int cnt = VisualTreeHelper.GetChildrenCount(this);
            for (int i = 0; i < cnt; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(this, i);
                yield return child;
            } 
        }
        #endregion

        protected void OnRowClicked()
        {
            if (RowClicked != null)
            {
                RowClicked(this, new DataGridRowClickedArgs
          (_LastDataGridRow, _LastDataGridColumn, _LastDataGridCell, _LastObject));
            }
        }

        protected void OnRowDoubleClicked()
        {
            if (RowDoubleClicked != null)
            {
                RowDoubleClicked(this, new DataGridRowClickedArgs
          (_LastDataGridRow, _LastDataGridColumn, _LastDataGridCell, _LastObject));
            }
        }

        protected void OnCellClicked()
        {
            if (CellClicked != null)
            {
                CellClicked(this, new DataGridCellClickedArgs
          (_LastDataGridRow, _LastDataGridColumn, _LastDataGridCell, _LastObject));
            }
        }

        protected void OnCellDoubleClicked()
        {
            if (CellDoubleClicked != null)
            {
                CellDoubleClicked(this, new DataGridCellClickedArgs
          (_LastDataGridRow, _LastDataGridColumn, _LastDataGridCell, _LastObject));
            }
        }

        private void DoubleClickDataGridClick(object sender, MouseButtonEventArgs e)
        {
            DateTime clickTime = DateTime.Now;
            DataGridRow currentRowClicked;
            DataGridColumn currentColumnClicked;
            DataGridCell currentCellClicked;
            object currentObject;



            //If we've found at least the row,
            if (GetDataGridCellByPosition(e.GetPosition(null), out currentRowClicked, out currentColumnClicked, out currentCellClicked, out currentObject))
            {
                if (currentRowClicked == _LastDataGridRow && currentCellClicked == _LastDataGridCell)
                {
                    bool isCellDoubleClick = clickTime.Subtract(_LastClick) <= TimeSpan.FromMilliseconds(_DoubleClickTime);
                    if (isCellDoubleClick) OnCellDoubleClicked();
                    else OnCellClicked();
                }
                else if (currentRowClicked == _LastDataGridRow)
                {
                    bool isDoubleClick = clickTime.Subtract(_LastClick) <= TimeSpan.FromMilliseconds(_DoubleClickTime);
                
                    if (isDoubleClick)
                    {
                        OnRowDoubleClicked();
                    }
                    else
                    {
                        OnRowClicked();
                    }
                }
                //MessageBox.Show(isDoubleClick + " : " + _DoubleClickTime + "=>" + clickTime + ":" + _LastClick + " ==> " + clickTime.Subtract(_LastClick) + ":" + TimeSpan.FromMilliseconds(_DoubleClickTime));
                _LastDataGridRow = currentRowClicked;
                _LastDataGridColumn = currentColumnClicked;
                _LastDataGridCell = currentCellClicked;
                _LastObject = currentObject;

                
            }
            else
            {
                _LastDataGridRow = null;
                _LastDataGridCell = null;
                _LastDataGridColumn = null;
                _LastObject = null;
            }

            _LastClick = clickTime;
        }

        private bool GetDataGridCellByPosition(Point pt, out DataGridRow dataGridRow, out DataGridColumn dataGridColumn, out DataGridCell dataGridCell, out object dataGridObject)
        {
            var elements = VisualTreeHelper.FindElementsInHostCoordinates(pt, this);
            dataGridRow = null;
            dataGridCell = null;
            dataGridColumn = null;
            dataGridObject = null;

            if (null == elements || elements.Count() == 0)
            {
                return false;
            }

            var rowQuery = from gridRow in elements
                           where gridRow
                               is DataGridRow
                           select gridRow as DataGridRow;
            dataGridRow = rowQuery.FirstOrDefault();
            if (dataGridRow == null)
            {
                return false;
            }

            dataGridObject = dataGridRow.DataContext;

            var cellQuery = from gridCell in elements
                            where gridCell is DataGridCell
                            select gridCell as DataGridCell;
            dataGridCell = cellQuery.FirstOrDefault();

            if (dataGridCell != null)
            {
                dataGridColumn = DataGridColumn.GetColumnContainingElement(dataGridCell);
            }

            //If we've got the row, return true - 
            //sometimes the Column, DataContext could be null
            return dataGridRow != null;
        }


    }

    public class DataGridRowClickedArgs
    {
        public DataGridRow DataGridRow { get; set; }
        public DataGridColumn DataGridColumn { get; set; }
        public DataGridCell DataGridCell { get; set; }
        public object DataGridRowItem { get; set; }

        public DataGridRowClickedArgs(DataGridRow dataGridRow, DataGridColumn dataGridColumn, DataGridCell dataGridCell, object dataGridRowItem)
        {
            DataGridRow = dataGridRow;
            DataGridColumn = dataGridColumn;
            DataGridCell = dataGridCell;
            DataGridRowItem = dataGridRowItem;
        }
    }

    public class DataGridCellClickedArgs
    {
        public DataGridRow DataGridRow { get; set; }
        public DataGridColumn DataGridColumn { get; set; }
        public DataGridCell DataGridCell { get; set; }
        public object DataGridRowItem { get; set; }

        public DataGridCellClickedArgs(DataGridRow dataGridRow, DataGridColumn dataGridColumn, DataGridCell dataGridCell, object dataGridRowItem)
        {
            DataGridRow = dataGridRow;
            DataGridColumn = dataGridColumn;
            DataGridCell = dataGridCell;
            DataGridRowItem = dataGridRowItem;
        }
    }

    #region == Copy & Paste 추가 ==
    public class PasteArgs
    {
        public DataGridRow DataGridRow { get; set; }
        public DataGridColumn DataGridColumn { get; set; }
        public DataGridCell DataGridCell { get; set; }
        public object DataGridRowItem { get; set; }
        public object BeforeValue { get; set; }
        public object AfterValue { get; set; }

        public PasteArgs(DataGridRow dataGridRow, DataGridColumn dataGridColumn, DataGridCell dataGridCell, object dataGridRowItem, object beforeValue, object afterValue)
        {
            DataGridRow = dataGridRow;
            DataGridColumn = dataGridColumn;
            DataGridCell = dataGridCell;
            DataGridRowItem = dataGridRowItem;
            BeforeValue = beforeValue;
            AfterValue = afterValue;
        }
    }
    #endregion

    #region == Extensions 전체 행가져오기, 전체 셀 가져오기, 전체 셀 내용 가져오기, 부분셀가져오기 등등등 ==
    /// <summary> 
    /// Extends the DataGrid. 
    /// </summary> 
    public static class DataGridExtensions
    {
        #region == DataGrid 용 ==
        /// <summary> 
        /// Gets the list of DataGridRow objects. 
        /// </summary> 
        /// <param name="grid">The grid wirhrows.</param> 
        /// <returns>List of rows of the grid.</returns> 
        public static ICollection<DataGridRow> GetRows(this DataGrid grid)
        {
            //DateTime start = DateTime.Now;
            List<DataGridRow> rows = new List<DataGridRow>();

            foreach (var rowItem in grid.ItemsSource)
            {
                // Ensures that all rows are loaded. 
                grid.ScrollIntoView(rowItem, grid.Columns.Last());

                // Get the content of the cell. 
                FrameworkElement el = grid.Columns.Last().GetCellContent(rowItem);

                // Retrieve the row which is parent of given element. 
                DataGridRow row = DataGridRow.GetRowContainingElement(el.Parent as FrameworkElement);

                // Sometimes some rows for some reason can be null. 
                if (row != null)
                    rows.Add(row);
            }

            //DateTime end = DateTime.Now;
            //double diff = end.Subtract(start).TotalMilliseconds;
            //MessageBox.Show("diff:" + diff.ToString());

            return rows;
        }

        /// <summary>
        /// 검증 X
        /// </summary>
        /// <param name="itemsControl"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DataGridRow GetRow(this DataGrid itemsControl, int index)
        {
            var rowsPresenter = itemsControl.GetVisualDescendants().OfType<DataGridRowsPresenter>().FirstOrDefault();
            if (rowsPresenter != null)
            {
                return rowsPresenter.Children.OfType<DataGridRow>()
                        .Where(row => row.GetIndex() == index).SingleOrDefault();
            }
            return null;
        } 



        #region == Copy & Paste 추가 ==
        /// <summary> 
        /// 아 귀찮
        /// </summary> 
        /// <param name="grid"></param> 
        /// <returns></returns> 
        public static ICollection<FrameworkElement> GetCellContents(this DataGrid grid)
        {
            IList<FrameworkElement> cells = new List<FrameworkElement>();

            foreach (var rowItem in grid.ItemsSource)
            {
                // Ensures that all rows are loaded. 
                //grid.ScrollIntoView(rowItem, grid.Columns.Last());

                foreach (DataGridColumn c in grid.Columns)
                {
                    FrameworkElement el = c.GetCellContent(rowItem) as FrameworkElement;



                    if (el != null)
                        cells.Add(el);
                }
            }

            return cells;
        }


        /// <summary> 
        /// 아 귀찮
        /// </summary> 
        /// <param name="grid"></param> 
        /// <returns></returns> 
        public static ICollection<DataGridCell> GetCells(this DataGrid grid)
        {
            //DateTime start = DateTime.Now;
            IList<DataGridCell> cells = new List<DataGridCell>();

            foreach (var rowItem in grid.ItemsSource)
            {
                // Ensures that all rows are loaded. 
                //grid.ScrollIntoView(rowItem, grid.Columns.Last());

                foreach (DataGridColumn c in grid.Columns)
                {
                    FrameworkElement el = c.GetCellContent(rowItem) as FrameworkElement;

                    if (el == null) continue;

                    DataGridCell elp = el.Parent as DataGridCell;

                    if (elp != null)
                        cells.Add(elp);
                }
            }

            //DateTime end = DateTime.Now;
            //double diff = end.Subtract(start).TotalMilliseconds;
            //MessageBox.Show("diff:" + diff.ToString());

            return cells;
        }


        #region GetDataGridCellByPosition
        public static DataGridCell GetDataGridCellByPosition(this DataGrid grid, Point pt)
        {
            if (grid.ItemsSource == null) return null;

            var elements = VisualTreeHelper.FindElementsInHostCoordinates(pt, grid);

            if (null == elements || elements.Count() == 0)
            {
                return null;
            }

            var cellQuery = from gridCell in elements
                            where gridCell is DataGridCell
                            select gridCell as DataGridCell;

            return cellQuery.FirstOrDefault();
        }
        #endregion

        #region GetDataGridCellsByPosition
        public static IEnumerable<DataGridCell> GetDataGridCellsByPosition(this DataGrid grid, Rect rt)
        {
            var elements = VisualTreeHelper.FindElementsInHostCoordinates(rt, grid);

            if (null == elements || elements.Count() == 0)
            {
                return null;
            }

            var cellQuery = from gridCell in elements
                            where gridCell is DataGridCell
                            select gridCell as DataGridCell;

            return cellQuery;
        }
        #endregion

        public static DataGridCell GetDataGridCellByIndex(this DataGrid grid, int xIndex, int yIndex)
        {
            if (grid.ItemsSource == null) return null;

            IEnumerator et =  grid.ItemsSource.GetEnumerator();
            for (int i = 0; i < yIndex; yIndex++)
            {
                et.MoveNext();
            }

            FrameworkElement el = grid.Columns[xIndex].GetCellContent(et.Current);

            return (DataGridCell)el.Parent;
        }

        public static IList<DataGridCell> GetDataGridCellsByIndex(this DataGrid grid, int cell1_ColumnIndex, int cell1_RowIndex, int cell2_ColumnIndex, int cell2_RowIndex)
        {
            int xStart;
            int xEnd;
            int yStart;
            int yEnd;

            if (cell1_ColumnIndex < cell2_ColumnIndex)
            {
                xStart = cell1_ColumnIndex;
                xEnd = cell2_ColumnIndex;
            }
            else
            {
                xStart = cell2_ColumnIndex;
                xEnd = cell1_ColumnIndex;
            }

            if (cell1_RowIndex < cell2_RowIndex)
            {
                yStart = cell1_RowIndex;
                yEnd = cell2_RowIndex;
            }
            else
            {
                yStart = cell2_RowIndex;
                yEnd = cell1_RowIndex;
            }

            IList<DataGridCell> SelectedCells = new List<DataGridCell>();

            //관련 columns 구하기
            IList<DataGridColumn> columns = new List<DataGridColumn>();
            for (int i = xStart; i <= xEnd; i++)
            {
                columns.Add(grid.Columns[i]);
            }

            //관련 rows 구하기
            IEnumerable<DataGridRow> rows = null;
            var rowsPresenter = grid.GetVisualDescendants().OfType<DataGridRowsPresenter>().FirstOrDefault();
            if (rowsPresenter != null)
            {
                rows = rowsPresenter.Children.OfType<DataGridRow>()
                            .Where(row => row.GetIndex() >= yStart && row.GetIndex() <= yEnd);
            }

            //교차 cells 구하기
            foreach (DataGridRow row in rows)
            {
                foreach (DataGridColumn column in columns)
                {
                    var cell = column.GetCellContent(row);

                    SelectedCells.Add((DataGridCell)cell.Parent);
                }
            }

            return SelectedCells;

        }

        //public static IList<DataGridCell> GetDataGridCellsByIndex(this DataGrid grid, DataGridCell cell1, DataGridCell cell2)
        //{
        //    DataGridCell cellStart;
        //    DataGridCell cellEnd;

        //    int cell1ColumnIndex = DataGridColumn.GetColumnContainingElement(cell1).DisplayIndex;
        //    int cell2ColumnIndex = DataGridColumn.GetColumnContainingElement(cell2).DisplayIndex;

        //    if (cell1ColumnIndex < cell2ColumnIndex)
        //    {
        //        cellStart = cell1;
        //        cellEnd = cell2;
        //    }
        //    else
        //    {
        //        cellStart = cell2;
        //        cellEnd = cell1;
        //    }

        //    //DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
        //    int cell1RowIndex = DataGridRow.GetRowContainingElement(cell1).GetIndex();
        //    int cell2RowIndex = DataGridRow.GetRowContainingElement(cell2).GetIndex();

            
        //    DataGridColumn.GetColumnContainingElement(cellStart).GetCellContent(
            
        //    if (x1Index < x2Index)
        //    {
        //        xStart = x1Index;
        //        xEnd = x2Index;
        //    }
        //    else
        //    {
        //        xStart = x2Index;
        //        xEnd = x1Index;
        //    }

        //    if (y1Index < y2Index)
        //    {
        //        yStart = y1Index;
        //        yEnd = y2Index;
        //    }
        //    else
        //    {
        //        yStart = y2Index;
        //        yEnd = y1Index;
        //    }




        //}

        #endregion
        #endregion

        public static IList<DataGridCell> DDD(this DataGrid grid)
        {
            

            return null;
        }
        #region == DataGridCell 용 ==
        public static T GetCellContent<T>(this DataGridCell cell)
        {
            return cell.GetVisualDescendants().OfType<T>().FirstOrDefault<T>();
        }

        //public static DataGridCell GetRightCell(this DataGridCell cell)
        //{
        //    DataGridRow row = DataGridRow.GetRowContainingElement(cell);
        //    DataGridColumn col = DataGridColumn.GetColumnContainingElement(cell);

            
        //    ////col.DisplayIndex


        //    //var cellQuery = from gridCell in elements
        //    //                where gridCell is DataGridCell
        //    //                select gridCell as DataGridCell;

        //    return null; 
        //}
        #endregion
    }
    #endregion

    #region == 선택된 셀들을 가져옴 & 프로그램상에서 셀들을 선택 ==
    public class CellSelection
    {
        private DataGrid dataGrid;
        public ICollection<DataGridCell> SelectedCellCollection
        {
            get
            {
                return GetSelectedCells();
            }
            set
            {
                SelectCells(value);
            }
        }

        public CellSelection(DataGrid dataGrid)
        {
            this.dataGrid = dataGrid;
        }

        #region == 선택된 셀 가져오기 ==
        /// <summary> 
        /// 아 귀찮
        /// </summary> 
        /// <param name="grid"></param> 
        /// <returns></returns> 
        public ICollection<DataGridCell> GetSelectedCells()
        {
            IList<DataGridCell> SelectedCells = new List<DataGridCell>();
            IList<DataGridCell> Cells = dataGrid.GetCells() as IList<DataGridCell>;

            foreach (DataGridCell cell in Cells)
            {
                DataGridColumn col = DataGridColumn.GetColumnContainingElement(cell) as DataGridColumn;
                string bindName = col.SortMemberPath;
                object dataModel = cell.DataContext;
                object value = CommonUtil.GetValue(dataModel, bindName + "_SELECTEDCOLOR");
                if (value != null)
                {
                    SelectedCells.Add(cell);
                }

                //if (VisualTreeHelper.GetChildrenCount(cell) != 0)
                //{
                //    FrameworkElement grid = (FrameworkElement)VisualTreeHelper.GetChild(cell, 0);

                //    IList groups = VisualStateManager.GetVisualStateGroups(grid);
                //    foreach (VisualStateGroup group in groups)
                //    {
                //        if ("CustomSelection".Equals(group.Name) == true && group.CurrentState != null && "CustomSelected".Equals(group.CurrentState.Name) == true)
                //        {
                //            SelectedCells.Add(cell);
                //        }
                //    }
                //}
            }

            return SelectedCells;
        }
        #endregion

        #region == 셀선택하기 ==
        public void SelectCells(IEnumerable<DataGridCell> Cells)
        {
            if (Cells == null)
            {
                UnSelectCells();
                return;
            }

            foreach (DataGridCell cell in Cells)
            {
                SelectCell(cell);
            }
        }

        public void SelectCell(DataGridCell cell)
        {
            try
            {
                DataGridColumn col = DataGridColumn.GetColumnContainingElement(cell) as DataGridColumn;
                string bindName = col.SortMemberPath;
                //int startIndex = bindName.LastIndexOf('_');
                //int length = bindName.Length - (startIndex + 1);
                //string number = bindName.Substring(startIndex + 1, length);


                object dataModel = cell.DataContext;
                CommonUtil.SetValue(dataModel, bindName + "_SELECTEDCOLOR", "#FF6DBDD1");

                //VisualStateManager.GoToState(cell, "CustomSelected", false);
                //cell.Tag = new SolidColorBrush(Colors.Red);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        public void SetCells(DataGridCell from, DataGridCell to)
        {
            if (from == null || to == null) return;




            //foreach (DataGridCell cell in Cells)
            //{
            //    VisualStateManager.GoToState(cell, "CustomSelection_Selected", false);
            //}
        }
        #endregion

        #region == 셀 해제 하기 ==
        public void UnSelectCell(DataGridCell cell)
        {
            try
            {
                DataGridColumn col = DataGridColumn.GetColumnContainingElement(cell) as DataGridColumn;
                string bindName = col.SortMemberPath;
                //int startIndex = bindName.LastIndexOf('_');
                //int length = bindName.Length - (startIndex + 1);
                //string number = bindName.Substring(startIndex + 1, length);


                object dataModel = cell.DataContext;
                CommonUtil.SetValue(dataModel, bindName + "_SELECTEDCOLOR", null);

                //VisualStateManager.GoToState(cell, "CustomSelected", false);
                //cell.Tag = new SolidColorBrush(Colors.Red);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        public void UnSelectCells(IEnumerable<DataGridCell> Cells)
        {
            if (Cells == null)
            {
                return;
            }

            foreach (DataGridCell cell in Cells) 
            {
                UnSelectCell(cell);
            }
        }

        /// <summary>
        /// CellSelection.SelectedCellCollection = null;
        /// </summary>
        public void UnSelectCells()
        {
            ICollection<DataGridCell> SelectedCells = GetSelectedCells();

            foreach (DataGridCell cell in SelectedCells)
            {
                try
                {
                    DataGridColumn col = DataGridColumn.GetColumnContainingElement(cell) as DataGridColumn;
                    string bindName = col.SortMemberPath;
                    //int startIndex = bindName.LastIndexOf('_');
                    //int length = bindName.Length - (startIndex + 1);
                    //string number = bindName.Substring(startIndex + 1, length);


                    object dataModel = cell.DataContext;
                    CommonUtil.SetValue(dataModel, bindName + "_SELECTEDCOLOR", null);

                    //VisualStateManager.GoToState(cell, "CustomSelected", false);
                    //cell.Tag = new SolidColorBrush(Colors.Red);
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.Message);
                }
            }
        }
        #endregion
    }
    #endregion


    public class SelectAction
    {
        public SelectionDrag selectionDrag = null;
        public SelectionShiftClick selectionShiftClick = null;
        private static IEnumerable<DataGridCell> previousCells; 

        public SelectAction(DoubleClickDataGrid dataControl)
        {
            selectionDrag = new SelectionDrag(dataControl);
            selectionShiftClick = new SelectionShiftClick(dataControl);
        }

        internal enum SelectionDragUnit
        {
            None,
            Row,
            Cell
        }

        #region == SelectionShiftClick ==
        public class SelectionShiftClick
        {
            private DataGridCell FirstDataGridCell { get; set; }
            private DataGridCell LastDataGridCell { get; set; }
            private int FirstDataGridCell_ColumnIndex { get; set; }
            private int FirstDataGridCell_RowIndex { get; set; }
            private int LastDataGridCell_ColumnIndex { get; set; }
            private int LastDataGridCell_RowIndex { get; set; }

            private DoubleClickDataGrid dataControl;
            //private IEnumerable<DataGridCell> previousCells;

            public SelectionShiftClick(DoubleClickDataGrid dataControl)
            {
                this.dataControl = dataControl;
            }

            public void ClickHere(Point p)
            {
                //기존셀은 클리어
                if (previousCells != null)
                {
                    dataControl.cellSelection.UnSelectCells(previousCells);
                }

                var cell = dataControl.GetDataGridCellByPosition(p);
                FirstDataGridCell_ColumnIndex = DataGridColumn.GetColumnContainingElement(cell).DisplayIndex;
                FirstDataGridCell_RowIndex = DataGridRow.GetRowContainingElement(cell).GetIndex();
                dataControl.cellSelection.SelectCell(cell);

                IList<DataGridCell> temp = new List<DataGridCell>();
                temp.Add(cell);
                previousCells = temp;
                FirstDataGridCell = cell;
            }


            public void ShiftClickHere(Point p)
            {
                if (FirstDataGridCell == null)
                {
                    ClickHere(p);
                    return;
                }

                //기존셀은 클리어
                if (previousCells != null)
                {
                    dataControl.cellSelection.UnSelectCells(previousCells);
                }

                var cell = dataControl.GetDataGridCellByPosition(p);
                LastDataGridCell_ColumnIndex = DataGridColumn.GetColumnContainingElement(cell).DisplayIndex;
                LastDataGridCell_RowIndex = DataGridRow.GetRowContainingElement(cell).GetIndex();
                LastDataGridCell = cell;

                var cells = dataControl.GetDataGridCellsByIndex(FirstDataGridCell_ColumnIndex, FirstDataGridCell_RowIndex, LastDataGridCell_ColumnIndex, LastDataGridCell_RowIndex);
                dataControl.cellSelection.SelectCells(cells);

                previousCells = cells;

            }
        }
        #endregion


        #region == SelectionDrag ==
        public class SelectionDrag
        {
            private static Point InvalidPosition = new Point(double.NegativeInfinity, double.NegativeInfinity);
            private static readonly Point ZERO = new Point();

            private DoubleClickDataGrid dataControl;
            private SelectionDragUnit dragUnit;
            private Point lastMousePosition;

            private DataGridCell LastDataGridCell;
            
            //private Pair<int, int> lastSelectedElementIndexes;
            private Point scrollChange;

            public bool IsDragging
            {
                get;
                private set;

            }

            //internal IMouseInfoProvider MouseInfoProvider
            //{
            //    get;
            //    set;
            //}

            //internal ISelectionDispatcher SelectionDispatcher
            //{
            //    get;
            //    set;
            //}

            //internal ScrollViewerCoordinator ScrollCoordinator
            //{
            //    get;
            //    set;
            //}
            #region == 생성자 ==
            public SelectionDrag(DoubleClickDataGrid dataControl)
            {
                this.dataControl = dataControl;
                this.InvalidateMousePosition();
                //this.MouseInfoProvider = new DefaultMouseInfoProvider(this.dataControl);
                //this.SelectionDispatcher = this.dataControl.SelectionDispatcher;

                //this.lastSelectedElementIndexes = new Pair<int, int>();
                //this.ScrollCoordinator = new ScrollViewerCoordinator();
                this.scrollChange = new Point();
            }
            #endregion

            #region == Drag 준비 ==
            private void InvalidateMousePosition()
            {
                this.lastMousePosition = InvalidPosition;
            }


            public void PrepareForRowDragging(Point initialMousePosition)
            {
                this.lastMousePosition = initialMousePosition;
                this.dragUnit = SelectionDragUnit.Row;
            }

            public void PrepareForCellDragging(Point initialMousePosition)
            {
                this.lastMousePosition = initialMousePosition;
                this.dragUnit = SelectionDragUnit.Cell;
            }
            #endregion

            #region == 진행  ==
            public void ProcessMouseMove(Point newMousePosition)
            {
                this.ProcessMouseMove(newMousePosition, false);
            }

            private void ProcessMouseMove(Point newMousePosition, bool scrollAdjusted)
            {
                //this.TryBeginDragging(newMousePosition);

                if (this.IsDragging)
                {
                    this.lastMousePosition = newMousePosition;

                    this.TrySelectElementUnderMouse(scrollAdjusted);

                    //if (this.ScrollCoordinator != null)
                    //{
                    //    this.ScrollCoordinator.UpdateCurrentPoint(newMousePosition);
                    //}
                }
            }


            private void TrySelectElementUnderMouse(bool scrollAdjusted)
            {
                if (!this.IsDragging)
                {
                    return;
                }

                //if (this.dragUnit == SelectionDragUnit.Row)
                //{
                //    //this.TrySelectRowUnderMouse(scrollAdjusted);
                //}
                //else 
                //if (this.dragUnit == SelectionDragUnit.Cell)
                //{
                this.TrySelectCellUnderMouse(scrollAdjusted);
                //}
            }

            //private void TrySelectRowUnderMouse(bool scrollAdjusted)
            //{
            //    var rowUnderMouse = this.MouseInfoProvider.GetRowUnderMouse(this.lastMousePosition);

            //    if ((rowUnderMouse == null || rowUnderMouse.IsSelected) && scrollAdjusted)
            //    {
            //        rowUnderMouse = this.PredictNextVisibleRowToSelect();
            //    }

            //    if (rowUnderMouse != null)
            //    {
            //        this.SelectionDispatcher.HandleSelectionForRowInput(rowUnderMouse, SelectionModificationOptions.ExtendOnly);

            //        this.lastSelectedElementIndexes.First = this.dataControl.Items.IndexOf(rowUnderMouse.Item);
            //    }

            //}
            private void TrySelectCellUnderMouse(bool scrollAdjusted)
            {
                DataGridCell cellUnderMouse = dataControl.GetDataGridCellByPosition(UserInteractionMonitor.Current.CurrentMousePosistion);// this.MouseInfoProvider.GetCellUnderMouse(this.lastMousePosition);

                //if ((cellUnderMouse == null /*|| cellUnderMouse.IsSelected*/) && scrollAdjusted)
                //{
                //    cellUnderMouse = this.PredictNextVisibleCellToSelect();
                //}

                if (cellUnderMouse != null)
                {
                    if (LastDataGridCell != cellUnderMouse)
                    {
                        LastDataGridCell = cellUnderMouse;

                        //기존셀은 클리어
                        if (previousCells != null)
                        {
                            dataControl.cellSelection.UnSelectCells(previousCells);
                        }

                        IEnumerable<DataGridCell> cells = dataControl.GetDataGridCellsByPosition(UserInteractionMonitor.Current.LastMouseDragingRect);
                        dataControl.cellSelection.SelectCells(cells);
                        previousCells = cells;
                    }



                    //DataGridCell c = dataControl.GetDataGridCellByPosition(UserInteractionMonitor.Current.CurrentMousePosistion) as DataGridCell;
                    //MessageBox.Show(VisualStateManager.GetVisualStateGroups(cellUnderMouse).Count.ToString()); // show 0
                    //MessageBox.Show(cellUnderMouse);
                    //cellUnderMouse.GetValue();

                    //VisualState
                    //VisualStateGroup


                    //VisualStateManager.GoToState(cellUnderMouse, "Selected", true); //not work
                    //VisualStateManager.GoToState(cellUnderMouse, "Selected", false); //not work
                    //Application.Current.RootVisual.Dispatcher.BeginInvoke(() => MessageBox.Show("Valid :" + VisualStateManager.GoToState(cellUnderMouse, "Valid", false).ToString())); // true
                    //Application.Current.RootVisual.Dispatcher.BeginInvoke(() => MessageBox.Show("Unselected :" + VisualStateManager.GoToState(cellUnderMouse, "Unselected", false).ToString()));
                    //Application.Current.RootVisual.Dispatcher.BeginInvoke(() => MessageBox.Show("Selected :" + VisualStateManager.GoToState(cellUnderMouse, "Selected", false).ToString()));
                    //Application.Current.RootVisual.Dispatcher.BeginInvoke(() => MessageBox.Show("Editing :" + VisualStateManager.GoToState(cellUnderMouse, "Editing", false).ToString()));
                    //Application.Current.RootVisual.Dispatcher.BeginInvoke(() => MessageBox.Show("Current :" + VisualStateManager.GoToState(cellUnderMouse, "Current", false).ToString())); //true
                    //Application.Current.RootVisual.Dispatcher.BeginInvoke(() => MessageBox.Show("Invalid :" + VisualStateManager.GoToState(cellUnderMouse, "Invalid", false).ToString()));  //true
                    //Application.Current.RootVisual.Dispatcher.BeginInvoke(() => MessageBox.Show("Normal :" + VisualStateManager.GoToState(cellUnderMouse, "Normal", false).ToString()));
                    //Application.Current.RootVisual.Dispatcher.BeginInvoke(() => MessageBox.Show("MouseOver :" + VisualStateManager.GoToState(cellUnderMouse, "MouseOver", false).ToString()));
                    //Application.Current.RootVisual.Dispatcher.BeginInvoke(() => MessageBox.Show("Unfocused :" + VisualStateManager.GoToState(cellUnderMouse, "Unfocused", false).ToString()));
                    //Application.Current.RootVisual.Dispatcher.BeginInvoke(() => MessageBox.Show("Focused :" + VisualStateManager.GoToState(cellUnderMouse, "Focused", false).ToString()));
                    //Application.Current.RootVisual.Dispatcher.BeginInvoke(() => MessageBox.Show("Regular :" + VisualStateManager.GoToState(cellUnderMouse, "Regular", false).ToString()));  //true
                    //Application.Current.RootVisual.Dispatcher.BeginInvoke(() => MessageBox.Show("RightGridLine :" + VisualStateManager.GoToState(cellUnderMouse, "RightGridLine", false).ToString()));                
                    //Application.Current.RootVisual.Dispatcher.BeginInvoke(() => MessageBox.Show("Display :" + VisualStateManager.GoToState(cellUnderMouse, "Display", false).ToString()));


                    //this.SelectionDispatcher.HandleSelectionForCellInput(cellUnderMouse, SelectionModificationOptions.MinimallyModifyAndExtend);

                    //this.lastSelectedElementIndexes.First = this.dataControl.Items.IndexOf(cellUnderMouse.DataContext);
                    //this.lastSelectedElementIndexes.Second = cellUnderMouse.Column.DisplayIndex;

                }

            }

            //private KeyValuePair<int, int> PredictCellIndexesToSelect()
            //{
            //    var hasHorizontalChange = System.Math.Abs(this.scrollChange.X) > 1;
            //    var hasVerticalChange = System.Math.Abs(this.scrollChange.Y) > 1;

            //    if ((!hasHorizontalChange || !hasVerticalChange) && !this.IsDragging)
            //        return null;

            //    int nextCellIndexToSelect = GetNextCellIndexToSelect(hasHorizontalChange);
            //    int nextRowIndexToSelect = GetNextRowIndexToSelect(hasVerticalChange);

            //    return new KeyValuePair<int, int>(nextRowIndexToSelect, nextCellIndexToSelect);
            //}

            //private int GetNextRowIndexToSelect(bool hasVerticalChange)
            //{
            //    int nextRowIndexToSelect = this.lastSelectedElementIndexes.First;
            //    if (hasVerticalChange)
            //    {
            //        nextRowIndexToSelect = this.lastSelectedElementIndexes.First + (this.scrollChange.Y > 0 ? 1 : -1);
            //        if (nextRowIndexToSelect < 0)
            //            nextRowIndexToSelect = 0;
            //    }

            //    return nextRowIndexToSelect;
            //}

            //private int GetNextCellIndexToSelect(bool hasHorizontalChange)
            //{
            //    int nextCellIndexToSelect = this.lastSelectedElementIndexes.Second;

            //    if (hasHorizontalChange)
            //    {
            //        nextCellIndexToSelect = this.lastSelectedElementIndexes.Second + (this.scrollChange.X > 0 ? 1 : -1);
            //        if (nextCellIndexToSelect < 0)
            //            nextCellIndexToSelect = 0;
            //    }
            //    return nextCellIndexToSelect;
            //}

            //private DataGridCell PredictNextVisibleCellToSelect()
            //{
            //    var predictedIndexes = PredictCellIndexesToSelect();

            //    DataGridRow row = null;
            //    if (!this.dataControl.Items.IsGrouped)
            //    {
            //        row = this.dataControl.ItemContainerGenerator.ContainerFromIndex(predictedIndexes.First) as DataGridRow;
            //    }
            //    else if (predictedIndexes.First < this.dataControl.Items.Count)
            //    {
            //        var item = this.dataControl.Items[predictedIndexes.First];

            //        row = this.dataControl.GetRowForItem(item);
            //    }

            //    if (row != null && row.CellsPresenter != null && row.CellsPresenter.GridViewCellsPanel != null)
            //    {
            //        var cell = row.CellsPresenter.GridViewCellsPanel.CellFromIndex(predictedIndexes.Second) as GridViewCell;
            //        if (cell != null)
            //        {

            //            var scrollViewer = this.dataControl.InternalScrollHost as GridViewScrollViewer;
            //            var childTransform = cell.TransformToVisualSafe(scrollViewer);

            //            Rect rectangle = childTransform.TransformBounds(new Rect(ZERO, cell.RenderSize));
            //            var isCellInView = rectangle.IntersectsWithExclusive(new Rect(ZERO, scrollViewer.RenderSize));

            //            if (isCellInView)
            //            {
            //                return cell;
            //            }
            //        }
            //    }
            //    return null;
            //}

            //private DataGridRow PredictNextVisibleRowToSelect()
            //{
            //    var predictedIndexes = PredictCellIndexesToSelect();

            //    DataGridRow row = null;
            //    if (!this.dataControl.Items.IsGrouped)
            //    {
            //        row = this.dataControl.ItemContainerGenerator.ContainerFromIndex(predictedIndexes.First) as DataGridRow;
            //    }
            //    else if (predictedIndexes.First < this.dataControl.Items.Count)
            //    {
            //        var item = this.dataControl.Items[predictedIndexes.First];

            //        row = this.dataControl.GetRowForItem(item);
            //    }

            //    if (row != null)
            //    {
            //        var scrollViewer = this.dataControl.InternalScrollHost as GridViewScrollViewer;
            //        var childTransform = row.TransformToVisualSafe(scrollViewer);

            //        Rect rectangle = childTransform.TransformBounds(new Rect(ZERO, row.RenderSize));
            //        var isCellInView = rectangle.IntersectsWithExclusive(new Rect(ZERO, scrollViewer.RenderSize));

            //        if (isCellInView)
            //        {
            //            return row;
            //        }
            //    }

            //    return null;
            //}
            #endregion

            #region == 선택시작 ==
            public void TryBeginDragging(Point mousePosition)
            {
                if (this.CanBeginDragging(mousePosition))
                {
                    this.BeginDragging(mousePosition);

                    //var element = this.dataControl.GetDataGridCellByPosition(UserInteractionMonitor.Current.CurrentMousePosistion);//VisualTreeHelperExtensions.GetElementsInHostCoordinates<UIElement>(this.dataControl, mousePosition).FirstOrDefault();


                    //if (this.dataControl.InternalScrollContentPresenter != null)
                    //{
                    //    this.ScrollCoordinator.StartTracking(element, this.dataControl.InternalScrollContentPresenter, this.dataControl);
                    //    this.ScrollCoordinator.ScrollViewerAdjusted += new System.EventHandler<ScrollViewerAdjustedEventArgs>(ScrollCoordinator_ScrollViewerAdjusted);
                    //}
                }
            }

            //void ScrollCoordinator_ScrollViewerAdjusted(object sender, ScrollViewerAdjustedEventArgs e)
            //{
            //    this.scrollChange = e.Adjustment;
            //    this.ProcessMouseMove(this.lastMousePosition, true);

            //}

            private bool CanBeginDragging(Point mousePosition)
            {
                var draggingIsNotAlreadyStarted = !this.IsDragging;
                var preparedForDragging = this.IsPreparedToDrag();
                var selectionModeIsExtended = this.dataControl.SelectionMode == System.Windows.Controls.DataGridSelectionMode.Extended;
                //var newMousePositionIsDistantFromTheLastMousePosition = !MathUtilities.AreClose(this.lastMousePosition, mousePosition);
                //var dragSelectEnabled = this.dataControl.DragElementAction == DragAction.Select || this.dataControl.DragElementAction == DragAction.ExtendedSelect;

                return
                    draggingIsNotAlreadyStarted &&
                    preparedForDragging &&
                    selectionModeIsExtended; //&&
                //newMousePositionIsDistantFromTheLastMousePosition &&
                //dragSelectEnabled;
            }

            private bool IsPreparedToDrag()
            {
                return
                    this.lastMousePosition != InvalidPosition &&
                    this.dragUnit != SelectionDragUnit.None;
            }

            private void BeginDragging(Point newMousePosition)
            {
                DataGridCell cell = this.dataControl.GetDataGridCellByPosition(UserInteractionMonitor.Current.CurrentMousePosistion);
                this.lastMousePosition = newMousePosition;
                this.LastDataGridCell = cell;
                this.IsDragging = true;

                this.CaptureMouseAndSubscribeForMouseCaptureEvents();
            }

            private void CaptureMouseAndSubscribeForMouseCaptureEvents()
            {
                if (this.dataControl.CaptureMouse())
                {
                    this.dataControl.LostMouseCapture += new MouseEventHandler(dataControl_LostMouseCapture);
                }
            }
            #endregion

            #region == 선택 끝 ==
            private void dataControl_LostMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
            {
                this.End();
            }

            public void End()
            {
                this.dragUnit = SelectionDragUnit.None;
                this.IsDragging = false;
                this.InvalidateMousePosition();
                this.ReleaseMouseCaptureAndUnsubscribeForMouseCaptureEvents();

                this.scrollChange = new Point();
                //this.ScrollCoordinator.StopTracking();

                //DataGridCell cellUnderMouse = dataControl.GetDataGridCellByPosition(UserInteractionMonitor.Current.CurrentMousePosistion);
                // this.MouseInfoProvider.GetCellUnderMouse(this.lastMousePosition);

            }

            private void ReleaseMouseCaptureAndUnsubscribeForMouseCaptureEvents()
            {
                this.dataControl.ReleaseMouseCapture();
                this.dataControl.LostMouseCapture -= new MouseEventHandler(dataControl_LostMouseCapture);
            }
            #endregion
        }
        #endregion

    }



    internal static class ClipboardHelper
    {
        static ClipboardHelper()
        {
            Platform = System.Environment.OSVersion.Platform;
        }

        internal const string MacNewLine = "\r";

        internal static List<List<string>> ParseStringToTabSeparatedValues(string text)
        {
            var splitter = new[] { CurrentPlatformNewLine };

            var values = text.Split(splitter, StringSplitOptions.None)
                        .Select((token) => token.Split(new[] { '\t' }).ToList())
                        .ToList();

            if (values.Count > 1 && values.Last().Count == 1 && values.Last().Last().Length == 0)
            {
                values = values.Take(values.Count - 1).ToList();
            }

            var numberOfTokensInLine = values[0].Count;

            if (values.All((token) => token.Count == numberOfTokensInLine))
            {
                return values;
            }

            return null;
        }

        internal static string CurrentPlatformNewLine
        {
            get
            {
                var linebreak = System.Environment.NewLine;

                if (Platform == PlatformID.MacOSX)
                {
                    linebreak = MacNewLine;
                }

                return linebreak;
            }
        }

        internal static PlatformID Platform { get; set; }
    }
}