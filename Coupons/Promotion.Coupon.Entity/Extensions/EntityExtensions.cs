using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Entity.Extensions
{
    public static class EntityExtensions
    {
        public static Person ApiSerialize(this Person entity)
        {
            return new Person()
            {
                //Address = Person.Address.ApiSerialize(),               
                cpf = entity.cpf,
                dtCreation = entity.dtCreation,
                email = entity.email,
                idPerson = entity.idPerson,
                name = entity.name,              
            };
        }

        public static Receipt ApiSerialize(this Receipt receipt)
        {
            return new Receipt()
            {
                dtCreation = receipt.dtCreation,
                idPerson = receipt.idPerson,
                idReceipt = receipt.idReceipt,               
                Person = receipt.Person.ApiSerialize()
            };
        }
    }
}