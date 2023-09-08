using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace MedicalRecordsSystem.WebService.Core.Models.Transfer.Patient.SignUp
{
    /// <summary>
    /// Represents a patient account on signing up.
    /// </summary>
    public struct PatientSignUp
    {
        /// <summary>
        /// The unique(CPF - Brazilian national-wide identification number) Token for identifying this MedicalPractioner.
        /// </summary>
        [Required]
        [StringLength(maximumLength: 14, MinimumLength = 11, ErrorMessage = "The CPF min and max length must be respectively 11 and 14.")]
        [Description("The unique national Token for identifying this Doctor.")]
        public String CPF { get; set; }

        /// <summary>
        /// The patient's name.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Doctor's name is a required field.")]
        [StringLength(maximumLength: 100, ErrorMessage = "The Doctor's. name must be up to One Hundred(100) characters long.")]
        [Description("The Doctor's name.")]
        public String Name { get; set; }

        /// <summary>
        /// The patient's account access password.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Doctor's account access password is a required field.")]
        [StringLength(maximumLength: 100, ErrorMessage = "The Doctor's. account password must be up to One Hundred(100) characters long.")]
        [Description("The Doctor's account access password.")]
        public String Password { get; set; }

        /// <summary>
        /// The patient's account access mail address.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Doctor's account access mail address is a required field.")]
        [Description("The Doctor's account access mail address.")]
        [StringLength(maximumLength: 100, ErrorMessage = "The Doctor's account mail address must be up to One Hundred(100) characters long.")]
        public String Email { get; set; }
    }
}