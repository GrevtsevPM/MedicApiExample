using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicWebApp.DTO
{
    /// <summary>
    /// Врач - данные для редактирования записи
    /// </summary>
    public class DoctorEditDto
    {        
        public int ID { get; set; }
        
        /// <summary>
        /// ФИО
        /// </summary>
        public string FIO { get; set; }

        /// <summary>
        /// Кабинет ID
        /// </summary>
        public int? Room { get; set; }

        /// <summary>
        /// Специализация ID
        /// </summary>
        public int? Speciality { get; set; }

        /// <summary>
        /// Участок ID
        /// </summary>
        public int? MedDistrict { get; set; }
    }
}