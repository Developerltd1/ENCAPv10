using BusinessLogic;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BusinessLogic.Model.StaticModel;
using System.Windows.Forms.DataVisualization.Charting;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;
using EasyModbus;

namespace ENCAPv3.UI
{
    public partial class Dashboard : Form//BaseForm//Form//BasetForm
    {
        private Series series;
        static List<ChartValues<double>> allLists = new List<ChartValues<double>>();

        private const int CELL1 = 0;
        private const int CELL2 = 1;
        private const int CELL3 = 2;
        private const int CELL4 = 3;
        private const int CELL5 = 4;
        private const int CELL6 = 5;
        private const int CELL7 = 6;
        private const int CELL8 = 7;
        private const int CELL9 = 8;
        private const int CELL10 = 9;
        private const int CELL11 = 10;
        private const int CELL12 = 11;
        private const int CELL13 = 12;
        private const int CELL14 = 13;
        private const int CELL15 = 14;
        private const int TEMP = 32;
        private const int VOLTAGE = 40;
        private const int CURRENT = 41;
        private const int SOC = 42;
        private const int AH = 48;
        private const int POWER = 57;

        DataTable dt = null;
        public Dashboard()//DataTable _dt)
        {

            InitializeComponent();

            //if (_dt != null)
            //    SetupDataGridViewData(_dt);
        }

        //private void chart()
        //{
        //    // Create the CartesianChart control
        //    var cartesianChart = new LiveCharts.WinForms.CartesianChart();
        //    cartesianChart.Dock = DockStyle.Fill;
        //    this.Controls.Add(cartesianChart);

        //    // Set up data series for cell voltages
        //    SeriesCollection seriesCollection = new SeriesCollection();

        //    // Example data: integer X values and decimal Y values
        //    double[] yValues = { 2.1, 3.1, 4.1, 5.0, 4.8, 3.9 }; // Replace with your actual data
        //    ChartValues<ObservablePoint> chartValues = new ChartValues<ObservablePoint>();

        //    for (int i = 0; i < yValues.Length; i++)
        //    {
        //        chartValues.Add(new ObservablePoint(i + 1, yValues[i]));
        //    }

        //    // Add series to the collection
        //    seriesCollection.Add(new LineSeries
        //    {
        //        Title = "Cell Voltages (V)",
        //        Values = chartValues,
        //        PointGeometrySize = 15
        //    });

        //    // Configure X axis
        //    cartesianChart.AxisX.Add(new LiveCharts.Wpf.Axis
        //    {
        //        Title = "Cell",
        //        Labels = new[] { "1", "2", "3", "4", "5", "6" } // X axis labels
        //    });

        //    // Configure Y axis
        //    cartesianChart.AxisY.Add(new LiveCharts.Wpf.Axis
        //    {
        //        Title = "Voltages (V)",
        //        LabelFormatter = value => value.ToString("N1") // Format Y axis labels as decimal with one decimal place
        //    });

        //    // Add legends
        //    cartesianChart.LegendLocation = LiveCharts.LegendLocation.Right;
        //    cartesianChart.Name = "Legend";

        //    // Assign series collection to chart
        //    cartesianChart.Series = seriesCollection;

        //}

        //private void SetupDataGridView()
        //{
        //    //DataTable dt = new StaticData().DataGridView(); //ParamatersRecord

