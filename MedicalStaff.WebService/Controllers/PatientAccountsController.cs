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
using MedicalStaff.WebService.Core.Models.Db.Physician;

namespace MedicalStaff.WebService.Controllers
{
    /// <summary>
    /// This controller is responsible for providing endpoints for accessing and manipulating any <see cref="PatientAccount"/> account. This class cannot be inherited.
    /// </summary>
    [ApiController, Route("api/patient")]
    public sealed partial class PatientAccountsController : ControllerBase, IPatientAccountEndPoints
    {
        private readonly ILogger<PatientRecordsController> logger;

        private readonly IAccountService<PatientAccount> accounts;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountsDbContext"></param>
        public PatientAccountsController(IAccountService<PatientAccount> accountsDbContext) => this.accounts = accountsDbContext;

        /// <inheritdoc/>
        [HttpGet("accounts")]
        public async Task<IActionResult> GetAllAsync() => this.Ok(await this.accounts.QueryAccountsAsync<PatientAccount>());

        /// <inheritdoc/>
        [HttpGet("account-credential/{CPF}")]
        public async Task<IActionResult> GetCredentialAsync([Required, CPF] String CPF)
        {
            IPatientAccountCredential accountCredential = await this.accounts.GetCredentialAsync<PatientAccountCrendential>(CPF);

            if (!accountCredential.IsNullOrEmpty())
                return this.Ok(accountCredential.CastFor<PatientAccountCrendential>());

            return this.NotFound();
        }

        /// <inheritdoc/>
        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAsync([Required, FromBody] PatientSignUpDTO account)
        {
            IPatientAccountCredential PatientCredentials = await this.accounts.GetCredentialAsync<PatientAccountCrendential>(account.CPF);

            if (PatientCredentials.IsNullOrEmpty())
            {
                IDbOperation<PatientAccount> accountCreation = await this.accounts.Create(new PatientAccount(account));

                if (accountCreation.OperationStatus == Core.Helpers.Properties.DbOperationsStatus.Success)
                    return this.StatusCode(HttpStatusCode.Created.ToInt32(), accountCreation.Result);
            }

            return this.StatusCode(HttpStatusCode.Forbidden.ToInt32());
        }

        /// <inheritdoc/>
        [HttpPut("account/{CPF}")]
        public async Task<IActionResult> UpdateAsync([Required, CPF] String CPF, [Required, FromBody] PatientSignUpDTO target)
        {
            PatientAccount current = await this.accounts.GetAccountAsync<PatientAccount>(CPF);

            if (current.IsValid())
            {
                current.CPF = target.CPF;
                current.Name = target.Name;
                current.Password = target.Password;
                current.Email = target.Email;

                PatientAccount UpdatedPatient = await this.accounts.UpdateAsync<PatientAccount>(new PatientAccount(current));

                if (UpdatedPatient.IsValid())
                    return this.Ok(UpdatedPatient);

                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }

            return this.NoContent();
        }

        /// <inheritdoc/>
        [HttpDelete("account/{CPF}")]
        public async Task<IActionResult> DeleteAsync([Required, CPF] String CPF)
        {
            PatientAccount CurrentPatient = await this.accounts.GetAccountAsync<PatientAccount>(CPF);

            if (CurrentPatient.IsValid())
            {
                if (await this.accounts.DeleteAsync<PatientAccount>(CPF))
                    return this.Ok(new PatientAccount(CurrentPatient));

                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }

            return this.BadRequest();
        }
    }
}