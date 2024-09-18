using BusinessLogic;
using BusinessLogic.Model;
using MetroSet_UI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace ENCAPv3.UI
{
    public partial class MainForm : Form//d BasetForm
    {
        Dashboard dashboard;
        AboutUsForm aboutUsForm;
        ExportChart exportChart;
        SettingsForm settingsForm;
        MainParamatersForm mainParamatersForm;
        ExportData exportData;
        KiloWattLab kiloWattLab;

        public static DataTable dt;
        public MainForm()
        {
            InitializeComponent();

        }

        #region DateTimeSetting
        private Timer pollingTimer;
        private void fnDateTime()
        {
            try
            {

                labelDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                labelTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
                if (!string.IsNullOrEmpty(LoginModel.username) && !string.IsNullOrEmpty(LoginModel.password))
                {
                    labelIsGuest.Text = "Admin";
                }
                else
                {
                    labelIsGuest.Text = "Guest";
                }
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }

        }
        private void PollingTimer_Tick(object sender, EventArgs e)
        {
            fnDateTime();
        }
        private void InitializePollingTimer()
        {
            pollingTimer = new Timer();
            pollingTimer.Interval = 500;// Convert.ToInt32(pollingTimeout.Value); // Polling interval in milliseconds (1 second here)
            pollingTimer.Tick += PollingTimer_Tick;
            pollingTimer.Start();
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {

                fnDateTime();
                InitializePollingTimer();
                ////StartSessionTimer();
                pnBtnMenu_Click(null, null);

            }
            catch (Exception)
            {

                throw;
            }

        }




        bool sidebarExpand = true;
        private void SidebarTransaction_Tick(object sender, EventArgs e)  //Timer
        {
            if (sidebarExpand)
            {
                sidebarPanel.Width -= 10;
                if (sidebarPanel.Width <= 65)
                {
                    sidebarExpand = false;
                    SidebarTransaction.Stop();
                    //widthtoSidePanel();
                }
            }
            else
            {
                sidebarPanel.Width += 10;
                if (sidebarPanel.Width >= 166)
                {
                    sidebarExpand = true;
                    SidebarTransaction.Stop();
                    // widthtoSidePanel();

                }
            }
        }
        private void btnHam_Click(object sender, EventArgs e)
        {
            SidebarTransaction.Start();
        }



        //FUNCATIONS
        

        private async void pnBtnAboutUs_Click(object sender, EventArgs e)
        {
            if (dashboard == null)
            {
                dashboard = new Dashboard();
                dashboard.FormClosed += Dashboard_FormClosed;
                dashboard.MdiParent = this;
                dashboard.Dock = DockStyle.Fill;
                dashboard.Show();
            }
            else
            {
                dashboard.Activate();
                await Task.Yield(); // Yield control to ensure UI updates.
            }
            await NavColor("pnBtnAboutUs");
        }
        private async void pnBtnSetting_Click(object sender, EventArgs e)
        {
            pnBtnSetting.BackColor = System.Drawing.Color.Black;
            if (settingsForm == null)
            {
                settingsForm = new SettingsForm();
                settingsForm.FormClosed += SettingsForm_FormClosed;
                settingsForm.MdiParent = this;
                settingsForm.Dock = DockStyle.Fill;
                settingsForm.Show();
            }
            else
            {
                settingsForm.checkIsGuest();
                settingsForm.Activate();
                await Task.Yield(); // Yield control to ensure UI updates.
            }
            Application.DoEvents();
            await NavColor("pnBtnSetting");
        }
        private async  void pnBtnMenu_Click(object sender, EventArgs e)
        {
            if (mainParamatersForm == null)
            {
                mainParamatersForm = new MainParamatersForm();
                mainParamatersForm.FormClosed += MainParamatersForm_FormClosed;
                mainParamatersForm.MdiParent = this;
                mainParamatersForm.Dock = DockStyle.Fill;
                mainParamatersForm.Show();
            }
            else
            {

                mainParamatersForm.Activate();
                //await  mainParamatersForm.UpdateSettingFormTextbox();
                await Task.Yield();
            }
            Application.DoEvents();
            await NavColor("pnBtnMenu");
        }
        private async void pnBtnDashboard_Click(object sender, EventArgs e)
        {
             
            if (kiloWattLab == null)
            {
                kiloWattLab = new KiloWattLab();
                kiloWattLab.FormClosed += KiloWattLab_FormClosed;
                kiloWattLab.MdiParent = this;
                kiloWattLab.Dock = DockStyle.Fill;
                kiloWattLab.Show();
            }
            else
            {
                // dt, "KilowaatForm");
                kiloWattLab.Activate();
                await Task.Yield(); // Yield control to ensure UI updates.
                // this.Activated += kiloWattLab.KiloWattLab_Activated; // Attach the Activated event handler
            }
            Application.DoEvents();
            await NavColor("pnBtnDashboard");

        }
        private async  void pnBtnDataExport_Click(object sender, EventArgs e)
        {
           
            if (exportData == null)
            {
                exportData = new ExportData();
                exportData.FormClosed += DataExport_FormClosed;
                exportData.MdiParent = this;
                exportData.Dock = DockStyle.Fill;
                exportData.Show();
            }
            else
            {
                exportData.ExportData_Load(null,null);
                exportData.Activate();
                await Task.Yield(); // Yield control to ensure UI updates.
            }
            Application.DoEvents();
            await NavColor("pnBtnDataExport");
        }

        public async Task  NavColor(string tab)
        {
            // Simulate an async operation
            await Task.Yield(); // Yields control back to the UI thread.


            if (tab == "pnBtnMenu")
            {
                pnBtnMenu.BackColor = System.Drawing.Color.Black;
                pnBtnAboutUs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnDataExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            }
            if (tab == "pnBtnAboutUs")
            {
                pnBtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57))))); 
                pnBtnAboutUs.BackColor = System.Drawing.Color.Black;
                pnBtnSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnDataExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            }
            if (tab == "pnBtnSetting")
            {
                pnBtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnAboutUs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnSetting.BackColor = System.Drawing.Color.Black; 
                pnBtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnDataExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            }
            if (tab == "pnBtnMenu")
            {
                pnBtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnAboutUs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnMenu.BackColor = System.Drawing.Color.Black; 
                pnBtnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnDataExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            }
            if (tab == "pnBtnDashboard")
            {
                pnBtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnAboutUs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnDashboard.BackColor = System.Drawing.Color.Black; 
                pnBtnDataExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            }
            if (tab == "pnBtnDataExport")
            {
                pnBtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnAboutUs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
                pnBtnDataExport.BackColor = System.Drawing.Color.Black; 
            }
        }

        private void ExportChart_FormClosed(object sender, FormClosedEventArgs e)
        {
            exportChart = null;
        }
        private void AboutUsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            aboutUsForm = null;
        }
        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            settingsForm = null;
        }
        private void MainParamatersForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainParamatersForm = null;
        }
        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            dashboard = null;
        }
        private void KiloWattLab_FormClosed(object sender, FormClosedEventArgs e)
        {
            kiloWattLab = null;
        }
        private void btnX_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void DataExport_FormClosed(object sender, FormClosedEventArgs e)
        {
            exportData = null;
        }

        private void pnBtnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void iconButtonIsGuest_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}
