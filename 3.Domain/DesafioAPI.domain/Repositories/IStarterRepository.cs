using System.Collections.Generic;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.domain.Repositories
{
    public interface IStarterRepository
    {
        List<Starter> GetStarters();

        Starter GetByIdStarter(int Id);

        List<Starter> GetByNameStarters(string name);

        Starter GetLastStarter();

        void PostStarter(Starter starter);

        void UpdateStarter(Starter starter);

        void DeleteStarter(Starter starter);
    }
}