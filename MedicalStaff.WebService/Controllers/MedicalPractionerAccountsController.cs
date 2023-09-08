using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MedicalRecordsSystem.WebService.Core.Data;
using MedicalRecordsSystem.WebService.Core.Interfaces;
using MedicalRecordsSystem.WebService.Core.Helpers.Mappers;
using MedicalRecordsSystem.WebService.Core.Helpers.Analysers;
using MedicalRecordsSystem.WebService.Core.Helpers.Properties;
using MedicalRecordsSystem.WebService.Core.Services.MedicalPractioner;
using MedicalRecordsSystem.WebService.Core.Models.Db.MedicalPractioner;
using MedicalRecordsSystem.WebService.Core.Models.Transfer.MedicalPractioner.SignUp;

namespace MedicalRecordsSystem.WebService.Controllers
{
    /// <summary>
    /// This class is responsible for providing endpoints for accessing and manipulating any Medical Practioner account. This class cannot be inherited.
    /// </summary>
    [ApiController]
    [Route("api/medical-practioner")]
    public sealed partial class MedicalPractionerAccountsController : MedicalAccountsServices, IMedicalPractionerAccountController
    {
        private readonly ILogger<MedicalPractionerAccountsController> logger;

        #pragma warning disable CS1591
        public MedicalPractionerAccountsController(ILogger<MedicalPractionerAccountsController> logger, SystemDbContext applicationDbContext) : base(applicationDbContext)
            => this.logger = logger;
        #pragma warning restore CS1591

        /// <summary>
        /// Retrieves the medical practioner accounts.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="MedicalPractionerAccount"/>.</returns>
        [HttpGet("accounts")]
        public async Task<ActionResult<IEnumerable<MedicalPractionerAccount>>> GetMedicalPractionersAccountsAsync()
            => this.Ok(await this.QueryAccountsAsync<MedicalPractionerAccount>());

        /// <summary>
        /// Retrieves any medical practioner account by the CPF.
        /// </summary>
        /// <param name="CPF">The associated account's CPF.</param>
        /// <returns>The requested <see cref="MedicalPractionerAccount"/>.</returns>
        [HttpGet("credential/{CPF}")]
        public async Task<ActionResult<MedicalAccountCredential>> GetMedicalPractionerAccountAsync([FromRoute] String CPF)
        {
            if (RegularExpressionsUtility.Matches(ExpressionType.CPF, CPF))
            {
                IMedicalAccountCredential MedicalPractionerAccountCredentials = await this.RequestAccountCredentialAsync<MedicalAccountCredential>(CPF);

                if (!MedicalPractionerAccountCredentials.IsNullOrEmpty())
                    return this.Ok((MedicalAccountCredential)MedicalPractionerAccountCredentials);

                return this.NotFound(null);
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Creates a medical practioner account.
        /// </summary>
        /// <param name="onSignUp">The medical practioner sign up data.</param>
        /// <returns>The created <see cref="MedicalPractionerAccount"/>.</returns>
        [HttpPost("account")]
        public async Task<ActionResult<MedicalPractionerAccount>> CreateMedicalPractionerAccountAsync([FromBody] MedicalPractionerSignUp onSignUp)
        {
            try
            {
                if (RegularExpressionsUtility.Matches(ExpressionType.CPF, onSignUp.CPF) && RegularExpressionsUtility.Matches(ExpressionType.CRM, onSignUp.CRM))
                {
                    IMedicalAccountCredential MedicalPractionerCredentials = await this.RequestAccountCredentialAsync<MedicalAccountCredential>(onSignUp.CPF);

                    if (MedicalPractionerCredentials.IsNullOrEmpty())
                    {
                        IMedicalPractionerAccount CreatedMedicalPractioner = await this.CreateAccount<MedicalPractionerAccount>(new MedicalPractionerAccount(onSignUp));

                        return this.StatusCode(HttpStatusCode.Created.ToInt32(), CreatedMedicalPractioner);
                    }

                    return this.StatusCode(HttpStatusCode.Forbidden.ToInt32());
                }

                return this.BadRequest();
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
        public async Task<ActionResult<MedicalPractionerAccount>> UpdateMedicalPractionerAccountAsync([FromRoute] String CPF, [FromBody] MedicalPractionerSignUp targetMedicalPractioner)
        {
            if (RegularExpressionsUtility.Matches(ExpressionType.CPF, CPF) && RegularExpressionsUtility.Matches(ExpressionType.CRM, targetMedicalPractioner.CRM))
            {
                MedicalPractionerAccount CurrentMedicalPractionerAccount = await this.RetrieveAccountAsync<MedicalPractionerAccount>(CPF);

                if (CurrentMedicalPractionerAccount.IsValidAccount())
                {
                    CurrentMedicalPractionerAccount.CPF = targetMedicalPractioner.CPF;
                    CurrentMedicalPractionerAccount.CRM = targetMedicalPractioner.CRM;
                    CurrentMedicalPractionerAccount.Name = targetMedicalPractioner.Name;
                    CurrentMedicalPractionerAccount.Password = targetMedicalPractioner.Password;
                    CurrentMedicalPractionerAccount.Email = targetMedicalPractioner.Email;

                    MedicalPractionerAccount UpdatedMedicalPractionerAccount = await this.UpdateAccountAsync<MedicalPractionerAccount>(new MedicalPractionerAccount(CurrentMedicalPractionerAccount));

                    if (UpdatedMedicalPractionerAccount.IsValidAccount())
                        return this.Ok(UpdatedMedicalPractionerAccount);

                    return this.BadRequest();
                }
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Deletes a medical practioenr accounnt.
        /// </summary>
        /// <param name="CPF">The medical practioner CPF(The Brazilian national-wide unique identification number).</param>
        /// <returns>The deleted medical practioner sign up data.</returns>
        [HttpDelete("account/{CPF}")]
        public async Task<ActionResult<MedicalPractionerAccount>> DeleteMedicalPractionerAsync([FromRoute] String CPF)
        {
            if (RegularExpressionsUtility.Matches(ExpressionType.CPF, CPF))
            {
                MedicalPractionerAccount CurrentMedicalPractioner = await this.RetrieveAccountAsync<MedicalPractionerAccount>(CPF);

                if (CurrentMedicalPractioner.IsValidAccount())
                {
                    if(await this.DeleteAccount<MedicalPractionerAccount>(CPF))
                        return this.Ok(new MedicalPractionerAccount(CurrentMedicalPractioner));
                }

                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }

            return this.BadRequest();
        }
    }
}