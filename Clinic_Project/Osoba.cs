using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Clinic_Project
{
    public enum EnumPlec { K, M }
    /// <summary>
    /// Abstract class Person
    /// </summary>
    [DataContract]
    public abstract class Osoba
    {
        string imie;
        string nazwisko;
        DateTime dataUrodzenia;
        string pesel;
        private EnumPlec plec;

        [DataMember]
        /// <summary>
        /// The Imie property provides access to the 'imie' field.
        /// </summary>
        public string Imie { get => imie; set => imie = value; }
        [DataMember]
        /// <summary>
        /// The property "Nazwisko" allows access to the "nazwisko" field.
        /// </summary>
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }
        [DataMember]
        /// <summary>
        /// The DataUrodzenia property allows access to the dataUrodzenia field.
        /// </summary>
        public DateTime DataUrodzenia { get => dataUrodzenia; set => dataUrodzenia = value; }
        [DataMember]
        /// <summary>
        /// The Plec property allows access to the plec field.
        /// </summary>
        public EnumPlec Plec { get => plec; set => plec = value; }
        [DataMember]
        /// <summary>
        /// The Pesel property allows access to the pesel field.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// The exception is thrown in case of an incorrect pesel format.
        /// </exception>
        public string Pesel
        {
            get => pesel;
            set
            {
                if (!WeryfikujPesel(value))
                {
                    throw new ArgumentException("Wrong PESEL!");
                }
                pesel = value;
            }
        }
        /// <summary>
        /// Default constructor of the Person class.
        /// </summary>
        #pragma warning disable CS8618
        public Osoba()
        #pragma warning restore CS8618
        {
            Imie = string.Empty;
            Nazwisko = string.Empty;
            DataUrodzenia = DateTime.Now;
            Pesel = new string('0', 11);
        }
        /// <summary>
        /// Parameterized constructor of the Person class.
        /// </summary>
        /// <param name="imie">Person's first name</param>
        /// <param name="nazwisko">Person's last name</param>
        /// <param name="plec">Person's gender</param>
        public Osoba(string imie, string nazwisko, EnumPlec plec) : this()
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Plec = plec;
        }
        /// <summary>
        /// Parameterized constructor of the Person class.
        /// </summary>
        /// <param name="imie">Person's first name</param>
        /// <param name="nazwisko">Person's last name</param>
        /// <param name="dataUrodzenia">Person's date of birth</param>
        /// <param name="pesel">Person's PESEL number</param>
        /// <param name="plec">Person's gender</param>
        public Osoba(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec) : this(imie, nazwisko, plec)
        {
            if (DateTime.TryParseExact(dataUrodzenia,
                new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy" ,"yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd"
                }, null, System.Globalization.DateTimeStyles.None,
                out DateTime res))
            {
                DataUrodzenia = res;
            }
            Pesel = pesel;
        }
        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer
        /// that indicates whether the current instance precedes, follows, or occurs in the same position
        /// in the sort order as the other object.
        /// </summary>
        /// <param name="other">The object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(Osoba? other)
        {
            if (other is null) { return 1; }
            int cmpnazw = Nazwisko.CompareTo(other.Nazwisko);
            if (cmpnazw != 0) { return cmpnazw; }
            return Imie.CompareTo(other.Imie);
        }
        /// <summary>
        /// Verifies the length and components of the PESEL number using a regular expression.
        /// </summary>
        /// <param name="pesel">PESEL number of the person.</param>
        /// <returns></returns>
        bool WeryfikujPesel(string pesel)
        {
            return Regex.IsMatch(pesel, @"\d{11}");
        }
        /// <summary>
        /// Overrides the method that describes the object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} ({Plec}), {DataUrodzenia:dd-MM-yyyy} ({Pesel})";
        }
    }
}