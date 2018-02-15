using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanqueCheio.Business.Exceptions
{
    public class VendorNotFoundException : Exception
    {
        public VendorNotFoundException() : base("O CNPJ não foi encontrado na base de postos autorizados") { }
    }
}
