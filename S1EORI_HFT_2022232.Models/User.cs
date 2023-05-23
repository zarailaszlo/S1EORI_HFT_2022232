using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace S1EORI_HFT_2022232.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Range(0, 100)]
        public int Age { get; set; }
        public virtual ICollection<GitRepository> GitRepositories { get; set; }

        public User(string line) 
        {
            string[] split = line.Split('#');
            IdUser = int.Parse(split[0]);
            Username = split[1];
            Password = split[2];
            FullName = split[3];
            Email = split[4];
            Age = int.Parse(split[5]);
        }
    }
}
