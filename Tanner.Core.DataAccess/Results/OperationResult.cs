using System;
using System.Collections.Generic;

namespace Tanner.Core.DataAccess.Results
{
    /// <summary>
    /// Object to return data
    /// </summary>
    public class OperationBaseResult
    {
        /// <summary>
        /// 
        /// </summary>
        public OperationErrorStatus? ErrorStatus { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public object Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object Resource { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static OperationBaseResult NotFoundResult(object value, string message = null)
        {
            var result = new OperationBaseResult
            {
                ErrorStatus = OperationErrorStatus.NotFound,
                Resource = value,
                Message = message
            };
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static OperationBaseResult ConflictResult(object value, string message = null)
        {
            var result = new OperationBaseResult
            {
                ErrorStatus = OperationErrorStatus.Conflict,
                Resource = value,
                Message = message
            };
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static OperationBaseResult BadRequestResult(string message)
        {
            var result = new OperationBaseResult
            {
                ErrorStatus = OperationErrorStatus.BadRequest,
                Message = message
            };
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static OperationBaseResult InternalErrorResult(Exception ex = null, string message = null)
        {
            message = message ?? "An internal error has occurred, try again later. If it happens again, contact your system administrator";
            if (ex != null)
            {
                message = $"{message} ERROR: {ex}";
            }
            var result = new OperationBaseResult
            {
                ErrorStatus = OperationErrorStatus.InternalError,
                Message = message,                
            };
            return result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OperationResult<T> : OperationBaseResult
    {
        /// <summary>
        /// 
        /// </summary>
        public OperationResult()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public OperationResult(T data)
        {
            Data = data;
        }

        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public new static OperationResult<T> InternalErrorResult(Exception ex = null, string message = null)
        {
            message = message ?? "An internal error has occurred, try again later. If it happens again, contact your system administrator";
            if (ex != null)
            {
                message = $"{message} ERROR: {ex}";
            }
            var result = new OperationResult<T>
            {
                ErrorStatus = OperationErrorStatus.InternalError,
                Message = message,
            };
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public new static OperationResult<T> NotFoundResult(object value, string message = null)
        {
            var result = new OperationResult<T>
            {
                ErrorStatus = OperationErrorStatus.NotFound,
                Resource = value,
                Message = message
            };
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public new static OperationResult<T> ConflictResult(object value, string message = null)
        {
            var result = new OperationResult<T>
            {
                ErrorStatus = OperationErrorStatus.Conflict,
                Resource = value,
                Message = message
            };
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public new static OperationResult<T> BadRequestResult(string message)
        {
            var result = new OperationResult<T>
            {
                ErrorStatus = OperationErrorStatus.BadRequest,
                Message = message
            };
            return result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OperationCollectionResult<T> : OperationBaseResult
    {
        /// <summary>
        /// Data collection
        /// </summary>
        public IEnumerable<T> DataCollection { get; set; }

        /// <summary>
        /// Total data collection
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public new static OperationCollectionResult<T> NotFoundResult(object value, string message = null)
        {
            var result = new OperationCollectionResult<T>
            {
                ErrorStatus = OperationErrorStatus.NotFound,
                Resource = value,
                Message = message
            };
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public new static OperationCollectionResult<T> InternalErrorResult(Exception ex = null, string message = null)
        {
            message = message ?? "An internal error has occurred, try again later. If it happens again, contact your system administrator";
            if (ex != null)
            {
                message = $"{message} ERROR: {ex}";
            }
            var result = new OperationCollectionResult<T>
            {
                ErrorStatus = OperationErrorStatus.InternalError,
                Message = message,
            };
            return result;
        }

    }

    public class CollectionResult<T>
    {
        /// <summary>
        /// Data collection
        /// </summary>
        public IEnumerable<T> DataCollection { get; set; }

        /// <summary>
        /// Total data collection
        /// </summary>
        public long Total { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum OperationErrorStatus
    {
        /// <summary>
        /// Solicitud Incorrecta
        /// </summary>
        BadRequest,

        /// <summary>
        /// Error interno
        /// </summary>
        InternalError,

        /// <summary>
        /// No encontrado
        /// </summary>
        NotFound,

        /// <summary>
        /// Conflicto
        /// </summary>
        Conflict
    }
}
