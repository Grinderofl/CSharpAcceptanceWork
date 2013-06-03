using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAcceptanceWork
{
    public class Configuration
    {
        public int ThreadCount
        {
            get { return ConfigurationReader.ReadConfigurationEntry<int>(SettingNames.ThreadCount); }
        }

        public int ThreadToUse
        {
            get { return ConfigurationReader.ReadConfigurationEntry<int>(SettingNames.ThreadToUse); }
        }
    }
}
