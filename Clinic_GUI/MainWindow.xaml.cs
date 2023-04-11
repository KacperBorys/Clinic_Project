using Clinic_Project;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clinic_GUI
{
    /// <summary>
    /// Main class
    /// </summary>
    public partial class MainWindow : Window
    {
        Placowka p;

        /// <summary>
        /// Default constructor creating an object of the Institution class.
        /// </summary>
        public MainWindow()
        {
            p = new();
            InitializeComponent();
        }

        /// <summary>
        /// Constructor that takes an argument of type Institution.
        /// </summary>
        /// <param name="institution"></param>
        public MainWindow(Placowka placówka) : this()
        {
            p = placówka;
        }

        /// <summary>
        /// Button that redirects to patient registration.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZarejestrujSie_Button_Click(object sender, RoutedEventArgs e)
        {
            Tworzenie_Konta_Pacjenta objSecondWindow = new Tworzenie_Konta_Pacjenta(p);
            this.Visibility = Visibility.Hidden;
            objSecondWindow.Show();
        }

        /// <summary>
        /// Changing the value of the combobox determines the presence of the account registration button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Profesja_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If the profession is different from the patient, the registration button disappears. Otherwise, it appears.
            if (Profesja.SelectedIndex != 1)
            {
                ZarejestrujSie_Button.Visibility = Visibility.Hidden;
            }
            else
            {
                ZarejestrujSie_Button.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Shows the password if we hold the mouse on the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();

        /// <summary>
        /// Hides the password when we release the mouse from the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();

        /// <summary>
        /// Hides the password when we leave the button field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();

        /// <summary>
        /// A button from the menu that allows us to save a file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuZapisz_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                int ad = p.Konta.Count;
                p.ZapiszDC(filename);        
            }
        }

        /// <summary>
        /// Menu button that allows us to read an XML file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuOtworz_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                p = Placowka.OdczytDC(filename);
            }
        }
        /// <summary>
        /// Function used to show a password and change the button icon.
        /// </summary>
        private void ShowPasswordFunction()
        {
            PasswordUnmask.Visibility = Visibility.Visible;
            PasswordHidden.Visibility = Visibility.Hidden;
            PasswordUnmask.Text = PasswordHidden.Password;
            Eye_Close.Source = new BitmapImage(new Uri(@"img/oko2.jpg", UriKind.Relative));
        }
        /// <summary>
        /// Function used to hide a password and change the button icon.
        /// </summary>

        private void HidePasswordFunction()
        {
            PasswordUnmask.Visibility = Visibility.Hidden;
            PasswordHidden.Visibility = Visibility.Visible;
            Eye_Close.Source = new BitmapImage(new Uri(@"/oko.png", UriKind.Relative));
        }

        /// <summary>
        /// Function used for logging in to a specific account. If the user selects ADMIN and enters the admin's login and password, it redirects to the Przychodnia_ADMIN window. If the user selects a patient or doctor and enters the correct login and password for that account, it redirects respectively to the Przychodnia_Pacjent or Przychodnia_Doktor window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Zaloguj_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Login_Text.Text.Length > 0 && PasswordHidden.Password.Length == 0)
            {
                MessageBox.Show("The password field cannot be empty!", "Error");
            }
            else if (Login_Text.Text.Length == 0 && PasswordHidden.Password.Length > 0)
            {
                MessageBox.Show("The login field cannot be empty!", "Error");
            }
            else if (Login_Text.Text.Length == 0 && PasswordHidden.Password.Length == 0)
            {
                MessageBox.Show("Please fill your login and password!", "Error");
            }
            else
            {
                string password = PasswordHidden.Password;
                if (Login_Text.Text == Admin.haslo && password.ToString() == Admin.haslo && Profesja.Text.ToString() == "ADMIN") //
                {
                    MessageBox.Show("Login successful!", "Success");
                    Przychodnia_ADMIN objSecondWindow = new Przychodnia_ADMIN(p);
                    this.Visibility = Visibility.Hidden;
                    objSecondWindow.Show();
                }

                else if (p.Konta.ContainsKey(Login_Text.Text))
                {
                    if (p.Konta[Login_Text.Text] == PasswordUnmask.Text.ToString() || p.Konta[Login_Text.Text] == password.ToString())
                    {
                        if (Profesja.Text.ToString() == "Patient")
                        {
                            if (p.Pacjenci.Find(p => p.Pesel == Login_Text.Text) != null)
                            {
                                MessageBox.Show("Login successful!", "Success");
                                Przychodnia_Pacjent objSecondWindow = new Przychodnia_Pacjent(p, Login_Text.Text);
                                this.Visibility = Visibility.Hidden;
                                objSecondWindow.Show();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Wrong password!", "Error");
                                return;
                            }
                        }
                        if (Profesja.Text.ToString() == "Doctor")
                        {

                            if (p.Lekarze.Find(p => p.Pesel == Login_Text.Text) != null)
                            {
                                MessageBox.Show("Login successful!", "Success");
                                Przychodnia_Doktor objSecondWindow = new Przychodnia_Doktor(p, Login_Text.Text, password.ToString(), "Lekarz");
                                this.Visibility = Visibility.Hidden;
                                objSecondWindow.Show();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Wrong password!", "Error");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wrong password!", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Wrong password!", "Error");
                }
            }
        }
        /// <summary>
        /// After pressing the Enter key, redirects us to the next field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PasswordHidden.Focus();
            }
        }

        /// <summary>
        /// After pressing the Enter key, redirects us to the next field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTextBox_KeyDown2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Zaloguj_Button_Click(sender, e);
            }
        }
    }
}