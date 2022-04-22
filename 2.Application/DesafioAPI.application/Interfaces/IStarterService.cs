using System.Collections.Generic;
using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.application.Interfaces
{
    public interface IStarterService
    {
        List<Starter> GetStarters();

        Starter GetByIdStarter(int id);

        List<Starter> GetByNameStarters(string name);

        Starter PostStarter(StarterCreateDto starterDto);

        void UploadPhotoByIdStarter(Starter starter, string path);

        void PatchByIdStarter(StarterUpdateDto starterDto, int id);

        void PutByIdStarter(StarterUpdateDto starterDto, int id);

        void DeleteByIdStarter(int id);
    }
}