using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicWebApp.Enums
{
    /// <summary>
    /// Пациент - поля сортировки
    /// </summary>
    public enum PatientListSortFieldEnum
    {
        /// <summary>
        /// не задано
        /// </summary>        
        NotSet = 0,

        /// <summary>
        /// Фамилия
        /// </summary>
        Surname = 1,

        /// <summary>
        /// Имя
        /// </summary>
        Name = 2,

        /// <summary>
        /// Отчество
        /// </summary>
        Lastname = 3,

        /// <summary>
        /// Адрес
        /// </summary>
        Address = 4,

        /// <summary>
        /// Дата рождения
        /// </summary>
        Birthday = 5,

        /// <summary>
        /// Пол
        /// </summary>
        Gender = 6,

        /// <summary>
        /// Участок - номер
        /// </summary>
        MedDistrictNumber = 7
    }
}