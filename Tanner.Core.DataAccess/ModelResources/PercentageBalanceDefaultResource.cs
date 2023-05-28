using Tanner.Core.DataAccess.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the percentages balance associated a Client
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa los porcentajes de los saldos de los clientes
    /// </summary>
    public class PercentageBalanceSluggishResource
    {
        /// <summary>
        /// Percentage Balance 0 days
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje saldo 0 días
        /// </summary>
        public decimal PercentageBalance0 { get; set; }

        /// <summary>
        /// Percentage Balance 1 to 15 days
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje saldo 1 a 15 días
        /// </summary>
        public decimal PercentageBalance1to15 { get; set; }

        /// <summary>
        /// Percentage Balance 16 to 25 days
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje saldo 16 a 25 días
        /// </summary>
        public decimal PercentageBalance16to25 { get; set; }

        /// <summary>
        /// Percentage Balance 26 to 55 days
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje saldo 26 as 55 días
        /// </summary>
        public decimal PercentageBalance26to55 { get; set; }

        /// <summary>
        /// Percentage Balance 56 or more days
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje saldo 56 o más días
        /// </summary>
        public decimal PercentageBalance56OrMore { get; set; }

        /// <summary>
        /// Percentage Balance 0 credits days
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje saldo 0 días créditos 
        /// </summary>
        public decimal PercentageBalance0Credit { get; set; }

        /// <summary>
        /// Percentage Balance 1 to 15 credits days
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje saldo 1 a 15 días créditos
        /// </summary>
        public decimal PercentageBalance1to15Credit { get; set; }

        /// <summary>
        /// Percentage Balance 16 to 25 days credits
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje saldo 16 a 25 días créditos
        /// </summary>
        public decimal PercentageBalance16to25Credit { get; set; }

        /// <summary>
        /// Percentage Balance 26 to 55 days credits
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje saldo 26 a 55 días créditos
        /// </summary>
        public decimal PercentageBalance26to55Credit { get; set; }

        /// <summary>
        /// Percentage Balance 26 to 55 days credits
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje saldo 26 a 55 días créditos
        /// </summary>
        public decimal PercentageBalance56OrMoreCredit { get; set; }

        public static (string, object) Query_PercentageDetailByRUT(string rut)
        {
            var query = $@"
                SELECT

	                  PorcSSaldo_Mora_0FACT AS PercentageBalance0,
                      PorcSSaldo_Mora1_15FACT AS PercentageBalance1to15,
                      PorcSSaldo_Mora16_25FACT AS PercentageBalance16to25,
                      PorcSSaldo_Mora26_55FACT AS PercentageBalance26to55,
                      PorcSSaldo_Mora56FACT AS PercentageBalance56OrMore,
                      PorcSSaldo_Mora_0CRED AS PercentageBalance0Credit,
                      PorcSSaldo_Mora1_15CRED AS PercentageBalance1to15Credit,
                      PorcSSaldo_Mora16_25CRED AS PercentageBalance16to25Credit,
                      PorcSSaldo_Mora26_55CRED AS PercentageBalance26to55Credit,
                      PorcSSaldo_Mora56CRED AS PercentageBalance56OrMoreCredit

                  FROM INE_MODELOS.Perfil_Cliente
                  WHERE RUT_CLIENTE =  @{nameof(rut)}
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

