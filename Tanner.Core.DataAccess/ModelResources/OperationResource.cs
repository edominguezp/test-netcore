using System;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class OperationResource
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
        public string ClientName
        {
            get
            {
                var result = _clientName.NormalizeString();
                return result;
            }
            set
            {
                _clientName = value;
            }
        }
        private string _clientName;

        /// <summary>
        /// Client RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        public string ClientRUT
        {
            get
            {
                var result = _clientRUT.NormalizeString();
                return result;
            }
            set
            {
                _clientRUT = value;
            }
        }
        private string _clientRUT;

        /// <summary>
        /// Register date of operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha registro de la operación
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// Name Product
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre Producto
        /// </summary>
        public string ProductName
        {
            get
            {
                var result = _productName.NormalizeString();
                return result;
            }
            set
            {
                _productName = value;
            }
        }
        private string _productName;

        /// <summary>
        /// Code of product
        /// </summary>
        /// <summary xml:lang="es">
        /// Codigo de producto
        /// </summary>
        public string ProductCode
        {
            get
            {
                var result = _productCode.NormalizeString();
                return result;
            }
            set
            {
                _productCode = value;
            }
        }
        private string _productCode;

        /// <summary>
        /// State of operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Estado de la operación
        /// </summary>
        public OperationState? State { get; set; }

        /// <summary>
        /// Current Task
        /// </summary>
        /// <summary xml:lang="es">
        /// Tarea actual
        /// </summary>
        public string CurrentTask
        {
            get
            {
                var result = _currentTask.NormalizeString();
                return result;
            }
            set
            {
                _currentTask = value;
            }
        }
        private string _currentTask;

        /// <summary>
        /// Name of executive
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del ejecutivo
        /// </summary>
        public string EmployeeName
        {
            get
            {
                var result = _employeeName.NormalizeString();
                return result;
            }
            set
            {
                _employeeName = value;
            }
        }
        private string _employeeName;

        /// <summary>
        /// Email Executive
        /// </summary>
        /// <summary xml:lang="es">
        /// Correo ejecutivo
        /// </summary>
        public string EmployeeEmail
        {
            get
            {
                var result = _employeeEmail.NormalizeString();
                return result;
            }
            set
            {
                _employeeEmail = value;
            }
        }
        private string _employeeEmail;


        

        public static (string, object) Query_GetOperationsByEmployeeEmail(OperationByExecutiveOrAgent request)
        {
            var query = $@"
                DECLARE @user_core VARCHAR(15)
                DECLARE @cod_suc VARCHAR(100)
                DECLARE @ld_tope DATETIME 
                DECLARE @position VARCHAR (50)
                DECLARE @ld_query_date DATETIME
				/*GLC inicio declare*/
				DECLARE @codigo_empleado INT
				DECLARE @cod_ejec TABLE( codigo_empleado INT)
				/*GLC fin declare*/

                SET @ld_query_date = DATEADD(day, -@operation_days, GETDATE())

                SELECT 
                    @user_core = login_name, 
	                @cod_suc = codigo_sucursal, 
	                @position = tipo_empleado,
					@codigo_empleado =  codigo_empleado --GLC agrega @codigo_empleado para validar si backup
                FROM 
                    dba.tb_fin06 EMP WITH(NOLOCK)
                WHERE 
                    LTRIM(RTRIM(casilla_correo)) = @employee_email

                DECLARE @search_operation TABLE 
                (
                    Number INT NULL,
                    ClientName VARCHAR(100) NULL,
                    ClientRUT VARCHAR(20) NULL,
                    RegisterDate datetime NULL,
                    State INT NULL, 
                    CurrentTask VARCHAR(100) NULL,
                    EmployeeName VARCHAR(100) NULL,                             
                    EmployeeEmail VARCHAR(50) NULL,
                    ProductName VARCHAR(50) NULL,
                    ProductCode VARCHAR (20) NULL,
                    estado_op_codigo INT NULL
                )

                IF @position = 'EJECOMERCIAL'  OR  @position = 'ASISCOMERCIAL'
                    BEGIN 
						/*GLC carga tabla con ejecutivos*/
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
						/*GLC fin carga tabla con ejecutivos*/
                    END

                ELSE IF @position = 'AGENTE'  
                    BEGIN

						/*GLC carga tabla con ejecutivos*/
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
						/*GLC fin carga tabla con ejecutivos*/
                    END

				--GLC se deja una sola Query para Cargar tabla @search_operation
                INSERT INTO 
                    @search_operation       
                SELECT 
                    OP.numero_operacion as Number,
                    ISNULL(CLI.nombre_cliente, PER.razon_social) as ClientName,
                    PER.rut_persona as ClientRUT,
                    OP.fecha_operacion as RegisterDate,
                    OP.estado_operacion as State,
                    CRED.tarea_actual as CurrentTask,
                    EMP.nombre_empleado as EmployeeName,
                    @employee_email as EmployeeEmail,
                    TDOC.descripcion_documento AS ProductName,
                    TDOC.cod_tipo_doc AS ProductCode,
                    EstOP.codigo as estado_op_codigo
                FROM dba.tb_fin17 as OP WITH ( NOLOCK )
                LEFT JOIN ( SELECT 
				        nombre_cliente, 
				        codigo_cliente, 
				        codigo_persona, 
				        codigo_empleado 
			        FROM dba.tb_fin01 (NOLOCK) ) CLI ON ( OP.codigo_cliente = CLI.codigo_cliente) 
                LEFT JOIN( SELECT 
				        razon_social,
				        rut_persona,
				        codigo_persona 
			        FROM dba.tb_fin41 WITH ( NOLOCK )) PER ON ( CLI.codigo_persona = PER.codigo_persona) 
                LEFT JOIN ( SELECT 
						nombre_empleado,
				        codigo_empleado,
				        login_name 
			        FROM dba.tb_fin06 WITH ( NOLOCK )) EMP ON (CLI.codigo_empleado = EMP.codigo_empleado) 
                LEFT JOIN ( SELECT 
				        descripcion,
				        codigo,
				        tipo 
			        FROM dba.tb_fin50 WITH ( NOLOCK ) ) EstOP ON (OP.estado_operacion = EstOp.codigo AND EstOp.tipo =3 AND EstOP.codigo IN @status)
                LEFT JOIN (
			        SELECT 
				        tarea_actual,
				        num_op,
				        vb_ejecutivo 
			        FROM  dbo.credito WITH ( NOLOCK )) CRED ON (OP.numero_operacion = CRED.num_op)
                LEFT JOIN (
			        SELECT 
				        descripcion_documento,
				        cod_tipo_doc,
				        tipo_documento 
			        FROM 
				        DBA.TB_FIN61  WITH (NOLOCK) ) TDOC ON (OP.tipo_documento = TDOC.tipo_documento)    
                INNER JOIN (
			        SELECT 
				        num_oper 
			        FROM  
				        log_evaluacion WITH ( NOLOCK ) 
			        GROUP BY 
				        num_oper 
			        HAVING		
				        MIN(fyh_log) >= @ld_query_date) logeva ON OP.numero_operacion = logeva.num_oper
                WHERE
                    EstOP.codigo IN @status AND 
                    (@operation_number IS NULL OR OP.numero_operacion = @operation_number) AND
			        (@executive_approval IS NULL OR CRED.vb_ejecutivo = @executive_approval) AND
					(EMP.codigo_empleado IN (SELECT codigo_empleado FROM @cod_ejec )) --GLC lista todas las operaciones donde el codigo de empleado contiene los ejecutivos cargados previamiente
                    and OP.enviada_cc = 'S'

                SELECT COUNT(*) FROM @search_operation

                SELECT * FROM @search_operation
            ";
            
            var param = new {
                employee_email = request.Email,
                operation_days = request.Days,
                status = request.Status,
                operation_number = request.OperationNumber,
                executive_approval = request.IsExecutiveApproval == null ? 
                                    null : (request.IsExecutiveApproval == true ? "S" : "N")
            };

            (string, object) result = (query, param);
            return result;
        }
    }
}
