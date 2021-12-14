using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API.ViewModel
{
    public class RegisterVMClient
    {
        public string NIK { get; set; }
      
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public string Degree { get; set; }
        public string Gpa { get; set; }
        public string UniversityName { get; set; }
        public string Gender { get; set; }

    }
}
