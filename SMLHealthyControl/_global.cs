using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace SMLHealthyControl
{
    public class _global
    {
        public enum _HealthyControlTypeEnum
        {
            ว่าง,
            /// <summary>
            /// ประวัติคนไข้
            /// </summary>
            M_PatientProfile,
            /// <summary>
            /// ข้อมูลสุขภาพ
            /// </summary>
            m_yourhealthy,
            /// <summary>
            /// ประวัติคำปรึกษาสุขภาพ
            /// </summary>
            M_Consultation,
            /// <summary>
            /// โรคประจำตัว
            /// </summary>
            M_congenital_disease,
            /// <summary>
            /// ประวัติการให้คำปรึกษาการใช้ยา
            /// </summary>
            M_drugsConsultants,
            /// <summary>
            /// ประวัติแพ้ยา
            /// </summary>
            m_allergic
        }
    }
}
