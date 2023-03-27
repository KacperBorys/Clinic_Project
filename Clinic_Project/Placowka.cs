using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Project
{
    [DataContract]
    [KnownType(typeof(Pacjent))]
    [KnownType(typeof(Lekarz))]
    /// <summary>
    /// Klasa Placowka
    /// </summary>
    public class Placowka
    {
        List<Lekarz> lekarze;
        List<Pacjent> pacjenci;
        List<Wizyta> wizyty;
        TimeSpan godzinaOtwarcia;
        TimeSpan godzinaZamkniecia;
        Dictionary<string, string> konta;

        [DataMember]
        /// <summary>Właściwość GodzinaOtwarcia umożliwia dostęp do pola godzinaOtwarcia.
        /// </summary>
        public TimeSpan GodzinaOtwarcia { get => godzinaOtwarcia; set => godzinaOtwarcia = value; }
        [DataMember]
        /// <summary>Właściwość GodzinaZamkniecia umożliwia dostęp do pola godzinaZamkniecia.
        /// </summary>
        public TimeSpan GodzinaZamkniecia { get => godzinaZamkniecia; set => godzinaZamkniecia = value; }
        [DataMember]
        /// <summary>Właściwość Lekarze umożliwia dostęp do pola lekarze.
        /// </summary>
        public List<Lekarz> Lekarze { get => lekarze; set => lekarze = value; }
        [DataMember]
        /// <summary>Właściwość Wizyty umożliwia dostęp do pola wizyty.
        /// </summary>
        public List<Wizyta> Wizyty { get => wizyty; set => wizyty = value; }
        [DataMember]
        /// <summary>Właściwość Pacjenci umożliwia dostęp do pola pacjenci.
        /// </summary>
        public List<Pacjent> Pacjenci { get => pacjenci; set => pacjenci = value; }
        [DataMember]
        /// <summary>Właściwość Konta umożliwia dostęp do pola pacjenci.
        /// </summary>
        public Dictionary<string, string> Konta { get => konta; set => konta = value; }

        /// <summary>
        /// Konstruktor domyślny klasy<see cref="Placowka"/>.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Placowka()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Lekarze = new();
            Pacjenci = new();
            Wizyty = new();
            godzinaOtwarcia = new();
            godzinaZamkniecia = new();
            Konta = new();
        }
        /// <summary>
        /// Konstruktor parametryczny klasy <see cref="Placowka"/>.
        /// </summary>
        /// <param name="godzinaOtwarcia">Godzina otwarcia placówki</param>
        /// <param name="godzinaZamkniecia">Godzina zamknięcia placówki</param>
        public Placowka(TimeSpan godzinaOtwarcia, TimeSpan godzinaZamkniecia) : this()
        {
            GodzinaOtwarcia = godzinaOtwarcia;
            GodzinaZamkniecia = godzinaZamkniecia;
        }
        /// <summary>
        /// Konstruktor parametryczny klasy <see cref="Placowka"/>.
        /// </summary>
        /// <param name="lekarze">Lekarze w placówce</param>
        /// <param name="pacjenci">Pacjenci w placówce</param>
        /// <param name="wizyty">Wizyty</param>
        /// <param name="godzinaOtwarcia">Godzina otwarcia placówki</param>
        /// <param name="godzinaZamkniecia">Godzina zamknięcia placówki</param>
        public Placowka(List<Lekarz> lekarze, List<Pacjent> pacjenci, List<Wizyta> wizyty, TimeSpan godzinaOtwarcia, TimeSpan godzinaZamkniecia) : this(godzinaOtwarcia, godzinaZamkniecia)
        {
            Lekarze = lekarze;
            Pacjenci = pacjenci;
            Wizyty = wizyty;
        }
        /// <summary>
        /// Dodaje wizyte
        /// </summary>
        /// <param name="wizyta">Wizyta</param>
        public void DodajWizyte(Wizyta wizyta)
        {
            if (wizyta == null) { return; }
            if (wizyta.Lekarz.SprawdzCzyMoznaUmowic(wizyta.Data.ToShortDateString(), wizyta.Godzina))
            {
                wizyta.Lekarz.Zaplanowane_Wizyty.Add(new Tuple<DateTime, TimeSpan>(wizyta.Data, wizyta.Godzina), true);
                Wizyty.Add(wizyta);
            }
        }
        /// <summary>
        /// Kończy aktualnie trwającą wizytę i dodaje diagnozę.
        /// </summary>
        /// <param name="diagnoza">Diagnoza</param>
        public void ZakonczWizyte(Diagnoza diagnoza) //jak w WPF bedzie to idk czy to trzeba bedzie zmienic na inne argumenty
        {
            Wizyta w1 = diagnoza.Wizyta;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Pacjent p1 = Pacjenci.Find(p => p.Equals(w1.Pacjent));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            p1.DodajDiagnoze(diagnoza);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            Wizyty.Remove(w1);
        }
        /// <summary>
        /// Lekarz anuluje wizyte
        /// </summary>
        /// <param name="pesel">Pesel pacjenta</param>
        /// <param name="data">Data wizyty</param>
        /// <param name="godzina">Godzina wizyty</param>
        public void AnulujWizyteJakoLekarz(string pesel, DateTime data, TimeSpan godzina)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Wizyta wizyta = Wizyty.FirstOrDefault(w => w.Pacjent.Pesel == pesel && w.Data == data && w.Godzina == godzina);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (wizyta != null)
            {
                Wizyty.Remove(wizyta);
                wizyta.Lekarz.Zaplanowane_Wizyty.Remove(new Tuple<DateTime, TimeSpan>(data, godzina));
            }
        }

        /// <summary>
        /// Pacjent anuluje wizyte
        /// </summary>
        /// <param name="pesel">Pesel pacjenta</param>
        /// <param name="data">Data wizyty</param>
        /// <param name="godzina">Godzina wizyty</param>
        public void AnulujWizytePacjent(string pesel, DateTime data, TimeSpan godzina)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Wizyta wizyta = Wizyty.FirstOrDefault(w => w.Pacjent.Pesel == pesel && w.Data == data && w.Godzina == godzina);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (wizyta != null)
            {
                Wizyty.Remove(wizyta);
                wizyta.Lekarz.Zaplanowane_Wizyty.Remove(new Tuple<DateTime, TimeSpan>(data, godzina));
            }
        }
        /// <summary>
        /// Dodaje pacjenta
        /// </summary>
        /// <param name="p1">Pacjent</param>
        public void DodajPacjenta(Pacjent p1)
        {
            if (p1 == null) { return; }
            Pacjenci.Add(p1);
        }
        /// <summary>
        /// Dodaje konto
        /// </summary>
        /// <param name="pesel">Pesel przypisany do konta</param>
        /// <param name="haslo">Hasło przypisane do konta</param>
        public void DodajKonto(string pesel, string haslo)
        {
            if (pesel == null || haslo == null) { return; }
            Konta.Add(pesel, haslo);
        }
        /// <summary>
        /// Dodaje lekarza
        /// </summary>
        /// <param name="l1">Lekarz</param>
        public void DodajLekarza(Lekarz l1)
        {
            if (l1 == null || Lekarze.Find(p => p.Pesel == l1.Pesel) != null) { return; }
            Lekarze.Add(l1);
        }
        /// <summary>
        /// Usuwa lekarza
        /// </summary>
        /// <param name="pesel">Pesel lekarza do usunięcia</param>
        public void UsunLekarza(string pesel)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Lekarz l1 = Lekarze.Find(p => p.Pesel == pesel);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (Lekarze.Find(p => p.Pesel == pesel) == null) { return; }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Wizyty.RemoveAll(p => p.Lekarz.Pesel == l1.Pesel);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
            Lekarze.Remove(l1);
