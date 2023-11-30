using System;
using System.Collections.Generic;

namespace Pi.Models.Entities.PI
{
    public partial class PiAdmin
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? MockFrontEndAccessToken { get; set; }
        public string? MockFrontEndRefreshToken { get; set; }
    }
}
