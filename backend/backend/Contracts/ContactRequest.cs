﻿using System.ComponentModel.DataAnnotations;

namespace backend.Contracts
{
    public record class ContactRequest()
    {
        public required bool IsGlobal { get; set; } = false;
        [Required(ErrorMessage = "Укажите имя пользователя")]
        public required string Name { get; set; } 
        [Required(ErrorMessage = "Укажите номер пользователя")]
        public required string Number { get; set; }

        public string? Description { get; set; } = string.Empty;
    }
}
