using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalStaff.Models
{
    public sealed class SignUpViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The CPF is a requirered field.")]
        [StringLength(maximumLength: 14, MinimumLength = 14)]
        public String? CPF { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The password is a requirered field.")]
        [DataType(DataType.Password)]
        public String? Password { get; set; }
    }
}