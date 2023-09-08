using System;
using System.ComponentModel;

namespace MedicalRecordsSystem.WebService.Core.Interfaces
{
    /// <summary>
    /// Stabilishes the attributes any system's user must have in common.
    /// </summary>
    public interface ISystemUser
    {
        /// <summary>
        /// The unique Global Token for identifying this Doctor.
        /// </summary>
        [Description("The unique Global Token for identifying the user.")]
        public Guid ID { get; set; }
    }
}