using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assment__Sarahmostafa.Models
{
    public class Patients
    {
        [Key]
        public int PatientID { get; set; }
        public string? Name { get; set; }
        public string ?Phone { get; set; }
        public string? Notes { get; set; }
        public  string ?Age { get; set; }    
    }
}
