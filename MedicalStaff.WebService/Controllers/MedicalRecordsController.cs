using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MedicalRecordsSystem.WebService.Core.Helpers.Analysers;
using MedicalRecordsSystem.WebService.Core.Helpers.Properties;
using MedicalRecordsSystem.WebService.Core.Infrastructure.MedicalRecords;
using MedicalRecordsSystem.WebService.Core.Models.Db.MedicalRecord;

namespace MedicalRecordsSystem.WebService.Controllers
{
    /// <summary>
    /// The medical records controller.
    /// </summary>
    [ApiController]
    [Route(template: "api")]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly ILogger<MedicalRecordsController> _logger;

        private readonly MedicalRecordsDbContext MedicalPractionerAccountsContext;

        #pragma warning disable CS1591
        public MedicalRecordsController(ILogger<MedicalRecordsController> logger, MedicalRecordsDbContext medicalRecordsContext)
        {
            this._logger = logger;
            this.MedicalPractionerAccountsContext = medicalRecordsContext;
        }
        #pragma warning restore CS1591

        /// <summary>
        /// Retrieves all medical records of the specified account's CPF.
        /// </summary>
        /// <param name="CPF">The account's CPF.</param>
        /// <returns>An <see cref="IList{T}"/> of <see cref="MedicalRecord"/>.</returns>
        [HttpGet(template: "medical-records/{CPF}")]
        public async Task<ActionResult<IList<MedicalRecord>>> GetMedicalRecords([FromRoute] String CPF)
        {
            if(RegularExpressionsUtility.Matches(ExpressionType.CPF, CPF))
            {
                try
                {
                    return this.Ok(await this.MedicalPractionerAccountsContext.GetMedicalRecordsOf(CPF));
                }
                catch
                {
                    return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
                }
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Creates a new medical record.
        /// </summary>
        /// <param name="medicalRecord">The medical record data.</param>
        /// <returns>The created medical record.</returns>
        [HttpPost(template: "medical-record")]
        public async Task<ActionResult<MedicalRecord>> InsertMedicalRecord([FromBody] MedicalRecord medicalRecord)
        {
            if (RegularExpressionsUtility.Matches(ExpressionType.CPF, medicalRecord.CPF) && RegularExpressionsUtility.Matches(ExpressionType.E164, medicalRecord.PhoneNumber))
            {
                try
                {
                    KeyValuePair<MedicalRecord, Boolean> InsertedMedicalRecord = await this.MedicalPractionerAccountsContext.InsertMedicalRecord(MedicalRecord.Normalize(medicalRecord));

                    if (InsertedMedicalRecord.Value)
                        return this.Ok(InsertedMedicalRecord.Key);

                    return this.StatusCode(HttpStatusCode.Forbidden.ToInt32());

                }
                catch
                {
                    return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
                }
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Updates an existing medical record.
        /// </summary>
        /// <param name="medicalRecord">The new or updated medical record.</param>
        /// <returns>The updated medical record.</returns>
        [HttpPut(template: "medical-record")]
        public async Task<ActionResult<MedicalRecord>> UpdateMedicalRecord([FromBody] MedicalRecord medicalRecord)
        {
            if (RegularExpressionsUtility.Matches(ExpressionType.CPF, medicalRecord.CPF))
            {
                try
                {
                    KeyValuePair<MedicalRecord, Boolean> UpdatedMedicalRecord = await this.MedicalPractionerAccountsContext.UpdateMedicalRecord(MedicalRecord.Prepare(medicalRecord));

                    if (UpdatedMedicalRecord.Value)
                        return this.Ok(UpdatedMedicalRecord.Key);

                    return this.StatusCode(HttpStatusCode.OK.ToInt32());

                }
                catch
                {
                    return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
                }
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Deletes an existing medical record.
        /// </summary>
        /// <param name="id">The medical record ID.</param>
        /// <returns><see langword="true"></see> if the existing record was deleted; otherwise, <see langword="false"></see>.</returns>
        [HttpDelete(template: "medical-record/{id}")]
        public async Task<ActionResult<Boolean>> DeleteMedicalRecord([FromRoute] Guid id)
        {
            try
            {
                KeyValuePair<MedicalRecord, Boolean> DeletedMedicalRecord = await this.MedicalPractionerAccountsContext.DeleteMedicalRecord(id);

                if (DeletedMedicalRecord.Value)
                    return this.Ok(DeletedMedicalRecord.Key);

                return this.NotFound(null);
            }
            catch
            {
                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }
        }
    }
}