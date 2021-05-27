using System;
using System.Collections.Generic;
using System.Text;

namespace DataServeFunction.Authorization
{
    public class RequestorDto
    {
        public UserId UserId { get; }

        public RequestorDto(Guid? userId)
        {
            UserId = UserId.Of(userId);
        }
    }
}
