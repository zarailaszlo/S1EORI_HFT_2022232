using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1EORI_HFT_2022232.Models
{
    public class Commit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCommit { get; set; }
        [Required]
        public string Hash { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime CommittedDate { get; set; }
        [ForeignKey("GitRepository")]
        public int GitRepositoryId { get; set; }
        public virtual GitRepository GitRepository { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

    }
}
