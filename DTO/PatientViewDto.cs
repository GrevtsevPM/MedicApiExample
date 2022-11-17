using MedicWebApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicWebApp.DTO
{
    /// <summary>
    /// Пациент - данные для отображения
    /// </summary>
    public class PatientViewDto
    {
        public int ID { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Пол текст
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// Участок Номер
        /// </summary>
        public int? MedDistrictNumber { get; set; }
    }
}