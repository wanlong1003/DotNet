using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utilities.NPOI
{
    public class Excel2007TransferData: ExcelTransferData
    {
        public override DataTable GetData(Stream stream)
        {
            base._workBook = new XSSFWorkbook(stream);
            return base.GetData(stream);
        }

        public override Stream GetStream(DataTable table)
        {
            base._workBook = new XSSFWorkbook();
            return base.GetStream(table);
        }
    }
}
