using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.Models
{
    public class OperationByDocumentRequest
    {
        public string DocumentNumber { get; set; }
        public string ClientRut { get; set; }
        public string DebtorRut { get; set; }
        public int ProductCode { get; set; }
    }
}
