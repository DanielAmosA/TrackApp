using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Models
{
    public class Machine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MachineID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string MachineType { get; set; }

        [ForeignKey("StatusID")]
        public int CurrentStatusId { get; set; }

        public string Notes { get; set; }

    }
}
