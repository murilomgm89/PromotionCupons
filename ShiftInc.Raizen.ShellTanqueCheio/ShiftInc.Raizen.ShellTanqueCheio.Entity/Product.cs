namespace ShiftInc.Raizen.ShellTanqueCheio.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            Receipts = new HashSet<Receipt>();
        }

        [Key]
        public int idProduct { get; set; }

        [Required]
        [StringLength(14)]
        public string type { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
