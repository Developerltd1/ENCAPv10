using BusinessLogic.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BusinessLogic.Model.StaticModel.DashboardModel;

namespace BusinessLogic
{
    public class MainLogicClass
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainLogicClass));
        public static bool CheckConnection(out string statusMessage)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new DbConnection().connectionString))
                {
                    connection.Open(); // Attempt to open the connection
                    statusMessage = "Connected";
                    return true; // Connection is successful
                }
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
                statusMessage = "Disconnected";
                Logger.Info("MainLogicClass/CheckConnection Exception: " + ex.Message);
                return false; // Connection failed
            }
        }
        public static int usp_RecordCount()
        {
            int Count = 0;
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(new DbConnection().connectionString))
            {
                connection.Open();

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter("Select Count(*) From  tblStorePoints", connection))
                {
                    // Fill the DataTable with the results of the query
                    dataAdapter.Fill(dataTable);
                }
                Count = Convert.ToInt32(dataTable.Rows[0][0]);
            }

            return Count;
        }
        public void SaveDataToDatabase(DataTable dataList)//List<BatteryData> dataList)
        {
            try
            {
                DataTable FinalDataTable = null;
                FinalDataTable = FilterEmptyColumns(dataList);


                BulkInsertData(dataList);
                dataList.Clear();   // dataTable.Clear(); // Clear the DataTable after insertion


            }
            catch (Exception ex)
            {

                Console.WriteLine("An error occurred: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                throw new Exception("Exception From Database InsertionTime : " + ex.Message);
            }

        }
        private void BulkInsertData(DataTable dataTable)
        {
            using (SqlConnection connection = new SqlConnection(new DbConnection().connectionString))
            {
                connection.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "tblStorePoints";

                    // Iterate through each row in the DataTable
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Check and set null for Battery1 and SlaveId1 if Battery1 is empty
                        for (int batteryIndex = 1; batteryIndex <= 20; batteryIndex++)
                        {
                            string batteryColumnName = $"Battery{batteryIndex}";

                            if (string.IsNullOrEmpty(row[batteryColumnName]?.ToString()))
                            {
                                row[batteryColumnName] = DBNull.Value; // Assign null to Battery column
                            }
                        }
                    }

                    #region Working
                    bulkCopy.ColumnMappings.Add("Parameter", "Parameter");
                    for (int batteryIndex = 1; batteryIndex <= 20; batteryIndex++)
                    {
                        string batteryColumnName = $"Battery{batteryIndex}";
                        bulkCopy.ColumnMappings.Add(batteryColumnName, batteryColumnName);
                    }
                    #endregion
                    #region ItrateDashes

                    // Iterate through all rows in the DataTable
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Iterate through all columns in the current row
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            // Check if the value in the cell is "-"
                            if (row[i] != DBNull.Value && row[i].ToString() == "-")
                            {
                                // Replace "-" with DBNull.Value (which represents NULL in the database)
                                row[i] = DBNull.Value;
                            }
                        }
                    }


                    #endregion
                    try
                    {
                        bulkCopy.WriteToServer(dataTable);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Exception From Bulk Database: " + ex.Message);
                        Console.WriteLine("Bulk insert failed:" + ex.Message);
                    }
                }
            }
        }
        public static void usp_Insert(DataTable dt, out string msg)
        {
            DataTable dt1 = new DataTable();
            msg = null;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlTransaction transaction = null;

            try
            {
                //  string ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename = " + System.IO.Path.GetFullPath("ENCAPdb.mdf") + "; Integrated Security = True";  //System.Configuration.ConfigurationManager.ConnectionStrings["DbConnectionString"].ToString();


                conn = new SqlConnection(new DbConnection().connectionString);
                conn.Open();
                transaction = conn.BeginTransaction();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmd = new SqlCommand("usp_InsertRecord", conn, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the current item in the loop
                    cmd.Parameters.AddWithValue("@Parameter", dt.Rows[0]["Parameter"].ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Battery1", dt.Rows[1]["Battery-1"].ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Battery2", dt.Rows[2]["Battery-2"].ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Battery3", dt.Rows[3]["Battery-3"].ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Battery4", dt.Rows[4]["Battery-4"].ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Battery5", dt.Rows[5]["Battery-5"].ToString() ?? "");

                    // Execute the command
                    int isInserted = cmd.ExecuteNonQuery();

                    if (isInserted > 0)
                    {
                        msg = "Saved Successfully";
                    }
                    else
                    {
                        msg = "Not Saved";
                    }

                    // Dispose command to clear parameters for the next iteration
                    cmd.Dispose();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                transaction?.Rollback();
                return;
            }
            finally
            {
                // Clean up resources
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Dispose();

                if (cmd != null)
                    cmd.Dispose();
            }
        }
        public async Task<List<ChartModel>> GetStorePoints(DateTime startDate, DateTime endDate, bool includeVoltage, bool includeCurrents, bool includePower, bool includeSOC, bool includeTemp, bool selectAll)
        {
            List<ChartModel> allList = new List<ChartModel>();
            using (SqlConnection connection = new SqlConnection(new DbConnection().connectionString))
            {
                using (SqlCommand command = new SqlCommand("usp_MainParametersCharts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@IncludeVoltage", includeVoltage ? 1 : 0);
                    command.Parameters.AddWithValue("@IncludeCurrents", includeCurrents ? 1 : 0);
                    command.Parameters.AddWithValue("@IncludePower", includePower ? 1 : 0);
                    command.Parameters.AddWithValue("@IncludeSOC", includeSOC ? 1 : 0);
                    command.Parameters.AddWithValue("@IncludeTemp", includeTemp ? 1 : 0);
                    command.Parameters.AddWithValue("@SelectAll", selectAll ? 1 : 0);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        try
                        {
                            DataTable originalTable = new DataTable();
                            await connection.OpenAsync();
                            await Task.Run(() => adapter.Fill(originalTable));

                            #region AssignToList
                            // Read data dynamically, checking if the column exists in the DataTable
                            foreach (DataRow row in originalTable.Rows)
                            {
                                var modelChart = new ChartModel();

                                if (originalTable.Columns.Contains("Voltage"))
                                    modelChart.Voltage = row["Voltage"] != DBNull.Value ? (double?)Convert.ToDouble(row["Voltage"]) : null;

                                if (originalTable.Columns.Contains("Currents"))
                                    modelChart.Currents = row["Currents"] != DBNull.Value ? (double?)Convert.ToDouble(row["Currents"]) : null;

                                if (originalTable.Columns.Contains("Power"))
                                    modelChart.Power = row["Power"] != DBNull.Value ? (double?)Convert.ToDouble(row["Power"]) : null;

                                if (originalTable.Columns.Contains("SOC"))
                                    modelChart.SOC = row["SOC"] != DBNull.Value ? (double?)Convert.ToDouble(row["SOC"]) : null;

                                if (originalTable.Columns.Contains("Temp"))
                                    modelChart.Temp = row["Temp"] != DBNull.Value ? (double?)Convert.ToDouble(row["Temp"]) : null;

                                if (originalTable.Columns.Contains("DateCheck"))
                                    modelChart.TimeStamp = row["DateCheck"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["DateCheck"]) : null;

                                allList.Add(modelChart);
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Exception from database when retrieving report list: " + ex.Message);
                        }
                    }
                }
            }
            return allList;
        }


        public async Task<DataTable> GetAlarmDataFrmDb(DateTime startDate, DateTime endDate)
        {
            List<AlarmModel> allList = new List<AlarmModel>();
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(new DbConnection().connectionString))
            {
                using (SqlCommand command = new SqlCommand("usp_GetAlarmDataFrmDb", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        var s = startDate.ToString();
                        try
                        {
                            DataSet ds = new DataSet();
                            await connection.OpenAsync();
                            await Task.Run(() => adapter.Fill(ds));
                            dataTable = ds.Tables[0];
                            #region AssignToList
                            //foreach (DataRow row in ds.Tables[0].Rows)
                            //{
                            //    var modelChart = new AlarmModel
                            //    {
                            //        BatteryId = row["BatteryId"] != DBNull.Value ? (Int32?)Convert.ToInt32(row["BatteryId"]) : null,
                            //        Alarm = row["Alarm"] != DBNull.Value ? (string?)Convert.ToString(row["Alarm"]) : null,
                            //        OccurrenceTime = row["OccurrenceTime"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["OccurrenceTime"]) : null
                            //        TimeStamp = row["TimeStamp"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["TimeStamp"]) : null
                            //    };
                            //    allList.Add(modelChart);
                            //}
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Exception From Database When Report List isRetriving Record: " + ex.Message);
                        }
                    }
                }
            }
            return dataTable;
        }
        public async Task<DataTable> GetDataForCSV(DateTime startDate, DateTime endDate, CheckBox cbVoltage, CheckBox cbCurrent, CheckBox cbPower, CheckBox cbSOC,  CheckBox cbTemp, CheckBox cbSelectAll)
        {
            DataTable transposedDataTable = null;
            DataTable mergeDataTable = null;
            using (SqlConnection connection = new SqlConnection(new DbConnection().connectionString))
            {
                using (SqlCommand command = new SqlCommand("usp_GetRdlcReportData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the SqlCommand
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@SelectAll", cbSelectAll.Checked ? 1 : 0);
                    command.Parameters.AddWithValue("@IncludeSOC", cbSOC.Checked ? 1 : 0);
                    command.Parameters.AddWithValue("@IncludeVoltage", cbVoltage.Checked ? 1 : 0);
                    command.Parameters.AddWithValue("@IncludeCurrent", cbCurrent.Checked ? 1 : 0);

                    // Create a SqlDataAdapter to fill the DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        var s = startDate.ToString();
                        try
                        {
                            // Open the connection
                            // connection.Open();//old
                            await connection.OpenAsync();
                            DataTable originalTable = new DataTable();
                            adapter.Fill(originalTable);
                           
                            #region Transpose
                            // Assuming you have an existing DataTable called "originalDataTable"
                            transposedDataTable = new DataTable();
                            // Step 1: Add DateCheck column as the first header
                            transposedDataTable.Columns.Add("DateCheck", typeof(DateTime));
                            transposedDataTable.Columns.Add("SlaveId", typeof(int));
                            int z = 1;
                            // Step 2: Add columns with Parameter values as headers
                            foreach (DataRow row in originalTable.Rows)
                            {
                                string parameter = row["Parameter"].ToString();
                                //if (parameter == "Serial")
                                //{
                                //}
                                //else
                                //{
                                    if (!transposedDataTable.Columns.Contains(parameter)) // Check if column already exists
                                    {
                                        transposedDataTable.Columns.Add(parameter);
                                    }
                                //}
                            }
                            // Assign "Battery1" value to the new row
                            foreach (DataRow row in originalTable.Rows) // Iterate over each row
                            {
                                foreach (DataColumn column in originalTable.Columns) // Iterate over each column in the row
                                {
                                    if (column.ColumnName != "Parameter" && column.ColumnName != "DateCheck")
                                    {
                                        int columnIndex = originalTable.Columns.IndexOf(column); // Get the index of the current column
                                        columnIndex = columnIndex - 2;
                                        columnIndex++;
                                        var value = row[column];
                                        if (value != null && !string.IsNullOrEmpty(value.ToString()) && value.ToString() != "{}")
                                        {
                                            DataRow newRow = transposedDataTable.NewRow();
                                            newRow["DateCheck"] = row["DateCheck"];
                                            newRow["SlaveId"] = columnIndex;
                                            string parameterName = row["Parameter"].ToString();
                                            if (parameterName == "SOC" && transposedDataTable.Columns.Contains("SOC"))
                                            {
                                                newRow["SOC"] = value;
                                            }
                                            else if (parameterName == "Power (kW)" && transposedDataTable.Columns.Contains("Power (kW)"))
                                            {
                                                newRow["Power (kW)"] = value;
                                            }
                                            else if (parameterName == "Voltage (V)" && transposedDataTable.Columns.Contains("Voltage (V)"))
                                            {
                                                newRow["Voltage (V)"] = value;
                                            }
                                            else if (parameterName == "Current (Amps)" && transposedDataTable.Columns.Contains("Current (Amps)"))
                                            {
                                                newRow["Current (Amps)"] = value;
                                            }
                                            else if (parameterName == "Total Remaining Capacity(Ah)" && transposedDataTable.Columns.Contains("Total Remaining Capacity(Ah)"))
                                            {
                                                newRow["Total Remaining Capacity(Ah)"] = value;
                                            }
                                            else if (parameterName == "Temperature (C)" && transposedDataTable.Columns.Contains("Temperature (C)"))
                                            {
                                                newRow["Temperature (C)"] = value;
                                            }
                                            else if (parameterName == "Cell-1 (V)" && transposedDataTable.Columns.Contains("Cell-1 (V)"))
                                            {
                                                newRow["Cell-1 (V)"] = value;
                                            }
                                            else if (parameterName == "Cell-2 (V)" && transposedDataTable.Columns.Contains("Cell-2 (V)"))
                                            {
                                                newRow["Cell-2 (V)"] = value;
                                            }
                                            else if (parameterName == "Cell-3 (V)" && transposedDataTable.Columns.Contains("Cell-3 (V)"))
                                            {
                                                newRow["Cell-3 (V)"] = value;
                                            }
                                            else if (parameterName == "Cell-4 (V)" && transposedDataTable.Columns.Contains("Cell-4 (V)"))
                                            {
                                                newRow["Cell-4 (V)"] = value;
                                            }
                                            else if (parameterName == "Cell-5 (V)" && transposedDataTable.Columns.Contains("Cell-5 (V)"))
                                            {
                                                newRow["Cell-5 (V)"] = value;
                                            }
                                            else if (parameterName == "Cell-6 (V)" && transposedDataTable.Columns.Contains("Cell-6 (V)"))
                                            {
                                                newRow["Cell-6 (V)"] = value;
                                            }
                                            else if (parameterName == "Cell-7 (V)" && transposedDataTable.Columns.Contains("Cell-7 (V)"))
                                            {
                                                newRow["Cell-7 (V)"] = value;
                                            }
                                            else if (parameterName == "Cell-8 (V)" && transposedDataTable.Columns.Contains("Cell-8 (V)"))
                                            {
                                                newRow["Cell-8 (V)"] = value;
                                            }
                                            else if (parameterName == "Cell-9 (V)" && transposedDataTable.Columns.Contains("Cell-9 (V)"))
                                            {
                                                newRow["Cell-9 (V)"] = value;
                                            }
                                            else if (parameterName == "Cell-10 (V)" && transposedDataTable.Columns.Contains("Cell-10 (V)"))
                                            {
                                                newRow["Cell-10 (V)"] = value;
                                            }
                                            else if (parameterName == "Cell-11 (V)" && transposedDataTable.Columns.Contains("Cell-11 (V)"))
                                            {
                                                newRow["Cell-11 (V)"] = value;
                                            }
                                            else if (parameterName == "Cell-12 (V)" && transposedDataTable.Columns.Contains("Cell-12 (V)"))
                                            {
                                                newRow["Cell-12 (V)"] = value;
                                            }
                                            else if (parameterName == "Cell-13 (V)" && transposedDataTable.Columns.Contains("Cell-13 (V)"))
                                            {
                                                newRow["Cell-13 (V)"] = value;
                                            }
                                            else if (parameterName == "Cell-14 (V)" && transposedDataTable.Columns.Contains("Cell-14 (V)"))
                                            {
                                                newRow["Cell-14 (V)"] = value;
                                            }
                                            else if (parameterName == "Cell-15 (V)" && transposedDataTable.Columns.Contains("Cell-15 (V)"))
                                            {
                                                newRow["Cell-15 (V)"] = value;
                                            }

                                            transposedDataTable.Rows.Add(newRow);

                                        }
                                    }
                                }

                            }
                            #endregion
                            #region Merge
                            mergeDataTable = MergeDataTable(transposedDataTable);
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Exception From Database When CSV Report Retriving Record: " + ex.Message);
                        }
                    }
                }
            }
            return mergeDataTable;
        }
        DataTable MergeDataTable(DataTable originalTable)
        {

            DataTable transposedDataTable = originalTable.Clone();

            // Step 2: Initialize a Dictionary to track combined rows
            var combinedRows = new Dictionary<string, DataRow>();

            // Step 3: Iterate through the original table
            foreach (DataRow row in originalTable.Rows)
            {
                // Get DateCheck and SlaveId values
                var dateCheck = row["DateCheck"];
                var slaveId = row["SlaveId"];

                // Skip rows with null DateCheck or SlaveId
                if (dateCheck == DBNull.Value || slaveId == DBNull.Value)
                    continue;

                // Create a unique key based on DateCheck and SlaveId
                string key = $"{dateCheck}_{slaveId}";

                // Check if the key already exists in the dictionary
                DataRow newRow;
                if (combinedRows.ContainsKey(key))
                {
                    newRow = combinedRows[key]; // Use the existing row
                }
                else
                {
                    newRow = transposedDataTable.NewRow(); // Create a new row
                    newRow["DateCheck"] = dateCheck;
                    newRow["SlaveId"] = slaveId;
                    combinedRows[key] = newRow;
                    transposedDataTable.Rows.Add(newRow);
                }

                // Iterate through each column and assign values
                foreach (DataColumn column in originalTable.Columns)
                {
                    string columnName = column.ColumnName;
                    var value = row[columnName];

                    if (value == DBNull.Value)
                        continue; // Skip null values

                    // Assign values to the corresponding columns
                    if (columnName == "SOC" && transposedDataTable.Columns.Contains("SOC"))
                    {
                        newRow["SOC"] = value;
                    }
                    else if (columnName == "Power (kW)" && transposedDataTable.Columns.Contains("Power (kW)"))
                    {
                        newRow["Power (kW)"] = value;
                    }
                    else if (columnName == "Voltage (V)" && transposedDataTable.Columns.Contains("Voltage (V)"))
                    {
                        newRow["Voltage (V)"] = value;
                    }
                    else if (columnName == "Current (Amps)" && transposedDataTable.Columns.Contains("Current (Amps)"))
                    {
                        newRow["Current (Amps)"] = value;
                    }
                    else if (columnName == "Temperature (C)" && transposedDataTable.Columns.Contains("Temperature (C)"))
                    {
                        newRow["Temperature (C)"] = value;
                    }
                    else if (columnName == "Total Remaining Capacity(Ah)" && transposedDataTable.Columns.Contains("Total Remaining Capacity(Ah)"))
                    {
                        newRow["Total Remaining Capacity(Ah)"] = value;
                    }
                    else if (columnName.StartsWith("Cell-") && transposedDataTable.Columns.Contains(columnName))
                    {
                        newRow[columnName] = value;
                    }
                }
            }
            return transposedDataTable;
        }
        public DataTable GetRdlcReportData(DateTime startDate, DateTime endDate, CheckBox cbSelectAll, CheckBox cbActivePower, CheckBox cbSOC, CheckBox cbPowerFactor, CheckBox cbVoltage, CheckBox cbCurrent)
        {
            try
            {


                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection(new DbConnection().connectionString))
                {
                    using (SqlCommand command = new SqlCommand("usp_GetRdlcReportData", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        command.Parameters.AddWithValue("@SelectAll", cbSelectAll.Checked ? 1 : 0);
                        command.Parameters.AddWithValue("@IncludeTotalRemainingCapacity", cbActivePower.Checked ? 1 : 0);
                        command.Parameters.AddWithValue("@IncludeSOC", cbSOC.Checked ? 1 : 0);
                        command.Parameters.AddWithValue("@IncludePower", cbPowerFactor.Checked ? 1 : 0);  //cbPowerFactor
                        command.Parameters.AddWithValue("@IncludeVoltage", cbVoltage.Checked ? 1 : 0);
                        command.Parameters.AddWithValue("@IncludeCurrent", cbCurrent.Checked ? 1 : 0);



                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                dataTable = FilterEmptyColumns(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception From Report Data Retive Time : " + ex.Message);
            }
        }
        public DataTable FilterEmptyColumns(DataTable originalTable)
        {
            // Create a new DataTable to hold the filtered data
            DataTable filteredTable = new DataTable();

            // Create a DataView to access the data
            DataView view = new DataView(originalTable);

            // Iterate through each column in the original DataTable
            foreach (DataColumn column in originalTable.Columns)
            {
                bool hasData = false;

                // Check each row in the column
                foreach (DataRowView rowView in view)
                {
                    string value = rowView[column.ColumnName]?.ToString();

                    if (!string.IsNullOrEmpty(value))
                    {
                        hasData = true;
                        break;
                    }
                }

                // If the column has non-empty data, add it to the filtered DataTable
                if (hasData)
                {
                    filteredTable.Columns.Add(column.ColumnName, column.DataType);
                }
            }

            // Add rows to the filtered DataTable
            foreach (DataRow row in originalTable.Rows)
            {
                DataRow newRow = filteredTable.NewRow();

                foreach (DataColumn column in filteredTable.Columns)
                {
                    newRow[column.ColumnName] = row[column.ColumnName];
                }

                filteredTable.Rows.Add(newRow);
            }

            return filteredTable;
        }

        public void SaveMainParamatersToDatabase(decimal? Voltage, decimal? Current, decimal? Power, decimal? SOC, decimal? Temprature)
        {
            try
            {
                string query = "INSERT INTO tblMainParameters (Voltage, Currents, Power, SOC, Temp) VALUES (@Voltage, @Current, @Power, @SOC, @Temperature)";
                using (SqlConnection connection = new SqlConnection(new DbConnection().connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Assign DBNull.Value if the parameter is null
                        cmd.Parameters.AddWithValue("@Voltage", Voltage.HasValue ? (object)Voltage.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Current", Current.HasValue ? (object)Current.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Power", Power.HasValue ? (object)Power.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@SOC", SOC.HasValue ? (object)SOC.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Temperature", Temprature.HasValue ? (object)Temprature.Value : DBNull.Value);


                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception From Database InsertionTime MainParamaters: " + ex.Message);
            }
        }
        public void SaveAlarmGridToDatabase(DataTable dataTableAlarm)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new DbConnection().connectionString))
                {
                    connection.Open();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        // Set the destination table name
                        bulkCopy.DestinationTableName = "tblAlarm";

                        // Map DataTable columns to database columns if column names are different
                        bulkCopy.ColumnMappings.Add("ID", "BatteryId");
                        bulkCopy.ColumnMappings.Add("Alarm", "Alarm");
                        bulkCopy.ColumnMappings.Add("OccurrenceTime", "OccurrenceTime");

                        // Write from the DataTable to the SQL Server
                        bulkCopy.WriteToServer(dataTableAlarm);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception From Database InsertionTime MainParamaters: " + ex.Message);
            }
        }



    }
}
