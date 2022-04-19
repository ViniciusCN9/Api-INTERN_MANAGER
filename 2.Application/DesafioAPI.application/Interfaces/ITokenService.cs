using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Account account);
    }
}