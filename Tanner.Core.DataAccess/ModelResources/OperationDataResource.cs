using System;
using System.Collections.Generic;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Operation data resource
    /// </summary>
    ///<summary xml:lang="es">
    /// Recurso para los datos de la operación
    /// </summary>
    public class OperationDataResource
    {
        /// <summary>
        /// Operation Number
        /// </summary>
        ///<summary xml:lang="es">
        /// Número de la operación
        /// </summary>
        public int OperationNumber { get; set; }

        /// <summary>
        /// Name of client
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre del cliente
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// RUT of client
        /// </summary>
        ///<summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        public string ClientRUT { get; set; }

        /// <summary>
        /// Operation Amount
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto de la operación
        /// </summary>
        public float OperationAmount { get; set; }

        /// <summary>
        /// OperationType
        /// </summary>
        ///<summary xml:lang="es">
        /// Tipo de operación
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// Ratify User
        /// </summary>
        ///<summary xml:lang="es">
        /// Usuario de ratificación
        /// </summary>
        public string RatifyUser { get; set; }

        /// <summary>
        /// Operation source
        /// </summary>
        ///<summary xml:lang="es">
        /// Origen operacion
        /// </summary>
        public string OperationSource { get; set; }

        /// <summary>
        /// Operation date
        /// </summary>
        ///<summary xml:lang="es">
        /// Fecha operacion
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Resource for Documents of operation
        /// </summary>
        ///<summary xml:lang="es">
        /// Recurso para guardar el o los documentos por operación
        /// </summary>
        public IEnumerable<DocumentDataResource> Documents { get; set; }

        /// <summary>
        /// Query to get documents data of a operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Consulta para obtener los documentos de una operación
        /// </summary>
        public static (string, object) Query_DataDocumentByOperation(int number)
        {
            var query = $@"
                DECLARE @Operation TABLE
                (
                    OperationNumber         INT,
                    ClientName              VARCHAR(128),
                    ClientRut               VARCHAR(10),
                    OperationAmount         BIGINT,
                    OperationType           VARCHAR(2),
                    ClientCode              INT,
                    RatifyUser              VARCHAR(15),
					OperationSource			VARCHAR(4),
                    OperationDate           DATETIME
                )
                DECLARE @Documents TABLE
                (
                    DocumentID                      INT,
                    DocumentNumber                  DECIMAL,
                    DocumentAmount                  BIGINT,
                    DebtorName                      VARCHAR(128),
                    DebtorRut                       VARCHAR(10),
                    DocumentType                    INT,
                    ReceptionDateSII                DATE,
                    AmountNumberTransactionsDebtor  INT,
                    DebtorClientCode                INT,
                    DebtorPaymentQuantity           INT,
                    DebtorPaymentTotal              DECIMAL(18,4),
                    RatifyStatus                    INT,
					EconomicActivity				VARCHAR(100),
					EconomicActivityNumber			INT,
                    CommercialBusiness              VARCHAR(64),
                    DocumentTypeSII                 INT,
                    RatifyObservation               VARCHAR(255),
                    IssuanceDate                    DATE
                )
                DECLARE @Payments TABLE
                (
                    DebtorClientCode                INT,
                    DebtorPaymentQuantity           INT,
                    DebtorPaymentTotal              DECIMAL(18,4)
                )
                DECLARE @TotalOperation INT, @TotalOperation6Months INT, @TotalOperation9Months INT, @MaximumOperationAmount BIGINT
				--GLC
				DECLARE @amortizado_cli   numeric(18,2)
						,@amortizado_deu   numeric(18,2)
						,@amortizado_ope   numeric(18,2)
						,@amortizado_total numeric(18,2)
						,@porcentaje_deudor		numeric(18,2) 
						,@codigo_cliente	int
				--
                IF EXISTS(SELECT 1 FROM dba.tb_fin17 WITH (NOLOCK) WHERE numero_operacion = @{nameof(number)} AND estado_operacion IN (0, 1, 2))
                BEGIN
                    INSERT INTO @Operation (OperationNumber, ClientName, ClientRut, OperationAmount, OperationType, ClientCode, RatifyUser, OperationSource, OperationDate)
                        SELECT DISTINCT
                            @{nameof(number)},
                            LTRIM(RTRIM(ISNULL(cli.razon_social,cli.nombre_cliente))),
                            Replace(LTrim(Replace(per.rut_persona, '0', ' ')), ' ', 0),
                            op.valor_nominal_operacion,
                            LTRIM(RTRIM(op.tipo_operacion)),
                            op.codigo_cliente,
                            rat.ratificador,
							cre.origen,
                            op.fecha_operacion
                        FROM
                            dba.tb_fin17 op WITH (NOLOCK)
                            JOIN
                                dba.tb_fin01 cli WITH (NOLOCK) ON op.codigo_cliente = cli.codigo_cliente
                            JOIN
                                dba.tb_fin41 per WITH (NOLOCK) ON cli.codigo_persona = per.codigo_persona
                            LEFT JOIN
                                dbo.ficha_ratificacion rat WITH (NOLOCK) ON op.numero_operacion = rat.numero_operacion
							LEFT JOIN
								dbo.credito cre WITH (NOLOCK) ON op.numero_operacion = cre.num_op
                        WHERE
                            op.numero_operacion = @{nameof(number)} AND
                            op.estado_operacion IN (0, 1, 2)
                    IF EXISTS(SELECT 1 FROM @Operation)
                        BEGIN
                            SELECT
                                @TotalOperation = COUNT(1)
                            FROM
                                dba.tb_fin17 op WITH (NOLOCK)
                                JOIN
                                    dbo.log_evaluacion le WITH (NOLOCK) ON op.numero_operacion = le.num_oper AND le.cod_accion = 20
                                JOIN
                                    @Operation ON op.codigo_cliente = ClientCode
                            WHERE
                                op.estado_operacion IN (2, 3)
                            SELECT
                                @TotalOperation9Months = ISNULL(SUM(t9.t),0)
                            FROM 
                                (SELECT
                                    1 AS t
                                FROM
                                    dba.tb_fin17 op WITH (NOLOCK)
                                    JOIN
                                        dbo.log_evaluacion le WITH (NOLOCK) ON op.numero_operacion = le.num_oper AND le.cod_accion = 20
                                    JOIN
                                        @Operation ON op.codigo_cliente = ClientCode
                                WHERE
                                    op.estado_operacion IN(2, 3) AND
                                    op.fecha_operacion > DATEADD(m,-9,getdate())
                                GROUP BY 
                                    MONTH(op.fecha_operacion)
                                ) t9
                            SELECT
                                @TotalOperation6Months = ISNULL(SUM(t6.t),0)
                            FROM 
                                (SELECT
                                    1 AS t
                                FROM
                                    dba.tb_fin17 op WITH (NOLOCK)
                                    JOIN
                                        dbo.log_evaluacion le WITH (NOLOCK) ON op.numero_operacion = le.num_oper AND le.cod_accion = 20
                                    JOIN
                                        @Operation ON op.codigo_cliente = ClientCode
                                WHERE
                                    op.estado_operacion IN(2, 3) AND
                                    op.fecha_operacion > DATEADD(m,-6,getdate())
                                GROUP BY 
                                    MONTH(op.fecha_operacion)
                                ) t6
    
                            SELECT
                                @MaximumOperationAmount = ISNULL(MAX(op.valor_nominal_operacion),0)
                            FROM
                                dba.tb_fin17 op WITH (NOLOCK)
                                JOIN
                                    dbo.log_evaluacion le WITH (NOLOCK) ON (op.numero_operacion = le.num_oper AND le.cod_accion = 20) --20: Instrucción de giro a tesorería
                                JOIN
                                    @Operation ON op.codigo_cliente = ClientCode
                            WHERE   
                                op.estado_operacion IN(2, 3)
							
						-- GLC
						select @codigo_cliente = ClientCode from @Operation
						--PRINT CONVERT(CHAR(30), @codigo_cliente)
						select  @amortizado_cli  = sum( case apli.ind_pago_cli_deu  when 'C' 
																then ( case when apli.origen_fondo <> 1 
																		then a.abono_capital + a.abono_interes 
																		else 0 
																		end )
																else 0 
															end )
									,@amortizado_deu  = sum( case apli.ind_pago_cli_deu  when 'D' then a.abono_capital + a.abono_interes  else 0 end )
									,@amortizado_ope  = sum( case when apli.origen_fondo = 1 then a.abono_capital + a.abono_interes else 0 end )
							from dba.tb_fin93 a with(nolock)
							inner join dba.tb_fin24 d  with(nolock)   on d.id_documento = a.id_documento
							inner join dba.tb_fin69 apli with(nolock) on apli.id_detalle_aplicacion = a.id_abono_documento
																		and apli.tipo_deuda = 1
							inner join dba.tb_fin17 op with(nolock) on op.numero_operacion = d.numero_operacion  									 	 	
							where d.codigo_cliente = @codigo_cliente
								and (( a.estado_abono = 1 and apli.ind_pago_cli_deu = 'D') or apli.ind_pago_cli_deu  = 'C')   
								AND a.estado_abono = 1
  								AND op.tipo_documento in (1,3,8,9,12,17,18,23,25,30,31,32,38,47,49,54,2,59,60,65)
							--
							select @amortizado_total = isnull(@amortizado_cli,0) + isnull(@amortizado_deu,0) + isnull(@amortizado_ope,0)
							--

							if @amortizado_total = 0
							select @porcentaje_deudor = 0
							else
							select @porcentaje_deudor = convert(numeric(18,2),isnull(@amortizado_deu,0) / ( @amortizado_total) * 100,2)
                            --
                            INSERT INTO 
                                @Payments (DebtorClientCode, DebtorPaymentQuantity, DebtorPaymentTotal)
							SELECT
                                docu.codigo_tercero,
                                COUNT(*) AS cantidad,
                                SUM(ISNULL(abo.monto_abono,0)) AS total
                            FROM 
                                dba.tb_fin93 abo WITH (NOLOCK)
                                INNER JOIN 
                                    dba.tb_fin24 docu WITH (NOLOCK) ON (abo.id_documento = docu.id_documento)
                                INNER JOIN 
                                    dba.tb_fin69 apli WITH (NOLOCK) ON (abo.id_abono_documento = apli.id_detalle_aplicacion)
                            WHERE
                                apli.ind_pago_cli_deu = 'D'                                
                                AND abo.fecha_abono BETWEEN DATEADD(MONTH,-12,GETDATE()) AND GETDATE()
                            GROUP BY
                                docu.codigo_tercero
                            
                            INSERT INTO 
                                @Documents(DocumentID, DocumentNumber, DocumentAmount, DebtorName, DebtorRut, DocumentType, ReceptionDateSII, DebtorClientCode, DebtorPaymentQuantity, DebtorPaymentTotal, RatifyStatus, EconomicActivity, EconomicActivityNumber, CommercialBusiness, DocumentTypeSII, RatifyObservation, IssuanceDate)
                            SELECT
                                doc.id_documento,
                                LTRIM(RTRIM(doc.numero_documento)),
                                doc.valor_nominal_documento,
                                LTRIM(RTRIM(ISNULL(deu.razon_social,deu.nombre_tercero))),
                                Replace(LTrim(Replace(per.rut_persona, '0', ' ')), ' ', 0),
                                doc.tipo_documento,
                                DOC.fecha_recep_sii,
                                cli.codigo_cliente,
                                ISNULL(abono.DebtorPaymentQuantity,0),
                                ISNULL(abono.DebtorPaymentTotal,0),
                                ISNULL(rat.estado_confirmacion,0),
								ISNULL(ac.des_act_eco,''),
								ISNULL(per.actividad_economica,0),
                                ISNULL(gc.desc_giro,''),
                                ISNULL(doc.tipo_dcto_sii,0),
                                ISNULL(rat.observacion,''),
                                doc.fecha_emision
                            FROM
                                dba.tb_fin24 doc WITH (NOLOCK)
                                JOIN
                                    dba.tb_fin08 deu WITH (NOLOCK) ON doc.codigo_tercero = deu.codigo_tercero
                                JOIN
                                    dba.tb_fin41 per WITH (NOLOCK) ON deu.codigo_persona = per.codigo_persona
                                LEFT JOIN
                                    dba.tb_fin01 cli WITH (NOLOCK) ON per.codigo_persona = cli.codigo_persona
                                JOIN
                                    dba.tb_fin17 op WITH (NOLOCK) ON doc.numero_operacion = op.numero_operacion
                                LEFT JOIN 
                                    @Payments AS abono ON abono.DebtorClientCode = doc.codigo_tercero
                                LEFT JOIN 
                                    dba.tb_fin77 rat WITH (NOLOCK) ON doc.id_documento = rat.id_documento
                                LEFT JOIN
                                    dbo.actividad_economica ac WITH (NOLOCK) ON per.actividad_economica = ac.cod_act_eco
                                LEFT JOIN
                                    dbo.giro_comercial gc WITH (NOLOCK) ON deu.cod_giro = gc.cod_giro
                            WHERE
                                op.numero_operacion = @{nameof(number)} AND
                                op.estado_operacion IN (0, 1, 2) AND
                                doc.estado_documento IN (0, 1, 2)
                            UPDATE doc
                                SET AmountNumberTransactionsDebtor = ISNULL(temp.total,0)
                            FROM
                                @Documents doc
                                LEFT JOIN
                                    (SELECT
                                        doctemp.DocumentID,
                                        COUNT(1) AS total
                                    FROM
                                        dba.tb_fin17 op WITH (NOLOCK)
                                        JOIN
                                            dbo.log_evaluacion le WITH (NOLOCK) ON op.numero_operacion = le.num_oper AND le.cod_accion = 20
                                        JOIN
                                            @Documents doctemp ON op.codigo_cliente = doctemp.DebtorClientCode
                                    WHERE
                                        op.estado_operacion IN (2, 3)
                                    GROUP BY 
                                        doctemp.DocumentID
                                    ) temp
                                    ON temp.DocumentID = doc.DocumentID
                        END
                END
                IF NOT EXISTS(SELECT 1 FROM @Documents)
                    BEGIN
                        DELETE FROM @Operation
                    END
                SELECT 
                    OperationNumber, 
                    ClientName, 
                    ClientRut, 
                    OperationAmount, 
                    OperationType, 
                    RatifyUser,
					OperationSource,
                    OperationDate
                FROM 
                    @Operation
                SELECT
                    DocumentID, 
                    DocumentNumber, 
                    DocumentAmount, 
                    DebtorName,
                    DebtorRut, 
                    DocumentType,
                    ReceptionDateSII, 
                    AmountNumberTransactionsDebtor, 
                    DebtorPaymentQuantity, 
                    DebtorPaymentTotal, 
                    RatifyStatus,
					EconomicActivity,
					EconomicActivityNumber,
                    CommercialBusiness,
                    DocumentTypeSII,
                    RatifyObservation,
                    IssuanceDate
                FROM 
                    @Documents
                SELECT @TotalOperation AS ClientTotalOperations
                SELECT @TotalOperation6Months AS ClientTotalOperations6Months
                SELECT @TotalOperation9Months AS ClientTotalOperation9Months
                SELECT @MaximumOperationAmount AS MaximumOperationAmount
				SELECT @porcentaje_deudor AS PaymentPercentage
            ";

            object param = new { number };
                (string, object) result = (query, param);
                return result;

        }

        /// <summary>
        /// Query to validate if operation exist 
        /// </summary>
        /// <summary xml:lang="es">
        /// Consulta para validar si la operación existe
        /// </summary>
        public static (string, object) Query_ExistOperation(int number)
        {
            var query = $@"
                select top 1
                    case
                    when COUNT(*) = 1 then 'true'
                    else 'false'
                    end  exist
                from dba.tb_fin17 where numero_operacion = @{nameof(number)}
                ";
            var param = new
            {
                number
            };
            (string, object) result = (query, param);
            return result;
        }

        /// <summary>
        /// Query to validate if address exist 
        /// </summary>
        /// <summary xml:lang="es">
        /// Validar si la dirección existe
        /// </summary>
        public static (string, object) Query_ExistAddress(decimal number)
        {
            var query = $@"
                select top 1
                    case
                    when COUNT(*) = 1 then 'true'
                    else 'false'
                    end  exist
                from dba.tb_fin05  where id_direccion = @{nameof(number)}
                ";
            var param = new
            {
                number
            };
            (string, object) result = (query, param);
            return result;
        }
    }
}
