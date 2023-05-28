using System.ComponentModel.DataAnnotations;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Commands
{
    /// <summary>
    /// Class that represent the attributes of file
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa los atributos del archivo
    /// </summary>
    public class AddFileCommand : TannerCommand<OperationResult<FileResource>>
    {
        /// <summary>
        /// Line Id
        /// </summary>
        ///<summary xml:lang="es">
        /// Id de la linea
        /// </summary>
        [Required]
        public int LineId { get; set; }

        /// <summary>
        /// File name
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre del archivo
        /// </summary>
        [Required]
        public string FileName { get; set; }

        /// <summary>
        /// File type
        /// </summary>
        ///<summary xml:lang="es">
        /// Tipo de archivo
        /// </summary>
        [Required]
        public int FileType { get; set; }

        /// <summary>
        /// URL storage
        /// </summary>
        ///<summary xml:lang="es">
        /// URL del repositorio
        /// </summary>
        [Required]
        public string URL { get; set; }

        /// <summary>
        /// File directory
        /// </summary>
        ///<summary xml:lang="es">
        /// Directorio del archivo
        /// </summary>
        [Required]
        public string Directory { get; set; }
    }
}
