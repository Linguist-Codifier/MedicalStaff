using MedicalStaff.WebService.Core.Helpers.Filters;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MedicalStaff.WebService.Core.Models.Transfer.Physician.SignUp
{
    /// <summary>
    /// Represents any Sign Up medical practioner account.
    /// </summary>
    public struct PhysicianAccountDTO
    {
        /// <summary>
        /// Represents the Brazilian national-wide Medical Practioner Professional Regional Identification Number.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} is requiered.")]
        [CRM]
        [StringLength(maximumLength: 13, MinimumLength = 6, ErrorMessage = "Format requierd is: CRM/SP 000000 | CRM/SP000000 | 000000")]
        [Description("Represents the Brazilian national-wide Medical Practioner Professional Regional Identification Number.")]
        public String CRM { get; set; }

        /// <summary>
        /// The unique(CPF - Brazilian national-wide identification number) Token for identifying this MedicalPractioner.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} is requiered.")]
        [CPF]
        [StringLength(maximumLength: 14, MinimumLength = 11, ErrorMessage = "Min and max length are respectively: 11, 14. Valid formats: 000.000.000-00 | 00000000000")]
        [Description("The unique (CPF - Brazilian national-wide identification number) Token for identifying this MedicalPractioner.")]
        public String CPF { get; set; }

        /// <summary>
        /// The Medical Practioner's name.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} is required.")]
        [StringLength(maximumLength: 100, ErrorMessage = "Max length is 100 characters long.")]
        [Description("The Medical Practioner's name.")]
        public String Name { get; set; }

        /// <summary>
        /// The Medical Practioner's account access mail address.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} is required.")]
        [StringLength(maximumLength: 100, ErrorMessage = "Max length is 100 characters long.")]
        [Description("The Medical Practioner's account access mail address.")]
        public String Email { get; set; }

        /// <summary>
        /// The Medical Practioner's account access password.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} is required.")]
        [StringLength(maximumLength: 100, ErrorMessage = "Max length is 100 characters long.")]
        [Description("The Medical Practioner's account access password.")]
        public String Password { get; set; }
    }
}