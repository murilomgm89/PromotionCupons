namespace ShiftInc.Raizen.ShellTanqueCheio.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Receipt")]
    public partial class Receipt
    {
        public Receipt()
        {
            LuckyCodes = new HashSet<LuckyCode>();
        }

        [Key]
        public int idReceipt { get; set; }

        public int idPerson { get; set; }

        public int idProduct { get; set; }

        //[Required]
        //[StringLength(17)]
        //public string vendorCNPJ { get; set; }

        //public bool isWinner { get; set; }

        public bool? isValidated { get; set; }
        
        [StringLength(200)]
        public string invalidateDescription { get; set; }

        public DateTime dtCreation { get; set; }

        public virtual ICollection<LuckyCode> LuckyCodes { get; set; }

        public virtual Person Person { get; set; }

        public virtual Product Product { get; set; }
    }
}
