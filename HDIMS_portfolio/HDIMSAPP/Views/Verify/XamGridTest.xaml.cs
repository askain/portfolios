using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Views.Verify
{
    public partial class XamGridTest : Page
    {

        #region == 초기화 ==
        public XamGridTest()
        {
            InitializeComponent();
            InitSearchPanel();

            //damGrid.CacheMode = new BitmapCache();
            damGrid.IsReadOnly = true;
            damGrid.AlternatingRowBackground = null;
            damGrid.LoadingRow += new EventHandler<DataGridRowEventArgs>(damGrid_LoadingRow);
            SubLayoutRoot.SizeChanged += new SizeChangedEventHandler(LayoutRoot_SizeChanged);
            damGrid.RowDoubleClicked += new Controls.DoubleClickDataGrid.RowDoubleClickedHandler(damGrid_RowDoubleClicked);
            //DataGridRow _row = new DataGridRow();
            //DataGridCell _cell = new DataGridCell();
            //_cell.
            
        }

        void damGrid_RowDoubleClicked(object sender, Controls.DataGridRowClickedArgs e)
        {
            DataGridTextColumn _tc = e.DataGridColumn as DataGridTextColumn;

            MessageBox.Show("popup: " + e.DataGridRow.GetIndex() + " / " + e.DataGridColumn.DisplayIndex + _tc.Binding.Path.Path);
        }
        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
            try
            {

                //damGrid.Width = (double)HtmlPage.Window.Eval("screen.width") - 90;
                //damGrid.Height = (double)HtmlPage.Window.Eval("screen.height") - (double)SubLayoutRoot.RowDefinitions[3].Height.Value - 343;

                damGrid.Width = SubLayoutRoot.ActualWidth - 22.0;
                damGrid.Height = SubLayoutRoot.ActualHeight - 60.0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //MessageBox.Show("size: " + damGrid.Width + " / " + damGrid.Height);
        }


        private void InitSearchPanel()
        {
            CreateDamGridColumns();
            //GetDamDataList();
        }
        #endregion

        IList<IDictionary> itemSource = new List<IDictionary>();
        IList<IDictionary> zitemSource = new List<IDictionary>();
        private void GetDamDataList()
        {

            int columnCount = int.Parse(columnTextBox.Text);
            int rowCount = int.Parse(rowTextBox.Text);
            int emptyCount = int.Parse(columnTextBox.Text);

            
            IDictionary<string, string> row = new Dictionary<string, string>();

            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                row = new Dictionary<string, string>();
                for (int colIndex = 0; colIndex < columnCount; colIndex++)
                {
                    if (colIndex > emptyCount)
                    {
                        row.Add("Column_" + colIndex.ToString(), " ");
                    }
                    else
                    {
                        row.Add("Column_" + colIndex.ToString(), rowIndex.ToString() + "_" + colIndex.ToString());
                    }
                }
                itemSource.Add((IDictionary)row);
            }
            //MessageBox.Show(itemSource.Count.ToString());
            
            damGrid.ItemsSource = itemSource.ToDataSource("aaa");

            //zitemSource = ZeroValueConv(itemSource);

        }

        private void damGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.GetIndex() == 2)
            {
                this.damGrid.SelectedIndex = e.Row.GetIndex();
                for (int i = 0; i < damGrid.Columns.Count; i++)
                {
                    DataGridColumn column = this.damGrid.Columns[i];
                    FrameworkElement fe = column.GetCellContent(e.Row);
                    FrameworkElement result = GetParent(fe, typeof(DataGridCell));
                    if (result != null)
                    {
                        DataGridCell cell = (DataGridCell)result;

                        cell.Background = new SolidColorBrush(Colors.Red);
                        cell.Foreground = new SolidColorBrush(Colors.Blue);
                        cell.FontSize = 14.0;
                        cell.FontWeight = FontWeights.Bold;
                    }
                }
            }
        }

        private FrameworkElement GetParent(FrameworkElement child, Type targetType)
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


        private void cimsBtn_Click(object sender, RoutedEventArgs e)
        {
            GetDamDataList();
        }

        private IList<IDictionary> ZeroValueConv(IList<IDictionary> source)
        {
            IList<IDictionary> zeroItemSource = (IList<IDictionary>)damGrid.ItemsSource;

            foreach (IDictionary row in itemSource)
            {
                IDictionary<string, string> dic = new Dictionary<string, string> ();
                for (int columnIndex = 0; columnIndex < int.Parse(columnTextBox.Text); columnIndex++)
                {
                    if ("0".Equals(row["Column_" + columnIndex.ToString()]))
                    {
                        dic.Add("Column_" + columnIndex.ToString(), "");
                    }
                    else 
                    {
                        dic.Add("Column_" + columnIndex.ToString(), row["Column_" + columnIndex.ToString()].ToString());
                    }
                }
                zeroItemSource.Add((IDictionary)dic);
            }

            return zeroItemSource;
        }


        bool bool_checked = false;
        private void checkbox_Checked(object sender, EventArgs e) 
        {
            if (bool_checked == true)
            {
                bool_checked = false;
                damGrid.ItemsSource = itemSource;
            }
            else
            {
                bool_checked = true;
                damGrid.ItemsSource = zitemSource;
            }
        }


        private void CreateDamGridColumns()
        {
            for (int colIndex = 0; colIndex < int.Parse(columnTextBox.Text); colIndex++)
            {
                DataGridTextColumn _uc = new DataGridTextColumn()
                {
                    Header = "Column_" + colIndex.ToString(),
                    Binding = new Binding("Column_" + colIndex.ToString()),
                    //IsFixed = FixedState.Left,
                    //HeaderStyle = HeaderCellStyle,
                    //CellStyle = DefaultCellStyle,
                    //ValueConverter = new HDIMSAPP.Common.Converter.DateTimeConverter(),
                    Width = new DataGridLength(50)
                };
                //_uc.ConditionalFormatCollection.Add(new DateTimeContionalFormatRule());
                damGrid.Columns.Add(_uc);
            }

            

        }
    }
}
