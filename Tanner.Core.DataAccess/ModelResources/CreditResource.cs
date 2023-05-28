using System;
using System.Collections.Generic;
using System.Text;
using Tanner.Core.DataAccess.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the credits of client
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa los créditos del cliente
    /// </summary>
    public class CreditResource
    {
        /// <summary>
        /// Credit operations
        /// </summary>
        /// <summary xml:lang="es">
        /// Operaciones de crédito
        /// </summary>
        public int CreditOperations { get; set; }

        /// <summary>
        /// Weighted Term
        /// </summary>
        /// <summary xml:lang="es">
        /// Plazo ponderado
        /// </summary>
        public decimal WeightedTerm { get; set; }

        /// <summary>
        /// Minimun credit Rate 
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa mínima de crédito
        /// </summary>
        public decimal MinimunRate { get; set; }

        /// <summary>
        /// TPP credit
        /// </summary>
        /// <summary xml:lang="es">
        /// TPP crédito
        /// </summary>
        public decimal TPP { get; set; }

        /// <summary>
        /// Maximun credit rate 
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa máxima de crédito
        /// </summary>
        public decimal MaximunRate { get; set; }

        public static (string, object) Query_CreditDetailByRUT(string rut)
        {
            var query = $@"
                SELECT 
	                ope_credito as CreditOperations,
	                Plazo_PP_credito as WeightedTerm,
	                MinTasa_credito as MinimunRate,
	                TPP_credito as TPP,
	                MaxTasa_credito as MaximunRate
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
