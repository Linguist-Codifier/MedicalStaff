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

namespace MedicalStaff.WebService.Controllers
{
    /// <summary>
    /// This controller is responsible for providing endpoints for accessing and manipulating any Medical Practioner account. This class cannot be inherited.
    /// </summary>
    [ApiController]
    [Route("api/physician")]
    public sealed partial class PhysicianAccountsController : PhysicianAccountService, IPhysicianAccountEndPoints
    {
        private readonly ILogger<PhysicianAccountsController> _logger;

        #pragma warning disable CS1591
        public PhysicianAccountsController(ILogger<PhysicianAccountsController> logger, SystemDbContext applicationDbContext) 
            : base(applicationDbContext) => this._logger = logger;
        #pragma warning restore CS1591

        /// <inheritdoc/>
        [HttpGet("accounts")]
        public async Task<IActionResult> GetPhysicianAccountsAsync()
            => this.Ok(await this.QueryAccountsAsync<PhysicianAccount>());

        /// <inheritdoc/>
        [HttpGet("credential/{CPF}")]
        public async Task<IActionResult> GetPhysicianAccountAsync([Required][CPF] String CPF)
        {
            IMedicalAccountCredential PhysicianCredentials = await this.RequestAccountCredentialAsync<MedicalAccountCredential>(CPF);

            if (!PhysicianCredentials.IsNullOrEmpty())
                return this.Ok((MedicalAccountCredential)PhysicianCredentials);

            return this.NotFound();
        }

        /// <inheritdoc/>
        [HttpPost("account")]
        public async Task<IActionResult> CreatePhysicianAccountAsync([Required][FromBody] PhysicianAccountDTO account)
        {
            try
            {
                IMedicalAccountCredential PhysicianCredentials = await this.RequestAccountCredentialAsync<MedicalAccountCredential>(account.CPF);

                if (PhysicianCredentials.IsNullOrEmpty())
                {
                    IPhysicianAccount CreatedPhysician = await this.CreateAccount<PhysicianAccount>(new PhysicianAccount(account));

                    return this.StatusCode(HttpStatusCode.Created.ToInt32(), CreatedPhysician);
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
        public async Task<IActionResult> UpdatePhysicianAccountAsync([Required][CPF]  String CPF, [FromBody][Required] PhysicianAccountDTO target)
        {
            PhysicianAccount CurrentPhysician = await this.RetrieveAccountAsync<PhysicianAccount>(CPF);

            if (CurrentPhysician.IsValid())
            {
                CurrentPhysician.CPF = target.CPF;
                CurrentPhysician.CRM = target.CRM;
                CurrentPhysician.Name = target.Name;
                CurrentPhysician.Password = target.Password;
                CurrentPhysician.Email = target.Email;

                PhysicianAccount UpdatedCurrentPhysician = await this.UpdateAccountAsync<PhysicianAccount>(new PhysicianAccount(CurrentPhysician));

                if (UpdatedCurrentPhysician.IsValid())
                    return this.Ok(UpdatedCurrentPhysician);

                return this.BadRequest();
            }
            return this.BadRequest();
        }

        /// <inheritdoc/>
        [HttpDelete("account/{CPF}")]
        public async Task<IActionResult> DeletePhysicianAsync([Required][CPF] String CPF)
        {
            PhysicianAccount CurrentPhysician = await this.RetrieveAccountAsync<PhysicianAccount>(CPF);

            if (CurrentPhysician.IsValid())
            {
                if (await this.DeleteAccount<PhysicianAccount>(CPF))
                    return this.Ok(new PhysicianAccount(CurrentPhysician));

                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }

            return this.BadRequest();
        }
    }
}