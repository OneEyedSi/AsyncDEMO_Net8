using AsyncDEMO_Net8._1_SyncVersion;
using Gold.ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDEMO_Net8
{
    [MenuClass("Menu")]
    public class Menu
    {
        [MenuMethod("Make breakfast synchronously")]
        public static void SyncBreakfast()
        {
            MakeBreakfast("Making breakfast synchronously", SyncBreakfastMaker.MakeBreakfast);
        }

        private static void MakeBreakfast(string title, Action<DateTime> makeBreakfastMethod)
        {
            var startTime = DateTime.Now;

            Console.WriteLine(title);
            Console.WriteLine();

            SyncBreakfastMaker.MakeBreakfast(startTime);

            var endTime = DateTime.Now;

            var timeTaken = endTime - startTime;

            Console.WriteLine($"Total time taken: {timeTaken.TotalSeconds.ToString(Constant.SecondsFormat)} seconds");
        }
    }
}
