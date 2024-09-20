using BusinessLogic;
using BusinessLogic.Extensions;
using BusinessLogic.Model;
using log4net;
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
using static BusinessLogic.Model.StaticModel.SettingsModel;

namespace EMView.UI
{

    public partial class SettingsForm : Form//BasetForm
    {
        private SerialPort serialPort;
        private string buffer = string.Empty;
        Int32 slaveID = 1;

        string modbusSetpointData;
        const int WRITE_DATA = 0x06;
        const int SETPOINTS_READ = 98;
        const int SERIAL_READ = 99;
        const int CAN_PKT = 0x50;
       
        const int HIGH_CELL_VOLTAGE = 0x8B;//single voltage is too high level alarm
        const int LOW_CELL_VOLTAGE = 0x8D;//Single voltage too low level alarm
        const int HIGH_SUM_VOLTAGE = 7489;
        const int LOW_SUM_VOLTAGE = 7491;
        const int HIGH_CURR_CHARGE = 0x93;//Charge current is too high for level 1 alarm
        const int HIGH_CURR_DISCHAR = 0x95;//The discharge current is too high level alarm
        const int HIGH_TEMP_CHARGE = 0x97;//The charging temperature alarm is too high
        const int HIGH_TEMP_DISCHARGE = 0x9D;//Discharge temperature is too high secondary alarm
        const int SOC_HIGH_ALARM = 7501;
        const int SOC_LOW_ALARM = 7503;
        const int BALANCE_START_VOLTAGE = 7505;
        const int BALANCE_VOLTAGE_DIFF = 7507;
        const int HIGH_DISCHARGE_TEMP = 7509;
        const int LOW_DISCHARGE_TEMP = 7511;
        const int CELL_RATED_VOLTAGE = 0x81;
        const int MAX_VOLTAGE_DIFF = 7515;
        const int BATTERY_CAPACITY = 0x80;
        const int SLEEP_TIME = 0x8A;
        const int SERIAL_1 = 7521;
        const int SERIAL_2 = 7523;
        const int SERIAL_3 = 7525;
        const int SERIAL_4 = 7526;
        const int SERIAL_5 = 7529;
        const int SERIAL_6 = 7531;
        const int SERIAL_7 = 7533;
        const int SERIAL_8 = 7535;
        const int SERIAL_9 = 7537;


        private static readonly ILog Logger = LogManager.GetLogger(typeof(SettingsForm));


        StaticModel.SettingsModel.SettingsRequest _settingsModel = new StaticModel.SettingsModel.SettingsRequest();

        public SettingsForm()
        {

            InitializeComponent();
            InitlizeStaticData();
            checkIsGuest();
            selectedModule_SelectedIndexChanged(null, null);

            StartSessionTimer();
        }


        #region Login5MinSetting
        private Timer LoginTimer;
        public void StartSessionTimer()
        {
            if (LoginTimer == null)
            {
                LoginTimer = new Timer();
                LoginTimer.Tick += LoginTimer_Tick;
            }
            LoginTimer.Interval = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Timer"].ToString()); //300000;  //5min
            LoginTimer.Start();

        }
        private void LoginTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // Always set username and password to null after the timer interval
                LoginModel.username = null;
                LoginModel.password = null;
                MainForm mainForm = new MainForm();
                //mainForm.labelIsGuest.Text = "Guest";

