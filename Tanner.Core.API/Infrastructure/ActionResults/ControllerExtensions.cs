
namespace Tanner.Core.API.Infrastructure.ActionResults
{
    /// <summary>
    /// Controller extensions
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Get controller name
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <returns>Name</returns>
        public static string GetControllerName(this string controller)
        {
            string result = controller?.Replace("Controller", "");
            return result;
        }
    }
}
