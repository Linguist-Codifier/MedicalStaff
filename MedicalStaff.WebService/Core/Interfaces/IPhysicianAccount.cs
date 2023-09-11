using System;
using System.ComponentModel;
using MedicalStaff.WebService.Core.Helpers.Properties;

namespace MedicalStaff.WebService.Core.Interfaces
{
    /// <summary>
    /// Represents a Medical Practioner's account.
    /// </summary>
    public interface IPhysicianAccount : ISystemUser
    {
        /// <summary>
        /// Represents the Brazilian national-wide Medical Practioner Professional Regional Identification Number.
        /// </summary>
        [Description("Represents the Brazilian national-wide Medical Practioner Professional Regional Identification Number.")]
        public String CRM { get; set; }

        /// <summary>
        /// Represents the unique national Token for identifying the Medical Practioner's.
        /// </summary>
        [Description("Represents the unique national Token for identifying the Medical Practioner's.")]
        public String CPF { get; set; }

        /// <summary>
        /// Represents the Medical Practioner's name.
        /// </summary>
        [Description("The Medical Practioner's name.")]
        public String Name { get; set; }

        /// <summary>
        /// Represents the Medical Practioner's account access mail address.
        /// </summary>
        [Description("Represents the Medical Practioner's account access mail address.")]
        public String Email { get; set; }

        /// <summary>
        /// Represents the Medical Practioner's account access password.
        /// </summary>
        [Description("Represents the Medical Practioner's account access password.")]
        public String Password { get; set; }

        /// <summary>
        /// Represents the type of access this accout would have.
        /// </summary>
        [Description("Represents the type of access this accout would have.")]
        public Role Role { get => Role.Physician; }
    }
}