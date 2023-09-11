using System;
using MedicalStaff.WebService.Core.Interfaces;

namespace MedicalStaff.WebService.Core.Helpers.Mappers
{
    /// <summary>
    /// Represents a Medical Practioner account credentials.
    /// </summary>
    public struct MedicalAccountCredential : IMedicalAccountCredential
    {
        /// <summary>
        /// Represents the Medical Practioner account access password.
        /// </summary>
        public String Credential { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="MedicalAccountCredential"/> by providing the Medical Practioner CPF(The Brazilian national-wide unique identification number) and its account password.
        /// </summary>
        /// <param name="password">The Medical Practioner account password.</param>
        public MedicalAccountCredential(String password)
        {
            this.Credential = password;
        }

        /// <summary>
        /// Provides an empty <typeparamref name="TCast"/> instance in where <typeparamref name="TCast"/> implements <see cref="IMedicalAccountCredential"/>.
        /// </summary>
        /// <typeparam name="TCast">An <see cref="IMedicalAccountCredential"/> type.</typeparam>
        /// <returns>An empty <typeparamref name="TCast"/> instance in where <typeparamref name="TCast"/> implements <see cref="IMedicalAccountCredential"/>.</returns>
        public static TCast Empty<TCast>() where TCast : IMedicalAccountCredential
        {
            IMedicalAccountCredential EmptyDoctorCredentials = new MedicalAccountCredential();

            return (TCast)EmptyDoctorCredentials;
        }
    }
}