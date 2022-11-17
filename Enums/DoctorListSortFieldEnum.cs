using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicWebApp.Enums
{
    /// <summary>
    /// Доктор - поля сортировки
    /// </summary>
    public enum DoctorListSortFieldEnum
    {
        /// <summary>
        /// не задано
        /// </summary>        
        NotSet = 0,

        /// <summary>
        /// ФИО
        /// </summary>
        FIO = 1,

        /// <summary>
        /// Кабинет - номер
        /// </summary>
        RoomNumber = 2,

        /// <summary>
        /// Специализация - имя
        /// </summary>
        SpecialityName = 3,

        /// <summary>
        /// Участок - номер
        /// </summary>
        MedDistrictNumber = 4
    }
}