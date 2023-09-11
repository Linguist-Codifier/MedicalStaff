using System;
using MedicalStaff.WebService.Core.Interfaces;

namespace MedicalStaff.WebService.Core.Helpers.Mappers
{
    /// <summary>
    /// Represents a Patient account credentials.
    /// </summary>
    public struct PatientAccountCrendential : IPatientAccountCredential
    {
        /// <summary>
        /// Represents the Medical Practioner account access password.
        /// </summary>
        public String Credential { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="PatientAccountCrendential"/> by providing the Patient CPF(The Brazilian national-wide unique identification number) and its account password.
        /// </summary>
        /// <param name="password">The Patient account password.</param>
        public PatientAccountCrendential(String password)
        {
            this.Credential = password;
        }

        /// <summary>
        /// Provides an empty <typeparamref name="TCast"/> instance in where <typeparamref name="TCast"/> implements <see cref="IPatientAccountCredential"/>.
        /// </summary>
        /// <typeparam name="TCast">An <see cref="IPatientAccountCredential"/> type.</typeparam>
        /// <returns>An empty <typeparamref name="TCast"/> instance in where <typeparamref name="TCast"/> implements <see cref="IPatientAccountCredential"/>.</returns>
        public static TCast Empty<TCast>() where TCast : IPatientAccountCredential
        {
            IPatientAccountCredential EmptyPatientCredentials = new PatientAccountCrendential();

            return (TCast)EmptyPatientCredentials;
        }
    }
}