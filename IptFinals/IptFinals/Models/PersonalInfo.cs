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

        string constr = "Data Source=DESKTOP-FAE0OA1; Initial Catalog=IptFinalsDB;User ID=sa; Password=123456789";
        //public PersonalInfo GetStudentId(string Id)
        //{
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        string sqlQuery = "SELECT * FROM PersonalInfo Where PersonalId=@Id";
        //        SqlCommand cmd = new SqlCommand(sqlQuery, con);
        //cmd.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar).Value = Id;
        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        PersonalInfo personal = new PersonalInfo();

        //                personal.StudentId = reader.GetString(1);
        //                personal.FirstName = reader.GetString(2);
        //                personal.LastName = reader.GetString(3);
 
        //        con.Close();
        //        return personal;
        //    }
           
        //}
    }
}

