using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Project
{
    [DataContract(IsReference = true)]
    /// <summary>
    /// Klasa Diagnoza
    /// </summary>
    public class Diagnoza
    {
        Wizyta wizyta;
        string choroba;
        string recepta;

        [DataMember]
        /// <summary>Właściwość Wizyta umożliwia dostęp do pola wizyta.
        /// </summary>
        public Wizyta Wizyta { get => wizyta; set => wizyta = value; }

        [DataMember]
        /// <summary>Właściwość Choroba umożliwia dostęp do pola choroba.
        /// </summary>
        public string Choroba { get => choroba; set => choroba = value; }

        [DataMember]
        /// <summary>Właściwość Recepta umożliwia dostęp do pola recepta.
        /// </summary>
        public string Recepta { get => recepta; set => recepta = value; }
        /// <summary>
        /// Konstruktor domyślny klasy Diagnoza.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Diagnoza()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Wizyta = new();
            choroba = string.Empty;
            recepta = string.Empty;
        }
        /// <summary>
        /// Konstruktor parametryczny klasy Diagnoza
        /// </summary>
        /// <param name="wizyta">Wizyta u lekarza</param>
        /// <param name="choroba">Zdiagnozowana choroba pacjenta</param>
        /// <param name="recepta">Recepta przypisana do pacjenta</param>
        public Diagnoza(Wizyta wizyta, string choroba, string recepta) : this()
        {
            Wizyta = wizyta;
            Choroba = choroba;
            Recepta = recepta;
        }
        /// <summary>
        /// Tworzy listę diagnoz.
        /// </summary>
        /// <returns>
        /// String który reprezentuje tą instancję.
        /// </returns>
        public override string ToString()
        {
            return $"Dzień: {Wizyta.Data:dd-MM-yyyy}, Diagnoza: {Choroba}, Recepta: {Recepta}";
        }
    }
}