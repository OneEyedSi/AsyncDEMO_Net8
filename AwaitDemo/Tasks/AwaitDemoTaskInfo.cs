using Common.MessageWriters;
using Common.Tasks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AwaitDemo.Tasks
{
    public class AwaitDemoTaskInfo : TaskColorInfoBase<AwaitDemoTaskId, ConsoleColor>
    {
        public AwaitDemoTaskInfo(AwaitDemoTaskId id, ConsoleColor textColor, 
            int duration, int indentLevel, bool includeThreadId) 
            : base (id, textColor, duration, indentLevel, includeThreadId, 
                  new ConsoleMessageWriter(textColor, includeThreadId))
        {
            
        }
    }
}
