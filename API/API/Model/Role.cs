using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    [Table("tb_m_role")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual AccountRole AccountRole { get; set; }
    }
}
