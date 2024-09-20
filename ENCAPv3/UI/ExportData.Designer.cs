
namespace ENCAPv3.UI
{
    partial class ExportData
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.set1 = new ENCAPv3.Set1();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.datePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.datePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbTemprature = new System.Windows.Forms.CheckBox();
            this.btnAlarmReport = new FontAwesome.Sharp.IconButton();
            this.btnExportCSV = new FontAwesome.Sharp.IconButton();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.cbCurrent = new System.Windows.Forms.CheckBox();
            this.cbVoltage = new System.Windows.Forms.CheckBox();
            this.cbPower = new System.Windows.Forms.CheckBox();
            this.cbSOC = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.chartExportData = new LiveCharts.WinForms.CartesianChart();
            this.storePointBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.set1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.storePointBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataTable1BindingSource
            // 
            this.dataTable1BindingSource.DataMember = "DataTable1";
            this.dataTable1BindingSource.DataSource = this.set1;
            // 
            // set1
            // 
            this.set1.DataSetName = "Set1";
            this.set1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.datePickerEndDate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.datePickerStartDate);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1133, 74);
            this.panel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(563, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 24);
            this.label2.TabIndex = 38;
            this.label2.Text = "End Time";
            // 
            // datePickerEndDate
            // 
            this.datePickerEndDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.datePickerEndDate.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.datePickerEndDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerEndDate.Location = new System.Drawing.Point(663, 23);
            this.datePickerEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.datePickerEndDate.Name = "datePickerEndDate";
            this.datePickerEndDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.datePickerEndDate.Size = new System.Drawing.Size(277, 29);
            this.datePickerEndDate.TabIndex = 37;
            this.datePickerEndDate.Value = new System.DateTime(2024, 7, 14, 0, 0, 0, 0);
            this.datePickerEndDate.ValueChanged += new System.EventHandler(this.datePickerEndDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(111, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 24);
            this.label1.TabIndex = 36;
            this.label1.Text = "Start Time";
            // 
            // datePickerStartDate
            // 
            this.datePickerStartDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.datePickerStartDate.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.datePickerStartDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerStartDate.Location = new System.Drawing.Point(212, 21);
            this.datePickerStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.datePickerStartDate.Name = "datePickerStartDate";
            this.datePickerStartDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.datePickerStartDate.Size = new System.Drawing.Size(277, 29);
            this.datePickerStartDate.TabIndex = 35;
            this.datePickerStartDate.Value = new System.DateTime(2024, 7, 14, 0, 0, 0, 0);
            this.datePickerStartDate.ValueChanged += new System.EventHandler(this.datePickerStartDate_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.panel2.Controls.Add(this.cbTemprature);
            this.panel2.Controls.Add(this.btnAlarmReport);
            this.panel2.Controls.Add(this.btnExportCSV);
            this.panel2.Controls.Add(this.cbSelectAll);
            this.panel2.Controls.Add(this.cbCurrent);
            this.panel2.Controls.Add(this.cbVoltage);
            this.panel2.Controls.Add(this.cbPower);
            this.panel2.Controls.Add(this.cbSOC);
            this.panel2.Location = new System.Drawing.Point(1, 77);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1133, 57);
            this.panel2.TabIndex = 39;
            // 
            // cbTemprature
            // 
            this.cbTemprature.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbTemprature.AutoSize = true;
            this.cbTemprature.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTemprature.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbTemprature.Location = new System.Drawing.Point(426, 15);
            this.cbTemprature.Name = "cbTemprature";
            this.cbTemprature.Size = new System.Drawing.Size(109, 21);
            this.cbTemprature.TabIndex = 56;
            this.cbTemprature.Text = "Temperature";
            this.cbTemprature.UseVisualStyleBackColor = true;
            this.cbTemprature.CheckedChanged += new System.EventHandler(this.ChkBox_CheckedChanged);
            // 
            // btnAlarmReport
            // 
            this.btnAlarmReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAlarmReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(192)))), ((int)(((byte)(139)))));
            this.btnAlarmReport.FlatAppearance.BorderSize = 0;
            this.btnAlarmReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlarmReport.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlarmReport.ForeColor = System.Drawing.Color.White;
            this.btnAlarmReport.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnAlarmReport.IconColor = System.Drawing.Color.White;
            this.btnAlarmReport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAlarmReport.IconSize = 32;
            this.btnAlarmReport.Location = new System.Drawing.Point(989, 3);
            this.btnAlarmReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAlarmReport.Name = "btnAlarmReport";
            this.btnAlarmReport.Size = new System.Drawing.Size(128, 46);
            this.btnAlarmReport.TabIndex = 51;
            this.btnAlarmReport.Text = "Alarm Report";
            this.btnAlarmReport.UseVisualStyleBackColor = false;
            this.btnAlarmReport.Click += new System.EventHandler(this.btnAlarmReport_Click);
            // 
            // btnExportCSV
            // 
            this.btnExportCSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportCSV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(192)))), ((int)(((byte)(139)))));
            this.btnExportCSV.FlatAppearance.BorderSize = 0;
            this.btnExportCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportCSV.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportCSV.ForeColor = System.Drawing.Color.White;
            this.btnExportCSV.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnExportCSV.IconColor = System.Drawing.Color.White;
            this.btnExportCSV.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExportCSV.IconSize = 32;
            this.btnExportCSV.Location = new System.Drawing.Point(815, 3);
            this.btnExportCSV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new System.Drawing.Size(168, 46);
            this.btnExportCSV.TabIndex = 55;
            this.btnExportCSV.Text = "Report CSV";
            this.btnExportCSV.UseVisualStyleBackColor = false;
            this.btnExportCSV.Click += new System.EventHandler(this.btnExportCSV_Click);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelectAll.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.cbSelectAll.Location = new System.Drawing.Point(19, 16);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(85, 21);
            this.cbSelectAll.TabIndex = 47;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.ChkBox_CheckedChanged);
            // 
            // cbCurrent
            // 
            this.cbCurrent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbCurrent.AutoSize = true;
            this.cbCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCurrent.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbCurrent.Location = new System.Drawing.Point(212, 15);
            this.cbCurrent.Name = "cbCurrent";
            this.cbCurrent.Size = new System.Drawing.Size(74, 21);
            this.cbCurrent.TabIndex = 45;
            this.cbCurrent.Text = "Current";
            this.cbCurrent.UseVisualStyleBackColor = true;
            this.cbCurrent.CheckedChanged += new System.EventHandler(this.ChkBox_CheckedChanged);
            // 
            // cbVoltage
            // 
            this.cbVoltage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbVoltage.AutoSize = true;
            this.cbVoltage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbVoltage.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbVoltage.Location = new System.Drawing.Point(130, 15);
            this.cbVoltage.Name = "cbVoltage";
            this.cbVoltage.Size = new System.Drawing.Size(75, 21);
            this.cbVoltage.TabIndex = 44;
            this.cbVoltage.Text = "Voltage";
            this.cbVoltage.UseVisualStyleBackColor = true;
            this.cbVoltage.CheckedChanged += new System.EventHandler(this.ChkBox_CheckedChanged);
            // 
            // cbPower
            // 
            this.cbPower.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbPower.AutoSize = true;
            this.cbPower.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPower.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbPower.Location = new System.Drawing.Point(292, 15);
            this.cbPower.Name = "cbPower";
            this.cbPower.Size = new System.Drawing.Size(66, 21);
            this.cbPower.TabIndex = 43;
            this.cbPower.Text = "Power";
            this.cbPower.UseVisualStyleBackColor = true;
            this.cbPower.CheckedChanged += new System.EventHandler(this.ChkBox_CheckedChanged);
            // 
            // cbSOC
            // 
            this.cbSOC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbSOC.AutoSize = true;
            this.cbSOC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSOC.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbSOC.Location = new System.Drawing.Point(364, 15);
            this.cbSOC.Name = "cbSOC";
            this.cbSOC.Size = new System.Drawing.Size(56, 21);
            this.cbSOC.TabIndex = 42;
            this.cbSOC.Text = "SOC";
            this.cbSOC.UseVisualStyleBackColor = true;
            this.cbSOC.CheckedChanged += new System.EventHandler(this.ChkBox_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(78)))), ((int)(((byte)(74)))));
            this.panel5.Controls.Add(this.reportViewer1);
            this.panel5.Controls.Add(this.chartExportData);
            this.panel5.Location = new System.Drawing.Point(1, 133);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1130, 585);
            this.panel5.TabIndex = 54;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DataSet0";
            reportDataSource1.Value = this.dataTable1BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ENCAPv3.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(25, 555);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1102, 0);
            this.reportViewer1.TabIndex = 1;
            this.reportViewer1.Visible = false;
            // 
            // chartExportData
            // 
            this.chartExportData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(78)))), ((int)(((byte)(74)))));
            this.chartExportData.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartExportData.ForeColor = System.Drawing.Color.White;
            this.chartExportData.Location = new System.Drawing.Point(0, 0);
            this.chartExportData.Name = "chartExportData";
            this.chartExportData.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.chartExportData.Size = new System.Drawing.Size(1130, 518);
            this.chartExportData.TabIndex = 0;
            this.chartExportData.Text = "cartesianChart1";
            // 
            // storePointBindingSource
            // 
            this.storePointBindingSource.DataSource = typeof(BusinessLogic.Model.StorePoint);
            // 
            // ExportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(78)))), ((int)(((byte)(74)))));
            this.ClientSize = new System.Drawing.Size(1134, 750);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ExportData";
            this.Text = "ExportData";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.ExportData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.set1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.storePointBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker datePickerStartDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datePickerEndDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.CheckBox cbCurrent;
        private System.Windows.Forms.CheckBox cbVoltage;
        private System.Windows.Forms.CheckBox cbPower;
        private System.Windows.Forms.CheckBox cbSOC;
        private System.Windows.Forms.Panel panel5;
        private LiveCharts.WinForms.CartesianChart chartExportData;
        private FontAwesome.Sharp.IconButton btnAlarmReport;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource storePointBindingSource;
        private System.Windows.Forms.BindingSource dataTable1BindingSource;
        private Set1 set1;
        private FontAwesome.Sharp.IconButton btnExportCSV;
        private System.Windows.Forms.CheckBox cbTemprature;
    }
}