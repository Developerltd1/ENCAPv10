using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENCAPv3.UI
{
    public class ExportDataPartial
    {

        public void WriteToCsv(List<List<StorePoint>> data)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
                saveFileDialog.Title = "Save CSV File";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder csvContent = new StringBuilder();

                    // Add header
                    csvContent.AppendLine("Parameter,Battery1,TimeStamp");

                    // Add rows
                    foreach (var list in data)
                    {
                        foreach (var item in list)
                        {
                            csvContent.AppendLine($"{item.Parameter},{item.Battery1},{item.TimeStamp:yyyy-MM-dd HH:mm:ss}");
                        }
                    }

                    // Write to file
                    File.WriteAllText(saveFileDialog.FileName, csvContent.ToString());
                }
            }
        }
    }
}



    #region MyRegion


    ////Funcation
    //private void InitializeChart()
    //{
    //    // Initialize series collection
    //    seriesCollection = new SeriesCollection();
    //    allSeries = new List<LineSeries>();

    //    // Configure chart
    //    chartExportData.AxisX.Add(new LiveCharts.Wpf.Axis
    //    {
    //        Title = "Date Time",
    //        LabelFormatter = value => new DateTime((long)value).ToString("yyyy-MM-dd HH:mm:ss")
    //    });

    //    chartExportData.AxisY.Add(new LiveCharts.Wpf.Axis
    //    {
    //        Title = "Values"
    //    });

    //    chartExportData.LegendLocation = LiveCharts.LegendLocation.Top;
    //    // Set legend text color and background color
    //    chartExportData.ForeColor = System.Drawing.Color.White;// White text color


    //    // Initialize checkboxes dynamically
    //    InitializeCheckBoxes();
    //}
    ////Event
    //// Event handler for individual checkbox change
    //private void ChkBox_CheckedChanged(object sender, EventArgs e)
    //{
    //    UpdateChartSeries();
    //}
    //// Event handler for Select All checkbox change
    //private void ChkSelectAll_CheckedChanged(object sender, EventArgs e)
    //{
    //    bool isChecked = ((CheckBox)sender).Checked;
    //    foreach (Control control in panel2.Controls)
    //    {
    //        if (control is CheckBox chkBox && chkBox.Name != "cbSelectAll")
    //        {
    //            chkBox.Checked = isChecked;
    //        }
    //    }
    //    UpdateChartSeries();
    //}
    //private void FilterByDateRange(DateTime fromDate, DateTime toDate)
    //{
    //    foreach (var series in allSeries)
    //    {
    //        // Ensure series.Values is properly typed as ChartValues<ObservablePoint>
    //        if (series.Values is ChartValues<ObservablePoint> chartValues)
    //        {
    //            ChartValues<ObservablePoint> filteredValues = new ChartValues<ObservablePoint>();

    //            foreach (var point in chartValues)
    //            {
    //                // Ensure each point is of type ObservablePoint
    //                if (point is ObservablePoint observablePoint)
    //                {
    //                    // Check if point's X value (assuming it's DateTime.Ticks) is within the specified range
    //                    if (observablePoint.X >= fromDate.Ticks && observablePoint.X <= toDate.Ticks)
    //                    {
    //                        filteredValues.Add(observablePoint);
    //                    }
    //                }
    //            }

    //            // Assign filtered values back to the series
    //            series.Values = filteredValues;
    //        }
    //    }

    //    // Update the chart after filtering
    //    chartExportData.Update(true, true); // Refresh chart with new data
    //}
    ////UpdateChart
    //// Update the chart series based on checkbox selections
    //private void UpdateChartSeries()
    //{
    //    seriesCollection.Clear(); // Clear existing series
    //    chartExportData.Series.Clear();
    //    allSeries.Clear(); // Clear all series from the list
    //    dynamic seriesData;
    //    foreach (Control control in panel2.Controls)
    //    {
    //        if (control is CheckBox chkBox && chkBox.Checked && chkBox.Name != "cbSelectAll")
    //        {

    //            if (chkBox.Name == "cbTotalEnergy")
    //            {
    //                seriesData = (Func<ChartValues<ObservablePoint>>)GenerateEnergyData; // Retrieve series data generator
    //            }
    //            else if (chkBox.Name == "cbActivePower")
    //            {
    //                seriesData = (Func<ChartValues<ObservablePoint>>)GeneratePowerData; // Retrieve series data generator
    //            }
    //            else if (chkBox.Name == "cbApperentPower")
    //            {
    //                seriesData = (Func<ChartValues<ObservablePoint>>)GenerateApparentPowerData; // Retrieve series data generator
    //            }
    //            else if (chkBox.Name == "cbPowerFactor")
    //            {
    //                seriesData = (Func<ChartValues<ObservablePoint>>)GeneratePowerFactorData; // Retrieve series data generator
    //            }
    //            else if (chkBox.Name == "cbVoltage")
    //            {
    //                seriesData = (Func<ChartValues<ObservablePoint>>)GenerateVoltageData; // Retrieve series data generator
    //            }
    //            else if (chkBox.Name == "cbCurrent")
    //            {
    //                seriesData = (Func<ChartValues<ObservablePoint>>)GenerateCurrentData; // Retrieve series data generator
    //            }
    //            else if (chkBox.Name == "cbFrequency")
    //            {
    //                seriesData = (Func<ChartValues<ObservablePoint>>)GenerateFrequencyData; // Retrieve series data generator
    //            }
    //            else
    //            {
    //                seriesData = (Func<ChartValues<ObservablePoint>>)chkBox.Tag;
    //            }
    //            var seriesValues = seriesData(); // Generate series data
    //            var lineSeries = new LineSeries
    //            {
    //                Title = chkBox.Text, // Set series title
    //                Values = seriesValues, // Set series values
    //                PointGeometrySize = chkBox.Name == "cbTotalEnergy" || chkBox.Name == "cbActivePower" ? 15 : 5 // Set point size for energy & power series
    //            };
    //            seriesCollection.Add(lineSeries); // Add series to collection
    //            allSeries.Add(lineSeries); // Add series to list for CSV export
    //        }
    //    }


    //    // Update chart with filtered date range


    //    chartExportData.Series = seriesCollection; // Update chart with new series
    //}


    ////data
    //// Example data generation functions (replace with actual data logic)
    //private ChartValues<ObservablePoint> GenerateEnergyData()
    //{
    //    // Example: Generate dummy energy data (replace with actual logic)
    //    return new ChartValues<ObservablePoint>
    //    {
    //        new ObservablePoint(DateTime.Now.Ticks, 10),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(5).Ticks, 15),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(10).Ticks, 20),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(15).Ticks, 18),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(20).Ticks, 25)
    //    };
    //}
    //private ChartValues<ObservablePoint> GeneratePowerData()
    //{
    //    // Example: Generate dummy power data (replace with actual logic)
    //    return new ChartValues<ObservablePoint>
    //    {
    //        new ObservablePoint(DateTime.Now.Ticks, 8),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(5).Ticks, 12),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(10).Ticks, 15),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(15).Ticks, 10),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(20).Ticks, 18)
    //    };
    //}
    //private ChartValues<ObservablePoint> GenerateApparentPowerData()
    //{
    //    // Example: Generate dummy power data (replace with actual logic)
    //    return new ChartValues<ObservablePoint>
    //    {
    //        new ObservablePoint(DateTime.Now.Ticks, 8),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(5).Ticks, 11),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(10).Ticks, 13),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(15).Ticks, 14),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(20).Ticks, 19)
    //    };
    //}
    //private ChartValues<ObservablePoint> GeneratePowerFactorData()
    //{
    //    // Example: Generate dummy power data (replace with actual logic)
    //    return new ChartValues<ObservablePoint>
    //    {
    //        new ObservablePoint(DateTime.Now.Ticks, 8),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(5).Ticks, 22),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(10).Ticks, 23),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(15).Ticks, 24),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(20).Ticks, 25)
    //    };
    //}
    //private ChartValues<ObservablePoint> GenerateVoltageData()
    //{
    //    // Example: Generate dummy power data (replace with actual logic)
    //    return new ChartValues<ObservablePoint>
    //    {
    //        new ObservablePoint(DateTime.Now.Ticks, 8),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(5).Ticks, 33),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(10).Ticks, 34),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(15).Ticks, 35),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(20).Ticks, 36)
    //    };
    //}
    //private ChartValues<ObservablePoint> GenerateCurrentData()
    //{
    //    // Example: Generate dummy power data (replace with actual logic)
    //    return new ChartValues<ObservablePoint>
    //    {
    //        new ObservablePoint(DateTime.Now.Ticks, 8),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(5).Ticks, 44),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(10).Ticks, 45),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(15).Ticks, 46),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(20).Ticks, 47)
    //    };
    //}
    //private ChartValues<ObservablePoint> GenerateFrequencyData()
    //{
    //    // Example: Generate dummy power data (replace with actual logic)
    //    return new ChartValues<ObservablePoint>
    //    {
    //        new ObservablePoint(DateTime.Now.Ticks, 8),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(5).Ticks, 55),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(10).Ticks, 56),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(15).Ticks, 57),
    //        new ObservablePoint(DateTime.Now.Ticks + TimeSpan.FromSeconds(20).Ticks, 58)
    //    };
    //}
    //private void InitializeCheckBoxes()
    //{
    //    // Initialize CheckBoxes for each series
    //    Dictionary<string, Func<ChartValues<ObservablePoint>>> seriesData = new Dictionary<string, Func<ChartValues<ObservablePoint>>>
    //    {
    //        { "Energy", GenerateEnergyData },
    //        { "Power", GeneratePowerData },
    //        { "Apparent Power", GenerateApparentPowerData },
    //        { "Power Factor", GeneratePowerFactorData },
    //        { "Voltage", GenerateVoltageData },
    //        { "Current", GenerateCurrentData },
    //        { "Frequency", GenerateFrequencyData }
    //    };

    //    foreach (var kvp in seriesData)
    //    {
    //        CheckBox chkBox = new CheckBox
    //        {
    //            Text = kvp.Key,
    //            Tag = kvp.Value
    //        };
    //        chkBox.CheckedChanged += ChkBox_CheckedChanged;
    //        panel5.Controls.Add(chkBox);
    //    }

    //    // Add Select All checkbox
    //    CheckBox cbSelectAll = new CheckBox
    //    {
    //        Text = "Select All",
    //        Name = "cbSelectAll"
    //    };
    //    cbSelectAll.CheckedChanged += ChkSelectAll_CheckedChanged;
    //    panel5.Controls.Add(cbSelectAll);
    //}

    #endregion







    #region ChartOnly
    //private void InitializeChart()
    //{
    //    // Initialize series collection and list
    //    seriesCollection = new SeriesCollection();
    //    allSeries = new List<LineSeries>();

    //    // Configure chart
    //    chartExportData.AxisX.Add(new LiveCharts.Wpf.Axis
    //    {
    //        Title = "Date Time",
    //        LabelFormatter = value => new DateTime((long)value).ToString("yyyy-MM-dd HH:mm:ss")
    //    });

    //    chartExportData.AxisY.Add(new LiveCharts.Wpf.Axis
    //    {
    //        Title = "Values"
    //    });

    //    chartExportData.LegendLocation = LiveCharts.LegendLocation.Bottom;
    //}

    //private void InitializeCheckBoxes()
    //{
    //    // Example: Initialize checkboxes dynamically based on your UI
    //    // Replace with actual checkbox initialization logic
    //    // For example:
    //    cbTotalEnergy.CheckedChanged += ChkBox_CheckedChanged;
    //    cbActivePower.CheckedChanged += ChkBox_CheckedChanged;
    //    cbApperentPower.CheckedChanged += ChkBox_CheckedChanged;
    //    cbPowerFactor.CheckedChanged += ChkBox_CheckedChanged;
    //    cbVoltage.CheckedChanged += ChkBox_CheckedChanged;
    //    cbCurrent.CheckedChanged += ChkBox_CheckedChanged;
    //    cbFrequency.CheckedChanged += ChkBox_CheckedChanged;
    //    cbSelectAll.CheckedChanged += ChkSelectAll_CheckedChanged;
    //}

    //private void ChkBox_CheckedChanged(object sender, EventArgs e)
    //{
    //    // Handle checkbox checked changed event
    //    GenerateChart();
    //}

    //private void ChkSelectAll_CheckedChanged(object sender, EventArgs e)
    //{
    //    // Handle select all checkbox checked changed event
    //    bool isChecked = cbSelectAll.Checked;

    //    // Example: Check or uncheck all other checkboxes
    //    cbTotalEnergy.Checked = isChecked;
    //    cbActivePower.Checked = isChecked;
    //    cbApperentPower.Checked = isChecked;
    //    cbPowerFactor.Checked = isChecked;
    //    cbVoltage.Checked = isChecked;
    //    cbCurrent.Checked = isChecked;
    //    cbFrequency.Checked = isChecked;

    //    GenerateChart();
    //}

    //private void btnGenerateChart_Click(object sender, EventArgs e)
    //{
    //    GenerateChart();
    //}

    //private void GenerateChart()
    //{
    //    // Clear existing series
    //    seriesCollection.Clear();
    //    allSeries.Clear();

    //    // Example: Add selected series based on checkbox state
    //    if (cbTotalEnergy.Checked)
    //    {
    //        seriesCollection.Add(CreateLineSeries("Total Energy"));
    //    }
    //    if (cbActivePower.Checked)
    //    {
    //        seriesCollection.Add(CreateLineSeries("Active Power"));
    //    }
    //    if (cbApperentPower.Checked)
    //    {
    //        seriesCollection.Add(CreateLineSeries("Apparent Power"));
    //    }
    //    if (cbPowerFactor.Checked)
    //    {
    //        seriesCollection.Add(CreateLineSeries("Power Factor"));
    //    }
    //    if (cbVoltage.Checked)
    //    {
    //        seriesCollection.Add(CreateLineSeries("Voltage"));
    //    }
    //    if (cbCurrent.Checked)
    //    {
    //        seriesCollection.Add(CreateLineSeries("Current"));
    //    }
    //    if (cbFrequency.Checked)
    //    {
    //        seriesCollection.Add(CreateLineSeries("Frequency"));
    //    }

    //    // Add series to chart
    //    foreach (var series in seriesCollection)
    //    {
    //        chartExportData.Series.Add(series);
    //    }

    //    // Update chart with filtered date range
    //    FilterByDateRange(datePickerStartDate.Value, datePickerEndDate.Value);
    //}

    //private void FilterByDateRange(DateTime fromDate, DateTime toDate)
    //{
    //    foreach (var series in allSeries)
    //    {
    //        // Check if series.Values is ChartValues<ObservablePoint>
    //        if (series.Values is ChartValues<ObservablePoint> chartValues)
    //        {
    //            ChartValues<ObservablePoint> filteredValues = new ChartValues<ObservablePoint>();

    //            foreach (var point in chartValues)
    //            {
    //                // Assuming point.X is DateTime.Ticks, check if it falls within the specified range
    //                if (point.X >= fromDate.Ticks && point.X <= toDate.Ticks)
    //                {
    //                    filteredValues.Add(point);
    //                }
    //            }

    //            // Assign filtered values back to the series
    //            series.Values = filteredValues;
    //        }

    //        // Update chart
    //        chartExportData.Update(true, true); // Refresh chart with new data
    //    }
    //}
    //public LineSeries CreateLineSeries(string title)
    //{
    //    // Example: Create LineSeries with dummy data (replace with your data logic)
    //    var lineSeries = new LineSeries
    //    {
    //        Title = title,
    //        Values = new ChartValues<ObservablePoint>
    //        {
    //            new ObservablePoint(DateTime.Now.Ticks, 10),
    //            new ObservablePoint(DateTime.Now.AddMinutes(10).Ticks, 15),
    //            new ObservablePoint(DateTime.Now.AddMinutes(20).Ticks, 12),
    //            new ObservablePoint(DateTime.Now.AddMinutes(30).Ticks, 8),
    //            new ObservablePoint(DateTime.Now.AddMinutes(40).Ticks, 11)
    //        },
    //        PointGeometrySize = 15
    //    };

    //    allSeries.Add(lineSeries); // Add to allSeries list for export

    //    return lineSeries;
    //}
    #endregion

 