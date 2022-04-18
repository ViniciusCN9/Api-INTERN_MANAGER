using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.domain.Repositories
{
    public interface IStarterRepository
    {
        Task<List<Starter>> GetStarters();

        Task<Starter> GetByIdStarter(int Id);

        Task<Starter> GetByNameStarter(string name);

        void PostStarter(Starter starter);

        void UpdateStarter();

        void DeleteByIdStarter(int id);
    }
}