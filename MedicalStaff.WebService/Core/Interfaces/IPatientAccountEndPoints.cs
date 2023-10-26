using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MedicalStaff.WebService.Core.Models.Db.Patient;
using MedicalStaff.WebService.Core.Models.Transfer.Patient.SignUp;

namespace MedicalStaff.WebService.Core.Interfaces
{
    /// <summary>
    /// Stabilishes the default endpoints for <see cref="PatientAccount"/> controller and how it must treat its workloads.
    /// </summary>
    public interface IPatientAccountEndPoints
    {
        /// <summary>
        /// Get asynchronously all <see cref="PatientAccount"/> accounts.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> : <see cref="PatientAccount"/>.</returns>
        Task<IActionResult> GetAllAsync();

        /// <summary>
        /// Get asynchronously a <see cref="PatientAccount"/> account by <paramref name="CPF"/>.
        /// </summary>
        /// <param name="CPF">The <paramref name="CPF"/> associated with this <see cref="PatientAccount"/>.</param>
        /// <returns>A <see cref="PatientAccount"/> account.</returns>
        Task<IActionResult> GetCredentialAsync(String CPF);

        /// <summary>
        /// Creates a <see cref="PatientAccount"/> account.
        /// </summary>
        /// <param name="account">The <see cref="PatientAccount"/> account to be created casted as its corresponding data transfer object.</param>
        /// <returns>The <see cref="PatientAccount"/> account just created.</returns>
        Task<IActionResult> CreateAsync([FromBody] PatientSignUpDTO account);

        /// <summary>
        /// Updates a <see cref="PatientAccount"/> account.
        /// </summary>
        /// <param name="CPF">The <paramref name="CPF"/> associated with this <see cref="PatientAccount"/>.</param>
        /// <param name="target">The <see cref="PatientAccount"/> account to be updated casted as its corresponding data transfer object.</param>
        /// <returns>The <see cref="PatientAccount"/> account just updated.</returns>
        Task<IActionResult> UpdateAsync(String CPF, [FromBody] PatientSignUpDTO target);

        /// <summary>
        /// Deletes a <see cref="PatientAccount"/> account.
        /// </summary>
        /// <param name="CPF">The <paramref name="CPF"/> associated with this <see cref="PatientAccount"/>.</param>
        /// <returns>The <see cref="PatientAccount"/> account just deleted.</returns>
        Task<IActionResult> DeleteAsync(String CPF);
    }
}