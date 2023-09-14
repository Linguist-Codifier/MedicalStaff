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
    /// This controller is responsible for providing endpoints for accessing and manipulating any <see cref="PatientRecords"/> account. This class cannot be inherited.
    /// </summary>
    [ApiController]
    [Route("api/patient-records")]
    public sealed partial class PatientRecordsController : PatientRecordsService, IPatientRecordsEndPoints
    {
        private readonly ILogger<PatientRecordsController> _logger;

        #pragma warning disable CS1591
        public PatientRecordsController(ILogger<PatientRecordsController> logger, SystemDbContext applicationDbContext)
            : base(applicationDbContext) => this._logger = logger;
        #pragma warning restore CS1591

        /// <inheritdoc/>
        [HttpGet("{CPF}")]
        public async Task<IActionResult> GetPatientRecords([Required] String CPF)
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

        /// <inheritdoc/>
        [HttpPost]
        public async Task<IActionResult> CreatePatientRecord([Required][FromBody] PatientRecordsDTO record)
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

        /// <inheritdoc/>
        [HttpPut("{ID}")]
        public async Task<IActionResult> UpdatePatientRecord([Required]Guid ID, [FromBody] PatientRecordsDTO record)
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

        /// <inheritdoc/>
        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeletePatientRecord([Required] Guid ID)
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