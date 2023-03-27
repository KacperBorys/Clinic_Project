using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Project
{
    /// <summary>
    /// Klasa Lekarz dziedzizcąca po klasie Osoba, implementuje interfejs ICloneable
    /// </summary>
    [DataContract]
    public class Lekarz : Osoba, ICloneable
    {
        string specjalizacja;
        Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>> godzinyPracy;
        Dictionary<Tuple<DateTime, TimeSpan>, bool> zaplanowane_Wizyty;
        [DataMember]
        /// <summary>Właściwość Specjalizacja umożliwia dostęp do pola specjalizacja.
        /// </summary>
        public string Specjalizacja { get => specjalizacja; set => specjalizacja = value; }
        [DataMember]
        /// <summary>Właściwość GodzinyPracy umożliwia dostęp do pola godzinyPracy.
        /// </summary>
        public Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>> GodzinyPracy { get => godzinyPracy; set => godzinyPracy = value; }
        [DataMember]
        /// <summary>Właściwość Zaplanowane_Wizyty umożliwia dostęp do pola zaplanowane_wizyty.
        /// </summary>
        public Dictionary<Tuple<DateTime, TimeSpan>, bool> Zaplanowane_Wizyty { get => zaplanowane_Wizyty; set => zaplanowane_Wizyty = value; }
        /// <summary>
        /// Konstruktor domyślny klasy Lekarz
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Lekarz()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Specjalizacja = string.Empty;
            GodzinyPracy = new();
            zaplanowane_Wizyty = new();
            Imie = string.Empty;
            Nazwisko = string.Empty;
            DataUrodzenia = new();
            Pesel = "00000000000";
        }
        /// <summary>
        /// Konstruktor parametrczyny klasy Lekarz
        /// </summary>
        /// <param name="imie">Imie lekarza</param>
        /// <param name="nazwisko">Nazwisko lekarza</param>
        /// <param name="dataUrodzenia">Data urodzenia lekarza</param>
        /// <param name="pesel">Pesel lekarza</param>
        /// <param name="plec">Płeć lekarza</param>
        /// <exception cref="ArgumentException">Wyjątek zwracany w przypadku ustawienia niepoprawnej daty urodzenia lekarza</exception>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Lekarz(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            if (!DateTime.TryParseExact(dataUrodzenia,
                new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy" ,"yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd"
                }, null, System.Globalization.DateTimeStyles.None,
                out DateTime res))
            {
                throw new ArgumentException("Zła data");
            }
            Imie = imie;
            Nazwisko = nazwisko;
            DataUrodzenia = res;
            Pesel = pesel;
            Plec = plec;
        }
        /// <summary>
        /// Konstruktor parametryczny klasy Lekarz dziedziczący po konstrukotrze bazowym
        /// </summary>
        /// <param name="imie">Imie lekarza</param>
        /// <param name="nazwisko">Nazwisko lekarza</param>
        /// <param name="dataUrodzenia">Data urodzenia lekarza</param>
        /// <param name="pesel">Pesel lekarza</param>
        /// <param name="plec">Płeć lekarza</param>
        /// <param name="specjalizacja">Specjalizacja lekarza</param>
        /// <param name="godzinyPracy">Godziny pracy lekarza</param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Lekarz(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec, string specjalizacja, Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>> godzinyPracy)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            : base(imie, nazwisko, dataUrodzenia, pesel, plec)
        {
            Specjalizacja = specjalizacja;
            GodzinyPracy = godzinyPracy;
            zaplanowane_Wizyty = new();
        }
        /// <summary>
        /// Sprawdza czy można umówić wizytę u lekarza w danym dniu o konkretnej godzinie.
        /// </summary>
        /// <param name="data"> Data wizyty</param>
        /// <param name="godzina">Godzina wizyty</param>
        /// <returns>Zwraca `true` jeżeli można umówić wizytę lub `false` jeżeli nie można umówić wizyty</returns>
        /// <exception cref="ArgumentException"> Wyjątek zgłaszany w przypadku niepoprawnej daty wizyty.</exception>
        public bool SprawdzCzyMoznaUmowic(string data, TimeSpan godzina)
        {
            if (Zaplanowane_Wizyty == null)
            {
                return true;
            }
            if (!DateTime.TryParseExact(data,
                new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy" ,"yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd"
                }, null, System.Globalization.DateTimeStyles.None,
                out DateTime res))
            {
                throw new ArgumentException("Zła data");
            }
            if (res < DateTime.Now) { return false; }
            DayOfWeek dzien = res.DayOfWeek;
            if (GodzinyPracy.ContainsKey(dzien))
            {
                Tuple<TimeSpan, TimeSpan> godzinyPrzyjec = GodzinyPracy[dzien];
                if ((godzina.Hours >= godzinyPrzyjec.Item1.Hours && godzina.Hours <= godzinyPrzyjec.Item2.Hours) && (godzina.Minutes == 0 || godzina.Minutes == 30))
                {
                    // sprawdzenie czy w tej godzinie jest już zaplanowana wizyta
                    if (Zaplanowane_Wizyty.ContainsKey(Tuple.Create(DateTime.Parse(data), godzina)))
                    {
                        if (Zaplanowane_Wizyty[Tuple.Create(DateTime.Parse(data), godzina)])
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Tworzy listę lekarzy.
        /// </summary>
        /// <returns>
        /// String który reprezentuje tą instancję.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<DayOfWeek, Tuple<TimeSpan, TimeSpan>> dzien in GodzinyPracy)
            {
                sb.AppendLine($"{dzien.Key.ToString()}: {dzien.Value.Item1.ToString()[0..5]}-{dzien.Value.Item2.ToString()[0..5]}");
            }
            return $"{Imie} {Nazwisko}, Specjalizacja: {Specjalizacja}\n{sb}";
        }
        /// <summary>
        /// Tworzy nowy obiekt, który jest kopią bieżącej instancji.
        /// </summary>
        /// <returns>
        /// Nowy obiekt będący kopią tej instancji.
        /// </returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}