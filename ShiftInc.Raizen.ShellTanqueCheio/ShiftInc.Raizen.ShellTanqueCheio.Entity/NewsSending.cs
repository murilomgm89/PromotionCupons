using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanqueCheio.Entity
{
    [Table("NewsSending")]
    public class NewsSending
    {
        [Key]
        public int idNewsSending { get; set; }

        [StringLength(80)]
        public string Destination { get; set; }

        public int? idReceipt { get; set; }

        public int? idPerson { get; set; }

        [StringLength(100)]
        public string Subject { get; set; }

        [StringLength(80)]
        public string fileName { get; set; }

        public short Status { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string StatusDetail { get; set; }

        public DateTime? dtSending { get; set; }

        [ForeignKey("idPerson")]
        public Entity.Person Person { get; set; }

        [ForeignKey("idReceipt")]
        public Entity.Receipt Receipt { get; set; }
    }
}
