using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortScanner
{
    public partial class MainForm : Form
    {
        PortScanner portScanner;
        IPAddress targetIpAddress;
        BindingList<PortInfo> portsBindingList;
        PortListForm portListForm;

        public MainForm()
        {
            InitializeComponent();

            PortInfo.InitializePortDictionary();
            portScanner = new PortScanner();
            portListForm = new PortListForm();
            portsBindingList = new BindingList<PortInfo>();
            listBoxPorts.DataSource = portsBindingList;
        }

        private async void BtnScan_ClickAsync(object sender, EventArgs e)
        {
            LockControls();
            ClearErrors();

            if (!String.IsNullOrEmpty(txtIpAddress.Text))
            {
                portScanner.GetIpAddress(txtIpAddress.Text, out IPAddress inputIp);

                if (inputIp == null)
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
                        AddPortStr(portsStr[i]);
                    }
                }
            }
            else
            {
                SetError(txtPort, "Must specify one or more ports to scan.");
                UnlockControls();
                return;
            }

            await portScanner.Scan(targetIpAddress, portsBindingList);
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

        private void AddPortStr(string portNumberStr)
        {
            ushort portNumber;
            try
            {
                portNumber = Convert.ToUInt16(portNumberStr);
            }
            catch
            {
                SetError(txtPort, $"{portNumberStr} is not a valid port.");
                UnlockControls();
                return;
            }

            AddPort(portNumber);
        }

        private void AddPort(ushort portNumber)
        {
            PortInfo newPortInfo = new PortInfo(portNumber);
            if (portsBindingList.Contains(newPortInfo))
            {
                SetError(txtPort, $"Port {portNumber} is repeated.");
                UnlockControls();
                return;
            }
            else
            {
                portsBindingList.Add(new PortInfo(portNumber));
            }
        }

        private void SetError(Control control, String errorMessage)
        {
            errorProvider.SetError(control, errorMessage);
            control.Width -= errorProvider.Icon.Width;
        }

        private void LockControls()
        {
            txtIpAddress.Enabled = false;
            txtPort.Enabled = false;
            btnSelectPort.Enabled = false;
            btnScan.Enabled = false;
        }

        private void UnlockControls()
        {
            txtIpAddress.Enabled = true;
            txtPort.Enabled = true;
            btnSelectPort.Enabled = true;
            btnScan.Enabled = true;
        }

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

    }
}
