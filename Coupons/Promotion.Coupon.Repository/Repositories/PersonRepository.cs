using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Promotion.Coupon.Entity.Poco;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Exceptions;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories.Base;

namespace Promotion.Coupon.Repository.Repositories
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public Dictionary<string, int> GetCountPerDateBy(DateTime? @from = null, DateTime? to = null)
        {
            using (var context = new GymPass())
            {
                var query = context.Person.AsQueryable();

                if (from != null)
                {
                    query = query.Where(r => r.dtCreation >= from);
                }

                if (to != null)
                {
                    query = query.Where(r => r.dtCreation <= to);
                }

                return query
                    .GroupBy(g => g.dtCreation.Year + "-" + (g.dtCreation.Month < 10 ? ("0" + g.dtCreation.Month.ToString()) : g.dtCreation.Month.ToString()) + "-" + (g.dtCreation.Day < 10 ? ("0" + g.dtCreation.Day.ToString()) : g.dtCreation.Day.ToString()))
                    .ToDictionary(r => r.Key, r => r.Count());
            }
        }

        public IEnumerable<Person> GetBy(DateTime @from, DateTime to)
        {
            using (var context = new GymPass())
            {
                return context.Person
                    .Where(p => p.dtCreation >= from 
                            && p.dtCreation <= to)
                    .ToList();
            }
        }

        public IEnumerable<Person> GetBySearch(string search)
        {
            using (var context = new GymPass())
            {
                int intSearch = 0;

                int.TryParse(search, out intSearch);

                return context.Person
                    .Where(p =>
                        p.cpf.Contains(search) ||
                        p.email.Contains(search) ||
                        p.name.Contains(search)
                    )
                    .ToList();
            }
        }

        public PersonSaveResult Save(Person person)
        {
            var result = new PersonSaveResult()
            {
                isSuccess = false
            };

            try
            {
                person.cpf = person.cpf.Replace(".", "").Replace("-", "");

                if (IsAllowedToSave(person))
                {
                    var personVerification = GetByCpf(person.cpf);
                    if (personVerification != null)
                    {
                        person.idPerson = personVerification.idPerson;
                    }
                    else
                    {
                        person.dtCreation = DateTime.Now;
                    }

                    this.Insert(person);

                    result.isSuccess = true;
                }
            }
            catch (PersonCpfFoundInBlacklistException)
            {
                result.isSuccess = false;
                result.ErrorCode = "error_cpf_found_in_blacklist";
            }
            catch (PersonCpfNotValidException)
            {
                result.isSuccess = false;
                result.ErrorCode = "error_cpf_invalid";
            }

            return result;
        }

        public bool IsAllowedToSave(Person person)
        {
            if (person.cpf.Length != 11)
                throw new PersonCpfNotValidException();

            return true;
        }

        public Person GetByCpf(string cpf)
        {
            using (var context = new GymPass())
            {
                return context.Person
                    .Where(pd => pd.cpf == cpf)
                    .Include(pd => pd.Receipt)
                    //.Include(pd => pd.Receipt.Select(r => r.Product))
                    .FirstOrDefault();
            }
        }

        public Person GetByCpfNotBlackList(string cpf)
        {
            using (var context = new GymPass())
            {
                return context.Person
                    .Where(pd => pd.cpf == cpf)
                    .Include(pd => pd.Receipt)
                    //.Include(pd => pd.Receipt.Select(r => r.Product))
                    .FirstOrDefault();
            }
        }

        public int GetCountBy(DateTime dtSince, DateTime? dtUntil = null)
        {
            using (var context = new GymPass())
            {
                var query = context.Person.Where(r => r.dtCreation >= dtSince).AsQueryable();

                if (dtUntil != null)
                {
                    query = query.Where(r => r.dtCreation <= dtUntil);
                }
                return query.Count();
            }
        }

        public Person GetById(int id)
        {
            using (var context = new GymPass())
            {
                return context.Person
                    .Where(pd => pd.idPerson == id)
                    .Include(pd => pd.Receipt)
                    //.Include(pd => pd.Receipt.Select(r => r.Product))
                    //.Include(pd => pd.Address)
                    //.Include(pd => pd.Address.City)
                    .FirstOrDefault();
            }
        }

        public bool AlreadyWinner(int idPerson)
        {
            using (var context = new GymPass())
            {
                return context.Receipt.Where(w => w.idPerson == idPerson).Any(a=> a.isValidated == true);
            }
        }
    }
}