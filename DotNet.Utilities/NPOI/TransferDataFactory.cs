using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utilities.NPOI
{
    public class TransferDataFactory
    {
        public static ITransferData GetUtil(string fileName)
        {
            var fileType = Path.GetExtension(fileName);
            switch (fileType.ToUpper())
            {
                case ".CSV": return new CsvTransferData();
                case ".XLS": return new Excel2003TransferData();
                case ".XLSX": return new Excel2007TransferData();
                default: throw new Exception("不支持的类型");
            }
        }
    }
}
