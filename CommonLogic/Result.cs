using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MedicWebApp.CommonLogic
{
    /// <summary>
    /// Класс для передачи внутри системы результата без возвращаемых данных (метод SuccessResult)
    /// при успехе или сообщения при возникновении предусмотренной ошибке (метод ErrorResult)
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Успешна ли операция
        /// </summary>
        public bool Success;

        /// <summary>
        /// Сообщение при неуспешной операции
        /// </summary>
        public string ErrorMessage;

        public Result() { }

        /// <summary>
        /// При успешной операции
        /// </summary>
        /// <returns></returns>
        public static Result SuccessResult()
        {
            return new Result()
            {
                Success = true
            };
        }

        /// <summary>
        /// При неуспешной операции
        /// </summary>
        /// <param name="errorMessage">Сообщение</param>
        public static Result ErrorResult(string errorMessage = null)
        {
            return new Result()
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }

    /// <summary>
    /// Класс для передачи внутри системы результата (метод SuccessResult)
    /// при успехе или сообщения при возникновении предусмотренной ошибке (метод ErrorResult)
    /// </summary>
    /// <typeparam name="T">тип возвращаемых данных операции</typeparam>
    public class Result<T>: Result
    {
        /// <summary>
        /// Значение результата
        /// </summary>
        public T Value;

        public Result() { }

        /// <summary>
        /// При успешной операции
        /// </summary>
        /// <returns></returns>
        public static Result<T> SuccessResult(T value)
        {
            return new Result<T>()
            {
                Value = value,
                Success = true
            };
        }

        /// <summary>
        /// При неуспешной операции
        /// </summary>
        /// <param name="errorMessage">Сообщение описывающее ошибку при операции</param>
        public static new Result<T> ErrorResult(string errorMessage = null)
        {
            return new Result<T>()
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }
}