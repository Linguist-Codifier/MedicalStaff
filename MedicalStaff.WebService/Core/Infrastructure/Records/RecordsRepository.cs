using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MedicalStaff.WebService.Core.Data;
using MedicalStaff.WebService.Core.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Models.Db.Records;
using MedicalStaff.WebService.Core.Helpers.Properties;

namespace MedicalStaff.WebService.Core.Infrastructure.Records
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RecordsRepository : ControllerBase
    {
        private readonly SystemDbContext SysContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDbContext"></param>
        protected RecordsRepository(SystemDbContext applicationDbContext)
            => this.SysContext = applicationDbContext;

        /// <summary>
        /// Retrieves a record by ID.
        /// </summary>
        /// <typeparam name="TRecord">The record type model.</typeparam>
        /// <param name="ID">The record ID.</param>
        /// <returns>The retrieved <typeparamref name="TRecord"/> record.</returns>
        /// <exception cref="NotImplementedException"></exception>
        protected async Task<IDbOperation<TRecord>> GetRecord<TRecord>(Guid ID) where TRecord : IRecords
        {
            if (typeof(TRecord).Implements<IPatientRecord>())
            {
                try
                {
                    PatientRecords? FoundRecord = await this.SysContext.PatientRecords.Where(record => record.ID == ID).FirstOrDefaultAsync();

                    if (FoundRecord is not null)
                        return (IDbOperation<TRecord>)new DbOperation<PatientRecords>(FoundRecord, DbOperationsStatus.Success);

                    return (IDbOperation<TRecord>)new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.Unallowed);
                }
                catch
                {
                    return (IDbOperation<TRecord>)new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.Failed);
                }
            }
            else
                throw new NotImplementedException("Type implementation is not available yet.");
        }

        /// <summary>
        /// Retrieves any records considering the specified <see cref="IRecords"/> type implementation.
        /// </summary>
        /// <param name="targetCPF">The target record's CPF.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <typeparamref name="TRecord"/>.</returns>
        protected async Task<IEnumerable<TRecord>> GetRecords<TRecord>(String targetCPF) where TRecord : IRecords
        {
            if (typeof(TRecord).Implements<IPatientRecord>())
            {
                String TargetCPF = targetCPF.RemoveSpecifically(new[] { '.', '-' });

                IEnumerable<PatientRecords> Records = await this.SysContext.PatientRecords.Where(record => record.CPF == TargetCPF).ToListAsync();

                if (Records.Any())
                    return Records.As<PatientRecords, TRecord>();

                return new List<PatientRecords>(0).As<PatientRecords, TRecord>();
            }
            else
                throw new NotImplementedException("Type implementation is not available yet.");
        }

        /// <summary>
        /// Creates a new <typeparamref name="TRecord"/> record.
        /// </summary>
        /// <typeparam name="TRecord">The record type model.</typeparam>
        /// <param name="comingRecord">The record to be added.</param>
        /// <returns>An <see cref="IDbOperation{TEntity}"/> : <typeparamref name="TRecord"/>.</returns>
        protected async Task<IDbOperation<TRecord>> CreateRecord<TRecord>(TRecord comingRecord) where TRecord : IRecords
        {
            if(typeof(TRecord).Implements<IPatientRecord>())
            {
                Boolean AlreadyCreated = await this.SysContext.PatientRecords.AnyAsync
                (
                    record => record.CPF == comingRecord.CPF && record.Name == comingRecord.Name && record.Birth == comingRecord.Birth && record.Email == comingRecord.Email
                        && record.PictureLocation == comingRecord.PictureLocation && record.Phone == comingRecord.Phone
                        && record.Address == comingRecord.Address
                );

                if (!AlreadyCreated)
                {
                    EntityEntry<PatientRecords> CreatedEntry = await this.SysContext.PatientRecords.AddAsync(new PatientRecords(comingRecord));

                    try
                    {
                        Boolean SuccessOnCreating = await this.SysContext.SaveChangesAsync() > 0;

                        if (SuccessOnCreating)
                            return (IDbOperation<TRecord>)new DbOperation<PatientRecords>(CreatedEntry.Entity, DbOperationsStatus.Success);
                    }
                    catch
                    {
                        return (IDbOperation<TRecord>)new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.Failed);
                    }
                }
                return (IDbOperation<TRecord>)new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.AlreadyExists);
            }
            else
                throw new NotImplementedException("Record type implementation is not available yet.");
        }

        /// <summary>
        /// Updates a <typeparamref name="TRecord"/> record.
        /// </summary>
        /// <typeparam name="TRecord">The record type model.</typeparam>
        /// <param name="comingRecord">The record to be updated.</param>
        /// <returns>An <see cref="IDbOperation{TEntity}"/> : <typeparamref name="TRecord"/>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        protected async Task<IDbOperation<TRecord>> UpdateRecord<TRecord>(TRecord comingRecord) where TRecord : IRecords
        {
            if (typeof(TRecord).Implements<IPatientRecord>())
            {
                PatientRecords? Records = await this.SysContext.PatientRecords.SingleOrDefaultAsync(record => record.ID == comingRecord.ID);

                if (Records is not null)
                {
                    Records.Update(comingRecord);

                    EntityEntry<PatientRecords> UpdatedEntry = this.SysContext.PatientRecords.Update(Records);

                    try
                    {
                        Boolean Updated = await this.SysContext.SaveChangesAsync() > 0;

                        if (Updated)
                            return (IDbOperation<TRecord>)new DbOperation<PatientRecords>(UpdatedEntry.Entity, DbOperationsStatus.Success);
                    }
                    catch
                    {
                        return (IDbOperation<TRecord>)new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.Failed);
                    }
                }

                return (IDbOperation<TRecord>)new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.Unallowed);
            }
            else
                throw new NotImplementedException("Record type implementation is not available yet.");
        }

        /// <summary>
        /// Drops a <typeparamref name="TRecord"/> record.
        /// </summary>
        /// <typeparam name="TRecord">The record type model.</typeparam>
        /// <param name="ID">The record ID.</param>
        /// <returns>An <see cref="IDbOperation{TEntity}"/> : <typeparamref name="TRecord"/>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        protected async Task<IDbOperation<TRecord>> DeleteRecord<TRecord>(Guid ID) where TRecord : IRecords
        {
            if(typeof(TRecord).Implements<IPatientRecord>())
            {
                PatientRecords? Record = await this.SysContext.PatientRecords.SingleOrDefaultAsync(record => record.ID == ID);

                if (Record is not null)
                {
                    EntityEntry<PatientRecords> DroppedEntry = this.SysContext.PatientRecords.Remove(Record);

                    try
                    {
                        Boolean Dropped = await this.SysContext.SaveChangesAsync() > 0;

                        if (Dropped)
                            return (IDbOperation<TRecord>)new DbOperation<PatientRecords>(DroppedEntry.Entity, DbOperationsStatus.Success);
                    }
                    catch
                    {
                        return (IDbOperation<TRecord>)new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.Failed);
                    }
                }

                return (IDbOperation<TRecord>)new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.Unallowed);
            }
            else
                throw new NotImplementedException("Record type implementation is not available yet.");
        }
    }
}