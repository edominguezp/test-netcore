using Tanner.Core.DataAccess.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent detail of client
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa el detalle del cliente
    /// </summary>
    public class ClientDetailResource
    {
        /// <summary>
        /// Client RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        public string RUT { get; set; }

        /// <summary>
        /// Client Name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del cliente
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Validity status
        /// </summary>
        /// <summary xml:lang="es">
        /// Glosa estado vigencia
        /// </summary>
        public string ValidityStatus { get; set; }

        /// <summary>
        /// Debtor lock
        /// </summary>
        /// <summary xml:lang="es">
        /// Bloqueo de deudor
        /// </summary>
        public string DebtorLock { get; set; }

        /// <summary>
        /// Commercial lock
        /// </summary>
        /// <summary xml:lang="es">
        /// Bloqueo comercial
        /// </summary>
        public string CommercialLock { get; set; }

        /// <summary>
        /// Line business
        /// </summary>
        /// <summary xml:lang="es">
        /// Rubro
        /// </summary>
        public string LineBusiness { get; set; }

        /// <summary>
        /// Name zone
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre de la zona
        /// </summary>
        public string Zone { get; set; }

        /// <summary>
        /// Name branch office
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre de la sucursal
        /// </summary>
        public string BranchOffice { get; set; }

        /// <summary>
        /// Executive name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del ejecutivo
        /// </summary>
        public string Executive { get; set; }

        /// <summary>
        /// Sales section
        /// </summary>
        /// <summary xml:lang="es">
        /// Tramo de ventas
        /// </summary>
        public string SalesSection { get; set; }

        public static (string, object) Query_ClientDetailByRUT(string rut)
        {
            var query = $@"
                SELECT 
		                RUT_CLIENTE AS RUT,
		                CLIENTE AS Name,
		                glosa_estado_vigencia AS ValidityStatus,
		                BLOQUEO_DEUDOR AS DebtorLock,
		                BLQ_COMERCIAL AS CommercialLock,
		                Rubro AS LineBusiness,
		                zona AS Zone,
		                sucursal AS BranchOffice,
		                ejecutivo AS Executive,
		                TramoVentas AS SalesSection
                FROM 
		                INE_MODELOS.Perfil_Cliente
                WHERE 
		                RUT_CLIENTE = @{nameof(rut)}";
            var param = new
            {
                rut = rut.FillRUT(false)
            };
            (string, object) result = (query, param);
            return result;
        }
    }
}