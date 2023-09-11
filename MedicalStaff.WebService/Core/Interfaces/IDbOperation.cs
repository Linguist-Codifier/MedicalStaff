using MedicalStaff.WebService.Core.Helpers.Properties;

namespace MedicalStaff.WebService.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbOperation<TEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        TEntity? Result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbOperationsStatus? OperationStatus { get; set; }

        /// <summary>
        /// Determines whether there's any resulting instance of any operation and returns its reference. When no result is address to this current operation then an exception is launched at runtime.
        /// </summary>
        /// <returns>The result of the operation as non-nullable instance.</returns>
        /// <exception cref="System.NullReferenceException"></exception>
        TEntity EnsureInstance();
    }
}
