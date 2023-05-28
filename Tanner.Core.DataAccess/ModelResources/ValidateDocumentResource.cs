using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class ValidateDocumentResource
    {

        /// <summary>
        /// RUT Client
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        /// <remarks>
        /// <example> 
        /// This example shows how the RUT Client field should be sent
        /// <code>
        /// '99999999-9'
        /// </code>
        /// </example>
        /// </remarks>
        /// <example xml:lang="es">
        /// Este ejemplo muestra cómo se debe enviar el campo RUT Client
        /// </example>
        public string RUTClient { get; set; }

        /// <summary>
        /// RUT Debtor
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del deudor
        /// </summary>
        /// <remarks>
        /// <example> 
        /// This example shows how the debtor RUT field should be sent
        /// <code>
        /// '99999999-9', '88888888-8', '77777777-7'
        /// </code>
        /// </example>
        /// </remarks>
        /// <example xml:lang="es">
        /// Este ejemplo muestra cómo se debe enviar el campo RUT deudor
        /// </example>
        public string RUTDebtor { get; set; }

        /// <summary>
        /// Document
        /// </summary>
        /// <summary xml:lang="es">
        /// Numeros de documentos
        /// </summary>
        /// <remarks>
        /// <example> 
        /// This example shows how the document field should be sent
        /// <code>
        /// '9999', '8888', '7777'
        /// </code>
        /// </example>
        /// </remarks>
        /// <example xml:lang="es">
        /// Este ejemplo muestra cómo se debe enviar el campo documento
        /// </example>
        public string Document { get; set; }

        /// <summary>
        /// Document Type
        /// </summary>
        /// <summary xml:lang="es">
        /// Tipo de documento
        /// </summary>
        public int DocumentType { get; set; }

        /// <summary>
        /// State
        /// </summary>
        /// <summary xml:lang="es">
        /// Estado del documento enviado
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Operation number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de operación
        /// </summary>
        public int OperationNumber { get; set; }

		public static (string, object) Query_DocumentValidate(string clientRUT, string debtorRUT, string documents, int documentType)
		{
			var query = @"
				SET NOCOUNT ON;

				--Se declaran variables de trabajo
				DECLARE @ll_client_count_general	int,
						@tot_registros_insertar		int,
						@ll_client_count			int,
						@ll_client_count_tabla		int, 
						@ll_deu_count				int,
						@ll_deu_count_tabla			int,
						@ll_docto_count				int,
						@ll_docto_count_tabla		int

				DECLARE @ll_count_llena		int,
						@ll_count_llena_tot int,
						@ln_codigo_cliente  int,
						@ln_codigo_tercero  int,
						@ln_num_docto		char(20),
						@ln_id_documento    numeric(18, 0),
						@ln_numero_operacion numeric(18, 0),
						@ln_tipo_documento  int,
						@ln_count			int

				DECLARE @s_rut_cliente	varchar(25),
						@s_rut_deudor	varchar(25),
						@s_num_docto	varchar(25)

				--manejo del RUT
				declare @RUT_cli			VARCHAR(50);
				declare @RUT_tercero		VARCHAR(50);
	
				--Se declara tablas de trabajo
				Declare @doctos_a_consultar Table
				(
					id int IDENTITY(1,1) NOT NULL Primary key,	
					rut_cliente			varchar(25),
					cod_cliente			int,
					rut_deudor			varchar(25),
					cod_tercero			int,
					documento			varchar(25),
					tipo_docto			int,
					estado				int,
					num_operacion			int
				)

				--Se declara tabla de salida
				Declare @doctos_validados Table
				(
					id int IDENTITY(1,1) NOT NULL Primary key,	
					rut_cliente			varchar(25),
					rut_deudor			varchar(25),
					documento			varchar(25),
					tipo_docto			int,
					estado				int,
					num_operacion			int
				)

				if isnull(@ll_num_documento,0) = 0
				begin
					set @ll_num_documento = 47
				end

				--validamos que los argumentos de entrada vengan con datos
				if len(ltrim(rtrim(@ss_rut_cliente))) = 0 or len(ltrim(rtrim(@ss_rut_deudor))) = 0 
				begin
					insert into @doctos_validados
					SELECT '', '', *, @ll_num_documento, 0, 0 FROM dbo.splitstring(@ss_documento)


					select	rut_cliente as RUTClient,
							rut_deudor as RUTDebtor,
							documento as Document,
							tipo_docto as Type,
							estado as State,
							num_operacion as OperationNumber
					 from @doctos_validados	

					return

				end

				if len(ltrim(rtrim(@ss_documento))) = 0
				begin
		
					select	rut_cliente as RUTClient,
							rut_deudor as RUTDebtor,
							documento as Document,
							tipo_docto as Type,
							estado as State,
							num_operacion as OperationNumber
					 from @doctos_validados	

					return
				end
	
				--Se declara tablas de trabajo
				Declare @cliente Table
				(
					id int IDENTITY(1,1) NOT NULL Primary key,	
					rut_cliente			varchar(25)
				)

				Declare @deudor Table
				(
					id int IDENTITY(1,1) NOT NULL Primary key,	
					rut_deudor			varchar(25)
				)

				Declare @documento Table
				(
					id int IDENTITY(1,1) NOT NULL Primary key,	
					documento			varchar(25)
				)

				--insertamos datos
				insert into @cliente
					(rut_cliente)
				SELECT * FROM dbo.splitstring(@ss_rut_cliente)

				insert into @deudor
					(rut_deudor)
				SELECT * FROM dbo.splitstring(@ss_rut_deudor)

				insert into @documento
					(documento)
				SELECT * FROM dbo.splitstring(@ss_documento)

				--generamos tabla matriz para consultar
				set @ll_client_count = 1
				select @ll_client_count_tabla	= count(1) 
				from @cliente

				set @ll_deu_count = 1
				select @ll_deu_count_tabla	= count(1) 
				from @deudor

				set @ll_docto_count = 1
				select @ll_docto_count_tabla	= count(1) 
				from @documento

				--calculamos la cantidad total de registros a insertar
				set @ll_client_count_general = 1
				--set @tot_registros_insertar = @ll_client_count_tabla * @ll_deu_count_tabla * @ll_docto_count_tabla
				set @tot_registros_insertar = @ll_deu_count_tabla * @ll_docto_count_tabla

				--Realizamos ciclo para generar generar matriz con CLIENTE
				WHILE @ll_client_count <= @ll_client_count_tabla
				BEGIN
		
					set @s_rut_cliente =  ''
					select @s_rut_cliente = rut_cliente
					 from @cliente
					 where id = @ll_client_count

					 --rescatamos el cod_cliente
					 set @ln_codigo_cliente =  0
					 set @RUT_cli		= ''
					 SET @RUT_cli		= replace(@s_rut_cliente,'-','')
					 SET @RUT_cli		= right( '0000000000' + @RUT_cli , 10 )

					 SELECT @ln_codigo_cliente = isnull(c.codigo_cliente, 0)
						FROM  dba.tb_fin01 c With (nolock)
							INNER JOIN dba.tb_fin41 p With (nolock) ON c.codigo_persona = p.codigo_persona 
						WHERE p.rut_persona = @RUT_cli
		
					set @ll_count_llena = 1
					while @ll_count_llena <= ( @tot_registros_insertar / @ll_client_count_tabla )    
					begin

						insert into @doctos_a_consultar	
						values (@s_rut_cliente,@ln_codigo_cliente,'',0,'',@ll_num_documento,0,0)

						SELECT @ll_count_llena = @ll_count_llena + 1

					end

					SELECT @ll_client_count = @ll_client_count + 1
				END--cerramos ciclo del WHILE @ll_client_count <= @ll_client_count_tabla

				--Realizamos ciclo para generar generar matriz con DEUDOR
				set @ll_client_count_general = 1
				select @ll_deu_count_tabla	= count(1) 
				from @deudor

				while @ll_client_count_general <= @tot_registros_insertar
				begin
		
					set @ll_deu_count = 1
					while @ll_deu_count <= @ll_deu_count_tabla
					begin
			
						set @s_rut_deudor =  ''			
						select @s_rut_deudor = rut_deudor
						 from @deudor
						where id = @ll_deu_count

						--rescatamos el cod_cliente
						 set @ln_codigo_tercero =  0
						 set @RUT_tercero		= ''
						 SET @RUT_tercero		= replace(@s_rut_deudor,'-','')
						 SET @RUT_tercero		= right( '0000000000' + @RUT_tercero , 10 )

						 SELECT @ln_codigo_tercero = deu.codigo_tercero
   						  FROM	dba.tb_fin41 per With (nolock)
							INNER JOIN dba.tb_fin08 deu With (nolock) ON per.codigo_persona = deu.codigo_persona
						  WHERE	per.rut_persona = @RUT_tercero

						update @doctos_a_consultar
						 set rut_deudor = @s_rut_deudor,
							 cod_tercero = @ln_codigo_tercero 
						where id = @ll_client_count_general   

						SELECT @ll_deu_count = @ll_deu_count + 1
						SELECT @ll_client_count_general = @ll_client_count_general + 1
					end  	

				end

				--Realizamos ciclo para generar generar matriz con DOCUMENTO
				set @ll_client_count_general = 1
				select @ll_docto_count_tabla = count(1) 
				from @documento
	
				while @ll_client_count_general <= @tot_registros_insertar
				begin
		
					set @ll_docto_count = 1
					while @ll_docto_count <= @ll_docto_count_tabla
					begin
			
						set @s_num_docto =  ''			
						select @s_num_docto = documento
						 from @documento
						where id = @ll_docto_count

						update @doctos_a_consultar
						 set documento = @s_num_docto
						where id = @ll_client_count_general   

						SELECT @ll_docto_count = @ll_docto_count + 1
						SELECT @ll_client_count_general = @ll_client_count_general + 1
					end  	

				end

				--validamos los documentos
				set @ll_client_count_general = 1
				select @tot_registros_insertar = count(1) 
				from @doctos_a_consultar
	
				while @ll_client_count_general <= @tot_registros_insertar
				begin
		
					set @ln_codigo_cliente	= 0
					set	@ln_codigo_tercero	= 0
					set @ln_num_docto		= ''
					set @ln_id_documento    = 0
					set @ln_numero_operacion = 0
					set @ln_tipo_documento  = 0

					select @ln_codigo_cliente	= cod_cliente,
						   @ln_codigo_tercero	= cod_tercero,
						   @ln_num_docto		= ltrim(rtrim(documento)),
						   @ln_tipo_documento	= tipo_docto
					 from @doctos_a_consultar
					where id = @ll_client_count_general

					SELECT @ln_id_documento = d.id_documento,
					@ln_numero_operacion = o.numero_operacion
					FROM dba.tb_fin24 d with(nolock)
					INNER JOIN dba.tb_fin17 o WITH(NOLOCK) ON d.numero_operacion = o.numero_operacion
					WHERE
					d.codigo_tercero = @ln_codigo_tercero
					AND d.codigo_cliente = @ln_codigo_cliente
					AND d.tipo_documento = @ln_tipo_documento
					AND d.numero_documento = @ln_num_docto
					AND d.estado_documento <> 9
					AND o.estado_operacion <> 5

					if @ln_id_documento > 0
					begin

						update @doctos_a_consultar
						 set estado = 1,
							 num_operacion = @ln_numero_operacion
						where id = @ll_client_count_general

					end
		
					SELECT @ll_client_count_general = @ll_client_count_general + 1
				end

				--Insertamos los doctos que existen
				insert into @doctos_validados
				select rut_cliente,
					   rut_deudor,
					   documento,
					   tipo_docto,
					   estado,
					   num_operacion	   	
				from @doctos_a_consultar
				where estado > 0

				--Insertamos los que faltan
				set @ll_client_count_general = 1
				select @tot_registros_insertar = count(1) 
				from @doctos_a_consultar
		
				while @ll_client_count_general <= @tot_registros_insertar
				begin

					set @s_num_docto = ''

					select @s_num_docto = documento
					 from @doctos_a_consultar
					 where id = @ll_client_count_general   	
		 		 		
					set @ln_count = 0
					select @ln_count = count(1)
					 from @doctos_validados
					where documento = @s_num_docto
		
					if @ln_count = 0
					begin

						insert into @doctos_validados
						select '',
							   '',
							   documento,
							   tipo_docto,
							   estado,
							   num_operacion	   	
						from @doctos_a_consultar
						where id = @ll_client_count_general   	

					end
		
					SELECT @ll_client_count_general = @ll_client_count_general + 1
				end

				--select *
				--from @doctos_a_consultar
		
				select	rut_cliente as RUTClient,
						rut_deudor as RUTDebtor,
						documento as Document,
						tipo_docto as DocumentType,
						estado as State,
						num_operacion as OperationNumber
				from @doctos_validados";

			object param = new {
				ss_rut_cliente = clientRUT,
				ss_rut_deudor = debtorRUT,
				ss_documento = documents,
				ll_num_documento = documentType
			};
			(string, object) result = (query, param);

			return result;
		}
	}
}
