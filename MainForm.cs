using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using PortScanner.Properties;

namespace PortScanner
{
    /// <summary>
    ///     Main form of the PortScan.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly PortListForm _portListForm;
        private IPAddress _targetIpAddress;

        public MainForm()
        {
            InitializeComponent();

            PortInfo.InitializePortDictionary();
            Icon = Resources.PortScannerIcon;
            notifyIcon.Icon = Resources.PortScannerIcon;
            _portListForm = new PortListForm();

            lbIpAddress.Text = Labels.IpAddress;
            lbPorts.Text = Labels.Ports;
            lbTimeout.Text = Labels.Timeout;
            btnSelectPort.Text = Labels.SelectPortButton;
            btnScan.Text = Labels.Scan;
            portGridViewColumn.HeaderText = Labels.PortColumn;
            descriptionGridViewColumn.HeaderText = Labels.DescriptionColumn;
            openGridViewColumn.HeaderText = Labels.OpenColumn;
        }

        private enum MessageType
        {
            Error,
            Information,
            Warning
        }

        #region Events

        private async void BtnScan_ClickAsync(object sender, EventArgs e)
        {
            LockControls();
            ClearErrors();
            portInfoBindingSource.Clear();

            if (!string.IsNullOrEmpty(txtIpAddress.Text))
            {
                if (!IPAddress.TryParse(txtIpAddress.Text, out IPAddress inputIp))
                {
                    SetError(txtIpAddress, Messages.InvalidIpAddress);
                    UnlockControls();
                    return;
                }

                _targetIpAddress = inputIp;
            }
            else
            {
                SetError(txtIpAddress, Messages.RequiredIpAddress);
                UnlockControls();
                return;
            }

            if (!string.IsNullOrEmpty(txtPorts.Text))
            {
                if (txtPorts.Text.Any(c => !char.IsDigit(c) && c != ';' && c != '-'))
                {
                    SetError(txtPorts, Messages.InvalidPortInput);
                    UnlockControls();
                    return;
                }

                string[] portsStr = txtPorts.Text.Split(';');

                for (int i = 0; i < portsStr.Length; i++)
                    if (portsStr[i].Contains('-'))
                    {
                        if (portsStr[i].Count(c => c == '-') > 1)
                        {
                            SetError(txtPorts, string.Format(Messages.InvalidPortRange, portsStr[i]));
                            UnlockControls();
                            return;
                        }

                        string[] portIntervalStr = portsStr[i].Split('-');

                        ushort portBegin, portEnd;
                        try
                        {
                            portBegin = Convert.ToUInt16(portIntervalStr[0]);
                        }
                        catch
                        {
                            SetError(txtPorts,
                                string.Format(Messages.InvalidPortInRange, portIntervalStr[0], portsStr[i]));
                            UnlockControls();
                            return;
                        }

                        try
                        {
                            portEnd = Convert.ToUInt16(portIntervalStr[1]);
                        }
                        catch
                        {
                            SetError(txtPorts,
                                string.Format(Messages.InvalidPortInRange, portIntervalStr[1], portsStr[i]));
                            UnlockControls();
                            return;
                        }

                        for (ushort portNumber = portBegin; portNumber <= portEnd; portNumber++) BindPort(portNumber);
                    }
                    else
                    {
                        if (ushort.TryParse(portsStr[i], out ushort portNumber))
                        {
                            BindPort(portNumber);
                        }
                        else
                        {
                            SetError(txtPorts, string.Format(Messages.InvalidPort, portsStr[i]));
                            UnlockControls();
                            return;
                        }
                    }
            }
            else
            {
                SetError(txtPorts, Messages.NoPortsSpecified);
                UnlockControls();
                return;
            }

            ushort timeout = (ushort) numTimeout.Value;
            List<Task> scanningTasks = new List<Task>(portInfoBindingSource.Count);
            foreach (PortInfo port in portInfoBindingSource)
                scanningTasks.Add(PortScan.ScanPortAsync(_targetIpAddress, port, timeout));

            bool errors = false;
            try
            {
                await Task.WhenAll(scanningTasks);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, Messages.ScanErrorTitle, MessageType.Error);
                errors = true;
            }

            portInfoBindingSource.ResetBindings(false);
            if (!errors)
                ShowMessage(Messages.ScanCompleteSuccessful, Messages.ScanResultsTitle, MessageType.Information);
            else
                ShowMessage(Messages.ScanAborted, Messages.ScanResultsTitle, MessageType.Warning);

