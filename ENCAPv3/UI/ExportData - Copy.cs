using BusinessLogic;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Maps;
using LiveCharts.Wpf;
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
using System.Windows.Forms;

namespace ENCAPv3.UI
{
    public partial class ExportData : BaseForm//Form
    {
        private SeriesCollection seriesCollection; // Collection to hold all series
        private List<LineSeries> allSeries; // List to hold individual LineSeries
        private string csvFilePath; // Path to save CSV file
        public ExportData()
        {
            InitializeComponent();
        }

        private void ExportData_Load(object sender, EventArgs e)
        {

            InitializeChart();
            InitializeCheckBoxes();
        }

        //SeriesCollection
        private void GenerateDataSeries(DateTime fromDate, DateTime toDate, LiveCharts.WinForms.CartesianChart chart)
        {

            seriesCollection = new SeriesCollection();
            chart.Series.Clear();
            // Example data generation for two series
            Random random = new Random();

            // Generate Energy series data
            LineSeries energySeries = new LineSeries
            {
                Title = "Energy",
                PointGeometrySize = 15,
                Values = new ChartValues<ObservablePoint>()
            };

            // Generate Power series data
            LineSeries powerSeries = new LineSeries
            {
                Title = "Power",
                PointGeometrySize = 15,
                Values = new ChartValues<ObservablePoint>()
            };

            // Generate data points for each day within the range
            DateTime currentDate = fromDate;
            while (currentDate <= toDate)
            {
                // Simulated data points, replace with your actual data retrieval logic
                double energyValue = random.Next(1, 10);
                double powerValue = random.Next(1, 10);

                // Add data points to series
                energySeries.Values.Add(new ObservablePoint(currentDate.Ticks, energyValue));
                powerSeries.Values.Add(new ObservablePoint(currentDate.Ticks, powerValue));

                // Move to next day
                currentDate = currentDate.AddDays(1);
            }

            // Add series to collection
            seriesCollection.Add(energySeries);
            seriesCollection.Add(powerSeries);

            #region AdditionalCode
            // Setup CartesianChart
            chart.Series = seriesCollection;

            // Configure X axis
            chart.AxisX.Add(new Axis
            {
                Title = "DateTime",
                LabelFormatter = value => new DateTime((long)value).ToString("MM/dd HH:mm")
            });

            // Configure Y axis
            chart.AxisY.Add(new Axis
            {
                Title = "Values",
                LabelFormatter = value => value.ToString()
            });

            // Refresh chart
            chart.Refresh();

            #endregion

            // return seriesCollection;
        }

        private void datePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime startDate = datePickerStartDate.Value;
            DateTime endDate = datePickerEndDate.Value;

          DataTable dt = new MainLogicClass().GetStorePoints(startDate, endDate);
            UpdateChartSeries(startDate, endDate);
        }

        #region Chart
        private void ChkBox_CheckedChanged(object sender, EventArgs e) //SelectSingle
        {
            UpdateChartSeries(DateTime.Now, DateTime.Now);
        }
        // Event handler for Select All checkbox change
        private void ChkSelectAll_CheckedChanged(object sender, EventArgs e)  //SelectAll
        {
            bool isChecked = ((CheckBox)sender).Checked;
            foreach (Control control in panel2.Controls)
            {
                if (control is CheckBox chkBox && chkBox.Name != "cbSelectAll")
                {
                    chkBox.Checked = isChecked;
                }
            }
            UpdateChartSeries(DateTime.Now, DateTime.Now);
        }
        //InitlizeChart First Time
        private void InitializeChart()
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

