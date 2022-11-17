using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicWebApp.Enums
{
    /// <summary>
    /// Направление сортировки
    /// </summary>
    public enum SortDirectionEnum
    {
        /// <summary>
        /// не задано
        /// </summary>
        
        NotSet = 0,

        /// <summary>
        /// по возрастанию
        /// </summary>
        Asc = 1,

        /// <summary>
        /// по убыванию
        /// </summary>
        Desc = 2
    }
}