using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MedicalRecordsSystem.WebService.Controllers;
using MedicalRecordsSystem.WebService.Core.Helpers.Mappers;
using MedicalRecordsSystem.WebService.Core.Models.Db.MedicalPractioner;
using MedicalRecordsSystem.WebService.Core.Models.Transfer.MedicalPractioner.SignUp;

namespace MedicalRecordsSystem.WebService.Core.Interfaces
{
    /// <summary>
    /// Stabilishes how a <see cref="MedicalPractionerAccountsController"/> should handle its concerns.
    /// </summary>
    public interface IMedicalPractionerAccountController
    {
        /// <summary>
        /// Specifies that there should be a method that provides an <see cref="IEnumerable{T}"/> <see langword="where T"/> : <see cref="MedicalPractionerAccount"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> <see langword="where T"/> : <see cref="MedicalPractionerAccount"/>.</returns>
        Task<ActionResult<IEnumerable<MedicalPractionerAccount>>> GetMedicalPractionersAccountsAsync();

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
        Task<ActionResult<MedicalPractionerAccount>> CreateMedicalPractionerAccountAsync([FromBody] MedicalPractionerSignUp doctorModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="targetDoctor"></param>
        /// <returns></returns>
        Task<ActionResult<MedicalPractionerAccount>> UpdateMedicalPractionerAccountAsync(String cpf, [FromBody] MedicalPractionerSignUp targetDoctor);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        Task<ActionResult<MedicalPractionerAccount>> DeleteMedicalPractionerAsync(String cpf);
    }
}