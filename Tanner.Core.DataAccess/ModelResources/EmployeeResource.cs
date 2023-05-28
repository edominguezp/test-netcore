namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent data of Employee
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa loa datos del empleado
    /// </summary>
    public class EmployeeResource
    {
        /// <summary>
        /// Email employee
        /// </summary>
        ///<summary xml:lang="es">
        /// Correo del empleado
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Employee name
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre del empleado
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Position Employee
        /// </summary>
        ///<summary xml:lang="es">
        /// Cargo del empleado
        /// </summary>
        public string Position { get; set; }

        public static (string, object) Query_EmployeeData(string email)
        {
            var query = $@"
                 declare @tb_empleado TABLE (
				 employeeCode varchar(50) null
		        ,employeeName varchar(100) null	
		        ,email varchar(100) null			
		        ,branchCode int null
		        ,branchName varchar(100) null		
		        ,agentName varchar(100) null	
		        ,userType varchar(100) null		
		        ,zoneCode int null
		        ,zoneName varchar(100) null
		                    )
                      INSERT INTO @tb_empleado
                      SELECT
                                    EMP.codigo_empleado
											, ltrim(rtrim(EMP.nombre_empleado)) as employeeName
											, ltrim(rtrim(EMP.casilla_correo)) as casilla_correo
									, CASE 
                                            when EMP.tipo_empleado = 'EJECOMERCIAL'  then EMP.codigo_sucursal
                                            when EMP.tipo_empleado = 'AGENTE'  then AGENTE.codigo_sucursal
                                            when EMP.tipo_empleado = 'ZONAL'  then null
                                            when EMP.tipo_empleado = 'ASISCOMERCIAL'  then null
                                            else EMP.codigo_sucursal 
											end as branchCode
			                        , CASE 
											when EMP.tipo_empleado = 'EJECOMERCIAL'  then SUC.descripcion_sucursal
                                            when EMP.tipo_empleado = 'AGENTE'  then SUC2.descripcion_sucursal
                                            when EMP.tipo_empleado = 'ZONAL'  then null
                                            when EMP.tipo_empleado = 'ASISCOMERCIAL'  then null
                                            else SUC.descripcion_sucursal
											end as descripcion_sucursal
											,null
											,isnull(EMP.tipo_empleado,'NINGUNO') as userType
                                    ,CASE 
											when EMP.tipo_empleado = 'EJECOMERCIAL'  then ZON.codigo_zona
                                            when EMP.tipo_empleado = 'AGENTE'  then ZON2.codigo_zona
                                            when EMP.tipo_empleado = 'ZONAL'  then ZONAL.codigo_zona
                                            when EMP.tipo_empleado = 'ASISCOMERCIAL'  then ASIS.codigo_zona
                                            else ZON.codigo_zona 
                                            end as zoneCode
			                                , null

	                            FROM dba.tb_fin06 EMP WITH(NOLOCK)
										   inner join segpriv.dba.usuario USU WITH(NOLOCK) ON EMP.login_name = USU.login_name 
										   left outer join dba.tb_fin44 SUC WITH(NOLOCK) ON EMP.codigo_sucursal = SUC.codigo_sucursal 
										   left outer join dba.tb_zona ZON WITH(NOLOCK) ON SUC.codigo_zona = ZON.codigo_zona 
										   left outer join dba.tb_asistente_comercial ASIS WITH(NOLOCK) ON EMP.codigo_empleado = ASIS.codigo_empleado 
										   left outer join dba.tb_zonal ZONAL WITH(NOLOCK) ON EMP.codigo_empleado = ZONAL.codigo_empleado
										   --AGENTE
										   left outer join dba.tb_agente AGENTE WITH(NOLOCK) ON EMP.codigo_empleado = AGENTE.codigo_empleado 
										   left outer join dba.tb_fin44 SUC2 WITH(NOLOCK) ON AGENTE.codigo_sucursal = SUC2.codigo_sucursal 
										   left outer join dba.tb_zona ZON2 WITH(NOLOCK) ON SUC2.codigo_zona = ZON2.codigo_zona 
										   where   EMP.login_name IS NOT NULL
										   and ( @{nameof(email)} is null or EMP.casilla_correo = @{nameof(email)} )
										   and ISNULL(USU.ausente,'N') = 'N'
										   ORDER BY EMP.codigo_empleado
	 
	                  SELECT 
										   ltrim(rtrim(email)) as Email
										   ,ltrim(rtrim(employeeName))	as Name
										   ,ltrim(rtrim(userType))	as Position
		              FROM @tb_empleado

                                    ";
            var parameters = new
            {
                email
            };
            (string, object) result = (query, parameters);
            return result;
        }
    }
}
