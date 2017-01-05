using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace SMLHealthyControl
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {
            switch (menuName.ToLower())
            {
                case "menu_ar_detail_healthy": return (new SMLERPAR.ar_dealerHealthy());
               // case "menu_cust_m_patientprofile": return (new  SMLHealthyControl._healthyControl.M_PatientProfile());
                case "menu_cust_healthy_m_yourhealthy": return (new SMLHealthyControl._healthyControl.M_yourhealthy());
                case "menu_cust_healthy_m_consultation": return (new SMLHealthyControl._healthyControl.M_Consultation());
                case "menu_cust_healthy_m_congenital_disease": return (new SMLHealthyControl._healthyControl.M_congenital_disease());
                case "menu_cust_healthy_m_drugsconsultants": return (new SMLHealthyControl._healthyControl.M_drugsConsultants());
                case "menu_cust_healthy_m_allergic": return (new SMLHealthyControl._healthyControl.M_allergic());
                case "menu_healthy_m_information": return (new SMLHealthyControl._healthyControl.M_information());  
                         
            }
            return null;
        }
    }
}
