using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CSharpAcceptanceWork
{
    public class FibonacciThreader
    {
        private readonly string _configurationFileName;
        private readonly ThreadSettings _settings;
        private DateTime _lastUpdate = DateTime.MinValue;

        private readonly Configuration _defaults = new Configuration()
                                                       {
                                                           RefreshIntervalInSeconds = 10,
                                                           ThreadFrequencyInMilliseconds = new List<int> {2000, 500},
                                                           ThreadToUse = 1
                                                       };

        public FibonacciThreader (string configurationFileName)
        {
            _configurationFileName = configurationFileName;
            _settings = new ThreadSettings();
        }

        /// <summary>
        /// Executes the fibonacci threader
        /// </summary>
        public void Execute()
        {
            LoadConfiguration();
            _settings.Threads.Add(Thread.CurrentThread);
            for (var i = 1; i < _settings.Configuration.ThreadFrequencyInMilliseconds.Count; i++)
                _settings.Threads.Add(new Thread(RunThread));
            _lastUpdate = DateTime.Now;
            RunThread(_settings);
        }

        /// <summary>
        /// Runs the actual thread
        /// </summary>
        /// <param name="param"> </param>
        private void RunThread(object param)
        {
            var settings = (ThreadSettings) param;
            var thread = settings.CurrentThread;
            while (true)
            {
                if ((DateTime.Now - settings.LastRefresh).TotalSeconds >= settings.Configuration.RefreshIntervalInSeconds)
                {
                    LoadConfiguration();
                    settings.LastRefresh = DateTime.Now;
                    if (settings.Configuration.ThreadToUse != settings.CurrentThread)
                    {
                        settings.CurrentThread = settings.Configuration.ThreadToUse;
                        settings.Threads[thread - 1] = new Thread(() => RunThread(settings));
                        settings.Threads[thread - 1].Start();
                        break;
                    }
                }
                if ((DateTime.Now - settings.LastCalculation).TotalMilliseconds >=
                    settings.Configuration.ThreadFrequencyInMilliseconds[settings.CurrentThread - 1])
                    Fibonacci();
                
                if(_lastUpdate == DateTime.MinValue || (DateTime.Now - _lastUpdate).TotalMilliseconds >= 50)
                    Update();
            }
        }

        /// <summary>
        /// Updates console screen
        /// </summary>
        public void Update()
        {
            _lastUpdate = DateTime.Now;
            Console.Clear();
            Console.WriteLine("Current thread: " + _settings.CurrentThread);
            Console.WriteLine("Time to next calculation: " + (long) Math.Abs(
                (DateTime.Now -
                 _settings.LastCalculation.AddMilliseconds(
                     _settings.Configuration.ThreadFrequencyInMilliseconds[_settings.CurrentThread - 1])).
                    TotalMilliseconds));
            Console.WriteLine("Current result: " + _settings.HighValue);
            Console.WriteLine("Current progression: " + _settings.Progression);
            Console.WriteLine("Time to next refresh: " + (long) Math.Abs(
                (DateTime.Now - _settings.LastRefresh.AddSeconds(_settings.Configuration.RefreshIntervalInSeconds)).
                    TotalMilliseconds));
            //Console.WriteLine("Threads: " + Process.GetCurrentProcess().Threads.Count);
            Console.WriteLine("Number of operations per minute: " + string.Format("{0:0.00}", (_settings.Progression / (float)(DateTime.Now - _settings.ProgramStart).TotalSeconds) * 60));
            //Console.WriteLine("Number of seconds since start: " + (DateTime.Now - _settings.ProgramStart).TotalSeconds);
        }

        /// <summary>
        /// Calculates the next number in Fibonacci sequence
        /// </summary>
        public void Fibonacci()
        {
            _settings.HighValue = _settings.HighValue + _settings.LowValue;
            _settings.LowValue = _settings.HighValue - _settings.LowValue;
            _settings.Progression++;
            _settings.LastCalculation = DateTime.Now;
        }

        /// <summary>
        /// Read configuration from configuration file and assign that or defaults if not found
        /// </summary>
        /// <returns></returns>
        public void LoadConfiguration()
        {
            // Possible to load from a backup from, say, Roaming
            if (File.Exists(_configurationFileName))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof (Configuration));
                    // Possible to save a backup here into Roaming etc.
                    using (var stream = new FileStream(_configurationFileName, FileMode.Open))
                        _settings.Configuration = (Configuration) serializer.Deserialize(stream);
                }
                catch
                {
                    // If needed we can act differently according to thrown exceptions
                }
            }
            else
                _settings.Configuration = _defaults;
        }

    }
}
