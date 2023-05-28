
namespace Tanner.Core.DataAccess.Enums
{
    /// <summary>
    /// enumerator representing the states of the operation
    /// </summary>
    /// <summary xml:lang="es">
    /// enumerador que representa los estados de la operación
    /// </summary>
    public enum OperationState
    {
        //EN ANALISIS
        IN_ANALYSIS = 0,
        //APROBADA
        APPROVED = 1,
        //VIGENTE
        VALID = 2,
        //CANCELADA
        PAYED = 3,
        //VENCIDA
        EXPIRED = 4,
        //CASTIGADA
        PUNISHED = 5
    }
}
