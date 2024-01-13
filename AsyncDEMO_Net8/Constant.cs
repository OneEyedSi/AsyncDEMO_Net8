using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDEMO_Net8
{
    public static class Constant
    {
        public const string SecondsFormat = "F1";

        /// <summary>
        /// Task durations in milliseconds.
        /// </summary>
        public static class TaskDuration
        {
            public const int HeatPan = 3000;
            public const int FryEggs = 3000;
            public const int FryBacon = 5000;
            public const int MakeCoffee = 4000;
            public const int MakeToast = 2000;
            public const int OtherWork = 5000;
        }
    }
}
