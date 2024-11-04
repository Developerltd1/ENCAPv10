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
using BusinessLogic.Model;
using System.Threading;
using System.IO.Ports;

namespace EMView.UI
{
    public partial class DashBoard1 : Form
    {

        private SerialPort _serialPort;
        private StringBuilder _dataBuffer = new StringBuilder();

        public DashBoard1()
        {
            InitializeComponent();
            
        }
        private void InitializeSerialPort()
        {
            // Initialize and configure the serial port
            _serialPort = new SerialPort
            {
                PortName = "COM3",         // Set COM port here
                BaudRate = 9600,           // Set baud rate
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                Encoding = Encoding.ASCII   // Set encoding (usually ASCII for CAN data)
            };

            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        private void btnTogglePolling1_Click(object sender, EventArgs e)
        {
            string _canBus = "CAN BUS";
            if (_canBus == "CAN BUS")   //uncomment
            {
                InitializeSerialPort();
            }
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                    richTextBox1.AppendText("Started reading CAN data...\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening serial port: " + ex.Message);
            }
            byte[] canDummyPkt = { 0xAA, 0xC8, 0xFF, 0x07, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x55 };
            SendCANPacket(canDummyPkt);
            
            byte[] canBaud9600 = { 0xAA, 0x55, 0x06, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0B};
            SendCANPacket(canBaud9600);

        }

        #region CanBus_Code


        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            int bytesToRead = _serialPort.BytesToRead;
            byte[] buffer = new byte[bytesToRead];
            _serialPort.Read(buffer, 0, bytesToRead);
            string hexData = BitConverter.ToString(buffer).Replace("-", "");
            _dataBuffer.Append(hexData);
            ProcessBuffer();
        }
        private void ProcessBuffer()
        {
            string bufferString = _dataBuffer.ToString();
            List<string> frames = new List<string>();

            while (true)
            {
                try
                {
                    int startIndex = bufferString.IndexOf("AAC8");
                    if (startIndex == -1) break; // No start marker found, exit loop
                    int minimumEndIndex = startIndex + 20 - 2; // -2 because "55" itself is 2 characters long
                    int endIndex = bufferString.IndexOf("55", minimumEndIndex);
                    if (endIndex == -1) break;
                    int frameLength = endIndex - startIndex + 2;
                    if (frameLength >= 20 && frameLength <= 26)
                    {
                        if (frameLength == 26)
                        {
                            string frame = bufferString.Substring(startIndex, frameLength);
                            frames.Add(frame);
                            ParseCANFrame(frame);
                            _dataBuffer.Remove(0, startIndex + frameLength);
                        }
                        else
                        {// If frame is within 20 to 26 but incomplete, wait for more data
                            break;
                        }
                    }
                    else
                    {   // If frame length does not meet the requirements, exit and wait for more data
                        break;
                    }
                    bufferString = _dataBuffer.ToString();
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    break; // Exit the loop if there is an out-of-range exception
                }
            }
            byte[] canDummyPkt = { 0xAA, 0xC8, 0xFF, 0x07, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x55 };
            SendCANPacket(canDummyPkt);
            //AppendFramesToRichTextBox(frames);
        }

