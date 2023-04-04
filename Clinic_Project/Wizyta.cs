using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Project
{
    [DataContract]
    /// <summary>
    /// The Visit class that implements the IComparable interface
    /// </summary>
    public class Wizyta : IComparable<Wizyta>
    {
        DateTime data;
        Lekarz lekarz;
        Pacjent pacjent;
        TimeSpan godzina;

        [DataMember]
        /// <summary>
        /// The Date property provides access to the date field.
        /// </summary>
        public DateTime Data { get => data; set => data = value; }
        [DataMember]
        /// <summary>
        /// The Physician property provides access to the Physician field.
        /// </summary>
        public Lekarz Lekarz { get => lekarz; set => lekarz = value; }
        [DataMember]
        /// <summary>
        /// The Patient property provides access to the patient field.
        /// </summary>
        public Pacjent Pacjent { get => pacjent; set => pacjent = value; }
        [DataMember]
        /// <summary>
        /// The Time property provides access to the time field.
        /// </summary>
        public TimeSpan Godzina { get => godzina; set => godzina = value; }
        /// <summary>
        /// Default constructor of the Visit class
        /// </summary>
        #pragma warning disable CS8618 
        public Wizyta()
        #pragma warning restore CS8618
        {
            Lekarz = new Lekarz();
            Pacjent = new Pacjent();
            Data = new DateTime();
            Godzina = new TimeSpan();
        }
        /// <summary>
        /// Parametric constructor of <see cref="Visit"/> class.
        /// </summary>
        /// <param name="date">Date of visit</param>
        /// <param name="doctor">Doctor attending the visit</param>
        /// <param name="patient">Patient attending visit</param>
        /// <param name="time">Time of visit</param>
        /// <exception cref="DataException">Wrong visit date format</exception>
        public Wizyta(string data, Lekarz lekarz, Pacjent pacjent, TimeSpan godzina) : this()
        {
            if (!DateTime.TryParseExact(data,
                new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy" ,"yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd"
                }, null, System.Globalization.DateTimeStyles.None,
                out DateTime res))
            {
                throw new DataException("Wrong date format!");
            }
            Data = res;
            Lekarz = lekarz;
            Pacjent = pacjent;
            Godzina = godzina;
        }
        /// <summary>
        /// Represents visit data
        /// </summary>
        /// <returns>
        /// String that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Pacjent: {pacjent.Imie} {pacjent.Nazwisko} ({pacjent.Pesel})\nLekarz: {lekarz.Imie} {lekarz.Nazwisko} ({lekarz.Pesel})" +
                $"\nData: {Data:dd-MM-yyyy} {Godzina.Hours:00}:{Godzina.Minutes:00}\n";
        }
        /// <summary>
        /// Compares the current instance to another object of the same type and returns an integer indicating whether the current instance precedes, follows, or is in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">Object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the compared objects. The returned value has the following meaning:
        /// <list type="table"><listheader><term> Value</term><description> Meaning</description></listheader><item><term> Less than 0</term><description> This instance precedes <paramref name="other" /> in the sort order.</description></item><item><term> 0</term><description> This instance occurs in the same position in the sort order as< paramref name="other" />.</description></item><item><term> Greater than 0</term><description> This instance follows <paramref name="other" /> in the sort order. </description></item></list>
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