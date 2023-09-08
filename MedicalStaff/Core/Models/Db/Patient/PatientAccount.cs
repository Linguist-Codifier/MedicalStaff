using System;
using System.ComponentModel;
using MedicalRecordsSystem.WebService.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using MedicalRecordsSystem.WebService.Core.Helpers.Properties;
using MedicalRecordsSystem.WebService.Core.Models.Transfer.Patient.SignUp;

namespace MedicalRecordsSystem.WebService.Core.Models.Db.Patient
{
    /// <summary>
    /// Represents an <see cref="IPatientAccount"/> type of account.
    /// </summary>
    public class PatientAccount : IPatientAccount
    {
        /// <summary>
        /// The unique Token for identifying this patient.
        /// </summary>
        [Key]
        [Description("The national Token for identifying this patient.")]
        public Guid ID { get; set; }

        /// <summary>
        /// The unique(CPF - Brazilian national-wide identification number) Token for identifying this patient.
        /// </summary>
        [Required]
        [StringLength(maximumLength: 14, MinimumLength = 14, ErrorMessage = "The CPF min and max length must be both 14.")]
        [Description("The unique(CPF - Brazilian national-wide identification number) Token for identifying this MedicalPractioner.")]
        public String CPF { get; set; }

        /// <summary>
        /// The patient's name.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The patient's name is a required field.")]
        [StringLength(maximumLength: 100, ErrorMessage = "The patient's. name must be up to One Hundred(100) characters long.")]
        [Description("The patient's name.")]
        public String Name { get; set; }

        /// <summary>
        /// The patient's account access password.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The patient's account access password is a required field.")]
        [Description("The patient's account access password.")]
        [StringLength(maximumLength: 100, ErrorMessage = "The patient's. account password must be up to One Hundred(100) characters long.")]
        public String Password { get; set; }

        /// <summary>
        /// The patient's account access mail address.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The patient's account access mail address is a required field.")]
        [Description("The patient's account access mail address.")]
        [StringLength(maximumLength: 100, ErrorMessage = "The patient's account mail address must be up to One Hundred(100) characters long.")]
        public String Email { get; set; }

        /// <summary>
        /// The patient's account type.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The patient's account type is a required field.")]
        [Description("The patient's account type.")]
        public Role Role { get => Role.Patient; set { } }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="PatientAccount"/> and bind the specified arguments with its object properties with the specified <see cref="PatientAccount"/> object.
        /// </summary>
        /// <param name="ID">The unique national Token for identifying this patient.</param>
        /// <param name="CPF">An unique(CPF - Brazilian national-wide identification number) Token for identifying this patient.</param>
        /// <param name="name">The patient's name.</param>
        /// <param name="password">The patient's account access password.</param>
        /// <param name="email">The patient's account access mail address.</param>
        public PatientAccount(Guid ID, String CPF, String name, String email, String password)
        {
            this.ID = ID;
            this.CPF = CPF;
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PatientAccount"/> and bind the specified arguments with its object properties with the specified <see cref="PatientAccount"/> object.
        /// </summary>
        /// <param name="patient">Any <see cref="PatientAccount"/> instance.</param>
        public PatientAccount(PatientAccount patient)
        {
            this.ID = patient.ID;
            this.CPF = patient.CPF;
            this.Name = patient.Name;
            this.Email = patient.Email;
            this.Password = patient.Password;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PatientAccount"/> and bind the specified arguments with its object properties with the specified <see cref="IPatientAccount"/> object implementation.
        /// </summary>
        /// <param name="patient">Any <see cref="IPatientAccount"/> implementation.</param>
        public PatientAccount(IPatientAccount patient)
        {
            this.ID = patient.ID;
            this.CPF = patient.CPF;
            this.Name = patient.Name;
            this.Email = patient.Email;
            this.Password = patient.Password;
        }

        /// <summary>
        /// <see cref="PatientAccount"/> Constructor.
        /// </summary>
        /// <param name="patient">A <see cref="PatientSignUp"/> data-tranfer-model.</param>
        public PatientAccount(PatientSignUp patient)
        {
            this.ID = Guid.NewGuid();
            this.CPF = patient.CPF;
            this.Name = patient.Name;
            this.Email = patient.Email;
            this.Password = patient.Password;
        }

        /// <summary>
        /// <see cref="PatientAccount"/> Constructor.
        /// </summary>
        public PatientAccount()
        {
            this.ID = Guid.Empty;
            this.CPF = String.Empty;
            this.Name = String.Empty;
            this.Email = String.Empty;
            this.Password = String.Empty;
        }

        #endregion

        /// <summary>
        /// Provides an empty <see cref="IPatientAccount"/> instance.
        /// </summary>
        /// <returns>The default empty <see cref="IPatientAccount"/> instance.</returns>
        public static IPatientAccount Empty() => new PatientAccount();
    }
}
