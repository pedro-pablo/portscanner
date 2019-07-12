using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortScanner
{

    /// <summary>
    /// Form used to display a list of ports based on the values of the ports dictionary.
    /// </summary>
    public partial class PortListForm : Form
    {
        public ushort SelectedPort { get; private set; }

        public PortListForm()
        {
            InitializeComponent();
            listPorts.DataSource = new BindingSource(PortInfo.portDictionary, null);
            listPorts.DisplayMember = "Value";
            listPorts.ValueMember = "Key";
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            SelectedPort = ((KeyValuePair<ushort, string>)listPorts.SelectedItem).Key;
            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
