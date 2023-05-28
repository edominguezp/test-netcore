
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.RelationalDataAccess;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Repositories
{
   public class CoreRepository : TannerRepository<CoreContext>, ICoreRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public CoreRepository(CoreContext ctx) : base(ctx)
        {
        }

        /// <inheritdoc cref="IOperationRepository.AnyAsync(Expression{Func{Operation, bool}})"/>
        public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> where) where T : class, IEntity
        {
            bool result = await Where<T>(where).AnyAsync();
            return result;
        }
    }
}
