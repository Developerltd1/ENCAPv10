
namespace ENCAPv3.UI
{
    partial class ExportChart
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
            this.label2 = new System.Windows.Forms.Label();
            this.datePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.datePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.cbTemprature = new System.Windows.Forms.CheckBox();
            this.cbCurrent = new System.Windows.Forms.CheckBox();
            this.cbVoltage = new System.Windows.Forms.CheckBox();
            this.cbPowerFactor = new System.Windows.Forms.CheckBox();
            this.cbSOC = new System.Windows.Forms.CheckBox();
            this.cbActivePower = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(60)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.datePickerEndDate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.datePickerStartDate);
            this.panel1.Location = new System.Drawing.Point(2, 6);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1132, 74);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(561, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 24);
            this.label2.TabIndex = 38;
            this.label2.Text = "End Time";
            // 
            // datePickerEndDate
            // 
            this.datePickerEndDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.datePickerEndDate.CustomFormat = "ss:mm:hh tt dd/MM/yyyy";
            this.datePickerEndDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerEndDate.Location = new System.Drawing.Point(686, 21);
            this.datePickerEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.datePickerEndDate.Name = "datePickerEndDate";
            this.datePickerEndDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.datePickerEndDate.Size = new System.Drawing.Size(277, 29);
            this.datePickerEndDate.TabIndex = 37;
            this.datePickerEndDate.Value = new System.DateTime(2024, 7, 14, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(105, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 24);
            this.label1.TabIndex = 36;
            this.label1.Text = "Start Time";
            // 
            // datePickerStartDate
            // 
            this.datePickerStartDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.datePickerStartDate.CustomFormat = "ss:mm:hh tt dd/MM/yyyy";
            this.datePickerStartDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerStartDate.Location = new System.Drawing.Point(235, 19);
            this.datePickerStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.datePickerStartDate.Name = "datePickerStartDate";
            this.datePickerStartDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.datePickerStartDate.Size = new System.Drawing.Size(277, 29);
            this.datePickerStartDate.TabIndex = 35;
            this.datePickerStartDate.Value = new System.DateTime(2024, 7, 14, 0, 0, 0, 0);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(60)))));
            this.panel2.Controls.Add(this.cbSelectAll);
            this.panel2.Controls.Add(this.cbTemprature);
            this.panel2.Controls.Add(this.cbCurrent);
            this.panel2.Controls.Add(this.cbVoltage);
            this.panel2.Controls.Add(this.cbPowerFactor);
            this.panel2.Controls.Add(this.cbSOC);
            this.panel2.Controls.Add(this.cbActivePower);
            this.panel2.Location = new System.Drawing.Point(0, 82);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1133, 57);
            this.panel2.TabIndex = 40;
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelectAll.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.cbSelectAll.Location = new System.Drawing.Point(30, 18);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(85, 21);
            this.cbSelectAll.TabIndex = 47;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            // 
            // cbTemprature
            // 
            this.cbTemprature.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbTemprature.AutoSize = true;
            this.cbTemprature.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTemprature.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbTemprature.Location = new System.Drawing.Point(1004, 18);
            this.cbTemprature.Name = "cbTemprature";
            this.cbTemprature.Size = new System.Drawing.Size(101, 21);
            this.cbTemprature.TabIndex = 46;
            this.cbTemprature.Text = "Temprature";
            this.cbTemprature.UseVisualStyleBackColor = true;
            // 
            // cbCurrent
            // 
            this.cbCurrent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbCurrent.AutoSize = true;
            this.cbCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCurrent.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbCurrent.Location = new System.Drawing.Point(889, 18);
            this.cbCurrent.Name = "cbCurrent";
            this.cbCurrent.Size = new System.Drawing.Size(74, 21);
            this.cbCurrent.TabIndex = 45;
            this.cbCurrent.Text = "Current";
            this.cbCurrent.UseVisualStyleBackColor = true;
            // 
            // cbVoltage
            // 
            this.cbVoltage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbVoltage.AutoSize = true;
            this.cbVoltage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbVoltage.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbVoltage.Location = new System.Drawing.Point(766, 18);
            this.cbVoltage.Name = "cbVoltage";
            this.cbVoltage.Size = new System.Drawing.Size(75, 21);
            this.cbVoltage.TabIndex = 44;
            this.cbVoltage.Text = "Voltage";
            this.cbVoltage.UseVisualStyleBackColor = true;
            // 
            // cbPowerFactor
            // 
            this.cbPowerFactor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbPowerFactor.AutoSize = true;
            this.cbPowerFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPowerFactor.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbPowerFactor.Location = new System.Drawing.Point(613, 18);
            this.cbPowerFactor.Name = "cbPowerFactor";
            this.cbPowerFactor.Size = new System.Drawing.Size(110, 21);
            this.cbPowerFactor.TabIndex = 43;
            this.cbPowerFactor.Text = "Power Factor";
            this.cbPowerFactor.UseVisualStyleBackColor = true;
            // 
            // cbSOC
            // 
            this.cbSOC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbSOC.AutoSize = true;
            this.cbSOC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSOC.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbSOC.Location = new System.Drawing.Point(505, 18);
            this.cbSOC.Name = "cbSOC";
            this.cbSOC.Size = new System.Drawing.Size(56, 21);
            this.cbSOC.TabIndex = 42;
            this.cbSOC.Text = "SOC";
            this.cbSOC.UseVisualStyleBackColor = true;
            // 
            // cbActivePower
            // 
            this.cbActivePower.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbActivePower.AutoSize = true;
            this.cbActivePower.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbActivePower.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbActivePower.Location = new System.Drawing.Point(226, 18);
            this.cbActivePower.Name = "cbActivePower";
            this.cbActivePower.Size = new System.Drawing.Size(215, 21);
            this.cbActivePower.TabIndex = 41;
            this.cbActivePower.Text = "Total Remaining Capacity(Ah)";
            this.cbActivePower.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(60)))));
            this.groupBox1.Controls.Add(this.reportViewer1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(2, 146);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox1.Size = new System.Drawing.Size(1131, 512);
            this.groupBox1.TabIndex = 65;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Report";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(3, 247);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(494, 262);
            this.reportViewer1.TabIndex = 0;
            // 
            // ExportChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 660);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ExportChart";
            this.Text = "ExportChart";
            this.Load += new System.EventHandler(this.ExportChart_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datePickerEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker datePickerStartDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.CheckBox cbTemprature;
        private System.Windows.Forms.CheckBox cbCurrent;
        private System.Windows.Forms.CheckBox cbVoltage;
        private System.Windows.Forms.CheckBox cbPowerFactor;
        private System.Windows.Forms.CheckBox cbSOC;
        private System.Windows.Forms.CheckBox cbActivePower;
        private System.Windows.Forms.GroupBox groupBox1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}