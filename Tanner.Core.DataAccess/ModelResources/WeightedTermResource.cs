using System;
using System.Collections.Generic;
using System.Text;
using Tanner.Core.DataAccess.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the weighted term
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa el pago ponderado 
    /// </summary>
    public class WeightedTermResource
    {
        /// <summary>
        /// Standard weighted term
        /// </summary>
        /// <summary xml:lang="es">
        /// Plazo ponderado norma
        /// </summary>
        public decimal Operations { get; set; }

        /// <summary>
        /// Standard reoperation term
        /// </summary>
        /// <summary xml:lang="es">
        /// Plazo ponderado reoperación
        /// </summary>
        public decimal Reoperations { get; set; }

        public static (string, object) Query_WeightedTermByRUT(string rut)
        {
            var query = $@"
                SELECT 
	                PlazoPondNorma as Operations,
	                PlazoPondReop as Reoperations
                FROM INE_MODELOS.Perfil_Cliente
                WHERE RUT_CLIENTE = @{nameof(rut)}
                union all
                SELECT 
	                NumDeudoresNorma,
	                NumDeudoresReop
                FROM INE_MODELOS.Perfil_Cliente
                WHERE RUT_CLIENTE = @{nameof(rut)}
                union all
                SELECT 
	                MinTasaNorma,
	                MaxTasaReop
                FROM INE_MODELOS.Perfil_Cliente
                WHERE RUT_CLIENTE = @{nameof(rut)}
                union all
                SELECT 
	                TasaPPNorma,
	                TasaPPReop
                FROM INE_MODELOS.Perfil_Cliente
                WHERE RUT_CLIENTE = @{nameof(rut)}
                union all
                SELECT 
	                MaxTasaNorma,
	                MinTasaReop
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
