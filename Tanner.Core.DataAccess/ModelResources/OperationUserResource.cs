using System.Collections.Generic;
using Tanner.Core.DataAccess.Models;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class OperationUserResource
    {
        /// <summary>
        /// Summary states
        /// </summary>
        /// <summary xml:lang="es">
        /// Resumen de estados
        /// </summary>
        public IEnumerable<SummaryStates> SummaryStates { get; set; }

        /// <summary>
        /// Operation total
        /// </summary>
        /// <summary xml:lang="es">
        /// Total del operaciones
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Operations
        /// </summary>
        /// <summary xml:lang="es">
        /// Operaciones
        /// </summary>
        public IEnumerable<OperationUser> Operations { get; set; }

        public static (string, object) Query_OperationByUser(string userID)
        {
            var parameters = new
            {
                email_ejecutivo = userID
            };

            var query = $@"
                DECLARE @usuario_core	VARCHAR(15)
				DECLARE @ld_tope		DATETIME 
				DECLARE @cod_suc		VARCHAR(100)
				DECLARE @tipo_empleado		VARCHAR (50)
				DECLARE @codigo_empleado INT
				DECLARE @ld_fec_consulta DATETIME
				DECLARE @ld_fec_consulta_vig DATETIME

				set @ld_fec_consulta = DATEADD(day, -7, GETDATE() )
				set @ld_fec_consulta_vig = DATEADD(day, -7, GETDATE() )
							
				SELECT 
					@usuario_core = login_name, 
					@cod_suc = codigo_sucursal, 
					@tipo_empleado = tipo_empleado,
					@codigo_empleado =  codigo_empleado
				FROM dba.tb_fin06 EMP WITH(NOLOCK)
				WHERE LTRIM(RTRIM(casilla_correo)) = LTRIM(RTRIM(@email_ejecutivo))

				DECLARE @cod_ejec TABLE( codigo_empleado INT)
				DECLARE @busqueda TABLE (numero_operacion		int				null
										,nombre					varchar(100)	null
										,rut_persona			varchar(20)		null
										,fecha_operacion		datetime		null
										,estado_operacion		varchar(15)		null
										,estado_op_codigo		int				null
										,tarea_actual			varchar(100)	null
										,nombre_empleado		varchar(100)	null								
										,email_ejecutivo		varchar(50)		null
										,nombre_sucursal		varchar(100)	null
										,nombre_zona			varchar(100)	null)
				print @tipo_empleado

				IF @tipo_empleado = 'EJECOMERCIAL'  OR  @tipo_empleado = 'ASISCOMERCIAL'
					BEGIN 
					--print 'EJECOMERCIAL'
						/*carga tabla con ejecutivos*/
						Insert Into @cod_ejec
						--Primer union: Ejecutivo o Asistentente Ingresado
						Select @codigo_empleado
						Union
						--Segunda union: Los Ejecutivos donde el Ejecutivo o Asistentente Ingresado es backup 
						Select EMP.codigo_empleado
						From	dba.tb_fin06 EMP WITH(NOLOCK)
						Where 	EMP.codigo_backup = @codigo_empleado And 
								EMP.backup_activo = 'S' And 
								EMP.tipo_empleado in ('EJECOMERCIAL','ASISCOMERCIAL')
						Union
						--Tercer union: Todos los Ejecutivos donde el Ejecutivo Ingresado es backup de un Agente
						Select EMP.codigo_empleado
						From	dba.tb_fin06 EMP WITH(NOLOCK)
						Where 	EMP.tipo_empleado =  'EJECOMERCIAL' and 
						Exists (Select 1
								From	dba.tb_agente AGE WITH(NOLOCK)
								Where 	AGE.codigo_backup = @codigo_empleado And 
										AGE.backup_activo = 'S' And
										AGE.codigo_sucursal = Emp.codigo_sucursal) 
						/*fin carga tabla con ejecutivos*/
					END

				ELSE 
					IF @tipo_empleado = 'AGENTE'  
						BEGIN
							--print 'AGENTE'
							/*carga tabla con ejecutivos*/
							Insert Into @cod_ejec
							--Agente Ingresado
							Select @codigo_empleado
							union
							--Primer union: Todos los Ejecutivo del Agente
							Select EMP.codigo_empleado
							From	dba.tb_fin06 EMP WITH(NOLOCK)
							Where 	EMP.tipo_empleado =  'EJECOMERCIAL' and 
							Exists (Select 1
									From	dba.tb_agente AGE WITH(NOLOCK)
									Where 	AGE.codigo_empleado = @codigo_empleado And 
											AGE.codigo_sucursal = Emp.codigo_sucursal) 
							union
							--Segunda union: todos Los Ejecutivos de un Agente donde su backup es el Agente Ingresado
							Select EMP.codigo_empleado
							From	dba.tb_fin06 EMP WITH(NOLOCK)
							Where 	EMP.tipo_empleado =  'EJECOMERCIAL' and 
							Exists (Select 1
									From	dba.tb_agente AGE WITH(NOLOCK)
									Where 	AGE.codigo_backup = @codigo_empleado And 
											AGE.backup_activo = 'S' And
											AGE.codigo_sucursal = Emp.codigo_sucursal) 
							--
							--Los Ejecutivos donde los ejecutivo cargados previamente son su backup 
							Insert Into @cod_ejec
							Select EMP.codigo_empleado
							From	dba.tb_fin06 EMP WITH(NOLOCK)
							inner join @cod_ejec e ON  EMP.codigo_backup = e.codigo_empleado
							Where 	EMP.backup_activo = 'S' And 
									EMP.tipo_empleado  = 'EJECOMERCIAL'
							--
							--se cargan todos los ejecutivos porque el agente es backup de un zonal
							Insert Into @cod_ejec
							Select EMP.codigo_empleado
							From	dba.tb_fin06 EMP WITH(NOLOCK)
							Where 	EMP.tipo_empleado  in ('EJECOMERCIAL', 'AGENTE' , 'ASISCOMERCIAL', 'ZONAL')
							And Exists (Select 1
									From	dba.tb_zonal ZON WITH(NOLOCK)
									Where 	ZON.codigo_backup = @codigo_empleado And 
											ZON.backup_activo = 'S') 
							/*fin carga tabla con ejecutivos*/
						END
					ELSE --'OTROS'
						BEGIN
						--print 'OTROS'
							/*carga todos los ejecutivos*/
							Insert Into @cod_ejec
							Select EMP.codigo_empleado
							From	dba.tb_fin06 EMP WITH(NOLOCK)
							Where 	EMP.tipo_empleado  in ('EJECOMERCIAL', 'AGENTE' , 'ASISCOMERCIAL', 'ZONAL')
						END
				-- INSERTAMOS OPERACIONES EN ANALISIS(0) y OPERACIONES APROBADAS(1) 
				insert into @busqueda
				select OP.numero_operacion,
						ISNULL(RTRIM(CLI.nombre_cliente), RTRIM(PER.razon_social)) as nombre ,
						PER.rut_persona as rut_persona,
						OP.fecha_operacion,
						EstOP.descripcion as estado_operacion,
						EstOP.codigo as estado_op_codigo,
						isnull(CRED.tarea_actual,''),
						RTRIM(EMP.nombre_empleado) as nombre_empleado,
						LTRIM(RTRIM(EMP.casilla_correo)) as email_ejecutivo,
						suc.descripcion_sucursal as nombre_sucursal,
						zo.nombre_zona as nombre_zona
				from dba.tb_fin17 as OP WITH ( NOLOCK )
				inner join dba.tb_fin44 as suc WITH ( NOLOCK ) ON OP.codigo_sucursal = suc.codigo_sucursal
				inner join dba.tb_zona as zo   WITH ( NOLOCK ) ON suc.codigo_zona = zo.codigo_zona 
				inner join ( SELECT 
										nombre_cliente, 
										codigo_cliente, 
										codigo_persona, 
										codigo_empleado 
									FROM dba.tb_fin01 (NOLOCK) ) CLI ON ( OP.codigo_cliente = CLI.codigo_cliente)
				inner join ( SELECT 
										razon_social,
										rut_persona,
										codigo_persona 
									FROM dba.tb_fin41 WITH ( NOLOCK )) PER ON ( CLI.codigo_persona = PER.codigo_persona) 
				inner join ( SELECT nombre_empleado,
										codigo_empleado,
										casilla_correo
									FROM dba.tb_fin06 WITH ( NOLOCK )) EMP ON (CLI.codigo_empleado = EMP.codigo_empleado) 
				inner join @cod_ejec as e on (EMP.codigo_empleado = e.codigo_empleado)  --lista todas las operaciones donde el codigo de empleado contiene los ejecutivos cargados previamiente
				inner join dba.tb_fin50 as EstOP WITH ( NOLOCK ) ON (OP.estado_operacion = EstOp.codigo and  EstOp.tipo =3) 
				inner join dbo.credito  as CRED WITH ( NOLOCK ) ON (OP.numero_operacion = CRED.num_op)
				inner join (SELECT num_oper FROM  log_evaluacion WITH ( NOLOCK ) GROUP BY num_oper HAVING  MAX(fyh_log) >= @ld_fec_consulta) logeva on  OP.numero_operacion = logeva.num_oper and EstOP.codigo in( 0,1)
				where OP.estado_operacion in (0,1)
				-- INSERTAMOS  VIGENTES(2)
				union
				select OP.numero_operacion,
						ISNULL(RTRIM(CLI.nombre_cliente), RTRIM(PER.razon_social)) as nombre ,
						PER.rut_persona as rut_persona,
						OP.fecha_operacion,
						EstOP.descripcion as estado_operacion,
						EstOP.codigo as estado_op_codigo,
						isnull(CRED.tarea_actual,''),
						RTRIM(EMP.nombre_empleado) as nombre_empleado,
						LTRIM(RTRIM(EMP.casilla_correo))  as email_ejecutivo,
						suc.descripcion_sucursal as nombre_sucursal,
						zo.nombre_zona as nombre_zona
				from dba.tb_fin17  as OP WITH ( NOLOCK )
				inner join dba.tb_fin44 as suc WITH ( NOLOCK ) ON OP.codigo_sucursal = suc.codigo_sucursal
				inner join dba.tb_zona as zo   WITH ( NOLOCK ) ON suc.codigo_zona = zo.codigo_zona 
				inner join ( SELECT 
										nombre_cliente, 
										codigo_cliente, 
										codigo_persona, 
										codigo_empleado 
									FROM dba.tb_fin01 (NOLOCK) ) CLI ON ( OP.codigo_cliente = CLI.codigo_cliente)
				inner join ( SELECT 
										razon_social,
										rut_persona,
										codigo_persona 
									FROM dba.tb_fin41 WITH ( NOLOCK )) PER ON ( CLI.codigo_persona = PER.codigo_persona) 
				inner join ( SELECT nombre_empleado,
										codigo_empleado,
										casilla_correo
									FROM dba.tb_fin06 WITH ( NOLOCK )) EMP  ON (CLI.codigo_empleado = EMP.codigo_empleado) 
				inner join @cod_ejec as e on (EMP.codigo_empleado = e.codigo_empleado) --lista todas las operaciones donde el codigo de empleado contiene los ejecutivos cargados previamiente
				inner join dba.tb_fin50 as EstOP WITH ( NOLOCK ) ON (OP.estado_operacion = EstOp.codigo and  EstOp.tipo =3) 
				inner join dbo.credito as CRED WITH ( NOLOCK ) ON (OP.numero_operacion = CRED.num_op)
				inner join (SELECT num_oper FROM  log_evaluacion WITH ( NOLOCK ) GROUP BY num_oper HAVING  MAX(fyh_log) >= @ld_fec_consulta_vig) logeva on  OP.numero_operacion = logeva.num_oper and EstOp.codigo = 2
				where OP.estado_operacion = 2
				--salida1
				select numero_operacion	AS OperationNumber	
				,nombre AS CustomerName
				,[dbo].[f_formatea_rut_azure](rut_persona) AS CustomerRUT
				,fecha_operacion AS OperationDate
				,estado_operacion AS OperationState
				,tarea_actual AS CurrentTask
				,nombre_empleado AS EmployeeName
				,email_ejecutivo AS ExecutiveEmail
				,RTRIM(nombre_sucursal) AS BranchOffice
				,RTRIM(nombre_zona) AS Zone
				,RTRIM(@tipo_empleado) AS EmployeeType
				from @busqueda	
				order by fecha_operacion DESC 
				--salida2
				select estado_operacion AS State, count (1) AS Total
				from @busqueda
				group by estado_operacion
				--salida3
				select count (numero_operacion) as Total
				from @busqueda";

            (string, object) result = (query, parameters);

            return result;
        }
    }
}
