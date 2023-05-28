using System.Collections.Generic;
using Tanner.Core.DataAccess.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the last operations by client
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa las últimas operaciones por cliente
    /// </summary>
    public class LastOperationsResource
    {
        /// <summary>
        /// Normal Recency
        /// </summary>
        /// <summary xml:lang="es">
        /// Recencia operaciones normales
        /// </summary>
        public int? NormalRecency { get; set; }

        /// <summary>
        /// Resource for normal operations
        /// </summary>
        ///<summary xml:lang="es">
        /// Recurso para guardar el o las operaciones normales
        /// </summary>
        public IEnumerable<NormalOperationsResource> NormalOperation { get; set; }

        /// <summary>
        /// Reoperation Recency
        /// </summary>
        /// <summary xml:lang="es">
        /// Recencia de reoperaciones
        /// </summary>
        public int? ReoperationRecency { get; set; }

        /// <summary>
        /// Resource for reoperations
        /// </summary>
        ///<summary xml:lang="es">
        /// Recurso para guardar el o las reoperaciones
        /// </summary>
        public IEnumerable<ReoperationResource> Reoperation { get; set; }

        /// <summary>
        /// Credit Recency
        /// </summary>
        /// <summary xml:lang="es">
        /// Recencia de créditos
        /// </summary>
        public int? CreditRecency { get; set; }

        //LastCreditRecency
        public IEnumerable<LastCreditResource> LastCredit { get; set; }


        public static (string, object) Query_LastOperationsByRUT(string rut)
        {
            var query = $@"
                SELECT ultant1Normal_Recencia as NormalRecency
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                    -- ultimas operaciones normales
                SELECT 
	                UltNormal_FECHA_APERTURA AS NormalDate,
	                UltNormal_PROD AS NormalCode,
	                UltNormal_Monto as NormalAmount,
	                UltNormal_tasa AS NormalRate,
	                UltNormal_Comision AS NormalCommission,
	                UltNormal_Gastos AS NormalExpenses
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                    union all
                    SELECT 
	                ultant1Normal_FECHA_APERTURA,
	                ultant1Normal_PROD,
	                ultant1Normal_Monto,
	                ultant1Normal_tasa,
	                ultant1Normal_Comision,
	                ultant1Normal_Gastos
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                    union all
                    SELECT 
	                ultant2Normal_FECHA_APERTURA,
	                ultant2Normal_PROD,
	                ultant2Normal_Monto,
	                ultant2Normal_tasa,
	                ultant2Normal_Comision,
	                Ultant2Normal_Gastos
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}

                    -- recencia reoperaciones
                    SELECT 
	                Ultant1Reop_Recencia as ReoperationRecency
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                    -- ultimas reoperaciones 
                    SELECT 
	                UltReop_FECHA_APERTURA as ReoperationDate,
	                UltReop_PROD AS ReoperationProd,
	                UltReop_Monto AS ReoperationAmount,
	                UltReop_tasa AS ReoperationRate,
	                UltReop_Comision AS ReoperationCommission,
	                UltReop_Gastos AS ReoperationExpenses
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                    union all
                    SELECT 
	                Ultant1Reop_FECHA_APERTURA,
	                Ultant1Reop_PROD,
	                Ultant1Reop_Monto,
	                Ultant1Reop_tasa,
	                Ultant1Reop_Comision,
	                Ultant1Reop_Gastos
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                    union all
                    SELECT 
	                Ultant2Reop_FECHA_APERTURA,
	                Ultant2Reop_PROD,
	                Ultant2Reop_Monto,
	                Ultant2Reop_tasa,
	                Ultant2Reop_Comision,
	                Ultant2Reop_Gastos
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                    --recencia credito
                    SELECT 
	                UltcreditoRecencia as CreditRecency
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                    --ultimos creditos
                    SELECT 
	                UltcreditoMonto AS CreditAmount,
	                UltcreditoTasa AS CreditRate
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                    union all
                    SELECT 
	                UltcreditoMonto2,
	                UltcreditoTasa2 
                    FROM INE_MODELOS.Perfil_Cliente
                    WHERE RUT_CLIENTE = @{nameof(rut)}
                    union all
                    SELECT 
	                UltcreditoMonto3,
	                UltcreditoTasa3
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
