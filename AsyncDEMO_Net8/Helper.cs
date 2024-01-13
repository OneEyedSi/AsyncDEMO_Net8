using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AsyncDEMO_Net8
{
    public static class Helper
    {
        public static void WriteWithElapsedTime(string message, DateTime startTime)
        {
            DateTime currentTime = DateTime.Now;
            var elapsedTime = currentTime - startTime;
            Console.WriteLine($"{elapsedTime.TotalSeconds.ToString(Constant.SecondsFormat)} seconds elapsed: {message}");
            Console.WriteLine();
        }
    }
}
