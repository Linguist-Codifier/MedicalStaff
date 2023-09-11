using System;
using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Helpers.Properties;
using MedicalStaff.WebService.Core.Models.Db.Patient;
using MedicalStaff.WebService.Core.Models.Db.Physician;

namespace MedicalStaff.WebService.Core.Models.Runtime
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
            if (typeof(TCast).Implements<IPhysicianAccount>())
            {
                return (TCast)(IPhysicianAccount)new PhysicianAccount
                (
                    ((IPhysicianAccount)systemUser).ID,
                    ((IPhysicianAccount)systemUser).CRM,
                    ((IPhysicianAccount)systemUser).CPF.RemoveSpecifically(new[] { '.', '-' }),
                    ((IPhysicianAccount)systemUser).Name,
                    ((IPhysicianAccount)systemUser).Email,
                    ((IPhysicianAccount)systemUser).Password
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