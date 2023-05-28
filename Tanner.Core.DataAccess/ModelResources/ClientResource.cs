using System.Collections.Generic;
using Tanner.Core.DataAccess.Extensions;
using Tanner.Utils.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class ClientResource
    {
        /// <summary>
        /// Name of client
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre del cliente
        /// </summary>
        public string Name
        {
            get
            {
                var result = _name.NormalizeString();
                return result;
            }
            set
            {
                _name = value;
            }
        }
        private string _name;

        /// <summary>
        /// RUT of Client
        /// </summary>
        ///<summary xml:lang="es">
        /// RUT de cliente
        /// </summary>
        public string RUT
        {
            get
            {
                if (string.IsNullOrEmpty(_rut) || _rut == "-")
                    return null;
                var result = _rut.NormalizeRut();
                return result;
            }
            set
            {
                _rut = value;
            }
        }
        private string _rut;

        /// <summary>
        /// Name of executive
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre de ejecutivo
        /// </summary>
        public string ExecutiveName
        {
            get
            {
                var result = _executiveName.NormalizeString();
                return result;
            }
            set
            {
                _executiveName = value;
            }
        }
        private string _executiveName;

        /// <summary>
        /// Email of executive
        /// </summary>
        ///<summary xml:lang="es">
        /// Correo de ejecutivo
        /// </summary>
        public string ExecutiveEmail { get; set; }

        /// <summary>
        /// Status of client 
        /// </summary>
        ///<summary xml:lang="es">
        /// Estado del cliente
        /// </summary>
        public string Status
        {
            get
            {
                var result = _status.NormalizeString();
                return result;
            }
            set
            {
                _status = value;
            }
        }
        private string _status;

        /// <summary>
        /// Email of Client
        /// </summary>
        ///<summary xml:lang="es">
        /// Correo del cliente
        /// </summary>
        public string Email
        {
            get
            {
                var result = _email.NormalizeString();
                return result;
            }
            set
            {
                _email = value;
            }
        }
        private string _email;

        /// <summary>
        /// Economic Activity
        /// </summary>
        ///<summary xml:lang="es">
        /// Actividad economica 
        /// </summary>
        public int? EconomicActivity { get; set; }

        /// <summary>
        /// Branch Office
        /// </summary>
        ///<summary xml:lang="es">
        /// Sucursal
        /// </summary>
        public string BranchOffice
        {
            get
            {
                var result = _branchOffice.NormalizeString();
                return result;
            }
            set
            {
                _branchOffice = value;
            }
        }
        private string _branchOffice;

        /// <summary>
        /// Normalization Status
        /// </summary>
        ///<summary xml:lang="es">
        /// Estado de normalización
        /// </summary>
        public bool NormalizationStatus { get; set; }

        /// <summary>
        /// Resource for Address of Client
        /// </summary>
        ///<summary xml:lang="es">
        /// Recurso para guardar la o las dirección del cliente
        /// </summary>
        public IEnumerable<ClientAddressResource> Address { get; set; }

        /// <summary>
        /// Resource for Contact of client
        /// </summary>
        ///<summary xml:lang="es">
        /// Recurso para guardar el o los contactos del cliente
        /// </summary>
        public IEnumerable<ClientContactResource> Contact { get; set; }

        /// <summary>
        /// SQL query string
        /// </summary>
        /// <summary lang="es">
        /// Consulta SQL
        /// </summary>
        /// <returns></returns>
        public static (string, object) Query_ClientByRUT(string rut)
        {
            var query = $@"
                           --CLIENT
                            SELECT     ISNULL(CLI.nombre_cliente, PER.razon_social) as Name,
                                    PER.rut_persona as RUT,
                                    RTRIM(EMP.nombre_empleado) as ExecutiveName,
                                    RTRIM(EMP.casilla_correo) AS ExecutiveEmail,
                                    DET_EST.descripcion as Status,       
                                    CLI.email AS Email,
                                    PER.actividad_economica as EconomicActitity,
                                    SUC.descripcion_sucursal as BranchOffice,
                                    NOR.estado_normalizador as NormalizationStatus
                            FROM dba.tb_fin41 as PER WITH(NOLOCK)
                                INNER JOIN dba.tb_fin01 as CLI  WITH(NOLOCK) ON ( PER.codigo_persona = CLI.codigo_persona)
                                LEFT JOIN  dba.tb_fin06 as EMP WITH(NOLOCK) ON ( CLI.codigo_empleado = EMP.codigo_empleado )
                                LEFT JOIN dba.tb_fin50 as DET_EST WITH(NOLOCK) ON (CLI.estado_cliente = DET_EST.codigo and DET_EST.tipo = 1)
                                LEFT JOIN dba.tb_fin44 as SUC WITH(NOLOCK) ON ( EMP.codigo_sucursal = SUC.codigo_sucursal )
                                LEFT JOIN dba.tb_cliente_normalizado as NOR WITH(NOLOCK) ON (CLI.codigo_cliente = NOR.codigo_cliente)
                            WHERE  
                                PER.rut_persona = @{nameof(rut)}

                            --ADDRESSES
                            SELECT 	DIR.direccion AS Address,
		                            DIR.numero_direccion AS Number,
		                            DIR.fono AS Phone,
                                    COM.nombre_comuna AS City,
                                    ISNULL(PAIS.nombre_pais,'') AS Country
                            FROM  dba.tb_fin41 as PER 
	                            INNER JOIN dba.tb_fin05 as DIR ON (PER.codigo_persona = DIR.codigo_persona)
                                LEFT JOIN dba.tb_fin120 as Pais ON (DIR.codigo_pais = Pais.codigo_pais)
                                INNER JOIN dbo.tbl_comunas as Com ON( Com.codigo_comuna = dir.codigo_comuna)
                            WHERE 
                                PER.rut_persona = @{nameof(rut)} AND IsNull(DIR.direccion, '') != ''
                            ORDER BY 
                                DIR.id_direccion ASC 

                            -- CONTACTS
                            SELECT 	CONTACTO.nombre_contacto as Name,
		                            CONCAT(CONTACTO.rut_contacto,'-',CONTACTO.dv_contacto ) as RUT, 
		                            CONTACTO.cargo AS Position,
		                            CONTACTO.telefono AS Phone,
		                            CONTACTO.direccion AS Address,
		                            CONTACTO.mail AS Email
                            FROM  dba.tb_fin41 as PER 
	                            INNER JOIN dba.tbl_contacto as CONTACTO on ( PER.codigo_persona = CONTACTO.codigo_persona) 
                            WHERE   
                                PER.rut_persona = @{nameof(rut)}";
            var param = new
            {
                rut = rut.FillRUT()
            };
            (string, object) result = (query, param);
            return result;
        }
    }
}