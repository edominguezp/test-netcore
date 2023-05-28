using System;
using System.Collections.Generic;
using System.Text;
using Tanner.Core.DataAccess.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the data of Factoring
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa los datos del factoring
    /// </summary>
    public class FactoringResource
    {
        /// <summary>
        /// Approved amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto aprobado
        /// </summary>
        public decimal ApprovedAmount { get; set; }

        /// <summary>
        /// Factoring amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de factoring
        /// </summary>
        public decimal FactoringAmount { get; set; }

        /// <summary>
        /// Normal Amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje disponible factoring
        /// </summary>
        public decimal NormalAmount { get; set; }

        /// <summary>
        /// Reoperation Amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto por reoperación
        /// </summary>
        public decimal ReoperationAmount { get; set; }

        /// <summary>
        /// Credit amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de crédito
        /// </summary>
        public decimal CreditAmount { get; set; }

        /// <summary>
        /// Stock amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto disponible
        /// </summary>
        public decimal StockAmount { get; set; }

        /// <summary>
        /// Percentage Stock Factoring
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje disponible factoring
        /// </summary>
        public decimal PercentageStockFact { get; set; }

        /// <summary>
        /// Percentage Normal Stock
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje normal disponible
        /// </summary>
        public decimal PercentageNormalStock { get; set; }

        /// <summary>
        /// Percentage stock Reoperation
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje de reoperación disponible
        /// </summary>
        public decimal PercentagestockReoperation { get; set; }

        /// <summary>
        /// Percentage Stock Credit
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje disponible de crédito
        /// </summary>
        public decimal PercentageStockCredit { get; set; }

        /// <summary>
        /// Percentage line bussy
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje de línea ocupado
        /// </summary>
        public decimal PercentageLineBussy { get; set; }

        public static (string, object) Query_FactoringDetailByRUT(string rut)
        {
            var query = $@"
                SELECT 
                      MONTO_APROBADO as ApprovedAmount,
	                  Stock_Factoring as FactoringAmount,
	                  Stock_Normal as NormalAmount,
	                  Stock_reop as ReoperationAmount,
	                  Stock_credito as CreditAmount,
                             Stock as StockAmount,
	                  PorcStock_Factoring as PercentageStockFact,
                             PorcStock_Normal as PercentageNormalStock,
	                  PorcStock_reop as PercentagestockReoperation,
	                  PorcStock_credito as PercentageStockCredit,
                             PorcLneaOcupado as PercentageLineBussy
                  FROM 
	                  INE_MODELOS.Perfil_Cliente
                  WHERE 
                      RUT_CLIENTE = @{nameof(rut)}
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
