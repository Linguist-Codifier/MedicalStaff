using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MedicalStaff.WebService.Core.Models.Db.Physician;
using MedicalStaff.WebService.Core.Models.Transfer.Physician.SignUp;

namespace MedicalStaff.WebService.Core.Interfaces
{
    /// <summary>
    /// Stabilishes the default endpoints for <see cref="PhysicianAccount"/> controller and how it must treat its workloads.
    /// </summary>
    public interface IPhysicianAccountEndPoints
    {
        /// <summary>
        /// Get asynchronously all <see cref="PhysicianAccount"/> accounts.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> : <see cref="PhysicianAccount"/>.</returns>
        Task<IActionResult> GetPhysicianAccountsAsync();

        /// <summary>
        /// Get asynchronously a <see cref="PhysicianAccount"/> account by <paramref name="CPF"/>.
        /// </summary>
        /// <param name="CPF">The <paramref name="CPF"/> associated with this <see cref="PhysicianAccount"/>.</param>
        /// <returns>A <see cref="PhysicianAccount"/> account.</returns>
        Task<IActionResult> GetPhysicianAccountAsync(String CPF);

        /// <summary>
        /// Creates a <see cref="PhysicianAccount"/> account.
        /// </summary>
        /// <param name="account">The <see cref="PhysicianAccount"/> account to be created casted as its corresponding data transfer object.</param>
        /// <returns>The <see cref="PhysicianAccount"/> account just created.</returns>
        Task<IActionResult> CreatePhysicianAccountAsync([FromBody] PhysicianAccountDTO account);

        /// <summary>
        /// Updates a <see cref="PhysicianAccount"/> account.
        /// </summary>
        /// <param name="CPF">The <paramref name="CPF"/> associated with this <see cref="PhysicianAccount"/>.</param>
        /// <param name="target">The <see cref="PhysicianAccount"/> account to be updated casted as its corresponding data transfer object.</param>
        /// <returns>The <see cref="PhysicianAccount"/> account just updated.</returns>
        Task<IActionResult> UpdatePhysicianAccountAsync(String CPF, [FromBody] PhysicianAccountDTO target);

        /// <summary>
        /// Deletes a <see cref="PhysicianAccount"/> account.
        /// </summary>
        /// <param name="CPF">The <paramref name="CPF"/> associated with this <see cref="PhysicianAccount"/>.</param>
        /// <returns>The <see cref="PhysicianAccount"/> account just deleted.</returns>
        Task<IActionResult> DeletePhysicianAsync(String CPF);
    }
}