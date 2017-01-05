using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPPShipment
{
    public partial class _saleForShipment : UserControl
    {
        public _saleForShipment()
        {
            InitializeComponent();

            this._shipmentControl1.Disposed += _saleForShipment_Disposed;
        }

        void _saleForShipment_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
