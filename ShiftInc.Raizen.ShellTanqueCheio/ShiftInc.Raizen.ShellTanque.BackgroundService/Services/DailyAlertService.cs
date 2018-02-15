using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanque.BackgroundService.Services
{
    public class DailyAlertService : AbstractService, IService
    {
        public override string GetName()
        {
            return "DailyAlertService";
        }

        private bool _sent = false;
        public override void Execute()
        {
            if (!_sent && DateTime.Now.Hour == 9)
            {
                int count = ShiftInc.Raizen.ShellTanqueCheio.Business.Receipt.GetCountByNotWinners();

                if (count > 0)
                {
                    ShiftInc.Raizen.ShellTanqueCheio.Business.NewsSending.SetToSend("danilocardia@gmail.com", ShellTanqueCheio.Business.NewsSending.NewsType.DailyAlert);
                }

                _sent = true;
            }
            else
            {
                _sent = false;
            }
        }
    }
}
