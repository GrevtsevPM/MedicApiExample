using MedicWebApp.DB;
using MedicWebApp.CommonLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using MedicWebApp.Enums;
using System.Threading.Tasks;
using MedicWebApp.DTO;
using System.Data.SqlClient;
using MedicWebApp.App_Start;
using System.Data.Entity;

namespace MedicWebApp.DataLogic
{
    /// <summary>
    /// Пациент - обмен данными с БД и логика
    /// </summary>
    public class PatientDataLogic
    {
        /// <summary>
        /// Пациент - получение данных записи для редактирования
        /// </summary>
        /// <param name="PatientId">ID в таблице tblPatient</param>
        public async Task<Result<PatientEditDto>> GetPatientEditDto(int PatientId)
        {
            using (MedicDB ctx = new MedicDB())
            {
                tblPatient PatientDbObj = await ctx.tblPatient.FindAsync(PatientId);
                if (PatientDbObj == null)
                    return Result<PatientEditDto>.ErrorResult($"Пациент не найден по ID {PatientId}");

                PatientEditDto PatientDto = new PatientEditDto()
                {
                    ID = PatientDbObj.ID,
                    Surname = PatientDbObj.Surname,
                    Name = PatientDbObj.Name,
                    Lastname = PatientDbObj.Lastname,
                    Address = PatientDbObj.Address,
                    Birthday = PatientDbObj.Birthday,
                    Gender = (GenderEnum)PatientDbObj.Gender,
                    MedDistrict = PatientDbObj.MedDistrict
                };

                return Result<PatientEditDto>.SuccessResult(PatientDto);
            }
        }

        /// <summary>
        /// Пациент - Получение списка записей для формы списка с поддержкой сортировки и постраничного возврата данных
        /// </summary>
        /// <param name="paramDto">параметры</param>        
        public async Task<Result<List<PatientViewDto>>> GetPatientViewDtoSortedList(PatientGetViewListParamDto paramDto)
        {
            using (MedicDB ctx = new MedicDB())
            {
                var patients = from patient in ctx.tblPatient select patient;

                if (paramDto.SortField != PatientListSortFieldEnum.NotSet && paramDto.SortDirection != SortDirectionEnum.NotSet)
                {
                    switch (paramDto.SortField)
                    {
                        case PatientListSortFieldEnum.Surname:
                            if (paramDto.SortDirection == SortDirectionEnum.Asc)
                                patients = patients.OrderBy(p => p.Surname);
                            else if (paramDto.SortDirection == SortDirectionEnum.Desc)
                                patients = patients.OrderByDescending(p => p.Surname);
                            break;
                        case PatientListSortFieldEnum.Name:
                            if (paramDto.SortDirection == SortDirectionEnum.Asc)
                                patients = patients.OrderBy(p => p.Name);
                            else if (paramDto.SortDirection == SortDirectionEnum.Desc)
                                patients = patients.OrderByDescending(p => p.Name);
                            break;
                        case PatientListSortFieldEnum.Lastname:
                            if (paramDto.SortDirection == SortDirectionEnum.Asc)
                                patients = patients.OrderBy(p => p.Lastname);
                            else if (paramDto.SortDirection == SortDirectionEnum.Desc)
                                patients = patients.OrderByDescending(p => p.Lastname);
                            break;
                        case PatientListSortFieldEnum.Address:
                            if (paramDto.SortDirection == SortDirectionEnum.Asc)
                                patients = patients.OrderBy(p => p.Address);
                            else if (paramDto.SortDirection == SortDirectionEnum.Desc)
                                patients = patients.OrderByDescending(p => p.Address);
                            break;
                        case PatientListSortFieldEnum.Birthday:
                            if (paramDto.SortDirection == SortDirectionEnum.Asc)
                                patients = patients.OrderBy(p => p.Birthday);
                            else if (paramDto.SortDirection == SortDirectionEnum.Desc)
                                patients = patients.OrderByDescending(p => p.Birthday);
                            break;
                        case PatientListSortFieldEnum.Gender:
                            if (paramDto.SortDirection == SortDirectionEnum.Asc)
                                patients = patients.OrderBy(p => p.Gender);
                            else if (paramDto.SortDirection == SortDirectionEnum.Desc)
                                patients = patients.OrderByDescending(p => p.Gender);
                            break;
                        case PatientListSortFieldEnum.MedDistrictNumber:
                            if (paramDto.SortDirection == SortDirectionEnum.Asc)
                                patients = patients.OrderBy(p => p.tblMedDistrict.Number);
                            else if (paramDto.SortDirection == SortDirectionEnum.Desc)
                                patients = patients.OrderByDescending(p => p.tblMedDistrict.Number);
                            break;
                    }
                }
                else
                {
                    //необходимо для skip - обязателен OrderBy
                    patients = patients.OrderBy(p => p.ID);
                }

                patients = patients.Skip(paramDto.PageNum * Constants.PatientGetViewListPageSize).Take(Constants.PatientGetViewListPageSize);

                var res = await patients.Select(p => new PatientViewDto()
                {
                    ID = p.ID,
                    Surname = p.Surname,
                    Name = p.Name,
                    Lastname = p.Lastname,
                    Address = p.Address,
                    Birthday = p.Birthday,
                    Gender = (GenderEnum)p.Gender,
                    MedDistrictNumber = p.tblMedDistrict.Number
                }).ToListAsync();

                return Result<List<PatientViewDto>>.SuccessResult(res);
            }
        }

