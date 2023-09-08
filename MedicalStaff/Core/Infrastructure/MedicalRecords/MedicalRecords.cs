using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MedicalRecordsSystem.WebService.Core.Data;
using MedicalRecordsSystem.WebService.Core.Helpers.Analysers;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MedicalRecordsSystem.WebService.Core.Models.Db.MedicalRecord;

namespace MedicalRecordsSystem.WebService.Core.Infrastructure.MedicalRecords
{
    /// <summary>
    /// 
    /// </summary>
    public class MedicalRecordsDbContext : ControllerBase
    {
        private readonly SystemDbContext Context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDbContext"></param>
        public MedicalRecordsDbContext(SystemDbContext applicationDbContext)
        {
            this.Context = applicationDbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public async Task<IList<MedicalRecord>> GetMedicalRecordsOf(String CPF)
        {
            String ProcessedCPF = CPF.RemoveSpecifically(new[] { '.', '-' });

            IList<MedicalRecord> MedicalRecords = await this.Context.MedicalRecords.Where(record => record.CPF == ProcessedCPF).ToListAsync();

            if (MedicalRecords.Any())
                return MedicalRecords;

            return new List<MedicalRecord>(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns></returns>
        public async Task<KeyValuePair<MedicalRecord, Boolean>> InsertMedicalRecord(MedicalRecord medicalRecord)
        {
            Boolean AlreadyInserted = await this.Context.MedicalRecords.AnyAsync(record =>
                record.CPF == medicalRecord.CPF && record.Name == medicalRecord.Name && record.PictureLocation == medicalRecord.PictureLocation
                    && record.PhoneNumber == medicalRecord.PhoneNumber && record.Address == medicalRecord.Address
            );

            if (!AlreadyInserted)
            {
                EntityEntry<MedicalRecord> InsertedReord = await this.Context.MedicalRecords.AddAsync(medicalRecord);

                Boolean Created = await this.Context.SaveChangesAsync() > 0;

                if (Created)
                    return new KeyValuePair<MedicalRecord, Boolean>(InsertedReord.Entity, true);

                return new KeyValuePair<MedicalRecord, Boolean>(new MedicalRecord(), false);
            }
            return new KeyValuePair<MedicalRecord, Boolean>(new MedicalRecord(), false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns></returns>
        public async Task<KeyValuePair<MedicalRecord, Boolean>> UpdateMedicalRecord(MedicalRecord medicalRecord)
        {
            MedicalRecord? CurrentMedicalRecord = await this.Context.MedicalRecords.SingleOrDefaultAsync(record => record.ID == medicalRecord.ID);

            if (CurrentMedicalRecord is not null)
            {
                CurrentMedicalRecord.Name = medicalRecord.Name;
                CurrentMedicalRecord.CPF = medicalRecord.CPF;
                CurrentMedicalRecord.PictureLocation = medicalRecord.PictureLocation;
                CurrentMedicalRecord.PhoneNumber = medicalRecord.PhoneNumber;
                CurrentMedicalRecord.Address = medicalRecord.Address;

                this.Context.MedicalRecords.Update(medicalRecord);

                Boolean Updated = await this.Context.SaveChangesAsync() > 0;

                if (Updated)
                    return new KeyValuePair<MedicalRecord, Boolean>(medicalRecord, true);

                return new KeyValuePair<MedicalRecord, Boolean>(new MedicalRecord(), false);
            }

            return new KeyValuePair<MedicalRecord, Boolean>(new MedicalRecord(), false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns></returns>
        public async Task<KeyValuePair<MedicalRecord, Boolean>> DeleteMedicalRecord(MedicalRecord medicalRecord)
        {
            MedicalRecord? CurrentMedicalRecord = await this.Context.MedicalRecords.SingleOrDefaultAsync(record => record.ID == medicalRecord.ID);

            if (CurrentMedicalRecord is not null)
            {
                EntityEntry<MedicalRecord> DeletedMedicalRecord = this.Context.MedicalRecords.Remove(medicalRecord);

                Boolean Deleted = await this.Context.SaveChangesAsync() > 0;

                if (Deleted)
                    return new KeyValuePair<MedicalRecord, Boolean>(DeletedMedicalRecord.Entity, true);

                return new KeyValuePair<MedicalRecord, Boolean>(new MedicalRecord(), false);
            }

            return new KeyValuePair<MedicalRecord, Boolean>(new MedicalRecord(), false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<KeyValuePair<MedicalRecord, Boolean>> DeleteMedicalRecord(Guid id)
        {
            MedicalRecord? CurrentMedicalRecord = await this.Context.MedicalRecords.SingleOrDefaultAsync(record => record.ID == id);

            if (CurrentMedicalRecord is not null)
            {
                EntityEntry<MedicalRecord> DeletedMedicalRecord = this.Context.MedicalRecords.Remove(CurrentMedicalRecord);

                Boolean Deleted = await this.Context.SaveChangesAsync() > 0;

                if (Deleted)
                    return new KeyValuePair<MedicalRecord, Boolean>(DeletedMedicalRecord.Entity, true);

                return new KeyValuePair<MedicalRecord, Boolean>(new MedicalRecord(), false);
            }

            return new KeyValuePair<MedicalRecord, Boolean>(new MedicalRecord(), false);
        }
    }
}