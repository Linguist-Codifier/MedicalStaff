using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MedicalStaff.WebService.Core.Data;
using System.ComponentModel.DataAnnotations;
using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Services.Records;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Models.Db.Records;
using MedicalStaff.WebService.Core.Helpers.Properties;
using MedicalStaff.WebService.Core.Models.Transfer.PatientRecordDTO;

namespace MedicalStaff.WebService.Controllers
{
    /// <summary>
    /// The patient records controller.
    /// </summary>
    [ApiController]
    [Route("api/patient-records")]
    public sealed partial class PatientRecordsController : PatientRecordsService
    {
        private readonly ILogger<PatientRecordsController> _logger;

        #pragma warning disable CS1591
        public PatientRecordsController(ILogger<PatientRecordsController> logger, SystemDbContext applicationDbContext)
            : base(applicationDbContext) => this._logger = logger;
        #pragma warning restore CS1591

        /// <summary>
        /// Retrieves all records for the specified account by CPF.
        /// </summary>
        /// <param name="CPF">The account's CPF.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="PatientRecords"/>.</returns>
        [HttpGet("{CPF}")]
        public async Task<ActionResult<IEnumerable<PatientRecords>>> GetPatientRecords([Required] String CPF)
        {
            try
            {
                IEnumerable<PatientRecords> Records = await this.GetRecords<PatientRecords>(CPF);

                return this.Ok(Records.Select(each => each.FormatSpecialProperties()));
            }
            catch
            {
                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());
            }
        }

        /// <summary>
        /// Creates a new patient record.
        /// </summary>
        /// <param name="record">The record to be added.</param>
        /// <returns>The created record.</returns>
        [HttpPost]
        public async Task<ActionResult<PatientRecords>> CreatePatientRecord([Required][FromBody] PatientRecordsDTO record)
        {
            IDbOperation<PatientRecords> OnCreating = await this.CreateRecord<PatientRecords>(new PatientRecords(record).NormalizeSpecialProperties());

            if (OnCreating.OperationStatus == DbOperationsStatus.Success)
                return this.Ok(OnCreating.Result?.FormatSpecialProperties());

            else if (OnCreating.OperationStatus == DbOperationsStatus.AlreadyExists)
                return this.StatusCode(HttpStatusCode.Forbidden.ToInt32());

            else if (OnCreating.OperationStatus == DbOperationsStatus.Failed)
                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());

            throw new NotImplementedException("Unavailable operation.");
        }

        /// <summary>
        /// Updates an existing patient record.
        /// </summary>
        /// <param name="ID">The record ID.</param>
        /// <param name="record">The updated data of the record.</param>
        /// <returns>The updated record.</returns>
        [HttpPut("{ID}")]
        public async Task<ActionResult<PatientRecords>> UpdatePatientRecord([Required]Guid ID, [FromBody] PatientRecordsDTO record)
        {
            IDbOperation<PatientRecords> Query = await this.GetRecord<PatientRecords>(ID);

            if(Query.OperationStatus == DbOperationsStatus.Success)
            {
                Query.Result?.Update(record);

                IDbOperation<PatientRecords> OnUpdating = await this.UpdateRecord<PatientRecords>(Query.EnsureInstance().NormalizeSpecialProperties());

                if (OnUpdating.OperationStatus == DbOperationsStatus.Success)
                    return this.Ok(OnUpdating.Result?.FormatSpecialProperties());

                else if (OnUpdating.OperationStatus == DbOperationsStatus.Unallowed)
                    return this.StatusCode(HttpStatusCode.Forbidden.ToInt32());

                else if (OnUpdating.OperationStatus == DbOperationsStatus.Failed)
                    return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());

                throw new NotImplementedException("Unavailable operation.");
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Deletes an existing patient record.
        /// </summary>
        /// <param name="ID">The record ID.</param>
        /// <returns><see langword="true"></see> whether the existing record was deleted; otherwise, <see langword="false"></see>.</returns>
        [HttpDelete("{ID}")]
        public async Task<ActionResult<Boolean>> DeletePatientRecord([Required] Guid ID)
        {
            IDbOperation<PatientRecords> OnUpdating = await this.DeleteRecord<PatientRecords>(ID);

            if (OnUpdating.OperationStatus == DbOperationsStatus.Success)
                return this.Ok(OnUpdating.Result?.FormatSpecialProperties());

            else if (OnUpdating.OperationStatus == DbOperationsStatus.Unallowed)
                return this.StatusCode(HttpStatusCode.Forbidden.ToInt32());

            else if (OnUpdating.OperationStatus == DbOperationsStatus.Failed)
                return this.StatusCode(HttpStatusCode.InternalServerError.ToInt32());

            throw new NotImplementedException("Unavailable operation.");
        }
    }
}