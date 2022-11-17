using MedicWebApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicWebApp.DTO
{
    /// <summary>
    /// Пациент - параметры получения списка записей для отображения
    /// </summary>
    public class PatientGetViewListParamDto: BaseGetViewListParamDto
    {
        /// <summary>
        /// Сортировка - поле
        /// </summary>
        public PatientListSortFieldEnum SortField;
    }
}