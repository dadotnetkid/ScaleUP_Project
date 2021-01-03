using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.SyncF.Models
{
    public class Employees
    {
        [Key] public int Id { get; set; }
        [MaxLength(50)] public string FirstName { get; set; }
        [MaxLength(50)] public string MiddleName { get; set; }
        [MaxLength(50)] public string LastName { get; set; }
        public string Position { get; set; }
        [MaxLength(50)] public string SalaryGrade { get; set; }

        
    }
}