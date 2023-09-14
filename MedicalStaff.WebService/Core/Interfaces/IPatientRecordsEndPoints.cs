using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MedicalStaff.WebService.Core.Models.Db.Records;
using MedicalStaff.WebService.Core.Models.Transfer.PatientRecordDTO;

namespace MedicalStaff.WebService.Core.Interfaces
{
    /// <summary>
    /// Stabilishes the default endpoints for <see cref="PatientRecords"/> controller and how it must treat its workloads.
    /// </summary>
    public interface IPatientRecordsEndPoints
    {
        /// <summary>
        /// Gets asynchronously all <see cref="PatientRecords"/> associated with this <paramref name="CPF"/>.
        /// </summary>
        /// <param name="CPF">The <paramref name="CPF"/> associated with this <see cref="PatientRecords"/>.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> : <see cref="PatientRecords"/>.</returns>
        Task<IActionResult> GetPatientRecords([Required] System.String CPF);

        /// <summary>
        /// Creates a <see cref="PatientRecords"/> record.
        /// </summary>
        /// <param name="record">The <see cref="PatientRecords"/> record to be created casted as its corresponding data transfer object.</param>
        /// <returns>The <see cref="PatientRecords"/> account just created.</returns>
        Task<IActionResult> CreatePatientRecord([FromBody, Required] PatientRecordsDTO record);

        /// <summary>
        /// Updates a <see cref="PatientRecords"/> record.
        /// </summary>
        /// <param name="ID">The <paramref name="ID"/> associated with the <see cref="PatientRecords"/> record.</param>
        /// <param name="record">The <see cref="PatientRecords"/> record to be created casted as its corresponding data transfer object</param>
        /// <returns>The <see cref="PatientRecords"/> account just updated.</returns>
        Task<IActionResult> UpdatePatientRecord([Required] Guid ID, [FromBody] PatientRecordsDTO record);

        /// <summary>
        /// Deletes a <see cref="PatientRecords"/> record.
        /// </summary>
        /// <param name="ID">The <paramref name="ID"/> associated with the <see cref="PatientRecords"/> record.</param>
        /// <returns>The <see cref="PatientRecords"/> account just deleted.</returns>
        Task<IActionResult> DeletePatientRecord([Required] Guid ID);
    }
}