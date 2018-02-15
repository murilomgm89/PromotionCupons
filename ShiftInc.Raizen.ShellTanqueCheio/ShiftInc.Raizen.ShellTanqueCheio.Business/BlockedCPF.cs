using ShiftInc.Raizen.ShellTanqueCheio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanqueCheio.Business
{
    public class BlockedCPF
    {
        public static bool IsCPFBlocked(string cpf)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.BlockedCPFs.Where(b => b.CPF == cpf).Count() > 0;
            }
        }
    }
}
