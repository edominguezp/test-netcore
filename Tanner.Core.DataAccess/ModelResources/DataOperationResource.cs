using Tanner.Core.DataAccess.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the data of operation
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa datos de la operación
    /// </summary>
    public class DataOperationResource
    {
        /// <summary>
        ///  Operation Number
        /// </summary>
        /// <summary xml:lang="es">
        ///  Número de la operación
        /// </summary>
        public decimal OperationNumber { get; set; }

        /// <summary>
        /// Client RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        public string ClientRUT { get; set; }
        
        /// <summary>
        /// Document Number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de documento
        /// </summary>
        public decimal DocumentNumber { get; set; }

        /// <summary>
        /// Document Amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto del documento
        /// </summary>
        public string DocumentAmount { get; set; }

        /// <summary>
        /// Debtor RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del Deudor
        /// </summary>
        public string DebtorRUT { get; set; }

        public static (string, object) Query_GetOperationbyDocumentNumber(string rut, string number)
        {
            var rutNormalize = rut.FillRUT();
            var query = $@"
                SET NOCOUNT ON;

	            SELECT		
                    aux3.[numero_operacion] AS OperationNumber,
                    @{nameof(rut)} AS ClientRUT,
				    aux3.numero_documento DocumentNumber,
                    aux3.valor_nominal_documento as DocumentAmount,
                        (select rut_persona
                            from [dba].[tb_fin41]
                            where codigo_persona = (select codigo_persona
                                                        from [dba].[tb_fin08]
                                                        where codigo_tercero = aux3.codigo_tercero ) ) as DebtorRUT
	            FROM 
				    [dba].[tb_fin41] AS aux1
	                INNER JOIN [dba].[tb_fin01] AS aux2 ON aux1.codigo_persona = aux2.codigo_persona
	                INNER JOIN [dba].[tb_fin24] AS aux3 ON aux2.codigo_cliente = aux3.codigo_cliente
	                INNER JOIN [dba].[tb_fin17] AS aux4 ON aux3.numero_operacion = aux4.numero_operacion
	                INNER JOIN [dba].[tb_fin50] AS de   ON aux3.tipo_documento = de.codigo and de.tipo = 66
		        WHERE 
                    aux1.rut_persona = @{nameof(rutNormalize)} AND 
                    aux3.numero_documento = @{nameof(number)} AND
                    aux3.tipo_documento = 47 AND -- 47: (Tipo SII 33 o 34)
                    (aux3.estado_documento = 0 OR aux3.estado_documento = 1 OR aux3.estado_documento = 2) AND -- 0: ingresado, 1: vigente, 2:Pagado
                    aux3.tipo_documento in (de.codigo);
            ";
            var param = new
            {
                rut,
                rutNormalize,
                number
            };
            (string, object) result = (query, param);
            return result;
        }
    }
}
