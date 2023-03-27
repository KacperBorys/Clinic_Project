using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;
using System;
using Clinic_Project;

namespace Clinic_GUI
{

    /// <summary>
    /// W klasie Tworzenie_Konta_Pacjenta tworzymy pole Placowka p i tworzymy jej instancję.
    /// </summary>
    public partial class Tworzenie_Konta_Pacjenta : Window
    {
        Placowka p;

        /// <summary>
        /// Konstruktor nieparametryczny konstruktor.
        /// </summary>
        public Tworzenie_Konta_Pacjenta()
        {
            p = new();
            InitializeComponent();
        }

        /// <summary>
        /// Konstruktor parametryczny zawierający argument plaówka. Przypisuje go do p.
        /// </summary>
        /// <param name="placowka"></param>
        public Tworzenie_Konta_Pacjenta(Placowka placowka) : this()
        {
            p = placowka;
        }

        /// <summary>
        /// Funckja powodująca zresetowanie wartości wpisanych przez użytkownika i przypisanie konkretnym polom pewnych wartości.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Imie.Text = "Jan";
            Nazwisko.Text = "Kowalski";
            Pesel.Text = "69463683526";
            Data_Urodzenia.Text = "08.06.1975";
            Plec.Text = "Man";
            PasswordHidden.Password = "";
        }

