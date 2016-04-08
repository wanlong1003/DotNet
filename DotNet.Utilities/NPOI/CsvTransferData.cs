using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utilities.NPOI
{
    public class CsvTransferData : ITransferData
    {
        public CsvTransferData()
        {
            Encoding = Encoding.UTF8;
        }

        public CsvTransferData(Encoding encoding)
        {
            Encoding = encoding;
        }

        public Encoding Encoding
        {
            get;
            set;
        }

        public DataTable GetData(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream, Encoding))
            {
                DataTable dt = new DataTable();
                while (!reader.EndOfStream)
                {
                    DataRow dr = dt.NewRow();
                    string rowText = reader.ReadLine();
                    string[] cols = rowText.Split(',');
                    for (int i = 0; i < cols.Length; i++)
                    {
                        if(dt.Columns.Count < i)
                        {
                            dt.Columns.Add("Column"+ i);
                        }
                        dr[i] = cols[i];
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }

        public Stream GetStream(DataTable table)
        {
            StringBuilder sb = new StringBuilder();
            if (table != null && table.Columns.Count > 0 && table.Rows.Count > 0)
            {
                foreach (DataRow item in table.Rows)
                {
                    var separator = string.Empty;
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        sb.Append(separator);
                        if (item[i] != null && item[i] != DBNull.Value)
                        {
                            sb.Append("\"").Append(item[i].ToString().Replace("\"", "\"\"")).Append("\"");
                        }
                        separator = ",";
                    }
                    sb.Append("\n");
                }
            }
            return new MemoryStream(Encoding.GetBytes(sb.ToString()));
        }
    }
}