        //    //dgvCellLevel.Rows.Clear();
        //    //foreach (DataRow r in dt.Rows)
        //    //{
        //    //    dgvCellLevel.Rows.Add(
        //    //        r["Parameter"].ToString(), r["Battery-1"].ToString(), r["Battery-2"].ToString(), r["Battery-3"].ToString(), r["Battery-4"].ToString(), r["Battery-5"].ToString());
        //    //}
        //}
        private void SetupDataGridViewData(DataTable dt)
        {
            //dgvCellLevelDash.DataSource = dt;
            //foreach (DataRow r in dt.Rows)
            //{
            //    dgvCellLevelDash.Rows.Add(
            //        r["Parameter"].ToString(), r["Battery-1"].ToString(), r["Battery-2"].ToString(), r["Battery-3"].ToString(), r["Battery-4"].ToString(), r["Battery-5"].ToString());
            //}
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        //Funcations
        private void Paremeters()
        {
            DashboardModel.Parameters _Parameters = new StaticData().DashboardData();
            labelChargeEnergy.Text = _Parameters.labelChargeEnergy.ToString();
            labelDiscEnergy.Text = _Parameters.labelDiscEnergy.ToString();
            labelPowerSystem.Text = _Parameters.labelPowerSystem.ToString();
            labelRemaningEnergy.Text = _Parameters.labelRemaningEnergy.ToString();
            labelRemaningTime.Text = _Parameters.labelRemaningTime.ToString();
            labelStateOfCharge.Text = _Parameters.labelStateOfCharge.ToString();
            labelSystemTemp.Text = _Parameters.labelSystemTemp.ToString();
            labelTermilanVolt.Text = _Parameters.labelTermilanVolt.ToString();
            labelTerminalCurrent.Text = _Parameters.labelTerminalCurrent.ToString();
        }
        //private void InitializeChart()
        //{
        //    // Example data
        //    int[] xValues = { 1, 2, 3, 4, 5 };
        //    double[] yValues = { 10.2, 15.1, 7.5, 22.3, 18.6 };

        //    // Clear existing series
        //    chart1.Series.Clear();

        //    // Initialize the series object
        //    series = new Series("Sample Series");
        //    series.ChartType = SeriesChartType.Line;

        //    for (int i = 0; i < xValues.Length; i++)
        //    {
        //        series.Points.AddXY(xValues[i], yValues[i]);
        //    }

        //    // Add series to the chart
        //    chart1.Series.Add(series);

        //    // Customize chart appearance
        //    chart1.ChartAreas[0].AxisX.Title = "X Axis";
        //    chart1.ChartAreas[0].AxisY.Title = "Y Axis";
        //    chart1.Titles.Add("Sample Line Chart");
        //}

        #region GridRealTimeData

        private bool isPollingEnabled = false;
        private Timer pollingTimer;
        private ModbusClient modbusClient;

        #region btnToggle
        private void btnToggle()
        {
            isPollingEnabled = !isPollingEnabled;

            if (isPollingEnabled)
            {
                //btnTogglePolling1.BackColor = Color.Gray;
                InitializePollingTimer();
                StartPolling();
            }
            else
            {
                // btnTogglePolling1.BackColor = Color.White;
                StopPolling();

            }
        }
        #endregion

        #region SupportFunctions
        private void InitializePollingTimer()
        {
            ////////pollingTimer = new Timer();
            ////////pollingTimer.Interval = 500;// Convert.ToInt32(pollingTimeout.Value); // Polling interval in milliseconds (1 second here)
            ////////pollingTimer.Tick += PollingTimer_Tick;
        }
        private void StartPolling()
        {
            pollingTimer.Start();
        }

        private void StopPolling()
        {
            pollingTimer.Stop();
            if (modbusClient.Connected)
            {
                modbusClient.Disconnect();
            }
        }

        private async  void PollingTimer_Tick(object sender, EventArgs e)
        {
            int functionCode = 3;// Convert.ToInt32(comboBoxFunctionCode.SelectedIndex) + 1;
            if (MainParamatersForm.isPollSelected)
            {
                List<ChartValues<double>> ll = await new MainParamatersForm().LoadModbusDataAsync(1,functionCode, 0, 0); // Load data function remains the same
                LoadModbusData(functionCode);                                                                   //AQIB
                DataTable ddt = new MainParamatersForm().fnpublic();
                new ChartSet().chartGT3(ll, cartesianChart1);
            }
        }

        public void LoadModbusData(int functionCode)
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Int32 modbusStartReg = 0;// Convert.ToInt32(startReg.Value);
            Int32 modbusRegCount = 58;// Convert.ToInt32(regCount.Value);
            Int32 modbusTotalReg = modbusRegCount + modbusStartReg;
            Int16 index = 0;
            Int32 searchValue = 0;
            Int32 searchRange = 0;
            Int32[] refValue = { 0, 0, 0, 0 };
            Int32 batteryIndex;
            double voltage, current, soc, power, temp, ah, avgcell, cell1, cell2, cell3, cell4, cell5, cell6, cell7, cell8, cell9, cell10, cell11, cell12, cell13, cell14, cell15;
            int[] registers = { 0 };
            bool[] boolsRegister = { false };
            bool searchRegisterFlag = false;
            int z = 0;

            InitializeModbusClient();
            //UpdateStatusBar();
            

            //statusConnection.Text = ("Connected");
            try
            {
                switch (functionCode)
                {
                    ////////case 1:
                    ////////    boolsRegister = modbusClient.ReadCoils(modbusStartReg, modbusRegCount);// Read Coils (0x01)
                    ////////    break;
                    ////////case 2:
                    ////////    boolsRegister = modbusClient.ReadDiscreteInputs(modbusStartReg, modbusRegCount);// Read Discrete Inputs (0x02)
                    ////////    break;
                    ////////case 3:
                    ////////    registers = modbusClient.ReadHoldingRegisters(modbusStartReg, modbusRegCount);// Read Holding Registers (0x03)
                    ////////    break;
                    ////////case 4:
                    ////////    registers = modbusClient.ReadInputRegisters(modbusStartReg, modbusRegCount);// Read Input Registers (0x04)
                    ////////    break;
                    ////////default:
                    ////////    throw new InvalidOperationException("Unsupported function code.");
                }
                /*  z = 0;
                  dataGridView1.Rows[z++].Cells[0].Value = "Voltage (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Current (Amps)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Power (kW)";
                  dataGridView1.Rows[z++].Cells[0].Value = "SOC";
                  dataGridView1.Rows[z++].Cells[0].Value = "Total Remaining Capacity(Ah)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Temperature (C)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-1 (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-2 (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-3 (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-4 (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-5 (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-6 (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-7 (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-8 (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-9 (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-10 (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-11 (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-12 (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-13 (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-14 (V)";
                  dataGridView1.Rows[z++].Cells[0].Value = "Cell-15 (V)";*/
                z = 0;


                ////////voltage = (registers[VOLTAGE]) * 0.1;
                ////////current = (registers[CURRENT] - 30000) * 0.1;
                ////////power = (registers[POWER]);
                ////////soc = (registers[SOC]) * 0.1;
                ////////ah = (registers[AH]) * 0.1;
                ////////temp = (registers[TEMP]) - 40;
                ////////cell1 = (registers[CELL1]) * 0.001;
                ////////cell2 = (registers[CELL2]) * 0.001;
                ////////cell3 = (registers[CELL3]) * 0.001;
                ////////cell4 = (registers[CELL4]) * 0.001;
                ////////cell5 = (registers[CELL5]) * 0.001;
                ////////cell6 = (registers[CELL6]) * 0.001;
                ////////cell7 = (registers[CELL7]) * 0.001;
                ////////cell8 = (registers[CELL8]) * 0.001;
                ////////cell9 = (registers[CELL9]) * 0.001;
                ////////cell10 = (registers[CELL10]) * 0.001;
                ////////cell11 = (registers[CELL11]) * 0.001;
                ////////cell12 = (registers[CELL12]) * 0.001;
                ////////cell13 = (registers[CELL13]) * 0.001;
                ////////cell14 = (registers[CELL14]) * 0.001;
                ////////cell15 = (registers[CELL15]) * 0.001;

                ////////Math.Round(voltage, 1);
                ////////Math.Round(current, 1);
                ////////Math.Round(power, 1);
                ////////Math.Round(soc, 1);
                ////////Math.Round(ah, 1);
                ////////Math.Round(temp, 1);
                ////////Math.Round(cell1, 1);
                ////////Math.Round(cell2, 1);
                ////////Math.Round(cell3, 1);
                ////////Math.Round(cell4, 1);
                ////////Math.Round(cell5, 1);
                ////////Math.Round(cell6, 1);
                ////////Math.Round(cell7, 1);
                ////////Math.Round(cell8, 1);
                ////////Math.Round(cell9, 1);
                ////////Math.Round(cell10, 1);
                ////////Math.Round(cell11, 1);
                ////////Math.Round(cell12, 1);
                ////////Math.Round(cell13, 1);
                ////////Math.Round(cell14, 1);

                batteryIndex = 1;

                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = voltage;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = current;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = power;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = soc;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = ah;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = temp;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell1;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell2;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell3;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell4;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell5;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell6;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell7;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell8;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell9;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell10;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell11;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell12;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell13;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell14;
                //dgvCellLevelDash.Rows[z++].Cells[batteryIndex].Value = cell15;

                ////////avgcell = ((cell1 + cell2 + cell3 + cell4 + cell5 + cell6 + cell7 + cell8 + cell9 + cell10 + cell11 + cell12 + cell13 + cell14) / 14);

                // labelAvgCell.Text = avgcell.ToString("0.0");


                //  tbCell_15.Text = registers[1].ToString();

                //SaveData();




            }
            catch (Exception ex)
            {//this is the place>>>>>>>>>>>>>>>>>>>>>>>>>where com error msg appears when data fails
             //MessageBox.Show("Error reading Modbus data: " + ex.Message);

            }
            finally
            {
                if (modbusClient.Connected)
                {
                    modbusClient.Disconnect(); // Disconnect from Modbus server

                }
            }
        }
        private void InitializeModbusClient()
        {
            try
            {
                /*if (string.IsNullOrEmpty(comPorts.Text))
                {
                    infoMessages.Text = "No COM port is selected";
                    return;
                }
                else*/
                {
                    modbusClient = new ModbusClient("COM8")//)comPorts.Text)
                    {
                        Baudrate = 9600,//Convert.ToInt32(cbBaudRate1.SelectedItem),
                        Parity = System.IO.Ports.Parity.None,
                        StopBits = System.IO.Ports.StopBits.One,
                        UnitIdentifier = Convert.ToByte(1),//numSlaveID.Value), // Slave ID
                        ConnectionTimeout = 1000//Convert.ToInt32(readingTimeOut.Value) // Timeout in milliseconds
                    };
                }

                modbusClient.Connect();
            }
            catch (Exception ex)
            {

            }
        }

        #endregion
        #endregion
        private void Dashboard_Load(object sender, EventArgs e)
        {
            ////////isPollingEnabled = !isPollingEnabled;

            ////////if (isPollingEnabled)
            ////////{
            ////////    InitializePollingTimer();
            ////////    StartPolling();
            ////////     new MainParamatersForm().StartPollingDatabase();
            ////////}
            ////////else
            ////////{
            ////////    StopPolling();
            ////////     new MainParamatersForm().StopPollingDatabase();
            ////////}

            ////////Paremeters();
         
        }
   
    
    
    }
}