        private void ParseCANFrame(string frame)
        {
            string canIdHex = frame.Substring(4, 4); // CAN ID is the first 4 characters (e.g., "0359")
            int canId = Convert.ToInt32(canIdHex, 16); // Convert CAN ID to integer for easier comparison
            byte[] data = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                data[i] = Convert.ToByte(frame.Substring(8 + i * 2, 2), 16);
            }
            switch (canId)
            {
                case 0x5903:// CAN ID 0x359: Parse Protection and Alarm Tables, Module Number
                    bool dischargeOverCurrent = (data[0] & 0x80) != 0; // Bit 7 of Byte 0
                    bool cellUnderTemperature = (data[0] & 0x40) != 0; // Bit 6 of Byte 0
                    byte moduleNumber = data[4];
                    char p = (char)data[5]; // Should be 'P' (0x50)
                    char n = (char)data[6]; // Should be 'N' (0x4E)
                    this.Invoke(new Action(() => richTextBox1.AppendText($"CAN ID 0x359: Module {moduleNumber}, P: {p}, N: {n}" + Environment.NewLine)));
                    break;

                case 0x5103:// CAN ID 0x351: Parse Battery Charge Voltage, Charge/Discharge Current Limits
                    int chargeVoltage = (data[1] << 8) | data[0]; // Combine bytes for 16-bit value
                    int chargeCurrentLimit = (short)((data[3] << 8) | data[2]); // Two's complement
                    int dischargeCurrentLimit = (short)((data[5] << 8) | data[4]); // Two's complement
                    this.Invoke(new Action(() => richTextBox1.AppendText($"CAN ID 0x351: Charge Voltage: {chargeVoltage * 0.1}V, Charge Limit: {chargeCurrentLimit * 0.1}A, Discharge Limit: {dischargeCurrentLimit * 0.1}A" + Environment.NewLine)));
                    break;

                case 0x5503:// CAN ID 0x355: Parse SOC and SOH
                    int soc = (data[1] << 8) | data[0];
                    int soh = (data[3] << 8) | data[2];
                    this.Invoke(new Action(() => richTextBox1.AppendText($"CAN ID 0x355: SOC: {soc}%, SOH: {soh}%" + Environment.NewLine)));
                    break;

                case 0x5603:// CAN ID 0x356: Parse Voltage, Current, Temperature
                    int moduleVoltage = (short)((data[1] << 8) | data[0]); // Two's complement
                    int totalCurrent = (short)((data[3] << 8) | data[2]); // Two's complement
                    int avgTemperature = (short)((data[5] << 8) | data[4]); // Two's complement
                    this.Invoke(new Action(() => richTextBox1.AppendText($"CAN ID 0x356: Module Voltage: {moduleVoltage * 0.01}V, Total Current: {totalCurrent * 0.1}A, Avg Temperature: {avgTemperature * 0.1}°C" + Environment.NewLine)));
                    break;

                case 0x5C03: // CAN ID 0x35C: Parse Request Flags
                    bool chargeEnable = (data[0] & 0x80) != 0; // Bit 7 of Byte 0
                    bool dischargeEnable = (data[0] & 0x40) != 0; // Bit 6 of Byte 0
                    this.Invoke(new Action(() => richTextBox1.AppendText($"CAN ID 0x35C: Charge Enable: {chargeEnable}, Discharge Enable: {dischargeEnable}" + Environment.NewLine)));
                    break;

                default:
                   
                    // Console.WriteLine($"Unknown CAN ID: {canIdHex}");
                    break;
            }
            richTextBox1.Invoke(new Action(() =>
            {
                richTextBox1.ScrollToCaret(); // Scroll to the caret
            }));
        }
        private void SendCANPacket(byte[] packet)
        {
            if (_serialPort.IsOpen)
            {
                try
                {
                    _serialPort.Write(packet, 0, packet.Length);
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
        private void AppendFramesToRichTextBox(List<string> frames)
        {
            foreach (string frame in frames)
            {
                AppendTextToRichTextBox("Frame: " + frame + Environment.NewLine);
            }
        }

        private void AppendTextToRichTextBox(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                // Invoke required to handle cross-thread operation
                richTextBox1.Invoke(new Action(() => richTextBox1.AppendText(text)));
            }
            else
            {
                // If already on UI thread, update directly
                richTextBox1.AppendText(text);
            }

        }

        private void UpdateTextBox(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateTextBox), text);
            }
            else
            {
                richTextBox1.AppendText(text + Environment.NewLine);
            }
        }
        #endregion

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
                richTextBox1.AppendText("Stopped reading CAN data.\n");
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
    }
}
