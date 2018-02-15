using ShiftInc.Raizen.ShellTanqueCheio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanqueCheio.Business
{
    public class Product
    {
        public static IEnumerable<Entity.Product> GetByType(string type)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.Products.Where(p => p.type == type).ToList();
            }
        }
    }
}
