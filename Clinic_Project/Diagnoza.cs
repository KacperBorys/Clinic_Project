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
    /// Diagnosis class
    /// </summary>
    public class Diagnoza
    {
        Wizyta wizyta;
        string choroba;
        string recepta;

        [DataMember]
        /// <summary>
        /// The Visit property provides access to the visit field.
        /// </summary>
        public Wizyta Wizyta { get => wizyta; set => wizyta = value; }

        [DataMember]
        /// <summary>
        /// The Disease property provides access to the disease field.
        /// </summary>
        public string Choroba { get => choroba; set => choroba = value; }

        [DataMember]
        /// <summary>
        /// The Prescription property provides access to the prescription field.
        /// </summary>
        public string Recepta { get => recepta; set => recepta = value; }
        /// <summary>
        /// Default constructor of the Diagnosis class.
        /// </summary>
        #pragma warning disable CS8618 
        public Diagnoza()
        #pragma warning restore CS8618
        {
            Wizyta = new();
            choroba = string.Empty;
            recepta = string.Empty;
        }
        /// /// <summary>
        /// Parametric constructor of the Diagnosis class
        /// </summary>
        /// <param name="visit">Visit a doctor</param>
        /// <param name="disease">Diagnosed patient disease</param>
        /// <param name="prescription">Prescription assigned to the patient</param>
        public Diagnoza(Wizyta wizyta, string choroba, string recepta) : this()
        {
            Wizyta = wizyta;
            Choroba = choroba;
            Recepta = recepta;
        }
        /// <summary>
        /// Creates a diagnosis list.
        /// </summary>
        /// <returns>
        /// String that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Dzień: {Wizyta.Data:dd-MM-yyyy}, Diagnoza: {Choroba}, Recepta: {Recepta}";
        }
    }
}