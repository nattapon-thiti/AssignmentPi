using Pi.Models.Entities.PI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pi.Models.ResponseModels.Users
{
    public class CreateUserResponse : BaseResponse<PiUser>
    {
        public CreateUserResponse(PiUser result) : base(result) { }
        public CreateUserResponse(bool isSuccess, PiUser result, string message) : base(isSuccess, message, result) { }
        public CreateUserResponse() : base() { }
    }
}
