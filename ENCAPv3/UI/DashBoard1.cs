using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;

namespace ENCAPv3.UI
{
    public partial class DashBoard1 : Form
    {

        private Random _random = new Random();
        private Timer _timer;

        public DashBoard1()
        {
            InitializeComponent();

            // Initialize the chart
            cartesianChart1.Series = new LiveCharts.SeriesCollection
            {
                new LineSeries
                {
                    Title = "Voltage (V)",
                    Values = new ChartValues<double>()
                },
                new LineSeries
                {
                    Title = "Current (Amps)",
                    Values = new ChartValues<double>()
                },
                new LineSeries
                {
                    Title = "Power (kW)",
                    Values = new ChartValues<double>()
                },
                new LineSeries
                {
                    Title = "SOC Power",
                    Values = new ChartValues<double>()
                },
                new LineSeries
                {
                    Title = "SOC Total Remaining Capacity",
                    Values = new ChartValues<double>()
                },
                new LineSeries
                {
                    Title = "SOC Temperature",
                    Values = new ChartValues<double>()
                }
            };

            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Metrics",
                Labels = new[] { "Voltage (V)", "Current (Amps)", "Power (kW)", "SOC", "Total Remaining Capacity (Ah)", "Temperature (C)" }
            });

            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Values"
            });

            // Set up the timer
            _timer = new Timer { Interval = 1000 }; // Update every second
            _timer.Tick += TimerOnTick;
            _timer.Start();


        }
        private void TimerOnTick(object sender, EventArgs e)
        {
            // Here you should replace the random data with your actual data
            double voltage = _random.Next(0, 100);
            double current = _random.Next(0, 100);
            double power = _random.Next(0, 100);
            double socPower = _random.Next(0, 100);
            double socTotalRemainingCapacity = _random.Next(0, 100);
            double socTemperature = _random.Next(0, 100);

            cartesianChart1.Series[0].Values.Add(voltage);
            cartesianChart1.Series[1].Values.Add(current);
            cartesianChart1.Series[2].Values.Add(power);
            cartesianChart1.Series[3].Values.Add(socPower);
            cartesianChart1.Series[4].Values.Add(socTotalRemainingCapacity);
            cartesianChart1.Series[5].Values.Add(socTemperature);
        }
    }
}
