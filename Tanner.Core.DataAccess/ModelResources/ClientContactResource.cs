using Tanner.Core.DataAccess.Extensions;
using Tanner.Utils.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class ClientContactResource
    {
        /// <summary>
        /// Name of Contact
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre de contacto
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
        /// RUT of Contact
        /// </summary>
        ///<summary xml:lang="es">
        /// RUT del contacto
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
        /// Position of Contact
        /// </summary>
        ///<summary xml:lang="es">
        /// Cargo del contacto
        /// </summary>
        public string Position
        {
            get
            {
                var result = _position.NormalizeString();
                return result;
            }
            set
            {
                _position = value;
            }
        }
        private string _position;

        /// <summary>
        /// Phone of contact
        /// </summary>
        ///<summary xml:lang="es">
        /// Telefono de contacto
        /// </summary>
        public string Phone
        {
            get
            {
                var result = _phone.NormalizeString();
                return result;
            }
            set
            {
                _phone = value;
            }
        }
        private string _phone;

        /// <summary>
        /// Address of Contact
        /// </summary>
        ///<summary xml:lang="es">
        /// Dirección del contacto
        /// </summary>
        public string Address
        {
            get
            {
                var result = _address.NormalizeString();
                return result;
            }
            set
            {
                _address = value;
            }
        }
        private string _address;

        /// <summary>
        /// Email of Contact
        /// </summary>
        ///<summary xml:lang="es">
        /// Correo electrónico del contacto
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

    }
}