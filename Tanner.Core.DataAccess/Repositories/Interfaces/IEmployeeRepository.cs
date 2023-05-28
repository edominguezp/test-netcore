using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    public interface IEmployeeRepository: ICoreRepository
    {
        /// <summary>
        /// If exist a employee with the passted email
        /// </summary>
        /// <summary lang="eng">
        /// Si existe un empleado con el correo electrónico pasado
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>True if it exist or false if not</returns>
        Task<bool> ExistEmployeeAsync(string email);

        /// <summary>
        /// Get Data by employee asociated to email
        /// </summary>
        /// <summary lang="es">
        /// Obtener los datos de un empleado asociado a un correo electrónico
        /// </summary>
        /// <param name="email">Client RUT</param>
        /// <returns>Data employee</returns>
        Task<OperationResult<EmployeeResource>> GetDataEmployeeByEmailAsync(string email);
    }
}
