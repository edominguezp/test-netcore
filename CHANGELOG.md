# Tanner.Core.API

## Release 2.6.0.0 - 2023-04-19 
* corrección query Query_DataDocumentByOperation dato venia en nulo y lo intenta parsear a decimal

## Release 2.5.0.0 - 2022-10-04 # 21406
* Agregar condicion para que cuando el estado actual sea positivo y ratificacion arroje un valor diferente de protocolo actualice el estado.

## Release 2.4.0.0 - 2022-10-03 # 21406
* Agregar validación para evitar que riesgo actualice el estado del documento en caso que el estado actual sea positivo.

## Release 2.3.9.0 - 2022-09-02 # 21062
* Se agrega se modifica el resumen de la operación para obtener el porcentaje de pago del cliente como deudor.

## Release 2.3.8.0 - 2022-08-30 # 20893
* Se agrega la información del banco al endpoint de obtener operacion
* Se agrega la información del deudor en los documentos del endpoint de planilla de liquidación

## Release 2.3.7.0 - 2022-06-09 # 19803
* Se corrige error al validar la existencia de un documento en una operación de core.

## Release 2.3.6.0 - 2022-06-07 # 19803
* Se corrige error al validar la existencia de un documento en una operación de core.

## Release 2.3.5.0 - 2022-06-06 #
* Se modifica endpoint SettlementForm del controler Operation para usar procedimiento almacenado.

## Release 2.3.4.0 - 2022-06-02 #19395
* Se agrega helper para formatear Rut a la estructura solicitada por CORE.

## Release 2.3.3.0 - 2022-06-01 #19752
* Se modifica la consulta sql para obtener los productos. Se le incluye el campo 'BusinessLineCode' donde se almacena el código de la línea de negocios.

## Release 2.3.2.0 - 2022-06-01 #19395
* Se agrega endpoint para obtener una operación a partir de un documento.

## Release 2.3.1.0 - 2022-05-24 #19632
* Se cambia el nombre del campo de fecha de la operación en la planilla de liquidación.

## Release 2.3.0.0 - 2022-05-24 #19632
* Se agrega el campo de fecha de la operación en la planilla de liquidación.

## Release 2.2.0.0 - 2022-05-10 #19210
* Se agrega el campo código del estado de la operación a la consulta de operaciones por cliente.

## Release 2.1.5.0 - 2022-05-10 #19210
* Se agrega endpoint para obtener los gerentes comerciales desde Segpriv.

## Release 2.1.4.0 - 2022-04-27 #18994
* Se agrega endpoint para la inserción de operaciones sin simular
* Se modifica la query de búsqueda de clientes
* Se agrega endpoint para obtener tipos de cambio almacenados en el sistema

## Release 2.1.3.5 - 2022-04-26 #19155
* Se agrega parámetro tipo de dte a las simulaciones.

## Release 2.1.3.4 - 2022-04-14 #19010
* Se agrega un filtro de operaciones por número de operación.

## Release 2.1.3.3 - 2022-01-24 #18099
* Se agrega filtrado y ordenamiento a endpoint de operaciones api/operation/{rut}/RUTClient.

## Release 2.1.3.2 - 2021-12-27 #17791
* se corrige formato de rut en planilla de liquidación.

## Release 2.1.3.1 - 2021-12-24 #17791
* se quitan espacios en blanco del resultado de la planilla de liquidacion.

## Release 2.1.3.0 - 2021-12-24 #17791
* se agregan campos faltantes en la planilla de liquidación.

## Release 2.1.2.5 - 2021-09-01 #15620
* se agregan estados vigente, pagados y protestados al filtro de estado de documento en la busqueda de la data de la planilla de liquidacion.

## Release 2.1.2.4 - 2021-08-31 #15698
* se refactoriza metodo de insercion de operacion para obtener los mensajes generados por el store procedure sp_cursarOperaciones.

## Release 2.1.2.3 - 2021-04-22 #14518
* Se corrige error en la consulta para validar los DTE.

## Release 2.1.2.2 - 2021-02-23 #13582
* Se corrige consulta de usuario.

## Release 2.1.2.1 - 2021-02-17 #13463
* Se modifica la query de validar DTE.

## Release 2.1.2.0 - 2021-02-09 #13252
* Se modifica la query de consultar cliente para agregar ciudad y país.

## Release 2.1.1.9 - 2020-12-07 #12385 #12399
* Se agrega un endpoint que obtendra el id de la linea de credito del cliente otorgada por el Rut
* Se agrega un endpoint que permite asociar el archivo a la linea del cliente

