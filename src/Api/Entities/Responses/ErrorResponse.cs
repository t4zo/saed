using SAED.API.Entities;
using System.Collections.Generic;

namespace SAED.Api.Entities.Responses
{
    public class ErrorResponse
    {
        public IEnumerable<Error> errors = new List<Error>();
    }
}
