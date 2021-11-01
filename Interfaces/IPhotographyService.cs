using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoilerPlate.Models;

namespace BoilerPlate.Interfaces
{
    public interface IPhotographyService
    {
        Task<List<Photographer>> GetPhotographers();
        Task<Photographer> GetSpecificPhotographers(int id);
        Task<List<Photographer>> GetPhotoGrapherByEventType(string eventType);
    }
}
