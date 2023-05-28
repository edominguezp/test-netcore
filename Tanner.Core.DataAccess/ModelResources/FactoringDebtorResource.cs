using System;
using System.Collections.Generic;
using System.Text;
using Tanner.Core.DataAccess.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the detail of factoring
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa el detalle del factoring
    /// </summary>
    public class FactoringDebtorResource
    {
        /// <summary>
        /// RUT debtor
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del deudor
        /// </summary>
        public string RUTDebtor { get; set; }

        /// <summary>
        /// Debtor Name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del deudor
        /// </summary>
        public string Debtor { get; set; }

        /// <summary>
        /// Amount debtor
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto Deudor mayor
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Percentage debtor 
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje deudor mayor
        /// </summary>
        public decimal Percentage { get; set; }

        public static (string, object) Query_FactoringDebtorByRUT(string rut)
        {
            var query = $@"
                SELECT
                     Rut_Deu_Mayor as RUTDebtor,
	                 Deu_Mayor as Debtor,
	                 MontoDeuMayor as Amount,
	                 PorcDeudorMayor as Percentage
                  FROM 
	                 INE_MODELOS.Perfil_Cliente
                  WHERE 
	                 RUT_CLIENTE = @{nameof(rut)}
                  union all
                  SELECT
	                 Rut_Deu_Mayor2,
	                 Deu_Mayor2,
	                 MontoDeuMayor2,
	                 PorcDeudorMayor2
                  FROM 
	                 INE_MODELOS.Perfil_Cliente
                  WHERE 
	                 RUT_CLIENTE = @{nameof(rut)}
                  union all 
                  SELECT
	                 Rut_Deu_Mayor3,
	                 Deu_Mayor3,
	                 MontoDeuMayor3,
	                 PorcDeudorMayor3
                  FROM 
                     INE_MODELOS.Perfil_Cliente
                  WHERE 
	                 RUT_CLIENTE = @{nameof(rut)}
                  union all
                  SELECT
	                 Rut_Deu_Mayor4,
	                 Deu_Mayor4,
	                 MontoDeuMayor4,
	                 PorcDeudorMayor4
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
