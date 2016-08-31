using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using NPOI.SS.UserModel;

namespace NPIOTester.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var result = new Models.Sheet();
            var file = new FileStream(@"C:\Users\lwl\Desktop\数据多的表格\秸秆利用情况.xls", FileMode.Open);
            var workBook = new HSSFWorkbook(file);
            var sheet = workBook.GetSheetAt(0);

            if (sheet != null)
            {
                var rows = sheet.GetRowEnumerator();
                while (rows.MoveNext())
                {
                    var temp = result.CreateRow();
                    var row = rows.Current as IRow;  
                    foreach (var cell in row.Cells)
                    {
                        var tempCell = CreateCell(cell, sheet);
                        if (tempCell != null)
                        {
                            temp.Cells.Add(tempCell);
                        }                      
                    }
                }
            }
            return Json(result);
        }

        public Models.Cell CreateCell(ICell cell, ISheet sheet)
        {
            Models.Cell result = null;
            bool valid = true;
            int rowSpan = 1;
            int columnSpan = 1;

            if (cell.IsMergedCell)
            {
                valid = false;
                for (var i = 0; i < sheet.NumMergedRegions; i++)
                {
                    var region = sheet.GetMergedRegion(i);
                    if (region.FirstRow == cell.RowIndex && region.FirstColumn == cell.ColumnIndex)
                    {
                        valid = true;
                        rowSpan = region.LastRow - region.FirstRow + 1;
                        columnSpan = region.LastColumn - region.FirstColumn + 1;
                        break;
                    }
                }
            }

            if(valid)
            {
                result = new Models.Cell();
                result.Value = GetValue(cell);
                result.ColumnIndex = cell.ColumnIndex;
                result.RowIndex = cell.RowIndex;
                result.ColumnSpan = columnSpan;
                result.RowSpan = rowSpan;
            }            
            return result;
        }

        private object GetValue(ICell cell)
        {
            object value = null;

            switch (cell.CellType)
            {
                case CellType.Blank:
                    break;
                case CellType.Boolean:
                    value = cell.BooleanCellValue ? "1" : "0";
                    break;
                case CellType.Error:
                    value = cell.ErrorCellValue;
                    break;
                case CellType.Formula:
                    HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Row.Sheet.Workbook);
                    cell = e.EvaluateInCell(cell);
                    value = GetValue(cell);
                    break;
                case CellType.Numeric:
                    value = cell.NumericCellValue.ToString();
                    break;
                case CellType.String:
                    value = cell.StringCellValue;
                    break;
                case CellType.Unknown:
                    break;
            }
            return value;
        }
    }
}