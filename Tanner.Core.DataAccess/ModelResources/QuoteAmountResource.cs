namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the amount of Quotation
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa los montos de la cotización
    /// </summary>
    public class QuoteAmountResource
    {
        /// <summary>
        /// Amount Document Currency Origin
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto Documento Moneda Origen 
        /// </summary>
        public decimal AmountDocumentCurrencyOrigin { get; set; }

        /// <summary>
        /// Amount Document National Currency
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto Documento Moneda Nacional 
        /// </summary>
        public int AmountDocumentNationalCurrency { get; set; }

        /// <summary>
        /// Amount Participated Currency Origin
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto Acticipado Moneda Origen 
        /// </summary>
        public decimal AmountParticipatedCurrencyOrigin { get; set; }

        /// <summary>
        /// National Currency Accrued Amount
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto Acticipado MonedaNacional
        /// </summary>
        public int NationalCurrencyAccruedAmount { get; set; }

        /// <summary>
        /// Price Difference Currency Origin
        /// </summary>
        ///<summary xml:lang="es">
        /// Diferencia De Precio Moneda Origen
        /// </summary>
        public decimal PriceDifferenceCurrencyOrigin { get; set; }

        /// <summary>
        /// National Currency Price Difference
        /// </summary>
        ///<summary xml:lang="es">
        /// Diferencia De Precio Moneda Nacional
        /// </summary>
        public int NationalCurrencyPriceDifference { get; set; }

        /// <summary>
        /// Commission Currency Origin
        /// </summary>
        ///<summary xml:lang="es">
        /// Comision Moneda Origen 
        /// </summary>
        public decimal CommissionCurrencyOrigin { get; set; }

        /// <summary>
        /// National Currency Commission
        /// </summary>
        ///<summary xml:lang="es">
        /// Comision Moneda Nacional 
        /// </summary>
        public int NationalCurrencyCommission { get; set; }

        /// <summary>
        /// Origin Currency Expense
        /// </summary>
        ///<summary xml:lang="es">
        /// Gasto Moneda Origen
        /// </summary>
        public decimal OriginCurrencyExpense { get; set; }

        /// <summary>
        /// National Currency Expenditure
        /// </summary>
        ///<summary xml:lang="es">
        /// Gasto Moneda Nacional 
        /// </summary>
        public int NationalCurrencyExpenditure { get; set; }

        /// <summary>
        /// IVA Currency Origin
        /// </summary>
        ///<summary xml:lang="es">
        /// IVA Moneda Origen 
        /// </summary>
        public decimal IVACurrencyOrigin { get; set; }

        /// <summary>
        /// IVA National Currency
        /// </summary>
        ///<summary xml:lang="es">
        /// IVA Moneda Nacional
        /// </summary>
        public int IVANationalCurrency { get; set; }

        /// <summary>
        /// Subtotal Documents Currency Origin
        /// </summary>
        ///<summary xml:lang="es">
        /// Subtotal Documentos Moneda Origen
        /// </summary>
        public decimal SubtotalDocumentsCurrencyOrigin { get; set; }

        /// <summary>
        /// Subtotal Documents National Currency
        /// </summary>
        ///<summary xml:lang="es">
        /// Subtotal Documentos Moneda Nacional 
        /// </summary>
        public int SubtotalDocumentsNationalCurrency { get; set; }

        /// <summary>
        /// Amount To Payment Currency Origin
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto A Pago Moneda Origen 
        /// </summary>
        public decimal AmountToPaymentCurrencyOrigin { get; set; }

        /// <summary>
        /// Amount To Payment National Currency
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto A Pago Moneda Nacional
        /// </summary>
        public int AmountToPaymentNationalCurrency { get; set; }

        public static (string, object) Query_GetAmountQuotation(int number)
        {
            var query = $@"
                DECLARE

	            @MontoDocumentoMonedaOrigen float, --Monto Doctos MX.
	            @MontoDocumentoMonedaNacional numeric,   --Monto Doctos PESO
	            @MontoActicipadoMonedaOrigen float, --Monto Anticipado MX
	            @MontoActicipadoMonedaNacional numeric, --Monto Anticipado PESO
	            @DiferenciaDePrecioMonedaOrigen float, --Dif. Precio MX
	            @DiferenciaDePrecioMonedaNacional numeric,   --Dif. Precio PESO
	            @ComisionMonedaOrigen float, --Comisión Cobranza MX
	            @ComisionMonedaNacional numeric, --Comisión Cobranza PESO
	            @GastoMonedaOrigen float, --Gasto Notarial MX
	            @GastoMonedaNacional numeric, --Gasto Notarial peso
	            @IVAMonedaOrigen float, --I.V.A. mx
	            @IVAMonedaNacional numeric, --I.V.A. peso
	            @SubtotalDocumentosMonedaOrigen float,  --Subtotal Descuentos mx
	            @SubtotalDocumentosMonedaNacional numeric,  --Subtotal Descuentos nacional
	            @MontoAPagoMonedaOrigen float,  --Subtotal Descuentos mx
	            @MontoAPagoMonedaNacional numeric  --Subtotal Descuentos nacional

            SELECT

	            @MontoDocumentoMonedaOrigen = sum (cab.monto_doctos_mx), --Monto Doctos MX.
	            @MontoDocumentoMonedaNacional = sum (cab.monto_doctos),   --Monto Doctos PESO
	            @MontoActicipadoMonedaOrigen = sum (cab.monto_anticipado_mx), --Monto Anticipado MX
	            @MontoActicipadoMonedaNacional =sum (cab.monto_anticipado) , --Monto Anticipado PESO
	            @DiferenciaDePrecioMonedaOrigen = sum (cab.dif_precio_mx) , --Dif. Precio MX
	            @DiferenciaDePrecioMonedaNacional = sum (cab.dif_precio) ,   --Dif. Precio PESO
	            @ComisionMonedaOrigen = cab.comision_cobranza_mx, --Comisión Cobranza MX
	            @ComisionMonedaNacional = cab.comision_cobranza, --Comisión Cobranza PESO
	            @GastoMonedaOrigen = cab.otros_cargos_afecto/cab.tipo_cambio, --Gasto Notarial MX
	            @GastoMonedaNacional = cab.otros_cargos_afecto,  --Gasto Notarial peso
	            @IVAMonedaOrigen = cab.iva_mx, --I.V.A. mx
	            @IVAMonedaNacional = cab.iva, --I.V.A. peso
	            @SubtotalDocumentosMonedaOrigen = ((sum (cab.dif_precio) +	otros_cargos_afecto +	iva +	comision_cobranza ) / cab.tipo_cambio)  ,  --Subtotal Descuentos mx
	            @SubtotalDocumentosMonedaNacional = (sum (cab.dif_precio) +	otros_cargos_afecto +	iva +	comision_cobranza )  ,  --Subtotal Descuentos peso
	            @MontoAPagoMonedaOrigen = (sum (cab.monto_anticipado_mx) - 
							            CASE 
								            When cab.es_vencido = 1 Then 0
							            ELSE
								            sum (cab.dif_precio_mx)
							            END
								            - cab.otros_cargos_afecto_mx - cab.descuento_mx	- cab.iva_mx - cab.comision_cobranza_mx	- ISNULL(cab.gasto_notarial_mx,0) -	cab.otros_descuento_mx
								            - ISNULL(cab.gasto_asoc_credito_mx,0) -	ISNULL(cab.comision_fogain_mx,0) + cab.prepago_interes_mx - ISNULL(cab.impuesto_mx,0) -	cab.anticipo_mx) ,
	            @MontoAPagoMonedaNacional = (sum (cab.monto_anticipado)	- 
							            CASE 
								            When cab.es_vencido = 1 Then 0 
							            ELSE
								            sum (cab.dif_precio)
							            END
								            - cab.otros_cargos_afecto-	cab.descuento-cab.iva-cab.comision_cobranza -ISNULL(cab.gasto_notarial,0)-cab.otros_descuento-ISNULL(cab.gasto_asoc_credito,0)
								            - ISNULL(cab.comision_fogain,0)	+cab.prepago_interes-ISNULL(cab.impuesto,0)	-cab.anticipo) 
	            FROM (
                SELECT  
	            operacion.numero_operacion, --Numero Operacion
	            credito.nivel_credito,
	            pers_cliente.razon_social as nombre_cliente,   --Nombre Cliente
	            pers_cliente.rut_persona as rut_cliente,   --R.U.T.
	            cliente.codigo_cliente,   
	            operacion.fecha_operacion,   --Fecha Operacion            
	            sucursal.descripcion_sucursal,   --Sucursal			
	            custodia.descripcion_sucursal custodia, --Custodia            
                moneda.nombre_moneda,   --Moneda
	            operacion.tasa_operacion,   --Tasa Operacion
                operacion.porcentaje_descuento,   --Porcentaje Descuento
							            Case 
								            When operacion.estado_operacion = '0' Then 'En Analísis' 
								            When operacion.estado_operacion = '1' Then 'Aprobada' 
								            When operacion.estado_operacion= '2' Then 'Vigente'
								            When operacion.estado_operacion= '3' Then 'Cancelada'
							            ELSE
								            ''
							            END estado_operacion, 
							            --operacion.codigo_producto,   --Producto         
							            Case 
								            When operacion.tipo_pagare = '1' Then 'Vista' 
								            When operacion.tipo_pagare = '2' Then 'Puntual' 
								            When operacion.tipo_pagare= '3' Then 'Vista c/Mandato'
							            ELSE
								            ''
							            END tipo_pagare, 
								            operacion.tipo_comi_cob,  --Tipo Comisión
								            comision.descripcion comi_cob_desc, -- --descripcion Tipo Comisión
								            operacion.factor_comi_cob,  --% (if( tipo_comi_cob = 3 or tipo_comi_cob = 4 or tipo_comi_cob = 6 , 1 , 0 ))
								            '22' + LTRIM(STR((operacion.tasa_operacion* 100),10))   + '33' Cuenta,   --Cuenta ('22' + string(  tasa_operacion  * 100 ) + '33')			
								            operacion.minimo_comi_cob,   --Mínimo
								            operacion.maximo_comi_cob,   --Máximo
							            Case 
								            When operacion.tipo_comi_cob   = 2 Then monto_comi_cob
							            ELSE
								            0
							            End monto_flat ,
							            Case 
								            When ISNULL(operacion.con_garantia,'N')  = 'S' Then 'Si'
							            ELSE
								            'No'
							            End garantia ,
							            Case 
								            When ISNULL(operacion.con_responsabilidad,2)   = 1 Then 'SI' 
								            When ISNULL(operacion.con_responsabilidad,2)   = 2 Then 'NO' 
							            ELSE
								            ''
							            End con_responsabilidad ,
							            Case 
								            When ISNULL(operacion.tipo_cobranza,2)= 1 Then 'DIRECTA' 
								            When ISNULL(operacion.tipo_cobranza,2) = 2 Then 'INDIRECTA' 
							            ELSE
								            ''
							            End tipo_cobranza ,
							            Case 
								            When ISNULL(operacion.con_notificacion,2)= 1 Then 'SI' 
								            When ISNULL(operacion.con_notificacion,2) = 2 Then 'NO' 
							            ELSE
								            ''
							            End con_notificacion ,
							            Case 
								            When ISNULL(operacion.con_custodia,2)= 1 Then 'SI' 
								            When ISNULL(operacion.con_custodia,2) = 2 Then 'NO' 
							            ELSE
								            ''
							            End con_custodia,
	            documento.valor_nominal_mx monto_doctos_mx, --Monto Doctos MX.
	            documento.valor_nominal_documento monto_doctos,   --Monto Doctos PESO
	            documento.valor_futuro_mx monto_anticipado_mx, --Monto Anticipado MX
	            documento.valor_futuro_documento monto_anticipado, --Monto Anticipado PESO

							            (case 
								            when td.otro_tratamiento = 'V' then 1
							            else 0
							            end ) as es_vencido,  /*CAS001*/	 
	
	            ( isnull(documento.interes_mx,0) +  isnull(documento.int_vencido_mx,0)  ) dif_precio_mx, --Dif. Precio MX
	            ( isnull(documento.interes_documento,0) +  isnull(documento.interes_vencido,0)  ) dif_precio,   --Dif. Precio PESO
	            ( isnull(operacion.monto_comi_cob,0)/operacion.tipo_cambio) comision_cobranza_mx, --Comisión Cobranza MX
	            isnull(operacion.monto_comi_cob,0) comision_cobranza, --Comisión Cobranza PESO
	            ( isnull(operacion.cargos_afectos,0)/operacion.tipo_cambio) otros_cargos_afecto_mx,  --Otros Cargos Afectos MX
	            isnull(operacion.cargos_afectos,0) otros_cargos_afecto,  --Otros Cargos Afectos peso
	            ( isnull(oc.notario,0)/operacion.tipo_cambio) gasto_notarial_mx, --Gasto Notarial MX
	            isnull(oc.notario,0) gasto_notarial, --Gasto Notarial peso
	            ( isnull(operacion.iva_operacion,0)/operacion.tipo_cambio) iva_mx, --I.V.A. mx
	            isnull(operacion.iva_operacion,0)  iva, --I.V.A. peso
	            ( isnull(operacion.cargos_exentos,0)/operacion.tipo_cambio) descuento_mx, --Descuento mx
	            isnull(operacion.cargos_exentos,0) descuento, --Descuento peso
	            ( isnull(operacion.otros_descuentos,0)/operacion.tipo_cambio) otros_descuento_mx, --Otros descuentos mx
	            isnull(operacion.otros_descuentos,0) otros_descuento, --Otros descuentos peso
	            ( isnull(operacion.impuesto,0)/operacion.tipo_cambio) impuesto_mx, --Impuesto mx
	            isnull(operacion.impuesto,0) impuesto, --Impuesto peso
	            ( isnull(operacion.otros_anticipos,0)/operacion.tipo_cambio) anticipo_mx, --Anticipo mx
	                isnull(operacion.otros_anticipos,0) anticipo, --Anticipo peso
	            ( isnull((oc.GAC - oc.notario),0)/operacion.tipo_cambio) gasto_asoc_credito_mx, --Gastos Asoc. al Credito mx
	                isnull((oc.GAC - oc.notario),0) gasto_asoc_credito, --Gastos Asoc. al Credito peso
	            ( isnull(oc.comision_fogain,0)/operacion.tipo_cambio) comision_fogain_mx, --Comision Fogain mx
	                isnull(oc.comision_fogain,0) comision_fogain, --Comision Fogain peso
	            ( isnull(operacion.diferencia_prepago,0)/operacion.tipo_cambio) prepago_interes_mx, --Prepago Intereses mx
	            isnull(operacion.diferencia_prepago,0) prepago_interes, --Prepago Intereses peso
	            isnull(operacion.descuento_x_fuera ,0) descuento_pro_fuera, --Descuento Por Fuera
	            operacion.tipo_cambio,
	            operacion.codigo_moneda,
	            tipo_operacion.descripcion_tipo_operacion
                FROM dba.tb_fin17_cotizador operacion  (nolock)
                    inner join dba.tb_fin24_cotizador documento       (nolock) on operacion.numero_operacion = documento.numero_operacion
		            --inner join dba.tb_fin01 cliente         (nolock) ON documento.codigo_cliente = cliente.codigo_cliente
		            inner join dba.tb_fin01 cliente         (nolock) ON operacion.codigo_cliente = cliente.codigo_cliente
                    inner join dba.tb_fin41 as pers_cliente (nolock) on cliente.codigo_persona = pers_cliente.codigo_persona   
                    inner join dba.tb_fin08 deudor          (nolock) on documento.codigo_tercero = deudor.codigo_tercero
		            inner join dba.tb_fin41 as pers_deudor  (nolock) on deudor.codigo_persona = pers_deudor.codigo_persona
                    inner join dba.tb_fin61 td              (nolock) on td.tipo_documento = operacion.tipo_documento
                    inner join dba.tb_fin45 moneda          (nolock) on moneda.codigo_moneda = operacion.codigo_moneda
                    inner join tipo_operacion               (nolock) on tipo_operacion.codigo_tipo_operacion = operacion.tipo_operacion
                    left outer join dba.tb_fin26 banco      (nolock) on banco.codigo_banco = documento.codigo_banco 
                    left outer join dba.tb_fin27 plaza      (nolock) on plaza.codigo_plaza = documento.codigo_plaza
                    left outer join dba.tb_fin44 sucursal   (nolock) on sucursal.codigo_sucursal = operacion.codigo_sucursal 
                    left outer join dba.tb_fin44 custodia   (nolock) on custodia.codigo_sucursal = operacion.sucursal_custodia                  
                    left outer join dba.tb_fin13_cotizador            (nolock) on dba.tb_fin13_cotizador.numero_operacion =operacion.numero_operacion
                    left outer join dbo.credito_cotizador credito                 (nolock) on credito.num_op = operacion.numero_operacion                                      
                    left outer join dbo.operacion_credito_cotizador oc (nolock) on operacion.numero_operacion = oc.numero_operacion
		            left outer join dba.tb_fin50 comision (nolock) on operacion.tipo_comi_cob = comision.codigo and comision.tipo = 16
                WHERE operacion.numero_operacion = @{nameof(number)}
                and documento.estado_documento not in(7,8,9,99)                    
                ) cab
                group by 
 	            cab.numero_operacion,
	            cab.nombre_cliente,
	            cab.rut_cliente,
	            cab.codigo_cliente,   
	            cab.fecha_operacion,
	            cab.descripcion_sucursal,
	            cab.custodia,
                cab.nombre_moneda,
	            cab.tasa_operacion,
                cab.porcentaje_descuento,
	            cab.estado_operacion, 
	            cab.tipo_pagare, 
	            cab.tipo_comi_cob,
	            cab.comi_cob_desc,
	            cab.factor_comi_cob,
	            cab.cuenta,
	            cab.minimo_comi_cob,
	            cab.maximo_comi_cob,
	            cab.monto_flat,
	            cab.garantia,
	            cab.con_responsabilidad,
	            cab.tipo_cobranza,
	            cab.con_notificacion,
	            cab.con_custodia,
	            cab.comision_cobranza_mx,
	            cab.comision_cobranza,
	            cab.otros_cargos_afecto_mx,
	            cab.otros_cargos_afecto,
	            cab.gasto_notarial_mx,
	            cab.gasto_notarial,
	            cab.iva_mx,
	            cab.iva,
	            cab.descuento_mx,
	            cab.descuento,
	            cab.otros_descuento_mx,
	            cab.otros_descuento,
	            cab.impuesto_mx,
	            cab.impuesto,
	            cab.anticipo_mx,
	            cab.anticipo,
	            cab.gasto_asoc_credito_mx,
	            cab.gasto_asoc_credito,
	            cab.comision_fogain_mx,
	            cab.comision_fogain,
	            cab.prepago_interes_mx,
	            cab.prepago_interes,
	            cab.descuento_pro_fuera,
	            cab.tipo_cambio,
	                cab.codigo_moneda,
	                cab.nivel_credito,
	                cab.descripcion_tipo_operacion,
	                cab.es_vencido
	 
            SELECT  
            AmountDocumentCurrencyOrigin = @MontoDocumentoMonedaOrigen ,
            AmountDocumentNationalCurrency= @MontoDocumentoMonedaNacional,
            AmountParticipatedCurrencyOrigin = @MontoActicipadoMonedaOrigen,
            NationalCurrencyAccruedAmount = @MontoActicipadoMonedaNacional,
            PriceDifferenceCurrencyOrigin = @DiferenciaDePrecioMonedaOrigen,
            NationalCurrencyPriceDifference = @DiferenciaDePrecioMonedaNacional,
            CommissionCurrencyOrigin = @ComisionMonedaOrigen,
            NationalCurrencyCommission = @ComisionMonedaNacional,
            OriginCurrencyExpense = @GastoMonedaOrigen,
            NationalCurrencyExpenditure = @GastoMonedaNacional,
            IVACurrencyOrigin = @IVAMonedaOrigen,
            IVANationalCurrency = @IVAMonedaNacional,
            SubtotalDocumentsCurrencyOrigin = @SubtotalDocumentosMonedaOrigen,
            SubtotalDocumentsNationalCurrency = @SubtotalDocumentosMonedaNacional,
            AmountToPaymentCurrencyOrigin = @MontoAPagoMonedaOrigen,
            AmountToPaymentNationalCurrency =@MontoAPagoMonedaNacional

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
