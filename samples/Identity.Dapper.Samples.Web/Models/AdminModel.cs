using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Dapper.Samples.Web.Models
{
    public class Employee
    {
        [Key]
        public Nullable<int> employeeID { get; set; }
        public string empName { get; set; }
        public string deptName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string username { get; set; }

        [Phone]
        public decimal mobileNo { get; set; }
        public Nullable<DateTime> joiningDate { get; set; }
        public Nullable<DateTime> relevingDate { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
        public bool isActive { get; set; }

        //public decimal CurrentCTC { get; set; }
        //public string EmployerComment { get; set; }
        //public string EmployeeComment { get; set; }
    }
    public enum Department
    {
        Development,
        Testing,
        Analysis
    };
}
