using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Interfaces
{
    public interface IHelperService
    {
        bool ValidateHeader(string encodedHeader);
    }
}
