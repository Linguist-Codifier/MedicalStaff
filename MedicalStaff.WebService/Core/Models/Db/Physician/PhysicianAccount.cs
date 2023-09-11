using System;
using System.ComponentModel;
using MedicalStaff.WebService.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using MedicalStaff.WebService.Core.Helpers.Properties;
using MedicalStaff.WebService.Core.Models.Transfer.Physician.SignUp;
using MedicalStaff.WebService.Core.Helpers.Attributes;

namespace MedicalStaff.WebService.Core.Models.Db.Physician
{
    /// <summary>
    /// Represents an <see cref="IPhysicianAccount"/> type of account.
    /// </summary>
    public sealed class PhysicianAccount : IPhysicianAccount
    {
        /// <summary>
        /// The unique Token for identifying this MedicalPractioner.
        /// </summary>
        [Key]
        [Description("The unique national Token for identifying this MedicalPractioner.")]
        public Guid ID { get; set; }

        /// <summary>
        /// The Brazilian national-wide Medical Practioner Professional Regional Identification Number.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The CRM is a requiered field.")]
        [CRM]
        [StringLength(maximumLength: 13, MinimumLength = 12, ErrorMessage = "The CRM is required. Format requierd is: CRM/SP 000000 | CRM/SP 000000")]
        [Description("The Brazilian national-wide Medical Practioner Professional Regional Identification Number.")]
        public String CRM { get; set; }

        /// <summary>
        /// The unique(CPF - Brazilian national-wide identification number) Token for identifying this MedicalPractioner.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The CPF is a requiered field.")]
        [CPF]
        [StringLength(maximumLength: 14, MinimumLength = 14, ErrorMessage = "The CPF min and max length must be both 14.")]
        [Description("The unique(CPF - Brazilian national-wide identification number) Token for identifying this MedicalPractioner.")]
        public String CPF { get; set; }

        /// <summary>
        /// The Medical Practioner's name.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Medical Practioner's name is a required field.")]
        [StringLength(maximumLength: 100, ErrorMessage = "The Medical Practioner's. name must be up to One Hundred(100) characters long.")]
        [Description("The Medical Practioner's name.")]
        public String Name { get; set; }

        /// <summary>
        /// The Medical Practioner's account access mail address.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Medical Practioner's account access mail address is a required field.")]
        [StringLength(maximumLength: 100, ErrorMessage = "The Medical Practioner's account mail address must be up to One Hundred(100) characters long.")]
        [Description("The Medical Practioner's account access mail address.")]
        public String Email { get; set; }

        /// <summary>
        /// The Medical Practioner's account access password.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Medical Practioner's account access password is a required field.")]
        [StringLength(maximumLength: 100, ErrorMessage = "The Medical Practioner's. account password must be up to One Hundred(100) characters long.")]
        [Description("The Medical Practioner's account access password.")]
        public String Password { get; set; }

        /// <summary>
        /// The patient's account type.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The User's account type is a required field.")]
        [Description("The patient's account type.")]
        public Role Role { get => Role.MedicalPractioner; set { } }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="PhysicianAccount"/> and bind the specified arguments with its object properties with the specified <see cref="PhysicianAccount"/> object.
        /// </summary>
        /// <param name="ID">The unique national Token for identifying this MedicalPractioner.</param>
        /// <param name="CRM">The Brazilian national-wide Medical Practioner Professional Regional Identification Number.</param>
        /// <param name="CPF">An unique(CPF - Brazilian national-wide identification number) Token for identifying this MedicalPractioner.</param>
        /// <param name="name">The Medical Practioner's name.</param>
        /// <param name="password">The Medical Practioner's account access password.</param>
        /// <param name="email">The Medical Practioner's account access mail address.</param>
        public PhysicianAccount(Guid ID, String CRM, String CPF, String name, String email, String password)
        {
            this.ID = ID;
            this.CRM = CRM;
            this.CPF = CPF;
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PhysicianAccount"/> and bind the specified arguments with its object properties with the specified <see cref="PhysicianAccount"/> object.
        /// </summary>
        /// <param name="medicalPractioner">Any <see cref="PhysicianAccount"/> instance.</param>
        public PhysicianAccount(PhysicianAccount medicalPractioner)
        {
            this.ID = medicalPractioner.ID;
            this.CRM = medicalPractioner.CRM;
            this.CPF = medicalPractioner.CPF;
            this.Name = medicalPractioner.Name;
            this.Email = medicalPractioner.Email;
            this.Password = medicalPractioner.Password;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PhysicianAccount"/> and bind the specified arguments with its object properties with the specified <see cref="IPhysicianAccount"/> object implementation.
        /// </summary>
        /// <param name="medicalPractioner">Any <see cref="IPatientAccount"/> implementation.</param>
        public PhysicianAccount(IPhysicianAccount medicalPractioner)
        {
            this.ID = medicalPractioner.ID;
            this.CRM = medicalPractioner.CRM;
            this.CPF = medicalPractioner.CPF;
            this.Name = medicalPractioner.Name;
            this.Email = medicalPractioner.Email;
            this.Password = medicalPractioner.Password;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PhysicianAccount"/> and bind the specified arguments with its object properties with the specified <see cref="PhysicianAccountDTO"/> object implementation.
        /// </summary>
        /// <param name="medicalPractioner">A <see cref="PhysicianAccountDTO"/> data-tranfer-model.</param>
        public PhysicianAccount(PhysicianAccountDTO medicalPractioner)
        {
            this.ID = Guid.NewGuid();
            this.CRM = medicalPractioner.CRM;
            this.CPF = medicalPractioner.CPF;
            this.Name = medicalPractioner.Name;
            this.Email = medicalPractioner.Email;
            this.Password = medicalPractioner.Password;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PhysicianAccount"/>.
        /// </summary>
        public PhysicianAccount()
        {
            this.ID = Guid.Empty;
            this.CPF = String.Empty;
            this.CRM = String.Empty; 
            this.Name = String.Empty;
            this.Email = String.Empty;
            this.Password = String.Empty;
        }

        #endregion

        /// <summary>
        /// Provides an empty <see cref="IPhysicianAccount"/> instance.
        /// </summary>
        /// <returns>The default empty <see cref="IPhysicianAccount"/> implementation.</returns>
        public static IPhysicianAccount Empty() => new PhysicianAccount();
    }
}