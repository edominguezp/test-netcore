using System;
using System.Collections.Generic;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the financial detail by client
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa el detalle financiero del cliente
    /// </summary>
    public class FinancialDetailResource
    {
        /// <summary>
        /// Initial date register
        /// </summary>
        ///<summary xml:lang="es">
        /// Fecha de inicio de registro
        /// </summary>
        public DateTime InitialDateRegister { get; set; }

        /// <summary>
        /// Client state
        /// </summary>
        ///<summary xml:lang="es">
        /// Estado del cliente
        /// </summary>
        public string ClientState { get; set; }

        /// <summary>
        /// Current client Debt 
        /// </summary>
        ///<summary xml:lang="es">
        /// Mora actual del cliente
        /// </summary>
        public string CurrentClientDebt { get; set; }

        /// <summary>
        /// Approved Amount
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto aprobado
        /// </summary>
        public string ApprovedAmount { get; set; }

        /// <summary>
        /// Amount Factoring
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto para factoring
        /// </summary>
        public string FactoringAmount { get; set; }

        /// <summary>
        /// Busy line percentage
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje de la línea ocupada
        /// </summary>
        public string BusyLinePercentage { get; set; }

        /// <summary>
        /// Quantity of operations
        /// </summary>
        ///<summary xml:lang="es">
        /// Cantidad de operaciones
        /// </summary>
        public int NumberOfOperations { get; set; }

        /// <summary>
        /// Reoperations percentage
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje de reoperaciones
        /// </summary>
        public decimal ReoperationsPercentage { get; set; }

        /// <summary>
        /// Client payment percentage
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje de pago a clientes
        /// </summary>
        public decimal ClientPaymentPercentage { get; set; }

        /// <summary>
        /// Recency
        /// </summary>
        ///<summary xml:lang="es">
        /// Recencia (días)
        /// </summary>
        public float Recency { get; set; }

        /// <summary>
        /// Number of debtors
        /// </summary>
        ///<summary xml:lang="es">
        /// Número de deudores
        /// </summary>
        public int NumberOfDebtors { get; set; }

        /// <summary>
        /// Current debt
        /// </summary>
        ///<summary xml:lang="es">
        /// Deuda vigente
        /// </summary>
        public decimal CurrentDebt { get; set; }

        /// <summary>
        /// Protested documents
        /// </summary>
        ///<summary xml:lang="es">
        /// Documentos protestados
        /// </summary>
        public decimal ProtestedDocuments { get; set; }

        /// <summary>
        /// Delinquent credits
        /// </summary>
        ///<summary xml:lang="es">
        /// Créditos morosos
        /// </summary>
        public decimal DelinquentCredits { get; set; }

        /// <summary>
        /// Resource that represent indicators
        /// </summary>
        ///<summary xml:lang="es">
        /// Recurso que representa los indicadores 
        /// </summary>
        public IEnumerable<IndicatorsResource> Indicators { get; set; }

        /// <summary>
        /// Resource that represent indicator percentage
        /// </summary>
        ///<summary xml:lang="es">
        /// Recurso que representa el porcentaje de los indicadores
        /// </summary>
        public IndicatorsPercentageResource IndicatorsPercentageResource { get; set; }

        /// <summary>
        /// Resource that represent data of debtor
        /// </summary>
        ///<summary xml:lang="es">
        /// Recurso que representa los datos del deudor
        /// </summary>
        public IEnumerable<DebtorResource> DataDebtorResource { get; set; }

        /// <summary>
        /// Query that represent financial detail by rut client
        /// </summary>
        ///<summary xml:lang="es">
        /// Consulta que representa el detalle financiero dado el rut del cliente
        /// </summary>
        public static (string, object) Query_FinancialDetailByRUT(string rut)
        {
            var query = $@"
            --DECLARAMOS TABLAS DE SALIDA    
	        Declare @ClientData Table
            (
                InitialDate Datetime NOT NULL,					-- fecha de inicio
                StateClient varchar(100),						-- estado de cliente 
                CurrentSluggish varchar(100),					-- mora actual
                AmountApproved varchar(100),					-- monto aprobado
                Factoring varchar(100),							-- factoring
                LineBusyPercentage varchar(100),				-- porcentaje de linea ocupada 
                NumberOfOperations numeric(20) NOT NULL,        -- numero de operaciones 
                ReoperationsPercentage numeric(20) NOT NULL,    -- porcentaje de reoperaciones 
                CustomerPaymentPercentage numeric(20) NOT NULL, -- porcentaje pago de clientes
                Recency bigint NOT NULL,						-- recencia (días)
                NumberOfDebtors bigint NOT NULL,				-- numero de deudores
                CurrentDebt bigint NOT NULL,					-- deuda vigente
                Protest bigint NOT NULL,						-- protestos
                Credit bigint NOT NULL							-- crédito
            )

            Declare @Indicators Table
            (
                id int IDENTITY(1,1) NOT NULL Primary key,
                numero_operacion bigint,           
                fecha_operacion datetime,
                producto varchar(25),
                Rate varchar(10),                      --tasa
                Commission varchar(100),               --comisión
                Expense varchar(100),                   --gastos
                monto_operacion numeric (20,0)
            )

            Declare @Indicators_porcentaje Table
            (
		        id int IDENTITY(1,1) NOT NULL Primary key,
		        porcen_Cancelado_Cli numeric(20, 4),
		        porcen_Cancelado_Deu numeric(20, 4),
		        porcen_Cancelado_Ope numeric(20, 4)
            )

            Declare @DataDebtors Table
            (
                rut_deu					varchar(25),
		        deudor					varchar(100),
		        cod_dcto				bigint,
		        tipo					varchar(2),
		        saldo_deu				bigint,
		        saldo_cli				bigint,
		        monto_total				bigint,
		        porcentaje				numeric(18, 2),
		        monto_linea				bigint,
		        fecha_vcto_linea		datetime,
		        cruce					varchar(15)
            )

            --DECLARAMOS TABLAS DE TRABAJO
            Declare @ClieConcentracionDeudores Table
            (
		        rut_persona					varchar(25),
		        id_linea					bigint,
		        codigo_cliente				bigint,
		        razon_social				varchar(150),
		        porcentaje_concentracion	varchar(15)
            )

            --Declaramos tabla para acumular los valores PROTESTADOS;	
            Declare @TProtestados Table
            (
                id int IDENTITY(1, 1) NOT NULL Primary key,
                tipo varchar(25),
                cantidad numeric(20),
                saldo numeric(20)
            )

            --Declaramos tabla para acumular los valores CREDITOS
            Declare @TCreditos Table
            (
                id int IDENTITY(1, 1) NOT NULL Primary key,
                tipo varchar(25),
                cantidad numeric(20),
                saldo numeric(20)
            )

            --Declaramos tabla para acumular los valores DEUDA VIGENTE
            Declare @TDVigente Table
            (
                id int IDENTITY(1, 1) NOT NULL Primary key,
                tipo varchar(25),
                cantidad numeric(20),
                saldo numeric(20)
            )

            -- DECLARAMOS VARIABLES DE TRABAJO
            DECLARE @ln_codigo_cliente              Int
            DECLARE @ll_cant_operaciones bigint
            DECLARE @nombre_cliente                 T_t_descripcion
            DECLARE @fecha_ingreso T_t_fecha
            DECLARE @rut_persona                    T_t_rut
            DECLARE @estado_cliente varchar(25)
            DECLARE @ll_count                       INT; SELECT @ll_count = 1
            DECLARE @ll_count_tabla                 INT; SELECT @ll_count_tabla = 0
            DECLARE @ll_numero_operacion            bigint
            DECLARE @ll_tasa_operacion numeric(20, 4)
            DECLARE @ll_gastos_operacion            numeric(20, 4)
            DECLARE @ll_monto_comision              numeric(20, 4)
            DECLARE @ll_cantidad_deudores           bigint
            DECLARE @ll_monto_aprobado_linea bigint
            DECLARE @ll_mora_actual                 bigint
            DECLARE @ll_porcen_linea_ocupada bigint
            DECLARE @ldt_fecha_ult_ope              datetime
            DECLARE @ln_recencia_días Int
            DECLARE @ll_ult_operacion               bigint
            DECLARE @ll_protestos numeric(20, 4)
            DECLARE @ll_creditos                    numeric(20, 4)
            DECLARE @ll_cartera_vigente             numeric(20, 4)

            --manejo del RUT
            DECLARE @RUT_sin_DV     VARCHAR(10);
            DECLARE @ln_RUT_sin_DV  int;

            SET @{nameof(rut)} = replace(@{nameof(rut)}, '-', '')
            SET @{nameof(rut)} = right('0000000000' + @{nameof(rut)}, 10)
            SET @RUT_sin_DV = SUBSTRING(@{nameof(rut)}, 1, LEN(@{nameof(rut)}) - 1)
            set @ln_RUT_sin_DV = convert(int, @RUT_sin_DV)

            ----------------------------------------------------------------------------
            --Consultamos código de cliente
            ----------------------------------------------------------------------------
            SELECT 
		        @ln_codigo_cliente = isnull(c.codigo_cliente, 0)
            FROM dba.tb_fin01 c With(nolock)
		        INNER JOIN dba.tb_fin41 p With(nolock) ON c.codigo_persona = p.codigo_persona
            WHERE p.rut_persona = @{nameof(rut)}

            --Leemos la linea aprobada VIGENTE para el cliente
            SELECT 
		        @ll_monto_aprobado_linea = ISNULL(sum(monto_aprobado), 0)
            FROM dba.tb_fin02 with(nolock)
            WHERE 
		        codigo_cliente = @ln_codigo_cliente AND 
		        estado = 1

            --Obtenemos la saldo de operaciones MORA ACTUAL
            SELECT 
		        @ll_mora_actual = SUM(doc.valor_futuro_documento)-- / 1000(si es necesario mostrar dividido M$)
            FROM dba.tb_fin24 doc WITH(NOLOCK)
		        INNER JOIN dba.tb_fin17 ope WITH(NOLOCK) on doc.numero_operacion = ope.numero_operacion
            WHERE 
		        ope.codigo_cliente = @ln_codigo_cliente AND 
		        ope.estado_operacion IN(2,3) AND 
		        estado_documento in (1,3)

            --Calculamos el % de linea ocupada
            SET @ll_porcen_linea_ocupada =  CASE WHEN ISNULL(@ll_monto_aprobado_linea,0)  = 0  THEN
			0
			ELSE
			(((ISNULL(@ll_monto_aprobado_linea, 0) - ISNULL(@ll_mora_actual, 0)) * 100 )) / ISNULL(@ll_monto_aprobado_linea, 0)
			END

            --Leeamos datos del cliente
            SELECT 
		        @nombre_cliente = p.razon_social, 
		        @estado_cliente = 
			        CASE
				        WHEN c.estado_cliente = 0 THEN 'INGRESADO'
				        WHEN c.estado_cliente = 1 THEN 'ACTIVO'
				        WHEN c.estado_cliente = 2 THEN 'BLOQUEADO'
			        END,                                   
                @fecha_ingreso = c.fecha_ingreso,                                   
                @rut_persona = p.rut_persona
            FROM dba.tb_fin01 c WITH(NOLOCK)
		        INNER JOIN dba.tb_fin41 p WITH(NOLOCK) on p.codigo_persona = c.codigo_persona
            WHERE 
		        c.codigo_cliente = @ln_codigo_cliente

            --Cargamos tabla temporal de DEUDORES
            INSERT INTO @ClieConcentracionDeudores
            SELECT 
		        persona.rut_persona,
	            linea.id_linea,
	            cliente.codigo_cliente,
	            deudor.razon_social,
	            CONVERT(VARCHAR(8), CONVERT(INT, concentracion_especial.porcentaje_concentracion)) + '%' AS porcentaje_concentracion
            FROM dba.tb_fin01 cliente(NOLOCK)
		        JOIN dba.tb_fin02 linea(NOLOCK) ON cliente.codigo_cliente = linea.codigo_cliente
		        JOIN dbo.concentraciones_especiales concentracion_especial ON concentracion_especial.id_linea = linea.id_linea
		        JOIN dba.tb_fin08 deudor(NOLOCK) ON deudor.codigo_tercero = concentracion_especial.codigo_tercero
		        JOIN dba.tb_fin41 persona(NOLOCK) ON persona.codigo_persona = deudor.codigo_persona
            WHERE cliente.codigo_cliente = ISNULL(@ln_codigo_cliente, cliente.codigo_cliente)

            --Obtenemos la cantidad de operaciones
            SELECT 
		        @ll_cant_operaciones = count(1)
            FROM 
		        dba.tb_fin17 WITH(NOLOCK)
            WHERE 
		        codigo_cliente = @ln_codigo_cliente AND 
		        estado_operacion IN(2, 3)

            SELECT TOP 1 
		        @ldt_fecha_ult_ope = fecha_operacion,
		        @ll_ult_operacion = numero_operacion
            FROM dba.tb_fin17 WITH(NOLOCK)
            WHERE 
		        codigo_cliente = @ln_codigo_cliente
            ORDER BY fecha_operacion DESC

            --Obtenemos la recencia(días)
            SET @ln_recencia_días = DATEDIFF(DD, convert(date, @ldt_fecha_ult_ope, 103), convert(date, GETDATE(), 103))
            set @ln_recencia_días = isnull(@ln_recencia_días, 0)

            --Obtenemos la cantidad de deudores con los que ha trabajado el CLIENTE
            SELECT 
		        @ll_cantidad_deudores = COUNT(DISTINCT codigo_tercero)
            FROM dba.tb_fin24(NOLOCK)
            WHERE 
		        codigo_cliente = @ln_codigo_cliente AND 
		        estado_documento <> 9


            --Cargamos los protestos--
            --Letras Protestadas
            INSERT INTO @TProtestados ( cantidad, saldo )
            EXEC dba.spe_c2_cons_letras_protestadas @ln_codigo_cliente, @ll_ult_operacion
            UPDATE @TProtestados
            SET tipo = 'Letras Protestadas'
            WHERE id = 1
	        --Cheques Protestados
            INSERT INTO @TProtestados ( cantidad, saldo )
            EXEC dba.spe_c2_conc_cheques_protestados @ln_codigo_cliente, @ll_ult_operacion
            UPDATE @TProtestados
            SET tipo = 'Cheques Protestados'
            WHERE id = 2
	        --Cargamos la variable de protestos
            SELECT 
		        @ll_protestos = isnull(SUM(saldo), 0)
            FROM @TProtestados    
	

	        --Cargamos los créditos--
            --Cartera Vigente
            INSERT INTO @TCreditos ( cantidad, saldo )
            EXEC dba.spe_c2_cons_cartera_vigente @ln_codigo_cliente, @ll_ult_operacion
            UPDATE @TCreditos
            SET tipo = 'Cheques Protestados'
            WHERE id = 1
	        --Cartera Morosa
            INSERT INTO @TCreditos ( cantidad, saldo )
            exec dba.spe_c2_cons_cartera_morosa @ln_codigo_cliente, @ll_ult_operacion
            UPDATE @TCreditos
            SET tipo = 'Cartera Morosa'
            WHERE id = 2
	        --Cartera Vigente CR
            INSERT INTO @TCreditos ( cantidad, saldo )
            exec dba.spe_c2_cons_cartera_vigente_cr @ln_codigo_cliente, @ll_ult_operacion
            UPDATE @TCreditos
            SET tipo = 'Cartera Vigente CR'
            WHERE id = 3
	        --Cartera Morosa  CR
            INSERT INTO @TCreditos ( cantidad, saldo )
            exec dba.spe_c2_cons_cartera_morosa_cr @ln_codigo_cliente, @ll_ult_operacion
            UPDATE @TCreditos
            SET tipo = 'Cartera Morosa  CR'
            WHERE id = 4
	        --Cargamos la variable de protestos
            SELECT 
		        @ll_creditos = isnull(SUM(saldo), 0)
            FROM @TCreditos	                
	

	        --cargamos los DEUDA VIGENTE--                
	        --Cartera Vigente
            INSERT INTO @TDVigente ( cantidad, saldo )
            EXEC dba.spe_c2_cons_cartera_vigente @ln_codigo_cliente, @ll_ult_operacion
            UPDATE @TDVigente
            SET tipo = 'Cartera Vigente'
            WHERE id = 1
	        --Cartera Vigente CR
            INSERT INTO @TDVigente ( cantidad, saldo )
            EXEC dba.spe_c2_cons_cartera_vigente_cr @ln_codigo_cliente, @ll_ult_operacion
            UPDATE @TDVigente
            SET tipo = 'Cartera Vigente CR'
            WHERE id = 2
	        --Cargamos la variable de cartera vigente
            SELECT 
		        @ll_cartera_vigente = isnull(SUM(saldo), 0)
            FROM @TDVigente
	

            --cargamos los datos de INDICADORES de porcentaje
            INSERT INTO @Indicators_porcentaje (porcen_Cancelado_Cli, porcen_Cancelado_Deu, porcen_Cancelado_Ope)
            EXEC FACTORLINECORE.dba.spe_cons_porc_canc_cli_deu_ope_c1 @ln_codigo_cliente


            --Insertamos tabla de Deudores
            INSERT INTO @DataDebtors
            EXECUTE dbo.PR_deudores_c1 @ln_RUT_sin_DV


	        --Insertamos tabla de salida
	        INSERT INTO @ClientData
	        (
		        InitialDate,				-- fecha de inicio
		        StateClient,				-- estado de cliente
		        CurrentSluggish,				-- mora actual
		        AmountApproved,				-- monto aprobado
		        Factoring,					-- factoring
		        LineBusyPercentage,			-- porcentaje de linea ocupada
		        NumberOfOperations,			-- numero de operaciones
		        ReoperationsPercentage,		-- porcentaje de reoperaciones
		        CustomerPaymentPercentage,	-- porcentaje pago de clientes
		        Recency,					-- recencia(días)
		        NumberOfDebtors,			-- numero de deudores
		        CurrentDebt,				-- deuda vigente
		        Protest,					-- protestos
		        Credit						-- crédito
	        )
	        VALUES
	        (
		        @fecha_ingreso,				-- fecha de inicio
		        @estado_cliente,			-- estado de cliente
		        @ll_mora_actual,			-- mora actual
		        @ll_monto_aprobado_linea,	-- monto aprobado
		        @ll_monto_aprobado_linea,	-- factoring
		        @ll_porcen_linea_ocupada,	-- porcentaje de linea ocupada
		        @ll_cant_operaciones,		-- numero de operaciones
		        0,							-- porcentaje de reoperaciones
		        0,							-- porcentaje pago de clientes
		        @ln_recencia_días,			-- recencia(días)
		        @ll_cantidad_deudores,		-- numero de deudores
		        @ll_cartera_vigente,		-- deuda vigente
		        @ll_protestos,				-- protestos
		        @ll_creditos				-- crédito
	        )


            --Insertamos tabla de salida - solo numeros de operación
            INSERT INTO @Indicators ( numero_operacion, fecha_operacion, producto, monto_operacion )
            SELECT TOP 3 
		        numero_operacion, 
		        fecha_operacion,
                (select cod_tipo_doc from dba.tb_fin61 where tipo_documento = dba.tb_fin17.codigo_producto),
                valor_nominal_operacion
            FROM dba.tb_fin17 WITH (NOLOCK)
            WHERE 
		        codigo_cliente = @ln_codigo_cliente AND 
		        estado_operacion IN (2,3)
            ORDER BY numero_operacion DESC


            --Buscamos los datos para UPDATE por numero de operación
            --Contamos registros de la tabla temporal
            SELECT @ll_count = 1
            SELECT @ll_count_tabla = COUNT(1) FROM @Indicators


            --Realizamos ciclo para generar desde -hasta para los porcentajes
            WHILE @ll_count <= @ll_count_tabla
	        BEGIN
		        --limpiamos variables de trabajo
                SET @ll_tasa_operacion = 0
                SET @ll_gastos_operacion = 0

                --Rescatamos la operación
                SELECT 
			        @ll_numero_operacion = numero_operacion
                FROM @Indicators
		        WHERE 
			        id = @ll_count

                --rescatamos los valores de la operación leida
                SELECT 
			        @ll_tasa_operacion = ISNULL(tasa_operacion, 0),
			        @ll_gastos_operacion = ISNULL(cargos_afectos, 0),
			        @ll_monto_comision = 
				        CASE
					        WHEN tipo_comi_cob = 2 and codigo_moneda<> 1 THEN monto_comi_mx
                            WHEN tipo_comi_cob = 2 THEN monto_comi_cob
				        END
		        FROM dba.tb_fin17 WITH(NOLOCK)
                WHERE 
			        numero_operacion = @ll_numero_operacion

                --UPDATE DE VALORES POR OPRACION - CICLO
                UPDATE @Indicators
                SET 
			        Rate = ISNULL(@ll_tasa_operacion, 0),		--tasa
			        Commission = ISNULL(@ll_monto_comision, 0),	--comisión
			        Expense = ISNULL(@ll_gastos_operacion, 0)	--gastos
		        WHERE 
			        numero_operacion = @ll_numero_operacion
				
                SELECT @ll_count = @ll_count + 1
            END


            --Select de tabla de salida
            SELECT 
		        InitialDate as InitialDateRegister,
                StateClient as ClientState,
                CurrentSluggish as CurrentClientDebt,
                AmountApproved as ApprovedAmount,
                Factoring as FactoringAmount,
                LineBusyPercentage as BusyLinePercentage,
                NumberOfOperations,
                ReoperationsPercentage,
                CustomerPaymentPercentage as ClientPaymentPercentage,
                Recency,
                NumberOfDebtors,
                CurrentDebt,
                Protest as ProtestedDocuments,
                Credit as DelinquentCredits
            FROM @ClientData


            --Select de tabla de salida
            SELECT  
		        Rate,
		        Commission,
		        Expense,
		        fecha_operacion as OperationDate,
		        producto as ProductCode,
                monto_operacion as OperationAmount
            FROM @Indicators


            --Select indicadores de porcentaje
            SELECT
		        ISNULL(porcen_Cancelado_Cli, 0) as PaidAmountPercentageToCli,
	            ISNULL(porcen_Cancelado_Deu, 0) as PaidAmountPercentageToDebtor,
	            ISNULL(porcen_Cancelado_Ope, 0) as PaidAmountPercentageByOpe
            FROM @Indicators_porcentaje


            SELECT TOP 5 
		        rut_deu AS RUT,
		        LTRIM(RTRIM(deudor)) AS Name,
		        cod_dcto AS DocumentCode,
		        tipo AS DocumentType,
		        saldo_deu AS DebtorBalance,
		        saldo_cli AS ClientBalance,
		        monto_total AS TotalAmount,
		        porcentaje AS Percentage,
		        cruce AS Crossing
            FROM @DataDebtors
            ORDER BY saldo_deu DESC
            ";

            object param = new { rut };
            (string, object) result = (query, param);
            return result;
        }
    }
}