using BusinessLogic;
using EasyModbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMView.UI
{
    public partial class KiloWattLab : Form
    {
        public int selectedModule = 1;
        private MainParamatersForm _mainParametersForm;
        public KiloWattLab(MainParamatersForm mainParametersForm)
        {
            InitializeComponent();
            _mainParametersForm = mainParametersForm;
        }

        #region RealTimeComplie

        private ModbusClient modbusClient;
        private bool isPollingEnabled = false;
        private Timer pollingTimer;
        private DateTime startTime;

        private void KiloWattLab_Load(object sender, EventArgs e)
        {
            try
            {
                isPollingEnabled = !isPollingEnabled;
                if (isPollingEnabled)
                {
                    InitializePollingTimer();
                    pollingTimer.Start();
                    //new MainParamatersForm().StartPollingDatabase();
                }
                else
                {
                    StopPolling();
                    //new MainParamatersForm().StopPollingDatabase();
                }

            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage("2:" + ex.Message);
            }


        }


        private async void PollingTimer_Tick(object sender, EventArgs e)
        {
            int dataPageIndex;
            try
            {
                if (MainParamatersForm.isPollSelected)
                {

                    // DataTable ddt = _mainParametersForm.fnpublic();
                    DataTable ddt = await Task.Run(() => _mainParametersForm.fnpublic());
                    if (ddt != null)
                    {
                        this.Invoke((Action)(() =>
                        {
                            labelModule1Serial.Text = ddt.Rows[0][1].ToString();
                            labelModule2Serial.Text = ddt.Rows[0][2].ToString();
                            labelModule3Serial.Text = ddt.Rows[0][3].ToString();

                            labelModule4Serial.Text = ddt.Rows[0][4].ToString();
                            labelModule5Serial.Text = ddt.Rows[0][5].ToString();
                            labelModule6Serial.Text = ddt.Rows[0][6].ToString();
                            labelModule7Serial.Text = ddt.Rows[0][7].ToString();
                            labelModule8Serial.Text = ddt.Rows[0][8].ToString();
                            labelModule9Serial.Text = ddt.Rows[0][9].ToString();
                            labelModule10Serial.Text = ddt.Rows[0][10].ToString();
                            labelModule11Serial.Text = ddt.Rows[0][11].ToString();
                            labelModule12Serial.Text = ddt.Rows[0][12].ToString();
                            labelModule13Serial.Text = ddt.Rows[0][13].ToString();
                            labelModule14Serial.Text = ddt.Rows[0][14].ToString();
                            labelModule15Serial.Text = ddt.Rows[0][15].ToString();
                            labelModule16Serial.Text = ddt.Rows[0][16].ToString();
                            labelModule17Serial.Text = ddt.Rows[0][17].ToString();
                            labelModule18Serial.Text = ddt.Rows[0][18].ToString();
                            labelModule19Serial.Text = ddt.Rows[0][19].ToString();
                            labelModule20Serial.Text = ddt.Rows[0][20].ToString();


                            dataPageIndex = 1;
                            labelModule1Volts.Text = ddt.Rows[dataPageIndex][1].ToString() + " V";
                            labelModule2Volts.Text = ddt.Rows[dataPageIndex][2].ToString() + " V";
                            labelModule3Volts.Text = ddt.Rows[dataPageIndex][3].ToString() + " V";
                            labelModule4Volts.Text = ddt.Rows[dataPageIndex][4].ToString() + " V";
                            labelModule5Volts.Text = ddt.Rows[dataPageIndex][5].ToString() + " V";
                            labelModule6Volts.Text = ddt.Rows[dataPageIndex][6].ToString() + " V";
                            labelModule7Volts.Text = ddt.Rows[dataPageIndex][7].ToString() + " V";
                            labelModule8Volts.Text = ddt.Rows[dataPageIndex][8].ToString() + " V";
                            labelModule9Volts.Text = ddt.Rows[dataPageIndex][9].ToString() + " V";
                            labelModule10Volts.Text = ddt.Rows[dataPageIndex][10].ToString() + " V";
                            labelModule11Volts.Text = ddt.Rows[dataPageIndex][11].ToString() + " V";
                            labelModule12Volts.Text = ddt.Rows[dataPageIndex][12].ToString() + " V";
                            labelModule13Volts.Text = ddt.Rows[dataPageIndex][13].ToString() + " V";
                            labelModule14Volts.Text = ddt.Rows[dataPageIndex][14].ToString() + " V";
                            labelModule15Volts.Text = ddt.Rows[dataPageIndex][15].ToString() + " V";
                            labelModule16Volts.Text = ddt.Rows[dataPageIndex][16].ToString() + " V";
                            labelModule17Volts.Text = ddt.Rows[dataPageIndex][17].ToString() + " V";
                            labelModule18Volts.Text = ddt.Rows[dataPageIndex][18].ToString() + " V";
                            labelModule19Volts.Text = ddt.Rows[dataPageIndex][19].ToString() + " V";
                            labelModule20Volts.Text = ddt.Rows[dataPageIndex][20].ToString() + " V";

                            labelTotalVoltage.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString() + " V";
                            labelTotalCurrent.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString() + " A";
                            labelTotalPower.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString() + " kW";

                            labelModule1SOC.Text = ddt.Rows[dataPageIndex][1].ToString() + " %";
                            labelModule2SOC.Text = ddt.Rows[dataPageIndex][2].ToString() + " %";
                            labelModule3SOC.Text = ddt.Rows[dataPageIndex][3].ToString() + " %";
                            labelModule4SOC.Text = ddt.Rows[dataPageIndex][4].ToString() + " %";
                            labelModule5SOC.Text = ddt.Rows[dataPageIndex][5].ToString() + " %";
                            labelModule6SOC.Text = ddt.Rows[dataPageIndex][6].ToString() + " %";
                            labelModule7SOC.Text = ddt.Rows[dataPageIndex][7].ToString() + " %";
                            labelModule8SOC.Text = ddt.Rows[dataPageIndex][8].ToString() + " %";
                            labelModule9SOC.Text = ddt.Rows[dataPageIndex][9].ToString() + " %";
                            labelModule10SOC.Text = ddt.Rows[dataPageIndex][10].ToString() + " %";
                            labelModule11SOC.Text = ddt.Rows[dataPageIndex][11].ToString() + " %";
                            labelModule12SOC.Text = ddt.Rows[dataPageIndex][12].ToString() + " %";
                            labelModule13SOC.Text = ddt.Rows[dataPageIndex][13].ToString() + " %";
                            labelModule14SOC.Text = ddt.Rows[dataPageIndex][14].ToString() + " %";
                            labelModule15SOC.Text = ddt.Rows[dataPageIndex][15].ToString() + " %";
                            labelModule16SOC.Text = ddt.Rows[dataPageIndex][16].ToString() + " %";
                            labelModule17SOC.Text = ddt.Rows[dataPageIndex][17].ToString() + " %";
                            labelModule18SOC.Text = ddt.Rows[dataPageIndex][18].ToString() + " %";
                            labelModule19SOC.Text = ddt.Rows[dataPageIndex][19].ToString() + " %";
                            labelModule20SOC.Text = ddt.Rows[dataPageIndex][20].ToString() + " %";


                            labelSOC.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString() + " %";
                            labelTemprature.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString() + " C";//Total Remaning Capicity //skipp
                            labelTemprature.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString() + " C";
                            //labelTerminal.Text = ddt.Rows[7]["Battery-1"].ToString() + "-";

                            //lblCell1.Text =  dt.Rows[6]["Battery-1"].ToString();
                            #region Battery1
                            dataPageIndex = 7;
                            lblCell1.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString();
                            lblCell2.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString();
                            lblCell3.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString();
                            lblCell4.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString();
                            lblCell5.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString();
                            lblCell6.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString();
                            lblCell7.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString();
                            lblCell8.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString();
                            lblCell9.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString();
                            lblCell10.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString();
                            lblCell11.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString();
                            lblCell12.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString();
                            lblCell13.Text = ddt.Rows[dataPageIndex++][selectedModule].ToString();
                            #endregion

                            //lblCell16.Text = ddt.Rows[21]["Battery-1"].ToString();
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                this.Invoke((Action)(() =>
                {
                    JIMessageBox.ErrorMessage("1:" + ex.Message);
                }));
            }

        }

        private void InitializePollingTimer()
        {
            pollingTimer = new Timer();
            pollingTimer.Interval = 1000;// Convert.ToInt32(pollingTimeout.Value); // Polling interval in milliseconds (1 second here)
            pollingTimer.Tick += PollingTimer_Tick;
        }

        private void StopPolling()
        {
            pollingTimer.Stop();
            if (modbusClient.Connected)
            {
                modbusClient.Disconnect();
            }
        }
        #endregion
        #region labels

        private void forModuleSync(string txt)
        {
            labelForModule.Text = txt;
            labelForModule.Visible = true;
        }
        private async Task forModule(string txt)
        {
            // Now, updating the UI in the main thread
            if (labelForModule.InvokeRequired)
            {
                labelForModule.Invoke(new Action(() =>
                {
                    labelForModule.Text = txt;
                    labelForModule.Visible = true;
                }));
            }
            else
            {
                labelForModule.Text = txt;
                labelForModule.Visible = true;
            }
        }
        private async void panel19_Paint(object sender, PaintEventArgs e)
        {
            selectedModule = 1;
            await forModule("Module1 Selected");
        }



        private async void panel21_Paint(object sender, PaintEventArgs e)
        {
            selectedModule = 3;
            await forModule("Module3 Selected");
        }

        private async void labelModule_Click(object sender, EventArgs e)
        {
            selectedModule = 1;
            await forModule("Module-1 Selected");
        }

        private async void label27_Click(object sender, EventArgs e)
        {
            selectedModule = 2;
            await forModule("Module-2 Selected");
        }

        private async void label42_Click(object sender, EventArgs e)
        {
            selectedModule = 3;
            await forModule("Module-3 Selected");
        }

        private async void labelModule4Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 4;
            await forModule("Module-4 Selected");
        }

        private async void labelModule5Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 5;
            await forModule("Module-5 Selected");
        }

        private async void labelModule6Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 6;
            await forModule("Module-6 Selected");
        }

        private async void labelModule7Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 7;
            await forModule("Module-7 Selected");
        }

        private async void labelModule8Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 8;
            await forModule("Module-8 Selected");
        }

        private async void labelModule9Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 9;
            await forModule("Module-9 Selected");
        }

        private async void labelModule10Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 10;
            await forModule("Module-10 Selected");
        }

        private async void labelModule11Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 11;
            await forModule("Module-11 Selected");
        }

        private async void labelModule12Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 12;
            await forModule("Module-12 Selected");
        }

        private async void labelModule13Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 13;
            await forModule("Module-13 Selected");
        }

        private async void labelModule14Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 14;
            await forModule("Module-14 Selected");
        }
        private async void label67_Click(object sender, EventArgs e)
        {
            selectedModule = 15;
            await forModule("Module-15 Selected");
        }

        private async void labelModule16Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 16;
            await forModule("Module-16 Selected");
        }

        private async void labelModule17Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 17;
            await forModule("Module-17 Selected");
        }

        private async void labelModule18Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 18;
            await forModule("Module-18 Selected");
        }

        private async void labelModule19Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 19;
            await forModule("Module-19 Selected");
        }

        private async void labelModule20Volts_Click(object sender, EventArgs e)
        {
            selectedModule = 20;
            await forModule("Module-20 Selected");
        }
        #endregion
    }
}
