using Clinic_Project;
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

namespace Clinic_GUI
{
    /// <summary>
    /// In the Change_Password class, we create the fields Placowka p and the fields Pacjent ZalogowanyPacjent and ZalogowanyLekarz. We instantiate a Patient and a Doctor.
    /// </summary>
    public partial class Zmiana_Hasla : Window
    {
        Placowka p = new();
        Pacjent ZalogowanyPacjent = new();
        Lekarz ZalogowanyLekarz = new();

        /// <summary>
        /// Non-parametric constructor creating an instance of the facility and loading the compiled page.
        /// </summary>
        public Zmiana_Hasla()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Parametric constructor with arguments "placowka" (facility) and "pacjent" (patient). If a doctor wants to change their password, we assign their object to the logged-in doctor,
        /// and if it's a patient, we assign the patient to the logged-in patient.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="pacjent"></param>
        public Zmiana_Hasla(Placowka p, Osoba pacjent) : this()
        {
            this.p = p;
            if (pacjent is Lekarz)
            {
                ZalogowanyLekarz = pacjent as Lekarz;
            }
            else
            {
                ZalogowanyPacjent = pacjent as Pacjent;
            }
        }

        /// <summary>
        /// Function responsible for the final password change for a patient or doctor only if all conditions are met. In case the conditions are not met,
        /// appropriate messages are displayed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (OldPassword.Password == "" || NewPassword.Password == "" || RepeatPassword.Password == "")
            {
                MessageBox.Show("Please fill all fields!", "Error");
            }
            else if (NewPassword.Password != RepeatPassword.Password)
            {
                MessageBox.Show("The new passwords are different!", "Error");
            }
            else
            {
                if (ZalogowanyLekarz.Imie == string.Empty)
                {
                    OldPassword.Password = p.Konta[ZalogowanyPacjent.Pesel];
                    if (OldPassword.Password.ToString() == p.Konta[ZalogowanyPacjent.Pesel])
                    {
                        p.Konta[ZalogowanyPacjent.Pesel] = NewPassword.Password.ToString();
                        MessageBox.Show("Password has been changed!", "Success");
                        Przychodnia_Pacjent objSecondWindow = new Przychodnia_Pacjent(p, ZalogowanyPacjent.Pesel);
                        this.Visibility = Visibility.Hidden;
                        objSecondWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("The password has been changed!", "Success");
                    }
                }
                else
                {
                    OldPassword.Password = p.Konta[ZalogowanyLekarz.Pesel];
                    if (OldPassword.Password.ToString() == p.Konta[ZalogowanyLekarz.Pesel])
                    {
                        p.Konta[ZalogowanyLekarz.Pesel] = NewPassword.Password.ToString();
                        MessageBox.Show("Password has been changed!", "Success");
                        Przychodnia_Doktor objSecondWindow = new Przychodnia_Doktor(p, ZalogowanyLekarz.Pesel, NewPassword.ToString(), "Lekarz");
                        this.Visibility = Visibility.Hidden;
                        objSecondWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("The old password is incorrect!", "Error");
                    }
                }
            }
        }
        /// <summary>
        /// Function responsible for returning to the patient or doctor account window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (ZalogowanyLekarz.Imie != string.Empty)
            {
                Przychodnia_Doktor objSecondWindow = new Przychodnia_Doktor(p, ZalogowanyLekarz.Pesel, NewPassword.ToString(), "Lekarz");
                this.Visibility = Visibility.Hidden;
                objSecondWindow.Show();
            }
            else
            {
                Przychodnia_Pacjent objSecondWindow = new Przychodnia_Pacjent(p, ZalogowanyPacjent.Pesel, NewPassword.ToString(), "Pacjent");
                this.Visibility = Visibility.Hidden;
                objSecondWindow.Show();
            }
        }
        /// <summary>
        /// Function that allows jumping to the next text field after pressing the Enter key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NewPassword.Focus();
            }
        }
        /// <summary>
        /// Function that allows jumping to the next text field after pressing the Enter key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTextBox_KeyDown2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RepeatPassword.Focus();
            }
        }
        /// <summary>
        /// Function that allows jumping to the next text field after pressing the Enter key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTextBox_KeyDown3(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ChangePassword_Click(sender, e);
            }
        }
    }
}