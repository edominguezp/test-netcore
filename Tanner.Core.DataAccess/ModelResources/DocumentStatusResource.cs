namespace Tanner.Core.DataAccess.ModelResources
{
    public class DocumentStatusResource
    {

        public static string Query_UpdateDocumentStatus(decimal documentId)
        {
            var query = $@"
                declare  @estado_confirmacion int
                declare @id_documento numeric (8,0) = {documentId}

                --1    EN PROCESO
                --2    POSITIVA
                --3    NEGATIVA
                --4    POSTERIOR
                --5    PROTOCOLO

                -- recupera estado del documento
                select @estado_confirmacion = isnull(estado_confirmacion,1)
                from dba.tb_fin77 c with (nolock)
                where c.id_documento = @id_documento

                --ej. salida
                select @estado_confirmacion
            ";

            return query;
        }
    }
}
