using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Core.Models
{
    public class TokensData
    {
        public string AccessJwt { get; set; } = String.Empty;
        public string RefreshJwt { get; set; } = String.Empty;
        public DateTime Expired { get; set; }
        public int LifeTime { get; set; }
    }
}
