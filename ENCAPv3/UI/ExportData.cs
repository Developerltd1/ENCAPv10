using BusinessLogic;
using BusinessLogic.Model;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Maps;
using LiveCharts.Wpf;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using CheckBox = System.Windows.Forms.CheckBox;

namespace ENCAPv3.UI
{
    public partial class ExportData : BaseForm//Form
    {
        public static List<List<StorePoint>> listForCSV = new List<List<StorePoint>>();
        private SeriesCollection seriesCollection; // Collection to hold all series
        private List<LineSeries> allSeries; // List to hold individual LineSeries
        public ExportData()
        {
            InitializeComponent();
        }



        public async  void ExportData_Load(object sender, EventArgs e)
        {
            try
            {
                datePickerStartDate.Value = DateTime.Now.AddHours(-1);
                datePickerEndDate.Value = DateTime.Now;
               await StartEndDateReport(datePickerStartDate.Value, datePickerEndDate.Value);
                InitializeChart();

                new ChartSet().ExportData_Clear(chartExportData, cbSelectAll, cbSOC, cbPower, cbVoltage, cbCurrent);
                cbSelectAll.Checked = true;
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }
        }



        

        #region Chart
        private async void ChkBox_CheckedChanged(object sender, EventArgs e) //SelectSingle
        {
            DateTime startDate = datePickerStartDate.Value;
            DateTime endDate = datePickerEndDate.Value;
            await StartEndDateReport(startDate, endDate);
        }

        //InitlizeChart First Time
        private void InitializeChart()
        {
            try
            {
                // Initialize series collection
                seriesCollection = new SeriesCollection();
                allSeries = new List<LineSeries>();

                // Set legend text color and background color
                chartExportData.ForeColor = System.Drawing.Color.White;// White text color
                chartExportData.LegendLocation = LiveCharts.LegendLocation.Top;
                // Configure chart
                chartExportData.AxisX.Add(new LiveCharts.Wpf.Axis
                {
                    Title = "Date Time",
                    LabelFormatter = value => new DateTime((long)value).ToString("yy-MM-dd HH:mm")
                });
                chartExportData.AxisY.Add(new LiveCharts.Wpf.Axis
                {
                    Title = "Values"
                });
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }
        }
      

        #endregion
        #region Export CSV

        private void btnDownloadRawData_Click(object sender, EventArgs e)
        {
            new ExportDataPartial().WriteToCsv(listForCSV);
        }
        #endregion

