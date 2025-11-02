using Assment__Sarahmostafa.Data;
using Assment__Sarahmostafa.Models;
using Microsoft.EntityFrameworkCore;
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

namespace Assment__Sarahmostafa
{
    /// <summary>
    /// Interaction logic for ReceptionistWindow.xaml
    /// </summary>
    public partial class ReceptionistWindow : Window
    {
        public ReceptionistWindow()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            List<string> sta = new List<string> { "Scheduled", "Completed", "Cancelled" };

            cmb_status.ItemsSource = sta;
            using (var db = new MyDbcontext())
            {
                AllPatients.ItemsSource = db.Appointment.Include(p => p.Patientss).Select(t => new
                {
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_name.Text) || string.IsNullOrEmpty(txt_notes.Text) || string.IsNullOrEmpty(txt_phone.Text) || string.IsNullOrEmpty(txt_reseson.Text) || cmb_status.SelectedItem == null)
            {
                MessageBox.Show("Please All Data Must Be Compelted");
                return;
            }
            if(!txt_phone.Text.Trim().All(char.IsDigit)||!txt_age.Text.Trim().All(char.IsDigit))
            {
                MessageBox.Show("Phone Number &Age Must Be Digit");
                return ;
            }
            using (var db = new MyDbcontext())
            {
                var check = db.Patient.FirstOrDefault(t => t.Name == txt_name.Text);
                if (check != null)
                {
                    MessageBox.Show("This Patient Already Exist ");
                    return;
                }
                var newPatient = new Patients
                {
                    Name = txt_name.Text,
                    Phone = txt_phone.Text,
                    Notes = txt_notes.Text,
                    Age = txt_age.Text,
                };
                db.Patient.Add(newPatient);
                db.SaveChanges();
                var newAppoientment = new Appointments
                {
                    PatientID = newPatient.PatientID,
                    datetime = DateTime.Now,
                    Reason = txt_reseson.Text,
                    Status = cmb_status.SelectedItem.ToString(),
                };
                db.Add(newAppoientment);
                db.SaveChanges();
                MessageBox.Show("Appoientment Added SuccessFully");
                LoadData();
                return;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AllPatients.SelectedItem == null)
            {
                return;
            }
            dynamic ss = AllPatients.SelectedItem;
            txt_name.Text = ss.PName;
            txt_age.Text = ss.PAge;
            txt_phone.Text = ss.PPhone;
            txt_notes.Text = ss.pnote;
            txt_reseson.Text = ss.Apreseaon;
            cmb_status.SelectedItem = ss.status;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_name.Text) || string.IsNullOrEmpty(txt_notes.Text) || string.IsNullOrEmpty(txt_phone.Text) || string.IsNullOrEmpty(txt_reseson.Text) || cmb_status.SelectedItem == null)
            {
                MessageBox.Show("Please All Data Must Be Compelted");
                return;
            }
            using (var db = new MyDbcontext())
            {
                var CHECK = db.Patient.FirstOrDefault(t => t.Name == txt_name.Text);
                if (CHECK == null)
                {
                    MessageBox.Show("Patient Not Found");
                    return;
                }
                var Update = db.Appointment.FirstOrDefault(t => t.PatientID == CHECK.PatientID);
                if (Update == null)
                {
                    MessageBox.Show("Appoientment Not Found");
                    return;
                }
                Update.Patientss.Name = txt_name.Text;
                Update.Patientss.Phone = txt_phone.Text;
                Update.Patientss.Age = txt_age.Text;
                Update.Patientss.Notes = txt_notes.Text;
                Update.Status = cmb_status.SelectedValue.ToString();
                Update.Reason = txt_reseson.Text;
                db.SaveChanges();
                MessageBox.Show("Patient Updated Yet");
                LoadData();
                return;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_name.Text) || string.IsNullOrEmpty(txt_notes.Text) || string.IsNullOrEmpty(txt_phone.Text) || string.IsNullOrEmpty(txt_reseson.Text) || cmb_status.SelectedItem == null)
            {
                MessageBox.Show("Please All Data Must Be Compelted");
                return;
            }
            using (var db = new MyDbcontext())
            {
                var CHECK = db.Patient.FirstOrDefault(t => t.Name == txt_name.Text);
                if (CHECK == null)
                {
                    MessageBox.Show("Patient Not Found");
                    return;
                }
                var Delete = db.Appointment.FirstOrDefault(t => t.PatientID == CHECK.PatientID);
                if (Delete == null)
                {
                    MessageBox.Show("Appoientment Not Found");
                    return;
                }
                db.Patient.Remove(CHECK);
                db.Appointment.Remove(Delete);
                db.SaveChanges();
                MessageBox.Show("Appoientment Deleted yet");
                LoadData();
                return;

            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txt_search.Text))
            {
                MessageBox.Show("Please Enter Patient Name");
                return ;
            }
            using (var db=new MyDbcontext())
            {
                var checkpa = db.Patient.FirstOrDefault(t => t.Name == txt_search.Text);
                if(checkpa==null)
                {
                    MessageBox.Show("Patient Not Found");
                    return ;
                }
                var checkappo = db.Appointment.Include(t => t.Patientss).FirstOrDefault(t => t.Patientss.Name == txt_search.Text);
                if(checkappo==null)
                {
                    MessageBox.Show("Appoinetment Not Found");
                    return ;    
                }
                dg_search.ItemsSource= db.Appointment.Where(t=>t.AppointmentID== checkappo.AppointmentID).Include(p => p.Patientss).Select(t => new
                {
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
    }
}
