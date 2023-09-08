using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MedicalRecordsSystem.WebService.Core.Data;
using MedicalRecordsSystem.WebService.Core.Interfaces;
using MedicalRecordsSystem.WebService.Core.Helpers.Mappers;
using MedicalRecordsSystem.WebService.Core.Helpers.Analysers;
using MedicalRecordsSystem.WebService.Core.Models.Runtime;
using MedicalRecordsSystem.WebService.Core.Models.Db.MedicalPractioner;
using MedicalRecordsSystem.WebService.Core.Infrastructure.Accounts;

namespace MedicalRecordsSystem.WebService.Core.Services.MedicalPractioner
{
    /// <summary>
    /// Provides access to the common Medical's accounts services.
    /// </summary>
    public class MedicalAccountsServices : Accounts
    {
        /// <summary>
        /// The <see cref="MedicalAccountsServices"/> contructor.
        /// </summary>
        /// <param name="applicationDbContext">The system's database context through where the <see cref="MedicalAccountsServices"/> access and perform data-base centered critical operations.</param>
        protected MedicalAccountsServices(SystemDbContext applicationDbContext) : base(applicationDbContext) { }

        /// <summary>
        /// Retrieves the medical practioner's account considering the specified account type model. The specified type model must implements <see cref="IMedicalPractionerAccount"/>.
        /// </summary>
        /// <typeparam name="TModel">The type model of medical account to seek where <typeparamref name="TModel"/> implements <see cref="IMedicalPractionerAccount"/> data model.</typeparam>
        /// <returns>An <see cref="IEnumerable{T}"/> of <typeparamref name="TModel"/>.</returns>
        protected new async Task<IEnumerable<TModel>> QueryAccountsAsync<TModel>() where TModel : IMedicalPractionerAccount
        {
            return (IEnumerable<TModel>)await base.QueryAccountsAsync<MedicalPractionerAccount>();
        }

        /// <summary>
        /// Retrieves any Medical's account regarding the specified account type model, where the account implements an <see cref="IMedicalPractionerAccount"/> data model.
        /// </summary>
        /// <typeparam name="TAccount">The type model of medical account to seek where <typeparamref name="TAccount"/> implements <see cref="IMedicalPractionerAccount"/> data model.</typeparam>
        /// <param name="medicalPractionerCPF">The Medical Practioner CPF. (The Brazilian national-wide unique identification number)</param>
        /// <returns>A Medical's account model instance in where <typeparamref name="TAccount"/> implements <see cref="IMedicalPractionerAccount"/>.</returns>
        protected new async Task<TAccount> RetrieveAccountAsync<TAccount>(String medicalPractionerCPF) where TAccount : IMedicalPractionerAccount
        {
            return (TAccount)(IMedicalPractionerAccount)await base.RetrieveAccountAsync<MedicalPractionerAccount>(medicalPractionerCPF);
        }

        /// <summary>
        /// Updates the target medical practioner account.
        /// </summary>
        /// <typeparam name="TAccountImplementation">The type of account implementation to be updated. <typeparamref name="TAccountImplementation"/> implements <see cref="IMedicalPractionerAccount"/>.</typeparam>
        /// <param name="medicalPractionerAccount"></param>
        /// <returns>The provided medical practioner account updated.</returns>
        protected new async Task<TAccountImplementation> UpdateAccountAsync<TAccountImplementation>(TAccountImplementation medicalPractionerAccount) where TAccountImplementation : IMedicalPractionerAccount
        {
            return (TAccountImplementation)(IMedicalPractionerAccount)await base.UpdateAccountAsync<MedicalPractionerAccount>(SystemUser.Cast<MedicalPractionerAccount>(medicalPractionerAccount));
        }

        /// <summary>
        /// Deltes a medical practioner account.
        /// </summary>
        /// <typeparam name="TAccountImplementation">The type of account implementation to be updated. <typeparamref name="TAccountImplementation"/> implements <see cref="IMedicalPractionerAccount"/>.</typeparam>
        /// <param name="patientCPF">The medical practioner CPF.</param>
        /// <returns>The provided medical practioner account deleted.</returns>
        protected new async Task<Boolean> DeleteAccount<TAccountImplementation>(String patientCPF) where TAccountImplementation : IMedicalPractionerAccount
        {
            return Convert.ToBoolean(await base.DeleteAccount<MedicalPractionerAccount>(patientCPF));
        }

        /// <summary>
        /// Retrieves the system's account considering the specified account type model. The specified type model must implements <see cref="IMedicalPractionerAccount"/>.
        /// </summary>
        /// <typeparam name="TImplementation">The type model of medical account to seek in where <typeparamref name="TImplementation"/> implements <see cref="IMedicalAccountCredential"/> data model.</typeparam>
        /// <param name="medicalPractionerCPF">The Medical Practioner CPF. (The Brazilian national-wide unique identification number)</param>
        /// <returns>A medical account model instance in where <typeparamref name="TImplementation"/> implements <see cref="IMedicalAccountCredential"/>.</returns>
        protected async Task<TImplementation> RequestAccountCredentialAsync<TImplementation>(String medicalPractionerCPF) where TImplementation : IMedicalAccountCredential
        {
            IMedicalPractionerAccount MedicalPractioner = await this.RetrieveAccountAsync<MedicalPractionerAccount>(medicalPractionerCPF);

            IMedicalAccountCredential MedicalAccountCredentials = new MedicalAccountCredential(MedicalPractioner.Password);

            if(!MedicalAccountCredentials.IsNullOrEmpty())
                return (TImplementation)MedicalAccountCredentials;

            return MedicalAccountCredential.Empty<TImplementation>();
        }

        /// <summary>
        /// Creates a new medical practioner account.
        /// </summary>
        /// <typeparam name="TAccountImplementation">The type of account implementation to be updated. <typeparamref name="TAccountImplementation"/> implements <see cref="IMedicalPractionerAccount"/>.</typeparam>
        /// <param name="medicalPractionerAccountCredentials">The medical practioner account to be updated.</param>
        /// <returns>The created medical practioner account.</returns>
        protected async Task<TAccountImplementation> CreateAccount<TAccountImplementation>(TAccountImplementation medicalPractionerAccountCredentials) where TAccountImplementation : IMedicalPractionerAccount
        {
            IMedicalPractionerAccount InsertedMedicalPractioner = await this.Push<MedicalPractionerAccount>(SystemUser.Cast<MedicalPractionerAccount>(medicalPractionerAccountCredentials));

            return InsertedMedicalPractioner.CPF is not null ? (TAccountImplementation)InsertedMedicalPractioner : (TAccountImplementation)MedicalPractionerAccount.Empty();
        }
    }
}