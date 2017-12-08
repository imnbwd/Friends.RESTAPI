using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Friends.Models
{
    public class Friend
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }

        public bool IsDeveloper { get; set; }
    }
}
