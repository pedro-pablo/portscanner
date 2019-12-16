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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbIpAddress = new System.Windows.Forms.Label();
            this.lbPorts = new System.Windows.Forms.Label();
            this.txtPorts = new System.Windows.Forms.TextBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.btnSelectPort = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.lbTimeout = new System.Windows.Forms.Label();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.scanResultGridview = new System.Windows.Forms.DataGridView();
            this.portGridViewColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionGridViewColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openGridViewColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.portInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanResultGridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lbIpAddress
            // 
            this.lbIpAddress.AutoSize = true;
            this.lbIpAddress.Location = new System.Drawing.Point(12, 9);
            this.lbIpAddress.Name = "lbIpAddress";
            this.lbIpAddress.Size = new System.Drawing.Size(60, 13);
            this.lbIpAddress.TabIndex = 0;
            this.lbIpAddress.Text = "IP address:";
            // 
            // lbPorts
            // 
            this.lbPorts.AutoSize = true;
            this.lbPorts.Location = new System.Drawing.Point(12, 61);
            this.lbPorts.Name = "lbPorts";
            this.lbPorts.Size = new System.Drawing.Size(34, 13);
            this.lbPorts.TabIndex = 4;
            this.lbPorts.Text = "Ports:";
            // 
            // txtPorts
            // 
            this.txtPorts.Location = new System.Drawing.Point(112, 58);
            this.txtPorts.Name = "txtPorts";
            this.txtPorts.Size = new System.Drawing.Size(260, 20);
            this.txtPorts.TabIndex = 5;
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(196, 84);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(176, 23);
            this.btnScan.TabIndex = 7;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.BtnScan_ClickAsync);
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(232, 6);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(140, 20);
            this.txtIpAddress.TabIndex = 1;
            // 
            // btnSelectPort
            // 
            this.btnSelectPort.Location = new System.Drawing.Point(12, 84);
            this.btnSelectPort.Name = "btnSelectPort";
            this.btnSelectPort.Size = new System.Drawing.Size(178, 23);
            this.btnSelectPort.TabIndex = 6;
            this.btnSelectPort.Text = "Select port from list";
            this.btnSelectPort.UseVisualStyleBackColor = true;
            this.btnSelectPort.Click += new System.EventHandler(this.BtnSelectPort_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipTitle = "PortScan";
            this.notifyIcon.Text = "PortScan";
            this.notifyIcon.Visible = true;
            // 
            // lbTimeout
            // 
            this.lbTimeout.AutoSize = true;
            this.lbTimeout.Location = new System.Drawing.Point(12, 35);
            this.lbTimeout.Name = "lbTimeout";
            this.lbTimeout.Size = new System.Drawing.Size(70, 13);
            this.lbTimeout.TabIndex = 2;
            this.lbTimeout.Text = "Timeout (ms):";
            // 
            // numTimeout
            // 
            this.numTimeout.Location = new System.Drawing.Point(313, 32);
            this.numTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numTimeout.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(59, 20);
            this.numTimeout.TabIndex = 3;
            this.numTimeout.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // scanResultGridview
            // 
            this.scanResultGridview.AllowUserToAddRows = false;
            this.scanResultGridview.AllowUserToDeleteRows = false;
            this.scanResultGridview.AllowUserToOrderColumns = true;
            this.scanResultGridview.AllowUserToResizeRows = false;
            this.scanResultGridview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scanResultGridview.AutoGenerateColumns = false;
            this.scanResultGridview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.scanResultGridview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.portGridViewColumn,
            this.descriptionGridViewColumn,
            this.openGridViewColumn});
            this.scanResultGridview.DataSource = this.portInfoBindingSource;
            this.scanResultGridview.Location = new System.Drawing.Point(12, 113);
            this.scanResultGridview.Name = "scanResultGridview";
            this.scanResultGridview.ReadOnly = true;
            this.scanResultGridview.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.scanResultGridview.RowHeadersVisible = false;
            this.scanResultGridview.RowTemplate.Height = 20;
            this.scanResultGridview.RowTemplate.ReadOnly = true;
            this.scanResultGridview.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.scanResultGridview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.scanResultGridview.Size = new System.Drawing.Size(360, 336);
            this.scanResultGridview.StandardTab = true;
            this.scanResultGridview.TabIndex = 8;
            this.scanResultGridview.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ScanResultGridview_CellFormatting);
            // 
            // portGridViewColumn
            // 
            this.portGridViewColumn.DataPropertyName = "Port";
            this.portGridViewColumn.HeaderText = "Port";
            this.portGridViewColumn.Name = "portGridViewColumn";
            this.portGridViewColumn.ReadOnly = true;
            this.portGridViewColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.portGridViewColumn.Width = 60;
            // 
            // descriptionGridViewColumn
            // 
            this.descriptionGridViewColumn.DataPropertyName = "Description";
            this.descriptionGridViewColumn.HeaderText = "Description";
            this.descriptionGridViewColumn.Name = "descriptionGridViewColumn";
            this.descriptionGridViewColumn.ReadOnly = true;
            this.descriptionGridViewColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.descriptionGridViewColumn.Width = 220;
            // 
            // openGridViewColumn
            // 
            this.openGridViewColumn.DataPropertyName = "Open";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.openGridViewColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.openGridViewColumn.HeaderText = "Open";
            this.openGridViewColumn.Name = "openGridViewColumn";
            this.openGridViewColumn.ReadOnly = true;
            this.openGridViewColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.openGridViewColumn.Width = 70;
            // 
            // portInfoBindingSource
            // 
            this.portInfoBindingSource.DataSource = typeof(PortScanner.PortInfo);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.scanResultGridview);
            this.Controls.Add(this.numTimeout);
            this.Controls.Add(this.lbTimeout);
            this.Controls.Add(this.btnSelectPort);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.txtPorts);
            this.Controls.Add(this.lbPorts);
            this.Controls.Add(this.lbIpAddress);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 800);
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PortScanner";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanResultGridview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portInfoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbIpAddress;
        private System.Windows.Forms.Label lbPorts;
        private System.Windows.Forms.TextBox txtPorts;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Button btnSelectPort;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label lbTimeout;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.DataGridView scanResultGridview;
        private System.Windows.Forms.BindingSource portInfoBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn portGridViewColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionGridViewColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn openGridViewColumn;
    }
}

