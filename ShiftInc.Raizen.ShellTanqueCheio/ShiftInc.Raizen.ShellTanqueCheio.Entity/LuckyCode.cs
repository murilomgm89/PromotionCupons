namespace ShiftInc.Raizen.ShellTanqueCheio.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LuckyCode")]
    public partial class LuckyCode
    {
        [Key]
        public int idLuckyCode { get; set; }

        [Required]
        [Index(IsUnique=true)]       
        public int code { get; set; } 
        public int idSerie { get; set; }
        public int serie { get; set; }
        public int idReceipt { get; set; }
        public bool isWinner { get; set; }       
        public DateTime? dtWinner { get; set; }
        public DateTime? dtRaffle { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}
