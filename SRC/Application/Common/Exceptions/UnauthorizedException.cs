using System;

namespace BasketballLeague.Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string email)
            : base($"User: \"{email}\" is not authorized.")
        {
        }
    }
}

