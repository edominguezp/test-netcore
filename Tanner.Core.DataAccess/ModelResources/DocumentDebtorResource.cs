using System;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// class that represents a Documents for Debtor
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa los documentos por deudor
    /// </summary>
    public class DocumentDebtorResource
    {
        /// <summary>
        /// Debtor RUT
        /// </summary>s
        ///<summary xml:lang="es">
        /// RUT del deudor
        /// </summary>
        public string DebtorRUT { get; set; }

        /// <summary>
        /// Debtor DV
        /// </summary>
        ///<summary xml:lang="es">
        /// DV del deudor
        /// </summary>
        public string DebtorDV { get; set; }

        /// <summary>
        /// Debtor RUT complet
        /// </summary>
        ///<summary xml:lang="es">
        /// RUT completo del deudor
        /// </summary>
        public string CompleteDebtorRUT { get; set; }

        /// <summary>
        /// Social Reason of debtor
        /// </summary>
        ///<summary xml:lang="es">
        /// Razón social del deudor
        /// </summary>
        public string SocialReasonDebtor { get; set; }

        /// <summary>
        /// Client RUT
        /// </summary>s
        ///<summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        public string ClientRUT { get; set; }

        /// <summary>
        /// Social Reason of client
        /// </summary>
        ///<summary xml:lang="es">
        /// Razón social del cliente
        /// </summary>
        public string SocialReasonClient { get; set; }

        /// <summary>
        /// Folio Document
        /// </summary>
        ///<summary xml:lang="es">
        /// Folio del Documento
        /// </summary>
        public long Folio { get; set; }

        /// <summary>
        /// Expired Date
        /// </summary>
        ///<summary xml:lang="es">
        /// Fecha de Vencimiento
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// Debtor Email
        /// </summary>
        ///<summary xml:lang="es">
        /// Correo del deudor
        /// </summary>
        public string DebtorEmail { get; set; }

        /// <summary>
        /// Document ID
        /// </summary>
        ///<summary xml:lang="es">
        /// ID del documento
        /// </summary>
        public long IDDocument { get; set; }

        /// <summary>
        /// Document Amount
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto del document
        /// </summary>
        public string DocumentAmount { get; set; }

        public static (string, object) Query_DebtorDocuments()
        {
            var query = $@"
         Declare  @ld_hoy		 datetime,
		 @ld_desde		 datetime,
		 @ld_hasta		 datetime

 
        Select @ld_hoy = dbo.f_fecha(getdate())--dbo.f_fecha([dbo].[Dia_habil](getdate()))
        Select @ld_desde = dateadd( dd , +9 , @ld_hoy)  
        Select @ld_hasta = DATEADD (dd,+15, @ld_hoy)

       
	            DECLARE  @CONTACTOS TABLE  
	            ( RUT_DEUDOR	varchar(10),  
	              CORREO_DEUDOR varchar(255));  
  
	            INSERT INTO @CONTACTOS  
	            SELECT distinct p.rut_persona as RUT_DEUDOR  
				             ,LOWER(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(correo,  
				             char(9),''), 
				             char(10),''),  
				             char(13),''),  
				             char(44),''),  
				             char(39),''),  
				             char(60),''),  
				             char(62),''),  
				             'mailto:',''),  
				             char(91),''),  
				             char(93),''),  
				             char(58),'')) AS CORREO_DEUDOR  
	            FROM   dba.tb_fin82 co ( Nolock )  
				             Inner Join dba.tb_fin08 d ( Nolock ) /*Documento*/ On d.codigo_persona = co.codigo_persona   
				             And co.correo Is not NULL And co.correo <> ''  
				             Inner Join dba.tb_fin41 p ( Nolock ) /*Documento*/  ON d.codigo_persona = p.codigo_persona  
	            where LEN(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(correo,  
				             char(9),''),  
				             char(10),''),  
				             char(13),''),  
				             char(44),''),  
				             char(39),''),  
				             char(60),''),  
				             char(62),''),  
				             'mailto:',''),  
				             char(91),''),  
				             char(93),''),  
				             char(58),'')) > 1  
	            order by RUT_DEUDOR;  


	            SELECT  DISTINCT 
		             SUBSTRING(deudores.rut_deudor,2,LEN(deudores.rut_deudor)-2) AS [DebtorRUT]   
		            ,SUBSTRING(deudores.rut_deudor,LEN(deudores.rut_deudor),1) AS [DebtorDV]  
		            ,FORMAT(cast(substring((replace(replace(deudores.rut_deudor,'.',''),'-','')),1,len(replace(replace(deudores.rut_deudor,'.',''),'-',''))-1) as integer),'###,###0') + '-' + right(replace(replace(deudores.rut_deudor,'.',''),'-',''),1) as [CompleteDebtorRUT]  
		            , SUBSTRING(ISNULL(deudores.razon_social_deudor,''),0,40) AS [SocialReasonDebtor]  
		            ,FORMAT(cast(substring((replace(replace(clientes.RUT_CLIENTE,'.',''),'-','')),1,len(replace(replace(clientes.RUT_CLIENTE,'.',''),'-',''))-1) as integer),'###,###0') + '-' + right(replace(replace(clientes.RUT_CLIENTE,'.',''),'-',''),1) as [ClientRUT]  
		            , SUBSTRING(ISNULL(clientes.razon_social,''),0,40) AS [SocialReasonClient]
		            , RTRIM(LTRIM(documentos.numero_documento)) AS [Folio]   
		            , CONVERT(DATE,documentos.fecha_vencimiento_documento)	AS [ExpiredDate]    
		            , isnull(con.CORREO_DEUDOR, 'mail15@tanner.cl') as DebtorEmail  
		            , DOCUMENTOS.ID_DOCUMENTO  as IDDocument
		            ,'$ ' +  replace(replace(convert(varchar,cast(Documentos.valor_nominal_documento as money),1), '.00',''),',','.')  AS [DocumentAmount]   
	              FROM (Select numero_documento,  ID_DOCUMENTO,  
				               cast (cast (valor_nominal_documento as decimal) as numeric) as monto_documento,    
				               fecha_original,     
				               cast (cast (saldo_documento as decimal) as numeric) as saldo_cliente,    
				               cast (cast (saldo_deudor as decimal) as numeric)  as saldo_deudor,     
				               fecha_vencimiento_documento,     
				               datediff(Day, fecha_vencimiento_documento, getdate()) dias_mora,    
				               codigo_cliente,    
				               codigo_tercero,    
				               numero_operacion,   
				               tipo_documento,   
				               valor_nominal_documento   
			            from dba.tb_fin24 (NoLock) /*Documento*/   
			            WHERE estado_documento IN (1)    
			            and tipo_documento in (2,47,90)   
			            and  fecha_vencimiento_documento  between @ld_desde and @ld_hasta
									              ) Documentos    
              inner join (select codigo_cliente,     
                                 rut_persona as rut_cliente,    
					             cliente.razon_social,    
					             codigo_empleado    
		                   from dba.tb_fin01 Cliente (NoLock)  /*Documento*/ 
              inner join (select codigo_persona,    
			                     rut_persona,     
					             razon_social   
			               from dba.tb_fin41 (NoLock)/*Documento*/ ) Cliente_Persona on Cliente.codigo_persona = Cliente_Persona.codigo_persona   
						              ) Clientes on Documentos.codigo_cliente = Clientes.codigo_cliente   
              inner join (select codigo_tercero,    
                                 rut_deudor,    
                                 razon_social_deudor    
			               from dba.tb_fin08 Deudor (NoLock)   /*Documento*/  
              inner join (select codigo_persona,    
			                     rut_persona as rut_deudor,     
				  	             razon_social as razon_social_deudor    
			               from dba.tb_fin41 (NoLock)/*Documento*/ ) Deudor_Persona on Deudor.codigo_persona = Deudor_Persona.codigo_persona   
                                      ) Deudores on Documentos.codigo_tercero = Deudores.codigo_tercero    
              inner join (Select numero_operacion,     
                                 fecha_operacion [Fecha otorgamiento]   
			               from dba.tb_fin17 (NoLock)  /*Documento*/  
			              ) Operaciones on Documentos.numero_operacion = Operaciones.numero_operacion   
              inner join (select tipo_documento,    
                                 descripcion_documento [Tipo de Documento],   
					             cod_tipo_doc [Codigo tipo documento]   
			               from dba.tb_fin61 (NoLock)   /*Documento*/  
			               where cod_tipo_doc in ('FA','FX','FR')    
			              ) DocTipo ON documentos.tipo_documento = DocTipo.tipo_documento   
             left JOIN @CONTACTOS con on con.RUT_DEUDOR = deudores.rut_deudor
                    ";
            (string, object) result = (query, null);
            return result;
        }
    }
}
