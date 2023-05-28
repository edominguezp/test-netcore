namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the data to modify of electronic receiver
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa los datos a modificar del receptor electrónico
    /// </summary>
    public class UpdateElectronicReceiver
    {
        /// <summary>
        /// Debtor RUT
        /// </summary>
        ///<summary xml:lang="es">
        /// RUT del deudor
        /// </summary>
        public string RUT { get; set; }

        /// <summary>
        /// Is electronic receiver
        /// </summary>
        ///<summary xml:lang="es">
        /// Es receptor electrónico
        /// </summary>
        public bool IsReceiver { get; set; }
    }
}
