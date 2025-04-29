using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = String.Empty;

    }
}
