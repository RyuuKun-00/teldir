using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.DataAccess.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public List<ContactEntity> Contacts { get; set; } = new();

        public List<TokenEntity> Tokens { get; set; } = new();
    }
}
