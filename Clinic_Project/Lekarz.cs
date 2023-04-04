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
    /// The Doctor class inherits from the Person class and implements the ICloneable interface
    /// </summary>
    [DataContract]
    public class Lekarz : Osoba, ICloneable
    {
        string specjalizacja;
        Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>> godzinyPracy;
        Dictionary<Tuple<DateTime, TimeSpan>, bool> zaplanowane_Wizyty;
        [DataMember]
        /// <summary>
        /// The Specjalizacja property provides access to the specjalizacja field.
        /// </summary>
        public string Specjalizacja { get => specjalizacja; set => specjalizacja = value; }
        [DataMember]
        /// <summary>
        /// Property GodzinyPracy allows access to the godzinyPracy field.
        /// </summary>
        public Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>> GodzinyPracy { get => godzinyPracy; set => godzinyPracy = value; }
        [DataMember]
        /// <summary>
        /// The Zaplanowane_Wizyty property allows access to the zaplanowane_wizyty field.
        /// </summary>
        public Dictionary<Tuple<DateTime, TimeSpan>, bool> Zaplanowane_Wizyty { get => zaplanowane_Wizyty; set => zaplanowane_Wizyty = value; }
        /// <summary>
        /// Default constructor of the Doctor class
        /// </summary>
#pragma warning disable CS8618 
        public Lekarz()
#pragma warning restore CS8618
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
        /// Parameterized constructor of the Lekarz class.
        /// </summary>
        /// <param name="imie">First name of the doctor</param>
        /// <param name="nazwisko">Last name of the doctor</param>
        /// <param name="dataUrodzenia">Date of birth of the doctor</param>
        /// <param name="pesel">PESEL of the doctor</param>
        /// <param name="plec">Gender of the doctor</param>
        /// <exception cref="ArgumentException">Thrown when an invalid date of birth is set for the doctor</exception>

        #pragma warning disable CS8618
        public Lekarz(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec)
        #pragma warning restore CS8618 

        {
            if (!DateTime.TryParseExact(dataUrodzenia,
                new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy" ,"yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd"
                }, null, System.Globalization.DateTimeStyles.None,
                out DateTime res))
            {
                throw new ArgumentException("Wrong date!");
            }
            Imie = imie;
            Nazwisko = nazwisko;
            DataUrodzenia = res;
            Pesel = pesel;
            Plec = plec;
        }
        /// <summary>
        /// Parameterized constructor of the Doctor class that inherits from the base constructor.
        /// </summary>
        /// <param name="firstName">Doctor's first name</param>
        /// <param name="lastName">Doctor's last name</param>
        /// <param name="dateOfBirth">Doctor's date of birth</param>
        /// <param name="pesel">Doctor's PESEL number</param>
        /// <param name="gender">Doctor's gender</param>
        /// <param name="specialization">Doctor's specialization</param>
        /// <param name="workingHours">Doctor's working hours</param>
        #pragma warning disable CS8618
        public Lekarz(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec, string specjalizacja, Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>> godzinyPracy)
        #pragma warning restore CS8618 
            : base(imie, nazwisko, dataUrodzenia, pesel, plec)
        {
            Specjalizacja = specjalizacja;
            GodzinyPracy = godzinyPracy;
            zaplanowane_Wizyty = new();
        }
        /// <summary>
        /// Checks if it is possible to schedule a visit with the doctor on a given date and time.
        /// </summary>
        /// <param name="date">Visit date</param>
        /// <param name="time">Visit time</param>
        /// <returns>Returns true if it is possible to schedule the visit or false if it is not possible to schedule the visit</returns>
        /// <exception cref="ArgumentException">Thrown when the provided date is invalid.</exception>
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
                throw new ArgumentException("Wrong date!");
            }
            if (res < DateTime.Now) { return false; }
            DayOfWeek dzien = res.DayOfWeek;
            if (GodzinyPracy.ContainsKey(dzien))
            {
                Tuple<TimeSpan, TimeSpan> godzinyPrzyjec = GodzinyPracy[dzien];
                if ((godzina.Hours >= godzinyPrzyjec.Item1.Hours && godzina.Hours <= godzinyPrzyjec.Item2.Hours) && (godzina.Minutes == 0 || godzina.Minutes == 30))
                {
                    // check if a visit is already scheduled at this time
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
        /// Creates a list of doctors.
        /// </summary>
        /// <returns>
        /// A string that represents this instance.
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
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns> 
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}