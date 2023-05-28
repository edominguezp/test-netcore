using System.Diagnostics.CodeAnalysis;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;
namespace Tanner.Core.DataAccess.Commands
{
    /// <summary>
    /// Class that represent the attributes of address
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa los atributos de la dirección
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AddAddressCommand : TannerCommand<OperationResult<AddressResource>>
    {
        /// <summary>
        /// Client RUT
        /// </summary>
        ///<summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        public string RUT { get; set; }
        /// <summary>
        /// Commune address
        /// </summary>
        ///<summary xml:lang="es">
        /// Comuna de la dirección
        /// </summary>
        public int CommuneID { get; set; }

        /// <summary>
        /// Address of client
        /// </summary>
        ///<summary xml:lang="es">
        /// Dirección del cliente
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Number of address
        /// </summary>
        ///<summary xml:lang="es">
        /// Número de la dirección
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Client Phone
        /// </summary>
        ///<summary xml:lang="es">
        /// Teléfono del cliente
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Country code
        /// </summary>
        ///<summary xml:lang="es">
        /// Código país
        /// </summary>
        public Country Country { get; set; }
    }
}
