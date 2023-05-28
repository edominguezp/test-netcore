using Tanner.Core.DataAccess.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Get data by debtor
    /// </summary>
    /// <summary xml:lang="es">
    /// Obtener los datos del deudor
    /// </summary>
    public class DebtorDataResource
    {
        /// <summary>
        /// Social Reason
        /// </summary>
        /// <summary xml:lang="es">
        /// Razón Social
        /// </summary>
        public string SocialReason { get; set; }

        /// <summary>
        /// Debtor State
        /// </summary>
        /// <summary xml:lang="es">
        /// Estado del deudor
        /// </summary>
        public string DebtorState { get; set; }

        /// <summary>
        /// Cause blockage
        /// </summary>
        /// <summary xml:lang="es">
        /// Causa de bloqueo
        /// </summary>
        public string CauseBlockage { get; set; }

        /// <summary>
        /// Is electronic receiver
        /// </summary>
        /// <summary xml:lang="es">
        /// Verifica si es receptor electrónico
        /// </summary>
        public int ElectronicReceiver { get; set; }

        /// <summary>
        /// Debtor Payment Quantity
        /// </summary>
        /// <summary xml:lang="es">
        /// Cantidad de pagos al deudor
        /// </summary>
        public int DebtorPaymentQuantity { get; set; }

        /// <summary>
        /// Debtor Payment Total
        /// </summary>
        /// <summary xml:lang="es">
        /// Cantidad de pagos total
        /// </summary>
        public decimal DebtorPaymentTotal { get; set; }

        /// <summary>
        /// Person RUT
        /// </summary>
        ///<summary xml:lang="es">
        /// RUT de la persona
        /// </summary>
        public string PersonRUT { get; set; }

        /// <summary>
        /// Third state
        /// </summary>
        ///<summary xml:lang="es">
        /// estado tercero
        /// </summary>
        public string ThirdState { get; set; }

        /// <summary>
        /// Third code
        /// </summary>
        ///<summary xml:lang="es">
        /// Código tercero
        /// </summary>
        public int ThirdCode { get; set; }

        public static (string, object) Query_DebtorDetailByRUT(string rut)
        {
            var query = $@"
                declare @rut_deudor varchar (25)
                declare @rut_set varchar (25)

                set @rut_deudor = @rut --'79690060-1'--'76196521-2'
                set @rut_set        = ''
                SET @rut_set        = replace(@rut_deudor,'-','')
                SET @rut_set        = right( '0000000000' + @rut_set , 10 )

                SELECT
                        deu.razon_social as SocialReason,
                    case 
                        when deu.estado_tercero = 1 then 'ACTIVO'
                        when deu.estado_tercero = 2 then 'BLOQUEADO'
                        else 'BLOQUEADO'
                    end as DebtorState,
                        deu.causa_bloqueo as CauseBlockage,
                    case
                        when deu.rfx = 'N' then 0
                        when deu.rfx = 'S' then 1
                        else 0
                    end as ElectronicReceiver,
                        abono.cantidad as DebtorPaymentQuantity,
                        abono.total as DebtorPaymentTotal ,
						per.rut_persona as PersonRUT,
						deu.estado_tercero as ThirdState,
						deu.codigo_tercero as ThirdCode

                    FROM    dba.tb_fin41 per With (nolock)
                        INNER JOIN dba.tb_fin08 deu With (nolock) ON per.codigo_persona = deu.codigo_persona
                        /*Cantitad y total de abonos del deudor*/
                        LEFT JOIN
                            (SELECT
                                docu.codigo_tercero,
                                COUNT(*) AS cantidad,
                                SUM(abo.monto_abono) AS total
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
                                docu.codigo_tercero) 
                            AS abono ON abono.codigo_tercero = deu.codigo_tercero 
                    WHERE    per.rut_persona = @rut_set
                        ";
            var param = new
            {
                rut = rut.FillRUT(false)
            };
            (string, object) result = (query, param);
            return result;
        }
    }
}
