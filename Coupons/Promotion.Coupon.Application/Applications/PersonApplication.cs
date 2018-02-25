using System;
using System.Collections.Generic;
using System.Linq;
using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Exceptions;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Entity.Poco;
using Promotion.Coupon.Repository.Repositories;

namespace Promotion.Coupon.Application.Applications
{
    public class PersonApplication : ApplicationBase<Person>, IPersonApplication
    {
        private readonly IPersonRepository _personRepository;
        //private readonly IBlockedCpfRepository _blockedCpfRepository;

        public PersonApplication()
        {
            _personRepository = new PersonRepository();
            //_blockedCpfRepository = new BlockedCpfRepository();
        }

        public Dictionary<string, int> GetCountPerDateBy(DateTime? @from = null, DateTime? to = null)
        {
            if (to == null)
                to = DateTime.Now;

            var data = _personRepository.GetCountPerDateBy(from, to);

            DateTime aux;

            if (from == null)
            {
                string strfrom = data.Min(r => r.Key);
                aux = new DateTime(Convert.ToInt32(strfrom.Split('-')[0]), Convert.ToInt32(strfrom.Split('-')[1]), Convert.ToInt32(strfrom.Split('-')[2]));
            }
            else
            {
                aux = new DateTime(from.Value.Year, from.Value.Month, from.Value.Day);
            }

            while (aux < to)
            {
                if (data.ContainsKey(aux.ToString("yyyy-MM-dd")))
                {

                }
                else
                {
                    data.Add(aux.ToString("yyyy-MM-dd"), 0);
                }

                aux = aux.AddDays(1);
            }

            return data;
        }

        public IEnumerable<Person> GetBy(DateTime @from, DateTime to)
        {
            return _personRepository.GetBy(from, to);
        }

        public IEnumerable<Person> GetBySearch(string search)
        {
            if (string.IsNullOrEmpty(search))
                return new List<Person>();


            if (search.IndexOf('@') < 0)
            {
                search = search.Replace(".", "");
                search = search.Replace("-", "");
                search = search.Replace("/", "");
                search = search.Replace(" ", "");
                search = search.Replace("(", "");
                search = search.Replace(")", "");
            }

            return _personRepository.GetBySearch(search);
        }

        public PersonSaveResult Save(Person person)
        {
            return _personRepository.Save(person);
        }

        public new void Insert(Person entity)
        {
            this.Save(entity);
        }

        public bool IsAllowedToSave(Person person)
        {
            //if (_blockedCpfRepository.IsCpfBlocked(person.cpf))
            //    throw new PersonCpfFoundInBlacklistException();
            
            if (person.cpf.Length != 11)
                throw new PersonCpfNotValidException();
            
            return true;
        }

        public Person GetByCpf(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");

            return IsAllowedToSave(new Person() { cpf = cpf }) ? _personRepository.GetByCpf(cpf) : null;
        }

        public Person GetByCpfNotBlackList(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");

            return _personRepository.GetByCpfNotBlackList(cpf);
        }

        public int GetCountBy(DateTime dtSince, DateTime? dtUntil = null)
        {
            return _personRepository.GetCountBy(dtSince, dtUntil);
        }

        public Person GetById(int id)
        {
            return _personRepository.GetById(id);
        }

        public bool AlreadyWinner(int idPerson)
        {
            return _personRepository.AlreadyWinner(idPerson);
        }
    }
}