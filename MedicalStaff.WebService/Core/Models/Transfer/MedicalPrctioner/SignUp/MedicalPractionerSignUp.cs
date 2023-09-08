using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace MedicalRecordsSystem.WebService.Core.Models.Transfer.MedicalPractioner.SignUp
{
    /// <summary>
    /// Represents any medical practioner account by signing up.
    /// </summary>
    public struct MedicalPractionerSignUp
    {
        /// <summary>
        /// Represents the Brazilian national-wide Medical Practioner Professional Regional Identification Number.
        /// </summary>
        [Required]
        [Description("Represents the Brazilian national-wide Medical Practioner Professional Regional Identification Number.")]
        [StringLength(maximumLength: 13, MinimumLength = 12, ErrorMessage = "The CRM is required. Format requierd is: CRM/SP 000000 | CRM/SP000000")]
        public String CRM { get; set; }

        /// <summary>
        /// The unique(CPF - Brazilian national-wide identification number) Token for identifying this MedicalPractioner.
        /// </summary>
        [Required]
        [Description("The unique(CPF - Brazilian national-wide identification number) Token for identifying this MedicalPractioner.")]
        [StringLength(maximumLength: 14, MinimumLength = 14, ErrorMessage = "The CPF min and max length must be both 14.")]
        public String CPF { get; set; }

        /// <summary>
        /// The Medical Practioner's name.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Medical Practioner's name is a required field.")]
        [StringLength(maximumLength: 100, ErrorMessage = "The Medical Practioner's. name must be up to One Hundred(100) characters long.")]
        [Description("The Medical Practioner's name.")]
        public String Name { get; set; }

        /// <summary>
        /// The Medical Practioner's account access mail address.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Medical Practioner's account access mail address is a required field.")]
        [StringLength(maximumLength: 100, ErrorMessage = "The Medical Practioner's account mail address must be up to One Hundred(100) characters long.")]
        [Description("The Medical Practioner's account access mail address.")]
        public String Email { get; set; }

        /// <summary>
        /// The Medical Practioner's account access password.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Medical Practioner's account access password is a required field.")]
        [StringLength(maximumLength: 100, ErrorMessage = "The Medical Practioner's. account password must be up to One Hundred(100) characters long.")]
        [Description("The Medical Practioner's account access password.")]
        public String Password { get; set; }
    }
}