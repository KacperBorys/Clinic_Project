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
    /// Class Placowka
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
        /// <summary>
        /// The GodzinaOtwarcia property allows access to the godzinaOtwarcia field.
        /// </summary>
        public TimeSpan GodzinaOtwarcia { get => godzinaOtwarcia; set => godzinaOtwarcia = value; }
        [DataMember]
        /// <summary>
        /// The GodzinaZamkniecia property allows access to the godzinaZamkniecia field.
        /// </summary>
        public TimeSpan GodzinaZamkniecia { get => godzinaZamkniecia; set => godzinaZamkniecia = value; }
        [DataMember]
        /// <summary>
        /// The Lekarze property allows access to the lekarze field.
        /// </summary>
        public List<Lekarz> Lekarze { get => lekarze; set => lekarze = value; }
        [DataMember]
        /// <summary>
        /// Property Wizyty allows access to the field wizyty.
        /// </summary>
        public List<Wizyta> Wizyty { get => wizyty; set => wizyty = value; }
        [DataMember]
        /// <summary>
        /// The Pacjenci property allows access to the pacjenci field.
        /// </summary>
        public List<Pacjent> Pacjenci { get => pacjenci; set => pacjenci = value; }
        [DataMember]
        /// <summary>
        /// The Account property allows access to the accounts field.
        /// </summary>
        public Dictionary<string, string> Konta { get => konta; set => konta = value; }
        /// <summary>
        /// Default constructor for the <see cref="Placowka"/> class.
        /// </summary>
        #pragma warning disable CS8618 
        public Placowka()
        #pragma warning restore CS8618
        {
            Lekarze = new();
            Pacjenci = new();
            Wizyty = new();
            godzinaOtwarcia = new();
            godzinaZamkniecia = new();
            Konta = new();
        }
        /// <summary>
        /// Parameterized constructor for <see cref="Placowka"/> class.
        /// </summary>
        /// <param name="godzinaOtwarcia">Opening time of the facility</param>
        /// <param name="godzinaZamkniecia">Closing time of the facility</param>
        public Placowka(TimeSpan godzinaOtwarcia, TimeSpan godzinaZamkniecia) : this()
        {
            GodzinaOtwarcia = godzinaOtwarcia;
            GodzinaZamkniecia = godzinaZamkniecia;
        }
        /// <summary>
        /// Parametrized constructor of the <see cref="Placowka"/> class.
        /// </summary>
        /// <param name="lekarze">The doctors in the facility</param>
        /// <param name="pacjenci">The patients in the facility</param>
        /// <param name="wizyty">The visits in the facility</param>
        /// <param name="godzinaOtwarcia">The opening hour of the facility</param>
        /// <param name="godzinaZamkniecia">The closing hour of the facility</param>
        public Placowka(List<Lekarz> lekarze, List<Pacjent> pacjenci, List<Wizyta> wizyty, TimeSpan godzinaOtwarcia, TimeSpan godzinaZamkniecia) : this(godzinaOtwarcia, godzinaZamkniecia)
        {
            Lekarze = lekarze;
            Pacjenci = pacjenci;
            Wizyty = wizyty;
        }
        /// <summary>
        /// Adds a visit to the list of visits.
        /// </summary>
        /// <param name="visit">The visit to be added.</param>
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
        /// Ends the currently ongoing visit and adds a diagnosis.
        /// </summary>
        /// <param name="diagnoza">The diagnosis to be added.</param>
        public void ZakonczWizyte(Diagnoza diagnoza)
        {
            Wizyta w1 = diagnoza.Wizyta;
        #pragma warning disable CS8600 
            Pacjent p1 = Pacjenci.Find(p => p.Equals(w1.Pacjent));
        #pragma warning restore CS8600 
        #pragma warning disable CS8602 
            p1.DodajDiagnoze(diagnoza);
        #pragma warning restore CS8602
            Wizyty.Remove(w1);
        }
        /// <summary>
        /// Cancel the appointment by the doctor
        /// </summary>
        /// <param name="pesel">Patient's PESEL</param>
        /// <param name="data">Appointment date</param>
        /// <param name="godzina">Appointment time</param>
        public void AnulujWizyteJakoLekarz(string pesel, DateTime data, TimeSpan godzina)
        {
        #pragma warning disable CS8600 
            Wizyta wizyta = Wizyty.FirstOrDefault(w => w.Pacjent.Pesel == pesel && w.Data == data && w.Godzina == godzina);
        #pragma warning restore CS8600 
            if (wizyta != null)
            {
                Wizyty.Remove(wizyta);
                wizyta.Lekarz.Zaplanowane_Wizyty.Remove(new Tuple<DateTime, TimeSpan>(data, godzina));
            }
        }
        /// <summary>
        /// Patient cancels an appointment.
        /// </summary>
        /// <param name="pesel">Patient's PESEL</param>
        /// <param name="data">Date of the appointment</param>
        /// <param name="godzina">Time of the appointment</param>
        public void AnulujWizytePacjent(string pesel, DateTime data, TimeSpan godzina)
        {
        #pragma warning disable CS8600 
            Wizyta wizyta = Wizyty.FirstOrDefault(w => w.Pacjent.Pesel == pesel && w.Data == data && w.Godzina == godzina);
        #pragma warning restore CS8600 
            if (wizyta != null)
            {
                Wizyty.Remove(wizyta);
                wizyta.Lekarz.Zaplanowane_Wizyty.Remove(new Tuple<DateTime, TimeSpan>(data, godzina));
            }
        }
        /// <summary>
        /// Adds a patient to the clinic.
        /// </summary>
        /// <param name="p1">Patient</param>
        public void DodajPacjenta(Pacjent p1)
        {
            if (p1 == null) { return; }
            Pacjenci.Add(p1);
        }
        /// <summary>
        /// Adds an account.
        /// </summary>
        /// <param name="pesel">The PESEL associated with the account.</param>
        /// <param name="haslo">The password associated with the account.</param>
        public void DodajKonto(string pesel, string haslo)
        {
            if (pesel == null || haslo == null) { return; }
            Konta.Add(pesel, haslo);
        }
        /// <summary>
        /// Adds a doctor to the clinic.
        /// </summary>
        /// <param name="doctor">Doctor to be added</param>
        public void DodajLekarza(Lekarz l1)
        {
            if (l1 == null || Lekarze.Find(p => p.Pesel == l1.Pesel) != null) { return; }
            Lekarze.Add(l1);
        }
        /// <summary>
        /// Removes a doctor.
        /// </summary>
        /// <param name="pesel">PESEL number of the doctor to be removed.</param>
        public void UsunLekarza(string pesel)
        {
        #pragma warning disable CS8600
            Lekarz l1 = Lekarze.Find(p => p.Pesel == pesel);
        #pragma warning restore CS8600 
            if (Lekarze.Find(p => p.Pesel == pesel) == null) { return; }
        #pragma warning disable CS8602 
            Wizyty.RemoveAll(p => p.Lekarz.Pesel == l1.Pesel);
        #pragma warning restore CS8602 
        #pragma warning disable CS8604 
            Lekarze.Remove(l1);
        #pragma warning restore CS8604 
        }
        /// <summary>
        /// Removes a patient.
        /// </summary>
        /// <param name="pesel">PESEL number of the patient to be removed</param>
        public void UsuńPacjenta(string pesel)
        {
            List<Wizyta> wizytyanulowane = new List<Wizyta>();
        #pragma warning disable CS8600
            Pacjent p1 = Pacjenci.Find(p => p.Pesel == pesel);
        #pragma warning restore CS8600 
            if (Pacjenci.Find(p => p.Pesel == pesel) == null) { return; }
            wizytyanulowane = Wizyty.FindAll(p => p.Pacjent.Pesel == pesel);

            foreach (Wizyta w in wizytyanulowane)
            {
                w.Lekarz.Zaplanowane_Wizyty.Remove(new Tuple<DateTime, TimeSpan>(w.Data, w.Godzina));
            }

        #pragma warning disable CS8602
            Wizyty.RemoveAll(p => p.Pacjent.Pesel == p1.Pesel);
        #pragma warning restore CS8602 
            Konta.Remove(pesel);
        #pragma warning disable CS8604 
            Pacjenci.Remove(p1);
            #pragma warning restore CS8604
        }
        /// <summary>
        /// Searches for a patient's history
        /// </summary>
        /// <param name="pesel">Patient's pesel</param>
        /// <returns>Patient's history in the facility</returns>
        public string HistoriaPacjenta(string pesel)
        {
            StringBuilder sb = new();
        #pragma warning disable CS8600 
            Pacjent pacjent = Pacjenci.Find(p => p.Pesel == pesel);
        #pragma warning restore CS8600 
            if (pacjent == null) { return "The patient does not exist in the database."; }
            pacjent.HistoriaWizyt.ForEach(w => sb.AppendLine(w.ToString()));
            if (sb.ToString() == null) { return "History not found";}
            return sb.ToString();
        }
        /// <summary>
        /// Finds all visits of a doctor on a given day
        /// </summary>
        /// <param name="pesel">Doctor's PESEL</param>
        /// <param name="data">Visit date</param>
        /// <returns>List of visits</returns>
        public List<Wizyta> LekarzWDanymDniu(string pesel, DateTime data)
        {

            List<Wizyta> wizytyulekarza = Wizyty.FindAll(w => w.Lekarz.Pesel == pesel && w.Data.Date == data);
            return wizytyulekarza;
        }
        /// <summary>
        /// Returns a list of all appointments in the clinic.
        /// </summary>
        /// <returns>List of all appointments</returns>
        public List<Wizyta> WszystkieWizyty()
        {
            return Wizyty;
        }
        /// <summary>
        /// Patient visits
        /// </summary>
        /// <param name="pesel">Patient PESEL number</param>
        /// <returns>List of visits of patient with the given PESEL number</returns>
        public List<Wizyta> WizytyPacjenta(string pesel)
        {
            List<Wizyta> wizyty = new();
            wizyty = Wizyty.FindAll(p => p.Pacjent.Pesel == pesel);
            return wizyty;
        }
        /// <summary>
        /// Saves to XML file.
        /// </summary>
        /// <param name="fname">File name</param>
        public void ZapiszDC(string fname)
        {
            using FileStream fs = new(fname, FileMode.Create);
            DataContractSerializer dc = new(typeof(Placowka));
            dc.WriteObject(fs, this);
        }
        /// <summary>
        /// Reads an XML file.
        /// </summary>
        /// <param name="fname">Name of the file</param>
        /// <returns>Contents of the XML file</returns>
        public static Placowka? OdczytDC(string fname)
        {
            if (!File.Exists(fname)) { return null; }
            using FileStream fs = new(fname, FileMode.Open);
            DataContractSerializer dc = new(typeof(Placowka));
            return dc.ReadObject(fs) as Placowka;
        }
        /// <summary>
        /// Sorts appointments
        /// </summary>
        public void SortujWizyta()
        {
            Wizyty.Sort();
        }
        /// <summary>
        /// Searches for doctors with a given specialization.
        /// </summary>
        /// <param name="specialization">Name of the specialization</param>
        /// <returns>List of doctors with the given specialization</returns>
        public List<Lekarz> WyszukajSpecjalizacja(string specjalizacja)
        {
            return Lekarze.FindAll(p => p.Specjalizacja.Equals(specjalizacja));
        }
        /// <summary>
        /// Finds all appointments of a given doctor.
        /// </summary>
        /// <param name="pesel">Doctor's PESEL</param>
        /// <returns>List of all appointments of the given doctor</returns>
        public List<Wizyta> WszystkieWizytyDanegoLekarza(string pesel)
        {
            return Wizyty.FindAll(p => p.Lekarz.Pesel == pesel);
        }
        /// <summary>
        /// All visits of a specific person to a specific doctor.
        /// </summary>
        /// <param name="doctorPesel">Doctor's PESEL</param>
        /// <param name="patientPesel">Patient's PESEL</param>
        /// <returns>List of all visits of the specific person to the specific doctor</returns>
        public List<Wizyta> WszystkieWizytyDanejOsobyUDanegoLekarza(string pesellekarza, string peselpacjenta)
        {
            List<Wizyta> w = WszystkieWizytyDanegoLekarza(pesellekarza);
        #pragma warning disable CS8603 // Possible null reference return.
            if (w == null) { return null; }
        #pragma warning restore CS8603 // Possible null reference return.
            return w.FindAll(p => p.Pacjent.Pesel == peselpacjenta);

        }
        /// <summary>
        /// Adds a patient account if they are not registered yet.
        /// </summary>
        /// <param name="pesel">Patient's PESEL number</param>
        /// <param name="haslo">Password for the patient's account</param>
        /// <returns>true if the patient does not have an account yet or false if the patient already has an account</returns>
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