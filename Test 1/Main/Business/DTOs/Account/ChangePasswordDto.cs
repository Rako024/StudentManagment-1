using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Account
{
    public record ChangePasswordDto
    {
        [Required,DataType(DataType.Password)]
        public string PreviousPassword { get; set; }
        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required, DataType(DataType.Password),Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }

    }
}
