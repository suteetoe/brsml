using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SINGHAReport
{
    public partial class _saleToolsWebControl : UserControl
    {
        public _saleToolsWebControl(string menuName, string urlMenu)
        {
            // get permission 
            urlMenu = urlMenu.Replace("userid=admagent", "userid=" + MyLib._myGlobal._userCode);

            MyLib._PermissionsType __permission = MyLib._myGlobal._isAccessMenuPermision(MyLib._myGlobal._mainMenuIdPassTrue, MyLib._myGlobal._mainMenuCodePassTrue);
            string __auth = "&auth=" + (__permission._isAdd ? "c" : "") + (__permission._isEdit ? "u" : "") + (__permission._isDelete ? "d" : "");
            //             string __url = "http://61.91.199.171/DBO_AGENT/" + urlMenu + __auth;

            string __url = ((_g.g._companyProfile._mobile_sale_url.IndexOf("http") == -1) ? "http://" : "" ) + _g.g._companyProfile._mobile_sale_url + "/" + urlMenu + __auth + "&bypasskey=" + _g.g._companyProfile._mobile_bypasskey;
            InitializeComponent();
            this._browser.Navigate(__url);

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
