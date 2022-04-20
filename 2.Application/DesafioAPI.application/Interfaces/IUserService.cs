using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.application.Interfaces
{
    public interface IUserService
    {
        List<Starter> VerifyStartersIsActive(List<Starter> starters);

        object HideStarterInformations(Starter starter);
    }
}