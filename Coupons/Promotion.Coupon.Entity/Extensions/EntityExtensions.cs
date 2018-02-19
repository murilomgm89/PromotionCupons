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
                birth = entity.birth,
                cpf = entity.cpf,
                dtCreation = entity.dtCreation,
                email = entity.email,
                idPerson = entity.idPerson,
                name = entity.name,
                phone = entity.phone
            };
        }

        public static Receipt ApiSerialize(this Receipt receipt)
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

        public static LuckyCode ApiSerialize(this LuckyCode code)
        {
            return new LuckyCode()
            {
                code = code.code,
                idLuckyCode = code.idLuckyCode
            };
        }
    }
}