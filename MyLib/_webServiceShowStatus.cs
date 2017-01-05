using System;
using System.Windows.Forms;

namespace MyLib
{
    public partial class _webServiceShowStatus : Form
    {
        public _webServiceShowStatus()
        {
            InitializeComponent();
            this._listView.Clear();
            for (int loop = 0; loop < _myGlobal._webServiceServerList.Count; loop++)
            {
                _myWebserviceType data = (_myWebserviceType)_myGlobal._webServiceServerList[loop];
                ListViewItem newItem = new ListViewItem();
                newItem.Text = data._webServiceName;
                if (data._connectBytesPerSecond != 0)
                {
                    newItem.Text = string.Concat(newItem.Text , " (" , data._connectBytesPerSecond.ToString() , " bytes/s)");
                }
                newItem.ImageIndex = (data._webServiceConnected) ? 0 : 1;
                this._listView.Items.Add(newItem);
            }
            this.Height = (_myGlobal._webServiceServerList.Count * this._listView.TileSize.Height) + 45;
        }

        private void _webServiceShowStatus_Load(object sender, EventArgs e)
        {
        }
    }
}