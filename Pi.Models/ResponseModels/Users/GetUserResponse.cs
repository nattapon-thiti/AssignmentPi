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
    public class GetUserResponse : BaseResponse<IEnumerable<PiUser>>
    {
        public GetUserResponse(IEnumerable<PiUser> result) : base(result) { }
    }
}
