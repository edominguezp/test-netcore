namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the regions of Chile
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa las regiones de Chile
    /// </summary>
    public class RegionResource
    {
        /// <summary>
        /// Region Code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código región
        /// </summary>
        public int IdRegion { get; set; }

        /// <summary>
        /// Region Name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre de región
        /// </summary>
        public string Name { get; set; }

        public static (string, object) Query_GetRegion()
        {
            var query = $@"
                    SELECT 
	                    region.codigo_region AS IdRegion, 
                        LTRIM(RTRIM(region.nombre_region)) AS Name
                    FROM 
	                    dbo.TBL_REGION region 
                    order by 
	                    region.codigo_region desc
                        ";
            var param = new
            {
            };
            (string, object) result = (query, param);
            return result;
        }
    }
}
