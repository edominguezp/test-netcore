
namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the product types
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa los tipos de producto
    /// </summary>
    public class ProductTypeResource
    {
        /// <summary>
        /// Product type code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código del tipo de producto
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Product type description
        /// </summary>
        /// <summary xml:lang="es">
        /// Descripción del tipo de producto
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Business line code
        /// </summary>
        /// <summary xml:lang="es">
        /// Línea de negocio
        /// </summary>
        public string BusinessLineCode { get; set; }

        /// <summary>
        /// Query to obtain product types
        /// </summary>
        /// <summary xml:lang="es">
        /// Consulta para obtener los tipos de operaciones
        /// </summary>
        internal static string Query_GetProductTypeResource()
        {
            var result = $@"                
                SELECT 
	                t.cod_tipo_doc AS Code,
	                LTRIM(RTRIM(x.descripcion_producto)) AS Description,
                    t.codigo_linea AS BusinessLineCode
                FROM 
	                dba.tb_fin80 p WITH(NOLOCK)
                INNER JOIN 
	                dba.tb_fin61 t WITH(NOLOCK) ON (p.tipo_documento = t.tipo_documento)
                LEFT OUTER JOIN 
	                dba.tb_fin74 x WITH(NOLOCK) ON (p.codigo_producto = x.codigo_producto)
                WHERE t.estado = 1
            ";
            return result;
        }
    }
}
