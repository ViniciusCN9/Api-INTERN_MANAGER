using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DesafioAPI.api.Helpers
{
    public class FilesHelper
    {
        private readonly IWebHostEnvironment _environment;

        public FilesHelper(IWebHostEnvironment environment)
        {
           _environment = environment; 
        }

        public string SaveImageOnRoot(IFormFile photo, string abbreviation)
        {
            try
            {
                string directoryPath = Path.Combine(_environment.WebRootPath, "Assets/Images");
                string photoUniqueName;
                string photoExtension = photo.FileName.Split('.').Last();

                photoUniqueName = Guid.NewGuid().ToString().Substring(0,9) + abbreviation + "." + photoExtension;
                string photoPath = Path.Combine(directoryPath, photoUniqueName);

                using (var fileStream = new FileStream(photoPath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }

                return photoUniqueName;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public byte[] ShowImageFromRoot(Starter starter)
        {
            string directoryPath = Path.Combine(_environment.WebRootPath, "Assets/Images");
            string photoPath = Path.Combine(directoryPath, starter.Photo);
            if (!System.IO.File.Exists(photoPath))
                throw new ArgumentException("Foto não encontrada");

            return System.IO.File.ReadAllBytes(photoPath);
        }

        public void DeleteImageOnRoot(string photo)
        {
            if (photo == "Default.jpg")
                return;

            try
            {
                string directoryPath = Path.Combine(_environment.WebRootPath, "Assets/Images");
                string photoPath = Path.Combine(directoryPath, photo);
                if (System.IO.File.Exists(photoPath))
                    System.IO.File.Delete(photoPath);
            }
            catch
            {
                throw new Exception("Falha ao deletar foto antiga");
            }
        }
    }
}