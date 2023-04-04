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
    /// W klasie Zmiana_Hasla tworzymy pola Placowka p oraz pole Pacjent ZalogowanyPacjent jak i ZalogowanyLekarz. Tworzymy istancję Pacjenta oraz Lekarza.
    /// </summary>
    public partial class Zmiana_Hasla : Window
    {
        Placowka p = new();
        Pacjent ZalogowanyPacjent = new();
        Lekarz ZalogowanyLekarz = new();

        /// <summary>
        ///  Konstruktor nieparametryczny tworzący istancję placowki, ładuję skompilowaną stronę.
        /// </summary>
        public Zmiana_Hasla()
        {
            InitializeComponent();
        }
        /// <summary>
        /// KOnstruktor parametryczny posiadający argumenty placowka oraz pacjent. Jeżeli Lekarz chce zmienić hasło to przypisujemy jego obiekt do
        /// zalogowanego lekarza, natomiast kiedy jest to pacjent to przypisujemy pacjenta do zalogowanego pacjenta.
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
        /// Funkcja odpowiedzialna za końcową zmianę hasła dla pacjenta lub lekarza tylko wtedy gdy wszystkie warunki są spełnione. W przypadku niespełnienia warunków
        /// dostajemy odpowiednie komunikaty.
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
        /// Funkcja odpowiedzialna za powrót do okna konta pacjenta lub lekarza.
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
        /// Funnkcja dzięki, ktorej po naciśnięciu entera kursor przeskakuje do kolejnego pola tekstowego.
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
        /// Funnkcja dzięki, ktorej po naciśnięciu entera kursor przeskakuje do kolejnego pola tekstowego.
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
        /// Funnkcja dzięki, ktorej po naciśnięciu entera kursor przeskakuje do kolejnego pola tekstowego.
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