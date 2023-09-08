using System;
using System.ComponentModel.DataAnnotations;
using MedicalRecordsSystem.WebService.Core.Helpers.Analysers;

namespace MedicalRecordsSystem.WebService.Core.Models.Db.MedicalRecord
{
    /// <summary>
    /// 
    /// </summary>
    #pragma warning disable CS8618
    public class MedicalRecord
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Max length is 100 characters long.")]
        public String Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(maximumLength: 200, ErrorMessage = "Max length is 200 characters long.")]
        public String PictureLocation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(maximumLength: 14, MinimumLength = 14, ErrorMessage = "The CPF min and max length must be both 14.")]
        public String CPF { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(maximumLength: 14, ErrorMessage = "The phone number max length must be both 14 acording to 'E.164'.")]
        public String PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Max length is 100 characters long.")]
        public String Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns></returns>
        public static MedicalRecord Normalize(MedicalRecord medicalRecord)
        {
            medicalRecord.ID = Guid.NewGuid();
            medicalRecord.CPF = medicalRecord.CPF.RemoveSpecifically(new[] { '.', '-' });
            return medicalRecord;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns></returns>
        public static MedicalRecord Prepare(MedicalRecord medicalRecord)
        {
            medicalRecord.CPF = medicalRecord.CPF.RemoveSpecifically(new[] { '.', '-' });
            return medicalRecord;
        }
    }
    #pragma warning restore
}