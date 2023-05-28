
using System.Linq;
using System.Reflection;
using Tanner.Utils.Extensions;

namespace Tanner.Core.DataAccess.Extensions
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class DataExtensions
    {
        /// <summary>
        /// Removes all leading and trailing white-space characters from the current System.String object.
        /// Return null i
        /// </summary>
        /// <param name="data">System.String object</param>
        /// <returns>Normalized string</returns>
        public static string NormalizeString(this string data)
        {
            string result = data?.Trim();
            return result;
        }

        public static T NormalizeObject<T>(this T data) where T: class
        {
            if (data == null)
            {
                return null;
            }
            PropertyInfo[] properties = typeof(T).GetProperties();
            bool anyWithValue = properties.Any(t=> t.GetValue(data) != null);
            if (!anyWithValue)
            {
                return null;
            }
            return data;
        }

        public static string FillRUT(this string rut, bool padLef = true)
        {
            string result = rut.NormalizeRut();
            result = result.Replace("-", "");
            if (padLef)
            {
                result = result.PadLeft(10, '0');
            }
            return result;
        }

    }
}
