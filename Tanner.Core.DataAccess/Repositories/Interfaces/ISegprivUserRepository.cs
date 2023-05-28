using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    public interface ISegprivUserRepository
    {
        Task<IEnumerable<Models.Segpriv.User>> GetCommercialManagersAsync();
    }
}
