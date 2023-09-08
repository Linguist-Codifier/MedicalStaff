using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MedicalRecordsSystem.WebService.Core.Data;
using MedicalRecordsSystem.WebService.Core.Interfaces;
using MedicalRecordsSystem.WebService.Core.Helpers.Mappers;
using MedicalRecordsSystem.WebService.Core.Services.Patient;
using MedicalRecordsSystem.WebService.Core.Helpers.Analysers;
using MedicalRecordsSystem.WebService.Core.Helpers.Properties;
using MedicalRecordsSystem.WebService.Core.Models.Db.Patient;
using MedicalRecordsSystem.WebService.Core.Models.Transfer.Patient.SignUp;

namespace MedicalRecordsSystem.WebService.Controllers
{
    /// <summary>
    /// This class is responsible for providing endpoints for accessing and manipulating any Patient account. This class cannot be inherited.
    /// </summary>
    [ApiController]
    [Route("api/patient")]
    public sealed partial class PatientAccountsController : PatientAccountsServices, IPatientAccountController
    {
        private readonly ILogger<PatientAccountsController> logger;

        #pragma warning disable CS1591
        public PatientAccountsController(ILogger<PatientAccountsController> logger, SystemDbContext applicationDbContext) : base(applicationDbContext)
            => this.logger = logger;
        #pragma warning restore CS1591

        /// <summary>
        /// Retrieves the patients accounts.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="PatientAccount"/>.</returns>
        [HttpGet("accounts")]
        public async Task<ActionResult<IEnumerable<PatientAccount>>> GetPatientsAccountsAsync()
            => this.Ok(await this.QueryAccountsAsync<PatientAccount>());

        /// <summary>
        /// Retrieves a patient account based on the CPF of theirs.
        /// </summary>
        /// <param name="CPF">The account's CPF.</param>
        /// <returns>The requested <see cref="PatientAccount"/>.</returns>
        [HttpGet("credential/{CPF}")]
        public async Task<ActionResult<PatientAccountCrendential>> GetPatientAccountAsync([FromRoute] String CPF)
        {
            if (RegularExpressionsUtility.Matches(ExpressionType.CPF, CPF))
            {
                IPatientAccountCredential PatientAccountCredentials = await this.RequestAccountCredentialAsync<PatientAccountCrendential>(CPF);

                if (!PatientAccountCredentials.IsNullOrEmpty())
                    return this.Ok((PatientAccountCrendential)PatientAccountCredentials);

                return this.NotFound(null);
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Creates a medical practioner account.
        /// </summary>
        /// <param name="onSignUp">The patient sign up data.</param>
        /// <returns>The created <see cref="PatientAccount"/>.</returns>
        [HttpPost("account")]
        public async Task<ActionResult<PatientAccount>> CreatePatientAsync([FromBody] PatientSignUp onSignUp)
        {
            try
            {
                if (RegularExpressionsUtility.Matches(ExpressionType.CPF, onSignUp.CPF))
                {
                    IPatientAccountCredential PatientCredentials = await this.RequestAccountCredentialAsync<PatientAccountCrendential>(onSignUp.CPF);

                    if (PatientCredentials.IsNullOrEmpty())
                    {
                        IPatientAccount Created = await this.CreateAccount<PatientAccount>(new PatientAccount(onSignUp));

                        return this.StatusCode(HttpStatusCode.Created.ToInt32(), Created);
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
        /// Updates a patient account.
        /// </summary>
        /// <param name="CPF">The patient CPF(The Brazilian national-wide unique identification number).</param>
        /// <param name="targetPatient">The new and/or updated patient sign up data.</param>
        /// <returns>The updated medical practioner sign up data.</returns>
        [HttpPut("account/{CPF}")]
        public async Task<ActionResult<PatientAccount>> UpdatePatientAsync([FromRoute] String CPF, [FromBody] PatientSignUp targetPatient)
        {
            if (RegularExpressionsUtility.Matches(ExpressionType.CPF, CPF))
            {
                PatientAccount CurrentPatient = await this.RetrieveAccountAsync<PatientAccount>(CPF);

                if (CurrentPatient.IsValidAccount())
                {
                    CurrentPatient.CPF = targetPatient.CPF;
                    CurrentPatient.Name = targetPatient.Name;
                    CurrentPatient.Password = targetPatient.Password;
                    CurrentPatient.Email = targetPatient.Email;

                    PatientAccount UpdatedPatient = await this.UpdateAccountAsync<PatientAccount>(new PatientAccount(CurrentPatient));

                    if (UpdatedPatient.IsValidAccount())
                        return this.Ok(UpdatedPatient);

                    return this.BadRequest();
                }
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Deletes a patient accounnt.
        /// </summary>
        /// <param name="CPF">The patient CPF(The Brazilian national-wide unique identification number).</param>
        /// <returns>The deleted patient sign up data.</returns>
        [HttpDelete("account/{CPF}")]
        public async Task<ActionResult<PatientAccount>> DeletePatientAsync([FromRoute]String CPF)
        {
            if (RegularExpressionsUtility.Matches(ExpressionType.CPF, CPF))
            {
                PatientAccount CurrentPatient = await this.RetrieveAccountAsync<PatientAccount>(CPF);

                if (CurrentPatient.IsValidAccount())
                {
                    if (await this.DeleteAccount<PatientAccount>(CPF))
                        return this.Ok(new PatientAccount(CurrentPatient));

                    return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
                }
            }

            return this.BadRequest();
        }
    }
}