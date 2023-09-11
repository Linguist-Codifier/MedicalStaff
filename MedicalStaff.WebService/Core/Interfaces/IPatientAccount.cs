using System;
using System.ComponentModel;
using MedicalStaff.WebService.Core.Helpers.Properties;

namespace MedicalStaff.WebService.Core.Interfaces
{
    /// <summary>
    /// Represents a Patient account.
    /// </summary>
    public interface IPatientAccount : ISystemUser
    {
        /// <summary>
        /// Represents the unique national Token for identifying the Patient.
        /// </summary>
        [Description("Represents the unique national Token for identifying the Patient.")]
        public String CPF { get; set; }

        /// <summary>
        /// Represents the Patient's name.
        /// </summary>
        [Description("The Patient's name.")]
        public String Name { get; set; }

        /// <summary>
        /// Represents the Patient's account access mail address.
        /// </summary>
        [Description("Represents the Patient's account access mail address.")]
        public String Email { get; set; }

        /// <summary>
        /// Represents the Patient's account access password.
        /// </summary>
        [Description("Represents the Patient account access password.")]
        public String Password { get; set; }

        /// <summary>
        /// Represents the type of access this accout would have.
        /// </summary>
        [Description("Represents the type of access this accout would have.")]
        public Role Role { get => Role.Patient; }
    }
}