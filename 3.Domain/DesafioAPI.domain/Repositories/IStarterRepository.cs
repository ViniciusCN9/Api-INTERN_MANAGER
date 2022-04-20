using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.domain.Repositories
{
    public interface IStarterRepository
    {
        List<Starter> GetStarters();

        Starter GetByIdStarter(int Id);

        Starter GetByNameStarter(string name);

        Starter GetLastStarter();

        void PostStarter(Starter starter);

        void UpdateStarter(Starter starter);

        void DeleteStarter(Starter starter);
    }
}