using BusinessLogic.Model;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using CheckBox = System.Windows.Forms.CheckBox;
namespace ENCAPv3
{
    public class ChartSet
    {
        public void ExportData_Clear(LiveCharts.WinForms.CartesianChart chart, CheckBox selectall, CheckBox ttlRemaining,
             CheckBox soc, CheckBox powerfact, CheckBox volt, CheckBox crnt)//, CheckBox temp)
        {
            chart.Series.Clear();
            selectall.Checked = false;
            ttlRemaining.Checked = false;
            soc.Checked = false;
            volt.Checked = false;
            crnt.Checked = false;
            //temp.Checked = false;
        }

        public void chartGT3(List<ChartValues<double>> allList, LiveCharts.WinForms.CartesianChart cartesianChart1)//, List<string> XAxisValue)
        {
            //cartesianChart1.AxisX.Clear();
            //cartesianChart1.AxisY.Clear();
            // Update X Axis labels
            //cartesianChart1.AxisX[0].Labels = XAxisValue;
            if (allList.Count > 0)
            {



                SolidColorBrush VoltageColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
                SolidColorBrush CurrentColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 254, 0));
                SolidColorBrush PowerColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(214, 252, 244));
                // Update or add series data for Voltage
                UpdateOrAddSeries(cartesianChart1.Series, 0, allList[0], "Voltage", 1, VoltageColor);

                // Update or add series data for Current
                UpdateOrAddSeries(cartesianChart1.Series, 1, allList[1], "Current", 3, CurrentColor);

                // Update or add series data for Power
                UpdateOrAddSeries(cartesianChart1.Series, 2, allList[2], "Power", 2, PowerColor);
            }


        }
        public void chartGT3(List<ChartValues<double>> allList, LiveCharts.WinForms.CartesianChart cartesianChart, List<List<StorePoint>> allList12)//, List<string> XAxisValue)
        {
            SolidColorBrush VoltageColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
            SolidColorBrush CurrentColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 254, 0));
            SolidColorBrush PowerColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(214, 252, 244));
            SolidColorBrush SocColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(254, 225, 255));
            SolidColorBrush TotalRemainingCapacityColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(253, 215, 229));
            SolidColorBrush TempColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(253, 242, 222));

            cartesianChart.Zoom = ZoomingOptions.X;
            cartesianChart.DisableAnimations = true;
            cartesianChart.Hoverable = false;
            cartesianChart.AxisX.Clear();
            cartesianChart.AxisY.Clear();
            cartesianChart.Series.Clear();
            //

            // Update X Axis labels
            //cartesianChart1.AxisX[0].Labels = XAxisValue;
            cartesianChart.Text = "cartesianChart";
            if (allList12[0].Count > 0)
            {
                UpdateOrAddSeries(cartesianChart.Series, 1, allList[0], "Voltage (V)", 1, VoltageColor);
            }
            if (allList12[1].Count > 0)
            {
                UpdateOrAddSeries(cartesianChart.Series, 2, allList[1], "Current (Amps)", 2, CurrentColor);
            }
            if (allList12[2].Count > 0)
            {
                UpdateOrAddSeries(cartesianChart.Series, 3, allList[2], "Power (kW)", 3, PowerColor);
            }
            if (allList12[3].Count > 0)
            {
                UpdateOrAddSeries(cartesianChart.Series, 4, allList[3], "SOC", 4, SocColor);
            }
            if (allList12[4].Count > 0)
            {
                UpdateOrAddSeries(cartesianChart.Series, 5, allList[4], "Total Remaining Capacity(Ah)", 5, TotalRemainingCapacityColor);
            }
            if (allList12[5].Count > 0)
            {

               UpdateOrAddSeries(cartesianChart.Series, 6, allList[5], "Temperature (C)", 6, TempColor);
            }

            // Add data to the chart
            ////cartesianChart.Series.Add(new LineSeries
            ////{
            ////    Title = "Sample Data",
            ////    Values = allList[0],
            ////    PointGeometry = DefaultGeometries.Circle,
            ////    PointGeometrySize = 10
            ////});

            dynamic dateLabels = null;
            // Convert TimeStamp to string labels
            for (int i = 0; i < allList12.Count; i++)
            {
                if (allList12[i].Count > 0)
                {
                    //dateLabels = Array.ConvertAll(allList12[i].ToArray(), sp => sp.TimeStamp.ToString("dd/MM/yyyy"));
                    dateLabels = Array.ConvertAll(allList12[i].ToArray(), sp => sp.TimeStamp.HasValue ? sp.TimeStamp.Value.ToString("dd/MM/yyyy") : string.Empty);

                }
            }


            // Configure the x-axis as DateTime
            cartesianChart.AxisX.Add(new Axis
            {
                Title = "Date",
                Labels = dateLabels,
                LabelsRotation = 15
            });

            // Configure the y-axis as numerical
            cartesianChart.AxisY.Add(new Axis
            {
                Title = "Value",
                LabelFormatter = value => value.ToString("N")
            });


            //foreach (var item in allList12)
            //{
            //    for (int i = 0; i < item.Count; i++)
            //    {
            //        UpdateOrAddSeries(cartesianChart.Series, i, allList[i], item[i].Parameter, i);

            //    }
            //}




        }
        private const int KeepRecords = 21;
        private void UpdateOrAddSeries(SeriesCollection seriesCollection, int index, ChartValues<double> newData, string title, int strokeThickness, SolidColorBrush pointForeground)
        {
            LiveCharts.Wpf.LineSeries series;
            if (index < seriesCollection.Count)
            {
                series = seriesCollection[index] as LineSeries;
                // series.Title = title;
                series.Values.Clear();
                foreach (var value in newData)
                {
                    series.Values.Add(value);
                }
                TrimChartValues((ChartValues<double>)series.Values, KeepRecords);
            }
            else
            {
                series = new LineSeries
                {
                    Title = title,
                    Values = new ChartValues<double>(newData),
                    StrokeThickness = strokeThickness,
                    Stroke = pointForeground,//new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 226, 0)),
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometrySize = 10,
                    PointForeground = pointForeground
                };

                seriesCollection.Add(series);
                TrimChartValues((ChartValues<double>)series.Values, KeepRecords);
            }
        }

        private void TrimChartValues(ChartValues<double> values, int maxRecords)
        {
            while (values.Count > maxRecords)
            {
                values.RemoveAt(0);
            }
        }

    }
}
