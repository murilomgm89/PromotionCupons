using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanque.BackgroundService
{
    interface IService
    {
        string GetName();
        void Init(int interval);
        void Execute();
    }
}
