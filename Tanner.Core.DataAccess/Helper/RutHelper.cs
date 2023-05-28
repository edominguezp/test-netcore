using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.Helper
{
    public static class RutHelper
    {
        /// <summary>
        /// Formatea un rut la estructura 10326492-8 al formato 0103264928
        /// </summary>
        /// <param name="dashedRut"></param>
        /// <returns></returns>
        public static string FormatRut(string dashedRut)
        {
            int formatedRutLength = 10;
            string escapedRut = dashedRut.Replace("-", string.Empty);
            int prefixLength = formatedRutLength - escapedRut.Length;
            var formatedRutPrefix = new string('0', prefixLength);

            var formatedRut = $"{formatedRutPrefix}{escapedRut}";
            return formatedRut;
        }
    }
}
