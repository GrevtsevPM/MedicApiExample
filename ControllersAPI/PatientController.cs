using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using MedicWebApp.CommonLogic;
using MedicWebApp.DataLogic;
using MedicWebApp.DTO;
using MedicWebApp.Enums;

namespace MedicWebApp.ControllersAPI
{
    /// <summary>
    /// Пациент - контроллер
    /// </summary>
    public class PatientController: BaseApiController
    {
        /// <summary>
        /// Пациент - Получение записи по ид для редактирования 
        /// </summary>
        /// <param name="id">ид Пациента</param>
        [HttpGet]
        public async Task<IHttpActionResult> GetPatient(int id)
        {
            return ActionResult(await (new PatientDataLogic()).GetPatientEditDto(id));
        }

        /// <summary>
        /// Пациент - Получение списка записей для формы списка с поддержкой сортировки и постраничного возврата данных
        /// </summary>
        /// <param name="sortField">текстовое значение соответствующего поля PatientListSortFieldEnum</param>
        /// <param name="sortDirection">asc или desc</param>
        /// <param name="pageNum">номер страницы выборки</param>
        [HttpGet]
        public async Task<IHttpActionResult> GetPatientViewDtoSortedList(PatientListSortFieldEnum sortField, SortDirectionEnum sortDirection, int pageNum)
        {
            PatientGetViewListParamDto paramDto = new PatientGetViewListParamDto()
            {
                SortField = sortField,
                SortDirection = sortDirection,
                PageNum = pageNum
            };

            return ActionResult(await (new PatientDataLogic()).GetPatientViewDtoSortedList(paramDto));
        }

        /// <summary>
        /// Пациент - Добавление записи
        /// </summary>
        /// <param name="paramDto">параметры</param>
        [HttpPost]
        public async Task<IHttpActionResult> CreatePatient([FromBody] PatientEditDto paramDto)
        {
            return ActionResult(await (new PatientDataLogic()).CreateUpdatePatient(paramDto, Enums.DataOperationTypeEnum.Create));
        }

        /// <summary>
        /// Пациент - Редактирование записи
        /// </summary>
        /// <param name="paramDto">параметры</param>
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePatient([FromBody] PatientEditDto paramDto)
        {
            return ActionResult(await (new PatientDataLogic()).CreateUpdatePatient(paramDto, Enums.DataOperationTypeEnum.Update));
        }

        /// <summary>
        /// Пациент - Удаление записи
        /// </summary>
        /// <param name="id">id Пациента</param>
        [HttpDelete]
        public async Task<IHttpActionResult> DeletePatient(int id)
        {
            return ActionResult(await (new PatientDataLogic()).DeletePatient(id));
        }

    }
}