using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Grids.Primitives;

namespace HDIMSAPP.Common
{
    /// <summary>
    /// Provides extension methods for the <see cref="XamGrid"/>.
    /// </summary>
    public static class XamGridClipboardExtensions
    {
        #region Methods

        #region Public

        #region PasteData

        /// <summary>
        /// Pastes data on the <see cref="XamGrid"/>.
        /// </summary>
        /// <param name="grid">The <see cref="XamGrid"/>.</param>
        /// <param name="values">The clipboard values from the <see cref="ClipboardPastingEventArgs"/>.</param>
        public static void PasteData(this XamGrid grid, IList<List<string>> values)
        {
            if (grid == null)
            {
                throw new ArgumentNullException("grid");
            }

            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            //단일값 체크 
            //단일값일 경우는 동일 셀인덱스로 잡지않고, 무조건 선택된 셀에 단일값을 입력한다.
            bool isSinglePaste = false;
            string parsedSingleValue = "";
            if (values.Count == 1 && values[0].Count == 1)
            {
                isSinglePaste = true;
                parsedSingleValue = values[0][0];
            }
            // We will paste only if we have an ActiveCell as starting point
            // and if the ActiveCell is on a DataRow
            if (grid.ActiveCell == null || grid.ActiveCell.Row.RowType != RowType.DataRow ||
                grid.ActiveCell.Column == null)
            {
                return;
            }

            IList<Cell> cellsToSelect = new List<Cell>();
            bool stopCellProcessing = false;

            if (isSinglePaste == true)
            {
                SelectedCellsCollection selCells = grid.SelectionSettings.SelectedCells;
                foreach (Cell selCel in selCells)
                {
                    Row row = selCel.Row as Row;
                    EditableColumn column = selCel.Column as EditableColumn;
                    bool isValid = SetCellValue(row, column, parsedSingleValue);

                    if (!isValid)
                    {
                        MessageBoxResult result = MessageBox.Show(
                            string.Format(
                                CultureInfo.CurrentCulture,
                                "{0}의 타입으로 변경할 수 없습니다. {1}은 유효한 값이 아닙니다.",
                                column.HeaderText ?? column.Key,
                                parsedSingleValue),
                            "붙혀넣기 에러",
                            MessageBoxButton.OKCancel);

                        if (result == MessageBoxResult.Cancel)
                        {
                            stopCellProcessing = true;
                            break;
                        }
                    }
                    else
                    {
                        Cell cell = row.Cells[column] as Cell;

                        if (cell != null)
                        {
                            cellsToSelect.Add(cell);
                        }
                    }
                }
            }
            else
            {

                // All coulumns except GroupColumns
                IList<Column> allColumns =
                    grid.ActiveCell.Column.ColumnLayout.Columns.AllColumns.OfType<Column>().Where(c => !(c is GroupColumn)).ToList();

                //선택된 셀의 크기

                int maxCellsPerRow = values.Max(r => r.Count);

                // The index of the column where the ActiveCell is
                int columnIndex = allColumns.IndexOf(grid.ActiveCell.Column);

                if (columnIndex + maxCellsPerRow > allColumns.Count)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "복사한 데이터가 붙혀넣기 영역보다 큽니다.\n계속 진행하시면 데이터가 손실될 것입니다.\n계속 하시겠습니까?",
                        "붙혀넣기 에러",
                        MessageBoxButton.OKCancel);

                    if (result == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                }

                // The index of the row where the Active cell is
                int rowIndex = grid.ActiveCell.Row.Index;

                RowCollection rowCollection = grid.GetRowCollectionResolved();

                // The number of rows we want to paste
                int rowsCount = values.Count;

                // The number of rows in the current band
                int bandRowsCount = rowCollection.Count;

                // The index of the last row where we will be able to paste data
                int lastRowIndex = Math.Min(bandRowsCount - 1, rowIndex + rowsCount - 1);

                

                // Iterating through the rows and pasting the data
                for (int i = rowIndex; i <= lastRowIndex; i++)
                {
                    if (stopCellProcessing)
                    {
                        break;
                    }

                    // The index of the row in the values list
                    int parsedRowIndex = i - rowIndex;

                    Row row = rowCollection[i];

                    // The number of cells we want to paste
                    int cellsCount = values[parsedRowIndex].Count;

                    // The index of the last cell where we will be able to paste data
                    int lastCellIndex = Math.Min(allColumns.Count - 1, cellsCount + columnIndex - 1);

                    for (int j = columnIndex; j <= lastCellIndex; j++)
                    {

                        // The index of the cell in the values list
                        int parsedColumnIndex = j - columnIndex;

                        EditableColumn column = allColumns[j] as EditableColumn;

                        // Skipping non-editable columns and UnboundColumns
                        if (column == null || column.IsReadOnly)
                        {
                            //읽기전용은 붙혀넣기 하지 않음.
                            continue;
                        }

                        if (column == null || column is UnboundColumn)
                        {
                            continue;
                        }
                        string parsedCellValue = values[parsedRowIndex][parsedColumnIndex];

                        bool isValid = SetCellValue(row, column, parsedCellValue);

                        if (!isValid)
                        {
                            MessageBoxResult result = MessageBox.Show(
                                string.Format(
                                    CultureInfo.CurrentCulture,
                                    "{0}의 타입으로 변경할 수 없습니다. {1}은 유효한 값이 아닙니다.",
                                    column.HeaderText ?? column.Key,
                                    parsedCellValue),
                                "붙혀넣기 에러",
                                MessageBoxButton.OKCancel);

                            if (result == MessageBoxResult.Cancel)
                            {
                                stopCellProcessing = true;
                                break;
                            }
                        }
                        else
                        {
                            Cell cell = row.Cells[column] as Cell;

                            if (cell != null)
                            {
                                cellsToSelect.Add(cell);
                            }
                        }
                    }
                }
            }
            grid.SelectCells(cellsToSelect);
        }

