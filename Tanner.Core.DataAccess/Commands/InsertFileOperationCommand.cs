using System.Diagnostics.CodeAnalysis;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Commands
{
    /// <summary>
    /// Interface that represent File operation
    /// </summary>
    /// <summary xml:lang="es">
    /// Interfaz que representa el archivo de la operación
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class InsertFileOperationCommand : TannerCommand<OperationResult<FileResource>>
    {
        /// <summary>
        /// Operation number 
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de operación
        /// </summary>
        public int OperationNumber { get; set; }

        /// <summary>
        /// File name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del archivo
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File URL
        /// </summary>
        /// <summary xml:lang="es">
        /// URL del archivo
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// File directory
        /// </summary>
        /// <summary xml:lang="es">
        /// Directorio del archivo
        /// </summary>
        public string Directory { get; set; }
    }
}