using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    public interface IDocumentsDebtorRepository
    {
        /// <summary>
        /// Get documents debtors and send email
        /// </summary>
        /// <summary lang="es">
        /// Obtener los documentos por deudor y enviar correo
        /// </summary>
        /// <returns></returns>
        Task<OperationCollectionResult<DocumentDebtorResource>> GetDebtors();
    }
}
