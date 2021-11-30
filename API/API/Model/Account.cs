using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    [Table("tb_m_account")]
    public class Account
    {
        [Key]
        public string NIK { get; set; }
        [Required]
        public string Password { get; set; }
        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        public virtual Profiling Profiling { get; set; }
        [JsonIgnore]
        public virtual AccountRole AccountRole { get; set; }

        public bool ShouldSerializeProfiling()
        {
            if (Profiling == null)
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
