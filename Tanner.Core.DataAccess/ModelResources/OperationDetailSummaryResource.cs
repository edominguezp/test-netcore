namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent a Operation or Quotation
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa una operación o cotización
    /// </summary>
    public class OperationDetailSummaryResource
    {
        /// <summary>
        /// Client RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        public string RUT { get; set; }

        /// <summary>
        /// Zone code
        /// </summary>
        /// <summary xml:lang="es">
        /// código de la zona
        /// </summary>
        public int Zone { get; set; }

        /// <summary>
        /// Branch office code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código de la sucursal
        /// </summary>
        public int BranchOffice { get; set; }

        /// <summary>
        /// Operation or quotation channel
        /// </summary>
        /// <summary xml:lang="es">
        /// Canal de la operación o cotización
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// Document type
        /// </summary>
        /// <summary xml:lang="es">
        /// Tipo de documento
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// Operation type
        /// </summary>
        /// <summary xml:lang="es">
        /// tipo de operación
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// Operation type code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código del tipo de operación
        /// </summary>
        public string OperationTypeCode { get; set; }

        /// <summary>
        /// Operation or cuotation amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de la operación o cotización
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// External code of coin
        /// </summary>
        /// <summary xml:lang="es">
        /// Código externo de la moneda
        /// </summary>
        private string _coinExternalCode;
        public string CoinExternalCode
        {
            get
            {
                return _coinExternalCode?.Trim();
            }
            set
            {
                _coinExternalCode = value;
            }
        }

        /// <summary>
        /// Query that represent data of operation or quotation
        /// </summary>
        /// <summary xml:lang="es">
        /// Consulta que representa los datos de una operación o cotización
        /// </summary>
        public static (string, object) Query_GetOperationSummary(int number, bool isQuotation)
        {
            var query = @"
                IF @isQuotation = 0
	                BEGIN
		                SELECT              
			                dbo.f_formatea_rut_azure(PER.RUT_PERSONA) AS RUT,
			                ZON.codigo_zona AS ZONE,
			                ZON.codigo_sucursal BRANCHOFFICE,
			                LTRIM(RTRIM(CRE.origen)) AS CHANNEL,
			                LTRIM(RTRIM(TD.cod_tipo_doc)) AS DOCUMENTTYPE,
			                LTRIM(RTRIM(TP.codigo_tipo_operacion)) AS OPERATIONTYPECODE,
			                LTRIM(RTRIM(TP.descripcion_tipo_operacion)) AS OPERATIONTYPE,
			                OPE.valor_futuro_operacion AS AMOUNT,
							MON.cod_ext_mon AS COINEXTERNALCODE
		                FROM
			                DBA.TB_FIN17 OPE WITH(NOLOCK)   
			            LEFT JOIN 
							DBA.TB_FIN01 CLI  WITH(NOLOCK) ON OPE.CODIGO_CLIENTE = CLI.CODIGO_CLIENTE   
			            LEFT JOIN 
							DBA.TB_FIN41 PER WITH(NOLOCK) ON CLI.CODIGO_PERSONA = PER.CODIGO_PERSONA  
			            LEFT JOIN 
							dba.tb_fin44 ZON WITH(NOLOCK) ON OPE.codigo_sucursal = ZON.codigo_sucursal
			            LEFT JOIN 
							dbo.credito CRE WITH(NOLOCK) ON OPE.numero_operacion = CRE.num_op
			            LEFT JOIN 
							dbo.tipo_operacion TP WITH(NOLOCK) ON OPE.tipo_operacion = TP.codigo_tipo_operacion   
			            LEFT JOIN 
							dba.tb_fin61 TD WITH(NOLOCK) ON OPE.tipo_documento = TD.tipo_documento
						INNER JOIN 
							dba.tb_fin45 MON WITH(NOLOCK) ON MON.codigo_moneda = OPE.codigo_moneda
		                WHERE		 
			                OPE.numero_operacion = @number
	                END
                ELSE
	                BEGIN
		                SELECT              
			                dbo.f_formatea_rut_azure(PER.RUT_PERSONA) AS RUT,
			                ZON.codigo_zona AS ZONE,
			                ZON.codigo_sucursal BRANCHOFFICE,
			                LTRIM(RTRIM(CRE.origen)) AS CHANNEL,
			                LTRIM(RTRIM(TD.cod_tipo_doc)) AS DOCUMENTTYPE,
			                LTRIM(RTRIM(TP.codigo_tipo_operacion)) AS OPERATIONTYPECODE,
			                LTRIM(RTRIM(TP.descripcion_tipo_operacion)) AS OPERATIONTYPE,
			                OPE.valor_futuro_operacion AS AMOUNT,
							MON.cod_ext_mon AS COINEXTERNALCODE
		                FROM
			                DBA.tb_fin17_cotizador OPE WITH(NOLOCK)    
			            LEFT JOIN 
							DBA.TB_FIN01 CLI  WITH(NOLOCK) ON OPE.CODIGO_CLIENTE = CLI.CODIGO_CLIENTE   
			            LEFT JOIN 
							DBA.TB_FIN41 PER WITH(NOLOCK) ON CLI.CODIGO_PERSONA = PER.CODIGO_PERSONA  
			            LEFT JOIN 
							dba.tb_fin44 ZON WITH(NOLOCK) ON OPE.codigo_sucursal = ZON.codigo_sucursal
			            LEFT JOIN 
							dbo.credito_cotizador CRE WITH(NOLOCK) ON OPE.numero_operacion = CRE.num_op
			            LEFT JOIN 
							dbo.tipo_operacion TP WITH(NOLOCK) ON OPE.tipo_operacion = TP.codigo_tipo_operacion
			            LEFT JOIN 
							dba.tb_fin61 TD WITH(NOLOCK) ON OPE.tipo_documento = TD.tipo_documento
						INNER JOIN 
							dba.tb_fin45 MON WITH(NOLOCK) ON MON.codigo_moneda = OPE.codigo_moneda
		                WHERE		 
			                OPE.numero_operacion = @number
	                END
            ";

            var param = new
            {
                number,
                isQuotation
            };

            (string, object) result = (query, param);
            return result;
        }
    }
}
