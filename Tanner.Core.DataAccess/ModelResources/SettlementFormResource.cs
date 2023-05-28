using System;
using System.Collections.Generic;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.Models;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class SettlementFormResource
    {
        /// <summary>
        /// Quotation ID
        /// </summary>
        /// <summary xml:lang="es">
        /// ID de la cotización
        /// </summary>
        public long OperationNumber { get; set; }

        /// <summary>
        /// Total amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto total
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Total anticipated
        /// </summary>
        /// <summary xml:lang="es">
        /// Total anticipado
        /// </summary>
        public decimal TotalAnticipated { get; set; }

        /// <summary>
        /// Price difference
        /// </summary>
        /// <summary xml:lang="es">
        /// Diferencia de precios
        /// </summary>
        public decimal PriceDifference { get; set; }

        /// <summary>
        /// Commission
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// Expenses
        /// </summary>
        /// <summary xml:lang="es">
        /// Gastos
        /// </summary>
        public decimal Expenses { get; set; }

        /// <summary>
        /// IVA
        /// </summary>
        /// <summary xml:lang="es">
        /// IVA
        /// </summary>
        public decimal IVA { get; set; }

        /// <summary>
        /// Subtotal discounts
        /// </summary>
        /// <summary xml:lang="es">
        /// Subtotal descuentos
        /// </summary>
        public decimal SubTotalDiscounts { get; set; }

        /// <summary>
        /// Amount to pay
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto a pagar
        /// </summary>
        public decimal AmountToPay { get; set; }

		/// <summary>
		/// Custody Commission
		/// </summary>
		/// <summary xml:lang="es">
		/// Comisión custodia
		/// </summary>
		public decimal CustodyCommission { get; set; }

		/// <summary>
		/// Discounts
		/// </summary>
		/// <summary xml:lang="es">
		/// Descuentos 
		/// </summary>
		public decimal Discounts { get; set; }

		/// <summary>
		/// Other discounts
		/// </summary>
		/// <summary xml:lang="es">
		/// Otros descuentos 
		/// </summary>
		public decimal OtherDiscounts { get; set; }

		/// <summary>
		/// Tax
		/// </summary>
		/// <summary xml:lang="es">
		/// Impuesto 
		/// </summary>
		public decimal Tax { get; set; }

		/// <summary>
		/// Advance
		/// </summary>
		/// <summary xml:lang="es">
		/// Anticipo
		/// </summary>
		public decimal Advance { get; set; }

		/// <summary>
		/// Customer Rut
		/// </summary>
		/// <summary xml:lang="es">
		/// Rut del cliente
		/// </summary>
		public string CustomerRut { get; set; }

		/// <summary>
		/// Grant date
		/// </summary>
		/// <summary xml:lang="es">
		/// Fecha de otorgamiento
		/// </summary>
		public DateTime GrantDate { get; set; }

		/// <summary>
		/// Company name
		/// </summary>
		/// <summary xml:lang="es">
		/// Nombre de la empresa
		/// </summary>
		public string CompanyName { get; set; }

		/// <summary>
		/// Document type
		/// </summary>
		/// <summary xml:lang="es">
		/// Tipo de documento
		/// </summary>
		public string DocumentType { get; set; }

		/// <summary>
		/// Documents total
		/// </summary>
		/// <summary xml:lang="es">
		/// Total de documentos
		/// </summary>
		public int DocumentsTotal { get; set; }

		/// <summary>
		/// Balance
		/// </summary>
		/// <summary xml:lang="es">
		/// Saldo
		/// </summary>
		public decimal Balance { get; set; }

		/// <summary>
		/// Operation rate
		/// </summary>
		/// <summary xml:lang="es">
		/// Tasa de la operación
		/// </summary>
		public decimal OperationRate { get; set; }

		/// <summary>
		/// Documents
		/// </summary>
		/// <summary xml:lang="es">
		/// Documentos
		/// </summary>
		public IEnumerable<SettlementFormDocument> Documents { get; set; }

		/// <summary>
		/// Operation date
		/// </summary>
		/// <summary xml:lang="es">
		/// Fecha de la operación
		/// </summary>
		public DateTime OperationDate { get; set; }

        public static (string, object) Query_SettlementFormByOperationNumber(long operationNumber)
        {
            var query = @"DECLARE @cod_respuesta		NUMERIC
					,@respuesta				VARCHAR(255)
					,@codigo_producto		NUMERIC
					,@valor_nominal			T_t_dinero
					,@monto_anticipado		T_t_dinero
					,@diferencia_precio		T_t_dinero
					,@es_vencido			bit
					,@label_temp			VARCHAR(128)
					/*-- define variables para ejecutar script directo en ide
					,@numero_operacion		NUMERIC
					,@ingresado	int
					,@vigente 	int
					,@pagado int 
					,@protestado int 
					set @numero_operacion = 693477--693817
					set @ingresado	=0
					set @vigente 	=1
					set @pagado =2 
					set @protestado =3 
					--*/
				-- Obtenemos si producto tiene otro tramiento o no
				IF (SELECT ISNULL(otro_tratamiento,'N')
					FROM dba.tb_fin61 t61 (NOLOCK)
						left join dba.tb_fin17_cotizador t17 (NOLOCK) ON t17.tipo_documento = t61.tipo_documento
					WHERE t17.numero_operacion = @numero_operacion) = 'V'
					SET @es_vencido = 1
				ELSE
					SET @es_vencido = 0

				-- Declaramos tabla local principal temporal donde almacenaremos los datos
				DECLARE @tabla_temp_planilla TABLE (
					nro_cotizacion T_t_identificador
					, moneda T_t_descripcion
					, tipo_cambio T_t_factor
					, tasa_operacion T_t_tasa
					, valor_nominal T_t_dinero
					, monto_anticipado T_t_dinero
					, diferencia_precio T_t_dinero
					, comision_custodia T_t_dinero
					, comision_operacional T_t_dinero
					, gasto_notarial T_t_dinero
					, iva T_t_dinero
					, subtotal_descuentos T_t_dinero			
					, descuento T_t_dinero
					, otros_descuentos T_t_dinero
					, impuesto T_t_dinero
					, anticipo T_t_dinero
					, seguro_desgravamen T_t_dinero
					, seguro_cesantia T_t_dinero
					, gac NUMERIC
					, comision_fogain T_t_dinero
					, diferencia_prepago T_t_dinero
					, monto_a_giro T_t_dinero
					, descuento_por_fuera T_t_dinero
					, monto_pago T_t_dinero
					, RutCliente char(10)
					, RazonSocial char(100)
					, FechaOtorgamiento CHAR(8)
					, TipoDocumento char(100)
					, CantidadDocumentos int
					, FechaOperacion CHAR(8)
				)

				-- Declaramos tabla temporal con salida general
				DECLARE @tabla_datos_generales TABLE(
					label VARCHAR(128)
					,valor VARCHAR(128)
					,tipo_valor VARCHAR(128)
				)

				-- Declaramos tabla temporal con salida de montos
				DECLARE @tabla_montos TABLE(
					label VARCHAR(128)
					,valor T_t_dinero
					,tipo_valor VARCHAR(128)
				)

				-- Declaramos tabla temporal con salida valores a descontar
				DECLARE @tabla_menos TABLE(
					label VARCHAR(128)
					,valor T_t_dinero
					,tipo_valor VARCHAR(128)
				)

				-- Declaramos tabla temporal con salida valores a sumar
				DECLARE @tabla_mas TABLE(
					label VARCHAR(128)
					,valor T_t_dinero
					,tipo_valor VARCHAR(128)
				)

				-- Declaramos tabla temporal con salida de totales
				DECLARE @tabla_totales TABLE(
					label VARCHAR(128)
					,valor T_t_dinero
					,tipo_valor VARCHAR(128)
				)

				-- Declaramos tabla temporal con documentos
				DECLARE @tabla_documentos TABLE(
					nro_documento					T_t_identificador
					,folio							T_t_codigo_alfa_largo
					,fecha_vencimiento_documento	DATETIME
					,monto							T_t_dinero
				)

				-- Obtenemos codigo_producto
				SELECT @codigo_producto = codigo_producto	
				FROM dba.tb_fin17 (NOLOCK)
				WHERE numero_operacion = @numero_operacion

				-- Insertamos registro principal
				INSERT INTO @tabla_temp_planilla
				SELECT @numero_operacion
					, ISNULL(moneda.nombre_moneda,'PESO')
					, ISNULL(operacion.tipo_cambio,1)
					, ISNULL(operacion.tasa_operacion,0)
					, 0 -- valor_nominal 
					, 0 -- monto_anticipado
					, 0 -- diferencia_precio
					, ISNULL(operacion.monto_comi_cob,0)
					, ISNULL(operacion.cargos_afectos,0)
					, ISNULL(oc.notario,0)
					, ISNULL(operacion.iva_operacion,0)
					, 0 --subtotal descuentos
					, ISNULL(operacion.cargos_exentos,0)
					, ISNULL(operacion.otros_descuentos,0)
					, ISNULL(operacion.impuesto,0)
					, ISNULL(operacion.otros_anticipos,0)
					, ISNULL(oc.seguro_desgravamen,0)
					, ISNULL(oc.seguro_cesantia,0)
					, ISNULL(oc.GAC,0) - ISNULL(oc.notario,0)
					, ISNULL(oc.comision_fogain,0)
					, ISNULL(operacion.diferencia_prepago,0)
					, 0 -- monto_a_giro
					, ISNULL(operacion.descuento_x_fuera,0)
					, 0 -- monto_pago
					, pers_cliente.RUT_PERSONA
					, pers_cliente.razon_social
					, CONVERT(CHAR(8), operacion.FECHA_ACTUALIZACION,112)
					, td.descripcion_documento
					, 0
					, CONVERT(CHAR(8), operacion.fecha_operacion,112)
				FROM dba.tb_fin17 operacion (NOLOCK)		
					INNER JOIN tipo_operacion tope (NOLOCK)	ON tope.codigo_tipo_operacion	= operacion.tipo_operacion
					LEFT OUTER JOIN dbo.operacion_credito oc (NOLOCK) ON operacion.numero_operacion	= oc.numero_operacion
					INNER JOIN dba.tb_fin45 moneda (NOLOCK) ON moneda.codigo_moneda = operacion.codigo_moneda
					inner join dba.tb_fin01 cliente         (nolock) on operacion.codigo_cliente = cliente.codigo_cliente
					inner join dba.tb_fin41 as pers_cliente (nolock) on cliente.codigo_persona = pers_cliente.codigo_persona
					inner join dba.tb_fin61 td              (nolock) on td.tipo_documento = operacion.tipo_documento   

				WHERE operacion.numero_operacion = @numero_operacion

				-- Insertamos documentos
				INSERT INTO @tabla_documentos
				SELECT doc.id_documento
					,doc.numero_documento
					,doc.fecha_vencimiento_documento --CONVERT(VARCHAR(20), doc.fecha_vencimiento_documento, 105)
					,ISNULL(doc.valor_nominal_documento,0)
				FROM dba.tb_fin24 doc (NOLOCK)		
				WHERE doc.numero_operacion = @numero_operacion --67149
					AND doc.estado_documento IN (@ingresado,@vigente,@pagado,@protestado)
				

				-- Obtenemos Valor nominal, monto ancipado y diferencia de precio
				SELECT @valor_nominal = SUM(ISNULL(valor_nominal_documento,0))
					,@monto_anticipado = SUM(ISNULL(valor_futuro_documento,0))
					,@diferencia_precio = SUM(ISNULL(interes_documento,0))  + SUM(ISNULL(interes_vencido,0))
				FROM dba.tb_fin24 (NOLOCK)
				WHERE numero_operacion = @numero_operacion
					AND estado_documento IN (@ingresado,@vigente,@pagado,@protestado)

				-- Actualizamos el registro principal con los valores antes obtenidos
				UPDATE @tabla_temp_planilla
					SET valor_nominal = ISNULL(@valor_nominal,0)
						,monto_anticipado = ISNULL(@monto_anticipado,0)
						,diferencia_precio = ISNULL(@diferencia_precio,0)
				WHERE nro_cotizacion = @numero_operacion

				-- Actualizamos el registro principal con el subtotal de descuentos
				UPDATE @tabla_temp_planilla
					SET subtotal_descuentos = diferencia_precio + comision_custodia + comision_operacional + iva
				WHERE nro_cotizacion = @numero_operacion

				-- Actualizamos el registro principal con el monto a giro
				UPDATE @tabla_temp_planilla
					SET monto_a_giro = monto_anticipado + diferencia_prepago - comision_operacional - descuento - iva - comision_custodia - otros_descuentos - gasto_notarial - gac - comision_fogain - impuesto - anticipo
				WHERE nro_cotizacion = @numero_operacion

				-- Si la el producto no tiene otro tratamiento al monto a giro se le descuenta la diferencia de precio
				IF @es_vencido = 0
				BEGIN
					UPDATE @tabla_temp_planilla
						SET monto_a_giro = monto_a_giro - diferencia_precio
					WHERE nro_cotizacion = @numero_operacion
				END

				-- Actualizamos el registro principal con el monto pago
				UPDATE @tabla_temp_planilla
					SET monto_pago = monto_a_giro - descuento_por_fuera
				WHERE nro_cotizacion = @numero_operacion

				-- Actualizamos cantidad de documentos
				UPDATE @tabla_temp_planilla
					SET CantidadDocumentos = (select count(*) from @tabla_documentos)
				WHERE nro_cotizacion = @numero_operacion

				---- Llenando Tabla temporal datos generales ----
				-- Mostramos Número de cotización
				INSERT INTO @tabla_datos_generales
				VALUES(	'Número Cotización'
						,CONVERT(VARCHAR(128) ,(SELECT top 1 nro_cotizacion from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
						,'Texto')


				-- Mostramos moneda
				INSERT INTO @tabla_datos_generales
				VALUES(	'Moneda'
						,(SELECT top 1 moneda from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion)
						,'Texto')

				-- Si la moneda es distinta a Peso se muestra el tipo de cambio
				IF (	SELECT top 1 moneda
						FROM @tabla_temp_planilla
						WHERE nro_cotizacion = @numero_operacion) != 'PESO'
					INSERT INTO @tabla_datos_generales
					VALUES(	'Tipo Cambio'
							,CONVERT(VARCHAR(128), (SELECT top 1 tipo_cambio from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
							,'Decimal')

				-- Mostramos tasa de operación
				INSERT INTO @tabla_datos_generales
				VALUES(	'Tasa Operación'
						,CONVERT(VARCHAR(128), (SELECT top 1 tasa_operacion from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
						,'Porcentaje')

				---- Llenando Tabla temporal montos ----
				-- Mostramos monto total de los documentos
				INSERT INTO @tabla_montos
				VALUES(	'Monto Doctos.'
						,CONVERT(NUMERIC(16,4), (SELECT top 1 valor_nominal from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
						,'Numérico')

				-- Si tiene otro tratamiento label cambia
				IF @es_vencido = 0
					SET @label_temp = 'Monto Ancitipado'

				ELSE
					SET @label_temp = 'Capital Financiado'

				-- Mostramos monto anticipado
				INSERT INTO @tabla_montos
				VALUES(	@label_temp
						,CONVERT(NUMERIC(16,4), (SELECT top 1 monto_anticipado from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
						,'Numérico')

				---- Llenando Tabla temporal menos ----
				-- Mostramos diferencia de precio
				INSERT INTO @tabla_menos
				VALUES(	'Dif. Precio'
						,CONVERT(NUMERIC(16,4), (SELECT top 1 diferencia_precio from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
						,'Numérico')

				-- Cambia Label si es Cheque
				IF @codigo_producto IN (1,21,31,60,3,8,56,32,48,54)
					SET @label_temp = 'Comisión Custodia'

				ELSE
					SET @label_temp = 'Comisión Cobranza'

				-- Mostramos comisión custodia o cobranza
				INSERT INTO @tabla_menos
				VALUES(	@label_temp
						,CONVERT(NUMERIC(16,4), (SELECT top 1 comision_custodia from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
						,'Numérico')

				-- Si tiene otro tratamiento Muestra gasto Notarial
				IF @es_vencido = 1
					INSERT INTO @tabla_menos
					VALUES(	'Gasto Notarial'
							,CONVERT(NUMERIC(16,4), (SELECT top 1 gasto_notarial from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
							,'Numérico')
				ELSE
				BEGIN
					-- Sino tiene otro tatamiento pero si el codigo de producto esta entre los indicados cambia el label
					IF @codigo_producto in (1,21,31,60,3,8,56,32,48,54)
						SET @label_temp = 'Comisión Operacional'

					ELSE IF @codigo_producto in (2,47,53,26,25,22,49,61)
						SET @label_temp = 'Comisión de Custodia'

					ELSE IF @codigo_producto in (13,52,42,24,4,30,55)
						SET @label_temp = 'Comisión por Cargo Operacional'

					ELSE
						SET @label_temp = 'Otros Cargos Afectos'

					-- Mostramos comisión operacional
					INSERT INTO @tabla_menos
					VALUES(	@label_temp
							,CONVERT(NUMERIC(16,4), (SELECT top 1 comision_operacional from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
							,'Numérico')
				END

				-- Mostramos Iva
				INSERT INTO @tabla_menos
				VALUES(	'I.V.A.'
						,CONVERT(NUMERIC(16,4), (SELECT top 1 iva from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
						,'Numérico')

				-- Mostramos subtotal de descuento
				INSERT INTO @tabla_menos
				VALUES(	'Subtotal Descuentos'
						,CONVERT(NUMERIC(16,4), (SELECT top 1 subtotal_descuentos from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
						,'Numérico')
		
				-- Se muestra descuento si es mayor a 0
				IF (SELECT top 1 descuento from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion) > 0
					INSERT INTO @tabla_menos
					VALUES(	'Descuento'
							,CONVERT(NUMERIC(16,4), (SELECT top 1 descuento from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
							,'Numérico')

				-- Se muestra otros descuentos si es mayor a 0
				IF (SELECT top 1 otros_descuentos from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion) > 0
					INSERT INTO @tabla_menos
					VALUES(	'Otros Descuentos'
							,CONVERT(NUMERIC(16,4), (SELECT top 1 otros_descuentos from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
							,'Numérico')

				-- Se muestra impuesto si es mayor a 0
				IF (SELECT top 1 impuesto from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion) > 0
					INSERT INTO @tabla_menos
					VALUES(	'Impuesto'
							,CONVERT(NUMERIC(16,4), (SELECT top 1 impuesto from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
							,'Numérico')

				-- Se muestra anticipo si es mayor a 0
				IF (SELECT top 1 anticipo from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion) > 0
					INSERT INTO @tabla_menos
					VALUES(	'Anticipo'
							,CONVERT(NUMERIC(16,4), (SELECT top 1 anticipo from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
							,'Numérico')

				IF (SELECT top 1 gac from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion) > 0
					INSERT INTO @tabla_menos
					VALUES(	'GAC'
							,CONVERT(NUMERIC(16,4), (SELECT top 1 gac from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
							,'Numérico')

				-- Se muestra seguro desgravamen si es mayor a 0
				IF (SELECT top 1 seguro_desgravamen from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion) > 0
					INSERT INTO @tabla_menos
					VALUES(	'S. Desgravamen'
							,CONVERT(NUMERIC(16,4), (SELECT top 1 seguro_desgravamen from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
							,'Numérico')

				-- Se muestra seguro cesantia  si es mayor a 0
				IF (SELECT top 1 seguro_cesantia from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion) > 0
					INSERT INTO @tabla_menos
					VALUES(	'S. Cesantía'
							,CONVERT(NUMERIC(16,4), (SELECT top 1 seguro_cesantia from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
							,'Numérico')

				-- Se muestra comision fogain  si es mayor a 0
				IF (SELECT top 1 comision_fogain from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion) > 0
					INSERT INTO @tabla_menos
					VALUES(	'Comisión Fogain'
							,CONVERT(NUMERIC(16,4), (SELECT top 1 comision_fogain from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
							,'Numérico')

				---- Llenando Tabla temporal mas ----
				-- Si diferencia prepago es mayor a cero se muestra
				IF (SELECT top 1 diferencia_prepago from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion) > 0
					INSERT INTO @tabla_mas
					VALUES(	'Prepago Intereses'
							,CONVERT(NUMERIC(16,4), (SELECT top 1 diferencia_prepago from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
							,'Numérico')

				---- Llenando Tabla temporal totales ----
				-- Si monto a giro es negativo cambia label
				IF (SELECT top 1 monto_a_giro from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion) < 0
					SET @label_temp = 'Valor Cuenta por Cobrar'
				ELSE
				BEGIN
					-- Cambia label si es cheque o no cuando es positivo
					IF @codigo_producto = 1
						SET @label_temp = 'Monto a Giro'
					ELSE
						SET @label_temp = 'Monto a Pago'
				END

				-- Se muestra monto a giro
				INSERT INTO @tabla_totales
				VALUES(	@label_temp
						,CONVERT(NUMERIC(16,4), (SELECT top 1 monto_a_giro from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion))
						,'Numérico')

				-- Si descuentos por fuera es mayor a 0 se muestra y además se muestra el Giro Efectivo.
				IF (SELECT top 1 descuento_por_fuera from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion) > 0
				BEGIN
	
					INSERT INTO @tabla_totales
					VALUES('Descuentos por Fuera', CONVERT(NUMERIC(16,4),(SELECT top 1 descuento_por_fuera from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion)), 'Numérico')

					INSERT INTO @tabla_totales
					VALUES('Giro Efectivo', CONVERT(NUMERIC(16,4),(SELECT top 1 monto_pago from @tabla_temp_planilla WHERE nro_cotizacion = @numero_operacion)), 'Numérico')
				END

				-- Salida Final WS
				select 
					nro_cotizacion as OperationNumber
					, CONVERT(NUMERIC(16,4),valor_nominal) as TotalAmount
					, CONVERT(NUMERIC(16,4),monto_anticipado) as TotalAnticipated
					, CONVERT(NUMERIC(16,4),diferencia_precio) as PriceDifference
					, CONVERT(NUMERIC(16,4),comision_custodia) as Commission
					, case when @es_vencido = 1 then 
							CONVERT(NUMERIC(16,4),gasto_notarial) 
						  else 
						   CONVERT(NUMERIC(16,4),comision_operacional) 
						  end Expenses         
					, CONVERT(NUMERIC(16,4),iva) as IVA 
					, CONVERT(NUMERIC(16,4),subtotal_descuentos) as SubTotalDiscounts
					, CONVERT(NUMERIC(16,4),monto_a_giro) as AmountToPay
					, CONVERT(NUMERIC(16,4),comision_operacional) as CustodyCommission --nuevo
					, CONVERT(NUMERIC(16,4),descuento) as Discounts --nuevo
					, CONVERT(NUMERIC(16,4),otros_descuentos) as OtherDiscounts --nuevo
					, CONVERT(NUMERIC(16,4),impuesto) as Tax --nuevo
					, CONVERT(NUMERIC(16,4),anticipo) as Advance --nuevo
					, dbo.f_formatea_rut_azure(RutCliente) as CustomerRut --nuevo
					, CONVERT(DATETIME,FechaOtorgamiento) as GrantDate --nuevo
					, LTRIM(RTRIM(RazonSocial)) as CompanyName  --nuevo
					, LTRIM(RTRIM(TipoDocumento)) as DocumentType --nuevo
					, CantidadDocumentos as DocumentsTotal --nuevo
					, CONVERT(NUMERIC(16,4),monto_anticipado) as Balance --nuevo
					, tasa_operacion as OperationRate --nuevo
					, CONVERT(DATETIME,FechaOperacion) as OperationDate
				from @tabla_temp_planilla


				select nro_documento	as DocumentID
						,folio	as Folio
						,fecha_vencimiento_documento	as ExpiryDate
						,monto as Amount
				from @tabla_documentos";

            object param = new 
			{ 
				numero_operacion = operationNumber, 
				ingresado = DocumentStatus.INGRESS, 
				vigente = DocumentStatus.ACTIVE, 
				pagado = DocumentStatus.PAID, 
				protestado = DocumentStatus.PROTESTED 
			};
            (string, object) result = (query, param);

            return result;
        }
    }
}
