using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanner.Core.API.Helpers
{
    /// <summary>
    /// Trace for telemetry the insight
    /// </summary>
    public class TraceHelper
    {
        
        private static TelemetryClient telemetryClient = new TelemetryClient();

        /// <summary>
        /// Enum for info of trace
        /// </summary>
        public enum TraceInfoTypes
        {
            /// <summary>
            /// Mensajes de información
            /// </summary>
            Information,

            /// <summary>
            /// Mensajes de peligro
            /// </summary>
            Warning,

            /// <summary>
            /// Mensajes de error
            /// </summary>
            Error
        }
        /// <summary>
        /// Trace for execption in insight
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string TraceException(Exception ex)
        {
            var exTelemetry = new ExceptionTelemetry
            {
                ProblemId = Guid.NewGuid().ToString(),
                Exception = ex
            };
            telemetryClient.TrackException(exTelemetry);
            return exTelemetry.ProblemId;
        }
        /// <summary>
        /// Message for trace insight
        /// </summary>
        /// <param name="message"></param>
        public static void TraceInfo(string message)
        {
            var parameters = new Dictionary<string, object>();
            TraceInfo(TraceInfoTypes.Information, message, parameters);
        }
        /// <summary>
        /// Parameters for trace info of insight
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        public static void TraceInfo(string method, IDictionary<string, object> parameters)
        {
            TraceInfo(TraceInfoTypes.Information, method, parameters);
        }

        /// <summary>
        /// Trace of telemetry for insight
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        public static void TraceInfo(TraceInfoTypes type, string method, IDictionary<string, object> parameters)
        {
            var sb = new StringBuilder(method);
            foreach (var parameter in parameters)
            {
                sb.Append(" ");
                if (parameter.Value == null)
                    sb.AppendFormat("[{0}=]", parameter.Key);
                else if (parameter.Value is IEnumerable)
                {
                    sb.AppendFormat("[{0}=", parameter.Key);
                    foreach (object v in parameter.Value as IEnumerable)
                        sb.Append(v);
                    sb.Append("]");
                }
                else
                {
                    var ex = parameter.Value as Exception;
                    if (ex != null)
                    {
                        sb.AppendFormat("[{0}={1}]", parameter.Key, ex.ToString());
                    }
                    else
                    {
                        sb.AppendFormat("[{0}={1}]", parameter.Key, parameter.Value);
                    }
                }
            }
            switch (type)
            {
                case TraceInfoTypes.Error:
                    telemetryClient.TrackTrace(sb.ToString(), Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Error);
                    break;
                case TraceInfoTypes.Warning:
                    telemetryClient.TrackTrace(sb.ToString(), Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Warning);
                    break;
                case TraceInfoTypes.Information:
                default:
                    telemetryClient.TrackTrace(sb.ToString(), Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Information);
                    break;
            }
        }
    }
}
