using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    [Table("tb_t_account_role")]
    public class AccountRole
    {
        [Key]
        public int AccountRoleId { get; set; }
        [Required]
        public string AccountNIK { get; set; }

        [Required]
        public int RoleId { get; set; }
        [JsonIgnore]
        public virtual ICollection<Role> Roles { get; set; }
        [JsonIgnore]
        public virtual ICollection<Account> Accounts { get; set; }

    }
}
