using System.ComponentModel.DataAnnotations;

namespace MCF_TestAPI.Models
{
    public class ms_storage_location
    {
        [Key]
        public string location_id { get; set; }
        public string location_name { get; set; }
        public virtual ICollection<tr_bpkb> tr_bpkbs { get; set; }
    }
}
