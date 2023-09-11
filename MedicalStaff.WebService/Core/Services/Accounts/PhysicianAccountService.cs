using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MedicalStaff.WebService.Core.Data;
using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Models.Runtime;
using MedicalStaff.WebService.Core.Helpers.Mappers;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Models.Db.Physician;

namespace MedicalStaff.WebService.Core.Services.Accounts
{
    /// <summary>
    /// Provides access to the common Medical's accounts services.
    /// </summary>
    public partial class PhysicianAccountService : Infrastructure.Accounts.AccountsRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PhysicianAccountService"/> by passing in its data-layer access. <see cref="SystemDbContext"/> is the bridge between this service and its corresponding database IO-Concerns mechanisms.
        /// </summary>
        /// <param name="applicationDbContext">The system's database context through where the <see cref="PhysicianAccountService"/> access and perform database-centered critical IO operations.</param>
        protected PhysicianAccountService(SystemDbContext applicationDbContext) : base(applicationDbContext) { }

        /// <summary>
        /// Retrieves Physician accounts considering the specified account type model. The specified type model must implements <see cref="IPhysicianAccount"/>.
        /// </summary>
        /// <typeparam name="TModel">The type model of medical account to seek where <typeparamref name="TModel"/> implements <see cref="IPhysicianAccount"/> data model.</typeparam>
        /// <returns>An <see cref="IEnumerable{T}"/> of <typeparamref name="TModel"/>.</returns>
        protected new async Task<IEnumerable<TModel>> QueryAccountsAsync<TModel>() where TModel : IPhysicianAccount
        {
            return (IEnumerable<TModel>)await base.QueryAccountsAsync<PhysicianAccount>();
        }

        /// <summary>
        /// Retrieves any Physician account considering the specified account type model, where the account implements an <see cref="IPhysicianAccount"/> data model.
        /// </summary>
        /// <typeparam name="TImpl">The type model of medical account to seek where <typeparamref name="TImpl"/> implements <see cref="IPhysicianAccount"/> data model.</typeparam>
        /// <param name="medicalPractionerCPF">The Medical Practioner CPF. (The Brazilian national-wide unique identification number)</param>
        /// <returns>A Medical's account model instance in where <typeparamref name="TImpl"/> implements <see cref="IPhysicianAccount"/>.</returns>
        protected new async Task<TImpl> RetrieveAccountAsync<TImpl>(String medicalPractionerCPF) where TImpl : IPhysicianAccount
        {
            return (TImpl)(IPhysicianAccount)await base.RetrieveAccountAsync<PhysicianAccount>(medicalPractionerCPF);
        }

        /// <summary>
        /// Updates the target medical practioner account.
        /// </summary>
        /// <typeparam name="TImpl">The type of account implementation to be updated. <typeparamref name="TImpl"/> implements <see cref="IPhysicianAccount"/>.</typeparam>
        /// <param name="medicalPractionerAccount"></param>
        /// <returns>The provided medical practioner account updated.</returns>
        protected new async Task<TImpl> UpdateAccountAsync<TImpl>(TImpl medicalPractionerAccount) where TImpl : IPhysicianAccount
        {
            return (TImpl)(IPhysicianAccount)await base.UpdateAccountAsync(SystemUser.Cast<PhysicianAccount>(medicalPractionerAccount));
        }

        /// <summary>
        /// Deltes a medical practioner account.
        /// </summary>
        /// <typeparam name="TImpl">The type of account implementation to be updated. <typeparamref name="TImpl"/> implements <see cref="IPhysicianAccount"/>.</typeparam>
        /// <param name="patientCPF">The medical practioner CPF.</param>
        /// <returns>The provided medical practioner account deleted.</returns>
        protected new async Task<Boolean> DeleteAccount<TImpl>(String patientCPF) where TImpl : IPhysicianAccount
        {
            return Convert.ToBoolean(await base.DeleteAccount<PhysicianAccount>(patientCPF));
        }

        /// <summary>
        /// Retrieves the system's account considering the specified account type model. The specified type model must implements <see cref="IPhysicianAccount"/>.
        /// </summary>
        /// <typeparam name="TImpl">The type model of medical account to seek in where <typeparamref name="TImpl"/> implements <see cref="IMedicalAccountCredential"/> data model.</typeparam>
        /// <param name="medicalPractionerCPF">The Medical Practioner CPF. (The Brazilian national-wide unique identification number)</param>
        /// <returns>A medical account model instance in where <typeparamref name="TImpl"/> implements <see cref="IMedicalAccountCredential"/>.</returns>
        protected async Task<TImpl> RequestAccountCredentialAsync<TImpl>(String medicalPractionerCPF) where TImpl : IMedicalAccountCredential
        {
            IPhysicianAccount MedicalPractioner = await RetrieveAccountAsync<PhysicianAccount>(medicalPractionerCPF);

            IMedicalAccountCredential MedicalAccountCredentials = new MedicalAccountCredential(MedicalPractioner.Password);

            if (!MedicalAccountCredentials.IsNullOrEmpty())
                return (TImpl)MedicalAccountCredentials;

            return MedicalAccountCredential.Empty<TImpl>();
        }

        /// <summary>
        /// Creates a new medical practioner account.
        /// </summary>
        /// <typeparam name="TImpl">The type of account implementation to be updated. <typeparamref name="TImpl"/> implements <see cref="IPhysicianAccount"/>.</typeparam>
        /// <param name="medicalPractionerAccountCredentials">The medical practioner account to be updated.</param>
        /// <returns>The created medical practioner account.</returns>
        protected async Task<TImpl> CreateAccount<TImpl>(TImpl medicalPractionerAccountCredentials) where TImpl : IPhysicianAccount
        {
            IPhysicianAccount InsertedMedicalPractioner = await Push(SystemUser.Cast<PhysicianAccount>(medicalPractionerAccountCredentials));

            return InsertedMedicalPractioner.CPF is not null ? (TImpl)InsertedMedicalPractioner : (TImpl)PhysicianAccount.Empty();
        }
    }
}