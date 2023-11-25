using AspNetCore.Reporting;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using IptFinals.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using static System.Collections.Specialized.BitVector32;

namespace IptFinals.Models
{
    public class IdentificationModel
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




        string constr = "Data Source=DESKTOP-FAE0OA1; Initial Catalog=IptFinalsDB;User ID=sa; Password=123456789";
        public DataTable GetStudentPersonalInfo()
        {
            var dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SPGetStudentPersonalInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter  da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            return dt;
        }

    }
}
