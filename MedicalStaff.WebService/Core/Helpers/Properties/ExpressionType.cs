namespace MedicalStaff.WebService.Core.Helpers.Properties
{
    /// <summary>
    /// Specifies a regular expression type.
    /// </summary>
    public enum ExpressionType : System.Int16
    {
        /// <summary>
        /// Specifies that the expression to be evaluated is supposed to be a formatted CPF . (Brazilian national-wide unique identification number.)
        /// </summary>
        FormattedCPF = 0,

        /// <summary>
        /// Specifies that the expression to be evaluated is supposed to be a unformatted CPF . (Brazilian national-wide unique identification number.)
        /// </summary>
        UnformattedCPF = 1,

        /// <summary>
        /// Specifies that the expression to be evaluated is supposed to be a Mobile Telefone Number; E164 Standard.
        /// </summary>
        E164 = 2,

        /// <summary>
        /// Specifies that the expression to be evaluated is supposed to be a formatted CRM. (Brazilian national-wide Medical Practioner Professional Regional Identification Number)
        /// </summary>
        FormattedCRM = 3,

        /// <summary>
        /// Specifies that the expression to be evaluated is supposed to be a unformatted CRM. (Brazilian national-wide Medical Practioner Professional Regional Identification Number)
        /// </summary>
        UnformattedCRM = 4

    }
}