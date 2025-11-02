using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Assment__Sarahmostafa.Models
{
    public class Appointments
    {
        [Key]
        public int AppointmentID { get; set; }
        public int DoctorID { get; set; } = 1;
        [ForeignKey("DoctorID")]
        public MyUser ?user { get; set; }
        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public Patients ?Patientss { get; set; }
        public DateTime datetime { get; set; } = DateTime.Now;
       // public DatePicker date { get; set; } = new DatePicker();
        [Required]
        public string? Reason { get; set; }
        public string? Status { get; set; }

    }
}
