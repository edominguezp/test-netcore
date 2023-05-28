using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    public interface IRegionRepository
    {
        /// <summary>
        /// Get Regions
        /// </summary>
        /// <summary lang="es">
        /// Obtener las regiones
        /// </summary>
        /// <returns> Regions </returns>
        Task<OperationCollectionResult<RegionResource>> GetRegionsAsync();

        /// <summary>
        /// Get communes by region
        /// </summary>
        /// <param name="code">region code</param>
        /// <summary lang="es">
        /// Obtener las comunas por región
        /// </summary>
        /// <returns> Communes by region id</returns>
        Task<OperationCollectionResult<CommuneResource>> GetCommunesByRegionAsync(int idRegion);
    }
}
