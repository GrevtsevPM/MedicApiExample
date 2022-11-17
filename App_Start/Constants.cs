using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicWebApp.App_Start
{
    /// <summary>
    /// Константы
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Врач - количество элементов страницы выборки при получении списка записей для отображения
        /// </summary>
        public static readonly int DoctorGetViewListPageSize = 3;

        /// <summary>
        /// Пациент - количество элементов страницы выборки при получении списка записей для отображения
        /// </summary>
        public static readonly int PatientGetViewListPageSize = 3;
    }
}