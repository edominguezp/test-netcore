namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the commune
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa la comuna
    /// </summary>
    public class CommuneResource
    {
        /// <summary>
        /// Code of commune
        /// </summary>
        /// <summary xml:lang="es">
        /// Código de la comuna
        /// </summary>
        public int IdCommune { get; set; }

        /// <summary>
        /// Name of the commune
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre de la comuna
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// id of region
        /// </summary>
        /// <summary xml:lang="es">
        /// identificador de la región
        /// </summary>
        public int RegionCode { get; set; }

        public static (string, object) Query_CommunesByRegion(int id)
        {
            var query = $@"
                SELECT  
	                tbl_comunas.codigo_comuna AS IdCommune,
	                ltrim(rtrim(tbl_comunas.nombre_comuna)) AS Name,
	                tbl_comunas.codigo_region AS RegionCode   
                FROM 
	                tbl_comunas 
                WHERE 
	                codigo_region = @{nameof(id)}";
            var param = new
            {
                id
            };
            (string, object) result = (query, param);
            return result;
        }
    }
}
