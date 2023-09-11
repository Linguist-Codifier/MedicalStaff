using System.ComponentModel.DataAnnotations;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Helpers.Properties;

namespace MedicalStaff.WebService.Core.Helpers.Filters
{
    /// <summary>
    /// Specifies that the target value should be a CPF.
    /// </summary>
    public class CRM : ValidationAttribute
    {

        /// <summary>
        /// The message to display if model validation fails.
        /// </summary>
        public new System.String? ErrorMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CRM"/> class.
        /// </summary>
        public CRM() { }

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

            if (!RegularExpressionsUtility.Matches(ExpressionType.FormattedCRM, (System.String)value) && !RegularExpressionsUtility.Matches(ExpressionType.UnformattedCRM, (System.String)value))
            {
                if(!System.String.IsNullOrEmpty(this.ErrorMessage))
                    return new ValidationResult(this.ErrorMessage);

                return new ValidationResult("Invalid CRM format. Format requierd is: CRM/SP 000000 | CRM/SP 000000");
            }

            return ValidationResult.Success;
        }
    }
}
