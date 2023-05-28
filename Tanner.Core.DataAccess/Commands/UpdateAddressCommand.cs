using System.Diagnostics.CodeAnalysis;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Commands
{
    /// <summary>
    /// Class that represent the address update
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa la actualización de la dirección
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UpdateAddressCommand : TannerCommand<OperationBaseResult>
    {
        /// <summary>
        /// Address ID
        /// </summary>
        ///<summary xml:lang="es">
        /// Identificador de la dirección
        /// </summary>
        public decimal ID { get; set; }

        /// <summary>
        /// Person ID
        /// </summary>
        ///<summary xml:lang="es">
        /// ID de la persona
        /// </summary>
        public int PersonID { get; set; }

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
    }
}