            // Initialize checkboxes dynamically
            InitializeCheckBoxes();
        }
        //InitlizeCheckBox
        private void InitializeCheckBoxes()
        {
            // Initialize CheckBoxes for each series
            Dictionary<string, Func<ChartValues<ObservablePoint>>> seriesData = new Dictionary<string, Func<ChartValues<ObservablePoint>>>
            {
                { "Energy", GenerateEnergyData },
                { "Power", GeneratePowerData },
                { "Apparent Power", GenerateApparentPowerData },
                { "Power Factor", GeneratePowerFactorData },
                { "Voltage", GenerateVoltageData },
                { "Current", GenerateCurrentData },
                { "Frequency", GenerateFrequencyData }
            };

            foreach (var kvp in seriesData)
            {
                CheckBox chkBox = new CheckBox
                {
                    Text = kvp.Key,
                    Tag = kvp.Value
                };
                chkBox.CheckedChanged += ChkBox_CheckedChanged;
                panel5.Controls.Add(chkBox);
            }

            // Add Select All checkbox
            CheckBox cbSelectAll = new CheckBox
            {
                Text = "Select All",
                Name = "cbSelectAll"
            };
            cbSelectAll.CheckedChanged += ChkSelectAll_CheckedChanged;
            panel5.Controls.Add(cbSelectAll);
        }
        //DateTime Func
        private void FilterByDateRange(DateTime fromDate, DateTime toDate)
        {
            foreach (var series in allSeries)
            {
                // Ensure series.Values is properly typed as ChartValues<ObservablePoint>
                if (series.Values is ChartValues<ObservablePoint> chartValues)
                {
                    ChartValues<ObservablePoint> filteredValues = new ChartValues<ObservablePoint>();

                    foreach (var point in chartValues)
                    {
                        // Ensure each point is of type ObservablePoint
                        if (point is ObservablePoint observablePoint)
                        {
                            // Check if point's X value (assuming it's DateTime.Ticks) is within the specified range
                            if (observablePoint.X >= fromDate.Ticks && observablePoint.X <= toDate.Ticks)
                            {
                                filteredValues.Add(observablePoint);
                            }
                        }
                    }

                    // Assign filtered values back to the series
                    series.Values = filteredValues;
                }
            }

            // Update the chart after filtering
            chartExportData.Update(true, true); // Refresh chart with new data
        }
        //UpdateChar 
        private void UpdateChartSeries(DateTime startDate, DateTime endDate)
        {
            seriesCollection.Clear(); // Clear existing series
            chartExportData.Series.Clear();
            allSeries.Clear(); // Clear all series from the list

            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;
              dynamic seriesData;

            foreach (Control control in panel2.Controls)
            {
                if (control is CheckBox chkBox && chkBox.Checked && chkBox.Name != "cbSelectAll")
                {

                    if (chkBox.Name == "cbTotalEnergy")
                    {
                        seriesData = (Func<ChartValues<ObservablePoint>>)GenerateEnergyData; // Retrieve series data generator
                    }
                    else if (chkBox.Name == "cbActivePower")
                    {
                        seriesData = (Func<ChartValues<ObservablePoint>>)GeneratePowerData; // Retrieve series data generator
                    }
                    else if (chkBox.Name == "cbApperentPower")
                    {
                        seriesData = (Func<ChartValues<ObservablePoint>>)GenerateApparentPowerData; // Retrieve series data generator
                    }
                    else if (chkBox.Name == "cbPowerFactor")
                    {
                        seriesData = (Func<ChartValues<ObservablePoint>>)GeneratePowerFactorData; // Retrieve series data generator
                    }
                    else if (chkBox.Name == "cbVoltage")
                    {
                        seriesData = (Func<ChartValues<ObservablePoint>>)GenerateVoltageData; // Retrieve series data generator
                    }
                    else if (chkBox.Name == "cbCurrent")
                    {
                        seriesData = (Func<ChartValues<ObservablePoint>>)GenerateCurrentData; // Retrieve series data generator
                    }
                    else if (chkBox.Name == "cbFrequency")
                    {
                        seriesData = (Func<ChartValues<ObservablePoint>>)GenerateFrequencyData; // Retrieve series data generator
                    }
                    else
                    {
                        seriesData = (Func<ChartValues<ObservablePoint>>)chkBox.Tag;
                    }
                    var seriesValues = seriesData(); // Generate series data
                    var lineSeries = new LineSeries
                    {
                        Title = chkBox.Text, // Set series title
                        Values = seriesValues, // Set series values
                        PointGeometrySize = chkBox.Name == "cbTotalEnergy" || chkBox.Name == "cbActivePower" ? 15 : 5 // Set point size for energy & power series
                    };
                    seriesCollection.Add(lineSeries); // Add series to collection
                    allSeries.Add(lineSeries); // Add series to list for CSV export
                }
        }

            // Update datetimePickerTo and datetimePickerFrom
            if (minDate != DateTime.MaxValue && maxDate != DateTime.MinValue)
            {
                datePickerStartDate.Value = minDate;
                datePickerEndDate.Value = maxDate;
            }
            // Update chart with filtered date range
            chartExportData.Series = seriesCollection; // Update chart with new series
        }

        private ChartValues<ObservablePoint> FilterSeriesByDate(ChartValues<ObservablePoint> seriesValues, DateTime startDate, DateTime endDate)
        {
            ChartValues<ObservablePoint> filteredSeries = new ChartValues<ObservablePoint>();

            foreach (var point in seriesValues)
            {
                // Assuming point.X is the timestamp or index you're filtering on
                if (point.X >= startDate.Ticks && point.X <= endDate.Ticks)
                {
                    filteredSeries.Add(point);
                }
            }

            return filteredSeries;

        }

        //data
        #region data
        private ChartValues<ObservablePoint> GenerateEnergyData()
        {
            // Example: Generate dummy energy data (replace with actual logic)
            return new ChartValues<ObservablePoint>
            {
                new ObservablePoint(DateTime.Now.AddDays(-2).Ticks, 10),
                new ObservablePoint(DateTime.Now.AddDays(-2).Ticks + TimeSpan.FromSeconds(5).Ticks, 15),
                new ObservablePoint(DateTime.Now.AddDays(-2).Ticks + TimeSpan.FromSeconds(10).Ticks, 20),
                new ObservablePoint(DateTime.Now.AddDays(-2).Ticks + TimeSpan.FromSeconds(15).Ticks, 18),
                new ObservablePoint(DateTime.Now.AddDays(-2).Ticks + TimeSpan.FromSeconds(20).Ticks, 25)
            };
        }
        private ChartValues<ObservablePoint> GeneratePowerData()
        {
            // Example: Generate dummy power data (replace with actual logic)
            return new ChartValues<ObservablePoint>
            {
                new ObservablePoint(DateTime.Now.AddDays(-4).Ticks, 1),
                new ObservablePoint(DateTime.Now.AddDays(-4).Ticks + TimeSpan.FromSeconds(5).Ticks, 2),
                new ObservablePoint(DateTime.Now.AddDays(-4).Ticks + TimeSpan.FromSeconds(10).Ticks, 3),
                new ObservablePoint(DateTime.Now.AddDays(-4).Ticks + TimeSpan.FromSeconds(15).Ticks, 4),
                new ObservablePoint(DateTime.Now.AddDays(-4).Ticks + TimeSpan.FromSeconds(20).Ticks, 5)
            };
        }
        private ChartValues<ObservablePoint> GenerateApparentPowerData()
        {
            // Example: Generate dummy power data (replace with actual logic)
            return new ChartValues<ObservablePoint>
            {
                new ObservablePoint(DateTime.Now.Ticks, 8),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(5).Ticks, 11),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(10).Ticks, 13),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(15).Ticks, 14),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(20).Ticks, 19)
            };
        }
        private ChartValues<ObservablePoint> GeneratePowerFactorData()
        {
            // Example: Generate dummy power data (replace with actual logic)
            return new ChartValues<ObservablePoint>
            {
                new ObservablePoint(DateTime.Now.Ticks, 8),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(5).Ticks, 22),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(10).Ticks, 23),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(15).Ticks, 24),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(20).Ticks, 25)
            };
        }
        private ChartValues<ObservablePoint> GenerateVoltageData()
        {
            // Example: Generate dummy power data (replace with actual logic)
            return new ChartValues<ObservablePoint>
            {
                new ObservablePoint(DateTime.Now.Ticks, 8),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(5).Ticks, 33),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(10).Ticks, 34),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(15).Ticks, 35),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(20).Ticks, 36)
            };
        }
        private ChartValues<ObservablePoint> GenerateCurrentData()
        {
            // Example: Generate dummy power data (replace with actual logic)
            return new ChartValues<ObservablePoint>
            {
                new ObservablePoint(DateTime.Now.Ticks, 8),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(5).Ticks, 44),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(10).Ticks, 45),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(15).Ticks, 46),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(20).Ticks, 47)
            };
        }
        private ChartValues<ObservablePoint> GenerateFrequencyData()
        {
            // Example: Generate dummy power data (replace with actual logic)
            return new ChartValues<ObservablePoint>
            {
                new ObservablePoint(DateTime.Now.Ticks, 8),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(5).Ticks, 55),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(10).Ticks, 56),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(15).Ticks, 57),
                new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(20).Ticks, 58)
            };
        }


        #endregion
        #endregion
        #region Export CSV
        // Export data to CSV file
        private void ExportDataToCSV()
        {
            try
            {
                // Choose location to save CSV file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    csvFilePath = saveFileDialog.FileName;

                    using (StreamWriter writer = new StreamWriter(csvFilePath))
                    {
                        // Write header line
                        writer.WriteLine("DateTime,Energy,Power,ApparentPower,PowerFactor,Voltage,Current,Frequency");

                        // Find common data points range (considering the series with maximum data points)
                        int maxCount = allSeries.Max(s => s.Values.Count);
                        for (int i = 0; i < maxCount; i++)
                        {
                            DateTime dateTime = new DateTime((long)((ObservablePoint)allSeries[0].Values[i]).X);

                            // Write data for each series
                            writer.Write(dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            foreach (var series in allSeries)
                            {
                                if (i < series.Values.Count)
                                {
                                    writer.Write($",{((ObservablePoint)series.Values[i]).Y}");
                                }
                                else
                                {
                                    writer.Write(",");
                                }
                            }
                            writer.WriteLine();
                        }
                    }

                    MessageBox.Show($"Data exported to CSV file: {csvFilePath}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDownloadRawData_Click(object sender, EventArgs e)
        {
            ExportDataToCSV();
        }
        #endregion

        private void datePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            
                DateTime startDate = datePickerStartDate.Value;
                DateTime endDate = datePickerEndDate.Value;
                UpdateChartSeries(startDate, endDate);
        }
    }
}
