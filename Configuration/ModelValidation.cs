using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoilerPlate.Models;

namespace BoilerPlate.Configuration
{
    public class ValidationResponse
    {
        public string Reason { get; set; }
        public bool ValidationResult { get; set; }
    }
    public static class ModelValidation
    {
        public static ValidationResponse ValidateNewUser(this User user)
        {
            var validationResponse = new ValidationResponse();
            validationResponse.ValidationResult = false;

            if (user == null)
            {
                validationResponse.Reason = "User object cannot be null";
                return validationResponse;
            }

            if(user.FirstName == null || user.FirstName.Length > 20
               || user.LastName == null || user.LastName.Length > 20
               || user.UserName == null || user.UserName.Length > 20)
            {
                validationResponse.Reason = "FirstName, LastName, UserName should not be null and less than 20 characters";
                return validationResponse;
            }

            if(user.Password == null || user.Password.Length < 4)
            {
                validationResponse.Reason = "Password cannot be null and should be greater than 4 characters";
                return validationResponse;
            }

            if (user.Gender == null)
            {
                validationResponse.Reason = "Gender cannot be null";
                return validationResponse;
            }

            return new ValidationResponse
            {
                ValidationResult = true
            };
        }
    }
}
