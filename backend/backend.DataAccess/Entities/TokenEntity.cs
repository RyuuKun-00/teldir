

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.DataAccess.Entities
{
    public class TokenEntity
    {
        [Required]
        public Guid Id {  get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity User {  get; set; }

        public UserAgentEntity AgentEntity {  get; set; }

        [Required]
        public string RefreshToken { get; set; }

        [Required]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime Expired { get; set; }

        [Required]
        public int LifeTime { get; set; }

        public TokenEntity()
        {

        }

    }
}
