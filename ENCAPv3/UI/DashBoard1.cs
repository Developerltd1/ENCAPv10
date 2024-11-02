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

        private SerialPort serialPort;
        private Thread readThread;
        private volatile bool keepReading;

        public DashBoard1()
        {
            InitializeComponent();

           


        }
        private void TimerOnTick(object sender, EventArgs e)
        {
            
        }

        private void btnTogglePolling1_Click(object sender, EventArgs e)
        {
            string _canBus = "CAN BUS";
            if (_canBus == "CAN BUS")   //uncomment
            {
                InitializeSerialPort();
            }
        }

        #region CanBus_Code
        private void InitializeSerialPort()
        {
            //serialPort = new SerialPort("COM3", 9600)
            //{
            //    Parity = Parity.None,
            //    StopBits = StopBits.One,
            //    DataBits = 8,
            //    Handshake = Handshake.None,
            //    ReadTimeout = 500,
            //    WriteTimeout = 500
            //};

            
                 serialPort = new SerialPort()
                {
                    PortName = "COM3",
                    BaudRate = 9600,
                    Parity = Parity.None,
                    DataBits = 8,
                    StopBits = StopBits.One,
                    Handshake = Handshake.None,
                    ReadTimeout = -1,
                };

               


            // Start reading when the form loads
            //this.Load += (sender, e) => StartReading();
            //this.FormClosing += (sender, e) => StopReading();
            StartReading();
        }

        private void StartReading()
        {
            try
            {

            
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
                keepReading = true;
                readThread = new Thread(ReadData);
                readThread.Start();
            }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void StopReading()
        {
            keepReading = false;
            readThread?.Join();
            if (serialPort.IsOpen) serialPort.Close();
        }

        private void ReadData()
        {
            int count = 0;
            while (keepReading)
            {
                count++;
                try
                {
                    Thread.Sleep(400);
                    string line = serialPort.ReadExisting(); // Read a line from the serial port
                    if (this.InvokeRequired)
                    {
                       
                        Invoke(new Action(() => richTextBox1.AppendText(count + ": " + line + Environment.NewLine) ));
                        
                    }

                    //if (!string.IsNullOrEmpty(line))
                    //{
                    //    // Parse the CAN bus data
                    //    CanFrame frame = ParseCanFrame(line);
                    //    if (frame != null)
                    //    {
                    //        // Update the UI (TextBox) with the parsed data
                    //        UpdateTextBox($"ID: {frame.Id}, DLC: {frame.Dlc}, Data: {BitConverter.ToString(frame.Data)}");
                    //    }
                    //}
                }
                catch (TimeoutException) { }
                catch (Exception ex)
                {
                    UpdateTextBox("Error: " + ex.Message);
                }
            }
        }

        private CanFrame ParseCanFrame(string rawData)
        {
            try
            {
                // Assume rawData format is: "ID:123 DLC:8 DATA:11 22 33 44 55 66 77 88"
                string[] parts = rawData.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                int id = 0, dlc = 0;
                byte[] data = new byte[8];

                // Parse ID
                foreach (string part in parts)
                {
                    if (part.StartsWith("ID:"))
                    {
                        id = int.Parse(part.Substring(3));
                    }
                    else if (part.StartsWith("DLC:"))
                    {
                        dlc = int.Parse(part.Substring(4));
                    }
                    else if (part.StartsWith("DATA:"))
                    {
                        // Read the data bytes
                        for (int i = 0; i < dlc && i < 8; i++)
                        {
                            data[i] = Convert.ToByte(parts[Array.IndexOf(parts, part) + i + 1], 16);
                        }
                    }
                }

                return new CanFrame { Id = id, Dlc = dlc, Data = data };
            }
            catch (Exception)
            {
                return null;
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
    }
}
