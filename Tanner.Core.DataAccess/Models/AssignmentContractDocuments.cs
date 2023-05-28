using System;

namespace Tanner.Core.DataAccess.Models
{
	/// <summary>
	/// Assignment contract documents
	/// </summary>
	///<summary xml:lang="es">
	/// Documentos del contrato de cesión
	/// </summary>
	public class AssignmentContractDocuments
	{
		/// <summary>
		/// Document number
		/// </summary>
		///<summary xml:lang="es">
		/// Número documento
		/// </summary>
		public long DocumentNumber { get; set; }

		/// <summary>
		/// Debtor RUT
		/// </summary>
		///<summary xml:lang="es">
		/// RUT del deudor
		/// </summary>
		public string DebtorRUT { get; set; }

		/// <summary>
		/// Document number
		/// </summary>
		///<summary xml:lang="es">
		/// Número documento
		/// </summary>
		public string DebtorName { get; set; }

		/// <summary>
		/// Emission date
		/// </summary>
		///<summary xml:lang="es">
		/// Fecha de emisión
		/// </summary>
		public DateTime EmissionDate { get; set; }

		/// <summary>
		/// Original expired date
		/// </summary>
		///<summary xml:lang="es">
		/// Fecha de vencimiento original
		/// </summary>
		public DateTime OriginalExpiredDate { get; set; }

		/// <summary>
		/// Effective expired date
		/// </summary>
		///<summary xml:lang="es">
		/// Fecha de vencimiento efectiva
		/// </summary>
		public DateTime EffectiveExpiredDate { get; set; }

		/// <summary>
		/// Amount documents
		/// </summary>
		///<summary xml:lang="es">
		/// Monto de los documentos
		/// </summary>
		public decimal AmountDocuments { get; set; }
	}
}
