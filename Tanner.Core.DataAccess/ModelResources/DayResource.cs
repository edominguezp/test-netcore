using System;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class DayResource
    {
        public static (string, object) Query_DocumentDays(DateTime expiryDate, string product)
        {
            var query = $@"
               BEGIN
				DECLARE @MINIMO_DIAS_DOCUMENTO INT;SELECT @MINIMO_DIAS_DOCUMENTO = 1
				DECLARE @TIPO_RETENCION_DIAS_CANJE INT ;SELECT @TIPO_RETENCION_DIAS_CANJE = 3 
				DECLARE @TIPO_RETENCION_SIN_RETENCION INT ;SELECT @TIPO_RETENCION_SIN_RETENCION = 1  
				DECLARE @TIPO_RETENCION_DIAS_FIJOS INT ;SELECT @TIPO_RETENCION_DIAS_FIJOS = 2
				DECLARE @cod_tipo_doc_defecto CHAR(2); SELECT @cod_tipo_doc_defecto = 'FX'
				DECLARE @pzadocto INT; SELECT @pzadocto = 320
				DECLARE @pzaoper INT; SELECT @pzaoper = 320
				DECLARE @fechdisp DATETIME
				DECLARE @tipo_retencion INT; DECLARE @dias_retencion INT;DECLARE @tipo_dias_retencion INT;DECLARE @dias_canje INT; 
				DECLARE @PlazaOrigen varchar(100);DECLARE @PlazaDestino varchar(100);DECLARE @respuest varchar(2000);SELECT @respuest = 'OK'
				DECLARE @ret INT; DECLARE @dias INT; DECLARE @fechoper DATE; Select @fechoper = getdate()
				--
				If exists (   select 1 from dba.tb_fin61  with(nolock) where cod_tipo_doc = @cod_tipo_doc  )                                                            
						Begin                                                            
							select         
							  @tipo_retencion = tipo_retencion
							,@dias_retencion = dias_retencion
							,@tipo_dias_retencion = tipo_dias_retencion
							from dba.tb_fin61  with(nolock)  
							where cod_tipo_doc = @cod_tipo_doc                                                     
						End                                                            
				Else                                                            
						Begin
							select         
							  @tipo_retencion = tipo_retencion
							,@dias_retencion = dias_retencion
							,@tipo_dias_retencion = tipo_dias_retencion
							from dba.tb_fin61  with(nolock)  
							where cod_tipo_doc = @cod_tipo_doc_defecto   
						End       
				--
				select @tipo_retencion = isnull( @tipo_retencion , @TIPO_RETENCION_DIAS_CANJE ) --por dias canje 

				/* calculo de fecha de disponibilidad segun tipo de retencion del tipo de documento */        
				if @tipo_retencion = @TIPO_RETENCION_DIAS_CANJE --por dias de canje        
					BEGIN    
						/* se calculan los dias de canje */        
						exec @ret = dba.pr_fin0034 @pzaoper, @pzadocto, @dias_canje output
					
						select @dias_canje = @dias_canje + @dias_retencion        
				        
								if @dias_canje  is null 
									begin 
										select @PlazaOrigen = convert(varchar(100),descripcion_plaza) from dba.tb_fin27 with (nolock) where  codigo_plaza = @pzaoper
										select @PlazaDestino = convert(varchar(100),descripcion_plaza) from dba.tb_fin27 with (nolock) where codigo_plaza = @pzadocto
										set  @respuest = 'Dias de retencion no definidos entre plaza: ' + @PlazaOrigen + '( '+ convert(varchar(10),@pzaoper)+' )' + ' y ' + @PlazaDestino +' ( '+ convert(varchar(10),@pzadocto)+' )';
										print @respuest				 
									end          
				        
						/* se calcula fecha de disponibilidad */    
						exec @ret = dba.pr_fin0078 @fechvenc, @dias_canje, @fechdisp output        
					END
				else        
					BEGIN        
						/* si no es por canje, entonces es fijo o sin retencion */        
						if @tipo_retencion = @TIPO_RETENCION_SIN_RETENCION --sin retencion        
							begin        
								select @fechdisp = @fechvenc        
							end        
						if @tipo_retencion = @TIPO_RETENCION_DIAS_FIJOS --dias fijos        
							begin        
								if @tipo_dias_retencion = 1 --dias calendario        
									select @fechdisp = dateadd( dd , @dias_retencion , @fechvenc )        
									if @tipo_dias_retencion = 2 --dias habiles        
										begin
											exec @ret = dba.pr_fin0078 @fechvenc, @dias_retencion, @fechdisp output        
										end
							end        
					END    

				if @respuest ='OK'
					begin			
						if datediff( dd , @fechoper , @fechdisp) < @MINIMO_DIAS_DOCUMENTO         
											select @fechdisp = dateadd( dd , @MINIMO_DIAS_DOCUMENTO , @fechoper ) 
						select @dias = datediff( dd, @fechoper, @fechdisp)  
						--salida ok
						select @dias  
					end
				else
					begin
						--salida con problema
						select @respuest  
					end 
				END
                        ";
            var param = new
            {
				fechvenc = expiryDate,
				cod_tipo_doc = product
			};
            (string, object) result = (query, param);
            return result;
        }
    }
}
