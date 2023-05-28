using System;
using System.Collections.Generic;
using System.Text;
using Tanner.Core.DataAccess.Models;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class DocumentCreateResource
    {
        /// <summary>
        /// Param to create document
        /// </summary>
        /// <summary xml:lang="es">
        /// Parámetros para crear un documento
        /// </summary>
        public static object Query_CreateDocument(CreateDocumentRequest request)
        {
            var param = new
            {
                iddoc = request.DocumentId,
                numero_documento = request.DocumentNumber,
                valor_nominal_documento = request.DocumentValue,
                fecha_vencimiento_documento = request.ExpirationDate,
                valor_futuro_documento = request.FutureValue,
                valor_presente_documento = request.PresentValue,
                interes_documento = request.DocumentInterest,
                numero_operacion = request.OperationNumber,
                codigo_tercero = request.ThirdCode,
                codigo_plaza = request.PlaceCode,
                codigo_banco = request.BankCode,
                codigo_cliente = request.ClientCode,
                estado_documento = request.DocumentState,
                ctacte = request.CurrentAccount,
                av_glosa = request.Gloss,
                av_login = request.User,
                ad_fecha_emision = request.IssueDate,
                av_es_simulacion = request.IsSimulation, 
                fecha_vencimiento_cheque = request.CheckDueDate,
                tipo_dcto_sii = request.DocumentTypeSii
            };

            return param;
        }
    }
}
