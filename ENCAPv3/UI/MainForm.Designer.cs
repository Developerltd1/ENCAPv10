
namespace ENCAPv3.UI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelIsGuest = new System.Windows.Forms.Label();
            this.iconButtonIsGuest = new FontAwesome.Sharp.IconButton();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHam = new System.Windows.Forms.PictureBox();
            this.sidebarPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.pnBtnMenu = new FontAwesome.Sharp.IconButton();
            this.pnBtnDashboard = new FontAwesome.Sharp.IconButton();
            this.pnBtnSetting = new FontAwesome.Sharp.IconButton();
            this.pnBtnDataExport = new FontAwesome.Sharp.IconButton();
            this.pnBtnAboutUs = new FontAwesome.Sharp.IconButton();
            this.SidebarTransaction = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHam)).BeginInit();
            this.sidebarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.labelIsGuest);
            this.panel1.Controls.Add(this.iconButtonIsGuest);
            this.panel1.Controls.Add(this.labelDate);
            this.panel1.Controls.Add(this.labelTime);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnHam);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1300, 40);
            this.panel1.TabIndex = 0;
            // 
            // labelIsGuest
            // 
            this.labelIsGuest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelIsGuest.AutoSize = true;
            this.labelIsGuest.BackColor = System.Drawing.Color.Transparent;
            this.labelIsGuest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelIsGuest.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIsGuest.ForeColor = System.Drawing.Color.Black;
            this.labelIsGuest.Location = new System.Drawing.Point(972, 9);
            this.labelIsGuest.Name = "labelIsGuest";
            this.labelIsGuest.Size = new System.Drawing.Size(47, 20);
            this.labelIsGuest.TabIndex = 17;
            this.labelIsGuest.Text = "Guest";
            // 
            // iconButtonIsGuest
            // 
            this.iconButtonIsGuest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconButtonIsGuest.BackColor = System.Drawing.Color.Transparent;
            this.iconButtonIsGuest.FlatAppearance.BorderSize = 0;
            this.iconButtonIsGuest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonIsGuest.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonIsGuest.ForeColor = System.Drawing.Color.White;
            this.iconButtonIsGuest.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.iconButtonIsGuest.IconColor = System.Drawing.Color.Black;
            this.iconButtonIsGuest.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonIsGuest.IconSize = 38;
            this.iconButtonIsGuest.Location = new System.Drawing.Point(1029, 5);
            this.iconButtonIsGuest.Name = "iconButtonIsGuest";
            this.iconButtonIsGuest.Size = new System.Drawing.Size(38, 32);
            this.iconButtonIsGuest.TabIndex = 16;
            this.iconButtonIsGuest.Text = "Guest";
            this.iconButtonIsGuest.UseVisualStyleBackColor = false;
            this.iconButtonIsGuest.Click += new System.EventHandler(this.iconButtonIsGuest_Click);
            // 
            // labelDate
            // 
            this.labelDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDate.AutoSize = true;
            this.labelDate.BackColor = System.Drawing.Color.Transparent;
            this.labelDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelDate.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDate.ForeColor = System.Drawing.Color.Black;
            this.labelDate.Location = new System.Drawing.Point(1083, 9);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(95, 20);
            this.labelDate.TabIndex = 15;
            this.labelDate.Text = "29/01/2024";
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.AutoSize = true;
            this.labelTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelTime.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.Black;
            this.labelTime.Location = new System.Drawing.Point(1184, 9);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(100, 20);
            this.labelTime.TabIndex = 14;
            this.labelTime.Text = "10:56:12 AM";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(131, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "EMView 1.0";
            // 
            // btnHam
            // 
            this.btnHam.Image = ((System.Drawing.Image)(resources.GetObject("btnHam.Image")));
            this.btnHam.Location = new System.Drawing.Point(0, 0);
            this.btnHam.Margin = new System.Windows.Forms.Padding(2);
            this.btnHam.Name = "btnHam";
            this.btnHam.Size = new System.Drawing.Size(127, 39);
            this.btnHam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnHam.TabIndex = 1;
            this.btnHam.TabStop = false;
            this.btnHam.Click += new System.EventHandler(this.btnHam_Click);
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(62)))), ((int)(((byte)(57)))));
            this.sidebarPanel.Controls.Add(this.pnBtnMenu);
            this.sidebarPanel.Controls.Add(this.pnBtnDashboard);
            this.sidebarPanel.Controls.Add(this.pnBtnSetting);
            this.sidebarPanel.Controls.Add(this.pnBtnDataExport);
            this.sidebarPanel.Controls.Add(this.pnBtnAboutUs);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 40);
            this.sidebarPanel.Margin = new System.Windows.Forms.Padding(2);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(166, 660);
            this.sidebarPanel.TabIndex = 1;
            // 
            // pnBtnMenu
            // 
            this.pnBtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.pnBtnMenu.FlatAppearance.BorderSize = 0;
            this.pnBtnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pnBtnMenu.ForeColor = System.Drawing.Color.White;
            this.pnBtnMenu.IconChar = FontAwesome.Sharp.IconChar.Dashboard;
            this.pnBtnMenu.IconColor = System.Drawing.Color.White;
            this.pnBtnMenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.pnBtnMenu.IconSize = 32;
            this.pnBtnMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pnBtnMenu.Location = new System.Drawing.Point(2, 2);
            this.pnBtnMenu.Margin = new System.Windows.Forms.Padding(2);
            this.pnBtnMenu.Name = "pnBtnMenu";
            this.pnBtnMenu.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.pnBtnMenu.Size = new System.Drawing.Size(179, 48);
            this.pnBtnMenu.TabIndex = 10;
            this.pnBtnMenu.Text = "Dashboard";
            this.pnBtnMenu.UseVisualStyleBackColor = false;
            this.pnBtnMenu.Click += new System.EventHandler(this.pnBtnMenu_Click);
            // 
            // pnBtnDashboard
            // 
            this.pnBtnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.pnBtnDashboard.FlatAppearance.BorderSize = 0;
            this.pnBtnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pnBtnDashboard.ForeColor = System.Drawing.Color.White;
            this.pnBtnDashboard.IconChar = FontAwesome.Sharp.IconChar.Usps;
            this.pnBtnDashboard.IconColor = System.Drawing.Color.White;
            this.pnBtnDashboard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.pnBtnDashboard.IconSize = 32;
            this.pnBtnDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pnBtnDashboard.Location = new System.Drawing.Point(2, 54);
            this.pnBtnDashboard.Margin = new System.Windows.Forms.Padding(2);
            this.pnBtnDashboard.Name = "pnBtnDashboard";
            this.pnBtnDashboard.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.pnBtnDashboard.Size = new System.Drawing.Size(179, 48);
            this.pnBtnDashboard.TabIndex = 11;
            this.pnBtnDashboard.Text = "Data";
            this.pnBtnDashboard.UseVisualStyleBackColor = false;
            this.pnBtnDashboard.Click += new System.EventHandler(this.pnBtnDashboard_Click);
            // 
            // pnBtnSetting
            // 
            this.pnBtnSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.pnBtnSetting.FlatAppearance.BorderSize = 0;
            this.pnBtnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pnBtnSetting.ForeColor = System.Drawing.Color.White;
            this.pnBtnSetting.IconChar = FontAwesome.Sharp.IconChar.SquareVirus;
            this.pnBtnSetting.IconColor = System.Drawing.Color.White;
            this.pnBtnSetting.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.pnBtnSetting.IconSize = 32;
            this.pnBtnSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pnBtnSetting.Location = new System.Drawing.Point(2, 106);
            this.pnBtnSetting.Margin = new System.Windows.Forms.Padding(2);
            this.pnBtnSetting.Name = "pnBtnSetting";
            this.pnBtnSetting.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.pnBtnSetting.Size = new System.Drawing.Size(179, 48);
            this.pnBtnSetting.TabIndex = 11;
            this.pnBtnSetting.Text = "Settings";
            this.pnBtnSetting.UseVisualStyleBackColor = false;
            this.pnBtnSetting.Click += new System.EventHandler(this.pnBtnSetting_Click);
            // 
            // pnBtnDataExport
            // 
            this.pnBtnDataExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.pnBtnDataExport.FlatAppearance.BorderSize = 0;
            this.pnBtnDataExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pnBtnDataExport.ForeColor = System.Drawing.Color.White;
            this.pnBtnDataExport.IconChar = FontAwesome.Sharp.IconChar.WindowMaximize;
            this.pnBtnDataExport.IconColor = System.Drawing.Color.White;
            this.pnBtnDataExport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.pnBtnDataExport.IconSize = 32;
            this.pnBtnDataExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pnBtnDataExport.Location = new System.Drawing.Point(2, 158);
            this.pnBtnDataExport.Margin = new System.Windows.Forms.Padding(2);
            this.pnBtnDataExport.Name = "pnBtnDataExport";
            this.pnBtnDataExport.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.pnBtnDataExport.Size = new System.Drawing.Size(179, 48);
            this.pnBtnDataExport.TabIndex = 15;
            this.pnBtnDataExport.Text = "Data Export";
            this.pnBtnDataExport.UseVisualStyleBackColor = false;
            this.pnBtnDataExport.Click += new System.EventHandler(this.pnBtnDataExport_Click);
            // 
            // pnBtnAboutUs
            // 
            this.pnBtnAboutUs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(57)))));
            this.pnBtnAboutUs.FlatAppearance.BorderSize = 0;
            this.pnBtnAboutUs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pnBtnAboutUs.ForeColor = System.Drawing.Color.White;
            this.pnBtnAboutUs.IconChar = FontAwesome.Sharp.IconChar.Uncharted;
            this.pnBtnAboutUs.IconColor = System.Drawing.Color.White;
            this.pnBtnAboutUs.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.pnBtnAboutUs.IconSize = 32;
            this.pnBtnAboutUs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pnBtnAboutUs.Location = new System.Drawing.Point(2, 210);
            this.pnBtnAboutUs.Margin = new System.Windows.Forms.Padding(2);
            this.pnBtnAboutUs.Name = "pnBtnAboutUs";
            this.pnBtnAboutUs.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.pnBtnAboutUs.Size = new System.Drawing.Size(179, 48);
            this.pnBtnAboutUs.TabIndex = 12;
            this.pnBtnAboutUs.Text = "About Us";
            this.pnBtnAboutUs.UseVisualStyleBackColor = false;
            this.pnBtnAboutUs.Visible = false;
            this.pnBtnAboutUs.Click += new System.EventHandler(this.pnBtnAboutUs_Click);
            // 
            // SidebarTransaction
            // 
            this.SidebarTransaction.Interval = 10;
            this.SidebarTransaction.Tick += new System.EventHandler(this.SidebarTransaction_Tick);
            // 
            // MainForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.sidebarPanel);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(60)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHam)).EndInit();
            this.sidebarPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnHam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel sidebarPanel;
        private FontAwesome.Sharp.IconButton pnBtnMenu;
        private FontAwesome.Sharp.IconButton pnBtnSetting;
        private FontAwesome.Sharp.IconButton pnBtnAboutUs;
        private System.Windows.Forms.Timer SidebarTransaction;
        private FontAwesome.Sharp.IconButton pnBtnDashboard;
        private MetroSet_UI.Controls.MetroSetControlBox metroSetControlBox1;
        private FontAwesome.Sharp.IconButton pnBtnDataExport;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelTime;
        private FontAwesome.Sharp.IconButton iconButtonIsGuest;
        public System.Windows.Forms.Label labelIsGuest;
    }
}