using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Clinic_Project
{
    [DataContract]
    public class Pacjent :Osoba, IEquatable<Pacjent>
    {
        List<Diagnoza> historiaWizyt;
        [DataMember]
        public List<Diagnoza> HistoriaWizyt { get => historiaWizyt; set => historiaWizyt = value; }
        /// <summary>
        /// Konstruktor domyślny klasy Pacjent dziedziczący po konstruktorze klasy Osoba
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Pacjent() : base() { HistoriaWizyt = new(); }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        /// <summary>
        /// Konstruktor parametryczny klasy Pacjent
        /// </summary>
        /// <param name="imie">Imie pacjenta</param>
        /// <param name="nazwisko">Nazwisko pacjenta</param>
        /// <param name="plec">Płeć pacjenta</param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Pacjent(string imie, string nazwisko, EnumPlec plec) : base(imie, nazwisko, plec) { HistoriaWizyt = new(); }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        /// <summary>
        /// Konstruktor parametryczny klasy Pacjent
        /// </summary>
        /// <param name="imie">Imie pacjenta</param>
        /// <param name="nazwisko">Nazwisko pacjenta</param>
        /// <param name="dataUrodzenia">Data urodzenia pacjenta</param>
        /// <param name="pesel">Pesel pacjenta</param>
        /// <param name="plec">Płeć pacjenta</param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Pacjent(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec) : base(imie, nazwisko, dataUrodzenia, pesel, plec)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        { HistoriaWizyt = new(); }
        /// <summary>
        /// Dodaje diagnozę do listy HistoriaWizyt
        /// </summary>
        /// <param name="d">Dodawana diagnoza</param>
        public void DodajDiagnoze(Diagnoza d)
        {
            if (HistoriaWizyt == null) { return; }
            HistoriaWizyt.Add(d);
        }
        /// <summary>
        /// Usuwa diagnozę z listy HistoriaWizyt
        /// </summary>
        /// <param name="d">Usuwana diagnoza</param>
        public void UsunDiagnoze(Diagnoza d)
        {
            HistoriaWizyt.Remove(d);
        }
        /// <summary>
        /// Wskazuje, czy bieżący obiekt jest równy innemu obiektowi tego samego typu.
        /// </summary>
        /// <param name="other">Obiekt do porównania z tym obiektem.</param>
        /// <returns>`true` jeśli bieżący obiekt jest równy parametrowi other ; w przeciwnym razie `false`</returns>
        public bool Equals(Pacjent? other)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return Pesel.Equals(other.Pesel);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}