## Release 2.1.1.8 - 2020-10-01 #11310
* Se actualizo la query del endpoint datos de operación de crédito

## Release 2.1.1.7 - 2020-10-01 #11309
* Se actualizo la query del endpoint días del documento para corregir error en tipo de producto cheques

## Release 2.1.1.6 - 2020-09-29 #11292
* Se actualizo la query del endpoint días del documento

## Release 2.1.1.5 - 2020-09-28 #11238
* Se agrega el endpoint para detalles del credito de la operación

## Release 2.1.1.4 - 2020-09-21 #11083
* Se agrega el endpoint días del documento

## Release 2.1.1.3 - 2020-09-02 #10695
* Se corrige formato en el rut del deudor.

## Release 2.1.1.2 - 2020-08-21 #10539
* Se actualiza la consulta de obtener los datos de contacto del cliente.

## Release 2.1.1.1 - 2020-08-21 #10361
* Se actualiza la consulta del endpoint operaciones por usuario.

## Release 2.1.1.0 - 2020-08-18 #10292
* Se agrega endpoint dummy de contacto principal por cliente.

## Release 2.1.0.9 - 2020-08-17 #10259
* Se agrega campo fechaEmision del documento en el endpoint Operation/{number}/Documents.

## Release 2.1.0.8 - 2020-08-03 #9892
* Se cambia el query del endpoint operaciones por usuario.

## Release 2.1.0.7 - 2020-07-31 #9770
* Se controla los errores de base de datos cuando se actualizan las condiciones comerciales.

## Release 2.1.0.6 - 2020-07-23 #9750
* Se modifica salida de endpoint Operation/{number}/Documents, añadiendo la observación de la ratificación de cada documento.

## Release 2.1.0.5 - 2020-07-20 #9606
* Se asigna formato al RUT del cliente en el endpoint de operaciones por usuario.

## Release 2.1.0.4 - 2020-07-14 #9535
* Se agregan validaciones de entrada de datos al endpoint de agregar log.

## Release 2.1.0.3 - 2020-07-13 #9492
* Se agrega el endpoint de consulta de operaciones por usuario.

## Release 2.1.0.2 - 2020-07-13 #9502
* Se modifica salida de endpoint Operation/{number}/Documents, añadiendo la fecha de la operación.

## Release 2.1.0.1 - 2020-07-10 #9455
* Se modifica la query para el endpoint de documentos de la operación por RUT.

## Release 2.1.0.0 - 2020-06-13 #8902
* Se agrega endpoint para obtener el estado de una operación.

## Release 2.0.2.0 - 2020-06-12 #8948
* Se actualiza cálculo del pago actual al cliente en el endpoint de obtener operación por número.
* Se corrige bug en local cuando se utilizaba el swagger desde https que no permitía las peticiones.

## Release 2.0.1.0 - 2020-06-11 #8919
* Se actualiza el cálculo del pago propuesto.

## Release 2.0.0.0 - 2020-06-04 #8619, #8622, #8625, #8628, #8631, #8823
* Se agrega endpoint para obtener las sucursales.
* Se agrega endpoint para obtener las zonas.
* Se agrega endpoint para obtener los canales (origen de la operación).
* Se agrega endpoint para obtener los tipos de productos.
* Se agrega endpoint para obtener los tipos de operaciones.
* Se modifica la ruta del endpoint de obtener las zonas por estado de las operaciones.
* Se devuelve el monto de la operación con decimales para poder mostrar el valor correcto cuando la operación está en UF.

## Release 1.3.0.1 - 2020-06-04 
* Se agrega endpoint para la consulta de operaciones con su documento por RUT.

## Release 1.3.0.0 - 2020-06-04 #8766 
* Se actualiza el cálculo del pago propuesto para las operaciones que son en UF.

## Release 1.2.0.1 - 2020-06-03 
* Se modifica el archivo release.yaml para el nuevo aks de QA

## Release 1.2.0.0 - 2020-05-28 #8494
* Se devuelve en el endpoint de obtener la operación el código externo de la moneda.
* Se devuelve en el endpoint de obtener el resumen de una operación el código externo de la moneda.

## Release 1.1.0.0 - 2020-05-27 #8514
* Se actualiza la respuesta de error en los endpoints. Se devuelve el id del error y un mensaje genérico.
* Se actualiza la consulta de obtener el resumen de una operacion-cotización que no se devolvía el monto correctamente.

## Release 1.0.1.0 - 2020-05-18
* Se agrega a la respuesta del endpoint "{number}/ProposedPayment" el giro y la tasa All In actual de una operación

## Release 1.0.0.0 - 2019-09-11
* Versión inicial