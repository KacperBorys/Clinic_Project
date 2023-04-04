using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Clinic_Project
{
    /// <summary>
    /// Class Patient inheriting from class Person.
    /// </summary>
    [DataContract]
    public class Pacjent :Osoba, IEquatable<Pacjent>
    {
        List<Diagnoza> historiaWizyt;
        [DataMember]
        public List<Diagnoza> HistoriaWizyt { get => historiaWizyt; set => historiaWizyt = value; }
        /// <summary>
        /// Default constructor of the Patient class, inheriting from the constructor of the Person class.
        /// </summary>
        #pragma warning disable CS8618
        public Pacjent() : base() { HistoriaWizyt = new(); }
        #pragma warning restore CS8618
        /// <summary>
        /// Parameterized constructor of the Pacjent class
        /// </summary>
        /// <param name="imie">The first name of the pacjent</param>
        /// <param name="nazwisko">The last name of the pacjent</param>
        /// <param name="plec">The gender of the pacjent</param>
        #pragma warning disable CS8618
        public Pacjent(string imie, string nazwisko, EnumPlec plec) : base(imie, nazwisko, plec) { HistoriaWizyt = new(); }
        #pragma warning restore CS8618
        /// <summary>
        /// Parameterized constructor of the Pacjent class.
        /// </summary>
        /// <param name="imie">Patient's first name</param>
        /// <param name="nazwisko">Patient's last name</param>
        /// <param name="dataUrodzenia">Patient's date of birth</param>
        /// <param name="pesel">Patient's PESEL number</param>
        /// <param name="plec">Patient's gender</param>
        #pragma warning disable CS8618
        public Pacjent(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec) : base(imie, nazwisko, dataUrodzenia, pesel, plec)
        #pragma warning restore CS8618
        { HistoriaWizyt = new(); }
        /// <summary>
        /// Adds a diagnosis to the list of VisitsHistory
        /// </summary>
        /// <param name="d">Diagnosis being added</param>
        public void DodajDiagnoze(Diagnoza d)
        {
            if (HistoriaWizyt == null) { return; }
            HistoriaWizyt.Add(d);
        }
        /// <summary>
        /// Removes a diagnosis from the list of previous visits (HistoriaWizyt).
        /// </summary>
        /// <param name="d">The diagnosis to be removed.</param>
        public void UsunDiagnoze(Diagnoza d)
        {
            HistoriaWizyt.Remove(d);
        }
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">The object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(Pacjent? other)
        {
        #pragma warning disable CS8602
            return Pesel.Equals(other.Pesel);
        #pragma warning restore CS8602
        }
    }
}