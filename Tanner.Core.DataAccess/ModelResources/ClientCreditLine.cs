using System;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class ClientCreditLine
    {
        // <summary>
        /// Line ID
        /// </summary>
        /// <summary xml:lang="es">
        /// Linea ID
        /// </summary>
        public int LineID { get; set; }

        // <summary>
        /// Admission date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de ingreso
        /// </summary>
        public DateTime AdmissionDate { get; set; }

        // <summary>
        /// Expiry date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de vencimiento
        /// </summary>
        public string ExpiryDate { get; set; }

        // <summary>
        /// Business name
        /// </summary>
        /// <summary xml:lang="es">
        /// Razón social
        /// </summary>
        public string BusinessName { get; set; }

        // <summary>
        /// Amount approved
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto aprovado
        /// </summary>
        public decimal AmountApproved { get; set; }

        // <summary>
        /// Amount requested
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto solicitado
        /// </summary>
        public long AmountRequested { get; set; }

        // <summary>
        /// Description
        /// </summary>
        /// <summary xml:lang="es">
        /// Descripción
        /// </summary>
        public string Description { get; set; }

        public static (string, object) Query_ClientCreditLineByRUT(string rut)
        {
            var query = $@"BEGIN
             DECLARE @li_codigo_cliente INT
		            ,@li_rut_cli		INT
             --INICIO SETEO DE PARAMETROS 
       
             SELECT @li_rut_cli = CONVERT(INT, SUBSTRING(@rut_cli, 1,LEN(@rut_cli) - 2)) 
             --
             SELECT @li_codigo_cliente = cli.codigo_cliente
             FROM dba.tb_fin01 cli (NoLock)
             INNER JOIN (SELECT codigo_persona FROM dba.tb_fin41 (NoLock) WHERE	CONVERT(INT, SUBSTRING(rut_persona, 1,LEN(rut_persona) - 1)) = @li_rut_cli) per ON per.codigo_persona = cli.codigo_persona
            --
             SELECT linea.id_linea AS LineID,       
		            linea.fecha_ingreso_linea AS AdmissionDate,
		            linea.fecha_vencimiento_linea AS ExpiryDate,
		            per.razon_social AS BusinessName,	       
                    linea.monto_aprobado AS AmountApproved,       
                    linea.monto_solicitado AS AmountRequested,       
                    para.descripcion AS Description
              FROM dba.tb_fin01					      clientes    
              JOIN dba.tb_fin02						  linea ON linea.codigo_cliente				= clientes.codigo_cliente 
              JOIN dba.tb_fin41						  per WITH (NOLOCK) ON clientes.codigo_persona			= per.codigo_persona     
              JOIN dba.tb_fin06						  ejecutivos  ON clientes.codigo_empleado			= ejecutivos.codigo_empleado    
              JOIN DBA.TB_FIN50						  PARA ON linea.estado = PARA.codigo      
               AND linea.codigo_cliente				= @li_codigo_cliente     
               AND linea.estado						= 1 -- 0=SOLICITADA 1=OTORGADA 2=RECHAZADA 4=INACTIVA 5=ELIMINADA
               AND para.tipo						= 29--TIPO PARAMETRO TIPO LINEAS

            END ";

            var param = new
            {
                rut_cli = rut
            };

            (string, object) result = (query, param);

            return result;
        }

        public static (string, object) Query_CreditLineByID(int lineId)
        {
            var query = $@"select top 1 id_linea from core2.dba.tb_fin02 With ( Nolock ) where id_linea = @lineId ";

            var param = new
            {
                lineId
            };

            (string, object) result = (query, param);

            return result;
        }
    }
}
