using DroneTaxiService.Models;
using DroneTaxiService.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Timers;


namespace DroneTaxiService
{
    class Program
    {
        static int tickerCount = 0;
        static int maxTickerCount;
        static Timer _tmr = new();

        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json").Build();

           var paramsClass = configuration.GetSection(nameof(Params)).Get<Params>();

            maxTickerCount = paramsClass.MaxTickerCount;

            ServiceEngine.Initialize(paramsClass);
            ServiceEngine.MakeOutput(++tickerCount);

            _tmr.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _tmr.Interval = paramsClass.Interval;
            _tmr.Enabled = true;

            Console.ReadLine();
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {

            ServiceEngine.Simulation();
            ServiceEngine.MakeOutput(++tickerCount);

            if (tickerCount >= maxTickerCount)
                _tmr.Enabled = false;


        }
    }
}

