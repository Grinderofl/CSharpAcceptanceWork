using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAcceptanceWork
{
    [Serializable]
    public class Configuration
    {
        public int ThreadToUse { get; set; }
        public int RefreshIntervalInSeconds { get; set; }
        public List<int> ThreadFrequencyInMilliseconds { get; set; }
    }
}
