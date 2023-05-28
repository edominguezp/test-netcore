using Tanner.Core.DataAccess.Extensions;
using Tanner.Utils.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class DebtorBaseResource
    {
        /// <summary>
        /// Debtor RUT
        /// </summary>
        ///<summary xml:lang="es">
        /// RUT de deudor
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
        /// Debtor name
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre del deudor
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

    }
}
