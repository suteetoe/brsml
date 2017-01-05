using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient
{
    public partial class _posCashControl : UserControl
    {
        public _posCashControl()
        {
            InitializeComponent();
            _setFont_vista_button();
        }

        private void _setFont_vista_button()
        {
            Font __font = new Font("tahoma", 12, FontStyle.Regular);
           
            foreach (Control __control in this._myFlowLayoutPanel_buttom_left.Controls)
            {
                if (__control.GetType() == typeof(MyLib.VistaButton))
                {
                    __control.Font = __font;
                    
            
                }

            }
            foreach (Control __control in this._myFlowLayoutPanel_buttom_right.Controls)
            {
                if (__control.GetType() == typeof(MyLib.VistaButton))
                {
                    __control.Font = __font;

                }

            }

            foreach (Control __control in this._myFlowLayoutPanel_buttom_top.Controls)
            {

                if (__control.GetType() == typeof(MyLib.VistaButton))
                {
                    __control.Font = __font;

                }

            }
            foreach (Control __control in this._myFlowLayoutPanel_top_fill.Controls)
            {
                if (__control.GetType() == typeof(MyLib.VistaButton))
                {
                    __control.Font = __font;
                    MyLib.VistaButton __getControl = (MyLib.VistaButton)__control;
                    Size __size = new Size(48, 48);
                    __getControl.ImageSize = __size;
                    
                }

            }
        }
    }
}
