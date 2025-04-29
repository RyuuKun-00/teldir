

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.DataAccess.Entities
{
    public class UserAgentEntity
    {
        [Required]
        public Guid Id {  get; set; }

        [ForeignKey("Id")]
        public TokenEntity Token { get; set; }

        [Required]
        public string OS { get; set; } = String.Empty;

        [Required]
        public string Browser { get; set; } = String.Empty;
    }
}
