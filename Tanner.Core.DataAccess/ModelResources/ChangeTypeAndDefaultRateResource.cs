using System;
using System.Collections.Generic;
using System.Text;
using Tanner.Core.DataAccess.Enums;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class ChangeTypeAndDefaultRateResource
    {
		/// <summary>
		/// Change type
		/// </summary>
		/// <summary xml:lang="es">
		/// Tipo de cambio
		/// </summary> 
		public decimal? ChangeType { get; set; }

		/// <summary>
		/// Default rate
		/// </summary>
		/// <summary xml:lang="es">
		/// Tasa mora
		/// </summary> 
		public decimal? DefaultRate { get; set; }

		/// <summary>
		/// Obtains the exchange rate and the default rate of a given date depending on the type of currency
		/// </summary>
		/// <summary xml:lang="es">
		/// Obtiene el tipo de cambio y la tasa mora de una fecha determinada dependiendo del tipo de moneda
		/// </summary> 
		/// <param name="currentType"></param>
		/// <param name="dateToConsult"></param>
		/// <returns></returns>
		public static (string, object) Query_GetChangeTypeAndDefaultRateByCurrentTypeAndDate(ChangeTypeEnum currentType, DateTime dateToConsult)
        {
            string query = @"
					declare @tabla_tasa_mora TABLE 
						(
						tp_peso	numeric(20, 4) NULL
						,tp_uf		numeric(20, 4) NULL
						,tp_dolar	numeric(20, 4) NULL
						)

						insert into @tabla_tasa_mora
						SELECT dba.tb_fin18.tp_peso,   
							   dba.tb_fin18.tp_dolar,   
							   dba.tb_fin18.tp_uf   
						FROM dba.tb_fin18 With (nolock) 
						WHERE dba.tb_fin18.dia = @dateToConsult

						--tipo de cambio
						declare @tipo_cambio T_t_factor
						if @currentType = 1
						begin
							set @tipo_cambio = 1
						end 

						if @currentType = 2
						begin
							set @tipo_cambio = (select tipo_cambio from dba.tb_fin46 With (nolock) where codigo_moneda = @currentType  and fecha_cambio = @dateToConsult)
						end 

						if @currentType = 3
						begin
							set @tipo_cambio = (select tipo_cambio from dba.tb_fin46 With (nolock) where codigo_moneda = @currentType  and fecha_cambio = @dateToConsult)
						end 

						/*
						1	PESO                                                                                                
						2	UNIDAD DE FOMENTO                                                                                   
						3	DOLAR ESTADOS UNIDOS                                                                                
						4	UNIDAD TRIBUTARIA MENSUAL        
						*/
						declare @an_tasa_mora T_t_tasa

						if @currentType = 1
						begin
							set @an_tasa_mora = (select tp_peso from @tabla_tasa_mora )
						end 

						if @currentType = 2
						begin
							set @an_tasa_mora = (select tp_uf from @tabla_tasa_mora )
						end 

						if @currentType = 3
						begin
							set @an_tasa_mora = (select tp_dolar from @tabla_tasa_mora )
						end 

						if @an_tasa_mora = null or @an_tasa_mora = 0--si es NULL dejamos la de PESO
						begin
							set @an_tasa_mora = (select tp_peso from @tabla_tasa_mora )
						end



					select @tipo_cambio as ChangeType, @an_tasa_mora as DefaultRate
				";
            var param = new
            {
                currentType,
                dateToConsult
            };

            (string, object) result = (query, param);
            return result;
        }
    }
}
