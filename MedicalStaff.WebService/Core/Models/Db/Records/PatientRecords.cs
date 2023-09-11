using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Helpers.Attributes;
using MedicalStaff.WebService.Core.Models.Transfer.PatientRecordDTO;
using System.Globalization;
using System.Text.Json.Nodes;
using System.Collections.Generic;

namespace MedicalStaff.WebService.Core.Models.Db.Records
{
    /// <summary>
    /// Represents the common data that's necessary for adding any patient record.
    /// </summary>
    public class PatientRecords : IPatientRecord
    {
        /// <inheritdoc/>
        [Key]
        public Guid ID { get; set; }

        /// <inheritdoc/>
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Max length is 100 characters long.")]
        public String Name { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} is requiered.")]
        [StringLength(maximumLength: 14, MinimumLength = 11, ErrorMessage = "Min and max length are respectively: 11, 14. Valid formats: 000.000.000-00 | 00000000000")]
        [Description("The unique (CPF - Brazilian national-wide identification number) Token for identifying this MedicalPractioner.")]
        [CPF]
        public String CPF { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} cannot be empty.")]
        [StringLength(maximumLength: 300, ErrorMessage = "Max length is 200 characters long.")]
        [DataType(DataType.ImageUrl, ErrorMessage = "The picture location should be an image URL.")]
        public String PictureLocation { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} cannot be empty.")]
        [StringLength(maximumLength: 14, ErrorMessage = "The phone number max length must be both 14 acording to 'E.164'.")]
        [E164]
        public String Phone { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} cannot be empty.")]
        [DataType(DataType.Date, ErrorMessage = "The {0} should be a date.")]
        public DateTime Birth { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} cannot be empty.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "The {0} should be a mail addres.")]
        public String Email { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} cannot be empty.")]
        [DataType(DataType.DateTime, ErrorMessage = "The {0} is a required field.")]
        public DateTime Created { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} cannot be empty.")]
        [StringLength(maximumLength: 100, ErrorMessage = "Max length is 100 characters long.")]
        [E164]
        public String Address { get; set; }

        #region Contructors
        /// <summary>
        /// Initializes a new instance of <see cref="PatientRecords"/>.
        /// </summary>
        /// <param name="ID">The record unique ID.</param>
        /// <param name="name">The patient name.</param>
        /// <param name="CPF">The patient CPF.</param>
        /// <param name="birth">The patient birth date.</param>
        /// <param name="email">The patient mail address.</param>
        /// <param name="phone">The patient phone number.</param>
        /// <param name="address">The patient address.</param>
        /// <param name="pictureLocation">The patient picture location.</param>
        /// <param name="created">The record creation's date and time.</param>
        public PatientRecords(Guid ID, String name, String CPF, DateTime birth, String email, String phone, String address, String pictureLocation, DateTime created)
        {
            this.ID = ID;
            this.Name = name;
            this.CPF = CPF;
            this.Birth = birth;
            this.Email = email;
            this.Phone = phone;
            this.Address = address;
            this.PictureLocation = pictureLocation;
            this.Created = created;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PatientRecords"/>.
        /// </summary>
        /// <param name="patientRecords">An <see cref="IRecords"/> instance.</param>
        public PatientRecords(IRecords patientRecords)
        {
            this.ID = patientRecords.ID;
            this.Name = patientRecords.Name;
            this.CPF = patientRecords.CPF;
            this.Birth = patientRecords.Birth;
            this.Email = patientRecords.Email;
            this.Phone = patientRecords.Phone;
            this.Address = patientRecords.Address;
            this.PictureLocation = patientRecords.PictureLocation;
            this.Created = patientRecords.Created;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PatientRecords"/>.
        /// </summary>
        /// <param name="patientRecordDTO"></param>
        public PatientRecords(PatientRecordsDTO patientRecordDTO)
        {
            this.ID = Guid.NewGuid();
            this.CPF = patientRecordDTO.CPF;
            this.Name = patientRecordDTO.Name;
            this.Birth = new DateTime(patientRecordDTO.Birth.Year, patientRecordDTO.Birth.Month, patientRecordDTO.Birth.Day, new GregorianCalendar(GregorianCalendarTypes.USEnglish));
            this.Email = patientRecordDTO.Email;
            this.Phone = patientRecordDTO.Phone;
            this.PictureLocation = patientRecordDTO.PictureLocation;
            this.Address = patientRecordDTO.Address;
            this.Created = DateTime.Now;
        }
        #endregion

        /// <summary>
        /// Normalizes the <see cref="PatientRecords.CPF"/> property by eliminating the special characters on its compisition.
        /// </summary>
        /// <returns>This current instance.</returns>
        public PatientRecords NormalizeSpecialProperties()
        {
            this.CPF = this.CPF.RemoveSpecifically(new[] { '.', '-' });

            return this;
        }

        /// <inheritdoc/>
        public Dictionary<String, String> FormatSpecialProperties()
        {
            return new Dictionary<String, String>()
            {
                { "id", this.ID.ToString() },
                { "cpf", this.CPF },
                { "Name", this.Name },
                { "birth", this.Birth.ToShortDateString() },
                { "email", this.Email },
                { "phone", this.Phone },
                { "pictureLocation", this.PictureLocation },
                { "address", this.Address },
                { "creationDateTime", this.Created.ToString() }
            };
        }

        /// <summary>
        /// Replaces this current instance's properties with the ones from the specified <see cref="PatientRecords"/> object.
        /// </summary>
        /// <param name="patientRecords">The updated <see cref="PatientRecords"/>.</param>
        public void Update(IRecords patientRecords)
        {
            this.CPF = patientRecords.CPF;
            this.Name = patientRecords.Name;
            this.Birth = patientRecords.Birth;
            this.Email = patientRecords.Email;
            this.Phone = patientRecords.Phone;
            this.PictureLocation = patientRecords.PictureLocation;
            this.Address = patientRecords.Address;
        }

        /// <summary>
        /// Replaces this current instance's properties with the ones from the specified <see cref="IPatientRecordsDTO"/> object.
        /// </summary>
        /// <param name="patientRecords">The updated <see cref="PatientRecords"/>.</param>
        public void Update(IPatientRecordsDTO patientRecords)
        {
            this.CPF = patientRecords.CPF;
            this.Name = patientRecords.Name;
            this.Birth = new DateTime(patientRecords.Birth.Year, patientRecords.Birth.Month, patientRecords.Birth.Day, new GregorianCalendar(GregorianCalendarTypes.USEnglish));
            this.Email = patientRecords.Email;
            this.Phone = patientRecords.Phone;
            this.PictureLocation = patientRecords.PictureLocation;
            this.Address = patientRecords.Address;
        }
    }
}