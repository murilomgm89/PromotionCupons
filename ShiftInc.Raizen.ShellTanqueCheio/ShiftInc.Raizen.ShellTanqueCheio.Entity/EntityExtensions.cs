using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanqueCheio.Entity
{
    public static class EntityExtensions
    {
        public static Entity.Person ApiSerialize(this Entity.Person Person)
        {
            return new Person()
            {
                //Address = Person.Address.ApiSerialize(),
                birth = Person.birth,
                cpf = Person.cpf,
                dtCreation = Person.dtCreation,
                email = Person.email,
                idPerson = Person.idPerson,
                name = Person.name,
                phone = Person.phone
            };
        }
       
        public static Receipt ApiSerialize(this Entity.Receipt receipt)
        {
            return new Receipt()
            {
                dtCreation = receipt.dtCreation,
                idPerson = receipt.idPerson,
                idProduct = receipt.idProduct,
                idReceipt = receipt.idReceipt,
                //isWinner = receipt.isWinner,
                //vendorCNPJ = receipt.vendorCNPJ,
                Person = receipt.Person.ApiSerialize()
            };
        }

        public static LuckyCode ApiSerialize(this Entity.LuckyCode code)
        {
            return new LuckyCode()
            {
                code = code.code,
                idLuckyCode = code.idLuckyCode               
            };
        }
    }
}
