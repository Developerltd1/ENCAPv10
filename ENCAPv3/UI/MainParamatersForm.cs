using BusinessLogic;
using BusinessLogic.Model;
using LiveCharts;
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
using EasyModbus;
using System.IO.Ports;
using static BusinessLogic.Model.StaticModel;
using LiveCharts.Defaults;
using System.Windows.Media;
using System.Data.SqlClient;
using log4net;
//System.Windows.Forms
namespace ENCAPv3.UI
{
    public partial class MainParamatersForm : Form//BasetForm
    {

        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainParamatersForm));


        #region DatabasePollingEnrty


        private bool isPolling;
        private List<BatteryData> dataList;
        /*  private Timer secondTimer;*/
        private Timer minuteTimer;
        private DateTime startTime;


        #endregion
        private SeriesCollection seriesCollection;
        private ModbusClient modbusClient;
        private const int MaxLogEntries = 100;
        private HashSet<string> activeAlarms = new HashSet<string>(); // Track active alarms


        #region Fault_Status
        private const int START_ADDRESS = 0;
        private const int REG_COUNT = 62;
        private const int FAULT2 = 0x2;
        private const int FAULT3 = 0x3;
        private const int FAULT4 = 0x4;

        //fault status 2
        Dictionary<int, string> lookupTableFaultStatus2 = new Dictionary<int, string>
        {
            { 1, "OVER_CHARGE_LEVEL1" },
            { 2, "CHARGE_OVERCURRENT_SECONDARY" },
            { 3, "DISCHARGE_LEVEL" },
            { 4, "DISCHARGE_FLOW_SECONDARY" },
            { 5, "SOC_LEVEL_ALARM" },
            { 6, "SOC_HIGH_SECONDARY" },
            { 7, "SOC_TOO_LOW_LEVEL1" },
            { 8, "SOC_TOO_LOW_LEVEL2" },
            { 9, "EXCESSIVE_PRESSSURE_DIFFERENCE" },
            { 10, "LEVEL_ALARM_SECONDARY" },
            { 11, "EXCESSIVE_TEMPERATURE_DIFFERENCE" },
            { 12, "SECONDARY_ALARM_EXCESSIVE_TEMP" }
        };

        Dictionary<int, string> lookupTableFaultStatus3 = new Dictionary<int, string>
        {
            { 1, "CHARGING_MOS_OVER_TEMP_WARNING" },
            { 2, "DISCHARGING_MOS_OVER_TEMP_WARNING" },
            { 3, "CHARGING_MOS_TEMP_SENSOR_FAULT" },
            { 4, "DISCHARGING_MOS_TEMP_SENSOR_FAULT" },
            { 5, "CHARGING_MOS_ADHESION_FAULT" },
            { 6, "DISCHARGING_MOS_ADHESION_FAULT" },
            { 7, "CHARGING_MOS_OPEN_CIRCUIT_FAULT" },
            { 8, "DISCHARGING_MOS_OPEN_CIRCUIT_FAULT" },
            { 9, "AFE_ACQUISITION_CHIP_FAILURE" },
            { 10, "SINGLE_LINE_COLLECTION" },
            { 11, "SINGLE_UNIT_TEMPERTURE_FAULT" },
            { 12, "EEPROM_STORAGE_FAULT" },
            { 13, "RTC_CLOCK_FAILURE" },
            { 14, "PRE_DISCHARGE_FAILED" }
        };

        Dictionary<int, string> lookupTableFaultStatus4 = new Dictionary<int, string>
        {
            { 1, "CURRENT_MODULE_FAULT" },
            { 2, "PRESSURE_MODULE_FAULT" },
            { 3, "SHORT_CIRCUIT_PROTECTION_FAULT" },
            { 4, "LOW_VOLTAGE_PROHIBITED" },
            { 5, "GPS_OR_SOFT_SWITCH_TURNED_OFF" },
            { 6, "CHARGING_CABINET_OFFLINE" }
        };

        #endregion

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
        private const int CHARGING_MOS = 53;
        private const int DISCHARGING_MOS = 54;
        private const int POWER = 57;
        private const int FAULT_STATUS_1 = 58;
        private const int FAULT_STATUS_2 = 59;
        private const int FAULT_STATUS_3 = 60;
        private const int FAULT_STATUS_4 = 61;

        private Int32 slaveID = 1;
        private bool readReady;

        private int serialNumber = 1; // Serial number for alarms


        private const int READ_COIL = 0x01;
        private const int READ_HOLDING_REGISTER = 0x03;
        private const int WRITE_REGISTER = 0x06;

        private const int READ_SERIAL_NUM = 99;

        private SerialPort serialPort;
        private string buffer = string.Empty;


        private Timer pollingTimer;
        private bool isPollingEnabled = false;

        public static DataTable staticDt = null;
        public DataTable fnpublic()
        {
            return staticDt;
        }
        private void InitializeModbusClient()
        {
            try
            {
                Logger.Info("MainParamaterForm/InitializeModbusClient| comPorts: " + comPorts.Text);
                modbusClient = new ModbusClient(comPorts.Text)
                {
                    Baudrate = 9600,//Convert.ToInt32(cbBaudRate1.SelectedItem),
                    Parity = System.IO.Ports.Parity.None,
                    StopBits = System.IO.Ports.StopBits.One,
                    UnitIdentifier = Convert.ToByte(slaveID), // Slave ID
                    ConnectionTimeout = 300//Convert.ToInt32(readingTimeOut.Value) // Timeout in milliseconds
                };

                statusConnection.Text = ("Connected");
               
                modbusClient.Connect();
                Logger.Info("MainParamaterForm/InitializeModbusClient| modbusClient.Connect()");
            }
            catch (Exception ex)
            {
                Logger.Error("MainParamaterForm/InitializeModbusClient| Exception: "+ ex.Message);
                string ss = ex.Message;
                string[] parts = ss.Split('\''); // Split by single quote
                string port = parts.Length > 1 ? parts[1] : string.Empty; // Get the second part (COM4)

                Console.WriteLine(port); // Outputs: COM4
                statusConnection.Text = ("Disconnected" + port + "is denied");
                infoMessages.Text = ("Error reading: " + "+ port: " + port + ex.Message);
            }
        }

        public MainParamatersForm(int num)
        {

        }

        private void CheckDatabaseConnection()
        {
            string statusMessage;
            bool isConnected = MainLogicClass.CheckConnection(out statusMessage);

            lblDbConnect.Text = statusMessage;
            lblDbConnect.ForeColor = isConnected ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }
        public MainParamatersForm()
        {
            try
            {
                InitializeComponent();
                Logger.Info("MainParametersForm/Constructor initialized.");
                CheckDatabaseConnection();
                RefreshComPortList();
                Logger.Info("MainParametersForm/Constructor RefreshComPortList");
                //Application.DoEvents();
                InitializePollingTimer();
                Logger.Info("MainParametersForm/Constructor InitializePollingTimer");

                dataList = new List<BatteryData>();

                minuteTimer = new Timer { Interval = 60000 }; // 1 minute interval
                minuteTimer.Tick += MinuteTimerTick;

                Paremeters();
                Logger.Info("MainParametersForm/Constructor Paremeters");
                SetupDataGridViewAlarm();
                Logger.Info("MainParametersForm/Constructor SetupDataGridViewAlarm");
                DataTable dt = SetupDataGridView();  //ParamatersDataTable
                Logger.Info("MainParametersForm/Constructor SetupDataGridView");
                btnTogglePolling1.Click += BtnTogglePolling_Click;
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
                Logger.Error("MainParametersForm/Constructor Exception: " + ex.Message);
            }

        }
        public void MainParamatersForm_Load(object sender, EventArgs e)
        {
            UpdateSettingFormTextbox();
        }

        public async Task UpdateSettingFormTextbox()
        {

            // textBoxText.Text = StaticModelValues.ForTestVar.ToString();
            // Value from staticModel to TextBox
            //Done
        }
        private void Paremeters()
        {
            try
            {

                StaticModel.MainParamatersForm_Parameters _Parameters = new StaticData().MainParamatersForm_ParametersFn();
                /* labelVolt.Text = _Parameters.Volt.ToString();
                 labelCurrent.Text = _Parameters.Current.ToString();
                 labelPower.Text = _Parameters.Power.ToString();
                 labelSoc.Text = _Parameters.Ah.ToString();
                 labelTemp.Text = _Parameters.Temp.ToString();*/
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }

        }
        //FUNCATIONS

        private void BtnTogglePolling_Click(object sender, EventArgs e)
        {
            try
            {
                isPollingEnabled = !isPollingEnabled;
                if (isPollingEnabled)
                {
                    isPollSelected = true;
                    btnTogglePolling1.BackColor = System.Drawing.Color.Gray;
                    InitializePollingTimer();
                    StartPolling();
                    StartPollingDatabase();
                }
                else
                {
                    isPollSelected = false;
                    btnTogglePolling1.BackColor = System.Drawing.Color.White;
                    StopPolling();
                    StopPollingDatabase();
                }
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }

        }



        public void StartPollingDatabase()
        {
            try
            {
                isPolling = true;
                btnTogglePolling1.Text = "Stop Reading";
                startTime = DateTime.Now;
                /*  secondTimer.Start(); */
                minuteTimer.Start();
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }
        }
        public void StopPollingDatabase()
        {
            try
            {
                isPolling = true;
                btnTogglePolling1.Text = "Start Reading";
                startTime = DateTime.Now;
                /*   secondTimer.Start();  */
                minuteTimer.Stop();
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }

        }


        int _lblDbInsertCount = 0;
        private void MinuteTimerTick(object sender, EventArgs e)
        {
            try
            {
                #region DataTable
                DataTable dataTable = new DataTable();
                // Add columns to the DataTable
                foreach (DataGridViewColumn column in dgvCellLevel.Columns)
                {
                    dataTable.Columns.Add(column.HeaderText);
                }
                // Add rows to the DataTable
                foreach (DataGridViewRow row in dgvCellLevel.Rows)
                {
                    // Skip the last empty row if AllowUserToAddRows is true
                    if (!row.IsNewRow)
                    {
                        if (row.Cells[0].Value == "Serial") { }
                        else if (row.Cells[0].Value == "FAULT-1")
                        {
                        }
                        else if (row.Cells[0].Value == "FAULT-2")
                        {
                        }
                        else if (row.Cells[0].Value == "FAULT-3")
                        {
                        }
                        else if (row.Cells[0].Value == "FAULT-4")
                        {
                        }
                        else if (row.Cells[0].Value == "FAULT-5")
                        {
                        }
                        else
                        {
                            DataRow dataRow = dataTable.NewRow();
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                dataRow[cell.ColumnIndex] = cell.Value;
                            }

                            dataTable.Rows.Add(dataRow);
                        }
                    }

                }
                #endregion
                CheckDatabaseConnection();
                new MainLogicClass().SaveDataToDatabase(dataTable);
                _lblDbInsertCount++;
                lblDbInsertCount.Text = _lblDbInsertCount.ToString();

                dataList.Clear();
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }
        }
        private const int MaxDataListSize = 1300; // Example limit
        private const int MaxChartValuesSize = 1300; // Example limit
        private void CaptureDataFromGridView()
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(CaptureDataFromGridView));
                    return;
                }
                foreach (DataGridViewRow row in dgvCellLevel.Rows)
                {
                    if (row.IsNewRow) continue;
                    double? TryConvertToDouble(object value)
                    {
                        if (value == null || value == DBNull.Value)
                        {
                            return null;
                        }
                        if (double.TryParse(value.ToString(), out double result))
                        {
                            return result;
                        }
                        return null;
                    }
                    var dataItem = new BatteryData
                    {
                        Timestamp = DateTime.Now,
                        Parameter = row.Cells["Parameter"].Value?.ToString()
                    };
                    bool hasValue = false;




                    double? battery1 = TryConvertToDouble(row.Cells["Battery1"].Value);
                    if (battery1.HasValue)
                    {
                        dataItem.Battery1 = battery1;
                        hasValue = true;
                    }
                    double? battery2 = TryConvertToDouble(row.Cells["Battery2"].Value);
                    if (battery2.HasValue)
                    {
                        dataItem.Battery2 = battery2;
                        hasValue = true;
                    }
                    double? battery3 = TryConvertToDouble(row.Cells["Battery3"].Value);
                    if (battery3.HasValue)
                    {
                        dataItem.Battery3 = battery3;
                        hasValue = true;
                    }
                    double? battery4 = TryConvertToDouble(row.Cells["Battery4"].Value);
                    if (battery4.HasValue)
                    {
                        dataItem.Battery4 = battery4;
                        hasValue = true;
                    }
                    double? battery5 = TryConvertToDouble(row.Cells["Battery5"].Value);
                    if (battery5.HasValue)
                    {
                        dataItem.Battery5 = battery5;
                        hasValue = true;
                    }
                    double? battery6 = TryConvertToDouble(row.Cells["Battery6"].Value);
                    if (battery5.HasValue)
                    {
                        dataItem.Battery6 = battery6;
                        hasValue = true;
                    }
                    double? battery7 = TryConvertToDouble(row.Cells["Battery7"].Value);
                    if (battery7.HasValue)
                    {
                        dataItem.Battery7 = battery7;
                        hasValue = true;
                    }
                    double? battery8 = TryConvertToDouble(row.Cells["Battery8"].Value);
                    if (battery8.HasValue)
                    {
                        dataItem.Battery8 = battery8;
                        hasValue = true;
                    }
                    double? battery9 = TryConvertToDouble(row.Cells["Battery9"].Value);
                    if (battery9.HasValue)
                    {
                        dataItem.Battery9 = battery9;
                        hasValue = true;
                    }
                    double? battery10 = TryConvertToDouble(row.Cells["Battery10"].Value);
                    if (battery10.HasValue)
                    {
                        dataItem.Battery10 = battery10;
                        hasValue = true;
                    }
                    double? battery11 = TryConvertToDouble(row.Cells["Battery11"].Value);
                    if (battery11.HasValue)
                    {
                        dataItem.Battery11 = battery11;
                        hasValue = true;
                    }
                    double? battery12 = TryConvertToDouble(row.Cells["Battery12"].Value);
                    if (battery12.HasValue)
                    {
                        dataItem.Battery12 = battery12;
                        hasValue = true;
                    }
                    double? battery13 = TryConvertToDouble(row.Cells["Battery13"].Value);
                    if (battery13.HasValue)
                    {
                        dataItem.Battery13 = battery13;
                        hasValue = true;
                    }
                    double? battery14 = TryConvertToDouble(row.Cells["Battery14"].Value);
                    if (battery14.HasValue)
                    {
                        dataItem.Battery14 = battery14;
                        hasValue = true;
                    }
                    double? battery15 = TryConvertToDouble(row.Cells["Battery15"].Value);
                    if (battery15.HasValue)
                    {
                        dataItem.Battery15 = battery15;
                        hasValue = true;
                    }
                    double? battery16 = TryConvertToDouble(row.Cells["Battery16"].Value);
                    if (battery16.HasValue)
                    {
                        dataItem.Battery16 = battery16;
                        hasValue = true;
                    }
                    double? battery17 = TryConvertToDouble(row.Cells["Battery17"].Value);
                    if (battery17.HasValue)
                    {
                        dataItem.Battery17 = battery17;
                        hasValue = true;
                    }
                    double? battery18 = TryConvertToDouble(row.Cells["Battery18"].Value);
                    if (battery18.HasValue)
                    {
                        dataItem.Battery18 = battery18;
                        hasValue = true;
                    }
                    double? battery19 = TryConvertToDouble(row.Cells["Battery19"].Value);
                    if (battery19.HasValue)
                    {
                        dataItem.Battery19 = battery19;
                        hasValue = true;
                    }
                    double? battery20 = TryConvertToDouble(row.Cells["Battery20"].Value);
                    if (battery20.HasValue)
                    {
                        dataItem.Battery20 = battery20;
                        hasValue = true;
                    }


                    if (hasValue)
                    {
                        dataList.Add(dataItem);
                        if (dataList.Count > MaxDataListSize)
                        {
                            dataList.RemoveAt(0); // Remove the oldest entry
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                JIMessageBox.ErrorMessage(ex.Message);
            }
        }

        private void InitializePollingTimer()
        {
            try
            {
                pollingTimer = new Timer();
                pollingTimer.Interval = 1000;// Convert.ToInt32(pollingTimeout.Value); // Polling interval in milliseconds (1 second here)
                pollingTimer.Tick += PollingTimer_Tick;
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }
        }
        private void StartPolling()
        {
            pollingTimer.Start();
        }
        private void StopPolling()
        {
            try
            {
                pollingTimer.Stop();
                if (modbusClient.Connected)
                {
                    modbusClient.Disconnect();
                }
            }
            catch (Exception ex)
            {

                JIMessageBox.ErrorMessage(ex.Message);
            }
        }
        static ChartValues<double> lstvoltage = new ChartValues<double>();
        static ChartValues<double> lstcurrent = new ChartValues<double>();
        static ChartValues<double> lstpower = new ChartValues<double>();
        static List<ChartValues<double>> allLists = new List<ChartValues<double>>();
        //List<string> XAxisValue = new List<string>() { "Voltage (V)", "Current (Amps)", "Power (kW)", "SOC Power" };
        public static bool isPollSelected = false;
        public void PollingTimer_Tick(object sender, EventArgs e)
        {
            ushort temp;
            readReady = false;
            #region Data Reading
            try
            {
                if (slaveID > moduleCount.Value || slaveID == 0)
                {
                    if (slaveID != 0)
                        AlarmCheck();
                    slaveID = 1;

                }
                LoadModbusData(slaveID, READ_HOLDING_REGISTER, 0xB9, 11);
                slaveID++;

                string[] avgVoltage = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                for (int z = 1; z <= 20; z++)
                {
                    var cellValue = dgvCellLevel.Rows[1].Cells[z]?.Value?.ToString();
                    if (string.IsNullOrEmpty(cellValue) || cellValue == "-")
                        break;
                    avgVoltage[z] = cellValue;
                    double[] doubleArray = Array.ConvertAll(avgVoltage, double.Parse);
                    double average = Math.Round(doubleArray.Where(x => x != 0).Average(), 1);
                    labelVolt.Text = average.ToString();
                }

                string[] totalCurrent = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                for (int z = 1; z <= 20; z++)
                {
                    var cellValue = dgvCellLevel.Rows[2].Cells[z]?.Value?.ToString();
                    if (string.IsNullOrEmpty(cellValue) || cellValue == "-")
                        break;
                    totalCurrent[z] = cellValue;
                    double[] doubleArray = Array.ConvertAll(totalCurrent, double.Parse);
                    double sum = Math.Round(doubleArray.Where(x => x != 0).Sum(), 1);
                    labelCurrent.Text = sum.ToString();
                }

                string[] totalPower = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                for (int z = 1; z <= 20; z++)
                {
                    var cellValue = dgvCellLevel.Rows[3].Cells[z]?.Value?.ToString();
                    if (string.IsNullOrEmpty(cellValue) || cellValue == "-")
                        break;
                    totalPower[z] = cellValue;
                    double[] doubleArray = Array.ConvertAll(totalPower, double.Parse);
                    double sum = Math.Round(doubleArray.Where(x => x != 0).Sum(), 1);
                    labelPower.Text = sum.ToString();
                }

                string[] avgSOC = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                for (int z = 1; z <= 20; z++)
                {
                    var cellValue = dgvCellLevel.Rows[4].Cells[z]?.Value?.ToString();
                    if (string.IsNullOrEmpty(cellValue) || cellValue == "-")
                        break;
                    avgSOC[z] = cellValue;
                    double[] doubleArray = Array.ConvertAll(avgSOC, double.Parse);
                    double average = Math.Round(doubleArray.Where(x => x != 0).Average(), 1);
                    labelSoc.Text = average.ToString();
                }
                string[] avgTemp = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                for (int z = 1; z <= 20; z++)
                {
                    var cellValue = dgvCellLevel.Rows[6].Cells[z]?.Value?.ToString();
                    if (string.IsNullOrEmpty(cellValue) || cellValue == "-")
                        break;
                    avgTemp[z] = cellValue;
                    double[] doubleArray = Array.ConvertAll(avgTemp, double.Parse);
                    double average = Math.Round(doubleArray.Where(x => x != 0).Average(), 1);
                    labelTemp.Text = average.ToString();
                }

                #endregion

                #region ForChart
                if ((string.IsNullOrEmpty(labelVolt.Text) || labelVolt.Text != "-") ||
                    (string.IsNullOrEmpty(labelCurrent.Text) || labelCurrent.Text != "-") ||
                    (string.IsNullOrEmpty(labelPower.Text) || labelPower.Text != "-"))
                {
                    // Add new data points to existing lists
                    lstvoltage.Add(Convert.ToDouble(labelVolt.Text));
                    lstcurrent.Add(Convert.ToDouble(labelCurrent.Text));
                    lstpower.Add(Convert.ToDouble(labelPower.Text));

                    // Update allLists with the updated data
                    allLists.Clear();
                    allLists.Add(new ChartValues<double>(lstvoltage));
                    allLists.Add(new ChartValues<double>(lstcurrent));
                    allLists.Add(new ChartValues<double>(lstpower));

                    TrimChartValues(lstvoltage);
                    TrimChartValues(lstcurrent);
                    TrimChartValues(lstpower);
                    new ChartSet().chartGT3(allLists, cartesianChart1);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiles section error: " + ex.Message);
            }
        }
        List<double> lst;
        string dateTime;
        Int32 modbusStartReg = START_ADDRESS;
        Int32 modbusRegCount = REG_COUNT;
        Int32 modbusTotalReg;
        Int16 index;
        Int32 searchValue;
        Int32 searchRange;
        Int32[] refValue = { 0, 0, 0, 0 };
        int[] registers = { 0 };
        bool[] boolsRegister = { false };
        bool searchRegisterFlag = false;
        int z;
        double voltage, current, soc, power, temp, ah, avgcell, cell1, cell2, cell3, cell4, cell5, cell6, cell7, 
                cell8, cell9, cell10, cell11, cell12, cell13, cell14, cell15, fault1, fault2, fault3, fault4, 
                maxCell, minCell, diffCell, CMOS, DMOS, EMOS, Safety;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void commType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewAlarm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }


        Random rnd;
        public List<ChartValues<double>> LoadModbusData(Int32 batteryIndex, int functionCode, int registerNumber, int data)
        {
            string serialNumberString = "-";
            try
            {
                lst = new List<double>();
                dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Logger.Info("MainParamaterForm/LoadModbusData| batteryIndex: " + batteryIndex.ToString() + " SERIAL_READ: " + functionCode.ToString() + " registerNumber: "+ registerNumber  + " data: "+ data.ToString());

                modbusTotalReg = modbusRegCount + modbusStartReg;
                Logger.Info("MainParamaterForm/LoadModbusData| modbusTotalReg: " + modbusTotalReg.ToString());

                index = 0;
                searchValue = 0;
                searchRange = 0;
                string hexString;
                z = 0;
                //int[] arr = new int[] { 185 };  //AQIBCODESTATIC arr  //Comment
                // for (batteryIndex = 1; batteryIndex <= numMobuleCount.Value; batteryIndex++)   //UnComment numMobuleCount.Value
                {
                    // if (readReady == true)
                    {
                        numSlaveID.Text = batteryIndex.ToString();
                        slaveID = batteryIndex;
                        StaticModelValues.slaveId1 = Convert.ToInt32(numSlaveID.Text);
                        Logger.Info("MainParamaterForm/LoadModbusData| slaveId1: " + StaticModelValues.slaveId1.ToString());
                        try
                        {
                            InitializeModbusClient();
                            statusConnection.Text = ("Connected");
                            try
                            {
                                Logger.Info("MainParamaterForm/LoadModbusData| functionCode: " + functionCode);
                                switch (functionCode)
                                {//StaticComment
                                    case 1:
                                        boolsRegister = modbusClient.ReadCoils(modbusStartReg, modbusRegCount);// Read Coils (0x01)
                                        Logger.Info("MainParamaterForm/LoadModbusData| boolsRegister: " + boolsRegister.ToString());
                                        break;
                                    case 2:
                                        boolsRegister = modbusClient.ReadDiscreteInputs(modbusStartReg, modbusRegCount);// Read Discrete Inputs (0x02)
                                        Logger.Info("MainParamaterForm/LoadModbusData| boolsRegister: " + boolsRegister.ToString());
                                        break;
                                    case 3:
                                        registers = modbusClient.ReadHoldingRegisters(modbusStartReg, modbusRegCount);    //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| boolsRegister: " + boolsRegister.ToString());
                                        int[] serialNumberRaw = modbusClient.ReadHoldingRegisters(registerNumber, data);  //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| serialNumberRaw.Count: " + serialNumberRaw.Count().ToString());
                                        //registers = arr;
                                        //int[] serialNumberRaw = registers;



                                        hexString = string.Join("", Array.ConvertAll(serialNumberRaw, val => val.ToString("X")));   //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| hexString: " + hexString.ToString());

                                        serialNumberString = ConvertHexStringToAscii(hexString);    //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| serialNumberString: " + serialNumberString.ToString());
                                        #region StaticDataByAQIB
                                        //rnd = new Random();
                                        //voltage = rnd.Next(0, 1000) * 0.1;
                                        //current = (rnd.Next(0, 1000) - 30000) * 0.1;
                                        //power = (rnd.Next(0, 1000));
                                        //soc = (rnd.Next(0, 1000)) * 0.1;
                                        //ah = (rnd.Next(0, 1000)) * 0.1;
                                        //temp = (rnd.Next(0, 1000)) - 40;
                                        //cell1 = (rnd.Next(0, 1000)) * 0.001;
                                        //cell2 = (rnd.Next(0, 1000)) * 0.001;
                                        //cell3 = (rnd.Next(0, 1000)) * 0.001;
                                        //cell4 = (rnd.Next(0, 1000)) * 0.001;
                                        //cell5 = (rnd.Next(0, 1000)) * 0.001;
                                        //cell6 = (rnd.Next(0, 1000)) * 0.001;
                                        //cell7 = (rnd.Next(0, 1000)) * 0.001;
                                        //cell8 = (rnd.Next(0, 1000)) * 0.001;
                                        //cell9 = (rnd.Next(0, 1000)) * 0.001;
                                        //cell10 = (rnd.Next(0, 1000)) * 0.001;
                                        //cell11 = (rnd.Next(0, 1000)) * 0.001;
                                        //cell12 = (rnd.Next(0, 1000)) * 0.001;
                                        //cell13 = (rnd.Next(0, 1000)) * 0.001;
                                        //cell14 = (rnd.Next(0, 1000)) * 0.001;
                                        //cell15 = (rnd.Next(0, 1000)) * 0.001;
                                        #endregion

                                        #region DynamicData
                                        voltage = (registers[VOLTAGE]) * 0.1;                  //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| voltage: " + voltage.ToString());
                                        current = (registers[CURRENT] - 30000) * 0.1;          //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| current: " + current.ToString());
                                        power = (registers[POWER]);                            //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| power: " + current.ToString());
                                        soc = (registers[SOC]) * 0.1;                          //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| soc: " + soc.ToString());
                                        ah = (registers[AH]) * 0.1;                            //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| ah " + ah.ToString());
                                        temp = (registers[TEMP]) - 40;                         //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| temp: " + temp.ToString());
                                        cell1 = (registers[CELL1]) * 0.001;                    //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell1: " + cell1.ToString());
                                        cell2 = (registers[CELL2]) * 0.001;                    //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell2: " + cell2.ToString());
                                        cell3 = (registers[CELL3]) * 0.001;                    //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell3: " + cell3.ToString());
                                        cell4 = (registers[CELL4]) * 0.001;                    //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell4: " + cell4.ToString());
                                        cell5 = (registers[CELL5]) * 0.001;                    //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell5: " + cell5.ToString());
                                        cell6 = (registers[CELL6]) * 0.001;                    //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell6: " + cell6.ToString());
                                        cell7 = (registers[CELL7]) * 0.001;                    //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell7: " + cell7.ToString());
                                        cell8 = (registers[CELL8]) * 0.001;                    //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell8: " + cell8.ToString());
                                        cell9 = (registers[CELL9]) * 0.001;                    //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell9: " + cell9.ToString());
                                        cell10 = (registers[CELL10]) * 0.001;                  //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell10: " + cell10.ToString());
                                        cell11 = (registers[CELL11]) * 0.001;                  //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell11: " + cell11.ToString());
                                        cell12 = (registers[CELL12]) * 0.001;                  //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell12: " + cell12.ToString());
                                        cell13 = (registers[CELL13]) * 0.001;                  //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell13: " + cell13.ToString());
                                        cell14 = (registers[CELL14]) * 0.001;                  //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell14: " + cell14.ToString());
                                        cell15 = (registers[CELL15]) * 0.001;                  //uncomment
                                        Logger.Info("MainParamaterForm/LoadModbusData| cell15: " + cell15.ToString());
                                        fault1 = (registers[FAULT_STATUS_1]);
                                        Logger.Info("MainParamaterForm/LoadModbusData| fault1: " + fault1.ToString());
                                        fault2 = (registers[FAULT_STATUS_2]);
                                        Logger.Info("MainParamaterForm/LoadModbusData| fault2: " + fault2.ToString());
                                        fault3 = (registers[FAULT_STATUS_3]);
                                        Logger.Info("MainParamaterForm/LoadModbusData| fault3: " + fault3.ToString());
                                        fault4 = (registers[FAULT_STATUS_4]);
                                        Logger.Info("MainParamaterForm/LoadModbusData| fault4: " + fault4.ToString());

                                        maxCell = FindMax(cell1, cell2, cell3, cell4, cell5, cell6, cell7, cell8, cell9, cell10, cell11, cell12, cell13, cell14, cell15);
                                        minCell = FindMin(cell1, cell2, cell3, cell4, cell5, cell6, cell7, cell8, cell9, cell10, cell11, cell12, cell13, cell14, cell15);
                                        diffCell = Math.Abs(maxCell - minCell);
                                        #endregion

                                        #region CMOS & DMOS reading
                                        string mos=(registers[CHARGING_MOS])+"/"+ (registers[DISCHARGING_MOS]);

                                        #endregion

                                        #region dynamicToGrid
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = serialNumberString;
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(voltage, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(current, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(power, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(soc, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(ah, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(temp, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell1, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell2, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell3, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell4, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell5, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell6, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell7, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell8, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell9, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell10, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell11, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell12, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell13, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell14, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(cell15, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = fault1;
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = fault2;
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = fault3;
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = fault4;
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(maxCell, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(minCell, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = Math.Round(diffCell, 2);
                                        dgvCellLevel.Rows[z++].Cells[batteryIndex].Value = mos; 
                                        var w = dgvCellLevel.Rows[23].Cells[batteryIndex].Value;
                                        int c = Convert.ToInt32(dgvCellLevel.Rows[23].Cells[batteryIndex].Value);//int.Parse(w.ToString());
                                        #endregion
                                        Logger.Info("MainParamaterForm/LoadModbusData| Grid complete ");

                                        #region FAULTS / ALARMS
                                        z = 0;//fault 2
                                              //CheckAndLogAlarms(batteryIndex,(short)registers[FAULT_STATUS_2]);

                                        #endregion

                                        staticDt = MainParamatersFormPartial.ConvertDataGridViewToDataTable(dgvCellLevel);
                                        Logger.Info("MainParamaterForm/LoadModbusData| staticDt " + staticDt.ToString());
                                        fnpublic();
                                        break;
                                    case 4:
                                        registers = modbusClient.ReadInputRegisters(modbusStartReg, modbusRegCount);// Read Input Registers (0x04)
                                        Logger.Info("MainParamaterForm/LoadModbusData| registers " + registers.ToString());
                                        break;
                                    case 6:
                                        //modbusClient.WriteSingleRegister(7485, 2345);
                                        modbusClient.WriteSingleRegister(registerNumber, data);
                                        //modbusClient.WriteSingleRegister(Convert.ToInt32(StaticModelValues.tbLowCellVolt), Convert.ToInt32(StaticModelValues.tbHighCellVolt));
                                        Logger.Info("MainParamaterForm/LoadModbusData| registerNumber " + registerNumber.ToString() + " data: "+ data.ToString());
                                        Logger.Info("MainParamaterForm/LoadModbusData| registers " + modbusClient.ToString());
                                        break;
                                    case 0x50:
                                        modbusClient.Disconnect();
                                        Logger.Info("MainParamaterForm/LoadModbusData| modbusClient.Disconnect() " + modbusClient.ToString());
                                        
                                        InitializeSerialPort();
                                        Logger.Info("MainParamaterForm/LoadModbusData| InitializeSerialPort()");
                                        string FixedFrameID = "A5401908";
                                        Logger.Info("MainParamaterForm/LoadModbusData|  FixedFrameID");
                                        short value4 = (short)registerNumber;
                                        Logger.Info("MainParamaterForm/LoadModbusData|  value4:"+ value4.ToString());
                                        short value3 = (short)registerNumber;
                                        Logger.Info("MainParamaterForm/LoadModbusData|  value3:" + value3.ToString());
                                        short value2 = (short)data;
                                        Logger.Info("MainParamaterForm/LoadModbusData|  value2:" + value2.ToString());
                                        short value1 = (short)data;
                                        Logger.Info("MainParamaterForm/LoadModbusData|  value2:" + value1.ToString());
                                        byte[] canData = new byte[8];
                                        Array.Copy(SwapBytes(BitConverter.GetBytes(value1)), 0, canData, 0, 2);
                                        Array.Copy(SwapBytes(BitConverter.GetBytes(value2)), 0, canData, 2, 2);
                                        Array.Copy(SwapBytes(BitConverter.GetBytes(value3)), 0, canData, 4, 2);
                                        Array.Copy(SwapBytes(BitConverter.GetBytes(value4)), 0, canData, 6, 2);
                                        Logger.Info("MainParamaterForm/LoadModbusData| Array.Copy");
                                        // Create the CAN packet
                                        byte[] canPacket = CreateCANPacket(FixedFrameID, canData);
                                        
                                        
                                        // Display the resulting CAN packet
                                        //canPkt.Text = "Generated CAN Packet: " + BitConverter.ToString(canPacket).Replace("-", "");
                                        SendCANPacket(canPacket);
                                        string tmpp = BitConverter.ToString(canPacket).Replace("-", string.Empty);
                                        Logger.Info("MainParamaterForm/LoadModbusData|  canPacket:"+ tmpp);
                                       /* try
                                        {
                                            serialPort.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.Error("MainParamaterForm/LoadModbusData|  SendCANPacket Exception:" + ex.Message.ToString());
                                            JIMessageBox.ErrorMessage(ex.Message);
                                        }*/
                                        break;
                                    case 98://load setting page data points
                                        #region AqibComentAgain
                                        ////////////////////int[] arr1 = new int[] { 128 };
                                        ////////////////////registers = arr1;
                                        #endregion
                                        registers = modbusClient.ReadHoldingRegisters(registerNumber, data);
                                        Logger.Info("MainParamaterForm/LoadModbusData|  registers:" + registers.ToString());
                                        StaticModelValues.register_Settings = registers;
                                        break;
                                    case 99://load serial number
                                        #region AqibComentAgain

                                        ////////////////////registers = arr;
                                        #endregion
                                        registers = modbusClient.ReadHoldingRegisters(registerNumber, data);
                                        Logger.Info("MainParamaterForm/LoadModbusData|  registers:" + registers.ToString());
                                        StaticModelValues.register_Settings = registers;
                                        hexString = string.Join("", Array.ConvertAll(registers, val => val.ToString("X")));
                                        serialNumberString = ConvertHexStringToAscii(hexString);
                                        Logger.Info("MainParamaterForm/LoadModbusData|  serialNumberString:" + serialNumberString.ToString());
                                        break;
                                    case 100://values writing function from settings page
                                        modbusClient.WriteSingleRegister(registerNumber, data);
                                        Logger.Info("MainParamaterForm/LoadModbusData|  modbusClient:" + modbusClient.ToString());
                                        registers = modbusClient.ReadHoldingRegisters(registerNumber, 1);// Read Holding Registers (0x03)
                                        Logger.Info("MainParamaterForm/LoadModbusData|  modbusClient:" + registers.ToString());
                                        StaticModelValues.tbLowCellVolt = registers[0];
                                        Logger.Info("MainParamaterForm/LoadModbusData|  registers[0]:" + registers[0].ToString());
                                        break;
                                    default:
                                        Logger.Info("MainParamaterForm/LoadModbusData|  Unsupported function code.");
                                        throw new InvalidOperationException("Unsupported function code.");
                                      
                                }

                                z = 0;
                                infoMessages.Text = ("Reading Successful");

                                avgcell = ((cell1 + cell2 + cell3 + cell4 + cell5 + cell6 + cell7 + cell8 + cell9 + cell10 + cell11 + cell12 + cell13 + cell14) / 14);
                                labelVolt.Text = voltage.ToString("0.0");
                                labelCurrent.Text = current.ToString("0.0");
                                labelPower.Text = power.ToString("0.0");
                                labelTemp.Text = temp.ToString("0.0");
                                labelSoc.Text = soc.ToString("0.0");
                            }
                            catch (Exception ex)
                            {
                                Logger.Error("MainParamaterForm/LoadModbusData|  Exception:" + ex.Message.ToString());
                                int[] tmp = { 0, 0 };
                                infoMessages.BackColor = System.Drawing.Color.Red;   //uncomment
                                infoMessages.ForeColor = System.Drawing.Color.White;
                                infoMessages.Text = ("Error reading Modbus data: " + ex.Message);
                                StaticModelValues.register_Settings = tmp;
                                for (z = 0; z <= 18; z++)
                                    dgvCellLevel.Rows[z].Cells[batteryIndex].Value = "-";
                                labelVolt.Text = "-";
                                labelCurrent.Text = "-";
                                labelPower.Text = "-";
                                labelTemp.Text = "-";

                                labelSoc.Text = "-";
                            }
                            finally   //uncomment
                            {
                                if (modbusClient.Connected)
                                {
                                    modbusClient.Disconnect(); // Disconnect from Modbus server
                                    statusConnection.Text = "Disconnected";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("MainParamaterForm/LoadModbusData|  Outer Exception:" + ex.Message.ToString());
                            //skip if no slave found
                        }
                        readReady = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error("MainParamaterForm/LoadModbusData|  Main Exception:" + ex.Message.ToString());
                JIMessageBox.ErrorMessage(ex.Message);
            }

            return allLists;
        }
        static double FindMax(params double[] numbers)
        {
            return numbers.Max();
        }

        static double FindMin(params double[] numbers)
        {
            return numbers.Min();
        }
        public static string ConvertHexStringToAscii(string hexString)
        {
            if (hexString.Length % 2 != 0)
                throw new ArgumentException("Hex string must have an even number of characters");

            char[] asciiChars = new char[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
            {
                string hexPair = hexString.Substring(i, 2);
                byte byteValue = Convert.ToByte(hexPair, 16);
                asciiChars[i / 2] = (char)byteValue;
            }

            return new string(asciiChars);
        }
        private void TrimChartValues(ChartValues<double> chartValues)
        {
            while (chartValues.Count > MaxChartValuesSize)
            {
                chartValues.RemoveAt(0); // Remove the oldest entry
            }
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (commType.Text == "MODBUS")   //uncomment
            {
                try
                {
                    DataTable dt = SetupDataGridView();  //GetPatamatersHeader
                    allLists = LoadModbusData(1, 3, 0, 0);
                    #region DataTable
                    DataTable dataTable = new DataTable();
                    // Add columns to the DataTable
                    foreach (DataGridViewColumn column in dgvCellLevel.Columns)
                    {
                        dataTable.Columns.Add(column.HeaderText);
                    }
                    // Add rows to the DataTable
                    foreach (DataGridViewRow row in dgvCellLevel.Rows)
                    {
                        // Skip the last empty row if AllowUserToAddRows is true
                        if (!row.IsNewRow)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            foreach (DataGridViewCell cell in row.Cells)
                            {

                                ////dgvCellLevel.Rows[0].Cells[5].Value = 1;
                                dataRow[cell.ColumnIndex] = cell.Value;

                            }
                            dataTable.Rows.Add(dataRow);
                        }

                    }
                    #endregion
                    CaptureDataFromGridView();
                    new MainLogicClass().SaveDataToDatabase(dataTable);   //Single
                    dataList.Clear();
                    new ChartSet().chartGT3(allLists, cartesianChart1);//, XAxisValue);
                }
                catch (Exception ex)
                {
                    JIMessageBox.ErrorMessage(ex.Message);
                }
            }  //uncomment
            else
            {
                JIMessageBox.WarningMessage("CAN BUS implementation in progress...");
            }
        }
        private void RefreshComPortList()
        {
            try
            {
                comPorts.Items.Clear();

                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    comPorts.Items.Add(port);
                }
                // Select the first item if the list is not empty
                if (comPorts.Items.Count > 0)
                {
                    commType.SelectedIndex = 0;
                    comPorts.SelectedIndex = comPorts.Items.Count - 1;
                }
                else
                {
                    infoMessages.Text = "No COM ports found.";//, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }

        }

        private void SetupDataGridViewAlarm()
        {
            DataTable dt;
            try
            {
                dt = new StaticData().DataGridViewAlarm();  //DynamicData  change your Data here......
                Logger.Info("DataGridViewAlarm| StaticData");
                dataGridViewAlarm.Rows.Clear();
                foreach (DataRow r in dt.Rows)
                {
                    dataGridViewAlarm.Rows.Add(
                        r["SNo"].ToString(), r["Alarm"].ToString());
                }
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
                Logger.Error("DataGridViewAlarm| Exception: " + ex.Message);
                dt = null;
            }


        }

        private DataTable SetupDataGridView()
        {
            DataTable dt;
            try
            {
                dt = new StaticData().DataGridView();  //Get datafrm dataTable and add GridViewColumn1 Values
                dgvCellLevel.Rows.Clear();
                foreach (DataRow r in dt.Rows)
                {
                    dgvCellLevel.Rows.Add(
                        r["Parameter"].ToString(), r["Battery1"].ToString(), r["Battery2"].ToString(), r["Battery3"].ToString(), r["Battery4"].ToString(), r["Battery5"].ToString()
                        , r["Battery6"].ToString(), r["Battery7"].ToString(), r["Battery8"].ToString(), r["Battery9"].ToString(), r["Battery10"].ToString(), r["Battery11"].ToString()
                        , r["Battery12"].ToString(), r["Battery13"].ToString(), r["Battery14"].ToString(), r["Battery15"].ToString(), r["Battery16"].ToString(), r["Battery17"].ToString()
                        , r["Battery18"].ToString(), r["Battery19"].ToString(), r["Battery20"].ToString());
                }
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
                dt = null;
            }

            return dt;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            //DataTable dt = new StaticData().DataGridView();
        }


        private void InitializeSerialPort()
        {
            try
            {
                serialPort = new SerialPort(comPorts.Text, 9600, Parity.None, 8, StopBits.One);
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                serialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading: " + ex.Message);
                serialPort.Close();
            }
            

        }

        private void SendCANPacket(byte[] packet)
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    serialPort.Write(packet, 0, packet.Length);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending setpoint command: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Serial port is not open.");
            }
        }
        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = (SerialPort)sender;
            string inData = serialPort.ReadExisting();

            Logger.Info("MainParamaterForm/DataReceivedHandler|  canPacket Ack:" + inData);
            try
            {
                serialPort.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("MainParamaterForm/DataReceivedHandler|  SendCANPacket Exception:" + ex.Message.ToString());
                JIMessageBox.ErrorMessage(ex.Message);
            }

        }

        private byte[] CreateCANPacket(string identifier, byte[] data)
        {
            if (identifier.Length != 8)
            {
                throw new ArgumentException("Identifier must be 8 characters long.");
            }

            if (data.Length > 8)
            {
                throw new ArgumentException("Data length exceeds the maximum 8 bytes for CAN.");
            }

            // Convert the identifier from hex string to bytes
            byte[] identifierBytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                identifierBytes[i] = Convert.ToByte(identifier.Substring(i * 2, 2), 16);
            }

            // Combine identifier and data for checksum calculation
            byte[] dataForChecksum = new byte[identifierBytes.Length + data.Length];
            Array.Copy(identifierBytes, 0, dataForChecksum, 0, identifierBytes.Length);
            Array.Copy(data, 0, dataForChecksum, identifierBytes.Length, data.Length);

            // Calculate the checksum over the combined identifier and data
            byte checksum = CalculateChecksum8(dataForChecksum);

            // Combine identifier, data, and checksum into one packet
            byte[] packet = new byte[identifierBytes.Length + data.Length + 1];
            Array.Copy(identifierBytes, 0, packet, 0, identifierBytes.Length);
            Array.Copy(data, 0, packet, identifierBytes.Length, data.Length);
            packet[packet.Length - 1] = checksum;

            return packet;
        }

        private static byte CalculateChecksum8(byte[] data)
        {
            int sum = 0;
            foreach (byte b in data)
            {
                sum += b;
            }
            return (byte)(sum % 256);
        }
        private byte[] SwapBytes(byte[] bytes)
        {
            if (bytes.Length != 2)
                throw new ArgumentException("Only 2-byte arrays are supported for swapping.");

            return new byte[] { bytes[1], bytes[0] };
        }
        #region alarms display section
        private void LogAlarm(int batteryIndex, int bitIndex, int faultNum)
        {
            string alarmID = $"{faultNum}{batteryIndex}{bitIndex}";
            //string alarmID = $"{batteryIndex}{bitIndex}";
            string result = "0";
            int intAlarmID;
            try
            {
                intAlarmID = Convert.ToInt32(alarmID);
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }
            // Check if the alarm is already logged
            if (activeAlarms.Contains(alarmID))
            {
                return; // Skip logging if already exists
            }
            if (faultNum == 2)
                lookupTableFaultStatus2.TryGetValue(bitIndex, out result);
            else if (faultNum == 3)
                lookupTableFaultStatus3.TryGetValue(bitIndex, out result);
            else if (faultNum == 4)
                lookupTableFaultStatus4.TryGetValue(bitIndex, out result);

            activeAlarms.Add(alarmID);

            string occurrenceTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string alarmDescription = $"Battery-{batteryIndex}: {result}";
            dataGridViewAlarm.Rows.Add(alarmID, alarmDescription, occurrenceTime);
            dataGridViewAlarm.FirstDisplayedScrollingRowIndex = dataGridViewAlarm.RowCount - 1;
        }

        private void ClearAlarm(int variableIndex, int bitIndex, int faultNum)
        {
            string alarmID = $"{faultNum}{variableIndex}{bitIndex}";
            if (!activeAlarms.Contains(alarmID))
            {
                return; // Skip if the alarm is not active
            }
            activeAlarms.Remove(alarmID);
            foreach (DataGridViewRow row in dataGridViewAlarm.Rows)
            {
                if (row.Cells["ID"].Value != null && row.Cells["ID"].Value.ToString().Contains(alarmID))
                {
                    dataGridViewAlarm.Rows.Remove(row);
                    break;
                }
            }
        }

        private void CheckAndLogAlarms()
        {
            short currentVariable2 = 0, currentVariable3 = 0, currentVariable4 = 0;
            for (int i = 1; i <= moduleCount.Value; i++)
            {
                try
                {
                    if (short.TryParse(dgvCellLevel.Rows[23].Cells[i].Value?.ToString(), out currentVariable2)) { }
                    else { currentVariable2 = 0; }
                    if (short.TryParse(dgvCellLevel.Rows[24].Cells[i].Value?.ToString(), out currentVariable3)) { }
                    else { currentVariable3 = 0; }
                    if (short.TryParse(dgvCellLevel.Rows[25].Cells[i].Value?.ToString(), out currentVariable4)) { }
                    else { currentVariable4 = 0; }

                    for (int bitIndex = 0; bitIndex < 16; bitIndex++)
                    {
                        if ((currentVariable2 & (1 << bitIndex)) != 0)
                        {
                            //LogAlarm(i, bitIndex + 1,faultNum);
                            LogAlarm(i, bitIndex + 1, 2);
                        }
                        else
                        {
                            ClearAlarm(i, bitIndex + 1, 2);
                        }
                        if ((currentVariable3 & (1 << bitIndex)) != 0)
                        {
                            //LogAlarm(i, bitIndex + 1,faultNum);
                            LogAlarm(i, bitIndex + 1, 3);
                        }
                        else
                        {
                            ClearAlarm(i, bitIndex + 1, 3);
                        }
                        if ((currentVariable4 & (1 << bitIndex)) != 0)
                        {
                            //LogAlarm(i, bitIndex + 1,faultNum);
                            LogAlarm(i, bitIndex + 1, 4);
                        }
                        else
                        {
                            ClearAlarm(i, bitIndex + 1, 4);
                        }
                    }
                }
                catch (Exception ex)
                {
                    JIMessageBox.ErrorMessage(ex.Message);
                }
            }

        }
        #endregion

        private void AlarmCheck()
        {
            int z = 1;
            for (int batteryIndex = 1; batteryIndex <= 20; batteryIndex++)
            {
                CheckAndLogAlarms();
            }
        }

    }


}
