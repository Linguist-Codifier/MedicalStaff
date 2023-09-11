using System;
using Microsoft.IdentityModel.Tokens;
using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Helpers.Mappers;
using MedicalStaff.WebService.Core.Helpers.Properties;

namespace MedicalStaff.WebService.Core.Helpers.Analysers
{
    /// <summary>
    /// Provides the <see cref="MedicalAccountCredential"/> instances with common validation helpers methods.
    /// </summary>
    public static class AcountCredentialsUtility : Object
    {
        /// <summary>
        /// Verifies whether this current account credential type implementation is valid or not. A valid <see cref="MedicalAccountCredential"/> account is regarded as such when the '<see cref="MedicalAccountCredential.Credential"/>' is not neither <see langword="null"></see> nor empty.
        /// </summary>
        /// <typeparam name="TCredential">The type of implementation of this account credentials. <typeparamref name="TCredential"/> must implement <see cref="IAccountCredentials"/>.</typeparam>
        /// <param name="_">This <see cref="IAccountCredentials"/> implementation.</param>
        /// <returns><see langword="true"/> if this current <typeparamref name="TCredential"/> credential is valid; otherwise, <see langword="false"/>.</returns>
        public static Boolean IsNullOrEmpty<TCredential>(this TCredential _) where TCredential : IAccountCredentials
        {
            return _.Credential.IsNullOrEmpty<Char>();
        }

        /// <summary>
        /// Verifies whether this current account's implementation type is valid or not. An <see cref="ISystemUser"/> account is properly valid when its properties are assigned, depending on the implementation of this type of account.
        /// </summary>
        /// <typeparam name="TCredential">The type of account implementation.</typeparam>
        /// <param name="_">This account implementation.</param>
        /// <returns><see langword="true"></see> whether this current account implementation has its properties fully assigned, considering on its implementation type; otherwise <see langword="false"></see>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Boolean IsValid<TCredential>(this TCredential _) where TCredential : ISystemUser
        {
            if (typeof(TCredential).Implements<IPhysicianAccount>())
            {
                return !((IPhysicianAccount)_).ID.Default() && ((IPhysicianAccount)_).CRM is not null && ((IPhysicianAccount)_).CPF is not null
                    && ((IPhysicianAccount)_).Name is not null && ((IPhysicianAccount)_).Email is not null
                    && ((IPhysicianAccount)_).Password is not null && ((IPhysicianAccount)_).Role is Role.MedicalPractioner;
            }

            if (typeof(TCredential).Implements<IPatientAccount>())
            {
                return !((IPatientAccount)_).ID.Default() && ((IPatientAccount)_).CPF is not null
                    && ((IPatientAccount)_).Name is not null && ((IPatientAccount)_).Email is not null
                    && ((IPatientAccount)_).Password is not null && ((IPatientAccount)_).Role is Role.Patient;
            }

            else
                throw new NotImplementedException("Type validation is not available yet.");
        }
    }
}