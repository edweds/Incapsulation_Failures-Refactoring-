using Incapsulation.Failures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Failures
{

    public class Common
    {
        
        public static int Earlier(object[] v, int day, int month, int year)
        {
            int vYear = (int)v[2];
            int vMonth = (int)v[1];
            int vDay = (int)v[0];
            if (vYear < year) return 1;
            if (vYear > year) return 0;
            if (vMonth < month) return 1;
            if (vMonth > month) return 0;
            if (vDay < day) return 1;
            return 0;
        }
    }

    public class ReportMaker
    {
        /// <summary>
        /// </summary>
        /// <param name="day"></param>
        /// <param name="failureTypes">
        /// 0 for unexpected shutdown, 
        /// 1 for short non-responding, 
        /// 2 for hardware failures, 
        /// 3 for connection problems
        /// </param>
        /// <param name="deviceId"></param>
        /// <param name="times"></param>
        /// <param name="devices"></param>
        /// <returns></returns>
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] failureTypes, 
            int[] deviceId, 
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            var result = new List<string>();
            DateTime receivedDate = new DateTime(year,month,day);
            var devicesList = new List<Device>();
            var failuresList = new List<Failure>();

            for (int i=0; i< deviceId.Length; i++)
            {
                var newDevice = new Device() { Name = devices[i]["Name"] as string, Id = (int)devices[i]["DeviceId"] };
                devicesList.Add(newDevice);
                var newFailure = new Failure()
                {
                    FailuredDevice = newDevice,
                    FailureTime = new DateTime((int)times[i][2],(int)times[i][1],(int)times[i][0]),
                    IsFailureCritical = Failure.IsFailureSerious(failureTypes[i])
                };
                    failuresList.Add(newFailure);                    
            }
            result = FindDevicesFailedBeforeDate(receivedDate,failuresList,devicesList);
            return result;

        }

        public static List<string> FindDevicesFailedBeforeDate (DateTime failureDate, List<Failure> failures, List<Device> devices)
        {
            var problematicDevices = new List <string> ();
            foreach (var device in devices)
                foreach (var failure in failures)
                {
                    if (device.Id == failure.FailuredDevice.Id && failure.FailureTime <= failureDate && failure.IsFailureCritical)
                        problematicDevices.Add(device.Name);
                }

            return problematicDevices;
        }

    }
}
