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
        public Nullable<int> EmployeeID { get; set; }
        public string EmpName { get; set; }
        public string DeptName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Username { get; set; }

        [Phone]
        public decimal MobileNo { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime RelevingDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsActive { get; set; }

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
