﻿using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface to channels
    /// </summary>
    /// <summary xml:lang="es">
    /// Interfaz del repositorio para los canales
    /// </summary>
    public interface IChannelRepository : ICoreRepository
    {
        /// <summary>
        /// Get all channel
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener todos los canales (origen de una operación)
        /// </summary>
        /// <returns>All data of channels</returns>
        Task<OperationCollectionResult<ChannelResource>> GetChannelAsync();
    }
    
}
