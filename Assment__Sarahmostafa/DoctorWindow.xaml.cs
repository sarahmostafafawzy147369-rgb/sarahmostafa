using Assment__Sarahmostafa.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Assment__Sarahmostafa.Models;
using Assment__Sarahmostafa.Data;
using Microsoft.EntityFrameworkCore;

namespace Assment__Sarahmostafa
{
    /// <summary>
    /// Interaction logic for DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        public DoctorWindow()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            List<string> sta = new List<string> { "Scheduled", "Completed", "Cancelled" };

            cmb_doctor.ItemsSource = sta;
            using (var db = new MyDbcontext())
            {
                dg_doctor.ItemsSource = db.Appointment.Where(t=>t.datetime.Day==DateTime.Today.Day).Include(p => p.Patientss).Select(t => new
                {
                    PID=t.AppointmentID,
                    PName = t.Patientss.Name,
                    PPhone = t.Patientss.Phone,
                    PAge = t.Patientss.Age,
                    pnote = t.Patientss.Notes,
                    Apreseaon = t.Reason,
                    appDate = t.datetime,
                    status = t.Status,
                }).ToList();
            }
        }

        private void dg_doctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dg_doctor.SelectedItem==null)
            {
                return;
            }
            dynamic sel = dg_doctor.SelectedItem;
            Id.Text= sel.PID.ToString();         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           if(dg_doctor.SelectedItem==null)
            {
                MessageBox.Show("Please Select Appoitment");
                return;
            }
           using (var db = new MyDbcontext())
            {
                var check = db.Appointment.FirstOrDefault(t => t.AppointmentID == int.Parse(Id.Text));
                if(check==null)
                {
                    MessageBox.Show("Appoientment Not Found");
                    return;
                }
                check.Status = cmb_doctor.SelectedValue.ToString();
                db.SaveChanges();
                MessageBox.Show("Status Updated Yet");
                LoadData();
                return;
            }
           
        }
    }
}
