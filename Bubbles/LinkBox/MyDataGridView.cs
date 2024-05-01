using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bubbles
{
    public class MyDataGridView : DataGridView
    {
        protected override void SetSelectedRowCore(int rowIndex, bool selected)
        {
            if (selected && !WantRowSelection(rowIndex))
            {
                
            }
            base.SetSelectedRowCore(rowIndex, selected);
        }

        //protected virtual void SetSelectedCellCore(int columnIndex, int rowIndex, bool selected)
        //{
        //    if (selected && WantRowSelection(rowIndex))
        //    {
        //        base.SetSelectedRowCore(rowIndex, selected);
        //    }
        //}

        bool WantRowSelection(int rowIndex)
        {
            if (rowIndex != 3)
                return true;
            else
                return false;
        }
    }
}
