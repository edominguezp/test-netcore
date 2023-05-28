namespace Tanner.Core.DataAccess.Enums
{
    /// <summary>
    /// enumerator representing the states of the reject
    /// </summary>
    /// <summary xml:lang="es">
    /// enumerador que representa los estados del rechazo
    /// </summary>
    public enum InvoiceCode
    {
        //ANULA DOCUMENTO
        CANCEL_DOCUMENT = 0,
        //RECHAZA CONTENIDO DOCUMENTO
        REJECT_DOCUMENT = 1,
        //RECLAMO FALTA TOTAL MERCADERIA
        CLAIM_TOTAL_MERCHANDISE = 2,
        //MODIFICA DOCUMENTO
        MODIFY_DOCUMENT = 3,
        //RECLAMO FALTA PARCIAL MERCADERIA
        CLAIM_PARTIAL_MERCHANDISE = 4,
    }
}
