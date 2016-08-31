using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPIOTester.Models
{
    public class Sheet
    {
        public Sheet()
        {
            this.Rows = new List<Row>();
        }

        public List<Row> Rows
        {
            get;set;
        }

        public int RowCount
        {
            get
            {
                return this.Rows.Count;
            }
        }

        public Row CreateRow()
        {
            var row = new Row();
            this.Rows.Add(row);
            return row;
        }
    }
}