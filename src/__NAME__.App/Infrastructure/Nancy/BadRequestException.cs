using System;
using System.Collections.Generic;

namespace __NAME__.App.Infrastructure.Nancy
{
    public class BadRequestException : ApplicationException
    {
        public IDictionary<string, string[]> ValidationErrors { get; }

        public BadRequestException(IDictionary<string, string[]> validationErrors)
        {
            ValidationErrors = validationErrors;
        }
    }
}
