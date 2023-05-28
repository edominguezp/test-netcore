using Tanner.Core.DataAccess.Models;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class LogResource
    {
        public static (string, object) Query_AddLog(LogRequest logRequest)
        {
            var query = $@"
				BEGIN TRY
					BEGIN TRANSACTION;
					declare @lv_direccion_fisica varchar(20)  
						, @lv_hostname varchar(20)  
						, @ln_proridad int =2

					If Not Exists( Select 1 from dba.tb_fin17 With (Nolock) Where numero_operacion  = @ln_num_oper  )                                             
						Begin;
						 THROW 100001, 'Operación no existe.',1 
						End   

					If Not Exists( Select 1 from dbo.catalogo_accion With (Nolock) Where cod_accion  = @ln_cod_accion  )                                             
						Begin;
						 THROW 100002, 'Código de acción no existe.',1
						End

					-- registramos direccion fisica de quien registra en log de devaluacion  
					select @lv_hostname = hostname from master..sysprocesses where spid = @@spid     
  
					-- registramos direccion fisica de quien registra en log de devaluacion  
					select @lv_direccion_fisica =  net_address from master..sysprocesses where spid = @@spid     
    
					insert into dbo.log_evaluacion (     
						  num_oper, login, cod_accion,  proridad, des_accion,  direccion_fisica,hostname )    
					values ( @ln_num_oper, @lv_login, @ln_cod_accion,  @ln_proridad, @lv_des_accion, @lv_direccion_fisica,@lv_hostname)    
    
					COMMIT TRANSACTION;
				END TRY
				BEGIN CATCH
					ROLLBACK TRANSACTION;
					THROW;
				END CATCH";
            
			var param = new
            {
				ln_num_oper = logRequest.OperationNumber,
				ln_cod_accion = logRequest.ActionCode,
				lv_des_accion = logRequest.ActionDescription,
				lv_login = logRequest.Login
			};

            (string, object) result = (query, param);

            return result;
        }
    }
}
