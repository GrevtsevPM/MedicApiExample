using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedicWebApp.CommonLogic
{
    /// <summary>
    /// ApiController с дополнительным функционалом
    /// </summary>
    public class BaseApiController: ApiController
    {
        /// <summary>
        /// Ответ сервера в зависимости от результата операции
        /// </summary>
        /// <typeparam name="T">тип результата</typeparam>
        /// <param name="result">объект результата</param>
        /// <returns>ответ сервера</returns>
        public IHttpActionResult ActionResult<T>(Result<T> result)
        {
            if (result.Success) 
                return Ok(result.Value);
            else 
                return BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Ответ сервера в зависимости от результата операции
        /// </summary>
        /// <param name="result">объект результата</param>
        /// <returns>ответ сервера</returns>
        public IHttpActionResult ActionResult(Result result)
        {
            if (result.Success)
                return Ok();
            else
                return BadRequest(result.ErrorMessage);
        }
    }
}