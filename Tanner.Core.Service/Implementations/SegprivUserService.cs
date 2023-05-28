using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Models.Segpriv;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.Core.Service.Dtos;
using Tanner.Core.Service.Interfaces;

namespace Tanner.Core.Service.Implementations
{

    public class SegprivUserService : ISegprivUserService
    {
        private readonly ISegprivUserRepository _pricingRepository;

        public SegprivUserService(ISegprivUserRepository pricingRepository)
        {
            this._pricingRepository = pricingRepository;
        }

        public async Task<OperationCollectionResult<CommercialManagerDto>> GetCommercialManagersAsync()
        {
            IEnumerable<User> entities = await _pricingRepository.GetCommercialManagersAsync();
            var result = new OperationCollectionResult<CommercialManagerDto>
            {
                DataCollection = entities.Select(MapEntityToCommercialManagerDto),
                Total = entities.Count()
            };
            return result;
        }

        

        private CommercialManagerDto MapEntityToCommercialManagerDto(User entity)
        {
            return new CommercialManagerDto
            {
                Name = entity.Name,
                Surname = entity.Surname,
                Email = entity.Email
            };
        }
    }
}
