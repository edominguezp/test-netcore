using Tanner.Core.DataAccess.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class ClientAddressResource
    {
        /// <summary>
        /// Address
        /// </summary>
        ///<summary xml:lang="es">
        /// Dirección
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
        /// Number of Contact
        /// </summary>
        ///<summary xml:lang="es">
        /// Numero de contacto
        /// </summary>
        public string Number
        {
            get
            {
                var result = _number.NormalizeString();
                return result;
            }
            set
            {
                _number = value;
            }
        }
        private string _number;

        /// <summary>
        /// Phone
        /// </summary>
        ///<summary xml:lang="es">
        /// Telefono
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
        /// City
        /// </summary>
        ///<summary xml:lang="es">
        /// Ciudad
        /// </summary>
        public string City
        {
            get
            {
                var result = _city.NormalizeString();
                return result;
            }
            set
            {
                _city = value;
            }
        }
        private string _city;


        /// <summary>
        /// Country
        /// </summary>
        ///<summary xml:lang="es">
        /// País
        /// </summary>
        public string Country
        {
            get
            {
                var result = _country.NormalizeString();
                return result;
            }
            set
            {
                _country = value;
            }
        }
        private string _country;
    }
}
