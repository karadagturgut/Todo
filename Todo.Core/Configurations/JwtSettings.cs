using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.Configurations
{
    public sealed class JwtSettings
    {
        [Required] public string Issuer { get; init; }
        [Required] public string Audience { get; init; }
        [Required, MinLength(16)] public string Key { get; init; }
        public int ExpiryMinutes { get; init; } = 60;
    }
}
