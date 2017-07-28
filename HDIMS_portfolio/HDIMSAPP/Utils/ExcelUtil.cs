using System.Windows.Media;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;

namespace HDIMSAPP.Utils
{
    public class ExcelUtil
    {
        public static Workbook CreateWorkbook(string sheetName="Sheet1")
        {
            Workbook _workbook = new Workbook();
            Worksheet worksheet = _workbook.Worksheets.Add(sheetName);
            _workbook.SetCurrentFormat(WorkbookFormat.Excel97To2003);
            return _workbook;
        }

        public static void SetWorksheetCellFormat(WorksheetCell _worksheetCell, RowType _rowType)
        {

            if (_rowType == RowType.HeaderRow)
            {
                _worksheetCell.CellFormat.FillPattern = FillPatternStyle.Solid;
                _worksheetCell.CellFormat.FillPatternForegroundColor = Colors.LightGray;
                _worksheetCell.CellFormat.Alignment = HorizontalCellAlignment.Center;
                _worksheetCell.CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
                _worksheetCell.CellFormat.WrapText = ExcelDefaultableBoolean.True;
                _worksheetCell.CellFormat.TopBorderColor = Colors.Black;
                _worksheetCell.CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.BottomBorderColor = Colors.Black;
                _worksheetCell.CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.LeftBorderColor = Colors.Black;
                _worksheetCell.CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.RightBorderColor = Colors.Black;
                _worksheetCell.CellFormat.RightBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.Font.Color = Colors.Black;
                _worksheetCell.CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                _worksheetCell.CellFormat.Font.Height = 200;
            }
            else if (_rowType == RowType.DataRow)
            {
                _worksheetCell.CellFormat.FillPattern = FillPatternStyle.Solid;
                _worksheetCell.CellFormat.FillPatternForegroundColor = Colors.White;
                _worksheetCell.CellFormat.WrapText = ExcelDefaultableBoolean.True;
                _worksheetCell.CellFormat.TopBorderColor = Colors.Black;
                _worksheetCell.CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.BottomBorderColor = Colors.Black;
                _worksheetCell.CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.LeftBorderColor = Colors.Black;
                _worksheetCell.CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.RightBorderColor = Colors.Black;
                _worksheetCell.CellFormat.RightBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.Font.Color = Colors.Black;
                _worksheetCell.CellFormat.Font.Height = 180;
            }
            else if (_rowType == RowType.FooterRow)
            {
            }
            else if (_rowType == RowType.GroupByRow)
            {
                _worksheetCell.CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            }
        }

        public static void SetMergeRegionCellFormat(WorksheetMergedCellsRegion _worksheetCell, RowType _rowType)
        {

            if (_rowType == RowType.HeaderRow)
            {
                _worksheetCell.CellFormat.FillPattern = FillPatternStyle.Solid;
                _worksheetCell.CellFormat.FillPatternForegroundColor = Colors.LightGray;
                _worksheetCell.CellFormat.Alignment = HorizontalCellAlignment.Center;
                _worksheetCell.CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
                _worksheetCell.CellFormat.WrapText = ExcelDefaultableBoolean.True;
                _worksheetCell.CellFormat.TopBorderColor = Colors.Black;
                _worksheetCell.CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.BottomBorderColor = Colors.Black;
                _worksheetCell.CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.LeftBorderColor = Colors.Black;
                _worksheetCell.CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.RightBorderColor = Colors.Black;
                _worksheetCell.CellFormat.RightBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.Font.Color = Colors.Black;
                _worksheetCell.CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                _worksheetCell.CellFormat.Font.Height = 200;
            }
            else if (_rowType == RowType.DataRow)
            {
                _worksheetCell.CellFormat.FillPattern = FillPatternStyle.Solid;
                _worksheetCell.CellFormat.FillPatternForegroundColor = Colors.White;
                _worksheetCell.CellFormat.WrapText = ExcelDefaultableBoolean.True;
                _worksheetCell.CellFormat.TopBorderColor = Colors.Black;
                _worksheetCell.CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.BottomBorderColor = Colors.Black;
                _worksheetCell.CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.LeftBorderColor = Colors.Black;
                _worksheetCell.CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.RightBorderColor = Colors.Black;
                _worksheetCell.CellFormat.RightBorderStyle = CellBorderLineStyle.Thin;
                _worksheetCell.CellFormat.Font.Color = Colors.Black;
                _worksheetCell.CellFormat.Font.Height = 180;
            }
            else if (_rowType == RowType.FooterRow)
            {
            }
            else if (_rowType == RowType.GroupByRow)
            {
                _worksheetCell.CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            }
        }
    }
}
