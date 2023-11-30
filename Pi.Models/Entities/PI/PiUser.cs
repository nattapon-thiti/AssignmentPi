using System;
using System.Collections.Generic;

namespace Pi.Models.Entities.PI
{
    public partial class PiUser
    {
        public int Id { get; set; }
        public string? GivenName { get; set; }
        public string? Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
