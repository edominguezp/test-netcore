using System.Collections.Generic;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    public interface IZoneRepository : ICoreRepository
    {
        /// <summary>
        /// Get branch office by operation state, zone and days
        /// </summary>
        /// <param name="statesOperation">operation state</param>
        /// <param name="daysOperations">operation days</param>
        /// <param name="id">Zone id</param>
        /// <summary lang="es">
        /// Obtener las sucursales por estado de operación, zona y días
        /// </summary>
        /// <returns> BranchOffice by operation state, zone and days</returns>
        Task<OperationCollectionResult<BranchOfficeResource>> GetBranchOfficeByZoneAndState(IEnumerable<OperationState> statesOperation, int daysOperations, int id);

        /// <summary>
        /// Get zones by operation state and days
        /// </summary>
        /// <param name="statesOperation">operation state</param>
        /// <param name="daysOperations">operation days</param>
        /// <summary lang="es">
        /// Obtener las zonas por estado de operación y días
        /// </summary>
        /// <returns> Zones by operation state and days</returns>
        Task<OperationCollectionResult<ZoneResource>> GetZonesByState(ZoneByOperationStatus request);

        /// <summary>
        /// Get zones
        /// </summary>
        /// <summary lang="es">
        /// Obtener las zonas
        /// </summary>
        /// <returns> Zone</returns>
        Task<OperationCollectionResult<ZoneResource>> GetZonesAsync();
    }
    
}
