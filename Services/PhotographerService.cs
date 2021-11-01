using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoilerPlate.Interfaces;
using BoilerPlate.Models;
using Newtonsoft.Json;
using System.IO;
using BoilerPlate.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BoilerPlate.Services
{
    public class PhotographerService : IPhotographyService
    {

        private string JsonFilePath = @"C:\Users\Nitin Rangarajan\source\repos\Personal\BoilerPlate\DataSource\photographers.json";

        private readonly ILogger<PhotographerService> _logger;
        private readonly AppSettings _appSettings;

        public PhotographerService(IOptions<AppSettings> appSettings,
            ILogger<PhotographerService> log)
        {
            _appSettings = appSettings.Value;
            _logger = log;
        }

        private async Task<List<Photographer>> LoadJson()
        {
            var result = new List<Photographer>();
            StreamReader data = null;
            using (data = new StreamReader(JsonFilePath))
            {
                var jsonParse = await data.ReadToEndAsync();
                result = JsonConvert.DeserializeObject<List<Photographer>>(jsonParse);
            }

            return result;
        }
        public async Task<List<Photographer>> GetPhotoGrapherByEventType(string eventType)
        {
            try
            {
                var photoList = await LoadJson();
                var result = new List<Photographer>();

                if (eventType != "birthdays" && eventType != "pet")
                {
                    throw new Exception("Only birthdays and pets covered for now");
                }

                var listVal = photoList.Where(s => s.Event_Type.Type.Contains(eventType));

                foreach(var val in listVal)
                {
                    result.Add(val);
                }

                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error in retrieving photographer by EventType. Error : \n", ex.Message);
            }

            return null;
        }

        public async Task<List<Photographer>> GetPhotographers()
        {
            try
            {
                return await LoadJson();
            }
            catch(Exception ex)
            {
                _logger.LogError("Error in retrieving all photographers. Error : \n", ex.Message);                
            }

            return null;
        }

        public async Task<Photographer> GetSpecificPhotographers(int id)
        {            
            try
            {
                var photoList = await LoadJson();

                var specific = photoList.Where(s => s.Id == id);

                return specific.First();

            }
            catch(Exception ex)
            {
                _logger.LogError("Error in retrieving photographer by Id. Error : \n", ex.Message);
            }

            return null;
        }
    }
}
