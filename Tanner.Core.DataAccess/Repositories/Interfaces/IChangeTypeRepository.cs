using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    public interface IChangeTypeRepository : ICoreRepository
    {
        /// <summary>
        /// Obtains the exchange rate and the default rate of a given date depending on the type of currency
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtiene el tipo de cambio y la tasa mora de una fecha determinada dependiendo del tipo de moneda
        /// </summary>
        /// <param name="changeType"></param>
        /// <param name="dateToConsult"></param>
        /// <returns></returns>
        Task<OperationResult<ChangeTypeAndDefaultRateResource>> GetChangeTypeAndDefaultRateAsync(ChangeTypeEnum changeType, DateTime dateToConsult);
    }
}
