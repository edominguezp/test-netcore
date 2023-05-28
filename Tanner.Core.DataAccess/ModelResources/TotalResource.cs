using System;
using System.Collections.Generic;
using System.Text;
using Tanner.Core.DataAccess.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the total of resource
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa el total de los documentos
    /// </summary>
    public class TotalResource
    {
        /// <summary>
        /// Resource for rates of client
        /// </summary>
        ///<summary xml:lang="es">
        /// Recurso para guardar la o las tasas del cliente
        /// </summary>
        public RateResource Rate { get; set; }

        /// <summary>
        /// Resource for amount of client
        /// </summary>
        ///<summary xml:lang="es">
        /// Recurso para guardar el o los montos del cliente
        /// </summary>
        public AmountResource Amount { get; set; }

        /// <summary>
        /// Resource for percentage of client
        /// </summary>
        ///<summary xml:lang="es">
        /// Recurso para guardar el o los porcentajes de los clientes
        /// </summary>
        public PercentageResource Percentage { get; set; }

        /// <summary>
        /// Resource for collection of client
        /// </summary>
        ///<summary xml:lang="es">
        /// Recurso para guardar el o los montos a pagar
        /// </summary>
        public IEnumerable<CollectionResource> Collection { get; set; }
        
        /// <summary>
        /// Resource for payments of client
        /// </summary>
        ///<summary xml:lang="es">
        /// Recurso para guardar el o los pagos del cliente
        /// </summary>
        public IEnumerable<PaymentResource> Payment { get; set; }

        public static (string, object) Query_TotalPaymentsByRUT(string rut)
        {
            var query = $@"
                SELECT
	                TasaMPP as WeightedSluggishRate,
	                TasaMoraMax as MaximumSluggishRate,
	                DiasMoraMax as MaximumSluggishDays,
	                DiasMoraMPP as SluggishWeightdDays
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}

                    SELECT
	                Monto_Pago as TotalAmount,
	                Monto_totalmORA as AmountSluggish,
	                Monto_Mora1_30 as AmountSluggish1to30,
	                Monto_Mora31_60 as AmountSluggish31to60,
	                Monto_Mora61 as AmountSluggish61orMore,
	                Monto_Mora_0 as AmountWithoutSluggish
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}

                    SELECT
	                PorcMontoMora as PercentageSluggish,
	                PorcMontoMora130 as PercentageSluggish1to30,
	                PorcMontoMora3160 as PercentageSluggish31to60,
	                PorcMontoMora61 as PercentageSluggish61orMore,
	                PorcMontoMora0 as PercentageWithoutSluggish
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                    -- recaudaciones
                    SELECT
	                totalRecaudado as AmountCollected,
	                100 as PercentageCollected
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                    union all
                SELECT
	                RecaudadoCli,
	                PorcRecaudadoCli
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                    union all
                    SELECT
	                RecaudadoDeu,
	                PorcRecaudadoDeu
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                    --total de pagos
                    SELECT
	                totalPagos as AmountPayments,
	                100 as percentagePayments
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                union all
                    SELECT
	                Pagos_Cliente,
	                PorcPagosCli
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                union all
                    SELECT
	                Pagos_Deudor,
	                PorcPagosDeu
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}

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
