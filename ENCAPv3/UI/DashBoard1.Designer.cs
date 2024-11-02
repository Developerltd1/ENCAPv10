
namespace EMView.UI
{
    partial class DashBoard1
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnTogglePolling1 = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(60, 146);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(876, 444);
            this.richTextBox1.TabIndex = 21;
            this.richTextBox1.Text = "";
            // 
            // btnTogglePolling1
            // 
            this.btnTogglePolling1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTogglePolling1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(192)))), ((int)(((byte)(139)))));
            this.btnTogglePolling1.FlatAppearance.BorderSize = 0;
            this.btnTogglePolling1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTogglePolling1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTogglePolling1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(60)))));
            this.btnTogglePolling1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnTogglePolling1.IconColor = System.Drawing.Color.White;
            this.btnTogglePolling1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnTogglePolling1.IconSize = 32;
            this.btnTogglePolling1.Location = new System.Drawing.Point(421, 60);
            this.btnTogglePolling1.Margin = new System.Windows.Forms.Padding(2);
            this.btnTogglePolling1.Name = "btnTogglePolling1";
            this.btnTogglePolling1.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnTogglePolling1.Size = new System.Drawing.Size(178, 49);
            this.btnTogglePolling1.TabIndex = 22;
            this.btnTogglePolling1.Text = "Start Reading";
            this.btnTogglePolling1.UseVisualStyleBackColor = false;
            this.btnTogglePolling1.Click += new System.EventHandler(this.btnTogglePolling1_Click);
            // 
            // DashBoard1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(998, 649);
            this.Controls.Add(this.btnTogglePolling1);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DashBoard1";
            this.Text = "DashBoard1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox1;
        private FontAwesome.Sharp.IconButton btnTogglePolling1;
    }
}