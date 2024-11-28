using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCF_TestAPI.Models
{
    public class ms_user
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public bool isActive { get; set; }
    }
}
