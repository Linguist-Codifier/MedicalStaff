using System;

namespace MedicalStaff.WebService.Core.Interfaces
{
    /// <summary>
    /// Represents a common data transfer object for <see cref="IPatientRecord"/>.
    /// </summary>
    public interface IPatientRecordsDTO
    {
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
        /// The patient mail address.
        /// </summary>
        String Email { get; set; }

        /// <summary>
        /// The patient birth date.
        /// </summary>
        DateOnly Birth { get; set; }
    }
}