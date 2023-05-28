using Tanner.Core.DataAccess.Models;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class DocumentGrantResource
    {
        public static (string, object) Query_UpdateDocumentGrant(DocumentGrantRequest documentGrant)
        {
            var query = $@"BEGIN TRY
                BEGIN TRANSACTION;
                
                DECLARE @ln_codigo_deudor	INT
                DECLARE @ll_id_documento	NUMERIC (8,0)
                DECLARE @RUT				VARCHAR(50)
                DECLARE @respuesta			VARCHAR(255);

                SET @RUT = right('00000000000' + replace(@ls_rut_deudor,'-',''),10)

                --Consultamos código deudor
                SELECT @ln_codigo_deudor = isnull(d.codigo_tercero, 0)
                    FROM  dba.tb_fin08 d WITH (NOLOCK)
		                INNER JOIN dba.tb_fin41 p WITH (NOLOCK) ON d.codigo_persona = p.codigo_persona 
	                WHERE p.rut_persona = @RUT

                --Validamos que el codigo de deudor exista en la base de datos
                if @ln_codigo_deudor > 0 
                    BEGIN
                        --Rescatamos el ultimo numero de operacion del cliente
                        SELECT @ll_id_documento = doc.id_documento 
		                FROM dba.tb_fin24 doc WITH (NOLOCK)
		                WHERE doc.numero_operacion = @ll_numero_operacion
		                AND doc.numero_documento = @ll_num_documento
		                AND doc.codigo_tercero   = @ln_codigo_deudor

                        SELECT @ll_id_documento = isnull(@ll_id_documento, 0)
                        ----------------------------------------------------------------------------

                        if @ll_id_documento > 0
			                BEGIN
				                UPDATE dba.tb_fin24
					                SET fecha_cesion_sii = isnull(@ld_fec_ces,fecha_cesion_sii) ,
						                docto_cedido = isnull(@ll_DoctoCedido,docto_cedido),
						                resultado_docto_cedido = isnull(@lv_resultado_docto_cedido, resultado_docto_cedido)
					                WHERE id_documento = @ll_id_documento
					                COMMIT TRANSACTION;
					                SET @respuesta = 'Documento modificado.'
   			                END
                        ELSE
		                 BEGIN
			                SET @respuesta = 'Documento no encontrado.'
		                 END
                    END
                ELSE
	                BEGIN
		                SET @respuesta = 'Deudor no encontrado.'
	                END
                 --select @respuesta --solo para ver el resultado
                END TRY

                BEGIN CATCH
	                ROLLBACK TRANSACTION;
                END CATCH";

            var param = new
            {
                ll_numero_operacion = documentGrant.OperationNumber,
                ll_num_documento = documentGrant.DocumentNumber,
                ls_rut_deudor = documentGrant.DebtorRUT,
                ll_DoctoCedido = documentGrant.GrantStatus,
                ld_fec_ces = documentGrant.GrantDate,
                lv_resultado_docto_cedido = documentGrant.GrantResult
            };

            (string, object) result = (query, param);

            return result;
        }
    }
}
