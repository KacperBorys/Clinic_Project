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
    /// Abstrakcyjna klasa Osoba
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
        /// <summary>Właściwość Imie umożliwia dostęp do pola imie.
        /// </summary>
        public string Imie { get => imie; set => imie = value; }
        [DataMember]
        /// <summary>Właściwość Nazwisko umożliwia dostęp do pola nazwisko.
        /// </summary>
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }
        [DataMember]
        /// <summary>Właściwość DataUrodzenia umożliwia dostęp do pola dataUrodzenia.
        /// </summary>
        public DateTime DataUrodzenia { get => dataUrodzenia; set => dataUrodzenia = value; }
        [DataMember]
        /// <summary>Właściwość Plec umożliwia dostęp do pola plec.
        /// </summary>
        public EnumPlec Plec { get => plec; set => plec = value; }
        [DataMember]
        /// <summary>Właściwość Pesel umożliwia dostęp do pola pesel.
        /// <exception cref="ArgumentException">
        /// Wyjątek jest wyrzucany w przypadku niepoprawnego formatu peselu.
        /// </exception>
        /// </summary>
        public string Pesel
        {
            get => pesel;
            set
            {
                if (!WeryfikujPesel(value))
                {
                    throw new ArgumentException("Zły pesel.");
                }
                pesel = value;
            }
        }
        /// <summary>
        /// Kontruktor domyślny klasy Osoba.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Osoba()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Imie = string.Empty;
            Nazwisko = string.Empty;
            DataUrodzenia = DateTime.Now;
            Pesel = new string('0', 11);
        }
        /// <summary>
        /// Kontruktor parametryczny klasy Osoba.
        /// </summary>
        /// <param name="imie">Imie osoby</param>
        /// <param name="nazwisko">Nazwisko osoby</param>
        /// <param name="plec">Płeć osoby</param>
        public Osoba(string imie, string nazwisko, EnumPlec plec) : this()
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Plec = plec;
        }
        /// <summary>
        /// Konstruktor parametryczny klasy Osoba.
        /// </summary>
        /// <param name="imie">Imie osoby</param>
        /// <param name="nazwisko">Nazwisko osoby</param>
        /// <param name="dataUrodzenia">Data urodzenia osoby</param>
        /// <param name="pesel">Pesel osoby</param>
        /// <param name="plec">Płeć osoby</param>
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
        /// Porównuje bieżące wystąpienie z innym obiektem tego samego typu i zwraca liczbę całkowitą, 
        /// która wskazuje, czy bieżące wystąpienie poprzedza, 
        /// następuje po lub występuje w tym samym położeniu, co inny obiekt w porządku sortowania.
        /// </summary>
        /// <param name="other">Obiekt, który ma zostać porównany z tym wystąpieniem.</param>
        /// <returns>Wartość wskazująca względną kolejność porównywanych obiektów. </returns>
        public int CompareTo(Osoba? other)
        {
            if (other is null) { return 1; }
            int cmpnazw = Nazwisko.CompareTo(other.Nazwisko);
            if (cmpnazw != 0) { return cmpnazw; }
            return Imie.CompareTo(other.Imie);
        }
        /// <summary>
        /// Weryfikuje długość i elementy składowe numera pesel za pomocą wyrażenia regularnego .
        /// </summary>
        /// <param name="pesel">Pesel osoby</param>
        /// <returns></returns>
        bool WeryfikujPesel(string pesel)
        {
            return Regex.IsMatch(pesel, @"\d{11}");
        }
        /// <summary>
        /// Nadpisanie metody opisującej obiekt
        /// </summary>
        /// <returns>Zwraca ciąg reprezentujący dany obiekt.</returns>
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} ({Plec}), {DataUrodzenia:dd-MM-yyyy} ({Pesel})";
        }
    }
}