using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoilerPlate.Configuration;
using BoilerPlate.Interfaces;
using Microsoft.Extensions.Options;

namespace BoilerPlate.Services
{
    public class HelperService : IHelperService
    {
        private readonly AppSettings _appSettings;

        public HelperService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        private string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public bool ValidateHeader(string encodedHeader)
        {
            var decoded = Base64Decode(encodedHeader.Replace("Basic ", ""));
            return decoded.Equals(_appSettings.Secret);

        }
    }
}
