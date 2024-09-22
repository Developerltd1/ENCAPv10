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
using System.Text.RegularExpressions;
using Color = System.Drawing.Color;
//System.Windows.Forms
namespace EMView.UI
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

        //  public static DataTable staticDt = null;
        private DataTable _table;
        public DataTable fnpublic()
        {
            return _table;
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
                Logger.Error("MainParamaterForm/InitializeModbusClient| Exception: " + ex.Message);
                string ss = ex.Message;
                string[] parts = ss.Split('\''); // Split by single quote
                string port = parts.Length > 1 ? parts[1] : string.Empty; // Get the second part (COM4)

                Console.WriteLine(port); // Outputs: COM4
                statusConnection.Text = ("Disconnected" + port + "is denied");
                infoMessages.Text = ("Error reading: " + "+ port: " + port + ex.Message);
            }
        }
        private void InitializeModbusClientAsync()
        {
            try
            {

                try
                {
                    Logger.Info("MainParamaterForm/InitializeModbusClient| comPorts: " + comPorts.Text);
                    // Check if the port is already connected and disconnect if necessary
                    if (modbusClient != null && modbusClient.Connected)
                    {
                        modbusClient.Disconnect();
                    }

                    // Reinitialize the Modbus client
                    modbusClient = new ModbusClient(comPorts.Text)
                    {
                        Baudrate = 9600,
                        Parity = System.IO.Ports.Parity.None,
                        StopBits = System.IO.Ports.StopBits.One,
                        UnitIdentifier = Convert.ToByte(slaveID),
                        ConnectionTimeout = 500 // Timeout in milliseconds
                    };

                    // Attempt to connect asynchronously
                    //await Task.Run(() => modbusClient.Connect());
                    modbusClient.Connect();
                    Logger.Info("MainParamaterForm/InitializeModbusClient| modbusClient.Connect()");

                }
                catch (Exception ex)
                {
                    Logger.Error("MainParamaterForm/InitializeModbusClient| Exception: " + ex.Message);
                    string ss = ex.Message;
                    string[] parts = ss.Split('\''); // Split by single quote
                    string port = parts.Length > 1 ? parts[1] : string.Empty; // Get the second part (COM4)

                    Console.WriteLine(port); // Outputs: COM4

                    // Update UI elements on the UI thread
                    this.Invoke(new Action(() =>
                    {
                        statusConnection.Text = $"Disconnected {port} is denied";
                        infoMessages.Text = $"Error reading: port: {port} {ex.Message}";
                    }));
                }
                finally
                {
                    // Always ensure to close the port if it was opened
                    //if (modbusClient != null && modbusClient.Connected)
                    //{
                    //    modbusClient.Disconnect();
                    //}
                }

            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur in the Task.Run method itself
                Logger.Error("MainParamaterForm/InitializeModbusClientAsync| Exception in Task: " + ex.Message);

                // Update UI elements on the UI thread
                this.Invoke(new Action(() =>
                {
                    statusConnection.Text = "Failed to initialize Modbus client";
                    infoMessages.Text = $"Unexpected error: {ex.Message}";
                }));
            }
        }


        private void CheckDatabaseConnection()
        {
            string statusMessage;
            bool isConnected = MainLogicClass.CheckConnection(out statusMessage);

            lblDbConnect.Text = statusMessage;
            lblDbConnect.ForeColor = isConnected ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }

        public MainParamatersForm(int num)
        {
        }

        public MainParamatersForm()
        {
            try
            {
                InitializeComponent();
                Application.DoEvents();


                Logger.Info("MainParametersForm/Constructor initialized.");
                CheckDatabaseConnection();
                RefreshComPortList();
                Logger.Info("MainParametersForm/Constructor RefreshComPortList");

                InitializePollingTimer();
                Logger.Info("MainParametersForm/Constructor InitializePollingTimer");

                dataList = new List<BatteryData>();
                minuteTimer = new Timer { Interval = 10000 };  //60000 }; // 1 minute interval hassanCode
                minuteTimer.Tick += MinuteTimerTick;
                Logger.Info("MainParametersForm/Constructor Paremeters");

                //SetupDataGridViewAlarm();
                SetupDataGridViewAlarmAsync();
                Logger.Info("MainParametersForm/Constructor SetupDataGridViewAlarm");

                SetupDataGridAsync();
                Logger.Info("MainParametersForm/Constructor SetupDataGridView");

                btnTogglePolling1.Click += BtnTogglePolling_Click;
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
                Logger.Error("MainParametersForm/Constructor Exception: " + ex.Message);
            }

        }
        private async void SetupDataGridAsync()
        {
            DataTable dt = await SetupDataGridViewAsync();  //ParamatersDataTable
        }

        public void MainParamatersForm_Load(object sender, EventArgs e)
        {
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
                JIMessageBox.ErrorMessage("001" + ex.Message);
            }

        }
        private void StartPolling()
        {
            pollingTimer.Start();
        }
        public void StartPollingDatabase()
        {
            try
            {
                isPolling = true;
                btnTogglePolling1.Text = "Stop Reading";
                startTime = DateTime.Now;
                /*  secondTimer.Start(); */
                 minuteTimer.Start(); //hassanCode
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage("02:" + ex.Message);
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
               minuteTimer.Stop(); //hassanCode
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage("03:" + ex.Message);
            }

        }

        int _lblDbInsertCount = 0;
        private void MinuteTimerTick(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = new DataTable();
                // Add HEADER columns to the DataTable
                foreach (DataGridViewColumn column in dgvCellLevel.Columns)
                {
                    dataTable.Columns.Add(column.HeaderText);
                }
                #region DataTable

                #region This Code Only Check Serial SpecialChar
                //// Add rows to the DataTable
                //Regex specialCharPattern = new Regex(@"^[,\/\-_%\s]+$");
                //foreach (DataGridViewRow row in dgvCellLevel.Rows)
                //{
                //    // Skip the last empty row if AllowUserToAddRows is true
                //    if (!row.IsNewRow)
                //    {
                //        var cellValue = row.Cells[0].Value?.ToString();

                //        if (cellValue == "Serial")
                //        {
                //            #region Skipp From DataTableCode
                //            //// Check if Serial value contains only special characters
                //            //if (!specialCharPattern.IsMatch(cellValue))
                //            //{
                //            //    // Valid Serial, assign to DataTable
                //            //    DataRow dataRow = dataTable.NewRow();
                //            //    foreach (DataGridViewCell cell in row.Cells)
                //            //    {
                //            //        dataRow[cell.ColumnIndex] = cell.Value;
                //            //    }
                //            //    dataTable.Rows.Add(dataRow);
                //            //}
                //            //else
                //            //{
                //            //    // Handle case where Serial contains only special characters (if needed)
                //            //    // Skip or log the invalid row
                //            //} 
                //            #endregion
                //            #region AssignNull to  DataTable  Currently i select this
                //            // Create a new DataRow
                //            DataRow dataRow = dataTable.NewRow();

                //            // Loop through all cells in the Serial row
                //            for (int i = 0; i < row.Cells.Count; i++)
                //            {
                //                var cellText = row.Cells[i].Value?.ToString();

                //                // If the cell contains only special characters, assign NULL
                //                if (specialCharPattern.IsMatch(cellText))
                //                {
                //                    dataRow[i] = DBNull.Value;
                //                }
                //                else
                //                {
                //                    // Otherwise, assign the actual value
                //                    dataRow[i] = row.Cells[i].Value;
                //                }
                //            }

                //            dataTable.Rows.Add(dataRow);
                //            #endregion
                //        }
                //        else if (row.Cells[0].Value == "FAULT-1")
                //        {
                //        }
                //        else if (row.Cells[0].Value == "FAULT-2")
                //        {
                //        }
                //        else if (row.Cells[0].Value == "FAULT-3")
                //        {
                //        }
                //        else if (row.Cells[0].Value == "FAULT-4")
                //        {
                //        }
                //        else if (row.Cells[0].Value == "FAULT-5")
                //        {
                //        }
                //        else
                //        {
                //            DataRow dataRow = dataTable.NewRow();
                //            foreach (DataGridViewCell cell in row.Cells)
                //            {
                //                dataRow[cell.ColumnIndex] = cell.Value;
                //            }
                //            dataTable.Rows.Add(dataRow);
                //        }
                //    }
                //}

                #endregion
                #region This Code Only Check All Column  SpecialChar
                // Define the special character pattern
                Regex specialCharPattern = new Regex(@"^[,\/\-_%\s]+$");

                DataTable dataTable1 = (DataTable)dgvCellLevel.DataSource;
                DataTable dt = (DataTable)dgvCellLevel.DataSource;
                // Loop through all rows in the DataGridView
                foreach (DataGridViewRow row in dgvCellLevel.Rows)
                {
                    // Skip the last empty row if AllowUserToAddRows is true
                    if (!row.IsNewRow)
                    {
                        DataRow dataRow = dataTable.NewRow(); // Create a new DataRow for the DataTable

                        // Loop through all cells in the current row
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            var cellText = row.Cells[i].Value?.ToString();

                            // Check if the cell value contains only special characters
                            if (specialCharPattern.IsMatch(cellText))
                            {
                                // Assign NULL if the cell contains only special characters
                                dataRow[i] = DBNull.Value;
                            }
                            else
                            {
                                // Assign the actual value if it doesn't contain special characters
                                dataRow[i] = row.Cells[i].Value;
                            }
                        }

                        // Add the row to the DataTable
                        dataTable.Rows.Add(dataRow);
                    }
                }


                #endregion
                #endregion
                CheckDatabaseConnection();
                new MainLogicClass().SaveDataToDatabase(dataTable);





                #region AlarmFromGrid
                DataTable dataTableAlarm = new DataTable();
                foreach (DataGridViewColumn column in dataGridViewAlarm.Columns)
                {
                    dataTableAlarm.Columns.Add(column.HeaderText);
                }
                // Check if the GridView is not empty
                if (dataGridViewAlarm.Rows.Count > 0)
                {
                    // Iterate through each row in the GridView
                    foreach (DataGridViewRow row in dataGridViewAlarm.Rows)
                    {
                        // Skip new or empty rows
                        if (!row.IsNewRow && row.Cells[0].Value != null)
                        {
                            DataRow dataRow = dataTableAlarm.NewRow();

                            // Assign cell values to the DataTable row
                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                dataRow[i] = row.Cells[i].Value;
                            }

                            // Add the DataRow to the DataTable
                            dataTableAlarm.Rows.Add(dataRow);
                        }
                    }
                    new MainLogicClass().SaveAlarmGridToDatabase(dataTableAlarm);
                }
                else
                {
                    // GridView is empty
                    // MessageBox.Show("The grid is empty!");
                }

                #endregion
                Regex specialCharOrAlphabetPattern = new Regex(@"^-?[0-9]*(\.[0-9]+)?$");

                string _volt = labelVolt.Text;
                string _current = labelCurrent.Text;
                string _power = labelPower.Text;
                string _soc = labelSoc.Text;
                string _temp = labelTemp.Text;

                // Using ternary operator to check for special characters or alphabets
                _volt = specialCharOrAlphabetPattern.IsMatch(_volt) ? null : _volt;
                _current = specialCharOrAlphabetPattern.IsMatch(_current) ? null : _current;
                _power = specialCharOrAlphabetPattern.IsMatch(_power) ? null : _power;
                _soc = specialCharOrAlphabetPattern.IsMatch(_soc) ? null : _soc;
                _temp = specialCharOrAlphabetPattern.IsMatch(_temp) ? null : _temp;

                // Convert to decimal only if the value is not null
                decimal? _labelVolt = _volt != null ? Convert.ToDecimal(_volt) : (decimal?)null;
                decimal? _labelCurrent = _current != null ? Convert.ToDecimal(_current) : (decimal?)null;
                decimal? _labelPower = _power != null ? Convert.ToDecimal(_power) : (decimal?)null;
                decimal? _labelSoc = _soc != null ? Convert.ToDecimal(_soc) : (decimal?)null;
                decimal? _labelTemp = _temp != null ? Convert.ToDecimal(_temp) : (decimal?)null;

                new MainLogicClass().SaveMainParamatersToDatabase(_labelVolt, _labelCurrent, _labelPower, _labelSoc, _labelTemp);

                _lblDbInsertCount++;
                lblDbInsertCount.Text = _lblDbInsertCount.ToString();

                dataList.Clear();
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage("05:" + ex.Message);
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

                JIMessageBox.ErrorMessage("04:" + ex.Message);
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
                JIMessageBox.ErrorMessage("06:" + ex.Message);
            }
        }

        private void StopPolling()
        {
            try
            {
                pollingTimer.Stop();
                if (modbusClient.Connected)  //hassancode
                {
                    modbusClient.Disconnect();
                }
            }
            catch (Exception ex)
            {

                JIMessageBox.ErrorMessage("07:" + ex.Message);
            }
        }
        static ChartValues<double> lstvoltage = new ChartValues<double>();
        static ChartValues<double> lstcurrent = new ChartValues<double>();
        static ChartValues<double> lstpower = new ChartValues<double>();
        static ChartValues<double> SocColor = new ChartValues<double>();
        static ChartValues<double> TempColor = new ChartValues<double>();
        static List<ChartValues<double>> allLists = new List<ChartValues<double>>();
        //List<string> XAxisValue = new List<string>() { "Voltage (V)", "Current (Amps)", "Power (kW)", "SOC Power" };
        public static bool isPollSelected = false;
        public async void PollingTimer_Tick(object sender, EventArgs e)
        {
            ushort temp;
            readReady = false;

            try
            {
                //await Task.Delay(100); // Adjust delay if needed

                if (slaveID > moduleCount.Value || slaveID == 0)
                {
                    if (slaveID != 0)
                        AlarmCheck();
                    slaveID = 1;

                }
                //LoadModbusData(slaveID, READ_HOLDING_REGISTER, 0xB9, 11);   //HassanCode
                await LoadModbusDataAsync(slaveID, READ_HOLDING_REGISTER, 0xB9, 11);   //HassanCode
                //await LoadModbusDataTestingByAqib(slaveID, READ_HOLDING_REGISTER, 0xB9, 11);  //AQIBSTAIC

                Dictionary<string, double> parametersAvg = new Dictionary<string, double>();
                string slaveidCount = moduleCount.Value.ToString();



                int validColumnCount = await CountValidColumnsAsync();


                if (!string.IsNullOrEmpty(slaveidCount) && slaveidCount != "-")
                {
                    ////  ProcessDataGridViewRowAsync(GridView, RowIndex, Formula, LabelName, moduleCount, CurrentRowTotalNumericColums);
                    await ProcessDataGridViewRowAsync(dgvCellLevel, 1, arr => arr.Average(), labelVolt , Convert.ToInt32(slaveidCount), validColumnCount);   //totalVolt
                    await ProcessDataGridViewRowAsync(dgvCellLevel, 2, arr => arr.Sum(), labelCurrent, Convert.ToInt32(slaveidCount), validColumnCount);  //totalCurrent
                    await ProcessDataGridViewRowAsync(dgvCellLevel, 3, arr => arr.Sum(), labelPower, Convert.ToInt32(slaveidCount), validColumnCount);  //totalPower
                    await ProcessDataGridViewRowAsync(dgvCellLevel, 4, arr => arr.Average(), labelSoc, Convert.ToInt32(slaveidCount), validColumnCount);  //avgSOC
                    await ProcessDataGridViewRowAsync(dgvCellLevel, 5, arr => arr.Average(), labelTemp, Convert.ToInt32(slaveidCount), validColumnCount);  //avgTemp

                    #region ForChart
                    if ((string.IsNullOrEmpty(labelVolt.Text) || labelVolt.Text != "-") ||
                            (string.IsNullOrEmpty(labelCurrent.Text) || labelCurrent.Text != "-") ||
                            (string.IsNullOrEmpty(labelPower.Text) || labelPower.Text != "-"))
                    {
                        // Add new data points to existing lists

                        double voltValue = SafeConvertToDouble(labelVolt.Text);
                        lstvoltage.Add(voltValue);
                        double currentValue = SafeConvertToDouble(labelCurrent.Text);
                        lstcurrent.Add(currentValue);
                        double powerValue = SafeConvertToDouble(labelPower.Text);
                        lstpower.Add(voltValue);
                        double SocValue = SafeConvertToDouble(labelSoc.Text);
                        SocColor.Add(SocValue);
                        double TempValue = SafeConvertToDouble(labelTemp.Text);
                        TempColor.Add(TempValue);

                        // Update allLists with the updated data
                        allLists.Clear();
                        allLists.Add(new ChartValues<double>(lstvoltage));
                        allLists.Add(new ChartValues<double>(lstcurrent));
                        allLists.Add(new ChartValues<double>(lstpower));
                        allLists.Add(new ChartValues<double>(SocColor));
                        allLists.Add(new ChartValues<double>(TempColor));
                        TrimChartValues(lstvoltage);
                        TrimChartValues(lstcurrent);
                        TrimChartValues(lstpower);
                        TrimChartValues(SocColor);
                        TrimChartValues(TempColor);
                        await new ChartSet().chartGT3Async(allLists, cartesianChart1);
                    }
                    #endregion
                }



                slaveID++;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiles section error: " + ex.Message);
            }
        }
        private async Task<double> ProcessDataGridViewRowAsync(DataGridView dgv, int rowIndex, Func<double[], double> calculationFunc, Label label, int slaveidCount, int validColumnCount)
        {
            string[] values = new string[dgv.Columns.Count];

            for (int z = 0; z < dgv.Columns.Count; z++)
            {
                var cellValue = dgv.Rows[rowIndex].Cells[z]?.Value?.ToString();
                if (!double.TryParse(cellValue, out _))
                {
                    continue; // Skip to the next iteration if not numeric
                }
                values[z] = cellValue;
            }

            if (Convert.ToInt32(slaveidCount) == validColumnCount)
            {
                double[] doubleArray = values.Where(x => !string.IsNullOrEmpty(x))
                                              .Select(double.Parse)
                                              .ToArray();

                if (doubleArray.Any())
                {
                    double result = Math.Round(calculationFunc(doubleArray), 1);
                    await Task.Run(() =>
                    {
                        label.Invoke(new Action(() => label.Text = result.ToString()));
                    });

                    // Clear values in columns beyond batteryIndex if needed
                    // ClearExtraColumns(columnIndex);
                }
            }

            return 0; // Return value if needed
        }

        private async Task<int> CountValidColumnsAsync()
        {
            return await Task.Run(() =>
            {
                return dgvCellLevel.Columns
                    .Cast<DataGridViewColumn>()
                    .Count(col =>
                    {
                        var cellValue = dgvCellLevel.Rows[1].Cells[col.Index]?.Value?.ToString();
                        return !string.IsNullOrEmpty(cellValue) && double.TryParse(cellValue, out _);
                    });
            });
        }

        //private void ClearExtraColumns(int batteryIndex)
        //{
        //    for (int colIndex = batteryIndex; colIndex < dgvCellLevel.Columns.Count; colIndex++)
        //    {
        //        for (int rowIndex = 0; rowIndex < dgvCellLevel.Rows.Count; rowIndex++)
        //        {
        //            dgvCellLevel.Rows[rowIndex].Cells[colIndex].Value = null; // Clear the cell value
        //        }
        //    }
        //}


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

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }

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
        public List<ChartValues<double>> LoadModbusDataOld(Int32 batteryIndex, int functionCode, int registerNumber, int data)
        {
            string serialNumberString = "-";
            try
            {
                lst = new List<double>();
                dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Logger.Info("MainParamaterForm/LoadModbusData| batteryIndex: " + batteryIndex.ToString() + " SERIAL_READ: " + functionCode.ToString() + " registerNumber: " + registerNumber + " data: " + data.ToString());

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
                                        System.Threading.Thread.Sleep(500); // Delay for 500ms
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
                                        string mos = (registers[CHARGING_MOS]) + "/" + (registers[DISCHARGING_MOS]);

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

                                        _table = MainParamatersFormPartial.ConvertDataGridViewToDataTable(dgvCellLevel);
                                        Logger.Info("MainParamaterForm/LoadModbusData| NonstaticDt " + _table.ToString());
                                        break;
                                    case 4:
                                        registers = modbusClient.ReadInputRegisters(modbusStartReg, modbusRegCount);// Read Input Registers (0x04)
                                        Logger.Info("MainParamaterForm/LoadModbusData| registers " + registers.ToString());
                                        break;
                                    case 6:
                                        //modbusClient.WriteSingleRegister(7485, 2345);
                                        modbusClient.WriteSingleRegister(registerNumber, data);
                                        //modbusClient.WriteSingleRegister(Convert.ToInt32(StaticModelValues.tbLowCellVolt), Convert.ToInt32(StaticModelValues.tbHighCellVolt));
                                        Logger.Info("MainParamaterForm/LoadModbusData| registerNumber " + registerNumber.ToString() + " data: " + data.ToString());
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
                                        Logger.Info("MainParamaterForm/LoadModbusData|  value4:" + value4.ToString());
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



                                        SendCANPacket(canPacket);
                                        string tmpp = BitConverter.ToString(canPacket).Replace("-", string.Empty);
                                        Logger.Info("MainParamaterForm/LoadModbusData|  canPacket:" + tmpp);

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
                                statusConnection.Text = ("Connected");
                                avgcell = ((cell1 + cell2 + cell3 + cell4 + cell5 + cell6 + cell7 + cell8 + cell9 + cell10 + cell11 + cell12 + cell13 + cell14) / 14);
                                //labelVolt.Text = voltage.ToString("0.0");
                                //labelCurrent.Text = current.ToString("0.0");
                                //labelPower.Text = power.ToString("0.0");
                                //labelTemp.Text = temp.ToString("0.0");
                                //labelSoc.Text = soc.ToString("0.0");
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
                JIMessageBox.ErrorMessage("MainParamaterForm / LoadModbusData | Main Exception: " + ex.Message);
            }

            return allLists;
        }
        public async Task<List<ChartValues<double>>> LoadModbusDataAsync(Int32 batteryIndex, int functionCode, int registerNumber, int data)
        {
            string serialNumberString = "-";
            try
            {
                lst = new List<double>();
                dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Logger.Info("MainParamaterForm/LoadModbusData| batteryIndex: " + batteryIndex.ToString() + " SERIAL_READ: " + functionCode.ToString() + " registerNumber: " + registerNumber + " data: " + data.ToString());

                modbusTotalReg = modbusRegCount + modbusStartReg;
                Logger.Info("MainParamaterForm/LoadModbusData| modbusTotalReg: " + modbusTotalReg.ToString());

                index = 0;
                searchValue = 0;
                searchRange = 0;
                string hexString;
                z = 0;

                #region Cases
                numSlaveID.Text = batteryIndex.ToString();
                slaveID = batteryIndex;
                StaticModelValues.slaveId1 = Convert.ToInt32(numSlaveID.Text);
                Logger.Info("MainParamaterForm/LoadModbusData| slaveId1: " + StaticModelValues.slaveId1.ToString());
                try
                {
                    //UpdateStatusConnection("Disconnected");
                    if (modbusClient == null)
                    {
                        InitializeModbusClientAsync(); // Initialize the modbusClient if it's null
                    }


                    try
                    {
                        Logger.Info("MainParamaterForm/LoadModbusData| functionCode: " + functionCode);
                        switch (functionCode)
                        {//StaticComment
                            case 1:
                                if (!modbusClient.Connected)
                                    InitializeModbusClientAsync();
                                await Task.Delay(100);
                                boolsRegister = modbusClient.ReadCoils(modbusStartReg, modbusRegCount);// Read Coils (0x01)
                                UpdateStatusConnection("Connected");
                                Logger.Info("MainParamaterForm/LoadModbusData| boolsRegister: " + boolsRegister.ToString());
                                break;
                            case 2:
                                UpdateStatusConnection("Connected");
                                if (!modbusClient.Connected)
                                    InitializeModbusClientAsync();
                                await Task.Delay(100);
                                boolsRegister = modbusClient.ReadDiscreteInputs(modbusStartReg, modbusRegCount);// Read Discrete Inputs (0x02)

                                Logger.Info("MainParamaterForm/LoadModbusData| boolsRegister: " + boolsRegister.ToString());
                                break;
                            case 3:

                                if (!modbusClient.Connected)
                                    InitializeModbusClientAsync(); // Initialize the modbusClient if it's null
                                this.Invoke(new Action(() =>
                                {
                                    statusConnection.Text = "Connected";
                                }));
                                await Task.Delay(100); // Remove this and replace with actual async call
                                registers = modbusClient.ReadHoldingRegisters(modbusStartReg, modbusRegCount);

                                Logger.Info("MainParamaterForm/LoadModbusData| boolsRegister: " + boolsRegister.ToString());
                                int[] serialNumberRaw = modbusClient.ReadHoldingRegisters(registerNumber, data);
                                Logger.Info("MainParamaterForm/LoadModbusData| serialNumberRaw.Count: " + serialNumberRaw.Count().ToString());

                                hexString = string.Join("", Array.ConvertAll(serialNumberRaw, val => val.ToString("X")));   //uncomment
                                Logger.Info("MainParamaterForm/LoadModbusData| hexString: " + hexString.ToString());

                                serialNumberString = ConvertHexStringToAscii(hexString);    //uncomment
                                Logger.Info("MainParamaterForm/LoadModbusData| serialNumberString: " + serialNumberString.ToString());

                                #region DynamicData from register to Variable
                                voltage = (registers[VOLTAGE]) * 0.1;
                                Logger.Info("MainParamaterForm/LoadModbusData| voltage: " + voltage.ToString());
                                current = (registers[CURRENT] - 30000) * 0.1;
                                Logger.Info("MainParamaterForm/LoadModbusData| current: " + current.ToString());
                                power = (registers[POWER]);
                                Logger.Info("MainParamaterForm/LoadModbusData| power: " + current.ToString());
                                soc = (registers[SOC]) * 0.1;
                                Logger.Info("MainParamaterForm/LoadModbusData| soc: " + soc.ToString());
                                ah = (registers[AH]) * 0.1;
                                Logger.Info("MainParamaterForm/LoadModbusData| ah " + ah.ToString());
                                temp = (registers[TEMP]) - 40;
                                Logger.Info("MainParamaterForm/LoadModbusData| temp: " + temp.ToString());
                                cell1 = (registers[CELL1]) * 0.001;
                                Logger.Info("MainParamaterForm/LoadModbusData| cell1: " + cell1.ToString());
                                cell2 = (registers[CELL2]) * 0.001;
                                Logger.Info("MainParamaterForm/LoadModbusData| cell2: " + cell2.ToString());
                                cell3 = (registers[CELL3]) * 0.001;
                                Logger.Info("MainParamaterForm/LoadModbusData| cell3: " + cell3.ToString());
                                cell4 = (registers[CELL4]) * 0.001;
                                Logger.Info("MainParamaterForm/LoadModbusData| cell4: " + cell4.ToString());
                                cell5 = (registers[CELL5]) * 0.001;
                                Logger.Info("MainParamaterForm/LoadModbusData| cell5: " + cell5.ToString());
                                cell6 = (registers[CELL6]) * 0.001;
                                Logger.Info("MainParamaterForm/LoadModbusData| cell6: " + cell6.ToString());
                                cell7 = (registers[CELL7]) * 0.001;
                                Logger.Info("MainParamaterForm/LoadModbusData| cell7: " + cell7.ToString());
                                cell8 = (registers[CELL8]) * 0.001;
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
                                #region CMOS & DMOS reading
                                string mos = (registers[CHARGING_MOS]) + "/" + (registers[DISCHARGING_MOS]);
                                #endregion
                                #endregion

                                #region dynamicToGrid
                                UpdateDataGridView(z++, batteryIndex, serialNumberString);
                                UpdateDataGridView(z++, batteryIndex, Math.Round(voltage, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(current, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(power, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(soc, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(ah, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(temp, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell1, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell2, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell3, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell4, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell5, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell6, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell7, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell8, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell9, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell10, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell11, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell12, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell13, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell14, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(cell15, 2));
                                UpdateDataGridView(z++, batteryIndex, fault1);
                                UpdateDataGridView(z++, batteryIndex, fault2);
                                UpdateDataGridView(z++, batteryIndex, fault3);
                                UpdateDataGridView(z++, batteryIndex, fault4);
                                UpdateDataGridView(z++, batteryIndex, Math.Round(maxCell, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(minCell, 2));
                                UpdateDataGridView(z++, batteryIndex, Math.Round(diffCell, 2));
                                UpdateDataGridView(z++, batteryIndex, mos);
                                // var w = dgvCellLevel.Rows[23].Cells[batteryIndex].Value; //?
                                // int c = Convert.ToInt32(dgvCellLevel.Rows[23].Cells[batteryIndex].Value);//int.Parse(w.ToString()); //?
                                #endregion
                                Logger.Info("MainParamaterForm/LoadModbusData| Grid complete ");

                                #region FAULTS / ALARMS
                                z = 0;//fault 2
                                #endregion
                                _table = MainParamatersFormPartial.ConvertDataGridViewToDataTable(dgvCellLevel);
                                Logger.Info("MainParamaterForm/LoadModbusData| staticDt " + _table.ToString());

                                break;
                            case 4:
                                if (!modbusClient.Connected)
                                    InitializeModbusClientAsync();
                                registers = modbusClient.ReadInputRegisters(modbusStartReg, modbusRegCount);// Read Input Registers (0x04)
                                UpdateStatusConnection("Connected");
                                Logger.Info("MainParamaterForm/LoadModbusData| registers " + registers.ToString());
                                break;
                            case 6:
                                if (!modbusClient.Connected)
                                    InitializeModbusClientAsync();
                                modbusClient.WriteSingleRegister(registerNumber, data);
                                UpdateStatusConnection("Connected");
                                Logger.Info("MainParamaterForm/LoadModbusData| registerNumber " + registerNumber.ToString() + " data: " + data.ToString());
                                Logger.Info("MainParamaterForm/LoadModbusData| registers " + modbusClient.ToString());
                                break;
                            case 0x50:
                                modbusClient.Disconnect();
                                Logger.Info("MainParamaterForm/LoadModbusData| modbusClient.Disconnect() " + modbusClient.ToString());
                                UpdateStatusConnection("Connected");
                                InitializeSerialPort();
                                Logger.Info("MainParamaterForm/LoadModbusData| InitializeSerialPort()");

                                string FixedFrameID = "A5401908";
                                Logger.Info("MainParamaterForm/LoadModbusData|  FixedFrameID");

                                short value4 = (short)registerNumber;
                                Logger.Info("MainParamaterForm/LoadModbusData|  value4:" + value4.ToString());

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

                                byte[] canPacket = CreateCANPacket(FixedFrameID, canData);


                                SendCANPacket(canPacket);
                                string tmpp = BitConverter.ToString(canPacket).Replace("-", string.Empty);
                                Logger.Info("MainParamaterForm/LoadModbusData|  canPacket:" + tmpp);

                                break;
                            case 98://load setting page data points

                                if (!modbusClient.Connected)
                                    InitializeModbusClientAsync();
                                await Task.Delay(100);
                                registers = modbusClient.ReadHoldingRegisters(registerNumber, data);
                                UpdateStatusConnection("Connected");
                                Logger.Info("MainParamaterForm/LoadModbusData|  registers:" + registers.ToString());
                                StaticModelValues.register_Settings = registers;
                                break;
                            case 99://load serial number

                                if (!modbusClient.Connected)
                                    InitializeModbusClientAsync();
                                await Task.Delay(100);
                                registers = modbusClient.ReadHoldingRegisters(registerNumber, data);
                                UpdateStatusConnection("Connected");
                                Logger.Info("MainParamaterForm/LoadModbusData|  registers:" + registers.ToString());
                                StaticModelValues.register_Settings = registers;
                                hexString = string.Join("", Array.ConvertAll(registers, val => val.ToString("X")));
                                serialNumberString = ConvertHexStringToAscii(hexString);
                                Logger.Info("MainParamaterForm/LoadModbusData|  serialNumberString:" + serialNumberString.ToString());
                                break;
                            case 100://values writing function from settings page
                                if (!modbusClient.Connected)
                                    InitializeModbusClientAsync();
                                await Task.Delay(100);
                                modbusClient.WriteSingleRegister(registerNumber, data);
                                UpdateStatusConnection("Connected");
                                Logger.Info("MainParamaterForm/LoadModbusData|  modbusClient:" + modbusClient.ToString());

                                registers = modbusClient.ReadHoldingRegisters(registerNumber, 1);// Read Holding Registers (0x03)
                                Logger.Info("MainParamaterForm/LoadModbusData|  modbusClient:" + registers.ToString());

                                StaticModelValues.tbLowCellVolt = registers[0];
                                Logger.Info("MainParamaterForm/LoadModbusData|  registers[0]:" + registers[0].ToString());
                                break;
                            default:
                                Logger.Info("MainParamaterForm/LoadModbusData|  Unsupported function code.");
                                UpdateInfoMessages("Unsupported function code.", Color.Red, Color.White);
                                UpdateStatusConnection("Disconnected");
                                break;

                        }
                        z = 0;
                        avgcell = ((cell1 + cell2 + cell3 + cell4 + cell5 + cell6 + cell7 + cell8 + cell9 + cell10 + cell11 + cell12 + cell13 + cell14) / 14);

                        this.Invoke(new Action(() =>
                        {
                            UpdateInfoMessages("Reading Successful", Color.DarkGreen, Color.White);
                            //labelVolt.Text = voltage.ToString("0.0");
                            //labelCurrent.Text = current.ToString("0.0");
                            //labelPower.Text = power.ToString("0.0");
                            //labelTemp.Text = temp.ToString("0.0");
                            //labelSoc.Text = soc.ToString("0.0");
                        }));
                    }
                    catch (Exception ex)
                    {

                        Logger.Error("MainParamaterForm/LoadModbusData|  Exception:" + ex.Message.ToString());
                        int[] tmp = { 0, 0 };
                        UpdateInfoMessages($"Error reading Modbus data:{ex.Message}", Color.Red, Color.White);
                        this.Invoke(new Action(() =>
                        {
                            statusConnection.Text = "Disconnected";
                        }));
                        StaticModelValues.register_Settings = tmp;
                        this.Invoke(new Action(() =>
                        {

                            for (z = 0; z <= 18; z++)
                                dgvCellLevel.Rows[z].Cells[batteryIndex].Value = "-";
                            labelVolt.Text = "-";
                            labelCurrent.Text = "-";
                            labelPower.Text = "-";
                            labelTemp.Text = "-";
                            labelSoc.Text = "-";
                        }));
                    }
                    finally   //uncomment
                    {
                        if (modbusClient.Connected)
                        {
                            modbusClient.Disconnect(); // Disconnect from Modbus server
                            this.Invoke(new Action(() =>
                            {
                                statusConnection.Text = "Disconnected";
                            }));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("MainParamaterForm/LoadModbusData|  Outer Exception:" + ex.Message.ToString());
                    UpdateStatusConnection("Disconnected");
                    //skip if no slave found
                }
                readReady = false;

                #endregion

            }
            catch (Exception ex)
            {
                Logger.Error("MainParamaterForm/LoadModbusData|  Main Exception:" + ex.Message.ToString());
                this.Invoke(new Action(() =>
                {
                    JIMessageBox.ErrorMessage("MainParamaterForm / LoadModbusData | Main Exception: " + ex.Message);
                }));
            }

            return allLists;
        }

        public async Task<List<ChartValues<double>>> LoadModbusDataTestingByAqib(Int32 batteryIndex, int functionCode, int registerNumber, int data)
        {
            string serialNumberString = "-";
            try
            {
                lst = new List<double>();
                dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Logger.Info("MainParamaterForm/LoadModbusData| batteryIndex: " + batteryIndex.ToString() + " SERIAL_READ: " + functionCode.ToString() + " registerNumber: " + registerNumber + " data: " + data.ToString());

                modbusTotalReg = modbusRegCount + modbusStartReg;
                Logger.Info("MainParamaterForm/LoadModbusData| modbusTotalReg: " + modbusTotalReg.ToString());

                index = 0;
                searchValue = 0;
                searchRange = 0;
                string hexString;
                z = 0;
                {
                    {
                        numSlaveID.Text = batteryIndex.ToString();
                        slaveID = batteryIndex;
                        StaticModelValues.slaveId1 = Convert.ToInt32(numSlaveID.Text);
                        Logger.Info("MainParamaterForm/LoadModbusData| slaveId1: " + StaticModelValues.slaveId1.ToString());
                        try
                        {
                            //InitializeModbusClient();
                            UpdateStatusConnection("Connected");
                            try
                            {
                                Logger.Info("MainParamaterForm/LoadModbusData| functionCode: " + functionCode);
                                switch (functionCode)
                                {//StaticComment
                                 //case 1:
                                 //boolsRegister = modbusClient.ReadCoils(modbusStartReg, modbusRegCount);// Read Coils (0x01)
                                 // Logger.Info("MainParamaterForm/LoadModbusData| boolsRegister: " + boolsRegister.ToString());
                                 // break;
                                 //case 2:
                                 // boolsRegister = modbusClient.ReadDiscreteInputs(modbusStartReg, modbusRegCount);// Read Discrete Inputs (0x02)
                                 //  break;
                                    case 3:
                                        //registers = modbusClient.ReadHoldingRegisters(modbusStartReg, modbusRegCount);    //uncomment
                                        //int[] serialNumberRaw = modbusClient.ReadHoldingRegisters(registerNumber, data);  //uncomment
                                        //registers = arr;
                                        //int[] serialNumberRaw = registers;



                                        //hexString = string.Join("", Array.ConvertAll(serialNumberRaw, val => val.ToString("X")));   //uncomment
                                        //Logger.Info("MainParamaterForm/LoadModbusData| hexString: " + hexString.ToString());

                                        //serialNumberString = ConvertHexStringToAscii(hexString);    //uncomment
                                        await Task.Delay(100); // Remove this and replace with actual async call

                                        Logger.Info("MainParamaterForm/LoadModbusData| serialNumberString: " + serialNumberString.ToString());
                                        #region StaticDataByAQIB
                                        rnd = new Random();
                                        voltage = rnd.Next(0, 1000) * 0.1;
                                        current = (rnd.Next(0, 1000) - 30000) * 0.1;
                                        power = (rnd.Next(0, 1000));
                                        soc = (rnd.Next(0, 1000)) * 0.1;
                                        ah = (rnd.Next(0, 1000)) * 0.1;
                                        temp = (rnd.Next(0, 1000)) - 40;
                                        cell1 = (rnd.Next(0, 1000)) * 0.001;
                                        cell2 = (rnd.Next(0, 1000)) * 0.001;
                                        cell3 = (rnd.Next(0, 1000)) * 0.001;
                                        cell4 = (rnd.Next(0, 1000)) * 0.001;
                                        cell5 = (rnd.Next(0, 1000)) * 0.001;
                                        cell6 = (rnd.Next(0, 1000)) * 0.001;
                                        cell7 = (rnd.Next(0, 1000)) * 0.001;
                                        cell8 = (rnd.Next(0, 1000)) * 0.001;
                                        cell9 = (rnd.Next(0, 1000)) * 0.001;
                                        cell10 = (rnd.Next(0, 1000)) * 0.001;
                                        cell11 = (rnd.Next(0, 1000)) * 0.001;
                                        cell12 = (rnd.Next(0, 1000)) * 0.001;
                                        cell13 = (rnd.Next(0, 1000)) * 0.001;
                                        cell14 = (rnd.Next(0, 1000)) * 0.001;
                                        cell15 = (rnd.Next(0, 1000)) * 0.001;

                                        fault1 = (rnd.Next(1, 1000)) * 0.009;
                                        fault2 = (rnd.Next(2, 1000)) * 0.009;
                                        fault3 = (rnd.Next(3, 1000)) * 0.009;
                                        fault4 = (rnd.Next(4, 1000)) * 0.009;
                                        #endregion

                                        #region DynamicData

                                        maxCell = FindMax(cell1, cell2, cell3, cell4, cell5, cell6, cell7, cell8, cell9, cell10, cell11, cell12, cell13, cell14, cell15);
                                        minCell = FindMin(cell1, cell2, cell3, cell4, cell5, cell6, cell7, cell8, cell9, cell10, cell11, cell12, cell13, cell14, cell15);
                                        diffCell = Math.Abs(maxCell - minCell);
                                        #endregion

                                        #region CMOS & DMOS reading
                                        string mos = (12) + "/" + (20);

                                        #endregion

                                        #region dynamicToGrid
                                        UpdateDataGridView(z++, batteryIndex, serialNumberString);
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(voltage, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(current, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(power, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(soc, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(ah, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(temp, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell1, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell2, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell3, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell4, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell5, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell6, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell7, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell8, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell9, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell10, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell11, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell12, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell13, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell14, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell15, 2));
                                        UpdateDataGridView(z++, batteryIndex, fault1);
                                        UpdateDataGridView(z++, batteryIndex, fault2);
                                        UpdateDataGridView(z++, batteryIndex, fault3);
                                        UpdateDataGridView(z++, batteryIndex, fault4);
                                        UpdateDataGridView(z++, batteryIndex, mos);

                                        var w = dgvCellLevel.Rows[23].Cells[batteryIndex].Value;
                                        int c = Convert.ToInt32(dgvCellLevel.Rows[23].Cells[batteryIndex].Value);//int.Parse(w.ToString());
                                        #endregion

                                        #region FAULTS / ALARMS
                                        z = 0;//fault 2
                                        #endregion

                                        _table = MainParamatersFormPartial.ConvertDataGridViewToDataTable(dgvCellLevel);
                                        Logger.Info("MainParamaterForm/LoadModbusData| staticDt " + _table.ToString());

                                        break;
                                    //case 4:
                                    //registers = modbusClient.ReadInputRegisters(modbusStartReg, modbusRegCount);// Read Input Registers (0x04)
                                    //Logger.Info("MainParamaterForm/LoadModbusData| registers " + registers.ToString());
                                    //break;
                                    //case 6:
                                    //modbusClient.WriteSingleRegister(registerNumber, data);
                                    //Logger.Info("MainParamaterForm/LoadModbusData| registerNumber " + registerNumber.ToString() + " data: " + data.ToString());
                                    //Logger.Info("MainParamaterForm/LoadModbusData| registers " + modbusClient.ToString());
                                    //break;
                                    //case 0x50:
                                    //modbusClient.Disconnect();
                                    //Logger.Info("MainParamaterForm/LoadModbusData| modbusClient.Disconnect() " + modbusClient.ToString());
                                    //
                                    //InitializeSerialPort();
                                    //Logger.Info("MainParamaterForm/LoadModbusData| InitializeSerialPort()");
                                    //string FixedFrameID = "A5401908";
                                    //Logger.Info("MainParamaterForm/LoadModbusData|  FixedFrameID");
                                    //short value4 = (short)registerNumber;
                                    //Logger.Info("MainParamaterForm/LoadModbusData|  value4:" + value4.ToString());
                                    //short value3 = (short)registerNumber;
                                    //Logger.Info("MainParamaterForm/LoadModbusData|  value3:" + value3.ToString());
                                    //short value2 = (short)data;
                                    //Logger.Info("MainParamaterForm/LoadModbusData|  value2:" + value2.ToString());
                                    //short value1 = (short)data;
                                    //Logger.Info("MainParamaterForm/LoadModbusData|  value2:" + value1.ToString());
                                    //byte[] canData = new byte[8];
                                    //Array.Copy(SwapBytes(BitConverter.GetBytes(value1)), 0, canData, 0, 2);
                                    //Array.Copy(SwapBytes(BitConverter.GetBytes(value2)), 0, canData, 2, 2);
                                    //Array.Copy(SwapBytes(BitConverter.GetBytes(value3)), 0, canData, 4, 2);
                                    //Array.Copy(SwapBytes(BitConverter.GetBytes(value4)), 0, canData, 6, 2);
                                    //Logger.Info("MainParamaterForm/LoadModbusData| Array.Copy");
                                    //// Create the CAN packet
                                    //byte[] canPacket = CreateCANPacket(FixedFrameID, canData);
                                    //
                                    //// Display the resulting CAN packet
                                    ////canPkt.Text = "Generated CAN Packet: " + BitConverter.ToString(canPacket).Replace("-", "");
                                    //SendCANPacket(canPacket);
                                    //string tmpp = BitConverter.ToString(canPacket).Replace("-", string.Empty);
                                    //Logger.Info("MainParamaterForm/LoadModbusData|  canPacket:" + tmpp);
                                    //case 98://load setting page data points
                                    //    registers = modbusClient.ReadHoldingRegisters(registerNumber, data);
                                    //    Logger.Info("MainParamaterForm/LoadModbusData|  registers:" + registers.ToString());
                                    //    StaticModelValues.register_Settings = registers;
                                    //    break;
                                    //case 99://load serial number
                                    //    registers = modbusClient.ReadHoldingRegisters(registerNumber, data);
                                    //    Logger.Info("MainParamaterForm/LoadModbusData|  registers:" + registers.ToString());
                                    //    StaticModelValues.register_Settings = registers;
                                    //    hexString = string.Join("", Array.ConvertAll(registers, val => val.ToString("X")));
                                    //    serialNumberString = ConvertHexStringToAscii(hexString);
                                    //    Logger.Info("MainParamaterForm/LoadModbusData|  serialNumberString:" + serialNumberString.ToString());
                                    //    break;
                                    //case 100://values writing function from settings page
                                    //    modbusClient.WriteSingleRegister(registerNumber, data);
                                    //    Logger.Info("MainParamaterForm/LoadModbusData|  modbusClient:" + modbusClient.ToString());
                                    //    registers = modbusClient.ReadHoldingRegisters(registerNumber, 1);// Read Holding Registers (0x03)
                                    //    Logger.Info("MainParamaterForm/LoadModbusData|  modbusClient:" + registers.ToString());
                                    //    StaticModelValues.tbLowCellVolt = registers[0];
                                    //    Logger.Info("MainParamaterForm/LoadModbusData|  registers[0]:" + registers[0].ToString());
                                    //    break;
                                    default:
                                        UpdateInfoMessages("Function code not handled.", Color.Red, Color.White);
                                        break;

                                }

                                z = 0;
                                infoMessages.Text = ("Reading Successful");

                                avgcell = ((cell1 + cell2 + cell3 + cell4 + cell5 + cell6 + cell7 + cell8 + cell9 + cell10 + cell11 + cell12 + cell13 + cell14) / 14);
                                //labelVolt.Text = voltage.ToString("0.0");
                                //labelCurrent.Text = current.ToString("0.0");
                                //labelPower.Text = power.ToString("0.0");
                                //labelTemp.Text = temp.ToString("0.0");
                                //labelSoc.Text = soc.ToString("0.0");
                            }
                            catch (Exception ex)
                            {
                                Logger.Error("MainParamaterForm/LoadModbusData|  Exception:" + ex.Message.ToString());
                                int[] tmp = { 0, 0 };
                                UpdateInfoMessages($"Error reading Modbus data:: {ex.Message}", Color.Red, Color.White);
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
                                //if (modbusClient.Connected)
                                //{
                                //    modbusClient.Disconnect(); // Disconnect from Modbus server
                                //    statusConnection.Text = "Disconnected";
                                //}
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("MainParamaterForm/LoadModbusData|  Outer Exception:" + ex.Message.ToString());
                            UpdateStatusConnection("Error: " + ex.Message);
                            UpdateInfoMessages($"MainParamaterForm/LoadModbusData|  Outer Exception:: {ex.Message}", Color.Red, Color.White);
                        }
                        readReady = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error("MainParamaterForm/LoadModbusData|  Main Exception:" + ex.Message.ToString());
                UpdateStatusConnection("Error: " + ex.Message);
                UpdateInfoMessages($"Error: {ex.Message}", Color.Red, Color.White);
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
        private async void iconButton1_Click(object sender, EventArgs e)
        {
            if (commType.Text == "MODBUS")   //uncomment
            {
                try
                {
                    DataTable dt = SetupDataGridView();  //GetPatamatersHeader
                    allLists = await LoadModbusDataAsync(1, 3, 0, 0);
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
                    await new ChartSet().chartGT3Async(allLists, cartesianChart1);//, XAxisValue);
                }
                catch (Exception ex)
                {
                    JIMessageBox.ErrorMessage("iconButton1_Click:" + ex.Message);
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
                JIMessageBox.ErrorMessage("RefreshComPortList: " + ex.Message);
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
                JIMessageBox.ErrorMessage("SetupDataGridViewAlarm: " + ex.Message);
                Logger.Error("DataGridViewAlarm| Exception: " + ex.Message);
                dt = null;
            }


        }
        private async Task SetupDataGridViewAlarmAsync()
        {
            try
            {
                DataTable dt = await Task.Run(() => new StaticData().DataGridViewAlarm());
                Logger.Info("DataGridViewAlarm| StaticData");

                // Update the DataGridView on the main thread
                if (dataGridViewAlarm.InvokeRequired)
                {
                    dataGridViewAlarm.Invoke((Action)(() =>
                    {
                        dataGridViewAlarm.Rows.Clear();
                        foreach (DataRow r in dt.Rows)
                        {
                            dataGridViewAlarm.Rows.Add(
                                r["SNo"].ToString(), r["Alarm"].ToString());
                        }
                    }));
                }
                else
                {
                    dataGridViewAlarm.Rows.Clear();
                    foreach (DataRow r in dt.Rows)
                    {
                        dataGridViewAlarm.Rows.Add(
                            r["SNo"].ToString(), r["Alarm"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage("SetupDataGridViewAlarm: " + ex.Message);
                Logger.Error("DataGridViewAlarm| Exception: " + ex.Message);
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
                JIMessageBox.ErrorMessage("SetupDataGridView:" + ex.Message);
                dt = null;
            }

            return dt;
        }

        private async Task<DataTable> SetupDataGridViewAsync()
        {
            DataTable dt = null;
            try
            {
                dt = await Task.Run(() => new StaticData().DataGridViewAsync());  // Use async method
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        dgvCellLevel.Rows.Clear();
                        foreach (DataRow r in dt.Rows)
                        {
                            dgvCellLevel.Rows.Add(
                                r["Parameter"].ToString(), r["Battery1"].ToString(), r["Battery2"].ToString(), r["Battery3"].ToString(), r["Battery4"].ToString(), r["Battery5"].ToString(),
                                r["Battery6"].ToString(), r["Battery7"].ToString(), r["Battery8"].ToString(), r["Battery9"].ToString(), r["Battery10"].ToString(), r["Battery11"].ToString(),
                                r["Battery12"].ToString(), r["Battery13"].ToString(), r["Battery14"].ToString(), r["Battery15"].ToString(), r["Battery16"].ToString(), r["Battery17"].ToString(),
                                r["Battery18"].ToString(), r["Battery19"].ToString(), r["Battery20"].ToString());
                        }
                    }));
                }
                else
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        dgvCellLevel.Rows.Add(
                            r["Parameter"].ToString(), r["Battery1"].ToString(), r["Battery2"].ToString(), r["Battery3"].ToString(), r["Battery4"].ToString(), r["Battery5"].ToString(),
                            r["Battery6"].ToString(), r["Battery7"].ToString(), r["Battery8"].ToString(), r["Battery9"].ToString(), r["Battery10"].ToString(), r["Battery11"].ToString(),
                            r["Battery12"].ToString(), r["Battery13"].ToString(), r["Battery14"].ToString(), r["Battery15"].ToString(), r["Battery16"].ToString(), r["Battery17"].ToString(),
                            r["Battery18"].ToString(), r["Battery19"].ToString(), r["Battery20"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                {
                    JIMessageBox.ErrorMessage("SetupDataGridView:" + ex.Message);
                }));
                }
                else
                {
                    JIMessageBox.ErrorMessage("SetupDataGridView:" + ex.Message);
                }

                dt = null;
            }

            return dt;
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
                JIMessageBox.ErrorMessage("DataReceivedHandler: " + ex.Message);
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
                JIMessageBox.ErrorMessage("LogAlarm: " + ex.Message);
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
                    JIMessageBox.ErrorMessage("alarm: " + ex.Message);
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











        #region DelegateUI_Code
        private delegate void UpdateUIDelegate();

        private void UpdateStatusConnection(string status)
        {
            if (statusConnection.InvokeRequired)
            {
                statusConnection.Invoke(new Action<string>(UpdateStatusConnection), status);
            }
            else
            {
                statusConnection.Text = status;
            }
        }

        private void UpdateInfoMessages(string message, Color backColor, Color foreColor)
        {
            if (infoMessages.InvokeRequired)
            {
                infoMessages.Invoke(new Action<string, Color, Color>(UpdateInfoMessages), message, backColor, foreColor);
            }
            else
            {
                infoMessages.Text = message;
                infoMessages.BackColor = backColor;
                infoMessages.ForeColor = foreColor;
            }
        }

        private void UpdateLabels(string voltage, string current, string power, string temp, string soc)
        {
            if (labelVolt.InvokeRequired || labelCurrent.InvokeRequired || labelPower.InvokeRequired || labelTemp.InvokeRequired || labelSoc.InvokeRequired)
            {
                labelVolt.Invoke(new Action<string>(text => labelVolt.Text = text), voltage);
                labelCurrent.Invoke(new Action<string>(text => labelCurrent.Text = text), current);
                labelPower.Invoke(new Action<string>(text => labelPower.Text = text), power);
                labelTemp.Invoke(new Action<string>(text => labelTemp.Text = text), temp);
                labelSoc.Invoke(new Action<string>(text => labelSoc.Text = text), soc);
            }
            else
            {
                labelVolt.Text = voltage;
                labelCurrent.Text = current;
                labelPower.Text = power;
                labelTemp.Text = temp;
                labelSoc.Text = soc;
            }
        }

        private void UpdateDataGridView(int rowIndex, int columnIndex, object value)
        {
            if (dgvCellLevel.InvokeRequired)
            {
                dgvCellLevel.Invoke(new Action<int, int, object>(UpdateDataGridView), rowIndex, columnIndex, value);
            }
            else
            {
                // Update the specific cell value
                dgvCellLevel.Rows[rowIndex].Cells[columnIndex].Value = value;


            }
        }

        private void ClearExtraColumns(int batteryIndex)
        {
            for (int colIndex = batteryIndex; colIndex < dgvCellLevel.Columns.Count; colIndex++)
            {
                for (int rowIndex = 0; rowIndex < dgvCellLevel.Rows.Count; rowIndex++)
                {
                    dgvCellLevel.Rows[rowIndex].Cells[colIndex].Value = null; // or set to a default value if needed
                }
            }
        }


        #endregion

        public static double SafeConvertToDouble(string input, double defaultValue = 0)
        {
            // Try to parse the input to a double
            if (double.TryParse(input, out double result))
            {
                return result; // Return the parsed value if successful
            }

            // Return the default value if the input is not numeric
            return defaultValue;
        }


    }


}
