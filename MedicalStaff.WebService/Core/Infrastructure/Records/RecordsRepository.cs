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
    /// Manages the <see cref="PatientRecords"/> repository.
    /// </summary>
    public partial class RecordsRepository : ControllerBase
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly SystemDbContext SysContext;

        /// <summary>
        /// Initializes a new instance of <see cref="RecordsRepository"/>.
        /// </summary>
        /// <param name="applicationDbContext">The system's database context through where the <see cref="RecordsRepository"/> access and perform database-centered critical IO operations.</param>
        protected RecordsRepository(SystemDbContext applicationDbContext)
            => this.SysContext = applicationDbContext;

        /// <summary>
        /// Retrieves a record by ID.
        /// </summary>
        /// <typeparam name="TRecord">The record type model.</typeparam>
        /// <param name="ID">The record ID.</param>
        /// <returns>The retrieved <typeparamref name="TRecord"/> record.</returns>
        /// <exception cref="NotImplementedException"></exception>
        protected virtual async Task<IDbOperation<TRecord>> GetRecord<TRecord>(Guid ID) where TRecord : IRecords
        {
            if (typeof(TRecord).Implements<IPatientRecord>())
            {
                try
                {
                    PatientRecords? FoundRecord = await this.SysContext.PatientRecords.Where(it => it.ID == ID).FirstOrDefaultAsync();

                    if (FoundRecord is not null)
                        return new DbOperation<PatientRecords>(FoundRecord, DbOperationsStatus.Success).CastOperation<PatientRecords, TRecord>();

                    return new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.Unallowed).CastOperation<PatientRecords, TRecord>();
                }
                catch
                {
                    return new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.Failed).CastOperation<PatientRecords, TRecord>();
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
        protected virtual async Task<IEnumerable<TRecord>> GetRecords<TRecord>(String targetCPF) where TRecord : IRecords
        {
            if (typeof(TRecord).Implements<IPatientRecord>())
            {
                String TargetCPF = targetCPF.RemoveSpecifically(new[] { '.', '-' });

                IEnumerable<PatientRecords> Records = await this.SysContext.PatientRecords.Where(it => it.CPF == TargetCPF).ToListAsync();

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
        /// <param name="it">The record to be added.</param>
        /// <returns>An <see cref="IDbOperation{TEntity}"/> : <typeparamref name="TRecord"/>.</returns>
        protected virtual async Task<IDbOperation<TRecord>> CreateRecord<TRecord>(TRecord it) where TRecord : IRecords
        {
            if(typeof(TRecord).Implements<IPatientRecord>())
            {
                Boolean AlreadyCreated = await this.SysContext.PatientRecords.AnyAsync
                (
                    current => current.CPF == it.CPF && current.Name == it.Name && current.Birth == it.Birth && current.Email == it.Email
                        && current.PictureLocation == it.PictureLocation && current.Phone == it.Phone
                        && current.Address == it.Address
                );

                if (!AlreadyCreated)
                {
                    EntityEntry<PatientRecords> CreatedEntry = await this.SysContext.PatientRecords.AddAsync(new PatientRecords(it));

                    try
                    {
                        Boolean SuccessOnCreating = await this.SysContext.SaveChangesAsync() > 0;

                        if (SuccessOnCreating)
                            return new DbOperation<PatientRecords>(CreatedEntry.Entity, DbOperationsStatus.Success).CastOperation<PatientRecords, TRecord>();
                    }
                    catch
                    {
                        return new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.Failed).CastOperation<PatientRecords, TRecord>();
                    }
                }
                return new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.AlreadyExists).CastOperation<PatientRecords, TRecord>();
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
        protected virtual async Task<IDbOperation<TRecord>> UpdateRecord<TRecord>(TRecord comingRecord) where TRecord : IRecords
        {
            if (typeof(TRecord).Implements<IPatientRecord>())
            {
                PatientRecords? Records = await this.SysContext.PatientRecords.SingleOrDefaultAsync(it => it.ID == comingRecord.ID);

                if (Records is not null)
                {
                    Records.Update(comingRecord);

                    EntityEntry<PatientRecords> UpdatedEntry = this.SysContext.PatientRecords.Update(Records);

                    try
                    {
                        Boolean Updated = await this.SysContext.SaveChangesAsync() > 0;

                        if (Updated)
                            return new DbOperation<PatientRecords>(UpdatedEntry.Entity, DbOperationsStatus.Success).CastOperation<PatientRecords, TRecord>();
                    }
                    catch
                    {
                        return new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.Failed).CastOperation<PatientRecords, TRecord>();
                    }
                }

                return new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.Unallowed).CastOperation<PatientRecords, TRecord>();
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
        protected virtual async Task<IDbOperation<TRecord>> DeleteRecord<TRecord>(Guid ID) where TRecord : IRecords
        {
            if(typeof(TRecord).Implements<IPatientRecord>())
            {
                PatientRecords? Record = await this.SysContext.PatientRecords.SingleOrDefaultAsync(it => it.ID == ID);

                if (Record is not null)
                {
                    EntityEntry<PatientRecords> DroppedEntry = this.SysContext.PatientRecords.Remove(Record);

                    try
                    {
                        Boolean Dropped = await this.SysContext.SaveChangesAsync() > 0;

                        if (Dropped)
                            return new DbOperation<PatientRecords>(DroppedEntry.Entity, DbOperationsStatus.Success).CastOperation<PatientRecords, TRecord>();
                    }
                    catch
                    {
                        return new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.Failed).CastOperation<PatientRecords, TRecord>();
                    }
                }

                return new DbOperation<PatientRecords>(operationsStatus: DbOperationsStatus.Unallowed).CastOperation<PatientRecords, TRecord>();
            }
            else
                throw new NotImplementedException("Record type implementation is not available yet.");
        }
    }
}