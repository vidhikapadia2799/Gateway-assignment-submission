using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SourceControlAssignment1.Custom_Validation;

namespace SourceControlAssignment1.Models
{
    public class Register
    {
        [Required(ErrorMessage ="UserId is required")]
        public int UserId { get; set; }

        [Required]
        [StringLength(20,MinimumLength =4,ErrorMessage ="Name should be between 4 to 20 characters")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password",ErrorMessage ="Password Mismatch")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

        [Required]
        [Range(18,40,ErrorMessage ="Age should be between 18 to 40")]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"\+91[0-9]{10}")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please enter joining date")]
        [Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [CustomJoiningDate(ErrorMessage = "Joining Date must be less than or equal to Today's Date")]
        public DateTime JoiningDate { get; set; }

        [Required]
        [Url]
        public string Website { get; set; }
    }
}