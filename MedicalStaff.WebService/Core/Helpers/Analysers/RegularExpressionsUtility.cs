using System;
using System.Text.RegularExpressions;
using MedicalStaff.WebService.Core.Helpers.Properties;

namespace MedicalStaff.WebService.Core.Helpers.Analysers
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
            if (expressionType.Equals(ExpressionType.FormattedCPF))
                return new Regex("[0-9]{3}\\.[0-9]{3}\\.[0-9]{3}\\-[0-9]{2}").IsMatch(expression);

            if (expressionType.Equals(ExpressionType.UnformattedCPF))
                return new Regex("[0-9]{11}").IsMatch(expression);

            else if (expressionType.Equals(ExpressionType.UnformattedCRM))
                return new Regex("[CRMcrm]{3}\\/[A-T-a-t]{2}\\ ?[0-9]{6}").IsMatch(expression);

            else if (expressionType.Equals(ExpressionType.UnformattedCPF))
                return new Regex("[0-9]{6}").IsMatch(expression);

            else if (expressionType.Equals(ExpressionType.E164))
                return new Regex("[0-9]{2}[0-9]{2}[9]{1}[0-9]{8}").IsMatch(expression);

            return false;
        }
    }
}