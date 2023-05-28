using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.Enums
{
    /// <summary>
    /// Enumerator to ratification state
    /// </summary>
    public enum RatificationState
    {
        //EN PROCESO
        IN_PROCESS = 1,
        //POSITIVA
        POSITIVE = 2,
        //NEGATIVA
        NEGATIVE = 3,
        //POSTERIOR
        LATER = 4,
        //PROTOCOLO
        PROTOCOL = 5,
    }
}
