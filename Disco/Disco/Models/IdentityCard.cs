using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Disco.Entities;

namespace Disco.Models
{
    [Table("IdentiyCard",Schema="dbo")]
    public class IdentityCard
    {
        // [ForeignKey("Member")]

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idcarid { get; set; }


        [Required]
        public int CardNumber { get; set; }
        [Required]
        public string NatNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public DateTime ValiDate { get; set; }
        [Required]
        public DateTime ExpirDate { get; set; }
        public int MemberId { get; set; }
        public  Member Member { get; set; }
    }
}
