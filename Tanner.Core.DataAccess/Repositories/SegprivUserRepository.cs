using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Constants;
using Tanner.Core.DataAccess.Enums.Segpriv;
using Tanner.Core.DataAccess.Repositories.Interfaces;

namespace Tanner.Core.DataAccess.Repositories
{
    public class SegprivUserRepository : ISegprivUserRepository
    {
        private readonly SegprivContext _context;
        private readonly ILogger<SegprivUserRepository> _logger;

        public SegprivUserRepository(SegprivContext context, ILogger<SegprivUserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Models.Segpriv.User>> GetCommercialManagersAsync()
        {
            IEnumerable<Models.Segpriv.User> commercialManagers = await _context.UserProfiles
                .Where(up => up.ProfileId == (int)Profile.CommercialManager)
                .Include(up => up.User)
                .Where(up => up.User.Absent == Segpriv.FalseValue && up.User.Blocked == Segpriv.FalseValue)
                .Select(up => up.User)
                .ToListAsync();

            IEnumerable<string> uniqueEmails = new HashSet<string>(commercialManagers.Select(cm => cm.Email));
            bool thereAreRepeatedEmails = commercialManagers.Count() > uniqueEmails.Count();
            if (thereAreRepeatedEmails)
            {
                _logger.LogWarning($"There are some commercial managers with dupplicated emails.  An email should be used only for one account in the Segpriv system.");
                IEnumerable<Models.Segpriv.User> uniqueComercialManagers = uniqueEmails.Select(email => commercialManagers.First(cm => cm.Email == email));
                return uniqueComercialManagers;
            }

            return commercialManagers;
        }
    }
}
