
namespace ENCAPv3.UI
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.reload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.selectedModule = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bodyPanel = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.iconButton4 = new FontAwesome.Sharp.IconButton();
            this.panel17 = new System.Windows.Forms.Panel();
            this.iconButton3 = new FontAwesome.Sharp.IconButton();
            this.tbSleepTime = new System.Windows.Forms.TextBox();
            this.tbBatteryCapacity = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.tbMaxVoltageDifference = new System.Windows.Forms.TextBox();
            this.tbCellRatedVoltage = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.btnDownloadValue = new FontAwesome.Sharp.IconButton();
            this.panel10 = new System.Windows.Forms.Panel();
            this.tbSerial = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnSetEnergyCharAndDisc = new FontAwesome.Sharp.IconButton();
            this.tbBalanceVoltageDiff = new System.Windows.Forms.TextBox();
            this.tbStartVoltage = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnHighCurrentCharAndDic = new FontAwesome.Sharp.IconButton();
            this.tbHighCurrDischarge = new System.Windows.Forms.TextBox();
            this.tbHighCurrCharge = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.btnHighAndLowSocAlarm = new FontAwesome.Sharp.IconButton();
            this.tbSocLowAlarm = new System.Windows.Forms.TextBox();
            this.tbSocHighAlarm = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnHighAndLowSumVolt = new FontAwesome.Sharp.IconButton();
            this.tbLowSumVolt = new System.Windows.Forms.TextBox();
            this.tbHighSumVolt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.btnHighTempCharAndDic = new FontAwesome.Sharp.IconButton();
            this.tbHighTempDischarge = new System.Windows.Forms.TextBox();
            this.tbHighTempCharge = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.btnUploadValue = new FontAwesome.Sharp.IconButton();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnhighAndLowCellVolt = new FontAwesome.Sharp.IconButton();
            this.tbLowCellVolt = new System.Windows.Forms.TextBox();
            this.tbHighCellVolt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.bodyPanel.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(170)))), ((int)(((byte)(110)))));
            this.panel1.Controls.Add(this.reload);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.selectedModule);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1111, 64);
            this.panel1.TabIndex = 2;
            // 
            // reload
            // 
            this.reload.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.reload.Location = new System.Drawing.Point(950, 24);
            this.reload.Name = "reload";
            this.reload.Size = new System.Drawing.Size(75, 23);
            this.reload.TabIndex = 37;
            this.reload.Text = "Reload";
            this.reload.UseVisualStyleBackColor = true;
            this.reload.Click += new System.EventHandler(this.reload_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(60)))));
            this.label1.Location = new System.Drawing.Point(451, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Selected Module:";
            // 
            // selectedModule
            // 
            this.selectedModule.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.selectedModule.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedModule.FormattingEnabled = true;
            this.selectedModule.Location = new System.Drawing.Point(599, 17);
            this.selectedModule.Name = "selectedModule";
            this.selectedModule.Size = new System.Drawing.Size(336, 32);
            this.selectedModule.TabIndex = 36;
            this.selectedModule.SelectedIndexChanged += new System.EventHandler(this.selectedModule_SelectedIndexChanged);
            this.selectedModule.Click += new System.EventHandler(this.selectedModule_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(60)))));
            this.label2.Location = new System.Drawing.Point(73, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Module Settings";
            // 
            // bodyPanel
            // 
            this.bodyPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bodyPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.bodyPanel.Controls.Add(this.progressBar1);
            this.bodyPanel.Controls.Add(this.iconButton4);
            this.bodyPanel.Controls.Add(this.panel17);
            this.bodyPanel.Controls.Add(this.panel16);
            this.bodyPanel.Controls.Add(this.btnDownloadValue);
            this.bodyPanel.Controls.Add(this.panel10);
            this.bodyPanel.Controls.Add(this.panel8);
            this.bodyPanel.Controls.Add(this.panel7);
            this.bodyPanel.Controls.Add(this.panel13);
            this.bodyPanel.Controls.Add(this.panel5);
            this.bodyPanel.Controls.Add(this.panel14);
            this.bodyPanel.Controls.Add(this.btnUploadValue);
            this.bodyPanel.Controls.Add(this.panel9);
            this.bodyPanel.Location = new System.Drawing.Point(0, 64);
            this.bodyPanel.Name = "bodyPanel";
            this.bodyPanel.Size = new System.Drawing.Size(1111, 655);
            this.bodyPanel.TabIndex = 3;
            this.bodyPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.bodyPanel_Paint);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.BackColor = System.Drawing.Color.Red;
            this.progressBar1.Location = new System.Drawing.Point(0, 1);
            this.progressBar1.Maximum = 20;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1111, 22);
            this.progressBar1.TabIndex = 36;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // iconButton4
            // 
            this.iconButton4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.iconButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(192)))), ((int)(((byte)(139)))));
            this.iconButton4.FlatAppearance.BorderSize = 0;
            this.iconButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton4.ForeColor = System.Drawing.Color.White;
            this.iconButton4.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconButton4.IconColor = System.Drawing.Color.White;
            this.iconButton4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton4.IconSize = 32;
            this.iconButton4.Location = new System.Drawing.Point(201, 515);
            this.iconButton4.Margin = new System.Windows.Forms.Padding(2);
            this.iconButton4.Name = "iconButton4";
            this.iconButton4.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.iconButton4.Size = new System.Drawing.Size(119, 39);
            this.iconButton4.TabIndex = 27;
            this.iconButton4.Text = "Load Data";
            this.iconButton4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton4.UseVisualStyleBackColor = false;
            this.iconButton4.Click += new System.EventHandler(this.iconButton4_Click);
            // 
            // panel17
            // 
            this.panel17.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.panel17.Controls.Add(this.iconButton3);
            this.panel17.Controls.Add(this.tbSleepTime);
            this.panel17.Controls.Add(this.tbBatteryCapacity);
            this.panel17.Controls.Add(this.label26);
            this.panel17.Controls.Add(this.label27);
            this.panel17.Location = new System.Drawing.Point(557, 208);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(531, 75);
            this.panel17.TabIndex = 35;
            // 
            // iconButton3
            // 
            this.iconButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(192)))), ((int)(((byte)(139)))));
            this.iconButton3.FlatAppearance.BorderSize = 0;
            this.iconButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton3.ForeColor = System.Drawing.Color.White;
            this.iconButton3.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconButton3.IconColor = System.Drawing.Color.White;
            this.iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton3.IconSize = 32;
            this.iconButton3.Location = new System.Drawing.Point(435, 33);
            this.iconButton3.Margin = new System.Windows.Forms.Padding(2);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.iconButton3.Size = new System.Drawing.Size(73, 39);
            this.iconButton3.TabIndex = 14;
            this.iconButton3.Text = "SET";
            this.iconButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton3.UseVisualStyleBackColor = false;
            this.iconButton3.Click += new System.EventHandler(this.iconButton3_Click);
            // 
            // tbSleepTime
            // 
            this.tbSleepTime.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSleepTime.Location = new System.Drawing.Point(232, 33);
            this.tbSleepTime.Name = "tbSleepTime";
            this.tbSleepTime.Size = new System.Drawing.Size(203, 39);
            this.tbSleepTime.TabIndex = 26;
            // 
            // tbBatteryCapacity
            // 
            this.tbBatteryCapacity.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBatteryCapacity.Location = new System.Drawing.Point(23, 33);
            this.tbBatteryCapacity.Name = "tbBatteryCapacity";
            this.tbBatteryCapacity.Size = new System.Drawing.Size(203, 39);
            this.tbBatteryCapacity.TabIndex = 25;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label26.Location = new System.Drawing.Point(228, 5);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(107, 21);
            this.label26.TabIndex = 15;
            this.label26.Text = "Sleep Time (s)";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label27.Location = new System.Drawing.Point(19, 5);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(168, 21);
            this.label27.TabIndex = 13;
            this.label27.Text = "Battery Capacity (kWh)";
            // 
            // panel16
            // 
            this.panel16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.panel16.Controls.Add(this.iconButton2);
            this.panel16.Controls.Add(this.tbMaxVoltageDifference);
            this.panel16.Controls.Add(this.tbCellRatedVoltage);
            this.panel16.Controls.Add(this.label24);
            this.panel16.Controls.Add(this.label25);
            this.panel16.Location = new System.Drawing.Point(557, 131);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(531, 75);
            this.panel16.TabIndex = 34;
            // 
            // iconButton2
            // 
            this.iconButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(192)))), ((int)(((byte)(139)))));
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton2.ForeColor = System.Drawing.Color.White;
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconButton2.IconColor = System.Drawing.Color.White;
            this.iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton2.IconSize = 32;
            this.iconButton2.Location = new System.Drawing.Point(435, 33);
            this.iconButton2.Margin = new System.Windows.Forms.Padding(2);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.iconButton2.Size = new System.Drawing.Size(73, 39);
            this.iconButton2.TabIndex = 14;
            this.iconButton2.Text = "SET";
            this.iconButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.UseVisualStyleBackColor = false;
            this.iconButton2.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // tbMaxVoltageDifference
            // 
            this.tbMaxVoltageDifference.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMaxVoltageDifference.Location = new System.Drawing.Point(232, 33);
            this.tbMaxVoltageDifference.Name = "tbMaxVoltageDifference";
            this.tbMaxVoltageDifference.Size = new System.Drawing.Size(203, 39);
            this.tbMaxVoltageDifference.TabIndex = 26;
            // 
            // tbCellRatedVoltage
            // 
            this.tbCellRatedVoltage.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCellRatedVoltage.Location = new System.Drawing.Point(23, 33);
            this.tbCellRatedVoltage.Name = "tbCellRatedVoltage";
            this.tbCellRatedVoltage.Size = new System.Drawing.Size(203, 39);
            this.tbCellRatedVoltage.TabIndex = 25;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label24.Location = new System.Drawing.Point(228, 5);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(204, 21);
            this.label24.TabIndex = 15;
            this.label24.Text = "Max Voltage Difference(mV)";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label25.Location = new System.Drawing.Point(19, 5);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(136, 21);
            this.label25.TabIndex = 13;
            this.label25.Text = "Cell Rated Voltage";
            // 
            // btnDownloadValue
            // 
            this.btnDownloadValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDownloadValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(170)))), ((int)(((byte)(110)))));
            this.btnDownloadValue.FlatAppearance.BorderSize = 0;
            this.btnDownloadValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadValue.ForeColor = System.Drawing.Color.White;
            this.btnDownloadValue.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnDownloadValue.IconColor = System.Drawing.Color.White;
            this.btnDownloadValue.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDownloadValue.IconSize = 32;
            this.btnDownloadValue.Location = new System.Drawing.Point(80, 456);
            this.btnDownloadValue.Margin = new System.Windows.Forms.Padding(2);
            this.btnDownloadValue.Name = "btnDownloadValue";
            this.btnDownloadValue.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnDownloadValue.Size = new System.Drawing.Size(171, 42);
            this.btnDownloadValue.TabIndex = 33;
            this.btnDownloadValue.Text = "Download Value";
            this.btnDownloadValue.UseVisualStyleBackColor = false;
            this.btnDownloadValue.Click += new System.EventHandler(this.btnDownloadValue_Click);
            // 
            // panel10
            // 
            this.panel10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.panel10.Controls.Add(this.tbSerial);
            this.panel10.Controls.Add(this.label15);
            this.panel10.Location = new System.Drawing.Point(557, 284);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(531, 75);
            this.panel10.TabIndex = 22;
            // 
            // tbSerial
            // 
            this.tbSerial.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSerial.Location = new System.Drawing.Point(23, 32);
            this.tbSerial.Name = "tbSerial";
            this.tbSerial.Size = new System.Drawing.Size(485, 39);
            this.tbSerial.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label15.Location = new System.Drawing.Point(11, 8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 21);
            this.label15.TabIndex = 13;
            this.label15.Text = "Serial";
            // 
            // panel8
            // 
            this.panel8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.panel8.Controls.Add(this.btnSetEnergyCharAndDisc);
            this.panel8.Controls.Add(this.tbBalanceVoltageDiff);
            this.panel8.Controls.Add(this.tbStartVoltage);
            this.panel8.Controls.Add(this.label13);
            this.panel8.Controls.Add(this.label14);
            this.panel8.Location = new System.Drawing.Point(557, 58);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(531, 75);
            this.panel8.TabIndex = 31;
            // 
            // btnSetEnergyCharAndDisc
            // 
            this.btnSetEnergyCharAndDisc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(192)))), ((int)(((byte)(139)))));
            this.btnSetEnergyCharAndDisc.FlatAppearance.BorderSize = 0;
            this.btnSetEnergyCharAndDisc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetEnergyCharAndDisc.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetEnergyCharAndDisc.ForeColor = System.Drawing.Color.White;
            this.btnSetEnergyCharAndDisc.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnSetEnergyCharAndDisc.IconColor = System.Drawing.Color.White;
            this.btnSetEnergyCharAndDisc.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSetEnergyCharAndDisc.IconSize = 32;
            this.btnSetEnergyCharAndDisc.Location = new System.Drawing.Point(435, 33);
            this.btnSetEnergyCharAndDisc.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetEnergyCharAndDisc.Name = "btnSetEnergyCharAndDisc";
            this.btnSetEnergyCharAndDisc.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnSetEnergyCharAndDisc.Size = new System.Drawing.Size(73, 39);
            this.btnSetEnergyCharAndDisc.TabIndex = 14;
            this.btnSetEnergyCharAndDisc.Text = "SET";
            this.btnSetEnergyCharAndDisc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetEnergyCharAndDisc.UseVisualStyleBackColor = false;
            this.btnSetEnergyCharAndDisc.Click += new System.EventHandler(this.btnSetEnergyCharAndDisc_Click);
            // 
            // tbBalanceVoltageDiff
            // 
            this.tbBalanceVoltageDiff.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBalanceVoltageDiff.Location = new System.Drawing.Point(232, 33);
            this.tbBalanceVoltageDiff.Name = "tbBalanceVoltageDiff";
            this.tbBalanceVoltageDiff.Size = new System.Drawing.Size(203, 39);
            this.tbBalanceVoltageDiff.TabIndex = 26;
            // 
            // tbStartVoltage
            // 
            this.tbStartVoltage.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbStartVoltage.Location = new System.Drawing.Point(23, 33);
            this.tbStartVoltage.Name = "tbStartVoltage";
            this.tbStartVoltage.Size = new System.Drawing.Size(203, 39);
            this.tbStartVoltage.TabIndex = 25;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label13.Location = new System.Drawing.Point(228, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(194, 21);
            this.label13.TabIndex = 15;
            this.label13.Text = "Balance Voltage Difference";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label14.Location = new System.Drawing.Point(19, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(155, 21);
            this.label14.TabIndex = 13;
            this.label14.Text = "Balance Start Voltage";
            // 
            // panel7
            // 
            this.panel7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.panel7.Controls.Add(this.btnHighCurrentCharAndDic);
            this.panel7.Controls.Add(this.tbHighCurrDischarge);
            this.panel7.Controls.Add(this.tbHighCurrCharge);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Controls.Add(this.label12);
            this.panel7.Location = new System.Drawing.Point(20, 208);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(531, 75);
            this.panel7.TabIndex = 28;
            // 
            // btnHighCurrentCharAndDic
            // 
            this.btnHighCurrentCharAndDic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(192)))), ((int)(((byte)(139)))));
            this.btnHighCurrentCharAndDic.FlatAppearance.BorderSize = 0;
            this.btnHighCurrentCharAndDic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHighCurrentCharAndDic.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHighCurrentCharAndDic.ForeColor = System.Drawing.Color.White;
            this.btnHighCurrentCharAndDic.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnHighCurrentCharAndDic.IconColor = System.Drawing.Color.White;
            this.btnHighCurrentCharAndDic.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHighCurrentCharAndDic.IconSize = 32;
            this.btnHighCurrentCharAndDic.Location = new System.Drawing.Point(435, 33);
            this.btnHighCurrentCharAndDic.Margin = new System.Windows.Forms.Padding(2);
            this.btnHighCurrentCharAndDic.Name = "btnHighCurrentCharAndDic";
            this.btnHighCurrentCharAndDic.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnHighCurrentCharAndDic.Size = new System.Drawing.Size(73, 39);
            this.btnHighCurrentCharAndDic.TabIndex = 14;
            this.btnHighCurrentCharAndDic.Text = "SET";
            this.btnHighCurrentCharAndDic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHighCurrentCharAndDic.UseVisualStyleBackColor = false;
            this.btnHighCurrentCharAndDic.Click += new System.EventHandler(this.btnHighCurrentCharAndDic_Click);
            // 
            // tbHighCurrDischarge
            // 
            this.tbHighCurrDischarge.BackColor = System.Drawing.SystemColors.Window;
            this.tbHighCurrDischarge.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHighCurrDischarge.Location = new System.Drawing.Point(232, 33);
            this.tbHighCurrDischarge.Name = "tbHighCurrDischarge";
            this.tbHighCurrDischarge.Size = new System.Drawing.Size(203, 39);
            this.tbHighCurrDischarge.TabIndex = 26;
            // 
            // tbHighCurrCharge
            // 
            this.tbHighCurrCharge.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHighCurrCharge.Location = new System.Drawing.Point(23, 33);
            this.tbHighCurrCharge.Name = "tbHighCurrCharge";
            this.tbHighCurrCharge.Size = new System.Drawing.Size(203, 39);
            this.tbHighCurrCharge.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label11.Location = new System.Drawing.Point(228, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(143, 21);
            this.label11.TabIndex = 15;
            this.label11.Text = "High Curr. DisChar.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label12.Location = new System.Drawing.Point(19, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(135, 21);
            this.label12.TabIndex = 13;
            this.label12.Text = "High Curr. Charge";
            // 
            // panel13
            // 
            this.panel13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.panel13.Controls.Add(this.btnHighAndLowSocAlarm);
            this.panel13.Controls.Add(this.tbSocLowAlarm);
            this.panel13.Controls.Add(this.tbSocHighAlarm);
            this.panel13.Controls.Add(this.label18);
            this.panel13.Controls.Add(this.label19);
            this.panel13.Location = new System.Drawing.Point(20, 358);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(531, 75);
            this.panel13.TabIndex = 30;
            // 
            // btnHighAndLowSocAlarm
            // 
            this.btnHighAndLowSocAlarm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(192)))), ((int)(((byte)(139)))));
            this.btnHighAndLowSocAlarm.FlatAppearance.BorderSize = 0;
            this.btnHighAndLowSocAlarm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHighAndLowSocAlarm.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHighAndLowSocAlarm.ForeColor = System.Drawing.Color.White;
            this.btnHighAndLowSocAlarm.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnHighAndLowSocAlarm.IconColor = System.Drawing.Color.White;
            this.btnHighAndLowSocAlarm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHighAndLowSocAlarm.IconSize = 32;
            this.btnHighAndLowSocAlarm.Location = new System.Drawing.Point(435, 33);
            this.btnHighAndLowSocAlarm.Margin = new System.Windows.Forms.Padding(2);
            this.btnHighAndLowSocAlarm.Name = "btnHighAndLowSocAlarm";
            this.btnHighAndLowSocAlarm.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnHighAndLowSocAlarm.Size = new System.Drawing.Size(73, 39);
            this.btnHighAndLowSocAlarm.TabIndex = 14;
            this.btnHighAndLowSocAlarm.Text = "SET";
            this.btnHighAndLowSocAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHighAndLowSocAlarm.UseVisualStyleBackColor = false;
            this.btnHighAndLowSocAlarm.Click += new System.EventHandler(this.btnHighAndLowSocAlarm_Click);
            // 
            // tbSocLowAlarm
            // 
            this.tbSocLowAlarm.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSocLowAlarm.Location = new System.Drawing.Point(232, 33);
            this.tbSocLowAlarm.Name = "tbSocLowAlarm";
            this.tbSocLowAlarm.Size = new System.Drawing.Size(203, 39);
            this.tbSocLowAlarm.TabIndex = 26;
            // 
            // tbSocHighAlarm
            // 
            this.tbSocHighAlarm.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSocHighAlarm.Location = new System.Drawing.Point(23, 33);
            this.tbSocHighAlarm.Name = "tbSocHighAlarm";
            this.tbSocHighAlarm.Size = new System.Drawing.Size(203, 39);
            this.tbSocHighAlarm.TabIndex = 25;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label18.Location = new System.Drawing.Point(228, 5);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(120, 21);
            this.label18.TabIndex = 15;
            this.label18.Text = "SOC Low Alarm";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label19.Location = new System.Drawing.Point(19, 5);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(124, 21);
            this.label19.TabIndex = 13;
            this.label19.Text = "SOC High Alarm";
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.panel5.Controls.Add(this.btnHighAndLowSumVolt);
            this.panel5.Controls.Add(this.tbLowSumVolt);
            this.panel5.Controls.Add(this.tbHighSumVolt);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Location = new System.Drawing.Point(20, 133);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(531, 75);
            this.panel5.TabIndex = 27;
            // 
            // btnHighAndLowSumVolt
            // 
            this.btnHighAndLowSumVolt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(192)))), ((int)(((byte)(139)))));
            this.btnHighAndLowSumVolt.FlatAppearance.BorderSize = 0;
            this.btnHighAndLowSumVolt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHighAndLowSumVolt.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHighAndLowSumVolt.ForeColor = System.Drawing.Color.White;
            this.btnHighAndLowSumVolt.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnHighAndLowSumVolt.IconColor = System.Drawing.Color.White;
            this.btnHighAndLowSumVolt.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHighAndLowSumVolt.IconSize = 32;
            this.btnHighAndLowSumVolt.Location = new System.Drawing.Point(435, 33);
            this.btnHighAndLowSumVolt.Margin = new System.Windows.Forms.Padding(2);
            this.btnHighAndLowSumVolt.Name = "btnHighAndLowSumVolt";
            this.btnHighAndLowSumVolt.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnHighAndLowSumVolt.Size = new System.Drawing.Size(73, 39);
            this.btnHighAndLowSumVolt.TabIndex = 14;
            this.btnHighAndLowSumVolt.Text = "SET";
            this.btnHighAndLowSumVolt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHighAndLowSumVolt.UseVisualStyleBackColor = false;
            this.btnHighAndLowSumVolt.Click += new System.EventHandler(this.btnHighAndLowSumVolt_Click);
            // 
            // tbLowSumVolt
            // 
            this.tbLowSumVolt.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLowSumVolt.Location = new System.Drawing.Point(232, 33);
            this.tbLowSumVolt.Name = "tbLowSumVolt";
            this.tbLowSumVolt.Size = new System.Drawing.Size(203, 39);
            this.tbLowSumVolt.TabIndex = 26;
            // 
            // tbHighSumVolt
            // 
            this.tbHighSumVolt.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHighSumVolt.Location = new System.Drawing.Point(23, 33);
            this.tbHighSumVolt.Name = "tbHighSumVolt";
            this.tbHighSumVolt.Size = new System.Drawing.Size(203, 39);
            this.tbHighSumVolt.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label8.Location = new System.Drawing.Point(228, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 21);
            this.label8.TabIndex = 15;
            this.label8.Text = "Low Sum Voltage";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label9.Location = new System.Drawing.Point(19, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(135, 21);
            this.label9.TabIndex = 13;
            this.label9.Text = "High Sum Voltage";
            // 
            // panel14
            // 
            this.panel14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.panel14.Controls.Add(this.btnHighTempCharAndDic);
            this.panel14.Controls.Add(this.tbHighTempDischarge);
            this.panel14.Controls.Add(this.tbHighTempCharge);
            this.panel14.Controls.Add(this.label20);
            this.panel14.Controls.Add(this.label21);
            this.panel14.Location = new System.Drawing.Point(20, 283);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(531, 75);
            this.panel14.TabIndex = 29;
            // 
            // btnHighTempCharAndDic
            // 
            this.btnHighTempCharAndDic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(192)))), ((int)(((byte)(139)))));
            this.btnHighTempCharAndDic.FlatAppearance.BorderSize = 0;
            this.btnHighTempCharAndDic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHighTempCharAndDic.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHighTempCharAndDic.ForeColor = System.Drawing.Color.White;
            this.btnHighTempCharAndDic.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnHighTempCharAndDic.IconColor = System.Drawing.Color.White;
            this.btnHighTempCharAndDic.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHighTempCharAndDic.IconSize = 32;
            this.btnHighTempCharAndDic.Location = new System.Drawing.Point(435, 33);
            this.btnHighTempCharAndDic.Margin = new System.Windows.Forms.Padding(2);
            this.btnHighTempCharAndDic.Name = "btnHighTempCharAndDic";
            this.btnHighTempCharAndDic.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnHighTempCharAndDic.Size = new System.Drawing.Size(73, 39);
            this.btnHighTempCharAndDic.TabIndex = 14;
            this.btnHighTempCharAndDic.Text = "SET";
            this.btnHighTempCharAndDic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHighTempCharAndDic.UseVisualStyleBackColor = false;
            this.btnHighTempCharAndDic.Click += new System.EventHandler(this.btnHighTempCharAndDic_Click);
            // 
            // tbHighTempDischarge
            // 
            this.tbHighTempDischarge.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHighTempDischarge.Location = new System.Drawing.Point(232, 33);
            this.tbHighTempDischarge.Name = "tbHighTempDischarge";
            this.tbHighTempDischarge.Size = new System.Drawing.Size(203, 39);
            this.tbHighTempDischarge.TabIndex = 26;
            // 
            // tbHighTempCharge
            // 
            this.tbHighTempCharge.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHighTempCharge.Location = new System.Drawing.Point(23, 33);
            this.tbHighTempCharge.Name = "tbHighTempCharge";
            this.tbHighTempCharge.Size = new System.Drawing.Size(203, 39);
            this.tbHighTempCharge.TabIndex = 25;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label20.Location = new System.Drawing.Point(228, 5);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(149, 21);
            this.label20.TabIndex = 15;
            this.label20.Text = "High Temp. DisChar.";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label21.Location = new System.Drawing.Point(19, 5);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(141, 21);
            this.label21.TabIndex = 13;
            this.label21.Text = "High Temp. Charge";
            // 
            // btnUploadValue
            // 
            this.btnUploadValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUploadValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(170)))), ((int)(((byte)(110)))));
            this.btnUploadValue.FlatAppearance.BorderSize = 0;
            this.btnUploadValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadValue.ForeColor = System.Drawing.Color.White;
            this.btnUploadValue.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnUploadValue.IconColor = System.Drawing.Color.White;
            this.btnUploadValue.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnUploadValue.IconSize = 32;
            this.btnUploadValue.Location = new System.Drawing.Point(271, 456);
            this.btnUploadValue.Margin = new System.Windows.Forms.Padding(2);
            this.btnUploadValue.Name = "btnUploadValue";
            this.btnUploadValue.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnUploadValue.Size = new System.Drawing.Size(171, 42);
            this.btnUploadValue.TabIndex = 24;
            this.btnUploadValue.Text = "Upload Value";
            this.btnUploadValue.UseVisualStyleBackColor = false;
            this.btnUploadValue.Click += new System.EventHandler(this.btnUploadValue_Click);
            // 
            // panel9
            // 
            this.panel9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.panel9.Controls.Add(this.btnhighAndLowCellVolt);
            this.panel9.Controls.Add(this.tbLowCellVolt);
            this.panel9.Controls.Add(this.tbHighCellVolt);
            this.panel9.Controls.Add(this.label6);
            this.panel9.Controls.Add(this.label10);
            this.panel9.Location = new System.Drawing.Point(20, 58);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(531, 75);
            this.panel9.TabIndex = 17;
            // 
            // btnhighAndLowCellVolt
            // 
            this.btnhighAndLowCellVolt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(192)))), ((int)(((byte)(139)))));
            this.btnhighAndLowCellVolt.FlatAppearance.BorderSize = 0;
            this.btnhighAndLowCellVolt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnhighAndLowCellVolt.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhighAndLowCellVolt.ForeColor = System.Drawing.Color.White;
            this.btnhighAndLowCellVolt.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnhighAndLowCellVolt.IconColor = System.Drawing.Color.White;
            this.btnhighAndLowCellVolt.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnhighAndLowCellVolt.IconSize = 32;
            this.btnhighAndLowCellVolt.Location = new System.Drawing.Point(435, 33);
            this.btnhighAndLowCellVolt.Margin = new System.Windows.Forms.Padding(2);
            this.btnhighAndLowCellVolt.Name = "btnhighAndLowCellVolt";
            this.btnhighAndLowCellVolt.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnhighAndLowCellVolt.Size = new System.Drawing.Size(73, 39);
            this.btnhighAndLowCellVolt.TabIndex = 14;
            this.btnhighAndLowCellVolt.Text = "SET";
            this.btnhighAndLowCellVolt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnhighAndLowCellVolt.UseVisualStyleBackColor = false;
            this.btnhighAndLowCellVolt.Click += new System.EventHandler(this.btnhighAndLowCellVolt_Click);
            // 
            // tbLowCellVolt
            // 
            this.tbLowCellVolt.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLowCellVolt.Location = new System.Drawing.Point(232, 33);
            this.tbLowCellVolt.Name = "tbLowCellVolt";
            this.tbLowCellVolt.Size = new System.Drawing.Size(203, 39);
            this.tbLowCellVolt.TabIndex = 26;
            // 
            // tbHighCellVolt
            // 
            this.tbHighCellVolt.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHighCellVolt.Location = new System.Drawing.Point(23, 33);
            this.tbHighCellVolt.Name = "tbHighCellVolt";
            this.tbHighCellVolt.Size = new System.Drawing.Size(203, 39);
            this.tbHighCellVolt.TabIndex = 25;
            this.tbHighCellVolt.TextChanged += new System.EventHandler(this.tbHighCellVolt_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label6.Location = new System.Drawing.Point(228, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 21);
            this.label6.TabIndex = 15;
            this.label6.Text = "Low Cell Voltage";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label10.Location = new System.Drawing.Point(19, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 21);
            this.label10.TabIndex = 13;
            this.label10.Text = "High Cell Voltage";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(1111, 720);
            this.Controls.Add(this.bodyPanel);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.bodyPanel.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel bodyPanel;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.TextBox tbSerial;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel8;
        private FontAwesome.Sharp.IconButton btnSetEnergyCharAndDisc;
        private System.Windows.Forms.TextBox tbBalanceVoltageDiff;
        private System.Windows.Forms.TextBox tbStartVoltage;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel7;
        private FontAwesome.Sharp.IconButton btnHighCurrentCharAndDic;
        private System.Windows.Forms.TextBox tbHighCurrDischarge;
        private System.Windows.Forms.TextBox tbHighCurrCharge;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel13;
        private FontAwesome.Sharp.IconButton btnHighAndLowSocAlarm;
        private System.Windows.Forms.TextBox tbSocLowAlarm;
        private System.Windows.Forms.TextBox tbSocHighAlarm;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel5;
        private FontAwesome.Sharp.IconButton btnHighAndLowSumVolt;
        private System.Windows.Forms.TextBox tbLowSumVolt;
        private System.Windows.Forms.TextBox tbHighSumVolt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel14;
        private FontAwesome.Sharp.IconButton btnHighTempCharAndDic;
        private System.Windows.Forms.TextBox tbHighTempDischarge;
        private System.Windows.Forms.TextBox tbHighTempCharge;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private FontAwesome.Sharp.IconButton btnhighAndLowCellVolt;
        private System.Windows.Forms.TextBox tbLowCellVolt;
        private System.Windows.Forms.TextBox tbHighCellVolt;
        private System.Windows.Forms.Panel panel17;
        private FontAwesome.Sharp.IconButton iconButton3;
        private System.Windows.Forms.TextBox tbSleepTime;
        private System.Windows.Forms.TextBox tbBatteryCapacity;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel panel16;
        private FontAwesome.Sharp.IconButton iconButton2;
        private System.Windows.Forms.TextBox tbMaxVoltageDifference;
        private System.Windows.Forms.TextBox tbCellRatedVoltage;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private FontAwesome.Sharp.IconButton btnDownloadValue;
        private FontAwesome.Sharp.IconButton btnUploadValue;
        private FontAwesome.Sharp.IconButton iconButton4;
        private System.Windows.Forms.ComboBox selectedModule;
        private System.Windows.Forms.Button reload;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}