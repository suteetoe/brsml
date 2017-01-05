using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPPShipment
{
    public partial class _shipment : UserControl
    {
        public _shipment()
        {
            InitializeComponent();

            this._shipmentControl1.Disposed += _shipmentControl1_Disposed;
        }

        void _shipmentControl1_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
