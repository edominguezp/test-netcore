using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    public interface ICoreRepository : ITannerRepository
    {
        /// <summary>
        /// Get true or false if existe operation
        /// </summary>
        /// <summary lang="es">
        /// obtiene verdadero o falso si existe la operación
        /// </summary>
        /// <returns>true or false if existe operation</returns>
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> where) where T : class, IEntity;
    }
}
