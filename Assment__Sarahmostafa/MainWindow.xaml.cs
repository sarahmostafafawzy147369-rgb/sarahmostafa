using Assment__Sarahmostafa.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assment__Sarahmostafa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_name.Text) || string.IsNullOrEmpty(txt_pass.Text))
            {
                MessageBox.Show("Please Insert All Data");
                return;
            }
            using (var db = new MyDbcontext())
            {
                var check = db.Users.FirstOrDefault(t => t.Name == txt_name.Text && t.Password == txt_pass.Text);
                if (check == null)
                {
                    MessageBox.Show("User Not Found ");
                    return;
                }
                if (check.Role == "Receptionist")
                {
                    ReceptionistWindow receptionistWindow=new ReceptionistWindow();
                    receptionistWindow.Show();
                }
                if(check.Role== "Doctor")
                {
                    DoctorWindow doctorWindow=new DoctorWindow();   
                    doctorWindow.Show();    
                }

            }
        }
    }
}