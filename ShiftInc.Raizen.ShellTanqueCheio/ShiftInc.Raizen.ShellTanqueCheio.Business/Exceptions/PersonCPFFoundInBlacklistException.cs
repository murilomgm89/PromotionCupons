using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanqueCheio.Business.Exceptions
{
    public class PersonCPFFoundInBlacklistException : Exception
    {
        public PersonCPFFoundInBlacklistException() : base("CPF encontrado na blacklist") { }
    }
}
