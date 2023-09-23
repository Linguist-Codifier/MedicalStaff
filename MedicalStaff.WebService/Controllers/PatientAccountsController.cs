using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MedicalStaff.WebService.Core.Data;
using System.ComponentModel.DataAnnotations;
using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Helpers.Mappers;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Services.Accounts;
using MedicalStaff.WebService.Core.Models.Db.Patient;
using MedicalStaff.WebService.Core.Helpers.Filters;
using MedicalStaff.WebService.Core.Models.Transfer.Patient.SignUp;

namespace MedicalStaff.WebService.Controllers
{
    /// <summary>
    /// This controller is responsible for providing endpoints for accessing and manipulating any <see cref="PatientAccount"/> account. This class cannot be inherited.
    /// </summary>
    [ApiController, Route("api/patient")]
    public sealed partial class PatientAccountsController : PatientAccountService, IPatientAccountEndPoints
    {
        private readonly ILogger<PatientAccountsController> _logger;

        #pragma warning disable CS1591
        public PatientAccountsController(ILogger<PatientAccountsController> logger, SystemDbContext applicationDbContext) 
            : base(applicationDbContext) => this._logger = logger;
        #pragma warning restore CS1591

        /// <inheritdoc/>
        [HttpGet("accounts")]
        public async Task<IActionResult> GetPatientsAccountsAsync()
            => this.Ok(await this.QueryAccountsAsync<PatientAccount>());

        /// <inheritdoc/>
        [HttpGet("credential/{CPF}")]
        public async Task<IActionResult> GetPatientAccountAsync([Required][CPF] String CPF)
        {
            IPatientAccountCredential PatientAccountCredentials = await this.RequestAccountCredentialAsync<PatientAccountCrendential>(CPF);

            if (!PatientAccountCredentials.IsNullOrEmpty())
                return this.Ok((PatientAccountCrendential)PatientAccountCredentials);

            return this.NotFound();
        }

        /// <inheritdoc/>
        [HttpPost("account")]
        public async Task<IActionResult> CreatePatientAsync([Required][FromBody] PatientSignUpDTO account)
        {
            try
            {
                IPatientAccountCredential PatientCredentials = await this.RequestAccountCredentialAsync<PatientAccountCrendential>(account.CPF);

                if (PatientCredentials.IsNullOrEmpty())
                {
                    IPatientAccount Created = await this.CreateAccount<PatientAccount>(new PatientAccount(account));

                    return this.StatusCode(HttpStatusCode.Created.ToInt32(), Created);
                }

                return this.StatusCode(HttpStatusCode.Forbidden.ToInt32());
            }
            catch
            {
                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }
        }

        /// <inheritdoc/>
        [HttpPut("account/{CPF}")]
        public async Task<IActionResult> UpdatePatientAsync([Required][CPF] String CPF, [Required][FromBody] PatientSignUpDTO target)
        {
            PatientAccount CurrentPatient = await this.RetrieveAccountAsync<PatientAccount>(CPF);

            if (CurrentPatient.IsValid())
            {
                CurrentPatient.CPF = target.CPF;
                CurrentPatient.Name = target.Name;
                CurrentPatient.Password = target.Password;
                CurrentPatient.Email = target.Email;

                PatientAccount UpdatedPatient = await this.UpdateAccountAsync<PatientAccount>(new PatientAccount(CurrentPatient));

                if (UpdatedPatient.IsValid())
                    return this.Ok(UpdatedPatient);

                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }

            return this.NoContent();
        }

        /// <inheritdoc/>
        [HttpDelete("account/{CPF}")]
        public async Task<IActionResult> DeletePatientAsync([Required][CPF] String CPF)
        {
            PatientAccount CurrentPatient = await this.RetrieveAccountAsync<PatientAccount>(CPF);

            if (CurrentPatient.IsValid())
            {
                if (await this.DeleteAccount<PatientAccount>(CPF))
                    return this.Ok(new PatientAccount(CurrentPatient));

                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }

            return this.BadRequest();
        }
    }
}