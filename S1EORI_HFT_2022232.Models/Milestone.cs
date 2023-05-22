using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace S1EORI_HFT_2022232.Models
{
    public class Milestone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMilestone { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        [DefaultValue(false)]
        public bool IsCompleted { get; set; }
        [ForeignKey("GitRepository")]
        public int GitRepositoryId { get; set; }
        [NotMapped]
        public virtual GitRepository GitRepository { get; set; }
    }
}
