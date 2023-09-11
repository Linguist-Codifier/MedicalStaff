namespace MedicalStaff.WebService.Core.Helpers.Properties
{
    /// <summary>
    /// Specifies a database operation status.
    /// </summary>
    public enum DbOperationsStatus : System.Int16
    {
        /// <summary>
        /// Specifies that an operation was successfully done.
        /// </summary>
        Success = 0,

        /// <summary>
        /// Specifies that an operation failed.
        /// </summary>
        Failed = 1,

        /// <summary>
        /// Specifies the an operation is redundant.
        /// </summary>
        AlreadyExists = 2,

        /// <summary>
        /// Specifies that within a certain context, the provided operation is not allowed.
        /// </summary>
        Unallowed = 3
    }
}