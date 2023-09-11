using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using MedicalStaff.WebService.Core.Data;
using MedicalStaff.WebService.Core.Helpers.Attributes;
using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Helpers.Mappers;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Models.Db.Patient;
using MedicalStaff.WebService.Core.Models.Transfer.Patient.SignUp;
using MedicalStaff.WebService.Core.Services.Accounts;

namespace MedicalStaff.WebService.Controllers
{
    /// <summary>
    /// This controller is responsible for providing endpoints for accessing and manipulating any Patient account. This class cannot be inherited.
    /// </summary>
    [ApiController]
    [Route("api/patient")]
    public sealed partial class PatientAccountsController : PatientAccountService, IPatientAccountController
    {
        private readonly ILogger<PatientAccountsController> _logger;

        #pragma warning disable CS1591
        public PatientAccountsController(ILogger<PatientAccountsController> logger, SystemDbContext applicationDbContext) 
            : base(applicationDbContext) => this._logger = logger;
        #pragma warning restore CS1591

        /// <summary>
        /// Retrieves the patients accounts.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Core.Models.Db.Patient.PatientAccount"/>.</returns>
        [HttpGet("accounts")]
        public async Task<ActionResult<IEnumerable<Core.Models.Db.Patient.PatientAccount>>> GetPatientsAccountsAsync()
            => this.Ok(await this.QueryAccountsAsync<Core.Models.Db.Patient.PatientAccount>());

        /// <summary>
        /// Retrieves a patient account based on the CPF of theirs.
        /// </summary>
        /// <param name="CPF">The account's CPF.</param>
        /// <returns>The requested <see cref="Core.Models.Db.Patient.PatientAccount"/>.</returns>
        [HttpGet("credential/{CPF}")]
        public async Task<ActionResult<PatientAccountCrendential>> GetPatientAccountAsync([Required][CPF] String CPF)
        {
            IPatientAccountCredential PatientAccountCredentials = await this.RequestAccountCredentialAsync<PatientAccountCrendential>(CPF);

            if (!PatientAccountCredentials.IsNullOrEmpty())
                return this.Ok((PatientAccountCrendential)PatientAccountCredentials);

            return this.NotFound();
        }

        /// <summary>
        /// Creates a medical practioner account.
        /// </summary>
        /// <param name="signUp">The patient sign up data.</param>
        /// <returns>The created <see cref="Core.Models.Db.Patient.PatientAccount"/>.</returns>
        [HttpPost("account")]
        public async Task<ActionResult<Core.Models.Db.Patient.PatientAccount>> CreatePatientAsync([Required][FromBody] PatientSignUpDTO signUp)
        {
            try
            {
                IPatientAccountCredential PatientCredentials = await this.RequestAccountCredentialAsync<PatientAccountCrendential>(signUp.CPF);

                if (PatientCredentials.IsNullOrEmpty())
                {
                    IPatientAccount Created = await this.CreateAccount<Core.Models.Db.Patient.PatientAccount>(new Core.Models.Db.Patient.PatientAccount(signUp));

                    return this.StatusCode(HttpStatusCode.Created.ToInt32(), Created);
                }

                return this.StatusCode(HttpStatusCode.Forbidden.ToInt32());
            }
            catch
            {
                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }
        }

        /// <summary>
        /// Updates a patient account.
        /// </summary>
        /// <param name="CPF">The patient CPF(The Brazilian national-wide unique identification number).</param>
        /// <param name="targetPatient">The new and/or updated patient sign up data.</param>
        /// <returns>The updated medical practioner sign up data.</returns>
        [HttpPut("account/{CPF}")]
        public async Task<ActionResult<Core.Models.Db.Patient.PatientAccount>> UpdatePatientAsync([Required][CPF] String CPF, [Required][FromBody] PatientSignUpDTO targetPatient)
        {
            Core.Models.Db.Patient.PatientAccount CurrentPatient = await this.RetrieveAccountAsync<Core.Models.Db.Patient.PatientAccount>(CPF);

            if (CurrentPatient.IsValid())
            {
                CurrentPatient.CPF = targetPatient.CPF;
                CurrentPatient.Name = targetPatient.Name;
                CurrentPatient.Password = targetPatient.Password;
                CurrentPatient.Email = targetPatient.Email;

                Core.Models.Db.Patient.PatientAccount UpdatedPatient = await this.UpdateAccountAsync<Core.Models.Db.Patient.PatientAccount>(new Core.Models.Db.Patient.PatientAccount(CurrentPatient));

                if (UpdatedPatient.IsValid())
                    return this.Ok(UpdatedPatient);

                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }

            return this.NoContent();
        }

        /// <summary>
        /// Deletes a patient accounnt.
        /// </summary>
        /// <param name="CPF">The patient CPF(The Brazilian national-wide unique identification number).</param>
        /// <returns>The deleted patient sign up data.</returns>
        [HttpDelete("account/{CPF}")]
        public async Task<ActionResult<Core.Models.Db.Patient.PatientAccount>> DeletePatientAsync([Required][CPF] String CPF)
        {
            Core.Models.Db.Patient.PatientAccount CurrentPatient = await this.RetrieveAccountAsync<Core.Models.Db.Patient.PatientAccount>(CPF);

            if (CurrentPatient.IsValid())
            {
                if (await this.DeleteAccount<Core.Models.Db.Patient.PatientAccount>(CPF))
                    return this.Ok(new Core.Models.Db.Patient.PatientAccount(CurrentPatient));

                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }

            return this.BadRequest();
        }
    }
}