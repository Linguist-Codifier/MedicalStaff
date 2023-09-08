using System;
using MedicalRecordsSystem.WebService.Core.Interfaces;
using MedicalRecordsSystem.WebService.Core.Helpers.Analysers;
using MedicalRecordsSystem.WebService.Core.Helpers.Properties;
using MedicalRecordsSystem.WebService.Core.Models.Db.Patient;
using MedicalRecordsSystem.WebService.Core.Models.Db.MedicalPractioner;

namespace MedicalRecordsSystem.WebService.Core.Models.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public struct SystemUser : ISystemUser
    {
        /// <summary>
        /// The unique Token for identifying this user..
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UserCPF { get; set; }

        /// <summary>
        /// Represents the Brazilian national-wide Medical Practioner Professional Regional Identification Number.
        /// </summary>
        public String? CRM { get; set; }

        /// <summary>
        /// The user's account name.
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// The user's account mail address.
        /// </summary>
        public String UserEmail { get; set; }

        /// <summary>
        /// The user's account password.
        /// </summary>
        public String UserPassword { get; set; }

        /// <summary>
        /// The user's account type.
        /// </summary>
        public Role UserRole { get; set; }

        /// <summary>
        /// Converts the base <see cref="ISystemUser"/> implementation type into the specified implementation model.
        /// </summary>
        /// <typeparam name="TCast"></typeparam>
        /// <param name="systemUser"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static TCast Cast<TCast>(ISystemUser systemUser) where TCast : ISystemUser
        {
            if (typeof(TCast).Implements<IMedicalPractionerAccount>())
            {
                return (TCast)(IMedicalPractionerAccount)new MedicalPractionerAccount
                (
                    ((IMedicalPractionerAccount)systemUser).ID,
                    ((IMedicalPractionerAccount)systemUser).CRM,
                    ((IMedicalPractionerAccount)systemUser).CPF.RemoveSpecifically(new[] { '.', '-' }),
                    ((IMedicalPractionerAccount)systemUser).Name,
                    ((IMedicalPractionerAccount)systemUser).Email,
                    ((IMedicalPractionerAccount)systemUser).Password
                );
            }

            else if (typeof(TCast).Implements<IPatientAccount>())
            {
                return (TCast)(IPatientAccount)new PatientAccount
                (
                    ((IPatientAccount)systemUser).ID,
                    ((IPatientAccount)systemUser).CPF.RemoveSpecifically(new[] { '.', '-' }),
                    ((IPatientAccount)systemUser).Name,
                    ((IPatientAccount)systemUser).Password,
                    ((IPatientAccount)systemUser).Email
                );
            }

            else
                throw new NotImplementedException("Conversion is not allowed yet.");
        }
    }
}