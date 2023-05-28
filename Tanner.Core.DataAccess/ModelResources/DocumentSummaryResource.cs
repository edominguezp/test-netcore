namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the summary of documents by operation
    /// </summary>s
    ///<summary xml:lang="es">
    /// Clase que representa el total de los documentos por operación
    /// </summary>
    public class DocumentSummaryResource
    {

        /// <summary>
        /// Debtor RUT
        /// </summary>
        ///<summary xml:lang="es">
        /// RUT del deudor
        /// </summary>
        public string DebtorRUT { get; set; }

        /// <summary>
        /// Debtor Name
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre del deudor
        /// </summary>
        public string DebtorName { get; set; }

        /// <summary>
        /// Total documents per debtor
        /// </summary>s
        ///<summary xml:lang="es">
        /// Total de documentos por deudor
        /// </summary>
        public int NumberOfDocuments { get; set; }

        /// <summary>
        /// Amount payable from the debtor's documents in the operation
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto a pagar de los documentos del deudor en la operación
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Percentage of the document with respect to the total
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje del documento respecto al total
        /// </summary>
        public decimal PercentageDocument { get; set; }

        /// <summary>
        /// Percentage of documents less eight days
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje de documentos menor a ocho días
        /// </summary>
        public decimal PercentageLessEightDays { get; set; }

        /// <summary>
        /// Percentage of documents greater eight days
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje de documentos mayor a ocho días
        /// </summary>
        public decimal PercentageGreaterEightDays { get; set; }

        /// <summary>
        /// Invoice with minimun days 
        /// </summary>
        ///<summary xml:lang="es">
        /// Factura con plazo mínimo de días
        /// </summary>
        public decimal MininumTerm { get; set; }

        /// <summary>
        /// Invoice with maximum days 
        /// </summary>
        ///<summary xml:lang="es">
        /// Factura con plazo máximo de días
        /// </summary>
        public decimal MaximumTerm { get; set; }

        /// <summary>
        /// Weighted Term
        /// </summary>
        ///<summary xml:lang="es">
        /// Plazo ponderado
        /// </summary>
        public decimal WeightedTerm { get; set; }

        /// <summary>
        /// Query that represent the documents by operation number
        /// </summary>
        ///<summary xml:lang="es">
        /// Consulta que representa los documentos por número de operación
        /// </summary>
        public static (string, object) Query_SummaryDocuments(int number)
        {
            var query = $@"
               DECLARE @num_oper					bigint
                DECLARE @ll_totoperacion			bigint
				DECLARE @ll_totvalor_nominal_dcto  bigint
                DECLARE @ll_count					int
                DECLARE @ll_count_tabla				int
                DECLARE @rut_deudor					varchar(20)
                DECLARE @ll_dias_min				int
                DECLARE @ll_dias_max				int
                
                --SETeamos variables de trabajo
                SET @num_oper	= @{nameof(number)} --575145--574460
                --declaramos tablas de trabajo
                DECLARE @t table
                    ( 
		                id					int IDENTITY(1,1) NOT NULL Primary key,	
		                numero_documento	bigint,
		                valor_documento		bigint,
		                porcentaje			numeric(20, 4),  
		                DebtorRut			varchar (20),
		                debtorName			varchar (50),
		                fecha_emision_docto	datetime,
		                fecha_vcto_docto	datetime,
		                dias_transcurridos	int,
		                menor_8				numeric(20, 2),
		                mayor_8				numeric(20, 2),
						plazo_ponderado		bigint
                    )

                --declarasmos tabla de salida
                DECLARE @tabla_salida table
                    ( 
		                --id							int IDENTITY(1,1) NOT NULL Primary key,	
		                DebtorRUT							varchar (20),
		                DebtorName						varchar (50),
		                NumberOfDocuments			bigint,
		                TotalDocuments				bigint,
		                PercentageDocument			numeric(20, 2),  
		                PercentageLessEightDays		numeric(20, 2),  
		                PercentageGreaterEightDays	numeric(20, 2),  
		                MininumTerm					numeric(20, 2),
		                MaximumTerm					numeric(20, 2),
		                plazo_ponderado				bigint
	                )
                DECLARE @tabla_deudores table
                    ( 
		                id							int IDENTITY(1,1) NOT NULL Primary key,	
		                DebtorRut					varchar (20)
	                )

	                select @ll_totoperacion = isnull(o.valor_futuro_operacion,0)
	                FROM dba.tb_fin17 o with (nolock)
	                where o.numero_operacion = @num_oper 
				    
					--select @ll_totoperacion
					select @ll_totvalor_nominal_dcto = SUM( d.valor_nominal_documento )
					FROM dba.tb_fin24 d with (nolock)
					 WHERE d.numero_operacion = @num_oper
					AND d.estado_documento not in (7,8,9)
					---
					insert into @t
	                select d.id_documento,
		                   d.valor_nominal_documento,
		                   (d.valor_nominal_documento*100)/(@ll_totvalor_nominal_dcto) as porcen,
		                   Replace(LTrim(Replace(per.rut_persona, '0', ' ')), ' ', 0),
		                   LTRIM(RTRIM(ISNULL(deu.razon_social,deu.nombre_tercero))),
		                   d.fecha_emision,
		                   d.fecha_vencimiento_documento,
		                   DATEDIFF(DD,d.fecha_emision, GETDATE()),
		                   case 
				                when DATEDIFF(DD,d.fecha_emision, GETDATE()) < 8 then d.valor_nominal_documento
				                else 0
			                end,
                            case 
				                when DATEDIFF(DD,d.fecha_emision, GETDATE()) >= 8 then d.valor_nominal_documento
				                else 0
			                end,
							(d.valor_nominal_documento * DATEDIFF(DD,d.fecha_emision, GETDATE())) / @ll_totoperacion
					  FROM dba.tb_fin24 d with (nolock)
					    INNER JOIN dba.tb_fin08 deu WITH (NOLOCK) ON d.codigo_tercero = deu.codigo_tercero
			            INNER JOIN dba.tb_fin41 per WITH (NOLOCK) ON deu.codigo_persona = per.codigo_persona
					WHERE d.numero_operacion = @num_oper 
					AND d.estado_documento not in (7,8,9)
				
				--select * from @t

				insert into @tabla_salida
                select DebtorRut,
	                   debtorname,
	                   count(1),
	                   sum(valor_documento),
	                   sum(porcentaje),
					   round((sum(menor_8)*100) / @ll_totoperacion,0),
					   round((sum(mayor_8)*100) / @ll_totoperacion,0),
	                   0,
	                   0,
	                   sum(plazo_ponderado)
                FROM @t
                group by DebtorRut,
	                     debtorname
                order by DebtorRut

                insert into @tabla_deudores
                select DebtorRut
                FROM @t
                group by DebtorRut
                order by DebtorRut

                --Contamos registros de la tabla temporal
                SELECT @ll_count = 1

                SELECT @ll_count_tabla = COUNT(1) 
                FROM @tabla_deudores

                --Realizamos ciclo para generar el nivel de firma
	                WHILE @ll_count <= @ll_count_tabla
	                BEGIN
		                --limpiamos la variable del deudor
		                SET @rut_deudor = ''
		                SET @ll_dias_min= 0
		                SET @ll_dias_max = 0

		                select @rut_deudor = DebtorRut
		                 FROM @tabla_deudores
		                where id = @ll_count

		                select @ll_dias_min = min(dias_transcurridos),
			                   @ll_dias_max = max(dias_transcurridos) 	 
		                FROM @t 
		                where DebtorRut = @rut_deudor

		                if @ll_dias_min = @ll_dias_max
		                begin
			                SET @ll_dias_min = 0
		                end
	
		                update @tabla_salida
		                  SET	MininumTerm	= @ll_dias_min,
				                MaximumTerm   = @ll_dias_max
		                where DebtorRUT = @rut_deudor
		
		                SELECT @ll_count = @ll_count + 1
	                END--cerramos ciclo del WHILE @ll_count <= @ll_count_tabla

				select  DebtorRUT, -- Rut Deudor
		                DebtorName, -- Deudor
		                NumberOfDocuments, -- N° de Doc(s)
		                TotalDocuments as TotalAmount, -- Monto Doc(s)
		                PercentageDocument,  -- % Doc(s)
		                PercentageLessEightDays,  -- % menor 8 días 
		                PercentageGreaterEightDays, --  % mayor 8 días
		                MininumTerm,-- Plazo Mín
		                MaximumTerm, -- Plazo Max
		                plazo_ponderado as WeightedTerm  --Plazo Ponderado
                FROM @tabla_salida
				order by TotalDocuments desc
                    ";
            var parameters = new
            {
                number
            };
            (string, object) result = (query, parameters);
            return result;
        }
    }
}
