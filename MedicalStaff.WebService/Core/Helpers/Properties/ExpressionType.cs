namespace MedicalRecordsSystem.WebService.Core.Helpers.Properties
{
    /// <summary>
    /// Specifies a regular expression type.
    /// </summary>
    public enum ExpressionType : System.Int16
    {
        /// <summary>
        /// Specifies that the expression to be evaluated is supposed to be a CPF. (Brazilian national-wide unique identification number.)
        /// </summary>
        CPF = 0,

        /// <summary>
        /// Specifies that the expression to be evaluated is supposed to be a Mobile Telefone Number; E164 Standard.
        /// </summary>
        E164 = 1,

        /// <summary>
        /// Specifies that the expression to be evaluated is supposed to be a CRM. (Brazilian national-wide Medical Practioner Professional Regional Identification Number)
        /// </summary>
        CRM = 2
    }
}