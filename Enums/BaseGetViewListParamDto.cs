using MedicWebApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicWebApp.DTO
{
    /// <summary>
    /// Параметры получения списка записей для отображения - ,базовый класс
    /// </summary>
    public abstract class BaseGetViewListParamDto
    {
        /// <summary>
        /// Сортировка - направление
        /// </summary>
        public SortDirectionEnum SortDirection;

        /// <summary>
        /// Номер страницы
        /// </summary>
        public int PageNum;
    }
}