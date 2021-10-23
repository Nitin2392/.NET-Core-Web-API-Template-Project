using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Classes
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassWord { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public List<string> PlacesToVisit { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
