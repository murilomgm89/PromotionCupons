using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanqueCheio.Business.Exceptions
{
    public class ReceiptCountPerPersonExceededException : Exception
    {
        public ReceiptCountPerPersonExceededException() : base("Já foram cadastrados para esse CPF o número máximo de cupons permitidos") { }
    }
}
