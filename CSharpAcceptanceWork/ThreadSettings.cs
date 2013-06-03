using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpAcceptanceWork
{
    public class ThreadSettings
    {
        public ThreadSettings()
        {
            CurrentThread = 1;
            HighValue = 1;
            LowValue = 1;
            Threads = new List<Thread>();
            Progression = 1;
            LastRefresh = DateTime.MinValue;
            LastCalculation = DateTime.MinValue;
            Configuration = new Configuration();
            ProgramStart = DateTime.Now;
        }

        public int CurrentThread { get; set; }
        public long HighValue { get; set; }
        public long LowValue { get; set; }
        public List<Thread> Threads { get; set; }
        public long Progression;
        public DateTime LastRefresh { get; set; }
        public DateTime LastCalculation { get; set; }
        public DateTime ProgramStart { get; set; }

        public Configuration Configuration { get; set; }
    }
}
