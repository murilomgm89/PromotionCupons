using System;
using System.Collections.Generic;
using Promotion.Coupon.Application.Interfaces.Base;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Poco;

namespace Promotion.Coupon.Application.Interfaces
{
    public interface IPersonApplication : IApplicationBase<Person>
    {
        Dictionary<string, int> GetCountPerDateBy(DateTime? from = null, DateTime? to = null);
        IEnumerable<Person> GetBy(DateTime from, DateTime to);
        IEnumerable<Receipt> GetBySearch(string search);
        PersonSaveResult Save(Person person);
        bool IsAllowedToSave(Person person);
        Person GetByCpf(string cpf);
        Person GetByCpfNotBlackList(string cpf);
        int GetCountBy(DateTime dtSince, DateTime? dtUntil = null);
        Person GetById(int id);
        bool AlreadyWinner(int idPerson);
    }
}