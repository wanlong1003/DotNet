using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utilities.NPOI
{
    public abstract class ExcelTransferData : ITransferData
    {
        protected IWorkbook _workBook;

        public Encoding Encoding
        {
            get;
            set;
        }

        public ExcelTransferData()
        {
            Encoding = Encoding.UTF8;
        }

        public ExcelTransferData(Encoding encoding)
        {
            Encoding = encoding;
        }

        public virtual DataTable GetData(Stream stream)
        {
            DataTable dt = new DataTable();
            var sheet = _workBook.GetSheetAt(0);
            if (sheet != null)
            {
                var headerRow = sheet.GetRow(sheet.FirstRowNum);
                for (int i = 0; i < headerRow.LastCellNum; i++)
                {
                    dt.Columns.Add("Column" + i.ToString());
                    var obj = GetValue(headerRow.GetCell(i));
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Column" + i.ToString()));
                    }
                    else
                    {
                        dt.Columns.Add(obj.ToString());
                    }
                }

                var rows = sheet.GetRowEnumerator();
                while (rows.MoveNext())
                {
                    var dr = dt.NewRow();
                    var row = rows.Current as IRow;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        var cell = row.GetCell(i);
                        if (cell != null)
                        {
                            dr[i] = GetValue(cell);
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        public virtual Stream GetStream(DataTable table)
        {
            var sheet = _workBook.CreateSheet();
            if (table != null)
            {
                var rowCount = table.Rows.Count;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var row = sheet.CreateRow(i);
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        var cell = row.CreateCell(j);
                        if (table.Rows[i][j] != null)
                        {
                            cell.SetCellValue(table.Rows[i][j].ToString());
                        }
                    }
                }
            }
            MemoryStream ms = new MemoryStream();
            _workBook.Write(ms);
            return ms;
        }

        private object GetValue(ICell cell)
        {
            object value = null;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    break;
                case CellType.Boolean:
                    value = cell.BooleanCellValue ? "1" : "0"; break;
                case CellType.Error:
                    value = cell.ErrorCellValue; break;
                case CellType.Formula:
                    value = "=" + cell.CellFormula; break;
                case CellType.Numeric:
                    value = cell.NumericCellValue.ToString(); break;
                case CellType.String:
                    value = cell.StringCellValue; break;
                case CellType.Unknown:
                    break;
            }
            return value;
        }
    }
}
