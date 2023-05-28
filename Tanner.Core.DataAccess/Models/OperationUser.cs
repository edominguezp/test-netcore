using System;

namespace Tanner.Core.DataAccess.Models
{
    public class OperationUser
    {
        /// <summary>
        /// Operation Number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de la operación
        /// </summary>
        public long OperationNumber { get; set; }

        /// <summary>
        /// Customer name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del cliente
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        public string CustomerRUT { get; set; }

        /// <summary>
        /// Operation date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de la operación
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Operation state
        /// </summary>
        /// <summary xml:lang="es">
        /// Estado de la operación
        /// </summary>
        public string OperationState { get; set; }

        /// <summary>
        /// Current task
        /// </summary>
        /// <summary xml:lang="es">
        /// Tarea actual
        /// </summary>
        public string CurrentTask { get; set; }

        /// <summary>
        /// Operation state
        /// </summary>
        /// <summary xml:lang="es">
        /// Estado de la operación
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Executive Email
        /// </summary>
        /// <summary xml:lang="es">
        /// Correo electrónico de ejecutivo
        /// </summary>
        public string ExecutiveEmail { get; set; }

        /// <summary>
        /// Branch office
        /// </summary>
        /// <summary xml:lang="es">
        /// Sucursal
        /// </summary>
        public string BranchOffice { get; set; }

        /// <summary>
        /// Zone
        /// </summary>
        /// <summary xml:lang="es">
        /// Zona
        /// </summary>
        public string Zone { get; set; }

        /// <summary>
        /// Employee type
        /// </summary>
        /// <summary xml:lang="es">
        /// Tipo de empleado
        /// </summary>
        public string EmployeeType { get; set; }
    }
}
