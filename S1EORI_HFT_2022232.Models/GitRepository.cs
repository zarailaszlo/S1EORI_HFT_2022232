using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.Json.Serialization;

namespace S1EORI_HFT_2022232.Models
{
    public class GitRepository
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGitRepository { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Visibility { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }        
        public virtual ICollection<Commit> Commits { get; set; }
        public GitRepository() { }
        public GitRepository(string line)
        {
            string[] split = line.Split('#');
            IdGitRepository = int.Parse(split[0]);
            Name = split[1];
            Visibility = split[2];
            string format = "yyyy-MM-dd HH:mm:ss";
            CreatedDate = DateTime.ParseExact(split[3], format, CultureInfo.InvariantCulture);
            UserId = int.Parse(split[4]);

        }
    }
}
