using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using System.Web;

namespace IptFinals.Models
{
    public class PersonalInfo
    {
        [Key]
        [Display(Name = "PersonalId")]
        //[StringLength(450)]
        public string PersonalId { get; set; }

        [Display(Name = "StudentId")]
        //[StringLength(450)]
        public string StudentId { get; set; }


        [Display(Name = "UserId")]
        //[StringLength(450)]
        public string UserId { get; set; }


        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        //[StringLength(450)]

        [Display(Name = "LastName")]
        public string LastName { get; set; }
        //[StringLength(450)]

        [Display(Name = "Section")]
        public string Section { get; set; }
        //[StringLength(450)]


        [Display(Name = "Course")]
        public string Course { get; set; }
        //[StringLength(450)]

        [Display(Name = "YearLevel")]
        public string YearLevel { get; set; }
        //[StringLength(450)]


        [StringLength(11, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 11)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Not a number")]
        [Display(Name = "ContactNumber")]
        public string ContactNumber { get; set; }
        //[StringLength(450)]

        [DataType(DataType.Date)]
        [Display(Name = "DateOfBirth")]
        public string DateOfBirth { get; set; }
        //[StringLength(450)]

        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        public string Address { get; set; }
        //[StringLength(450)]

        [DataType(DataType.Text)]
        [Display(Name = "EmergencyContact")]
        public string EmergencyContact { get; set; }
        //[StringLength(450)]

        //[DataType(DataType.ImageUrl)]
        //[Display(Name = "StudentImage")]
        //public string StudentImage { get; set; }

    }
}