        #endregion // PasteData

        #region IsSelectionValid

        /// <summary>
        /// Determines whether the selected region is valid for copying.
        /// </summary>
        /// <param name="grid">The <see cref="XamGrid"/>.</param>
        /// <returns>
        ///     <c>true</c> if the selection is valid; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// The selection is considered as valid if the selected cells form a rectangular region in a single band.
        /// </remarks>
        public static bool IsSelectionValid(this XamGrid grid)
        {
            if (grid == null)
            {
                throw new ArgumentNullException("grid");
            }

            if (grid.GetCopyTypeResolved() == GridClipboardCopyType.SelectedRows)
            {
                SelectedRowsCollection selectedRows = grid.SelectionSettings.SelectedRows;

                if (!selectedRows.Any() || grid.IsSelectionCrossBand())
                {
                    return false;
                }

                int minRowIndex = selectedRows.Min(r => r.Index);
                int maxRowIndex = selectedRows.Max(r => r.Index);

                if ((maxRowIndex - minRowIndex + 1) == selectedRows.Count)
                {
                    return true;
                }
            }
            else
            {
                SelectedCellsCollection selectedCells = grid.SelectionSettings.SelectedCells;

                if (!selectedCells.Any() || grid.IsSelectionCrossBand())
                {
                    return false;
                }

                IList<ColumnBase> allColumns =
                    selectedCells[0].Column.ColumnLayout.Columns.AllColumns.Where(i => i is Column && !(i is GroupColumn)).ToList();

                int minRowIndex = selectedCells.Min(c => c.Row.Index);
                int maxRowIndex = selectedCells.Max(c => c.Row.Index);
                int minColIndex = selectedCells.Min(c => allColumns.IndexOf(c.Column));
                int maxColIndex = selectedCells.Max(c => allColumns.IndexOf(c.Column));

                int expectedCellCount = (maxColIndex - minColIndex + 1) * (maxRowIndex - minRowIndex + 1);

                if (expectedCellCount == selectedCells.Count)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion // IsSelectionValid

        #endregion // Public

        #region Private

        #region GetCopyTypeResolved

        /// <summary>
        /// Gets the CopyType used by the <see cref="XamGrid"/>.
        /// </summary>
        /// <param name="grid">The <see cref="XamGrid"/>.</param>
        /// <returns>The resolved <see cref="GridClipboardCopyType"/>.</returns>
        private static GridClipboardCopyType GetCopyTypeResolved(this XamGrid grid)
        {
            if (grid.ClipboardSettings.CopyType == GridClipboardCopyType.Default)
            {
                if (grid.SelectionSettings.CellClickAction == CellSelectionAction.SelectCell)
                {
                    return GridClipboardCopyType.SelectedCells;
                }

                return GridClipboardCopyType.SelectedRows;
            }

            return grid.ClipboardSettings.CopyType;
        }

        #endregion // GetCopyTypeResolved

        #region GetRowCollectionResolved

        /// <summary>
        /// Gets the <see cref="RowCollection"/> containing the row of the <see cref="XamGrid.ActiveCell"/>.
        /// </summary>
        /// <param name="grid">The <see cref="XamGrid"/>.</param>
        /// <returns>The <see cref="RowCollection"/> containing the row of the <see cref="XamGrid.ActiveCell"/>.</returns>
        private static RowCollection GetRowCollectionResolved(this XamGrid grid)
        {
            ChildBand childBand = ((Row)grid.ActiveCell.Row).ParentRow as ChildBand;
            GroupByRow groupByRow = ((Row)grid.ActiveCell.Row).ParentRow as GroupByRow;

            if (childBand != null)
            {
                return childBand.Rows;
            }

            if (groupByRow != null)
            {
                return groupByRow.Rows;
            }

            return grid.Rows;
        }

        #endregion // GetRowCollectionResolved

        #region IsSelectionCrossBand

        /// <summary>
        /// Determines whether the selected cells/rows are in different bands.
        /// </summary>
        /// <param name="grid">The <see cref="XamGrid"/>.</param>
        /// <returns>
        ///     <c>true</c> if the selection is cross-band; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsSelectionCrossBand(this XamGrid grid)
        {
            if (grid.GetCopyTypeResolved() == GridClipboardCopyType.SelectedCells)
            {
                SelectedCellsCollection selectedCells = grid.SelectionSettings.SelectedCells;

                if (!selectedCells.Any())
                {
                    return false;
                }

                int level = selectedCells[0].Row.Level;

                return selectedCells.Any(cell => cell.Row.Level != level);
            }
            else
            {
                SelectedRowsCollection selectedRows = grid.SelectionSettings.SelectedRows;

                if (!selectedRows.Any())
                {
                    return false;
                }

                int level = selectedRows[0].Level;

                return selectedRows.Any(row => row.Level != level);
            }
        }

        #endregion // IsSelectionCrossBand

        #region SetCellValue

        /// <summary>
        /// Sets the cell of a value.
        /// </summary>
        /// <param name="row">The <see cref="Row"/> containing the cell.</param>
        /// <param name="column">The <see cref="Column"/> containing the cell.</param>
        /// <param name="parsedCellValue">The parsed cell value.</param>
        /// <returns>
        ///     <c>true</c> if the parsed value can be converted and set to the cell; otherwise, <c>false</c>.
        /// </returns>
        private static bool SetCellValue(Row row, EditableColumn column, string parsedCellValue)
        {
            bool isValid = true;

            CellValueObject cellValueObj = new CellValueObject();

            Binding binding = new Binding(column.Key)
            {
                Mode = BindingMode.TwoWay,
                Source = row.Data,
                Converter = column.EditorValueConverter,
                ConverterParameter = column.EditorValueConverterParameter,
                ConverterCulture = CultureInfo.CurrentCulture
            };

            BindingOperations.SetBinding(cellValueObj, CellValueObject.ValueProperty, binding);

            if (column.EditorValueConverter != null && column is TextColumn)
            {
                cellValueObj.Value = parsedCellValue;
            }
            else
            {
                object resolvedValue = null;
                bool targetTypeIsNullable = false;

                try
                {
                    // The target data type
                    Type targetType = column.DataType;

                    if (targetType != null)
                    {
                        // If the targetType is Nullable we have to get the underlying type so we can use it for convertion
                        if (targetType.IsGenericType
                            && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                        {
                            targetType = Nullable.GetUnderlyingType(targetType);
                            targetTypeIsNullable = true;
                        }

                        if (!(targetTypeIsNullable && string.IsNullOrEmpty(parsedCellValue)))
                        {
                            // Convert the parsed value to the target type
                            resolvedValue = Convert.ChangeType(parsedCellValue, targetType, CultureInfo.CurrentCulture);
                        }
                    }
                }
                catch (InvalidCastException)
                {
                    isValid = false;
                }
                catch (FormatException)
                {
                    isValid = false;
                }
                catch (OverflowException)
                {
                    isValid = false;
                }
                catch (ArgumentException)
                {
                    isValid = false;
                }

                if (resolvedValue != null)
                {
                    cellValueObj.Value = resolvedValue;
                }
                else if (targetTypeIsNullable && string.IsNullOrEmpty(parsedCellValue))
                {
                    // We have a nullable targert type and empty parsed value,
                    // it looks like an empty cell ... 
                    cellValueObj.Value = null;
                }
            }

            return isValid;
        }

        #endregion // SetCellValue

        #region SelectCells

        /// <summary>
        /// Selects cells of the <see cref="XamGrid"/>.
        /// </summary>
        /// <param name="grid">The <see cref="XamGrid"/>.</param>
        /// <param name="cellsToSelect">The cells that will be selected.</param>
        private static void SelectCells(this XamGrid grid, IEnumerable<Cell> cellsToSelect)
        {
            if (cellsToSelect.Any())
            {
                grid.SelectionSettings.SelectedCells.Clear();
            }

            using (var selectedCollection = new SelectedCellsCollection())
            {
                foreach (var cell in cellsToSelect)
                {
                    selectedCollection.Add(cell);
                }

                grid.SelectionSettings.SelectedCells.AddRange(selectedCollection);
            }
        }

        #endregion // SelectCells

        #endregion // Private

        #endregion // Methods

        #region CellValueObject Class

        /// <summary>
        /// A class used to store off the value of a <see cref="Cell"/>.
        /// </summary>
        internal class CellValueObject : DependencyObject
        {
            #region Value

            /// <summary>
            /// Identifies the <see cref="Value"/> dependency property. 
            /// </summary>
            public static readonly DependencyProperty ValueProperty =
                DependencyProperty.Register("Value", typeof(object), typeof(CellValueObject), null);

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>The value.</value>
            public object Value
            {
                get
                {
                    return this.GetValue(ValueProperty);
                }

                set
                {
                    this.SetValue(ValueProperty, value);
                }
            }

            #endregion // Value
        }

        #endregion // CellValueObject Class
    }
}
