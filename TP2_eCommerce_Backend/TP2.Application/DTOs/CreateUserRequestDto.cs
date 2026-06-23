using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Application.DTOs
{
    public class CreateUserRequestDto
    {
        public string Nas { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}
