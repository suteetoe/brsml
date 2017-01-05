using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public class _clone_icTrans : UserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private SMLInventoryControl._icTransControl _ictrans;

        public _clone_icTrans(_g.g._transControlTypeEnum mode, string menuName)
        {
            if (mode == _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก || mode == _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด)
            {
                // toe fix ia remove haeder from auto adjust
                string __removeQuery = "delete from ic_trans_detail where trans_flag = 66 and not exists(select doc_no from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag) ";
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __removeQuery);
            }

            this._ictrans = new SMLInventoryControl._icTransControl();
            this.SuspendLayout();
            // 
            // _ictrans
            // 
            this._ictrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ictrans._transControlType = mode;
            this._ictrans.Location = new System.Drawing.Point(0, 0);
            this._ictrans.Name = "_ictrans";
            this._ictrans.Size = new System.Drawing.Size(859, 708);
            this._ictrans.TabIndex = 0;
            // 
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ictrans);
            this.Name = menuName;
            this.Size = new System.Drawing.Size(1056, 604);
            this.ResumeLayout(false);
            //
            this._ictrans.Disposed += new EventHandler(_ictrans_Disposed);
        }

        void _ictrans_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
