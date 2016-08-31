using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPIOTester.Models
{
    public class Cell
    {
        public object Value
        {
            get; set;
        }

        public int RowSpan
        {
            get;set;
        }

        public int ColumnSpan
        {
            get;set;
        }

        public int RowIndex
        {
            get;set;
        }

        public int ColumnIndex
        {
            get;set;
        }
    }
}