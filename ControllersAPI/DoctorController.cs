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
    /// Врач - контроллер
    /// </summary>
    public class DoctorController: BaseApiController
    {
        /// <summary>
        /// Врач - Получение записи по ид для редактирования 
        /// </summary>
        /// <param name="id">ид врача</param>
        [HttpGet]        
        public async Task<IHttpActionResult> GetDoctor(int id)
        {
            return ActionResult(await (new DoctorDataLogic()).GetDoctorEditDto(id));
        }

        /// <summary>
        /// Врач - Получение списка записей для формы списка с поддержкой сортировки и постраничного возврата данных
        /// </summary>
        /// <param name="sortField">текстовое значение соответствующего поля DoctorListSortFieldEnum</param>
        /// <param name="sortDirection">asc или desc</param>
        /// <param name="pageNum">номер страницы выборки</param>
        [HttpGet]
        public async Task<IHttpActionResult> GetDoctorViewDtoSortedList(DoctorListSortFieldEnum sortField, SortDirectionEnum sortDirection, int pageNum)
        {
            DoctorGetViewListParamDto paramDto = new DoctorGetViewListParamDto()
            {
                SortField = sortField,
                SortDirection = sortDirection,
                PageNum = pageNum
            };

            //return ActionResult((new DoctorDataLogic()).GetDoctorViewDtoSortedList(paramDto));
            return ActionResult(await (new DoctorDataLogic()).GetDoctorViewDtoSortedListUsingLinq(paramDto));

        }

        /// <summary>
        /// Врач - Добавление записи
        /// </summary>
        /// <param name="paramDto">параметры</param>
        [HttpPost]
        public async Task<IHttpActionResult> CreateDoctor([FromBody] DoctorEditDto paramDto)
        {
            return ActionResult(await (new DoctorDataLogic()).CreateUpdateDoctor(paramDto, Enums.DataOperationTypeEnum.Create));
        }

        /// <summary>
        /// Врач - Редактирование записи
        /// </summary>
        /// <param name="paramDto">параметры</param>
        [HttpPost]
        public async Task<IHttpActionResult> UpdateDoctor([FromBody] DoctorEditDto paramDto)
        {
            return ActionResult(await (new DoctorDataLogic()).CreateUpdateDoctor(paramDto, Enums.DataOperationTypeEnum.Update));
        }

        /// <summary>
        /// Врач - Удаление записи
        /// </summary>
        /// <param name="id">id врача</param>
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteDoctor(int id)
        {
            return ActionResult(await (new DoctorDataLogic()).DeleteDoctor(id));
        }
    }
}