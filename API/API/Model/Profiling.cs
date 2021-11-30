using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    [Table("tb_t_profiling")]
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }
        [Required]
        public int EducationId { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        public virtual Education Education { get; set; }
        public bool ShouldSerializeEducation()
        {
            if (Education == null)
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