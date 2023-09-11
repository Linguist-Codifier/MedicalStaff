using MedicalStaff.WebService.Core.Helpers.Filters;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MedicalStaff.WebService.Core.Models.Transfer.Patient.SignUp
{
    /// <summary>
    /// Represents any Sign Up patient account.
    /// </summary>
    public struct PatientSignUpDTO
    {
        /// <summary>
        /// The unique(CPF - Brazilian national-wide identification number) Token for identifying this MedicalPractioner.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} is requiered.")]
        [CPF]
        [StringLength(maximumLength: 14, MinimumLength = 11, ErrorMessage = "Min and max length are respectively: 11, 14. Valid formats: 000.000.000-00 | 00000000000")]
        [Description("The unique national Token for identifying this patient.")]
        public String CPF { get; set; }

        /// <summary>
        /// The patient's name.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} is requiered.")]
        [StringLength(maximumLength: 100, ErrorMessage = "Max length is 100 characters long.")]
        [Description("The patient's name.")]
        public String Name { get; set; }

        /// <summary>
        /// The patient's account access password.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} is requiered.")]
        [StringLength(maximumLength: 100, ErrorMessage = "Max length is 100 characters long.")]
        [Description("The patient's account access password.")]
        public String Password { get; set; }

        /// <summary>
        /// The patient's account access mail address.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} is requiered.")]
        [StringLength(maximumLength: 100, ErrorMessage = "Max length is 100 characters long.")]
        [Description("The patient's account access mail address.")]
        public String Email { get; set; }
    }
}