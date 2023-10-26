using System;

namespace MedicalStaff.WebService.Core.Interfaces
{
    /// <summary>
    /// Provides the base interface for any system account-model.
    /// </summary>
    public interface IAccountCredential
    {
        /// <summary>
        /// Represents the User's account access password.
        /// </summary>
        public String Credential { get; set; }
    }
}