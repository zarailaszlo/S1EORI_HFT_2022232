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
        [JsonIgnore]
        public virtual GitRepository GitRepository { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; } 
        public Commit() { }
        public Commit(string line) 
        {
            string[] split = line.Split('#');
            IdCommit = int.Parse(split[0]);
            Hash = split[1];
            Message = split[2];
            string format = "yyyy-MM-dd HH:mm:ss";
            CommittedDate = DateTime.ParseExact(split[3], format, CultureInfo.InvariantCulture);
            GitRepositoryId = int.Parse(split[4]);
            UserId = int.Parse(split[5]);
            
        }

    }
}
