using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;
using System;
using Clinic_Project;

namespace Clinic_GUI
{

    /// <summary>
    /// In the class Tworzenie_Konta_Pacjenta we create a field Placowka p and create its instance.
    /// </summary>
    public partial class Tworzenie_Konta_Pacjenta : Window
    {
        Placowka p;

        /// <summary>
        /// Non-parameterized constructor.
        /// </summary>
        public Tworzenie_Konta_Pacjenta()
        {
            p = new();
            InitializeComponent();
        }

        /// <summary>
        /// Parameterized constructor that takes a Placowka argument and assigns it to p.
        /// </summary>
        /// <param name="placowka"></param>
        public Tworzenie_Konta_Pacjenta(Placowka placowka) : this()
        {
            p = placowka;
        }

        /// <summary>
        /// Function that resets user input values and assigns certain values to specific fields.
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
        /// Function that allows the user to return to the login window.
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
        /// Function that allows the user to delete values from a text field by double-clicking.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Imie(object sender, RoutedEventArgs e)
        {
            Imie.Text = "";
        }

        /// <summary>
        /// Function that allows the user to delete values from a text field by double-clicking.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Click_Nazwisko(object sender, RoutedEventArgs e)
        {
            Nazwisko.Text = "";
        }

        /// <summary>
        /// Function that allows the user to delete values from a text field by double-clicking.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DataUrodzenia(object sender, RoutedEventArgs e)
        {
            Data_Urodzenia.Text = "";
        }

        /// <summary>
        /// Function that allows the user to delete values from a text field by double-clicking.
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
        /// Function that creates an account if the data entered by the user meets certain conditions. Fields cannot be empty and there cannot already be an account with the entered PESEL number. In case of errors, an appropriate message will appear. After creating an account correctly, the user will be redirected to the login window.
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
                    MessageBox.Show("Wrong date format!");
                    return;
                }
                if (res > DateTime.Now)
                {
                    MessageBox.Show("Wrong date format!", "Error");
                    return;
                }
                if (!Regex.IsMatch(Pesel.Text, @"^\d{11}$"))
                {
                    MessageBox.Show("Wrong Pesel format!", "Error");
                    return;
                }
                if (Plec.Text == "Woman")
                {
                    Pacjent p1 = new Pacjent(Imie.Text, Nazwisko.Text, Data_Urodzenia.Text, Pesel.Text, EnumPlec.K);
                    if (p.Lekarze.Find(lek => lek.Pesel == p1.Pesel) == null)
                    {
                        if (p.HasloRejestracjaPacjent(Pesel.Text, PasswordHidden.Password))
                        {
                            MessageBox.Show("Added successfully!", "Success");
                            p.DodajPacjenta(p1);
                            MainWindow objSecondWindow = new MainWindow(p);
                            this.Visibility = Visibility.Hidden;
                            objSecondWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong!", "Error");
                            return;
                        }
                    }
                    else if (p.Pacjenci.Find(pac => pac.Pesel == Pesel.Text) != null)
                    {
                        MessageBox.Show("Account for this Pesel already exists!", "Error");
                        return;
                    }
                    else
                    {
                        #pragma warning disable CS8600 
                        Lekarz lek = p.Lekarze.Find(lek => lek.Pesel == p1.Pesel);
                        #pragma warning restore CS8600 
                        string haslo = p.Konta[p1.Pesel];

                        #pragma warning disable CS8602 
                        if (lek.Imie == p1.Imie && lek.Nazwisko == p1.Nazwisko && lek.DataUrodzenia == p1.DataUrodzenia && lek.Plec == p1.Plec && haslo == PasswordHidden.Password)
                        {
                            MessageBox.Show("Added successfully!", "Success");
                            p.DodajPacjenta(p1);
                            MainWindow objSecondWindow = new MainWindow(p);
                            this.Visibility = Visibility.Hidden;
                            objSecondWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("You must fill the exact doctor values!", "Error");
                            return;
                        }
                        #pragma warning restore CS8602 
                    }
                }
                else
                {
                    Pacjent p2 = new Pacjent(Imie.Text, Nazwisko.Text, Data_Urodzenia.Text, Pesel.Text, EnumPlec.M);
                    if (p.Lekarze.Find(lek => lek.Pesel == p2.Pesel) == null)
                    {
                        if (p.HasloRejestracjaPacjent(Pesel.Text, PasswordHidden.Password))
                        {
                            MessageBox.Show("Added successfully!", "Success");
                            p.DodajPacjenta(p2);
                            MainWindow objSecondWindow = new MainWindow(p);
                            this.Visibility = Visibility.Hidden;
                            objSecondWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong!", "Error");
                            return;
                        }
                    }
                    else if (p.Pacjenci.Find(pac => pac.Pesel == Pesel.Text) != null)
                    {
                        MessageBox.Show("Account for this Pesel already exists!", "Error");
                        return;
                    }
                    else
                    {
                        #pragma warning disable CS8600 
                        Lekarz lek = p.Lekarze.Find(lek => lek.Pesel == p2.Pesel);
                        #pragma warning restore CS8600 
                        string haslo = p.Konta[p2.Pesel];

                        #pragma warning disable CS8602 
                        if (lek.Imie == p2.Imie && lek.Nazwisko == p2.Nazwisko && lek.DataUrodzenia == p2.DataUrodzenia && lek.Plec == p2.Plec && haslo == PasswordHidden.Password)
                        {
                            MessageBox.Show("Added successfully!", "Success");
                            p.DodajPacjenta(p2);
                            MainWindow objSecondWindow = new MainWindow(p);
                            this.Visibility = Visibility.Hidden;
                            objSecondWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("You must fill the exact doctor values!", "Error");
                            return;
                        }
                        #pragma warning restore CS8602
                    }
                }
            }
            else
            {
                MessageBox.Show("Fill the fields!", "Error");
            }
        }
        /// <summary>
        /// Function that calls the ShowPasswordFunction() function when the mouse button is clicked on the eye image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();

        /// <summary>
        /// Function that calls the ShowPasswordFunction() function when the mouse button is clicked on the eye image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();

        /// <summary>
        /// Function that calls the ShowPasswordFunction() function when the cursor is outside the image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();

        /// <summary>
        /// Function that allows us to see the password we have entered.
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
        /// Function that masks our entered password.
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
        /// Function that allows the cursor to move to the next text field after pressing Enter.
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
        /// Function that allows the cursor to move to the next text field after pressing Enter.
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
        /// Function that allows the cursor to move to the next text field after pressing Enter.
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
        /// Function that triggers the ZapiszButton_Click function when the enter key is pressed.
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