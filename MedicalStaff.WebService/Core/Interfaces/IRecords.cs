using System;
using System.Collections.Generic;

namespace MedicalStaff.WebService.Core.Interfaces
{
    /// <summary>
    /// Represents a common record. 
    /// </summary>
    public interface IRecords
    {
        /// <summary>
        /// The record unique token for identifying this record.
        /// </summary>
        Guid ID { get; set; }

        /// <summary>
        /// The name of the patient.
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// The patient picture location address.
        /// </summary>
        String PictureLocation { get; set; }

        /// <summary>
        /// The patient CPF. (The Brazilian national-wide unique identification number)
        /// </summary>
        String CPF { get; set; }

        /// <summary>
        /// The patient address.
        /// </summary>
        String Address { get; set; }

        /// <summary>
        /// The patient phone number.
        /// </summary>
        String Phone { get; set; }

        /// <summary>
        /// The patient birth date.
        /// </summary>
        DateTime Birth { get; set; }

        /// <summary>
        /// The patient mail address.
        /// </summary>
        String Email { get; set; }

        /// <summary>
        /// The record creation's date and time.
        /// </summary>
        DateTime Created { get; set; }

        /// <summary>
        /// Formats this instance properties for presentation means.
        /// </summary>
        /// <returns>This current with some predefined formated properties.</returns>
        Dictionary<String, String> FormatSpecialProperties();
    }
}