        /// <summary>
        /// Funkcja umożliwiająca powrót do okienka logowania.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WrocButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objSecondWindow = new MainWindow(p);
            this.Visibility = Visibility.Hidden;
            objSecondWindow.Show();
        }

        /// <summary>
        /// Funkcja umożliwiająca usuwanie wartości z pola tekstowego poprzez podwójne kliknięcie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Imie(object sender, RoutedEventArgs e)
        {
            Imie.Text = "";
        }

        /// <summary>
        /// Funkcja umożliwiająca usuwanie wartości z pola tekstowego poprzez podwójne kliknięcie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Click_Nazwisko(object sender, RoutedEventArgs e)
        {
            Nazwisko.Text = "";
        }

        /// <summary>
        /// Funkcja umożliwiająca usuwanie wartości z pola tekstowego poprzez podwójne kliknięcie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DataUrodzenia(object sender, RoutedEventArgs e)
        {
            Data_Urodzenia.Text = "";
        }

        /// <summary>
        /// Funkcja umożliwiająca usuwanie wartości z pola tekstowego poprzez podwójne kliknięcie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Pesel(object sender, RoutedEventArgs e)
        {
            Pesel.Text = "";
        }

        /// <summary>
        /// Funkcja umożliwiająca usuwanie wartości z pola tekstowego poprzez podwójne kliknięcie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Haslo(object sender, RoutedEventArgs e)
        {
            PasswordHidden.Password = "";
        }
        /// <summary>
        /// Funkcja powodująca utworzenie konta jeśli wprowadzone przez nas dane spełniają określone warunki.Pola nie mogą być puste iraz nie może już istnieś konto o danym peselu.
        /// W przypadku błędów pojawi się odpowiedni komunikat. Po prawidłowym utworzeniu konta zostaniemy przekirowani do okienka logowania.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZapiszButton_Click(object sender, RoutedEventArgs e)
        {

            if (Imie.Text.Length > 0 && Nazwisko.Text.Length > 0 && Data_Urodzenia.Text.Length > 0 && Plec.Text.Length > 0 && Pesel.Text.Length > 0 && PasswordHidden.Password.Length > 0)
            {
                if (!DateTime.TryParseExact(Data_Urodzenia.Text,
                new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy" ,"yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd"
                }, null, System.Globalization.DateTimeStyles.None,
                out DateTime res))
                {
                    MessageBox.Show("Wrong data format");
                    return;
                }
                if (res > DateTime.Now)
                {
                    MessageBox.Show("Wrong date format");
                    return;
                }
                if (!Regex.IsMatch(Pesel.Text, @"^\d{11}$"))
                {
                    MessageBox.Show("Wrong Pesel format.");
                    return;
                }
                if (Plec.Text == "Woman")
                {
                    Pacjent p1 = new Pacjent(Imie.Text, Nazwisko.Text, Data_Urodzenia.Text, Pesel.Text, EnumPlec.K);
                    if (p.Lekarze.Find(lek => lek.Pesel == p1.Pesel) == null)
                    {
                        if (p.HasloRejestracjaPacjent(Pesel.Text, PasswordHidden.Password))
                        {
                            MessageBox.Show("Added successfully");
                            p.DodajPacjenta(p1);
                            MainWindow objSecondWindow = new MainWindow(p);
                            this.Visibility = Visibility.Hidden;
                            objSecondWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong");
                            return;
                        }

                    }

                    else if (p.Pacjenci.Find(pac => pac.Pesel == Pesel.Text) != null)
                    {
                        MessageBox.Show("Account for this PESEL already exists.");
                        return;
                    }

                    else
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        Lekarz lek = p.Lekarze.Find(lek => lek.Pesel == p1.Pesel);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        string haslo = p.Konta[p1.Pesel];

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        if (lek.Imie == p1.Imie && lek.Nazwisko == p1.Nazwisko && lek.DataUrodzenia == p1.DataUrodzenia && lek.Plec == p1.Plec && haslo == PasswordHidden.Password)
                        {
                            MessageBox.Show("Added successfully");
                            p.DodajPacjenta(p1);
                            MainWindow objSecondWindow = new MainWindow(p);
                            this.Visibility = Visibility.Hidden;
                            objSecondWindow.Show();
                        }

                        else
                        {
                            MessageBox.Show("You must fill the exact doctor values.");
                            return;
                        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }
                }
                else
                {
                    Pacjent p2 = new Pacjent(Imie.Text, Nazwisko.Text, Data_Urodzenia.Text, Pesel.Text, EnumPlec.M);
                    if (p.Lekarze.Find(lek => lek.Pesel == p2.Pesel) == null)
                    {
                        if (p.HasloRejestracjaPacjent(Pesel.Text, PasswordHidden.Password))
                        {
                            MessageBox.Show("Added successfully");
                            p.DodajPacjenta(p2);
                            MainWindow objSecondWindow = new MainWindow(p);
                            this.Visibility = Visibility.Hidden;
                            objSecondWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong");
                            return;
                        }

                    }

                    else if (p.Pacjenci.Find(pac => pac.Pesel == Pesel.Text) != null)
                    {
                        MessageBox.Show("Account for this PESEL already exists.");
                        return;
                    }

                    else
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        Lekarz lek = p.Lekarze.Find(lek => lek.Pesel == p2.Pesel);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        string haslo = p.Konta[p2.Pesel];

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        if (lek.Imie == p2.Imie && lek.Nazwisko == p2.Nazwisko && lek.DataUrodzenia == p2.DataUrodzenia && lek.Plec == p2.Plec && haslo == PasswordHidden.Password)
                        {
                            MessageBox.Show("Added successfully");
                            p.DodajPacjenta(p2);
                            MainWindow objSecondWindow = new MainWindow(p);
                            this.Visibility = Visibility.Hidden;
                            objSecondWindow.Show();
                        }

                        else
                        {
                            MessageBox.Show("You must fill the exact doctor values.");
                            return;
                        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }
                }
            }
            else
            {
                MessageBox.Show("Fill the fields");
            }
        }
        /// <summary>
        /// Funkcja wywołująca w sobie funkcję ShowPasswordFunction() po nakliknięciu przycisku myszy na obrazek oka.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();

        /// <summary>
        /// Funkcja wywołująca w sobie funkcję ShowPasswordFunction() po odkliknięciu przycisku myszy na obrazek oka.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();

        /// <summary>
        /// Funkcja wywołująca w sobie funkcję ShowPasswordFunction() kiedy kursor jest poza obrazkiem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();

        /// <summary>
        /// Funkcja, dzięki, której jesteśmy w stanie zobaczyć wpisane przez nas haslo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ShowPasswordFunction()
        {
            PasswordUnmask.Visibility = Visibility.Visible;
            PasswordHidden.Visibility = Visibility.Hidden;
            PasswordUnmask.Text = PasswordHidden.Password;
            Eye_Close.Source = new BitmapImage(new Uri(@"/oko2.jpg", UriKind.Relative));
        }
        /// <summary>
        /// Funkcja, dzięki, której nasze wpisane hasło jest zamaskowane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void HidePasswordFunction()
        {
            PasswordUnmask.Visibility = Visibility.Hidden;
            PasswordHidden.Visibility = Visibility.Visible;
            Eye_Close.Source = new BitmapImage(new Uri(@"/oko.png", UriKind.Relative));
        }

        /// <summary>
        /// Funnkcja dzięki, ktorej po naciśnięciu entera kursor przeskakuje do kolejnego pola tekstowego.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTextBox_KeyDown1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Nazwisko.Focus();
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
                Data_Urodzenia.Focus();
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
                Pesel.Focus();
            }
        }

        /// <summary>
        /// Funnkcja dzięki, ktorej po naciśnięciu entera kursor przeskakuje do kolejnego pola tekstowego.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTextBox_KeyDown4(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Plec.Focus();
            }
        }

        /// <summary>
        /// Funnkcja dzięki, ktorej po naciśnięciu entera kursor przeskakuje do kolejnego pola tekstowego.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTextBox_KeyDown5(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PasswordHidden.Focus();
            }
        }

        /// <summary>
        /// Funnkcja dzięki, ktorej po naciśnięciu entera zostaje wykonana funkcja ZApiszButton_CLick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTextBox_KeyDown6(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ZapiszButton_Click(sender, e);
            }
        }
    }
}