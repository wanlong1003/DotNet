using DotNet.Utilities.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            OracleHelper helper = new OracleHelper("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=ip)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=FLSTEST)));Persist Security Info=True;User ID=bfdrp;Password=a;");
            DataSet ds = helper.ExecuteDataSet("select * from prod");
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                for (int i = 0; i < dr.ItemArray.Count(); i++)
                {
                    Console.Write(dr[i]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
