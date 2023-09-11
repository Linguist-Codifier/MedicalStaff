using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MedicalStaff.WebService.Core.Data;
using System.ComponentModel.DataAnnotations;
using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Helpers.Mappers;
using MedicalStaff.WebService.Core.Services.Accounts;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Helpers.Attributes;
using MedicalStaff.WebService.Core.Models.Db.Physician;
using MedicalStaff.WebService.Core.Models.Transfer.Physician.SignUp;

namespace MedicalStaff.WebService.Controllers
{
    /// <summary>
    /// This controller is responsible for providing endpoints for accessing and manipulating any Medical Practioner account. This class cannot be inherited.
    /// </summary>
    [ApiController]
    [Route("api/physician")]
    public sealed partial class PhysicianAccountsController : PhysicianAccountService, IPhysicianAccountController
    {
        private readonly ILogger<PhysicianAccountsController> _logger;

        #pragma warning disable CS1591
        public PhysicianAccountsController(ILogger<PhysicianAccountsController> logger, SystemDbContext applicationDbContext) 
            : base(applicationDbContext) => this._logger = logger;
        #pragma warning restore CS1591

        /// <summary>
        /// Retrieves the medical practioner accounts.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="PhysicianAccount"/>.</returns>
        [HttpGet("accounts")]
        public async Task<ActionResult<IEnumerable<PhysicianAccount>>> GetMedicalPractionersAccountsAsync()
            => this.Ok(await this.QueryAccountsAsync<PhysicianAccount>());

        /// <summary>
        /// Retrieves any medical practioner account by the CPF.
        /// </summary>
        /// <param name="CPF">The associated account's CPF.</param>
        /// <returns>The requested <see cref="PhysicianAccount"/>.</returns>
        [HttpGet("credential/{CPF}")]
        public async Task<ActionResult<MedicalAccountCredential>> GetMedicalPractionerAccountAsync([Required][CPF] String CPF)
        {
            IMedicalAccountCredential MedicalPractionerAccountCredentials = await this.RequestAccountCredentialAsync<MedicalAccountCredential>(CPF);

            if (!MedicalPractionerAccountCredentials.IsNullOrEmpty())
                return this.Ok((MedicalAccountCredential)MedicalPractionerAccountCredentials);

            return this.NotFound();
        }

        /// <summary>
        /// Creates a medical practioner account.
        /// </summary>
        /// <param name="signUp">The medical practioner sign up data.</param>
        /// <returns>The created <see cref="PhysicianAccount"/>.</returns>
        [HttpPost("account")]
        public async Task<ActionResult<PhysicianAccount>> CreateMedicalPractionerAccountAsync([Required][FromBody] PhysicianAccountDTO signUp)
        {
            try
            {
                IMedicalAccountCredential MedicalPractionerCredentials = await this.RequestAccountCredentialAsync<MedicalAccountCredential>(signUp.CPF);

                if (MedicalPractionerCredentials.IsNullOrEmpty())
                {
                    IPhysicianAccount CreatedMedicalPractioner = await this.CreateAccount<PhysicianAccount>(new PhysicianAccount(signUp));

                    return this.StatusCode(HttpStatusCode.Created.ToInt32(), CreatedMedicalPractioner);
                }

                return this.StatusCode(HttpStatusCode.Forbidden.ToInt32());
            }
            catch
            {
                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }
        }

        /// <summary>
        /// Updates a medical practioner account.
        /// </summary>
        /// <param name="CPF">The medical practioner CPF(The Brazilian national-wide unique identification number).</param>
        /// <param name="targetMedicalPractioner">The new and/or updated medical practioner sign up data.</param>
        /// <returns>The updated medical practioner sign up data.</returns>
        [HttpPut("account/{CPF}")]
        public async Task<ActionResult<PhysicianAccount>> UpdateMedicalPractionerAccountAsync([Required][CPF]  String CPF, [FromBody][Required] PhysicianAccountDTO targetMedicalPractioner)
        {
            PhysicianAccount CurrentMedicalPractioner = await this.RetrieveAccountAsync<PhysicianAccount>(CPF);

            if (CurrentMedicalPractioner.IsValid())
            {
                CurrentMedicalPractioner.CPF = targetMedicalPractioner.CPF;
                CurrentMedicalPractioner.CRM = targetMedicalPractioner.CRM;
                CurrentMedicalPractioner.Name = targetMedicalPractioner.Name;
                CurrentMedicalPractioner.Password = targetMedicalPractioner.Password;
                CurrentMedicalPractioner.Email = targetMedicalPractioner.Email;

                PhysicianAccount UpdatedCurrentMedicalPractioner = await this.UpdateAccountAsync<PhysicianAccount>(new PhysicianAccount(CurrentMedicalPractioner));

                if (UpdatedCurrentMedicalPractioner.IsValid())
                    return this.Ok(UpdatedCurrentMedicalPractioner);

                return this.BadRequest();
            }
            return this.BadRequest();
        }

        /// <summary>
        /// Deletes a medical practioenr accounnt.
        /// </summary>
        /// <param name="CPF">The medical practioner CPF (The Brazilian national-wide unique identification number).</param>
        /// <returns>The deleted medical practioner sign up data.</returns>
        [HttpDelete("account/{CPF}")]
        public async Task<ActionResult<PhysicianAccount>> DeleteMedicalPractionerAsync([Required][CPF] String CPF)
        {
            PhysicianAccount CurrentMedicalPractioner = await this.RetrieveAccountAsync<PhysicianAccount>(CPF);

            if (CurrentMedicalPractioner.IsValid())
            {
                if (await this.DeleteAccount<PhysicianAccount>(CPF))
                    return this.Ok(new PhysicianAccount(CurrentMedicalPractioner));

                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }

            return this.BadRequest();
        }
    }
}