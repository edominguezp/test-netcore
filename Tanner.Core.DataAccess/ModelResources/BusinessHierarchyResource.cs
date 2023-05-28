namespace Tanner.Core.DataAccess.ModelResources
{
    public class BusinessHierarchyResource
    {
        /// <summary>
        /// Code of Employee
        /// </summary>
        /// <summary xml:lang="es">
        /// Codigo de empleado
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Name of Employee
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre de empleado
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Email Executive
        /// </summary>
        ///<summary xml:lang="es">
        /// Correo ejecutivo
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Login for CORE
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre para hacer login en CORE
        /// </summary>
        public string LoginNameCORE { get; set; }

        /// <summary>
        /// Phone Executive
        /// </summary>        
        /// ///<summary xml:lang="es">
        /// Telefono ejecutivo
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Code of Branch
        /// </summary>
        ///<summary xml:lang="es">
        /// Codigo de sucursal
        /// </summary>
        public int BranchCode { get; set; }

        /// <summary>
        /// Name of BranchOffice of Executive
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre de Sucursal
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// Name of Agent
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre de agente
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// Email of Agent
        /// </summary>
        ///<summary xml:lang="es">
        /// Correo del agente
        /// </summary>
        public string AgentEmail { get; set; }

        /// <summary>
        /// Type of User
        /// </summary>
        ///<summary xml:lang="es">
        /// Tipo de usuario
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// Code of Zone 
        /// </summary>7
        ///<summary xml:lang="es">
        /// Codigo de zona
        /// </summary>
        public int ZoneCode { get; set; }

        /// <summary>
        /// Name of Zone
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre de zona
        /// </summary>
        public string ZoneName { get; set; }
        
        /// <summary>
        /// Name of Zonal
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre del zonal
        /// </summary>
        public string ZonalName { get; set; }

        /// <summary>
        /// Email of Zonal
        /// </summary>
        ///<summary xml:lang="es">
        /// Correo del zonal
        /// </summary>
        public string ZonalEmail { get; set; }

        /// <summary>
        /// Code of State
        /// </summary>
        ///<summary xml:lang="es">
        /// codigo de estado
        /// </summary>
        public int StateCode { get; set; }

        public static (string, object) Query_CommercialHierarchy(string email)
        {
            var query = $@"
                DECLARE @employeeCode varchar(50)
                DECLARE @employeeName varchar(100)
                --DECLARE @email varchar(100)
                DECLARE @loginNameCORE varchar(50)		
                DECLARE @phone varchar(100)				
                DECLARE @branchCode int
                DECLARE @branchName varchar(100)
                DECLARE @agentName varchar(100)
                DECLARE @agentEmail varchar(100)
                DECLARE @userType varchar(100)		
                DECLARE @zoneCode int
                DECLARE @zoneName varchar(100)
                DECLARE @zonalName varchar(100)
                DECLARE @zonalEmail varchar(100)
                DECLARE @statateCode int
                DECLARE @codigo_empleado int
                --
                --SET @email = 'cortiz@tanner.cl'
                --SET @email = 'zonal.prueba@tanner.cl'
                --SET @email = 'jahumada@tanner.cl'
                --
                SELECT @codigo_empleado =  codigo_empleado FROM dba.tb_fin06 EMP WITH(NOLOCK)  WHERE LTRIM(RTRIM(casilla_correo)) = @email
                --
                SELECT TOP 1 
                    @employeeCode	= EMP.codigo_empleado,
                    @employeeName	= EMP.nombre_empleado,
	                @email			= EMP.casilla_correo,
                    @loginNameCORE = EMP.login_name,
                    @phone			=EMP.telefono,
                    @branchCode = CASE 
                        when EMP.tipo_empleado = 'EJECOMERCIAL' THEN EMP.codigo_sucursal
                        when EMP.tipo_empleado = 'AGENTE' THEN AGENTE.codigo_sucursal
                        when EMP.tipo_empleado = 'ZONAL' THEN NULL
                        when EMP.tipo_empleado = 'ASISCOMERCIAL' THEN NULL
                        else EMP.codigo_sucursal 
                    END,
	                @branchName = CASE 
                        WHEN EMP.tipo_empleado = 'EJECOMERCIAL' THEN SUC.descripcion_sucursal
                        WHEN EMP.tipo_empleado = 'AGENTE' THEN SUC2.descripcion_sucursal
                        WHEN EMP.tipo_empleado = 'ZONAL' THEN NULL
                        WHEN EMP.tipo_empleado = 'ASISCOMERCIAL' THEN NULL
                        ELSE SUC.descripcion_sucursal
                    end,
                    @userType = isnull(EMP.tipo_empleado,'NINGUNO'),
                    @zoneCode = CASE 
                        WHEN EMP.tipo_empleado = 'EJECOMERCIAL' THEN ZON.codigo_zona
                        WHEN EMP.tipo_empleado = 'AGENTE' THEN ZON2.codigo_zona
                        WHEN EMP.tipo_empleado = 'ZONAL' THEN ZONAL.codigo_zona
                        WHEN EMP.tipo_empleado = 'ASISCOMERCIAL' THEN ASIS.codigo_zona
                        ELSE ZON.codigo_zona 
                    END,
	                @zoneName = Isnull(case 
                            when EMP.tipo_empleado = 'EJECOMERCIAL'  then ZON.nombre_zona
			                when EMP.tipo_empleado = 'AGENTE'  then ZON2.nombre_zona
			                when EMP.tipo_empleado = 'ZONAL'  then ZONAZON.nombre_zona
			                when EMP.tipo_empleado = 'ASISCOMERCIAL'  then ZONASIS.nombre_zona
			                else ZON.nombre_zona 
		                  end,'Sin Zona'),
                    @statateCode = CASE 
                        WHEN (ISNULL(USU.ausente,'N')='S' AND ISNULL(USU.bloqueado,'N')='S') THEN 1 
                        ELSE 0 end
                FROM dba.tb_fin06 EMP WITH(NOLOCK)
                    INNER JOIN segpriv.dba.usuario USU WITH(NOLOCK) ON EMP.login_name = USU.login_name 
	                LEFT OUTER JOIN dba.tb_fin44 SUC WITH(NOLOCK) ON EMP.codigo_sucursal = SUC.codigo_sucursal 
                    LEFT OUTER JOIN dba.tb_zona ZON WITH(NOLOCK) ON SUC.codigo_zona = ZON.codigo_zona 
                    LEFT OUTER JOIN dba.tb_asistente_comercial ASIS WITH(NOLOCK) ON EMP.codigo_empleado = ASIS.codigo_empleado 
	                left outer join dba.tb_zona ZONASIS WITH(NOLOCK) ON ASIS.codigo_zona = ZONASIS.codigo_zona

                    LEFT OUTER JOIN dba.tb_zonal ZONAL WITH(NOLOCK) ON EMP.codigo_empleado = ZONAL.codigo_empleado
	                left outer join dba.tb_zona ZONAZON WITH(NOLOCK) ON ZONAL.codigo_zona = ZONAZON.codigo_zona 
                    LEFT OUTER JOIN dba.tb_agente AGENTE WITH(NOLOCK) ON EMP.codigo_empleado = AGENTE.codigo_empleado 
                    LEFT OUTER JOIN dba.tb_fin44 SUC2 WITH(NOLOCK) ON AGENTE.codigo_sucursal = SUC2.codigo_sucursal 
                    LEFT OUTER JOIN dba.tb_zona ZON2 WITH(NOLOCK) ON SUC2.codigo_zona = ZON2.codigo_zona 
                WHERE   EMP.codigo_empleado = @codigo_empleado
                AND ( ISNULL(USU.ausente,'N') = 'N' OR ISNULL(USU.bloqueado,'N')='N')


                --SUCURSAL
                IF @branchCode IS NOT NULL 
                BEGIN
                SELECT  @agentName =emp.nombre_empleado,
		                @agentEmail = emp.casilla_correo
                FROM dba.tb_fin06 emp WITH(NOLOCK) 
                INNER JOIN ( SELECT  CASE 
						                WHEN AGENTE3.codigo_backup is not null and AGENTE3.backup_activo ='S' THEN
							                AGENTE3.codigo_backup
						                ELSE 
							                AGENTE3.codigo_empleado
						                END AS CODIGO_EMPLEADO 
			                FROM dba.tb_agente AGENTE3 WITH(NOLOCK) 
			                 WHERE AGENTE3.codigo_sucursal = @branchCode ) agent  ON (emp.codigo_empleado = agent.codigo_empleado)
                END 

                --ZONA
                IF @zoneCode IS NOT NULL 
                BEGIN
	                SELECT  @zonalName =emp.nombre_empleado,
			                @zonalEmail = emp.casilla_correo
	                FROM dba.tb_fin06 emp WITH(NOLOCK) 
	                INNER JOIN ( SELECT 
						                CASE WHEN @userType =  'ZONAL' THEN
								                ZONAL3.codigo_empleado
							                 ELSE 
								                CASE 
									                WHEN ZONAL3.codigo_backup is not null and ZONAL3.backup_activo ='S' THEN
										                ZONAL3.codigo_backup
									                ELSE 
										                ZONAL3.codigo_empleado
									                END
							                 END AS CODIGO_EMPLEADO
				                FROM dba.tb_zonal ZONAL3 WITH(NOLOCK) 
				                 WHERE ZONAL3.codigo_zona = @zoneCode ) zonal  ON (emp.codigo_empleado = zonal.codigo_empleado)
                END 

                --Resultado
                SELECT 
		                ltrim(rtrim(@employeeCode)) as employeeCode,
		                ltrim(rtrim(@employeeName))	as employeeName,
		                ltrim(rtrim(@email)) as email,
		                ltrim(rtrim(@loginNameCORE)) as loginNameCORE,
		                ltrim(rtrim(@phone)) as phone,
		                @branchCode as branchCode,
		                ltrim(rtrim(@branchName)) as branchName,
		                ltrim(rtrim(@agentName)) as agentName,
		                ltrim(rtrim(@agentEmail)) as agentEmail,
		                ltrim(rtrim(@userType)) as userType,
		                @zoneCode as zoneCode,
		                ltrim(rtrim(@zoneName)) as zoneName,
		                ltrim(rtrim(@zonalName)) as zonalName,
		                ltrim(rtrim(@zonalEmail)) as zonalEmail,
		                ltrim(rtrim(@statateCode)) as stateCode";

            object param = new { email };

            (string, object) result = (query, param);
            return result;
        }

    }
}
