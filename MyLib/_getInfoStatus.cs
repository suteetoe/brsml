using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Management;
using System.Collections;
namespace MyLib
{
    public class _getInfoStatus
    {
        [DllImport("kernel32.dll")]
        private static extern long GetVolumeInformation(
            string PathName,
            StringBuilder VolumeNameBuffer,
            UInt32 VolumeNameSize,
            ref UInt32 VolumeSerialNumber,
            ref UInt32 MaximumComponentLength,
            ref UInt32 FileSystemFlags,
            StringBuilder FileSystemNameBuffer,
            UInt32 FileSystemNameSize);

        /// <summary>
        /// return Volume Serial Number from hard drive
        /// </summary>
        /// <param name="strDriveLetter">[optional] Drive letter</param>
        /// <returns>[string] VolumeSerialNumber</returns>
        public string GetVolumeSerial(string strDriveLetter)
        {
            if (strDriveLetter == "" || strDriveLetter == null) strDriveLetter = "C";
            string _xReturn = "";
            try
            {
                ManagementObject __disk;
                __disk = new ManagementObject("win32_logicaldisk.deviceid=\'" + strDriveLetter + ":\'");
                __disk.Get();
                _xReturn = Convert.ToString(__disk["VolumeSerialNumber"]);
            }
            catch
            {
                try
                {

                }
                catch
                {

                }
            }
            return _xReturn;
            //return disk["VolumeSerialNumber"].ToString();
        }
        public ArrayList _scrandive()
        {
            ArrayList __listdirve = new ArrayList();

            try
            {
                ManagementObject __disk;
                string[] __xDrivelocal = Environment.GetLogicalDrives();
                for (int __x = 0; __x < __xDrivelocal.Length; __x++)
                {
                    string __xdriveManage = __xDrivelocal[__x].Substring(0, 1);

                    __disk = new ManagementObject("win32_logicaldisk.deviceid=\'" + __xdriveManage + ":\'");
                    __disk.Get();
                    string _a = __disk["Caption"].ToString();
                    string valuex = __disk["DriveType"].ToString();
                    if (valuex.Equals("2") && _a.CompareTo("A:") != 0)
                    {
                        __listdirve.Add(__xdriveManage);

                    }
                    else
                    {

                    }
                }
            }
            catch
            {

            }
            return __listdirve;
        }

        /// <summary>
        /// Returns MAC Address from first Network Card in Computer
        /// </summary>
        /// <returns>[string] MAC Address</returns>
        public string GetMACAddress()
        {
            ManagementClass __mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection __moc = __mc.GetInstances();
            string __MACAddress = String.Empty;
            foreach (ManagementObject mo in __moc)
            {
                if (__MACAddress == String.Empty)  // only return MAC Address from first card
                {
                    if ((bool)mo["IPEnabled"] == true) __MACAddress = mo["MacAddress"].ToString();
                }
                mo.Dispose();
            }
            __MACAddress = __MACAddress.Replace(":", "");
            return __MACAddress;
        }
        /// <summary>
        /// Return processorId from first CPU in machine
        /// </summary>
        /// <returns>[string] ProcessorId</returns>
        public string GetCPUId()
        {
            string __cpuInfo = String.Empty;
            string __temp = String.Empty;
            ManagementClass __mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection __moc = __mc.GetInstances();
            foreach (ManagementObject __mo in __moc)
            {
                if (__cpuInfo == String.Empty)
                {// only return cpuInfo from first CPU
                    __cpuInfo = __mo.Properties["ProcessorId"].Value.ToString();
                }
            }
            return __cpuInfo;
        }
    }
}


