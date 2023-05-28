
namespace Tanner.Core.DataAccess.Enums
{
    /// <summary>
    /// Enumerator representing states of a document
    /// </summary>
    /// <summary xml:lang="es">
    /// Enumerador que representa estados de un documento
    /// </summary>
    public enum DocumentStatus
    {
        //Ingresado
        INGRESS   = 0,

        //vigente
        ACTIVE    = 1,

        //Pagado
        PAID      = 2,

        //Protestado
        PROTESTED = 3
    }
}