                // Optionally, update SettingsForm button visibility if it's open
                foreach (Form form in Application.OpenForms)
                {
                    if (form is SettingsForm settingsForm)
                    {
                        settingsForm.checkIsGuest();
                    }
                }
                // Stop the timer after it has ticked
                LoginTimer.Stop();

            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }

        }
        #endregion



        public void checkIsGuest()
        {
            if (string.IsNullOrEmpty(LoginModel.username) && string.IsNullOrEmpty(LoginModel.password))
            {
                btnhighAndLowCellVolt.Visible = false;
                btnHighAndLowSumVolt.Visible = false;
                btnHighCurrentCharAndDic.Visible = false;
                btnHighTempCharAndDic.Visible = false;
                btnHighAndLowSocAlarm.Visible = false;
                btnSetEnergyCharAndDisc.Visible = false;
                iconButton2.Visible = false;
                iconButton3.Visible = false;
            }
            else
            {

                btnhighAndLowCellVolt.Visible = true;
                btnHighAndLowSumVolt.Visible = true;
                btnHighCurrentCharAndDic.Visible = true;
                btnHighTempCharAndDic.Visible = true;
                btnHighAndLowSocAlarm.Visible = true;
                btnSetEnergyCharAndDisc.Visible = true;

                iconButton2.Visible = true;
                iconButton3.Visible = true;
            }

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {


            InitlizeStaticData();
            iconButton4_Click(null, null);
            //UpdateCaseValueFromMainParamaterForm();

            if (tbSerial.Text != "")
            {
                selectedModule.Items.Add(1 + " : " + tbSerial.Text);
                selectedModule.SelectedIndex = 0;
            }
        }


        private async void btnHighAndLowSumVolt_Click(object sender, EventArgs e)
        {
            StaticModelValues.tbHighCurrCharge = tbHighCurrCharge.Text.ToDouble();
            await new MainParamatersForm().LoadModbusDataAsync(slaveID, WRITE_DATA, HIGH_CURR_CHARGE, Convert.ToInt32(StaticModelValues.tbHighCurrCharge));

            StaticModelValues.tbHighCurrDischarge = tbHighCurrDischarge.Text.ToDouble();
            await new MainParamatersForm().LoadModbusDataAsync(slaveID, WRITE_DATA, HIGH_CURR_DISCHAR, Convert.ToInt32(StaticModelValues.tbHighCurrDischarge));

            //Get
            _settingsModel.tbLowSumVolt = tbLowSumVolt.Text.ToDouble();
            _settingsModel.tbHighSumVolt = tbHighSumVolt.Text.ToDouble();
            //Set
            tbLowSumVolt.Text = _settingsModel.tbLowSumVolt.ToString();
            tbHighSumVolt.Text = _settingsModel.tbHighSumVolt.ToString();
        }

        private async void btnHighCurrentCharAndDic_Click(object sender, EventArgs e)
        {
            StaticModelValues.tbHighCurrCharge = tbHighCurrCharge.Text.ToDouble();
            await new MainParamatersForm().LoadModbusDataAsync(slaveID, WRITE_DATA, HIGH_CURR_CHARGE, Convert.ToInt32(StaticModelValues.tbHighCurrCharge));

            StaticModelValues.tbHighCurrDischarge = tbHighCurrDischarge.Text.ToDouble();
            await new MainParamatersForm().LoadModbusDataAsync(slaveID, WRITE_DATA, HIGH_CURR_DISCHAR, Convert.ToInt32(StaticModelValues.tbHighCurrDischarge));

            //Get
            _settingsModel.tbHighCurrCharge = tbHighCurrCharge.Text.ToDouble();
            _settingsModel.tbHighCurrDischarge = tbHighCurrDischarge.Text.ToDouble();
            //Set
            tbHighCurrCharge.Text = _settingsModel.tbHighCurrCharge.ToString();
            tbHighCurrDischarge.Text = _settingsModel.tbHighCurrDischarge.ToString();

        }

        private async void btnHighTempCharAndDic_Click(object sender, EventArgs e)
        {
            StaticModelValues.tbHighTempDischarge = tbHighTempDischarge.Text.ToDouble();
            await new MainParamatersForm().LoadModbusDataAsync(slaveID, WRITE_DATA, HIGH_TEMP_DISCHARGE, Convert.ToInt32(StaticModelValues.tbHighTempDischarge));

            StaticModelValues.tbHighTempCharge = tbHighTempCharge.Text.ToDouble();
            await new MainParamatersForm().LoadModbusDataAsync(slaveID, WRITE_DATA, HIGH_TEMP_CHARGE, Convert.ToInt32(StaticModelValues.tbHighTempCharge));

            //Get
            _settingsModel.tbHighTempCharge = tbHighTempCharge.Text.ToDouble();
            _settingsModel.tbHighTempDischarge = tbHighTempDischarge.Text.ToDouble();
            //Set
            tbHighTempCharge.Text = _settingsModel.tbHighTempCharge.ToString();
            tbHighTempDischarge.Text = _settingsModel.tbHighTempDischarge.ToString();
        }

        private void btnHighAndLowSocAlarm_Click(object sender, EventArgs e)
        {   //Get
            _settingsModel.tbSocLowAlarm = tbSocLowAlarm.Text.ToDouble();
            _settingsModel.tbSocHighAlarm = tbSocHighAlarm.Text.ToDouble();
            //Set
            tbSocLowAlarm.Text = _settingsModel.tbSocLowAlarm.ToString();
            tbSocHighAlarm.Text = _settingsModel.tbSocHighAlarm.ToString();
        }

        private void btnSetEnergyCharAndDisc_Click(object sender, EventArgs e)
        {   //Get
            _settingsModel.tbSetChargeEnergy = tbStartVoltage.Text.ToDouble();
            _settingsModel.tbSetDishargeEnergy = tbBalanceVoltageDiff.Text.ToDouble();
            //Set
            tbStartVoltage.Text = _settingsModel.tbSetChargeEnergy.ToString();
            tbBalanceVoltageDiff.Text = _settingsModel.tbSetDishargeEnergy.ToString();
        }

        private void bodyPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        //Funcations
        private void InitlizeStaticData()
        {
            SettingsRequest model = new StaticData().SettingsData();

            tbLowCellVolt.Text = model.tbLowCellVolt.ToString();
            tbHighCellVolt.Text = model.tbHighCellVolt.ToString();
            tbHighCurrCharge.Text = model.tbHighCurrCharge.ToString();
            tbHighCurrDischarge.Text = model.tbHighCurrDischarge.ToString();
            tbHighTempCharge.Text = model.tbHighTempCharge.ToString();
            tbHighTempDischarge.Text = model.tbHighTempDischarge.ToString();
            tbHighSumVolt.Text = model.tbHighSumVolt.ToString();
            tbLowSumVolt.Text = model.tbLowSumVolt.ToString();
            tbSocHighAlarm.Text = model.tbSocHighAlarm.ToString();
            tbSocLowAlarm.Text = model.tbSocLowAlarm.ToString();
            tbStartVoltage.Text = model.tbSetChargeEnergy.ToString();
            tbBalanceVoltageDiff.Text = model.tbSetDishargeEnergy.ToString();
            tbSerial.Text = model.tbSerial.ToString();


            //cbMode = items,

        }



        private void tbHighCellVolt_TextChanged(object sender, EventArgs e)
        {

        }
        //public void UpdateCaseValueFromMainParamaterForm()
        //{
        //    tbHighCellVolt.Text =  StaticModelValues.tbLowCellVolt.ToString();
        //}
        private void iconButton4_Click(object sender, EventArgs e)
        {
            loadData();
        }
        private async void loadData()
        {
            UInt16 temp;
            //StaticModelValues.ForTestVar = Convert.ToDouble(tbHighCellVolt.Text);  //AQIB CODE
            //Showing you Steps
            //goto  StaticModelValues class create variable in this class
            //Assign Value from Textbox to Static Model  StaticModelValues
            // Get value from StaticModelValues Model on Main Paramater Form and assign to textbox

            try
            {
                Logger.Info("SettingForm/loadData initialized");
                string hexString = null;
                await new MainParamatersForm().LoadModbusDataAsync(slaveID, SERIAL_READ, 0xB9, 11);
                Logger.Info("SettingForm/loadData LoadModbusData| slaveID: " + slaveID.ToString() + " SERIAL_READ: " + SERIAL_READ.ToString() + "0xB9 : 11");
                try
                {
                    hexString = string.Join("", Array.ConvertAll(StaticModelValues.register_Settings, val => val.ToString("X")));
                }
                catch (Exception ex)
                {
                    Logger.Error("SettingForm/loadData| Exception: " + ex.Message.ToString());
                    JIMessageBox.ErrorMessage("Serial Data Reading Failed");
                }
                if (hexString != null)
                {
                    string asciiString = ConvertHexStringToAscii(hexString);
                    tbSerial.Text = asciiString;
                    Logger.Info("SettingForm/loadData| asciiString: " + asciiString.ToString());
                }


                await new MainParamatersForm().LoadModbusDataAsync(slaveID, SETPOINTS_READ, 0x81, 30);
                Logger.Info("SettingForm/loadData LoadModbusData");

                #region MyRegion
                if (StaticModelValues.register_Settings != null)
                {
                    Logger.Info("SettingForm/loadData| asciiString: " + StaticModelValues.register_Settings.Count().ToString());
                    for (int i = 0; i < StaticModelValues.register_Settings.Count(); i++)
                    {

                        if (i == 0)
                        {
                            tbCellRatedVoltage.Text = ((UInt16)StaticModelValues.register_Settings[0]).ToString();
                        }
                        if (i == 9)
                        {
                            tbSleepTime.Text = ((UInt16)StaticModelValues.register_Settings[9]).ToString();
                        }
                        if (i == 10)
                        {
                            tbHighCellVolt.Text = ((UInt16)StaticModelValues.register_Settings[10]).ToString();
                        }
                        if (i == 12)
                        {
                            tbLowCellVolt.Text = ((UInt16)StaticModelValues.register_Settings[12]).ToString();
                        }
                        if (i == 18)
                        {
                            tbHighCurrCharge.Text = ((UInt16)StaticModelValues.register_Settings[18]).ToString();
                        }
                        if (i == 20)
                        {
                            tbHighCurrDischarge.Text = ((UInt16)StaticModelValues.register_Settings[20]).ToString();
                        }
                        if (i == 22)
                        {
                            tbHighTempCharge.Text = ((UInt16)StaticModelValues.register_Settings[22]).ToString();
                        }
                        if (i == 28)
                        {
                            tbHighTempDischarge.Text = ((UInt16)StaticModelValues.register_Settings[28]).ToString();
                        }
                        tbHighSumVolt.Text = "N/A";
                        tbLowSumVolt.Text = "N/A";// StaticModelValues.register_Settings[1].ToString();}
                        tbSocHighAlarm.Text = "N/A";//StaticModelValues.register_Settings[1].ToString();
                        tbSocLowAlarm.Text = "N/A";//StaticModelValues.register_Settings[1].ToString();
                        tbStartVoltage.Text = "N/A";//StaticModelValues.register_Settings[1].ToString();
                        tbBalanceVoltageDiff.Text = "N/A";//StaticModelValues.register_Settings[1].ToString();
                    }
                }
                #endregion

            }
            catch (ArgumentException ex)
            {
                JIMessageBox.ErrorMessage("Error: " + ex.Message + "Conversion Error");
                Logger.Error("SettingForm/loadData| ArgumentException: " + ex.Message.ToString());
            }
            catch (FormatException ex)
            {
                JIMessageBox.ErrorMessage("Error: Invalid hex string format.");
                Logger.Error("SettingForm/loadData| FormatException: " + ex.Message.ToString());
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage("Unexpected error: " + ex.Message);
                Logger.Error("SettingForm/loadData| Exception1: " + ex.Message.ToString());
            }
        }

        public static string ConvertHexStringToAscii(string hexString)
        {
            if (hexString == null)
            {
                return null;
            }
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


        private async void iconButton2_Click(object sender, EventArgs e)
        {
            StaticModelValues.tbCellRatedVoltage = tbCellRatedVoltage.Text.ToDouble();
            await new MainParamatersForm().LoadModbusDataAsync(slaveID, WRITE_DATA, CELL_RATED_VOLTAGE, Convert.ToInt32(StaticModelValues.tbCellRatedVoltage));

        }



        private void btnDownloadValue_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.Title = "Save Values";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string[] values = {
                            tbHighCellVolt.Text,
                            tbLowCellVolt.Text,
                            tbHighSumVolt.Text,
                            tbLowSumVolt.Text,
                            tbHighCurrCharge.Text,
                            tbHighCurrDischarge.Text,
                            tbHighTempCharge.Text,
                            tbHighTempDischarge.Text,
                            tbSocHighAlarm.Text,
                            tbSocLowAlarm.Text,
                            tbStartVoltage.Text,
                            tbBalanceVoltageDiff.Text,

                            tbCellRatedVoltage.Text,
                            tbMaxVoltageDifference.Text,
                            tbBatteryCapacity.Text,
                            tbSleepTime.Text,
                            tbSerial.Text
                        };

                        File.WriteAllLines(saveFileDialog.FileName, values);
                        JIMessageBox.InformationMessage("Values saved to file successfully.");
                    }
                    catch (Exception ex)
                    {
                        JIMessageBox.ErrorMessage($"Error saving values to file: {ex.Message}");
                    }
                }
            }
        }

        private void btnUploadValue_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Open Values";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string[] values = File.ReadAllLines(openFileDialog.FileName);

                        if (values.Length >= 19)
                        {
                            tbHighCellVolt.Text = values[0];
                            tbLowCellVolt.Text = values[1];
                            tbHighSumVolt.Text = values[2];
                            tbLowSumVolt.Text = values[3];
                            tbHighCurrCharge.Text = values[4];
                            tbHighCurrDischarge.Text = values[5];
                            tbHighTempCharge.Text = values[6];
                            tbHighTempDischarge.Text = values[7];
                            tbSocHighAlarm.Text = values[8];
                            tbSocLowAlarm.Text = values[9];
                            tbStartVoltage.Text = values[10];
                            tbBalanceVoltageDiff.Text = values[11];

                            tbCellRatedVoltage.Text = values[14];
                            tbMaxVoltageDifference.Text = values[15];
                            tbBatteryCapacity.Text = values[16];
                            tbSleepTime.Text = values[17];
                            tbSerial.Text = values[18];
                            JIMessageBox.InformationMessage("Values loaded from file successfully.");
                        }
                        else
                        {
                            JIMessageBox.ErrorMessage("File does not contain enough values.");
                        }
                    }
                    catch (Exception ex)
                    {
                        JIMessageBox.ErrorMessage($"Error loading values from file: {ex.Message}");
                    }
                }
            }
        }



        #region CANBUS writing section

        private async void btnhighAndLowCellVolt_Click(object sender, EventArgs e)
        {
            await new MainParamatersForm().LoadModbusDataAsync(slaveID, CAN_PKT, Convert.ToInt16(tbLowCellVolt.Text), Convert.ToInt16(tbHighCellVolt.Text));
            #region testing code
            /* try
             {
                  string FixedFrameID = "A5401908";
                 // Read the Int16 values from the text boxes
                 short value4 = Convert.ToInt16(tbLowCellVolt.Text);
                 short value3 = Convert.ToInt16(tbLowCellVolt.Text);
                 short value2 = Convert.ToInt16(tbHighCellVolt.Text);
                 short value1 = Convert.ToInt16(tbHighCellVolt.Text);

                 // Convert the Int16 values to a byte array
                 byte[] data = new byte[8];
                 Array.Copy(SwapBytes(BitConverter.GetBytes(value1)), 0, data, 0, 2);
                 Array.Copy(SwapBytes(BitConverter.GetBytes(value2)), 0, data, 2, 2);
                 Array.Copy(SwapBytes(BitConverter.GetBytes(value3)), 0, data, 4, 2);
                 Array.Copy(SwapBytes(BitConverter.GetBytes(value4)), 0, data, 6, 2);

                 // Create the CAN packet
                 byte[] canPacket = CreateCANPacket(FixedFrameID, data);

                 // Display the resulting CAN packet
                 canPkt.Text = "Generated CAN Packet: " + BitConverter.ToString(canPacket).Replace("-", "");

                 SendCANPacket(canPacket);
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Error: " + ex.Message);
             }*/
            #endregion
        }
        private async void iconButton3_Click(object sender, EventArgs e)
        {
            await new MainParamatersForm().LoadModbusDataAsync(slaveID, CAN_PKT, Convert.ToInt16(tbSleepTime.Text), Convert.ToInt16(tbHighCellVolt.Text));

        }

        private byte[] SwapBytes(byte[] bytes)
        {
            if (bytes.Length != 2)
                throw new ArgumentException("Only 2-byte arrays are supported for swapping.");

            return new byte[] { bytes[1], bytes[0] };
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
        #endregion



        private async void reload_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("SettingForm/reload_Click initialized");

                selectedModule.Items.Clear();
                for (Int32 i = 1; i <= 20; i++)
                {

                    #region ProgressBar
                    progressBar1.Value = i;
                    progressBar1.Refresh();
                    #endregion
                    string hexString = null;
                    await new MainParamatersForm().LoadModbusDataAsync(i, SERIAL_READ, 0xB9, 11);
                    Logger.Info("SettingForm/reload_Click new MainParamatersForm().LoadModbusData(i, SERIAL_READ, 0xB9, 11): " + i + ": " + SERIAL_READ);
                    try
                    {
                        hexString = string.Join("", Array.ConvertAll(StaticModelValues.register_Settings, val => val.ToString("X")));
                    }
                    catch (Exception ex)
                    {
                        JIMessageBox.ErrorMessage("Serial Data Reading Failed");
                        Logger.Error("SettingForm/reload_Click Inner Exception: " + ex.Message);
                    }

                    string asciiString = ConvertHexStringToAscii(hexString);
                    tbSerial.Text = asciiString;
                    if (tbSerial.Text != "")
                        selectedModule.Items.Add(i + " : " + tbSerial.Text);
                    selectedModule.SelectedIndex = 0;
                }
                Logger.Info("SettingForm/reload_Click End");
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage("Error: " + ex.Message);
                Logger.Error("SettingForm/reload_Click Exception: " + ex.Message);
            }
        }

        private void selectedModule_Click(object sender, EventArgs e)
        {


        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void selectedModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tmp = "000000000000";

            try
            {
                // Check if tmp is null or too short to get the first two digits
                if (string.IsNullOrEmpty(tmp) || tmp.Length < 2)
                {
                    throw new ArgumentException("The selected module text is either null, empty, or too short.");
                }

                // Safely extract the first two digits
                string firstTwoDigits = tmp.Substring(0, 2);

                // Convert the first two digits to an integer
                if (!int.TryParse(firstTwoDigits, out int slaveID))
                {
                    throw new FormatException("The first two characters of the selected module text are not a valid number.");
                }

                // Proceed to load data
                loadData();
            }
            catch (Exception ex)
            {
                // Handle the exception, for example, log it or show a message to the user
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
