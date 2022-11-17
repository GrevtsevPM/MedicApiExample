using MedicWebApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicWebApp.DTO
{
    /// <summary>
    /// Врач - параметры получения списка записей для отображения
    /// </summary>
    public class DoctorGetViewListParamDto: BaseGetViewListParamDto
    {
        /// <summary>
        /// Сортировка - поле
        /// </summary>
        public DoctorListSortFieldEnum SortField;
    }
}