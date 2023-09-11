using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MedicalStaff.WebService.Controllers;
using MedicalStaff.WebService.Core.Helpers.Mappers;
using MedicalStaff.WebService.Core.Models.Db.Physician;
using MedicalStaff.WebService.Core.Models.Transfer.Physician.SignUp;

namespace MedicalStaff.WebService.Core.Interfaces
{
    /// <summary>
    /// Stabilishes how a <see cref="PhysicianAccountsController"/> should handle its concerns.
    /// </summary>
    public interface IPhysicianAccountController
    {
        /// <summary>
        /// Specifies that there should be a method that provides an <see cref="IEnumerable{T}"/> <see langword="where T"/> : <see cref="PhysicianAccount"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> <see langword="where T"/> : <see cref="PhysicianAccount"/>.</returns>
        Task<ActionResult<IEnumerable<PhysicianAccount>>> GetMedicalPractionersAccountsAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        Task<ActionResult<MedicalAccountCredential>> GetMedicalPractionerAccountAsync(String cpf);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doctorModel"></param>
        /// <returns></returns>
        Task<ActionResult<PhysicianAccount>> CreateMedicalPractionerAccountAsync([FromBody] PhysicianAccountDTO doctorModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="targetDoctor"></param>
        /// <returns></returns>
        Task<ActionResult<PhysicianAccount>> UpdateMedicalPractionerAccountAsync(String cpf, [FromBody] PhysicianAccountDTO targetDoctor);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        Task<ActionResult<PhysicianAccount>> DeleteMedicalPractionerAsync(String cpf);
    }
}