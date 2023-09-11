using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Helpers.Properties;
using System.ComponentModel.DataAnnotations;

namespace MedicalStaff.WebService.Core.Helpers.Attributes
{
    /// <summary>
    /// Specifies that the target value should be at E.164 format.
    /// </summary>
    public class E164 : ValidationAttribute
    {
        /// <summary>
        /// The message to display if model validation fails.
        /// </summary>
        public new System.String? ErrorMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CRM"/> class.
        /// </summary>
        public E164() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult? IsValid(System.Object? value, ValidationContext validationContext)
        {
            if (value is null)
                return new ValidationResult("Value cannot be null.");

            if (value is not System.String)
                return new ValidationResult("Value is not a String reference.");

            if (((System.String)value).Empty())
                return new ValidationResult("Value cannot be empty.");

            if (!RegularExpressionsUtility.Matches(ExpressionType.E164, (System.String)value))
            {
                if (!System.String.IsNullOrEmpty(this.ErrorMessage))
                    return new ValidationResult(this.ErrorMessage);

                return new ValidationResult("Foramt is invalid. Valid format is E.164 - 0011234455667");
            }

            return ValidationResult.Success;
        }
    }
}
