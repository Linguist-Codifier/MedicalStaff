using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Helpers.Filters;

namespace MedicalStaff.WebService.Core.Models.Transfer.PatientRecordDTO
{
    /// <summary>
    /// Represents a common patient record data tranfer object.
    /// </summary>
    public struct PatientRecordsDTO : IPatientRecordsDTO
    {
        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} cannot be empty.")]
        [StringLength(maximumLength: 100, ErrorMessage = "Max length is 100 characters long.")]
        public String Name { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} cannot be empty.")]
        [Description("The unique (CPF - Brazilian national-wide identification number) Token for identifying this MedicalPractioner.")]
        [CPF]
        public String CPF { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} cannot be empty.")]
        [DataType(DataType.ImageUrl, ErrorMessage = "The {0} should be an image URL.")]
        [StringLength(maximumLength: 300, ErrorMessage = "Max length is 200 characters long.")]
        public String PictureLocation { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} cannot be empty.")]
        [E164]
        public String Phone { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} cannot be empty.")]
        [DataType(DataType.Date, ErrorMessage = "The {0} should be a date.")]
        public DateOnly Birth { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} cannot be empty.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "The {0} should be amail address.")]
        public String Email { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} cannot be empty.")]
        [StringLength(maximumLength: 100, ErrorMessage = "Max length is 100 characters long.")]
        public String Address { get; set; }
    }
}