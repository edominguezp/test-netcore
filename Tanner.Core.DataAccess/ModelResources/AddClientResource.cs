
using Tanner.Core.DataAccess.Enums;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class AddClientResource
    {
        /// <summary>
        /// RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        public string RUT { get; set; }

        /// <summary>
        /// Client Name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del cliente
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// <summary xml:lang="es">
        /// Correo electrónico del cliente
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Bank Code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código del banco
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// Bank Account
        /// </summary>
        /// <summary xml:lang="es">
        /// Cuenta bancaria del cliente
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        /// <summary xml:lang="es">
        /// Dirección del cliente
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Person Type
        /// </summary>
        /// <summary xml:lang="es">
        /// Tipo de persona 
        /// </summary>
        public PersonType PersonType { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        /// <summary xml:lang="es">
        /// Apellido Paterno
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// MotherLastName
        /// </summary>
        /// <summary xml:lang="es">
        /// Apellido Materno
        /// </summary>
        public string MotherLastName { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        /// <summary xml:lang="es">
        /// Primer Nombre
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// SecondName
        /// </summary>
        /// <summary xml:lang="es">
        /// Segundo Nombre
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Business Name
        /// </summary>
        /// <summary xml:lang="es">
        /// Razón Social
        /// </summary>
        public string BusinessName { get; set; }

        /// <summary>
        /// Economic Activity
        /// </summary>
        /// <summary xml:lang="es">
        /// Actividad económica
        /// </summary>
        public int EconomicActivity { get; set; }

        /// <summary>
        /// Client Status
        /// </summary>
        /// <summary xml:lang="es">
        /// Estado del cliente
        /// </summary>
        public int ClientStatus { get; set; }

        /// <summary>
        /// Branch Office ID
        /// </summary>
        /// <summary xml:lang="es">
        /// Sucursal
        /// </summary>
        public int BranchOfficeID { get; set; }

        /// <summary>
        /// Executive ID
        /// </summary>
        /// <summary xml:lang="es">
        /// Ejecutivo
        /// </summary>
        public int ExecutiveID { get; set; }

        /// <summary>
        /// Client Classification ID
        /// </summary>
        /// <summary xml:lang="es">
        /// Clasificación del cliente
        /// </summary>
        public int ClientClassificationID { get; set; }

        /// <summary>
        /// true if code is SBIF
        /// </summary>
        /// <summary xml:lang="es">
        /// Verdadero si el codigo es de la super intendencia
        /// </summary>
        public bool IsBankSbif { get; set; }


    }
}
