using System;
using System.Text.RegularExpressions;
using MedicalRecordsSystem.WebService.Core.Helpers.Properties;

namespace MedicalRecordsSystem.WebService.Core.Helpers.Analysers
{
    /// <summary>
    /// Provides regular expressions evaluation methods.
    /// </summary>
    public static class RegularExpressionsUtility
    {
        /// <summary>
        /// Determines whether the specified expression matches the specified expression type or not.
        /// </summary>
        /// <param name="expressionType">The type of expression to be evaluated.</param>
        /// <param name="expression">The expression to be evaluated.</param>
        /// <returns></returns>
        public static Boolean Matches(ExpressionType expressionType, String expression)
        {
            if (expressionType.Equals(ExpressionType.CPF))
                return new Regex("[0-9]{3}\\.[0-9]{3}\\.[0-9]{3}\\-[0-9]{2}").IsMatch(expression);

            else if (expressionType.Equals(ExpressionType.E164))
                return new Regex("[0-9]{2}[0-9]{2}[9]{1}[0-9]{8}").IsMatch(expression);

            else if (expressionType.Equals(ExpressionType.CRM))
                return new Regex("[CRMcrm]{3}\\/[A-T-a-t]{2}\\ ?[0-9]{6}").IsMatch(expression);

            return false;
        }
    }
}