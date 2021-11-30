using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    [Table("tb_m_education")]
    public class Education
    {
        [Key]
        public int EducationId { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string Gpa { get; set; }
        [Required]
        public int UniversityId { get; set; }
        [JsonIgnore]
        public virtual ICollection<Profiling> Profilings { get; set; }

        public virtual University University { get; set; }
        public bool ShouldSerializeUniversity()
        {
            if (University == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
