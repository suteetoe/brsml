using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DTSClientDownload
{
    public partial class _dtsMain : Form
    {
        Thread _dataSyncDTSServer = null;

        string _versionInfo = "";
        string _programName = "SCG Download";

        public _dtsMain()
        {
            InitializeComponent();

            System.Version __myVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime __myTime = new DateTime(2000, 1, 1).AddDays(__myVersion.Build).AddSeconds(__myVersion.Revision * 2);
            this._versionInfo = string.Format("Build : {0:s}", __myTime);

            this.Text = string.Format("{0} {1}", this._programName, this._versionInfo);

            _dts_item_download _itemDownload = new _dts_item_download();
            _itemDownload.Dock = DockStyle.Fill;
            this._itemTab.Controls.Add(_itemDownload);

            _dts_po_download _poDownload = new _dts_po_download();
            _poDownload.Dock = DockStyle.Fill;
            this._poTab.Controls.Add(_poDownload);

            _so_Download _soDownload = new _so_Download();
            _soDownload.Dock = DockStyle.Fill;
            this._soTab.Controls.Add(_soDownload);

            _download_log _logDownload = new _download_log();
            _logDownload.Dock = DockStyle.Fill;
            this._logTab.Controls.Add(_logDownload);

            this.Load += new EventHandler(_dtsMain_Load);
        }

        void _dtsMain_Load(object sender, EventArgs e)
        {
            this._dataSyncDTSServer = new Thread(new ThreadStart(_dtsSync._startSync));
            this._dataSyncDTSServer.IsBackground = true;
            this._dataSyncDTSServer.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("ต้องการจบโปรแกรมจริงหรือไม่", DTSClientDownload._global._champMessageCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
            }
            base.OnClosing(e);
        }

    }
}
