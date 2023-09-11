using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MedicalStaff.WebService.Core.Data;
using MedicalStaff.WebService.Core.Interfaces;

namespace MedicalStaff.WebService.Core.Services.Records
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PatientRecordsService : Infrastructure.Records.RecordsRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PatientRecordsService"/> by passing in its data-layer access. <see cref="SystemDbContext"/> is the bridge between this service and its corresponding database IO-Concerns mechanisms.
        /// </summary>
        /// <param name="applicationDbContext">The system's database context through where the <see cref="PatientRecordsService"/> access and perform database-centered critical IO operations.</param>
        protected PatientRecordsService(SystemDbContext applicationDbContext) : base(applicationDbContext) { }

        /// <summary>
        /// Retrieves a record by ID.
        /// </summary>
        /// <typeparam name="TImpl">The record type model.</typeparam>
        /// <param name="ID">The record ID.</param>
        /// <returns>The retrieved <typeparamref name="TImpl"/> record.</returns>
        /// <exception cref="NotImplementedException"></exception>
        protected new async Task<IDbOperation<TImpl>> GetRecord<TImpl>(Guid ID) where TImpl : IPatientRecord
        {
            return await base.GetRecord<TImpl>(ID);
        }

        /// <summary>
        /// Retrieves the <typeparamref name="TImpl"/> records that belongs to the specified <paramref name="targetCPF"/>.
        /// </summary>
        /// <typeparam name="TImpl">The record type model.</typeparam>
        /// <param name="targetCPF">The target record's CPF.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> : <typeparamref name="TImpl"/>.</returns>
        protected new async Task<IEnumerable<TImpl>> GetRecords<TImpl>(String targetCPF) where TImpl : IPatientRecord
        {
            return await base.GetRecords<TImpl>(targetCPF);
        }

        /// <summary>
        /// Creates a new <typeparamref name="TImpl"/> record.
        /// </summary>
        /// <typeparam name="TImpl">The record type model.</typeparam>
        /// <param name="record">The record to be added.</param>
        /// <returns>An <see cref="IDbOperation{TImpl}"/> : <typeparamref name="TImpl"/>.</returns>
        protected new async Task<IDbOperation<TImpl>> CreateRecord<TImpl>(TImpl record) where TImpl : IPatientRecord
        {
            return await base.CreateRecord<TImpl>(record);
        }

        /// <summary>
        /// Updates a <typeparamref name="TImpl"/> record.
        /// </summary>
        /// <typeparam name="TImpl">The record type model.</typeparam>
        /// <param name="record"></param>
        /// <returns>An <see cref="IEnumerable{T}"/> : <typeparamref name="TImpl"/>.</returns>
        protected new async Task<IDbOperation<TImpl>> UpdateRecord<TImpl>(TImpl record) where TImpl : IPatientRecord
        {
            return await base.UpdateRecord<TImpl>(record);
        }

        /// <summary>
        /// Deletes a <typeparamref name="TImpl"/> record.
        /// </summary>
        /// <typeparam name="TImpl">The record type model.</typeparam>
        /// <param name="ID">The record ID.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> : <typeparamref name="TImpl"/>.</returns>
        protected new async Task<IDbOperation<TImpl>> DeleteRecord<TImpl>(Guid ID) where TImpl : IPatientRecord
        {
            return await base.DeleteRecord<TImpl>(ID);
        }
    }
}