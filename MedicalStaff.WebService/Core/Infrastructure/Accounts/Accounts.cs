using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MedicalRecordsSystem.WebService.Core.Data;
using MedicalRecordsSystem.WebService.Core.Interfaces;
using MedicalRecordsSystem.WebService.Core.Helpers.Analysers;
using MedicalRecordsSystem.WebService.Core.Models.Runtime;
using MedicalRecordsSystem.WebService.Core.Models.Db.Patient;
using MedicalRecordsSystem.WebService.Core.Models.Db.MedicalPractioner;

namespace MedicalRecordsSystem.WebService.Core.Infrastructure.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    public class Accounts : ControllerBase
    {
        private readonly SystemDbContext SysContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDbContext"></param>
        protected Accounts(SystemDbContext applicationDbContext)
        {
            this.SysContext = applicationDbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="userCPF"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected async Task<TUser> RetrieveAccountAsync<TUser>(String userCPF) where TUser : ISystemUser
        {
            if (typeof(TUser).Implements<IMedicalPractionerAccount>())
            {
                String ProcessedUserCPF = userCPF.RemoveSpecifically(new[] { '.', '-' });

                IMedicalPractionerAccount? SearchedMedicalAccount = await this.SysContext.MedicalPractionerAccounts.FirstOrDefaultAsync(credentials => credentials.CPF == ProcessedUserCPF);

                if (SearchedMedicalAccount is not null)
                    return (TUser)SearchedMedicalAccount;

                return (TUser)MedicalPractionerAccount.Empty();
            }

            else if (typeof(TUser).Implements<IPatientAccount>())
            {
                String ProcessedUserToken = userCPF.RemoveSpecifically(new[] { '.', '-' });

                IPatientAccount? SearchedPatient = await this.SysContext.PatientsAccounts.FirstOrDefaultAsync(credentials => credentials.CPF == ProcessedUserToken);

                if (SearchedPatient is not null)
                    return (TUser)SearchedPatient;

                return (TUser)PatientAccount.Empty();
            }

            else
            {
                throw new NotImplementedException("This type of account has not yet been configured.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected async Task<TUser> Push<TUser>(TUser user) where TUser : ISystemUser
        {
            if (typeof(TUser).Implements<IMedicalPractionerAccount>())
            {
                _ = await this.SysContext.MedicalPractionerAccounts.AddAsync(SystemUser.Cast<MedicalPractionerAccount>(user));

                Boolean Saved = await this.SysContext.SaveChangesAsync() > 0;

                if (Saved)
                    return user;

                return (TUser)MedicalPractionerAccount.Empty();
            }

            else if (typeof(TUser).Implements<IPatientAccount>())
            {
                _ = await this.SysContext.PatientsAccounts.AddAsync(SystemUser.Cast<PatientAccount>(user));

                Boolean Saved = await this.SysContext.SaveChangesAsync() > 0;

                if (Saved)
                    return user;

                return (TUser)PatientAccount.Empty();
            }
            else
            {
                throw new NotImplementedException("This type of account has not yet been configured.");
            }
        }

        /// <summary>
        /// Retrieves the system's accounts regarding the specified account type, where the account implements an <see cref="ISystemUser"/> data model.
        /// </summary>
        /// <typeparam name="TUser">The type of account to seek in where <typeparamref name="TUser"/> implements <see cref="ISystemUser"/> data model.</typeparam>
        /// <returns>An <see cref="IEnumerable{T}"/> of <typeparamref name="TUser"/>.</returns>
        protected async Task<IEnumerable<TUser>> QueryAccountsAsync<TUser>() where TUser : ISystemUser
        {
            if (typeof(TUser).Implements<IMedicalPractionerAccount>())
                return (IEnumerable<TUser>)await this.SysContext.MedicalPractionerAccounts.ToListAsync();

            else if (typeof(TUser).Implements<IPatientAccount>())
                return (IEnumerable<TUser>)await this.SysContext.PatientsAccounts.ToListAsync();

            else
                return (IEnumerable<TUser>)new List<MedicalPractionerAccount>(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected async Task<TUser> UpdateAccountAsync<TUser>(TUser user) where TUser : ISystemUser
        {
            if (typeof(TUser).Implements<IMedicalPractionerAccount>())
            {
                try
                {
                    MedicalPractionerAccount UpdatedDoctor = SystemUser.Cast<MedicalPractionerAccount>(user);

                    this.SysContext.MedicalPractionerAccounts.Update(UpdatedDoctor);

                    Boolean Updated = await this.SysContext.SaveChangesAsync() > 0;

                    if (Updated)
                        return user;

                    return (TUser)MedicalPractionerAccount.Empty();
                }
                catch
                {
                    return (TUser)MedicalPractionerAccount.Empty();
                }
            }

            else if (typeof(TUser).Implements<IPatientAccount>())
            {
                try
                {
                    PatientAccount UpdatedPatient = SystemUser.Cast<PatientAccount>(user);

                    this.SysContext.PatientsAccounts.Update(UpdatedPatient);

                    Boolean Updated = await this.SysContext.SaveChangesAsync() > 0;

                    if (Updated)
                        return user;

                    return (TUser)PatientAccount.Empty();
                }
                catch
                {
                    return (TUser)PatientAccount.Empty();
                }
            }

            else
                throw new NotImplementedException("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="userCPF"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected async Task<Boolean> DeleteAccount<TUser>(String userCPF) where TUser : ISystemUser
        {
            if (typeof(TUser).Implements<IMedicalPractionerAccount>())
            {
                try
                {
                    IMedicalPractionerAccount OnDeleting = await this.RetrieveAccountAsync<MedicalPractionerAccount>(userCPF);

                    this.SysContext.MedicalPractionerAccounts.Remove(new MedicalPractionerAccount(OnDeleting));

                    return await this.SysContext.SaveChangesAsync() > 0;
                }
                catch
                {
                    return false;
                }
            }

            else if (typeof(TUser).Implements<IPatientAccount>())
            {
                try
                {
                    IPatientAccount OnDeleting = await this.RetrieveAccountAsync<PatientAccount>(userCPF);

                    this.SysContext.PatientsAccounts.Remove(new PatientAccount(OnDeleting));

                    return await this.SysContext.SaveChangesAsync() > 0;
                }
                catch
                {
                    return false;
                }
            }
            else
                throw new NotImplementedException("Other models are not available yet.");
        }
    }
}