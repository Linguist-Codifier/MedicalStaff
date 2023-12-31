﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MedicalStaff.WebService.Core.Data;
using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Models.Runtime;
using MedicalStaff.WebService.Core.Models.Db.Patient;
using MedicalStaff.WebService.Core.Models.Db.Physician;

namespace MedicalStaff.WebService.Core.Infrastructure.Accounts
{
    /// <summary>
    /// Manages accounts repositories.
    /// </summary>
    public partial class AccountsRepository
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly SystemDbContext SysContext;

        /// <summary>
        /// Initializes a new instance of <see cref="AccountsRepository"/>.
        /// </summary>
        /// <param name="applicationDbContext">The system's database context through where the <see cref="AccountsRepository"/> access and perform database-centered critical IO operations.</param>
        protected AccountsRepository(SystemDbContext applicationDbContext) => this.SysContext = applicationDbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="userCPF"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected virtual async Task<TUser> GetAccountAsync<TUser>(String userCPF) where TUser : ISystemUser
        {
            if (typeof(TUser).Implements<IPhysicianAccount>())
            {
                String ProcessedUserCPF = userCPF.RemoveSpecifically(new[] { '.', '-' });

                IPhysicianAccount? SearchedMedicalAccount = await this.SysContext.PhysicianAccounts.FirstOrDefaultAsync(credentials => credentials.CPF == ProcessedUserCPF);

                if (SearchedMedicalAccount is not null)
                    return (TUser)SearchedMedicalAccount;

                return (TUser)PhysicianAccount.Empty();
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
        protected virtual async Task<TUser> Create<TUser>(TUser user) where TUser : ISystemUser
        {
            if (typeof(TUser).Implements<IPhysicianAccount>())
            {
                _ = await this.SysContext.PhysicianAccounts.AddAsync(SystemUser.Cast<PhysicianAccount>(user));

                Boolean Saved = await this.SysContext.SaveChangesAsync() > 0;

                if (Saved)
                    return user;

                return (TUser)PhysicianAccount.Empty();
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
        protected virtual async Task<IEnumerable<TUser>> QueryAccountsAsync<TUser>() where TUser : ISystemUser
        {
            if (typeof(TUser).Implements<IPhysicianAccount>())
                return (IEnumerable<TUser>)await this.SysContext.PhysicianAccounts.ToListAsync();

            else if (typeof(TUser).Implements<IPatientAccount>())
                return (IEnumerable<TUser>)await this.SysContext.PatientsAccounts.ToListAsync();

            else
                return (IEnumerable<TUser>)new List<PhysicianAccount>(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected virtual async Task<TUser> UpdateAsync<TUser>(TUser user) where TUser : ISystemUser
        {
            if (typeof(TUser).Implements<IPhysicianAccount>())
            {
                try
                {
                    PhysicianAccount UpdatedDoctor = SystemUser.Cast<PhysicianAccount>(user);

                    this.SysContext.PhysicianAccounts.Update(UpdatedDoctor);

                    Boolean Updated = await this.SysContext.SaveChangesAsync() > 0;

                    if (Updated)
                        return user;

                    return (TUser)PhysicianAccount.Empty();
                }
                catch
                {
                    return (TUser)PhysicianAccount.Empty();
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
        protected virtual async Task<Boolean> DeleteAsync<TUser>(String userCPF) where TUser : ISystemUser
        {
            if (typeof(TUser).Implements<IPhysicianAccount>())
            {
                try
                {
                    IPhysicianAccount OnDeleting = await this.GetAccountAsync<PhysicianAccount>(userCPF);

                    this.SysContext.PhysicianAccounts.Remove(new PhysicianAccount(OnDeleting));

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
                    IPatientAccount OnDeleting = await this.GetAccountAsync<PatientAccount>(userCPF);

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