        /// <summary>
        /// Пациент - создание / обновление записи в БД
        /// </summary>
        /// <param name="dto">данные</param>
        /// <param name="operation">операция Создание или Обновление</param>
        public async Task<Result> CreateUpdatePatient(PatientEditDto dto, DataOperationTypeEnum operation)
        {
            using (MedicDB ctx = new MedicDB())
            {
                if (operation == DataOperationTypeEnum.Update
                    && dto.ID == 0)
                    return Result.ErrorResult("Нет значения ID");

                if (string.IsNullOrWhiteSpace(dto.Surname))
                    return Result.ErrorResult("Поле Фамилия не заполнено. Обязательно для заполнения. Данные не сохранены.");

                if (string.IsNullOrWhiteSpace(dto.Name))
                    return Result.ErrorResult("Поле Имя не заполнено. Обязательно для заполнения. Данные не сохранены.");

                if (dto.MedDistrict.HasValue)
                {
                    if (await ctx.tblMedDistrict.FindAsync(dto.MedDistrict.Value) == null)
                        return Result.ErrorResult($"Участок не найден по ID (MedDistrict = {dto.MedDistrict.Value}). Данные не сохранены.");
                }

                tblPatient PatientDbObj;

                if (operation == DataOperationTypeEnum.Update)
                {
                    PatientDbObj = ctx.tblPatient.FindAsync(dto.ID).Result;
                    if (PatientDbObj == null)
                        return Result.ErrorResult($"Пациент не найден по ID {dto.ID}. Данные не сохранены.");
                }
                else if (operation == DataOperationTypeEnum.Create)
                    PatientDbObj = new tblPatient();
                else
                    throw new Exception($"Неверный параметр DataOperationTypeEnum operation {operation}");

                PatientDbObj.Surname = dto.Surname;
                PatientDbObj.Name = dto.Name;
                PatientDbObj.Lastname = dto.Lastname;
                PatientDbObj.Address = dto.Address;
                PatientDbObj.Birthday = dto.Birthday;
                PatientDbObj.Gender = (int)dto.Gender;
                PatientDbObj.MedDistrict = dto.MedDistrict;

                if (operation == DataOperationTypeEnum.Create)
                    ctx.tblPatient.Add(PatientDbObj);

                await ctx.SaveChangesAsync();
                return Result.SuccessResult();
            }
        }

        /// <summary>
        /// Пациент - удаление записи
        /// </summary>
        /// <param name="id">id Пациента</param>
        /// <returns></returns>
        public async Task<Result> DeletePatient(int id)
        {
            using (MedicDB ctx = new MedicDB())
            {
                tblPatient PatientDbObj = ctx.tblPatient.FindAsync(id).Result;
                if (PatientDbObj == null)
                    return Result.ErrorResult($"Пациент не найден по ID {id}.");
                ctx.tblPatient.Remove(PatientDbObj);
                await ctx.SaveChangesAsync();
                return Result.SuccessResult();
            }
        }
    }
}