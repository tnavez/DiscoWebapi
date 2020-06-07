using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Disco.Entities
{

    [Table("Cards", Schema = "dbo")]
    public class Cards
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CardId { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ExpirDate { get; set; }
        public Boolean Lost { get; set; }
       
        public Member Member { get; set; } 
    }
}
