using System;
using Tanner.Core.DataAccess.Enums;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// class that represents a operation
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa una operación
    /// </summary>
    public class OperationsStatesResource
    {
        /// <summary>
        /// Number of operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de operación
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Client name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del cliente
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Client RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        public string ClientRut { get; set; }

        /// <summary>
        /// Register date of operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha registro de la operación
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// Code of product
        /// </summary>
        /// <summary xml:lang="es">
        /// Código de producto
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// Name Product
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre Producto
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Amount of Operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de la operación
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Zone
        /// </summary>
        /// <summary xml:lang="es">
        /// Zona
        /// </summary>
        public string Zone { get; set; }

        /// <summary>
        /// BranchOffice
        /// </summary>
        /// <summary xml:lang="es">
        /// Sucursal
        /// </summary>
        public string BranchOffice { get; set; }

        /// <summary>
        /// Current Task
        /// </summary>
        /// <summary xml:lang="es">
        /// Tarea actual
        /// </summary>
        public string CurrentTask { get; set; }

        /// <summary>
        /// State of operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Estado de la operación
        /// </summary>
        public OperationState State { get; set; }

        /// <summary>
        /// method to obtain operations
        /// </summary>
        /// <summary xml:lang="es">
        /// método para obtener las operaciones
        /// </summary>
        public static (string, object) Query_OperationInAnalysis(OperationByStatus request)
        {
            var parameters = new
            {
                operation_number = request.OperationNumber,
                status = request.Status,
                zone_code = request.ZoneCode,
                branch_office_code = request.BranchOfficeCode,
                operation_days = request.Days,
                executive_approval = request.IsExecutiveApproval == null ?
                                    null : (request.IsExecutiveApproval == true ? "S" : "N")
            };

            var query = $@"
                DECLARE @ld_fec_consulta DATETIME
                DECLARE @ln_sucursal smallint
                DECLARE @ln_zona smallint

                SET @ld_fec_consulta = DATEADD(day, -@{nameof(parameters.operation_days)}, GETDATE())
                SET @ln_sucursal = ISNULL(@{nameof(parameters.branch_office_code)}, 0)
                SET @ln_zona = ISNULL(@{nameof(parameters.zone_code)}, 0)

                SELECT 
	                COUNT(*) 
                FROM
	                dba.tb_fin17 ope WITH (NOLOCK)
                INNER JOIN 
	                dba.tb_fin44 suc WITH (NOLOCK) ON ope.codigo_sucursal = suc.codigo_sucursal
                INNER JOIN 
	                dba.tb_zona zo WITH (NOLOCK) ON suc.codigo_zona = zo.codigo_zona 
                INNER JOIN 
	                dba.tb_fin01 cli WITH (NOLOCK) ON ope.codigo_cliente = cli.codigo_cliente
                INNER JOIN 
	                DBA.TB_FIN61 TDOC WITH (NOLOCK) ON (ope.tipo_documento  = TDOC.tipo_documento) 
                LEFT JOIN 
	                operaciones_en_analisis opan WITH (NOLOCK) ON (ope.numero_operacion = opan.numero_operacion)
                INNER JOIN 
	                dba.tb_fin41 per WITH (NOLOCK) ON (per.codigo_persona = cli.codigo_persona)
                LEFT JOIN (
	                SELECT 
		                tarea_actual,
		                num_op,
		                vb_ejecutivo 
	                FROM 
		                dbo.credito WITH (NOLOCK)
                ) CRED ON (OPE.numero_operacion = CRED.num_op)
                WHERE   
	                ope.estado_operacion IN @{nameof(parameters.status)} AND
                    (@{nameof(parameters.operation_number)} IS NULL OR OPE.numero_operacion = @{nameof(parameters.operation_number)}) AND
	                ope.fecha_operacion >= @ld_fec_consulta AND
	                (@ln_sucursal = 0 OR ope.codigo_sucursal = @ln_sucursal) AND
	                (@ln_zona = 0 OR suc.codigo_zona = @ln_zona) AND
	                (@{nameof(parameters.executive_approval)} IS NULL OR CRED.vb_ejecutivo = @{nameof(parameters.executive_approval)})


                SELECT
	                OPE.numero_operacion AS Number, 
	                LTRIM(RTRIM(cli.nombre_cliente)) AS ClientName,
	                per.rut_persona AS ClientRUT,
	                OPE.fecha_operacion AS RegisterDate,
	                TDOC.cod_tipo_doc AS ProductCode, 
	                LTRIM(RTRIM(TDOC.descripcion_documento)) AS ProductName,
	                OPE.valor_nominal_operacion AS Amount,
	                LTRIM(RTRIM(zo.nombre_zona)) AS Zone,
	                LTRIM(RTRIM(suc.descripcion_sucursal)) AS BranchOffice, 
	                opan.tarea_actual AS CurrentTask,
	                OPE.estado_operacion AS State
                FROM
	                dba.tb_fin17 ope WITH (NOLOCK)
                INNER JOIN 
	                dba.tb_fin44 suc WITH (NOLOCK) ON ope.codigo_sucursal = suc.codigo_sucursal
                INNER JOIN 
	                dba.tb_zona zo WITH (NOLOCK) ON suc.codigo_zona = zo.codigo_zona 
                INNER JOIN 
	                dba.tb_fin01 cli WITH (NOLOCK) ON ope.codigo_cliente = cli.codigo_cliente
                INNER JOIN 
	                DBA.TB_FIN61 TDOC WITH (NOLOCK) ON (ope.tipo_documento  = TDOC.tipo_documento) 
                LEFT JOIN 
	                operaciones_en_analisis opan WITH (NOLOCK) ON (ope.numero_operacion = opan.numero_operacion)
                INNER JOIN 
	                dba.tb_fin41 per WITH (NOLOCK) ON (per.codigo_persona = cli.codigo_persona)
                LEFT JOIN (
	                SELECT 
		                tarea_actual,
		                num_op,
		                vb_ejecutivo 
	                FROM 
		                dbo.credito WITH (NOLOCK)
                ) CRED ON (OPE.numero_operacion = CRED.num_op)
                WHERE   
	                ope.estado_operacion IN @{nameof(parameters.status)} AND
                    (@{nameof(parameters.operation_number)} IS NULL OR OPE.numero_operacion = @{nameof(parameters.operation_number)}) AND
	                ope.fecha_operacion >= @ld_fec_consulta AND
	                (@ln_sucursal = 0 OR ope.codigo_sucursal = @ln_sucursal) AND
	                (@ln_zona = 0 OR suc.codigo_zona = @ln_zona) AND
	                (@{nameof(parameters.executive_approval)} IS NULL OR CRED.vb_ejecutivo = @{nameof(parameters.executive_approval)})

            ";
            
            (string, object) result = (query, parameters);
            return result;
        }
    }
}
