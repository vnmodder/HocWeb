using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Service.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string subject, string claimType);
        string GetClaim(string token, string claimType);
        bool ValidateCurrentToken(string token);
    }
}
