using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MedicalStaff.WebService.Core.Data;
using System.ComponentModel.DataAnnotations;
using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Helpers.Mappers;
using MedicalStaff.WebService.Core.Services.Accounts;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Helpers.Filters;
using MedicalStaff.WebService.Core.Models.Db.Physician;
using MedicalStaff.WebService.Core.Models.Transfer.Physician.SignUp;
using MedicalStaff.WebService.Core.Infrastructure.Accounts;

namespace MedicalStaff.WebService.Controllers
{
    /// <summary>
    /// This controller is responsible for providing endpoints for accessing and manipulating any Medical Practitioner account. This class cannot be inherited.
    /// </summary>
    [ApiController, Route("api/physician")]
    public sealed partial class PhysicianAccountsController : ControllerBase, IPhysicianAccountEndPoints
    {
        private readonly IAccountService<PhysicianAccount> accounts;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDbContext"></param>
        public PhysicianAccountsController(SystemDbContext applicationDbContext) => this.accounts = new AccountService<PhysicianAccount>(applicationDbContext);

        /// <inheritdoc/>
        [HttpGet("accounts")]
        public async Task<IActionResult> GetAllAsync() => this.Ok(await this.accounts.QueryAccountsAsync<PhysicianAccount>());

        /// <inheritdoc/>
        [HttpGet("account-credential/{CPF}")]
        public async Task<IActionResult> GetCredentialAsync([Required, CPF] String CPF)
        {
            IMedicalAccountCredential accountCredential = await this.accounts.GetCredentialAsync<MedicalAccountCredential>(CPF);

            if (!accountCredential.IsNullOrEmpty())
                return this.Ok(accountCredential.CastFor<MedicalAccountCredential>());

            return this.NotFound();
        }

        /// <inheritdoc/>
        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAsync([Required, FromBody] PhysicianAccountDTO account)
        {
            IMedicalAccountCredential accountCredential = await this.accounts.GetCredentialAsync<MedicalAccountCredential>(account.CPF);

            if (accountCredential.IsNullOrEmpty())
            {
                IDbOperation<PhysicianAccount> accountCreation = await this.accounts.Create(new PhysicianAccount(account));

                if (accountCreation.OperationStatus == Core.Helpers.Properties.DbOperationsStatus.Success)
                    return this.StatusCode(HttpStatusCode.Created.ToInt32(), accountCreation.Result);
            }

            return this.StatusCode(HttpStatusCode.Forbidden.ToInt32());
        }

        /// <inheritdoc/>
        [HttpPut("account/{CPF}")]
        public async Task<IActionResult> UpdateAsync([Required, CPF] String CPF, [FromBody, Required] PhysicianAccountDTO target)
        {
            PhysicianAccount current = await this.accounts.GetAccountAsync<PhysicianAccount>(CPF);

            if (current.IsValid())
            {
                current.CPF = target.CPF;
                current.CRM = target.CRM;
                current.Name = target.Name;
                current.Password = target.Password;
                current.Email = target.Email;

                PhysicianAccount updated = await this.accounts.UpdateAsync<PhysicianAccount>(new PhysicianAccount(current));

                if (updated.IsValid())
                    return this.Ok(updated);

                return this.BadRequest();
            }
            return this.BadRequest();
        }

        /// <inheritdoc/>
        [HttpDelete("account/{CPF}")]
        public async Task<IActionResult> DeleteAsync([Required, CPF] String CPF)
        {
            PhysicianAccount current = await this.accounts.GetAccountAsync<PhysicianAccount>(CPF);

            if (current.IsValid())
            {
                if (await this.accounts.DeleteAsync<PhysicianAccount>(CPF))
                    return this.Ok(new PhysicianAccount(current));

                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }

            return this.BadRequest();
        }
    }
}