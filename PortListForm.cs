using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PortScanner.Properties;

namespace PortScanner
{
    /// <summary>
    ///     Form used to display a list of ports based on the values of the ports dictionary.
    /// </summary>
    public sealed partial class PortListForm : Form
    {
        public PortListForm()
        {
            InitializeComponent();
            Text = Labels.PortListForm;
            SelectedPorts = new List<ushort>();
            listPorts.DataSource = new BindingSource(PortInfo.PortDictionary, null);
            listPorts.DisplayMember = "Value";
            listPorts.ValueMember = "Key";
        }

        public List<ushort> SelectedPorts { get; }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<ushort, string> selectedPort in listPorts.SelectedItems)
                SelectedPorts.Add(selectedPort.Key);

            listPorts.ClearSelected();

            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}