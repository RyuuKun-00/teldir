﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.DataAccess.Entities
{
    public class UserEntity
    {
        public Guid Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Passwors { get; set; } = string.Empty;
    }
}
