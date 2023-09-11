using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MedicalStaff.WebService.Core.Data;
using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Models.Runtime;
using MedicalStaff.WebService.Core.Helpers.Mappers;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Models.Db.Patient;

namespace MedicalStaff.WebService.Core.Services.Accounts
{
    /// <summary>
    /// Provides access to the common Patient's accounts services.
    /// </summary>
    public partial class PatientAccountService : Infrastructure.Accounts.AccountsRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PatientAccountService"/> by passing in its data-layer access. <see cref="SystemDbContext"/> is the bridge between this service and its corresponding database IO-Concerns mechanisms.
        /// </summary>
        /// <param name="applicationDbContext">The system's database context through where the <see cref="PatientAccountService"/> access and perform database-centered critical IO operations.</param>
        protected PatientAccountService(SystemDbContext applicationDbContext) : base(applicationDbContext) { }

        /// <summary>
        /// Retrieves the patient's account regarding the specified account type-model. The specified type-model must implements <see cref="IPatientAccount"/>.
        /// </summary>
        /// <typeparam name="TModel">The type of account to seek where <typeparamref name="TModel"/> implements <see cref="IPatientAccount"/> data-model.</typeparam>
        /// <returns>An <see cref="IEnumerable{T}"/> of <typeparamref name="TModel"/>.</returns>
        protected new async Task<IEnumerable<TModel>> QueryAccountsAsync<TModel>() where TModel : IPatientAccount
        {
            return (IEnumerable<TModel>)await base.QueryAccountsAsync<PatientAccount>();
        }

        /// <summary>
        /// Retrieves any Patient's account considering the specified account type-model, where the account implements an <see cref="IPatientAccount"/> data-model.
        /// </summary>
        /// <typeparam name="TAccount">The type-model of patient account to seek where <typeparamref name="TAccount"/> implements <see cref="IPatientAccount"/> data-model.</typeparam>
        /// <param name="patientCPF">The Patient CPF. (The Brazilian national-wide unique identification number)</param>
        /// <returns>A Patient's account model instance in where <typeparamref name="TAccount"/> implements <see cref="IPatientAccount"/>.</returns>>
        /// <returns></returns>
        protected new async Task<TAccount> RetrieveAccountAsync<TAccount>(String patientCPF) where TAccount : IPatientAccount
        {
            return (TAccount)(IPatientAccount)await base.RetrieveAccountAsync<PatientAccount>(patientCPF);
        }

        /// <summary>
        /// Updates the target patient account.
        /// </summary>
        /// <typeparam name="TAccountImplementation">The type of account implementation to be updated. <typeparamref name="TAccountImplementation"/> implements <see cref="IPatientAccount"/>.</typeparam>
        /// <param name="patientAccount"></param>
        /// <returns>The provided patient account updated.</returns>
        protected new async Task<TAccountImplementation> UpdateAccountAsync<TAccountImplementation>(TAccountImplementation patientAccount) where TAccountImplementation : IPatientAccount
        {
            return (TAccountImplementation)(IPatientAccount)await base.UpdateAccountAsync(SystemUser.Cast<PatientAccount>(patientAccount));
        }

        /// <summary>
        /// Deltes a patient account.
        /// </summary>
        /// <typeparam name="TAccountImplementation">The type of account implementation to be updated. <typeparamref name="TAccountImplementation"/> implements <see cref="IPatientAccount"/>.</typeparam>
        /// <param name="patientCPF">The patient CPF.</param>
        /// <returns>The provided patient account deleted.</returns>
        protected new async Task<Boolean> DeleteAccount<TAccountImplementation>(String patientCPF) where TAccountImplementation : IPatientAccount
        {
            return Convert.ToBoolean(await base.DeleteAccount<PatientAccount>(patientCPF));
        }

        /// <summary>
        /// Retrieves the system's account regarding the specified account type-model, where the account implements an <see cref="IPatientAccountCredential"/> data-model.
        /// </summary>
        /// <typeparam name="TImplementation">The type-model of patient account to seek where <typeparamref name="TImplementation"/> implements <see cref="IPatientAccountCredential"/> data-model.</typeparam>
        /// <param name="patientCPF">The Patient CPF. (The Brazilian national-wide unique identification number)</param>
        /// <returns>A patient account model instance in where <typeparamref name="TImplementation"/> implements <see cref="IPatientAccountCredential"/>.</returns>
        protected async Task<TImplementation> RequestAccountCredentialAsync<TImplementation>(String patientCPF) where TImplementation : IPatientAccountCredential
        {
            IPatientAccount Patient = await RetrieveAccountAsync<PatientAccount>(patientCPF);

            IPatientAccountCredential PatientAccountCredentials = new PatientAccountCrendential(Patient.Password);

            if (!PatientAccountCredentials.IsNullOrEmpty())
                return (TImplementation)PatientAccountCredentials;

            return PatientAccountCrendential.Empty<TImplementation>();
        }

        /// <summary>
        /// Creates a new patient account.
        /// </summary>
        /// <typeparam name="TAccountImplementation">The type of account implementation to be updated. <typeparamref name="TAccountImplementation"/> implements <see cref="IPatientAccount"/>.</typeparam>
        /// <param name="patientAccount">The patient account to be updated.</param>
        /// <returns>The created patient account.</returns>
        protected async Task<TAccountImplementation> CreateAccount<TAccountImplementation>(TAccountImplementation patientAccount) where TAccountImplementation : IPatientAccount
        {
            IPatientAccount InsertedPatient = await Push(SystemUser.Cast<PatientAccount>(patientAccount));

            return InsertedPatient.CPF is not null ? (TAccountImplementation)InsertedPatient : (TAccountImplementation)PatientAccount.Empty();
        }
    }
}