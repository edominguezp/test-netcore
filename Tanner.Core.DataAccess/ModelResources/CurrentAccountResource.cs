
using Tanner.Core.DataAccess.Models;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class CurrentAccountResource
    {
        /// <summary>
        /// Bank code
        /// </summary>
        /// <summary xml:lang="es">
        /// Codigo de banco
        /// </summary>
        public decimal ID { get; set; }

        /// <summary>
        /// Current Account 
        /// </summary>
        /// <summary xml:lang="es">
        /// Cuenta Corriente
        /// </summary>
        public string CurrentAcount { get; set; }

        public static implicit operator CurrentAccountResource(CurrentAccount data)
        {
            var result = new CurrentAccountResource {
                CurrentAcount = data.CurrentAcount,
                ID = data.ID
            };
            return result;
        }
    }
}
