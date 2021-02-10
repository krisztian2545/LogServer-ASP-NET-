using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogServer.Model
{
    public class LogMessage
    {

        public string AppName { get; set; }
        public string Message { get; set; }
        public long Time { get; set; }

    }
}
