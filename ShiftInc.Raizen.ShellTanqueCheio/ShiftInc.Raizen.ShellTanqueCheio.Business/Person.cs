using ShiftInc.Raizen.ShellTanqueCheio.Business.Repository;
using ShiftInc.Raizen.ShellTanqueCheio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ShiftInc.Raizen.ShellTanqueCheio.Business.Exceptions;

namespace ShiftInc.Raizen.ShellTanqueCheio.Business
{
    public class Person
    {
        public static Dictionary<string, int> GetCountPerDateBy(DateTime? from = null, DateTime? to = null)
        {
            if (to == null)
            {
                to = DateTime.Now;
            }

            var response = new Dictionary<string, int>();

            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                var query = context.People.AsQueryable();

                if (from != null)
                {
                    query = query.Where(r => r.dtCreation >= from);
                }

                if (to != null)
                {
                    query = query.Where(r => r.dtCreation <= to);
                }               
                response = query.GroupBy(g => g.dtCreation.Year + "-" + (g.dtCreation.Month < 10 ? ("0" + g.dtCreation.Month.ToString()) : g.dtCreation.Month.ToString()) + "-" + (g.dtCreation.Day < 10 ? ("0" + g.dtCreation.Day.ToString()) : g.dtCreation.Day.ToString())).ToDictionary(r => r.Key, r => r.Count());
            }

            DateTime aux = DateTime.Now;

            if (from == null)
            {
                string strfrom = response.Min(r => r.Key);
                aux = new DateTime(Convert.ToInt32(strfrom.Split('-')[0]), Convert.ToInt32(strfrom.Split('-')[1]), Convert.ToInt32(strfrom.Split('-')[2]));
            }
            else
            {
                aux = new DateTime(from.Value.Year, from.Value.Month, from.Value.Day);
            }

            while (aux < to)
            {
                if (response.ContainsKey(aux.ToString("yyyy-MM-dd")))
                {

                }
                else
                {
                    response.Add(aux.ToString("yyyy-MM-dd"), 0);
                }

                aux = aux.AddDays(1);
            }

            return response;
        }

        public static IEnumerable<Entity.Person> GetBy(DateTime from, DateTime to)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.People.Where(p => p.dtCreation >= from && p.dtCreation <= to).ToList();
            }
        }

        public static IEnumerable<Entity.Person> GetBySearch(string search)
        {
            if (search == "")
                return new List<Entity.Person>();


            if (search.IndexOf('@') < 0)
            {
                search = search.Replace(".", "");
                search = search.Replace("-", "");
                search = search.Replace("/", "");
                search = search.Replace(" ", "");
                search = search.Replace("(", "");
                search = search.Replace(")", "");
            }
            

            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                int intSearch = 0;

                int.TryParse(search, out intSearch);

                return context.People
                    .Where(p =>                         
                        p.cpf.Contains(search) ||
                        p.email.Contains(search) ||
                        p.name.Contains(search) 
                    )
                    .ToList();
            }
        }

        public static PersonSaveResult Save(Entity.Person person)
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
                    var personVerification = GetByCPF(person.cpf);
                    if (personVerification != null)
                    {
                        person.idPerson = personVerification.idPerson;    
                    }
                    else
                    {
                        person.dtCreation = DateTime.Now;
                    }
                   
                    ShiftIncRepository.Save<Entity.Person>(person);

                    result.isSuccess = true;
                }
            }
            catch (PersonCPFFoundInBlacklistException)
            {
                result.isSuccess = false;
                result.ErrorCode = "error_cpf_found_in_blacklist";
            }
            catch (PersonCPFNotValidException)
            {
                result.isSuccess = false;
                result.ErrorCode = "error_cpf_invalid";
            }

            return result;
        }

        private static bool IsAllowedToSave(Entity.Person person)
        {
            if (BlockedCPF.IsCPFBlocked(person.cpf))
            {
                throw new PersonCPFFoundInBlacklistException();
            }
            
            if (person.cpf.Length != 11)
            {
                throw new PersonCPFNotValidException();
            }

            return true;
        }

        public static Entity.Person GetByCPF(string cpf)
        {
            cpf = cpf.Replace(".","").Replace("-","");

            if (IsAllowedToSave(new Entity.Person() { cpf = cpf }))
            {
                using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
                {
                    return context.People
                        .Where(pd => pd.cpf == cpf)     
                        .Include(pd => pd.Receipts)
                        .Include(pd => pd.Receipts.Select(r => r.Product))
                        .FirstOrDefault();
                }
            }

            return null;
        }

        public static Entity.Person GetByCPFNotBlackList(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.People
                    .Where(pd => pd.cpf == cpf)                    
                    .Include(pd => pd.Receipts)
                    .Include(pd => pd.Receipts.Select(r => r.Product))
                    .FirstOrDefault();
            }  
        }

        public static int GetCountBy(DateTime dtSince, DateTime? dtUntil = null)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                var query = context.People.Where(r => r.dtCreation >= dtSince).AsQueryable();

                if (dtUntil != null)
                {
                    query = query.Where(r => r.dtCreation <= dtUntil);
                }                
                return query.Count();
            }
        }

        public static Entity.Person GetById(int id)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.People
                    .Where(pd => pd.idPerson == id)                  
                    .Include(pd => pd.Receipts)
                    .Include(pd => pd.Receipts.Select(r => r.Product))
                    .FirstOrDefault();
            }
        }

        public class PersonSaveResult
        {
            public bool isSuccess { get; set; }
            public string ErrorCode { get; set; }
        }
    }
}