#pragma warning restore CS8604 // Possible null reference argument.
        }
        /// <summary>
        /// Usuwa pacjenta
        /// </summary>
        /// <param name="pesel">Pesel pacjenta do usunięcia</param>
        public void UsuńPacjenta(string pesel)
        {
            List<Wizyta> wizytyanulowane = new List<Wizyta>();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Pacjent p1 = Pacjenci.Find(p => p.Pesel == pesel);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (Pacjenci.Find(p => p.Pesel == pesel) == null) { return; }
            wizytyanulowane = Wizyty.FindAll(p => p.Pacjent.Pesel == pesel);

            foreach (Wizyta w in wizytyanulowane)
            {
                w.Lekarz.Zaplanowane_Wizyty.Remove(new Tuple<DateTime, TimeSpan>(w.Data, w.Godzina));
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Wizyty.RemoveAll(p => p.Pacjent.Pesel == p1.Pesel);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            Konta.Remove(pesel);
#pragma warning disable CS8604 // Possible null reference argument.
            Pacjenci.Remove(p1);
#pragma warning restore CS8604 // Possible null reference argument.
        }
        /// <summary>
        /// Wyszukuje historię pacjenta
        /// </summary>
        /// <param name="pesel">Pesel pacjenta</param>
        /// <returns>Historia pacjenta w placówce</returns>
        public string HistoriaPacjenta(string pesel)
        {
            StringBuilder sb = new();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Pacjent pacjent = Pacjenci.Find(p => p.Pesel == pesel);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (pacjent == null) { return "Brak pacjenta w bazie danych."; }
            pacjent.HistoriaWizyt.ForEach(w => sb.AppendLine(w.ToString()));
            if (sb.ToString() == null) { return "Brak historii"; }
            return sb.ToString();
        }
        /// <summary>
        /// Znajduje wszystkie wizyty lekarza w danym dniu
        /// </summary>
        /// <param name="pesel">Pesel lekarza</param>
        /// <param name="data">Data wizyty lekarza</param>
        /// <returns>Lista wizyt</returns>
        public List<Wizyta> LekarzWDanymDniu(string pesel, DateTime data)
        {

            List<Wizyta> wizytyulekarza = Wizyty.FindAll(w => w.Lekarz.Pesel == pesel && w.Data.Date == data);
            return wizytyulekarza;
        }
        /// <summary>
        /// Wszystkie wizyty
        /// </summary>
        /// <returns>Lista wszystkich wizyt</returns>
        public List<Wizyta> WszystkieWizyty()
        {
            return Wizyty;
        }
        /// <summary>
        /// Wizyty pacjenta
        /// </summary>
        /// <param name="pesel">Pesel pacjenta</param>
        /// <returns>Lista wizyt pacjenta o podanym peselu</returns>
        public List<Wizyta> WizytyPacjenta(string pesel)
        {
            List<Wizyta> wizyty = new();
            wizyty = Wizyty.FindAll(p => p.Pacjent.Pesel == pesel);
            return wizyty;
        }
        /// <summary>
        /// Zapisanie do pliku XML
        /// </summary>
        /// <param name="fname">Nazwa pliku</param>
        public void ZapiszDC(string fname)
        {
            using FileStream fs = new(fname, FileMode.Create);
            DataContractSerializer dc = new(typeof(Placowka));
            dc.WriteObject(fs, this);
        }
        /// <summary>
        /// Odczytanie pliku XML
        /// </summary>
        /// <param name="fname">Nazwa pliku</param>
        /// <returns></returns>
        public static Placowka? OdczytDC(string fname)
        {
            if (!File.Exists(fname)) { return null; }
            using FileStream fs = new(fname, FileMode.Open);
            DataContractSerializer dc = new(typeof(Placowka));
            return dc.ReadObject(fs) as Placowka;
        }
        /// <summary>
        /// Sortuje wizyty
        /// </summary>
        public void SortujWizyta()
        {
            Wizyty.Sort();
        }
        /// <summary>
        /// Wyszukuje specjalizacje
        /// </summary>
        /// <param name="specjalizacja">Nazwa specjalizacji</param>
        /// <returns>Lista lekarzy o podanej specjalizacji</returns>
        public List<Lekarz> WyszukajSpecjalizacja(string specjalizacja)
        {
            return Lekarze.FindAll(p => p.Specjalizacja.Equals(specjalizacja));
        }
        /// <summary>
        /// Wyszukuje wszystkie wizyty danego lekarza
        /// </summary>
        /// <param name="pesel">Pesel lekarza</param>
        /// <returns>Lista wszystkich wizyt danego lekarza</returns>
        public List<Wizyta> WszystkieWizytyDanegoLekarza(string pesel)
        {
            return Wizyty.FindAll(p => p.Lekarz.Pesel == pesel);
        }
        /// <summary>
        /// Wszystkie wizyty danej osoby u danego lekarza.
        /// </summary>
        /// <param name="pesellekarza">Pesel lekarza</param>
        /// <param name="peselpacjenta">Pesel pacjenta</param>
        /// <returns>Lista wszystkich wizyt danej osoby u konkretnego lekarza</returns>
        public List<Wizyta> WszystkieWizytyDanejOsobyUDanegoLekarza(string pesellekarza, string peselpacjenta)
        {
            List<Wizyta> w = WszystkieWizytyDanegoLekarza(pesellekarza);
#pragma warning disable CS8603 // Possible null reference return.
            if (w == null) { return null; }
#pragma warning restore CS8603 // Possible null reference return.
            return w.FindAll(p => p.Pacjent.Pesel == peselpacjenta);

        }
        /// <summary>
        /// Dodaje konto pacjenta jeżel nie jest jeszcze zarejestrowany
        /// </summary>
        /// <param name="pesel">Pesel pacjenta</param>
        /// <param name="haslo">Hasło do konta pacjenta</param>
        /// <returns>true jeżeli pacjent nie ma jeszcze konta lub false jeżeli pacjent posiada już konto</returns>
        public bool HasloRejestracjaPacjent(string pesel, string haslo)
        {
            if (Konta.ContainsKey(pesel))
            {
                if (Pacjenci.FindAll(p => p.Pesel == pesel).Count == 0)
                {
                    return true;
                }
                return false;
            }
            Konta.Add(pesel, haslo);
            return true;
        }
    }
}