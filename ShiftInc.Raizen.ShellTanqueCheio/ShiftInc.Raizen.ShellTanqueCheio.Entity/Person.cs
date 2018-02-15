namespace ShiftInc.Raizen.ShellTanqueCheio.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person")]
    public partial class Person
    {
        public Person()
        {
            Receipts = new HashSet<Receipt>();
        }

        [Key]
        public int idPerson { get; set; }

        [Required]
        [StringLength(14)]
        [Index(IsUnique=true)]
        public string cpf { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public string birth { get; set; }

        [Required]
        [StringLength(14)]
        public string phone { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        public DateTime dtCreation { get; set; }

        //public virtual Address Address { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
