using System.Collections.Generic;

namespace BoilerPlate.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public class Poll
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Options> Options { get; set; }
        
    }

    public class Options
    {
        public int Id { get; set; }
        public string Option { get; set; }
    }
}
