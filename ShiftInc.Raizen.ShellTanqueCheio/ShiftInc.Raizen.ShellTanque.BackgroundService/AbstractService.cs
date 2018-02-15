using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ShiftInc.Raizen.ShellTanque.BackgroundService
{
    public class AbstractService
    {
        int _interval = 1000 * 60;
        Timer _timer;

        public virtual string GetName()
        {
            return "AbstractService";
        }

        public void Init(int interval)
        {
            this._interval = interval;

            _timer = new Timer(_interval);
            _timer.Elapsed += _timer_Elapsed;

            _timer.Start();
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();

            var dtNow = DateTime.Now;
            Console.WriteLine("[{0}] - Início {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), this.GetName());

            Parallel.Invoke(() =>
            {
                try
                {
                    this.Execute();
                }
                catch (Exception ex)
                {
                    
                }

                _timer.Start();

                Console.WriteLine("[{0}] - Termino {1} em {2}s", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), this.GetName(), DateTime.Now.Subtract(dtNow).TotalSeconds);
            });
        }

        public virtual void Execute()
        {

        }

    }
}
