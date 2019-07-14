namespace PortScanner
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
            this.lb = new System.Windows.Forms.Label();
            this.lbRange = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.btnSelectPort = new System.Windows.Forms.Button();
            this.listBoxPorts = new System.Windows.Forms.ListBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lb
            // 
            this.lb.AutoSize = true;
            this.lb.Location = new System.Drawing.Point(12, 9);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(60, 13);
            this.lb.TabIndex = 0;
            this.lb.Text = "IP address:";
            // 
            // lbRange
            // 
            this.lbRange.AutoSize = true;
            this.lbRange.Location = new System.Drawing.Point(12, 37);
            this.lbRange.Name = "lbRange";
            this.lbRange.Size = new System.Drawing.Size(34, 13);
            this.lbRange.TabIndex = 2;
            this.lbRange.Text = "Ports:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(77, 34);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(245, 20);
            this.txtPort.TabIndex = 3;
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(12, 87);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(310, 23);
            this.btnScan.TabIndex = 5;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.BtnScan_ClickAsync);
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(77, 6);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(245, 20);
            this.txtIpAddress.TabIndex = 1;
            // 
            // btnSelectPort
            // 
            this.btnSelectPort.Location = new System.Drawing.Point(12, 60);
            this.btnSelectPort.Name = "btnSelectPort";
            this.btnSelectPort.Size = new System.Drawing.Size(310, 23);
            this.btnSelectPort.TabIndex = 4;
            this.btnSelectPort.Text = "Select port from list";
            this.btnSelectPort.UseVisualStyleBackColor = true;
            this.btnSelectPort.Click += new System.EventHandler(this.BtnSelectPort_Click);
            // 
            // listBoxPorts
            // 
            this.listBoxPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.listBoxPorts.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPorts.FormattingEnabled = true;
            this.listBoxPorts.IntegralHeight = false;
            this.listBoxPorts.Location = new System.Drawing.Point(12, 116);
            this.listBoxPorts.MaximumSize = new System.Drawing.Size(310, 635);
            this.listBoxPorts.Name = "listBoxPorts";
            this.listBoxPorts.ScrollAlwaysVisible = true;
            this.listBoxPorts.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxPorts.Size = new System.Drawing.Size(310, 185);
            this.listBoxPorts.TabIndex = 6;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipTitle = "PortScanner";
            this.notifyIcon.Text = "PortScanner";
            this.notifyIcon.Visible = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 311);
            this.Controls.Add(this.btnSelectPort);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.listBoxPorts);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lbRange);
            this.Controls.Add(this.lb);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(350, 800);
            this.MinimumSize = new System.Drawing.Size(350, 250);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PortScanner";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.Label lbRange;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Button btnSelectPort;
        private System.Windows.Forms.ListBox listBoxPorts;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

