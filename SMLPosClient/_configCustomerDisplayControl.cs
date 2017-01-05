using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace SMLPosClient
{
    public partial class _configCustomerDisplayControl : UserControl
    {

        public _configCustomerDisplayControl()
        {
            InitializeComponent();

            InitializeControlValues();
            _enable(this._use_customer_checkbox.Checked);
        }

        /*
        private void RefreshComPortList()
        {
            // Determain if the list of com port names has changed since last checked
            string selected = RefreshComPortList(cmbPortName.Items.Cast<string>(), cmbPortName.SelectedItem as string, comport.IsOpen);

            // If there was an update, then update the control showing the user the list of port names
            if (!String.IsNullOrEmpty(selected))
            {
                cmbPortName.Items.Clear();
                cmbPortName.Items.AddRange(OrderedPortNames());
                cmbPortName.SelectedItem = selected;
            }
        }

        private string[] OrderedPortNames()
        {
            // Just a placeholder for a successful parsing of a string to an integer
            int num;

            // Order the serial port names in numberic order (if possible)
            return SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToArray();
        }


        private string RefreshComPortList(IEnumerable<string> PreviousPortNames, string CurrentSelection, bool PortOpen)
        {
            // Create a new return report to populate
            string selected = null;

            // Retrieve the list of ports currently mounted by the operating system (sorted by name)
            string[] ports = SerialPort.GetPortNames();

            // First determain if there was a change (any additions or removals)
            bool updated = PreviousPortNames.Except(ports).Count() > 0 || ports.Except(PreviousPortNames).Count() > 0;

            // If there was a change, then select an appropriate default port
            if (updated)
            {
                // Use the correctly ordered set of port names
                ports = OrderedPortNames();

                // Find newest port if one or more were added
                string newest = SerialPort.GetPortNames().Except(PreviousPortNames).OrderBy(a => a).LastOrDefault();

                // If the port was already open... (see logic notes and reasoning in Notes.txt)
                if (PortOpen)
                {
                    if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else selected = ports.LastOrDefault();
                }
                else
                {
                    if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else selected = ports.LastOrDefault();
                }
            }

            // If there was a change to the port list, return the recommended default selection
            return selected;
        }
         * */

        /// <summary> Populate the form's controls with default settings. </summary>
        private void InitializeControlValues()
        {
            cmbParity.Items.Clear(); cmbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cmbStopBits.Items.Clear(); cmbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));

            /*
            cmbParity.Text = settings.Parity.ToString();
            cmbStopBits.Text = settings.StopBits.ToString();
            cmbDataBits.Text = settings.DataBits.ToString();
            cmbParity.Text = settings.Parity.ToString();
            cmbBaudRate.Text = settings.BaudRate.ToString();
            CurrentDataMode = settings.DataMode;

            RefreshComPortList();

            chkClearOnOpen.Checked = settings.ClearOnOpen;
            chkClearWithDTR.Checked = settings.ClearWithDTR;
            */

            // If it is still avalible, select the last com port used
            /*
            if (cmbPortName.Items.Contains(settings.PortName)) cmbPortName.Text = settings.PortName;
            else if (cmbPortName.Items.Count > 0) cmbPortName.SelectedIndex = cmbPortName.Items.Count - 1;
            else
            {
                MessageBox.Show(this, "There are no COM Ports detected on this computer.\nPlease install a COM Port and restart this app.", "No COM Ports Installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }*/
        }

        private void _use_customer_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            _enable(this._use_customer_checkbox.Checked);
        }

        void _enable(bool enable)
        {
            _myLabel1.Enabled = enable;
            _myLabel2.Enabled = enable;
            _myLabel3.Enabled = enable;
            label1.Enabled = enable;
            this.lblDataBits.Enabled = enable;
            this.lblStopBits.Enabled = enable;
            
            _customerDisplayPort.Enabled = enable;
            cmbBaudRate.Enabled = enable;
            cmbDataBits.Enabled = enable;
            cmbParity.Enabled = enable;
            cmbStopBits.Enabled = enable;
            _customerDisplayLineCount.Enabled = enable;
            _testConnect.Enabled = enable;
        }

        private void _testConnect_Click(object sender, EventArgs e)
        {
            try
            {
                SerialPort __port = new SerialPort(this._customerDisplayPort.Text, (int)int.Parse(this.cmbBaudRate.Text), (Parity)Enum.Parse(typeof(Parity), this.cmbParity.Text), (int)int.Parse(this.cmbDataBits.Text), (StopBits)Enum.Parse(typeof(StopBits), this.cmbStopBits.Text));
                __port.Open();
                __port.Write(new byte[] { 12 }, 0, 1);
                __port.Write("Customer Display");
                __port.Close();

                MessageBox.Show("Connected.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}
