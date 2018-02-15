using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanque.BackgroundService
{
    public class Program
    {
        static List<IService> _services = new List<IService>();

        static void Main(string[] args)
        {
            LoadServices();
            InitServices();

            Console.ReadKey();
        }

        static void LoadServices()
        {
            _services.Add(new Services.SendEmailService());
        }

        static void InitServices()
        {
            foreach (var service in _services)
            {
                var configInterval = ConfigurationManager.AppSettings.Get(service.GetName() + ".Interval");
                if (String.IsNullOrEmpty(configInterval))
                {
                    configInterval = ConfigurationManager.AppSettings.Get("AbstractService.Interval");
                }

                var interval = Convert.ToInt32(configInterval);
                service.Init(interval);
            }
        }
    }
}
