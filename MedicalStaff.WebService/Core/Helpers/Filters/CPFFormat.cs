using System.ComponentModel.DataAnnotations;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Helpers.Properties;

namespace MedicalStaff.WebService.Core.Helpers.Filters
{
    /// <summary>
    /// Specifies that the target value should be a CPF.
    /// </summary>
    public class CPF : ValidationAttribute
    {

        /// <summary>
        /// The message to display if model validation fails.
        /// </summary>
        public new System.String? ErrorMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CPF"/> class.
        /// </summary>
        public CPF() { }

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

            if (!RegularExpressionsUtility.Matches(ExpressionType.FormattedCPF, (System.String)value) && !RegularExpressionsUtility.Matches(ExpressionType.UnformattedCPF, (System.String)value))
            {
                if(!System.String.IsNullOrEmpty(this.ErrorMessage))
                    return new ValidationResult(this.ErrorMessage);

                return new ValidationResult("Invalid CPF format. Format requiered is: 000.000.000-00 | 00000000000");
            }

            return ValidationResult.Success;
        }
    }
}
