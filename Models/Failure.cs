using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Failures
{
    public class Failure
    {
        public enum FailureType
        {
            UnExpectedShutDown=0,
            ShortNonResponding=1,
            HardwareFailure=2,
            ConnectionProblems=3
        }
        
        public Device FailuredDevice { get; set; }
        public DateTime FailureTime { get; set; }
        public bool IsFailureCritical { get; set; }

        public static bool IsFailureSerious(int failureType)
        {
            if (failureType % 2 == 0) return true;
            return false;
        }

    }
}
