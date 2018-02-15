using ShiftInc.Raizen.ShellTanqueCheio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanqueCheio.Business
{
    public class AdminAccount
    {
        public static Entity.AdminAccount GetByCredentials(string username, string password)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.AdminAccounts.Where(aa => aa.Username == username && aa.Password == password).FirstOrDefault();
            }
        }
    }
}
