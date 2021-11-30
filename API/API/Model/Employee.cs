using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Model
{

    [Table("tb_m_employee")]
    public class Employee 
    {
        [Key]
        public string NIK { get; set; }
       
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public virtual Account Account { get; set; }
        public bool ShouldSerializeAccount()
        {
            if (Account == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    public enum Gender
    {
        Male,
        Female
    }
}

