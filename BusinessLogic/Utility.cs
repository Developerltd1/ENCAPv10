using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static BusinessLogic.Model.StaticModel.DashboardModel;

namespace BusinessLogic
{
    public class Utility
    {



        public DataTable ReplaceSpecialCharsWithNull(DataTable dataTable)
        {
            Regex specialCharPattern = new Regex(@"^[,\/\-_%\s]+$");

            // Loop through all rows in the DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Loop through each column in the row
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var cellValue = row[i]?.ToString();

                    // Check if the cell value contains only special characters
                    if (specialCharPattern.IsMatch(cellValue))
                    {
                        // Assign NULL if the cell contains only special characters
                        row[i] = DBNull.Value;
                    }
                }
            }

            return dataTable; // Return the updated DataTable
        }


        public DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }



        public DataTable ListToDataTable(List<string> list)
        {
            DataTable dt = new DataTable();
            List<GridViewDbModel> _list = new List<GridViewDbModel>();
            GridViewDbModel model = new GridViewDbModel();
            List<object> llist  = null;
            // Add columns to DataTable
            dt.Columns.Add("Parameter", typeof(string));
            dt.Columns.Add("Battery1", typeof(string));
            dt.Columns.Add("Battery2", typeof(string));
            dt.Columns.Add("Battery3", typeof(string));
            dt.Columns.Add("Battery4", typeof(string));
            dt.Columns.Add("Battery5", typeof(string));
            dt.Columns.Add("Battery6", typeof(double));
            dt.Columns.Add("Battery7", typeof(double));
            dt.Columns.Add("Battery8", typeof(double));
            dt.Columns.Add("Battery9", typeof(double));
            dt.Columns.Add("Battery10", typeof(double));
            dt.Columns.Add("Battery11", typeof(double));
            dt.Columns.Add("Battery12", typeof(double));
            dt.Columns.Add("Battery13", typeof(double));
            dt.Columns.Add("Battery14", typeof(double));
            dt.Columns.Add("Battery15", typeof(double));
            dt.Columns.Add("Battery16", typeof(double));
            dt.Columns.Add("Battery17", typeof(double));
            dt.Columns.Add("Battery18", typeof(double));
            dt.Columns.Add("Battery19", typeof(double));
            dt.Columns.Add("Battery20", typeof(double));
            dt.Columns.Add("SlaveId1", typeof(int));
            dt.Columns.Add("SlaveId2", typeof(int));
            dt.Columns.Add("SlaveId3", typeof(int));
            dt.Columns.Add("SlaveId4", typeof(int));
            dt.Columns.Add("SlaveId5", typeof(int));
            dt.Columns.Add("SlaveId6", typeof(int));
            dt.Columns.Add("SlaveId7", typeof(int));
            dt.Columns.Add("SlaveId8", typeof(int));
            dt.Columns.Add("SlaveId9", typeof(int));
            dt.Columns.Add("SlaveId10", typeof(int));
            dt.Columns.Add("SlaveId11", typeof(int));
            dt.Columns.Add("SlaveId12", typeof(int));
            dt.Columns.Add("SlaveId13", typeof(int));
            dt.Columns.Add("SlaveId14", typeof(int));
            dt.Columns.Add("SlaveId15", typeof(int));
            dt.Columns.Add("SlaveId16", typeof(int));
            dt.Columns.Add("SlaveId17", typeof(int));
            dt.Columns.Add("SlaveId18", typeof(int));
            dt.Columns.Add("SlaveId19", typeof(int));
            dt.Columns.Add("SlaveId20", typeof(int));






            // Add rows from the list
            foreach (string item in list)
            {
                DataRow row = dt.NewRow();
                row["Parameter"] = item;
                // Other columns are left blank for each row
                dt.Rows.Add(item);
                model.Parameter = item;
                _list.Add(model);
            }
           
            return dt;
        }
    }
}
