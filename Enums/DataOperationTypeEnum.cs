using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicWebApp.Enums
{
    /// <summary>
    /// Тип операции производимой с данными
    /// </summary>
    public enum DataOperationTypeEnum
    {
        /// <summary>
        /// не задано
        /// </summary>

        NotSet = 0,

        /// <summary>
        /// Создание
        /// </summary>
        Create = 1,

        /// <summary>
        /// Обновление
        /// </summary>
        Update = 2
    }
}