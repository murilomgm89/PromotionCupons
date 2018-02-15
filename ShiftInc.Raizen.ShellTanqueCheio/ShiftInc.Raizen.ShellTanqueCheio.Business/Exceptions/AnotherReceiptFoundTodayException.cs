using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanqueCheio.Business.Exceptions
{
    public class AnotherReceiptFoundTodayException : Exception
    {
        public AnotherReceiptFoundTodayException() : base("Outro cupom desse tipo já foi adicionado hoje, o usuário deve tentar novamente amanhã") { }
    }
}
