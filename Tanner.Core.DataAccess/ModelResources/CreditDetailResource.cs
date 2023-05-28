using System;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class CreditDetailResource
    {
        /// <summary>
        /// Operation number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de operación
        /// </summary>
        public long OperationNumber { get; set; }

        /// <summary>
        /// Is credit
        /// </summary>
        /// <summary xml:lang="es">
        /// es credito
        /// </summary>
        public bool IsCredit { get; set; }

        /// <summary>
        /// Future value
        /// </summary>
        /// <summary xml:lang="es">
        /// Valor futuro
        /// </summary>
        public decimal FutureValue { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        /// <summary xml:lang="es">
        /// Moneda
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Operation date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de operación
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Expiry date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de vencimiento
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Expiry date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de vencimiento
        /// </summary>
        public int Term { get; set; }

        /// <summary>
        /// Exempts TMC control
        /// </summary>
        /// <summary xml:lang="es">
        /// Exime control TMC
        /// </summary>
        public bool ExemptsTMCControl { get; set; }

        public static (string, object) Query_GetCreditDetail(long operationNumber)
        {
            var query = $@"
                BEGIN
                    declare @EsCredito CHAR(10);declare @exime_control_tmc CHAR(10);declare @nombre_moneda char(100)
                    declare @ln_num_cuotas int;declare @tipo_documento int;declare @ln_plazo int
                    declare @fec_ultimo_vcto datetime;declare @fecha_operacion Datetime
                    declare @valor_futuro_operacion numeric (16,4)     
    
                    Select @tipo_documento	=	o.tipo_documento
		                    , @fecha_operacion	=	o.fecha_operacion  
		                    , @ln_num_cuotas	=	( Select count(1) FROM dba.tb_fin24 d with(nolock) WHERE d.numero_operacion = o.numero_operacion and d.estado_documento < 5 )   
		                    , @nombre_moneda	=	m.cod_ext_mon
		                    , @valor_futuro_operacion	=	o.valor_futuro_operacion                                     
                        From dba.tb_fin17 o With (Nolock)                           
	                    inner join dba.tb_fin45 m With (Nolock) on m.codigo_moneda = o.codigo_moneda                           
                        Where o.numero_operacion = @numero_operacion                          

                    Select @EsCredito = case 
						                    when otro_tratamiento = 'V' then 'True' 
						                    when otro_tratamiento = 'C' then 'True' 
						                    else 'False'
                                        end
	                      ,@exime_control_tmc = case 
						                    when isnull(exime_control_tmc,0) = 0 then 'False' 
						                    else 'True'
                                        end

                    from dba.tb_fin61 With (Nolock) 
                    where tipo_documento = @tipo_documento

                    if @EsCredito = 'True' and isnull(@ln_num_cuotas,0) > 0 
                        begin
                            Select @fec_ultimo_vcto = max(fecha_vencimiento_documento)
                            from dba.tb_fin24 (nolock)
                            where numero_operacion = @numero_operacion
                              and estado_documento < 5
      
                            set @ln_plazo = datediff(dd,@fecha_operacion,@fec_ultimo_vcto)           
                        end 
                    if @EsCredito = 'False' 
	                    begin
		                    select @numero_operacion as OperationNumber
				                    , @EsCredito as IsCredit
				                    , null as FutureValue
				                    , null as Currency
				                    , null as OperationDate
				                    , null as ExpiryDate
				                    , null as Term
				                    , null as ExemptsTMCControl
	                    end 
                    else
	                    begin
		                    select @numero_operacion as OperationNumber
				                    , @EsCredito as IsCredit
				                    , @valor_futuro_operacion as FutureValue
				                    , @nombre_moneda as Currency
				                    , @fecha_operacion as OperationDate
				                    , @fec_ultimo_vcto as ExpiryDate
				                    , @ln_plazo as Term
				                    , @exime_control_tmc as ExemptsTMCControl
	                    end
                    END
            ";

            var param = new
            {
                numero_operacion = operationNumber
            };

            (string, object) result = (query, param);
            
            return result;
        }
    }
}
