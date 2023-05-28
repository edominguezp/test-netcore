using System;
using Tanner.Core.DataAccess.Extensions;
using Tanner.Utils.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class ClientBaseResource
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
        /// Creation Date of Client
        /// </summary>
        ///<summary xml:lang="es">
        /// Fecha de creación del cliente
        /// </summary>
        public DateTime CreationDate { get; set; }


    }
}