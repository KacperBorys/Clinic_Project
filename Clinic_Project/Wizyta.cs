using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
//sprawdzic czy lekarz moze przyjac ludzi - lambda w diagnoza
//interfejsy
namespace Clinic_Project
{
    [DataContract]
    /// <summary>
    /// Klasa Wizyta implementująca interfejs IComparable
    /// </summary>
    public class Wizyta : IComparable<Wizyta>
    {
        DateTime data;
        Lekarz lekarz;
        Pacjent pacjent;
        TimeSpan godzina;

        [DataMember]
        /// <summary>Właściwość Data umożliwia dostęp do pola data.
        /// </summary>
        public DateTime Data { get => data; set => data = value; }
        [DataMember]
        /// <summary>Właściwość Lekarz umożliwia dostęp do pola lekarz.
        /// </summary>
        public Lekarz Lekarz { get => lekarz; set => lekarz = value; }
        [DataMember]
        /// <summary>Właściwość Pacjent umożliwia dostęp do pola pacjent.
        /// </summary>
        public Pacjent Pacjent { get => pacjent; set => pacjent = value; }
        [DataMember]
        /// <summary>Właściwość Godzina umożliwia dostęp do pola godzina.
        /// </summary>
        public TimeSpan Godzina { get => godzina; set => godzina = value; }
        /// <summary>
        /// Konstruktor domyślny klasy Wizyta
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Wizyta()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Lekarz = new Lekarz();
            Pacjent = new Pacjent();
            Data = new DateTime();
            Godzina = new TimeSpan();
        }
        /// <summary>
        /// Konstruktor parametryczny klasy <see cref="Wizyta"/>.
        /// </summary>
        /// <param name="data">Data wizyty</param>
        /// <param name="lekarz">Lekarz uczestniczący w wizycie</param>
        /// <param name="pacjent">Pacjent uczestniczący w wizycie</param>
        /// <param name="godzina">Godzina wizyty</param>
        /// <exception cref="DataException">Zły format daty wizyty</exception>
        public Wizyta(string data, Lekarz lekarz, Pacjent pacjent, TimeSpan godzina) : this()
        {
            if (!DateTime.TryParseExact(data,
                new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy" ,"yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd"
                }, null, System.Globalization.DateTimeStyles.None,
                out DateTime res))
            {
                throw new DataException("Zły format daty!");
            }
            Data = res;
            Lekarz = lekarz;
            Pacjent = pacjent;
            Godzina = godzina;
        }

        /// <summary>
        /// Reprezentuje dane dotyczace wizyty
        /// </summary>
        /// <returns>
        /// String który reprezentuje tą instancję.
        /// </returns>
        public override string ToString()
        {
            return $"Pacjent: {pacjent.Imie} {pacjent.Nazwisko} ({pacjent.Pesel})\nLekarz: {lekarz.Imie} {lekarz.Nazwisko} ({lekarz.Pesel})" +
                $"\nData: {Data:dd-MM-yyyy} {Godzina.Hours:00}:{Godzina.Minutes:00}\n";
        }
        /// <summary>
        /// Porównuje bieżącą instancję z innym obiektem tego samego typu i zwraca liczbę całkowitą wskazującą, czy bieżące wystąpienie poprzedza, następuje lub występuje w tej samej pozycji w porządku sortowania co inny obiekt.
        /// </summary>
        /// <param name="other">Obiekt do porównania z tym wystąpieniem.</param>
        /// <returns>
        /// Wartość, która wskazuje względną kolejność porównywanych obiektów. Wartość zwracana ma następujące znaczenie:
        /// <list type="table"><listheader><term> Wartość</term><description> Znaczenie</description></listheader><item><term> Mniej niż 0</term><description> To wystąpienie poprzedza <paramref name="other" /> w porządku sortowania.</description></item><item><term> 0</term><description> To wystąpienie występuje w tej samej pozycji w porządku sortowania co<paramref name="other" />.</description></item><item><term> Więcej niż 0</term><description> Ta instancja następuje po <paramref name="other" /> w porządku sortowania.</description></item></list>
        /// </returns>
        public int CompareTo(Wizyta? other)
        {
            if (other == null) return 1;
            int cmpdata = 0;
            cmpdata = Data.CompareTo(other.Data);
            if (cmpdata == 0)
            {
                return Godzina.CompareTo(other.Godzina);
            }
            return cmpdata;
        }

    }
}