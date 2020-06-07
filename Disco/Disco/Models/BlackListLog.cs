using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Disco.Entities;

namespace Disco.Models
{

    [Table("BlackList", Schema ="dbo")]
    public class BlackListLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BlackId { get; set; }
        public Member Member { get; set; }
        public DateTime BlackListDate { get; set; }

    }
}
