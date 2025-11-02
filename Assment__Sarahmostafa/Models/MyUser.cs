using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assment__Sarahmostafa.Models
{
    public class MyUser
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string ?Name { get; set; }
        [Required]
        public string ?Password { get; set; }
        [Required]
        public string ?Email { get; set; }
        [Required]
        public string ?Role { get; set; }
    }
}
