namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent File by operation
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa el archivo por operación
    /// </summary>
    public class FileResource
    {
        /// <summary>
        /// File ID 
        /// </summary>
        /// <summary xml:lang="es">
        /// Identificador del fichero
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Operation number 
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de operación
        /// </summary>
        public long OperationNumber { get; set; }

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

        public static (string, object) Query_GetFileByID(decimal number, int id)
        {
            var query = $@"   
                SELECT
	                  AR.id_archivo as ID,
	                  OPA.numero_operacion AS OperationNumber,
                      ltrim(rtrim(AR.nm_file)) as Name,
	                  ltrim(rtrim(AR.filesUrl)) as URL,
	                  ltrim(rtrim(AR.fileDirectory)) as Directory
                  FROM 
	                  [Core2_Archivos].[dbo].[ARCHIVO] AR WITH(NOLOCK)
                  INNER JOIN dbo.OPER_ARCHIVO OPA  WITH(NOLOCK)
                  ON 
	                  AR.id_archivo = OPA.id_archivo
                  WHERE 
                      AR.id_archivo =  @{nameof(id)} AND OPA.numero_operacion = @{nameof(number)}
            ";
            var parameters = new
            {
                id,
                number
            };
            (string, object) result = (query, parameters);
            return result;
        }
    }
}
