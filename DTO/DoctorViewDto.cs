using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicWebApp.DTO
{
    /// <summary>
    /// Врач - данные для отображения
    /// </summary>
    public class DoctorViewDto
    {
        public int ID { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string FIO { get; set; }

        /// <summary>
        /// Кабинет Номер
        /// </summary>
        public int? RoomNumber { get; set; }

        /// <summary>
        /// Специализация Имя
        /// </summary>
        public string SpecialityName { get; set; }

        /// <summary>
        /// Участок Номер
        /// </summary>
        public int? MedDistrictNumber { get; set; }
    }
}