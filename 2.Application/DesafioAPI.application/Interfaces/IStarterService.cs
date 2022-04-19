using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.application.Interfaces
{
    public interface IStarterService
    {
        Task<List<Starter>> GetStarters();

        Task<Starter> GetByIdStarter(int id);

        Task<Starter> GetByNameStarter(string name);

        void PostStarter(StarterDto starterDto);

        void PatchByIdStarter(StarterDto starterDto, int id);

        void PutByIdStarter(StarterDto starterDto, int id);

        void DeleteByIdStarter(int id);
    }
}