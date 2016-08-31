using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPIOTester.Models
{
    public class Row
    {
        public Row()
        {
            this.Cells = new List<Cell>();
        }

        public List<Cell> Cells
        {
            get; set;
        }

        public Cell CreateCell()
        {
            var cell = new Cell();
            this.Cells.Add(cell);
            return cell;
        }

        public Cell CreateCell(object value)
        {
            var temp = CreateCell();
            temp.Value = value;
            return temp;
        }

        public int RowIndex
        {
            get;set;
        }
    }
}