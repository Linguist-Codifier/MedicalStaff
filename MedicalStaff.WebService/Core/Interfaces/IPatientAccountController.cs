using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MedicalStaff.WebService.Core.Helpers.Mappers;
using MedicalStaff.WebService.Core.Models.Db.Patient;
using MedicalStaff.WebService.Core.Models.Transfer.Patient.SignUp;

namespace MedicalStaff.WebService.Core.Interfaces
{
    /// <summary>
    /// Stabilishes how a <see cref="IPatientAccountController"/> should handle its concerns.
    /// </summary>
    public interface IPatientAccountController
    {
        /// <summary>
        /// Specifies that there should be a method that provides an <see cref="IEnumerable{T}"/> <see langword="where T"/> : <see cref="PatientAccount"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> <see langword="where T"/> : <see cref="PatientAccount"/>.</returns>
        Task<ActionResult<IEnumerable<PatientAccount>>> GetPatientsAccountsAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        Task<ActionResult<PatientAccountCrendential>> GetPatientAccountAsync(String cpf);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doctorModel"></param>
        /// <returns></returns>
        Task<ActionResult<PatientAccount>> CreatePatientAsync([FromBody] PatientSignUpDTO doctorModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="targetDoctor"></param>
        /// <returns></returns>
        Task<ActionResult<PatientAccount>> UpdatePatientAsync(String cpf, [FromBody] PatientSignUpDTO targetDoctor);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        Task<ActionResult<PatientAccount>> DeletePatientAsync(String cpf);
    }
}