        private void datePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            //DateTime startDate = datePickerStartDate.Value;
            //string ss = datePickerEndDate.Value.ToShortDateString().ToString();
            //if (ss == "7/14/2024" || ss == "14/7/2024")
            //{
            //    datePickerEndDate.Value = DateTime.Now;
            //}
            //DateTime endDate = datePickerEndDate.Value;
            //StartEndDateReport(startDate, endDate);
        }
        private void datePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime startDate = datePickerStartDate.Value;
            DateTime endDate = datePickerEndDate.Value;
            StartEndDateReport(startDate, endDate);
        }

        private async Task StartEndDateReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                {
                    JIMessageBox.InformationMessage("EndDate Must be less then StartDate");
                    return;
                }
                //Get frm Db
                List<ChartModel> LstChartRecordFromDb = new List<ChartModel>();

                LstChartRecordFromDb = await new MainLogicClass().GetStorePoints(startDate, endDate, cbVoltage.Checked,cbCurrent.Checked, cbPower.Checked, cbSOC.Checked, cbTemprature.Checked, cbSelectAll.Checked); //Get From DB

                //  listForCSV = allList;//hassanCode
                #region ChartList
                 List<LiveCharts.ChartValues<double>> allLists = new List<LiveCharts.ChartValues<double>>();
                ChartValues<double> voltageList = new ChartValues<double>();
                ChartValues<double> currentList = new ChartValues<double>();
                ChartValues<double> powerList = new ChartValues<double>();
                ChartValues<double> socList = new ChartValues<double>();
                ChartValues<double> tempList = new ChartValues<double>();

                if (LstChartRecordFromDb.Count > 0)
                {
                    foreach (var item in LstChartRecordFromDb)
                    {
                        voltageList.Add(item.Voltage ?? 0);  // Default to 0 if null
                        currentList.Add(item.Currents ?? 0);
                        powerList.Add(item.Power ?? 0);
                        socList.Add(item.SOC ?? 0);
                        tempList.Add(item.Temp ?? 0);
                    }

                    // Create the main list that holds all data series
                    allLists.Add(voltageList);  // Add Voltage series
                    allLists.Add(currentList);  // Add Currents series
                    allLists.Add(powerList);    // Add Power series
                    allLists.Add(socList);      // Add SOC series
                    allLists.Add(tempList);     // Add Temperature series
                                                // Define X-axis labels (dates)

                    List<string> xAxisLabels = LstChartRecordFromDb
                        .Where(r => r.TimeStamp.HasValue)
                        .Select(r => r.TimeStamp.Value.ToString("dd/MM/yyyy HH:mm:ss"))
                        .ToList();
                    new ChartSet().chartGT3(allLists, chartExportData, xAxisLabels);
                }
                else
                {
                    List<string> xAxisLabels = LstChartRecordFromDb
                        .Where(r => r.TimeStamp.HasValue)
                        .Select(r => r.TimeStamp.Value.ToString("dd/MM/yyyy HH:mm:ss"))
                        .ToList();
                    new ChartSet().chartGT3(allLists, chartExportData, xAxisLabels);
                }
                #endregion
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            DateTime startDate = datePickerStartDate.Value;
            DateTime endDate = datePickerEndDate.Value;
            StartEndDateReport(startDate, endDate);
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {

        }
        
        private async void btnExportCSV_Click(object sender, EventArgs e)
        {
            btnExportCSV.Enabled = false; // Disable button to prevent multiple clicks
            try
            {
                if (cbVoltage.Checked == false && cbCurrent.Checked == false &&  cbPower.Checked == false && cbSOC.Checked == false && cbTemprature.Checked == false && cbSelectAll.Checked == false)
                {
                    return;
                }
                DataTable dt = new DataTable();
                dt = await new MainLogicClass().GetDataForCSV(datePickerStartDate.Value, datePickerEndDate.Value, cbVoltage, cbCurrent, cbPower, cbSOC, cbTemprature, cbSelectAll);
                if (dt.Rows.Count > 0)
                {
                    ExportToCsv(dt, datePickerStartDate.Value, datePickerEndDate.Value);
                }
                else
                {
                    JIMessageBox.WarningMessage("No Record Found");
                }
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }
            finally
            {
                btnExportCSV.Enabled = true; // Re-enable button after operation
            }
        }

        public async Task ExportToCsv(DataTable dataTable, DateTime startDate, DateTime endDate)
        {
            // Initialize SaveFileDialog
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Set the default filename to the current date and time
                saveFileDialog.FileName = startDate.ToString("yyyy-MM-dd HH-mm-ss tt") + "__" + endDate.ToString("yyyy-MM-dd HH-mm-ss tt") + ".csv";
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.Title = "Save CSV File";

                // Show the dialog and get the result
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the file path from the dialog
                    string filePath = saveFileDialog.FileName;

                    // Export the DataTable to the selected file path
                    using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                    {
                        // Write the headers
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            await writer.WriteAsync(dataTable.Columns[i].ColumnName);
                            if (i < dataTable.Columns.Count - 1)
                                await writer.WriteAsync(",");
                        }
                        await writer.WriteLineAsync();

                        // Write the data rows
                        foreach (DataRow row in dataTable.Rows)
                        {
                            for (int i = 0; i < dataTable.Columns.Count; i++)
                            {
                                writer.Write(row[i].ToString());
                                if (i < dataTable.Columns.Count - 1)
                                    await writer.WriteAsync(",");
                            }
                            await writer.WriteLineAsync();
                        }
                    }

                    MessageBox.Show("Data exported successfully to " + filePath, "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public async Task ExportToAlarmCsv(DataTable dataTable, DateTime startDate, DateTime endDate)
        {
            // Initialize SaveFileDialog
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Set the default filename to the current date and time
                saveFileDialog.FileName = "AlarmReport__"+startDate.ToString("yyyy-MM-dd HH-mm-ss tt") + "__" + endDate.ToString("yyyy-MM-dd HH-mm-ss tt") + ".csv";
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.Title = "Save CSV File";

                // Show the dialog and get the result
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the file path from the dialog
                    string filePath = saveFileDialog.FileName;

                    // Export the DataTable to the selected file path
                    using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                    {
                        // Write the headers
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            await writer.WriteAsync(dataTable.Columns[i].ColumnName);
                            if (i < dataTable.Columns.Count - 1)
                                await writer.WriteAsync(",");
                        }
                        await writer.WriteLineAsync();

                        // Write the data rows
                        foreach (DataRow row in dataTable.Rows)
                        {
                            for (int i = 0; i < dataTable.Columns.Count; i++)
                            {
                                writer.Write(row[i].ToString());
                                if (i < dataTable.Columns.Count - 1)
                                    await writer.WriteAsync(",");
                            }
                            await writer.WriteLineAsync();
                        }
                    }

                    MessageBox.Show("Alarm Data exported successfully to " + filePath, "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void btnAlarmReport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = await new MainLogicClass().GetAlarmDataFrmDb(datePickerStartDate.Value, datePickerEndDate.Value);
            if (dt.Rows.Count > 0)
            {
                ExportToAlarmCsv(dt, datePickerStartDate.Value, datePickerEndDate.Value);
            }
            else
            {
                JIMessageBox.WarningMessage("No Record Found");
            }
        }
    }
}
