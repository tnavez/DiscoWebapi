using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Disco.Models;

namespace Disco.Entities
{

    [Table("Member", Schema = "dbo")]
    public class Member
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int MemberId { get; set; }

        //!!!!
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<Cards> Cards { get; set; } 
        public List<BlackListLog> BlackListLogs { get; set; }

        public IdentityCard Identity { get; set; }
 
    }
}
