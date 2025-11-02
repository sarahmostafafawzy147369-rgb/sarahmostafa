using Assment__Sarahmostafa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Assment__Sarahmostafa.Data
{
    public class MyDbcontext : DbContext
    {
        public DbSet<MyUser> Users { get; set; }
        public DbSet<Appointments> Appointment { get; set; }
        public DbSet<Patients> Patient { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=COM163-LAB3\\SQLEXPRESS;Initial Catalog=Medical_Appointment;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
