using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalStaff.WebService.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAccountService<T> where T : ISystemUser
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<Model>> QueryAccountsAsync<Model>() where Model : T;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="medicalPractitionerCPF"></param>
        /// <returns></returns>
        Task<Model> GetAccountAsync<Model>(System.String medicalPractitionerCPF) where Model : T;
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<Model> UpdateAsync<Model>(Model account) where Model : T;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="physicianCPF"></param>
        /// <returns></returns>
        Task<System.Boolean> DeleteAsync<Model>(System.String physicianCPF) where Model : T;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="CPF"></param>
        /// <returns></returns>
        Task<Model> GetCredentialAsync<Model>(System.String CPF) where Model : IAccountCredential;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<IDbOperation<T>> Create(T account) ;
    }
}
