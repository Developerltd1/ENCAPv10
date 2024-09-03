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



        private async  void ExportData_Load(object sender, EventArgs e)
        {
            try
            {
                datePickerStartDate.Value = DateTime.Now.AddHours(-1);
                datePickerEndDate.Value = DateTime.Now;
               await StartEndDateReport(datePickerStartDate.Value, datePickerEndDate.Value);
                InitializeChart();

                new ChartSet().ExportData_Clear(chartExportData, cbSelectAll, cbActivePower, cbSOC, cbPowerFactor, cbVoltage, cbCurrent);//, cbTemprature);
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
                List<List<StorePoint>> allList = new List<List<StorePoint>>();
                allList = await new MainLogicClass().GetStorePoints(startDate, endDate); //Get From DB
                                                                                   // DataTable dt1 = new MainLogicClass().Transpose(dt);  //Column to Row
                if (cbSelectAll.Checked)
                {
                }
                else
                {
                    if (cbVoltage.Checked == false)
                    {
                        allList[0] = new List<StorePoint>();

                    }
                    if (cbCurrent.Checked == false)
                    {
                        allList[1] = new List<StorePoint>();
                    }
                    if (cbPowerFactor.Checked == false)
                    {
                        allList[2] = new List<StorePoint>();
                    }
                    if (cbSOC.Checked == false)
                    {
                        allList[3] = new List<StorePoint>();
                    }
                    if (cbActivePower.Checked == false)   // Total Remaining Capacity(Ah)
                    {
                        allList[4] = new List<StorePoint>();
                    }
                    if (cbTemprature.Checked == false)
                    {
                        allList[5] = new List<StorePoint>();
                    }
                    if (allList[0].Count == 0 && allList[1].Count == 0 && allList[2].Count == 0 && allList[3].Count == 0 && allList[4].Count == 0 && allList[5].Count == 0)
                    {
                        allList = new List<List<StorePoint>>();
                        allList.Clear();
                    }

                }
                listForCSV = allList;//CSVList
                #region ChartList

                // Convert allList to allLists
                List<LiveCharts.ChartValues<double>> allLists = new List<LiveCharts.ChartValues<double>>();
                if (allList.Count > 0)
                {
                    foreach (var list in allList)
                    {
                        ChartValues<double> chartValues = new ChartValues<double>();

                        foreach (var item in list)
                        {
                            chartValues.Add((double)item.Battery1.GetValueOrDefault());
                        }
                        allLists.Add(chartValues);
                    }
                    new ChartSet().chartGT3(allLists, chartExportData, allList);

                }
                Console.WriteLine("Voltage (V) Data:");
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
            DataTable dt = new MainLogicClass().GetRdlcReportData(datePickerStartDate.Value, datePickerEndDate.Value, cbSelectAll, cbSOC, cbPowerFactor, cbVoltage,
            cbCurrent, cbActivePower);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "Reports/Report1.rdlc";
            if (dt.Rows.Count <= 0)
            {
                JIMessageBox.WarningMessage("No Record Found");
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.RefreshReport();
                return;
            }

            ReportDataSource RdsAll = new ReportDataSource("DataSetAll", dt);
            reportViewer1.LocalReport.DataSources.Add(RdsAll);
            reportViewer1.RefreshReport();


        }
        public async Task reportFn1()
        {
            try
            {
                //var lst = ExportData.listForCSV;
                var lst = await new MainLogicClass().GetStorePoints(datePickerStartDate.Value, datePickerEndDate.Value); //Get From DB

                if (cbSelectAll.Checked)
                {
                }
                else
                {
                    if (cbVoltage.Checked == false)
                    {
                        lst[0] = new List<StorePoint>();

                    }
                    if (cbCurrent.Checked == false)
                    {
                        lst[1] = new List<StorePoint>();
                    }
                    if (cbPowerFactor.Checked == false)
                    {
                        lst[2] = new List<StorePoint>();
                    }
                    if (cbSOC.Checked == false)
                    {
                        lst[3] = new List<StorePoint>();
                    }
                    if (cbActivePower.Checked == false)   // Total Remaining Capacity(Ah)
                    {
                        lst[4] = new List<StorePoint>();
                    }
                    //if (lst[0].Count == 0 && lst[1].Count == 0 && lst[2].Count == 0 && lst[3].Count == 0 && lst[4].Count == 0 && lst[5].Count == 0)
                    {
                        lst = new List<List<StorePoint>>();
                        lst.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }
        }

        private async void btnExportCSV_Click(object sender, EventArgs e)
        {
            btnExportCSV.Enabled = false; // Disable button to prevent multiple clicks
            try
            {
                DataTable dt = new DataTable();
                dt = await new MainLogicClass().GetDataForCSV(datePickerStartDate.Value, datePickerEndDate.Value, cbSelectAll, cbSOC, cbPowerFactor, cbVoltage, cbCurrent, cbActivePower);
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
    }
}
