using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Navigation;
using HDIMSAPP.Utils;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using Newtonsoft.Json;

namespace HDIMSAPP.Views.DataSearch
{
    public partial class QueryBrowser : Page
    {
        private string queryUrl = "/DataSearch/GetQueryExecute/";

        public QueryBrowser()
        {
            InitializeComponent();
        }

        // 사용자가 이 페이지를 탐색할 때 실행됩니다.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (queryTxt == null || queryTxt.Text.Trim().Length < 1)
            {
                MessageBox.Show("쿼리를 입력하셔야 합니다.");
                return;
            }
            ExecQuery();
        }

        private void ExecQuery()
        {
            LoadingBar.IsBusy = true;
            dataGrid.ItemsSource = null;

            string param = HttpUtility.UrlEncode(queryTxt.Text.Trim());

            WebClient client = new WebClient();
            client.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(ExecQueryCompleted);
            client.UploadStringAsync(new Uri(queryUrl, UriKind.Relative), "POST", "query=" + param);
        }

        private void ExecQueryCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                LoadingBar.IsBusy = false;
                MessageBox.Show("잘못된 쿼리이거나, 서버에서 처리할 수 없는 쿼리입니다.\n쿼리를 확인하신 후 다시 조회하세요");
                return;
            }
            string strJson = e.Result;
            IList<IDictionary<string, string>> list = JsonConvert.DeserializeObject<IList<IDictionary<string, string>>>(strJson);
            IList<IDictionary> _tmp = new List<IDictionary>();
            foreach (Dictionary<string, string> _dc in list)
            {
                IDictionary _item = _dc;
                _tmp.Add(_item);
            }
            dataGrid.ItemsSource = _tmp.ToDataSource(); ;
            LoadingBar.IsBusy = false;

        }
        #region -- 엑셀 다운로드 --
        private void excelBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel Workbook (.xls)|*.xls";

            if (saveDialog.ShowDialog().Value)
            {

                Workbook _workbook = ExcelUtil.CreateWorkbook("Sheet1");
                Worksheet _worksheet = _workbook.Worksheets[0];
                Stream _stream = null;
                WorksheetRow _worksheetRow;
                WorksheetCell _worksheetCell;

                try
                {
                    _stream = saveDialog.OpenFile();

                    //Freeze Cell
                    _worksheet.DisplayOptions.PanesAreFrozen = true;
                    _worksheet.DisplayOptions.FrozenPaneSettings.FrozenRows = 1;
                    //Header Row
                    _worksheetRow = _worksheet.Rows[0];
                    for (int ix = 0; ix < this.dataGrid.Columns.Count; ix++)
                    {
                        _worksheetCell = _worksheetRow.Cells[ix];
                        _worksheetCell.Value = this.dataGrid.Columns[ix].Key;
                        ExcelUtil.SetWorksheetCellFormat(_worksheetCell, RowType.HeaderRow);

                    }
                    //Data Row
                    foreach (Row _gridRow in this.dataGrid.Rows)
                    {
                        _worksheetRow = _worksheet.Rows[_gridRow.Index + 1]; //Excel Next Row Number 1 start
                        for (int _colIdx = 0; _colIdx < this.dataGrid.Columns.Count; _colIdx++)
                        {
                            _worksheetCell = _worksheetRow.Cells[_colIdx];
                            _worksheetCell.Value = _gridRow.Cells[_colIdx].Value;
                            ExcelUtil.SetWorksheetCellFormat(_worksheetCell, RowType.DataRow);
                        }
                    }
                    _workbook.Save(_stream);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
                finally
                {
                    if (_stream != null) _stream.Close();
                }
            }

        }
        #endregion

    }
}
