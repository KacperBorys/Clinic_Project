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
    /// Klasa Main
    /// </summary>
    public partial class MainWindow : Window
    {
        Placowka p;

        /// <summary>
        /// Kontruktor domyślny tworzący obiekt placowka.
        /// </summary>
        public MainWindow()
        {
            p = new();
            InitializeComponent();
        }

        /// <summary>
        /// Konstruktor pobierający argument placowka.
        /// </summary>
        /// <param name="placówka"></param>
        public MainWindow(Placowka placówka) : this()
        {
            p = placówka;
        }

        /// <summary>
        /// Przycisk przekierowujący do rejestracji pacjenta.
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
        /// Zmiana wartości comboboxa decyduje o występowaniu przycisku rejestracji konta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Profesja_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Jesli  profesja rozna od pacjenta to przycisk rejestracji znika. W przeciwnym razie pojawia sie
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
        /// Pokazuje hasło, jeśli przytrzymamy myszke na przycisku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();

        /// <summary>
        /// Chowa hasło, jeśli puscimy myszke z przycisku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();

        /// <summary>
        /// Chowa hasło, jeśli opuścimy pole przycisku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();

        /// <summary>
        /// Przycisk z menu, który umożliwia nam zapisanie pliku.
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
        /// Przycisk z menu, który umożliwia nam odczytanie pliku typu xml
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
        /// Funkcja służąca za pokazanie hasła i zmienienia ikonki przycisku.
        /// </summary>
        private void ShowPasswordFunction()
        {
            PasswordUnmask.Visibility = Visibility.Visible;
            PasswordHidden.Visibility = Visibility.Hidden;
            PasswordUnmask.Text = PasswordHidden.Password;
            Eye_Close.Source = new BitmapImage(new Uri(@"img/oko2.jpg", UriKind.Relative));
        }

        /// <summary>
        /// Funkcja służąca za schowanie hasła i zmienienia ikonki przycisku.
        /// </summary>

        private void HidePasswordFunction()
        {
            PasswordUnmask.Visibility = Visibility.Hidden;
            PasswordHidden.Visibility = Visibility.Visible;
            Eye_Close.Source = new BitmapImage(new Uri(@"/oko.png", UriKind.Relative));
        }

        /// <summary>
        /// Funcja służąca za zalogowanie się na dane konto. Jeśli logujący się wybrał ADMIN i wpisał hasło i login admina, to przekierowuje do okienka Przychodnia_ADMIN. Jeśli logujący wybrał pacjenta lub lekarza i wpisał dobre login i hasło
        /// dla niego, to przekierowywało odpowiednio do okienka Przychodnia_Pacjent lub Przychodnia_Doktor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Zaloguj_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Login_Text.Text.Length > 0 && PasswordHidden.Password.Length == 0)
            {
                MessageBox.Show("Pole hasło nie może być puste!");
            }
            else if (Login_Text.Text.Length == 0 && PasswordHidden.Password.Length > 0)
            {
                MessageBox.Show("Pole login nie może być puste!");
            }
            else if (Login_Text.Text.Length == 0 && PasswordHidden.Password.Length == 0)
            {
                MessageBox.Show("Proszę uzupełnić login i hasło");
            }
            else
            {
                string password = PasswordHidden.Password;
                if (Login_Text.Text == Admin.haslo && password.ToString() == Admin.haslo && Profesja.Text.ToString() == "ADMIN") //
                {
                    MessageBox.Show("Logowanie poprawne");
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
                                MessageBox.Show("Logowanie poprawne");
                                Przychodnia_Pacjent objSecondWindow = new Przychodnia_Pacjent(p, Login_Text.Text);
                                this.Visibility = Visibility.Hidden;
                                objSecondWindow.Show();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Złe hasło!");
                                return;
                            }
                        }
                        if (Profesja.Text.ToString() == "Doctor")
                        {

                            if (p.Lekarze.Find(p => p.Pesel == Login_Text.Text) != null)
                            {
                                MessageBox.Show("Logowanie poprawne");
                                Przychodnia_Doktor objSecondWindow = new Przychodnia_Doktor(p, Login_Text.Text, password.ToString(), "Lekarz");
                                this.Visibility = Visibility.Hidden;
                                objSecondWindow.Show();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Złe hasło!");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Złe hasło!");
                    }
                }
                else
                {
                    MessageBox.Show("Złe hasło!");
                }
            }
        }
        /// <summary>
        /// Po przyciśnięciu entera, przekierowuje nas do kolejnego pola
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
        /// Po przycisnieciu entera, przekierowuje nad do kolejnego pola.
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