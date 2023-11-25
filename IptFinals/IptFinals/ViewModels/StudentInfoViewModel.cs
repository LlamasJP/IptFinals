using IptFinals.Models;
using System.ComponentModel.DataAnnotations;

namespace IptFinals.ViewModels
{
    public class StudentInfoViewModel
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Section { get; set; }
        public string Course { get; set; }
        public string YearLevel { get; set; }
        public string ContactNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string EmergencyContact { get; set; }


    }

}
