using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MedicalStaff.WebService.Core.Data;
using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Models.Runtime;
using MedicalStaff.WebService.Core.Helpers.Mappers;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Models.Db.Patient;
using MedicalStaff.WebService.Core.Models.Db.Physician;
using MedicalStaff.WebService.Core.Infrastructure.Accounts;
using MedicalStaff.WebService.Core.Helpers.Properties;

namespace MedicalStaff.WebService.Core.Services.Accounts
{
    /// <summary>
    /// Provides access to the common Medical's accounts services.
    /// </summary>
    public class AccountService<Scope> : AccountsRepository, IAccountService<Scope> where Scope : ISystemUser
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AccountService{T}"/> by passing in its data-layer access. <see cref="SystemDbContext"/> is the bridge between this service and its corresponding database I/O-Concerns mechanisms.
        /// </summary>
        /// <param name="applicationDbContext">The system's database context through where the <see cref="AccountService{T}"/> access and perform database-centered critical I/O operations.</param>
        public AccountService(SystemDbContext applicationDbContext) : base(applicationDbContext) { }

        /// <summary>
        /// Retrieves Physician accounts considering the specified account type model. The specified type model must implements <see cref="IPhysicianAccount"/>.
        /// </summary>
        /// <typeparam name="Model">The type model of medical account to seek where <typeparamref name="Model"/> implements <see cref="IPhysicianAccount"/> data model.</typeparam>
        /// <returns>An <see cref="IEnumerable{T}"/> of <typeparamref name="Model"/>.</returns>
        public new async Task<IEnumerable<Model>> QueryAccountsAsync<Model>() where Model : Scope
        {
            return (IEnumerable<Model>)await base.QueryAccountsAsync<Scope>();
        }

        /// <summary>
        /// Retrieves any Physician account considering the specified account type model, where the account implements an <see cref="IPhysicianAccount"/> data model.
        /// </summary>
        /// <typeparam name="Model">The type model of medical account to seek where <typeparamref name="Model"/> implements <see cref="IPhysicianAccount"/> data model.</typeparam>
        /// <param name="CPF">The Medical Practioner CPF. (The Brazilian national-wide unique identification number)</param>
        /// <returns>A Medical's account model instance in where <typeparamref name="Model"/> implements <see cref="IPhysicianAccount"/>.</returns>
        public new async Task<Model> GetAccountAsync<Model>(String CPF) where Model : Scope
        {
            return (Model)await base.GetAccountAsync<Scope>(CPF);
        }

        /// <summary>
        /// Updates the target Physician account.
        /// </summary>
        /// <typeparam name="Model">The type of account implementation to be updated. <typeparamref name="Model"/> implements <see cref="IPhysicianAccount"/>.</typeparam>
        /// <param name="account"></param>
        /// <returns>The provided Physician account updated.</returns>
        public new async Task<Model> UpdateAsync<Model>(Model account) where Model : Scope
        {
            return (Model)await base.UpdateAsync<Scope>(SystemUser.Cast<Scope>(account));
        }

        /// <summary>
        /// Deletes a Physician account.
        /// </summary>
        /// <typeparam name="Model">The type of account implementation to be updated. <typeparamref name="Model"/> implements <see cref="IPhysicianAccount"/>.</typeparam>
        /// <param name="physicianCPF">The Physician CPF.</param>
        /// <returns>The provided Physician account deleted.</returns>
        public new async Task<Boolean> DeleteAsync<Model>(String physicianCPF) where Model : Scope
        {
            return Convert.ToBoolean(await base.DeleteAsync<Scope>(physicianCPF));
        }

        /// <summary>
        /// Retrieves the system's account considering the specified account type model. The specified type model must implements <see cref="IPhysicianAccount"/>.
        /// </summary>
        /// <typeparam name="Model">The type model of medical account to seek in where <typeparamref name="Model"/> implements <see cref="IMedicalAccountCredential"/> data model.</typeparam>
        /// <param name="CPF">The Medical Practioner CPF. (The Brazilian national-wide unique identification number)</param>
        /// <returns>A medical account model instance in where <typeparamref name="Model"/> implements <see cref="IMedicalAccountCredential"/>.</returns>
        public async Task<Model> GetCredentialAsync<Model>(String CPF) where Model : IAccountCredential
        {
            Scope account = await base.GetAccountAsync<Scope>(CPF);

            if(typeof(Scope).Implements<IPhysicianAccount>())
            {
                IMedicalAccountCredential accountCredential = new MedicalAccountCredential(SystemUser.Cast<PhysicianAccount>(account).Password);

                return (Model)accountCredential;
            }

            else if (typeof(Scope).Implements<IPatientAccount>())
            {
                IPatientAccountCredential accountCredential = new PatientAccountCrendential(SystemUser.Cast<PatientAccount>(account).Password);

                if (!accountCredential.IsNullOrEmpty())
                    return (Model)accountCredential;
            }

            return (Model)(IMedicalAccountCredential)MedicalAccountCredential.Empty<MedicalAccountCredential>();
        }

        /// <summary>
        /// Creates a new Physician account.
        /// </summary>
        /// <param name="account">The Physician account to be updated.</param>
        /// <returns>The created Physician account.</returns>
        public async Task<IDbOperation<Scope>> Create(Scope account)
        {
            try
            {
                if(typeof(Scope).Implements<IPhysicianAccount>())
                {
                    PhysicianAccount acc = SystemUser.Cast<PhysicianAccount>(account);

                    IMedicalAccountCredential accountCredential = await this.GetCredentialAsync<MedicalAccountCredential>(acc.CPF);

                    if (accountCredential.IsNullOrEmpty())
                    {
                        IPhysicianAccount createdAccount = await base.Create<PhysicianAccount>(acc);

                        if (createdAccount.CPF is not null)
                            return (IDbOperation<Scope>)new DbOperation<PhysicianAccount>(SystemUser.Cast<PhysicianAccount>(createdAccount), DbOperationsStatus.Success);
                        else
                            return (IDbOperation<Scope>)new DbOperation<PhysicianAccount>(SystemUser.Cast<PhysicianAccount>(PhysicianAccount.Empty()), DbOperationsStatus.Failed);
    
                    }
                }

                else if (typeof(Scope).Implements<IPatientAccount>())
                {
                    PatientAccount acc = SystemUser.Cast<PatientAccount>(account);

                    IPatientAccountCredential accountCredential = await this.GetCredentialAsync<PatientAccountCrendential>(acc.CPF);

                    if (accountCredential.IsNullOrEmpty())
                    {
                        IPatientAccount createdAccount = await base.Create<PatientAccount>(acc);

                        if (createdAccount.CPF is not null)
                            return (IDbOperation<Scope>)new DbOperation<PatientAccount>(SystemUser.Cast<PatientAccount>(createdAccount), DbOperationsStatus.Success);
                        else
                            return (IDbOperation<Scope>)new DbOperation<PatientAccount>(SystemUser.Cast<PatientAccount>(PatientAccount.Empty()), DbOperationsStatus.Failed);

                    }
                }

                return (IDbOperation<Scope>)new DbOperation<PatientAccount>(SystemUser.Cast<PatientAccount>(PatientAccount.Empty()), DbOperationsStatus.Failed);

            }
            catch
            {
                return (IDbOperation<Scope>)new DbOperation<PatientAccount>(SystemUser.Cast<PatientAccount>(PatientAccount.Empty()), DbOperationsStatus.Failed);
            }
        }
    }
}