    using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENCAPv3.UI
{
    public static partial class MainParamatersFormPartial
    {
        public static DataTable staticDt;
        public static bool isPollingEnabled = false;
        public static DataTable dataTable;
        public static DataTable ConvertDataGridViewToDataTable(DataGridView dgv)
        {
            dataTable = new DataTable();

            // Add columns to the DataTable
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                dataTable.Columns.Add(column.HeaderText);
            }

            // Add rows to the DataTable
            DataRow dataRow;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                // Skip the last empty row if AllowUserToAddRows is true
                if (!row.IsNewRow)
                {
                    dataRow = dataTable.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dataRow[cell.ColumnIndex] = cell.Value;
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            staticDt = dataTable;
            return dataTable;
        }


    }
}
 