            UnlockControls();
        }

        private void BtnSelectPort_Click(object sender, EventArgs e)
        {
            if (_portListForm.ShowDialog(this) == DialogResult.OK)
            {
                string addedPorts = string.Empty;

                if (!string.IsNullOrEmpty(txtPorts.Text) && !txtPorts.Text.EndsWith(";")) addedPorts += ';';

                int itemCount = _portListForm.SelectedPorts.Count;
                for (int i = 0; i < itemCount; i++)
                {
                    addedPorts += _portListForm.SelectedPorts[i].ToString();
                    if (i < itemCount - 1) addedPorts += ';';
                }

                txtPorts.Text += addedPorts;
            }
        }

        private void ScanResultGridview_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            PortInfo portRow = (PortInfo) portInfoBindingSource[e.RowIndex];
            if (portRow != null)
                switch (portRow.Open)
                {
                    case true:
                        e.CellStyle.BackColor = Color.LightGreen;
                        break;
                    case false:
                        e.CellStyle.BackColor = Color.LightCoral;
                        break;
                }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sets the error message for the specified control and resizes it to fit the error icon.
        /// </summary>
        /// <param name="control">Control related to the error.</param>
        /// <param name="errorMessage">Error message.</param>
        private void SetError(Control control, string errorMessage)
        {
            errorProvider.SetError(control, errorMessage);
            control.Width -= errorProvider.Icon.Width;
        }

        /// <summary>
        ///     Disables controls that interfere with the port scanning.
        /// </summary>
        private void LockControls()
        {
            txtIpAddress.Enabled = false;
            txtPorts.Enabled = false;
            btnSelectPort.Enabled = false;
            btnScan.Enabled = false;
            numTimeout.Enabled = false;
            scanResultGridview.Enabled = false;
        }

        /// <summary>
        ///     Enables controls that interfere with the port scanning.
        /// </summary>
        private void UnlockControls()
        {
            txtIpAddress.Enabled = true;
            txtPorts.Enabled = true;
            btnSelectPort.Enabled = true;
            btnScan.Enabled = true;
            numTimeout.Enabled = true;
            scanResultGridview.Enabled = true;
        }

        /// <summary>
        ///     Clear the errors for all controls in this form and resets their original width.
        /// </summary>
        private void ClearErrors()
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                Control control = Controls[i];
                if (!string.IsNullOrEmpty(errorProvider.GetError(control))) control.Width += errorProvider.Icon.Width;
            }

            errorProvider.Clear();
        }

        /// <summary>
        ///     Shows a message with the specified text, title and type.
        ///     The message may be exhibited in a message box or a tooltip notification,
        ///     depending on the state of the main form (if it is minimized or not).
        /// </summary>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <param name="type"></param>
        private void ShowMessage(string text, string title, MessageType type)
        {
            switch (WindowState)
            {
                case FormWindowState.Maximized:
                case FormWindowState.Normal:
                    MessageBoxIcon messageBoxIcon;

                    switch (type)
                    {
                        case MessageType.Error:
                            messageBoxIcon = MessageBoxIcon.Error;
                            break;
                        case MessageType.Information:
                            messageBoxIcon = MessageBoxIcon.Information;
                            break;
                        case MessageType.Warning:
                            messageBoxIcon = MessageBoxIcon.Exclamation;
                            break;
                        default:
                            throw new ArgumentException(Messages.InvalidMessageType, nameof(type));
                    }

                    MessageBox.Show(text, title, MessageBoxButtons.OK, messageBoxIcon);
                    break;

                case FormWindowState.Minimized:
                    ToolTipIcon toolTipIcon;

                    switch (type)
                    {
                        case MessageType.Error:
                            toolTipIcon = ToolTipIcon.Error;
                            break;
                        case MessageType.Information:
                            toolTipIcon = ToolTipIcon.Info;
                            break;
                        case MessageType.Warning:
                            toolTipIcon = ToolTipIcon.Warning;
                            break;
                        default:
                            throw new ArgumentException(Messages.InvalidMessageType, nameof(type));
                    }

                    notifyIcon.ShowBalloonTip(5, title, text, toolTipIcon);
                    break;
            }
        }

        /// <summary>
        ///     Creates a PortInfo object for a given port number and binds it to the ports binding source.
        /// </summary>
        /// <param name="portNumber">Port number.</param>
        private void BindPort(ushort portNumber)
        {
            PortInfo port = new PortInfo(portNumber);
            if (!portInfoBindingSource.Contains(port)) portInfoBindingSource.Add(port);
        }

        #endregion
    }
}