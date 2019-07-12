using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace PortScanner
{
    /// <summary>
    /// Main form of the PortScanner.
    /// </summary>
    public partial class MainForm : Form
    {
        List<PortInfo> ports;
        readonly BindingList<PortInfo> portsBindingList;
        readonly PortListForm portListForm;
        IPAddress targetIpAddress;

        public MainForm()
        {
            InitializeComponent();

            PortInfo.InitializePortDictionary();
            Icon = Properties.Resources.PortScannerIcon;
            notifyIcon.Icon = Properties.Resources.PortScannerIcon;
            ports = new List<PortInfo>();
            portsBindingList = new BindingList<PortInfo>();
            portListForm = new PortListForm();
            listBoxPorts.DataSource = portsBindingList;
        }

        #region Events

        private async void BtnScan_ClickAsync(object sender, EventArgs e)
        {
            ports.Clear();
            LockControls();
            ClearErrors();

            if (!String.IsNullOrEmpty(txtIpAddress.Text))
            {
                if (!IPAddress.TryParse(txtIpAddress.Text, out IPAddress inputIp))
                {
                    SetError(txtIpAddress, "Not a valid IP address.");
                    UnlockControls();
                    return;
                }
                else
                {
                    targetIpAddress = inputIp;
                }
            }
            else
            {
                SetError(txtIpAddress, "IP address is required.");
                UnlockControls();
                return;
            }

            if (!String.IsNullOrEmpty(txtPort.Text))
            {
                if (txtPort.Text.Any(c => !char.IsDigit(c) && c != ';' && c != '-'))
                {
                    SetError(txtPort, "Invalid value.");
                    UnlockControls();
                    return;
                }

                string[] portsStr = txtPort.Text.Split(';');
                portsBindingList.Clear();

                for (int i = 0; i < portsStr.Length; i++)
                {
                    if (portsStr[i].Contains('-'))
                    {
                        if (portsStr[i].Count(c => c == '-') > 1)
                        {
                            SetError(txtPort, $"Invalid port range format in {portsStr[i]}.");
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
                            SetError(txtPort, $"Port {portIntervalStr[0]} in {portsStr[i]} is invalid.");
                            UnlockControls();
                            return;
                        }

                        try
                        {
                            portEnd = Convert.ToUInt16(portIntervalStr[1]);
                        }
                        catch
                        {
                            SetError(txtPort, $"Port {portIntervalStr[1]} in {portsStr[i]} is invalid.");
                            UnlockControls();
                            return;
                        }

                        for (ushort port = portBegin; port <= portEnd; port++)
                        {
                            AddPort(port);
                        }
                    }
                    else
                    {
                        try
                        {
                            AddPortStr(portsStr[i]);
                        }
                        catch (FormatException ex)
                        {
                            SetError(txtPort, ex.Message);
                            UnlockControls();
                            return;
                        }
                    }
                }
            }
            else
            {
                SetError(txtPort, "Must specify one or more ports to scan.");
                UnlockControls();
                return;
            }

            ports = ports.OrderBy(p => p.Port).ToList();
            BindPortsToList();
            await PortScanner.Scan(targetIpAddress, ports);
            ShowMessage("The port scan has been finished.", "Scan results", MessageType.Information);
            UnlockControls();
        }

        private void BtnSelectPort_Click(object sender, EventArgs e)
        {
            if (portListForm.ShowDialog(this) == DialogResult.OK)
            {
                string port = portListForm.SelectedPort.ToString();
                if (!String.IsNullOrEmpty(txtPort.Text))
                {
                    if (txtPort.Text.EndsWith(";"))
                    {
                        txtPort.Text += port;
                    }
                    else
                    {
                        txtPort.Text += ";" + port;
                    }
                }
                else
                {
                    txtPort.Text = port;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to add a new port to the ports list.
        /// </summary>
        /// <param name="portNumberStr">String representation of a port number.</param>
        /// <exception cref="FormatException"></exception>
        private void AddPortStr(string portNumberStr)
        {
            ushort portNumber;
            try
            {
                portNumber = Convert.ToUInt16(portNumberStr);
            }
            catch (Exception ex)
            {
                throw new FormatException($"{portNumberStr} is not a valid port.", ex);
            }

            AddPort(portNumber);
        }

        /// <summary>
        /// Creates a new PortInfo with the given port number and adds it to the ports list.
        /// </summary>
        /// <param name="portNumber">Number of the port.</param>
        private void AddPort(ushort portNumber)
        {
            PortInfo newPortInfo = new PortInfo(portNumber);
            if (!ports.Contains(newPortInfo))
            {
                ports.Add(newPortInfo);
            }
        }

        /// <summary>
        /// Sets the error message for the specified control and resizes it to fit the error icon.
        /// </summary>
        /// <param name="control">Control related to the error.</param>
        /// <param name="errorMessage">Error message.</param>
        private void SetError(Control control, String errorMessage)
        {
            errorProvider.SetError(control, errorMessage);
            control.Width -= errorProvider.Icon.Width;
        }

        /// <summary>
        /// Disables controls that interfere with the port scanning.
        /// </summary>
        private void LockControls()
        {
            txtIpAddress.Enabled = false;
            txtPort.Enabled = false;
            btnSelectPort.Enabled = false;
            btnScan.Enabled = false;
        }

        /// <summary>
        /// Enables controls that interfere with the port scanning.
        /// </summary>
        private void UnlockControls()
        {
            txtIpAddress.Enabled = true;
            txtPort.Enabled = true;
            btnSelectPort.Enabled = true;
            btnScan.Enabled = true;
        }

        /// <summary>
        /// Clear the errors for all controls in this form and resets their original width.
        /// </summary>
        private void ClearErrors()
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                Control control = Controls[i];
                if (!String.IsNullOrEmpty(errorProvider.GetError(control)))
                {
                    control.Width += errorProvider.Icon.Width;
                }
            }
            errorProvider.Clear();
        }

        /// <summary>
        /// Shows a message with the specified text, title and type.
        /// The message may be exhibited in a message box or a tooltip notification,
        /// depending on the state of the main form (if it is minimized or not).
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
                            throw new ArgumentException("Invalid message type.", nameof(type));
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
                            throw new ArgumentException("Invalid message type.", nameof(type));
                    }

                    notifyIcon.ShowBalloonTip(5, title, text, toolTipIcon);
                    break;
            }
        }

        /// <summary>
        /// Adds the ports in the ports list to the binding list.
        /// </summary>
        private void BindPortsToList()
        {
            portsBindingList.Clear();
            for (int i = 0; i < ports.Count; i++)
            {
                portsBindingList.Add(ports[i]);
            }
            portsBindingList.ResetBindings();
        }

        #endregion

        enum MessageType
        {
            Error,
            Information,
            Warning
        }

    }
}
