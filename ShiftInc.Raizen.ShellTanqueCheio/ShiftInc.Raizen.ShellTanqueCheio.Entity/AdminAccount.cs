using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanqueCheio.Entity
{
    [Table("AdminAccount")]
    public class AdminAccount
    {
        [Key]
        public int idAdminAccount { get; set; }

        [Index(IsUnique=true)]
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(20)]
        public string Password { get; set; }
    }
}
