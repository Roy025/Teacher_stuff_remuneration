using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.OtherDTOs
{
    public class AdminLoginResDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        // public string Image { get; set; }
    }
}