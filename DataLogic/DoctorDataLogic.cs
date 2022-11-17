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
    /// Врач - обмен данными с БД и логика
    /// </summary>
    public class DoctorDataLogic
    {
        /// <summary>
        /// Врач - получение данных записи для редактирования
        /// </summary>
        /// <param name="doctorId">ID в таблице tblDoctor</param>
        public async Task<Result<DoctorEditDto>> GetDoctorEditDto(int doctorId)
        {
            using (MedicDB ctx = new MedicDB())
            {
                tblDoctor doctorDbObj = await ctx.tblDoctor.FindAsync(doctorId);
                if (doctorDbObj == null)
                    return Result<DoctorEditDto>.ErrorResult($"Врач не найден по ID {doctorId}");

                DoctorEditDto doctorDto = new DoctorEditDto()
                {
                    ID = doctorDbObj.ID,
                    FIO = doctorDbObj.FIO,
                    MedDistrict = doctorDbObj.MedDistrict,
                    Room = doctorDbObj.Room,
                    Speciality = doctorDbObj.Speciality
                };

                return Result<DoctorEditDto>.SuccessResult(doctorDto);
            }
        }

        /// <summary>
        /// Врач - Получение списка записей для формы списка с поддержкой сортировки и постраничного возврата данных (чистый SQL)
        /// </summary>
        /// <param name="paramDto">параметры</param>
        public Result<List<DoctorViewDto>> GetDoctorViewDtoSortedList(DoctorGetViewListParamDto paramDto)
        {
            using (MedicDB ctx = new MedicDB())
            {
                string sql = @"
                    SELECT  doc.ID,
		                    doc.FIO,
		                    room.Number AS RoomNumber,
		                    speciality.Name AS SpecialityName,
		                    meddistrict.Number AS MedDistrictNumber
                    FROM tblDoctor AS doc
	                    LEFT JOIN tblRoom AS room ON room.ID=doc.Room
	                    LEFT JOIN tblSpeciality AS speciality ON speciality.ID=doc.Speciality
	                    LEFT JOIN tblMedDistrict AS meddistrict ON meddistrict.ID=doc.MedDistrict
                    ORDER BY
	                    CASE WHEN @sortField='FIO' AND @sortDir='asc' THEN FIO END,
	                    CASE WHEN @sortField='FIO' AND @sortDir='desc' THEN FIO END DESC,
	                    CASE WHEN @sortField='RoomNumber' AND @sortDir='asc' THEN room.Number END,
	                    CASE WHEN @sortField='RoomNumber' AND @sortDir='desc' THEN room.Number END DESC,
	                    CASE WHEN @sortField='SpecialityName' AND @sortDir='asc' THEN speciality.Name END,
	                    CASE WHEN @sortField='SpecialityName' AND @sortDir='desc' THEN speciality.Name END DESC,
	                    CASE WHEN @sortField='MedDistrictNumber' AND @sortDir='asc' THEN meddistrict.Number END,
	                    CASE WHEN @sortField='MedDistrictNumber' AND @sortDir='desc' THEN meddistrict.Number END DESC
                    OFFSET @skipRows ROWS FETCH NEXT @takeRows ROWS ONLY;
                ";

                var res = ctx.Database.SqlQuery<DoctorViewDto>(sql
                    , new SqlParameter("@sortField", paramDto.SortField.ToString())
                    , new SqlParameter("@sortDir", paramDto.SortDirection.ToString())
                    , new SqlParameter("@skipRows", paramDto.PageNum * Constants.DoctorGetViewListPageSize)
                    , new SqlParameter("@takeRows", Constants.DoctorGetViewListPageSize)).ToList();
                return Result<List<DoctorViewDto>>.SuccessResult(res);
            }
        }

        /// <summary>
        /// Врач - Получение списка записей для формы списка с поддержкой сортировки и постраничного возврата данных (LinqToSql)
        /// </summary>
        /// <param name="paramDto">параметры</param>        
        public async Task<Result<List<DoctorViewDto>>> GetDoctorViewDtoSortedListUsingLinq(DoctorGetViewListParamDto paramDto)
        {
            using (MedicDB ctx = new MedicDB())
            {
                var doctors = from doc in ctx.tblDoctor select doc;

                if (paramDto.SortField != DoctorListSortFieldEnum.NotSet && paramDto.SortDirection != SortDirectionEnum.NotSet)
                {
                    switch (paramDto.SortField)
                    {
                        case DoctorListSortFieldEnum.FIO:
                            if (paramDto.SortDirection == SortDirectionEnum.Asc)
                                doctors = doctors.OrderBy(d => d.FIO);
                            else if (paramDto.SortDirection == SortDirectionEnum.Desc)
                                doctors = doctors.OrderByDescending(d => d.FIO);
                            break;
                        case DoctorListSortFieldEnum.RoomNumber:
                            if (paramDto.SortDirection == SortDirectionEnum.Asc)
                                doctors = doctors.OrderBy(d => d.tblRoom.Number);
                            else if (paramDto.SortDirection == SortDirectionEnum.Desc)
                                doctors = doctors.OrderByDescending(d => d.tblRoom.Number);
                            break;
                        case DoctorListSortFieldEnum.SpecialityName:
                            if (paramDto.SortDirection == SortDirectionEnum.Asc)
                                doctors = doctors.OrderBy(d => d.tblSpeciality.Name);
                            else if (paramDto.SortDirection == SortDirectionEnum.Desc)
                                doctors = doctors.OrderByDescending(d => d.tblSpeciality.Name);
                            break;
                        case DoctorListSortFieldEnum.MedDistrictNumber:
                            if (paramDto.SortDirection == SortDirectionEnum.Asc)
                                doctors = doctors.OrderBy(d => d.tblMedDistrict.Number);
                            else if (paramDto.SortDirection == SortDirectionEnum.Desc)
                                doctors = doctors.OrderByDescending(d => d.tblMedDistrict.Number);
                            break;
                    }
                }
                else
                {
                    //необходимо для skip - обязателен OrderBy
                    doctors = doctors.OrderBy(d => d.ID);
                }

                doctors = doctors.Skip(paramDto.PageNum * Constants.DoctorGetViewListPageSize).Take(Constants.DoctorGetViewListPageSize);

                var res = await doctors.Select(d => new DoctorViewDto()
                {
                    ID = d.ID,
                    FIO = d.FIO,
                    RoomNumber = d.tblRoom.Number,
                    SpecialityName = d.tblSpeciality.Name,
                    MedDistrictNumber = d.tblMedDistrict.Number
                }).ToListAsync();

                return Result<List<DoctorViewDto>>.SuccessResult(res);
            }
        }


        /// <summary>
        /// Врач - создание / обновление записи в БД
        /// </summary>
        /// <param name="dto">данные</param>
        /// <param name="operation">операция Создание или Обновление</param>
        public async Task<Result> CreateUpdateDoctor(DoctorEditDto dto, DataOperationTypeEnum operation)
        {
            using (MedicDB ctx = new MedicDB())
            {
                if (operation == DataOperationTypeEnum.Update
                    && dto.ID == 0)
                    return Result.ErrorResult("Нет значения ID");

                if (string.IsNullOrWhiteSpace(dto.FIO))
                    return Result.ErrorResult("Поле FIO не заполнено. Обязательно для заполнения. Данные не сохранены.");

                if (dto.MedDistrict.HasValue)
                {
                    if (await ctx.tblMedDistrict.FindAsync(dto.MedDistrict.Value) == null)
                        return Result.ErrorResult($"Участок не найден по ID (MedDistrict = {dto.MedDistrict.Value}). Данные не сохранены.");
                }

                if (dto.Room.HasValue)
                {
                    if (await ctx.tblRoom.FindAsync(dto.Room.Value) == null)
                        return Result.ErrorResult($"Кабинет не найден по ID (Room = {dto.Room.Value}). Данные не сохранены.");
                }

                if (dto.Speciality.HasValue)
                {
                    if (await ctx.tblSpeciality.FindAsync(dto.Speciality.Value) == null)
                        return Result.ErrorResult($"Специализация не найдена по ID (Speciality = {dto.Speciality.Value}). Данные не сохранены.");
                }

                tblDoctor doctorDbObj;

                if (operation == DataOperationTypeEnum.Update)
                {
                    doctorDbObj = ctx.tblDoctor.FindAsync(dto.ID).Result;
                    if (doctorDbObj == null)
                        return Result.ErrorResult($"Врач не найден по ID {dto.ID}. Данные не сохранены.");
                }
                else if (operation == DataOperationTypeEnum.Create)
                    doctorDbObj = new tblDoctor();
                else
                    throw new Exception($"Неверный параметр DataOperationTypeEnum operation {operation}");

                doctorDbObj.FIO = dto.FIO;
                doctorDbObj.MedDistrict = dto.MedDistrict;
                doctorDbObj.Room = dto.Room;
                doctorDbObj.Speciality = dto.Speciality;

                if (operation == DataOperationTypeEnum.Create)
                    ctx.tblDoctor.Add(doctorDbObj);

                await ctx.SaveChangesAsync();
                return Result.SuccessResult();
            }
        }

        /// <summary>
        /// Врач - удаление записи
        /// </summary>
        /// <param name="id">id врача</param>
        /// <returns></returns>
        public async Task<Result> DeleteDoctor(int id)
        {
            using (MedicDB ctx = new MedicDB())
            {
                tblDoctor doctorDbObj = ctx.tblDoctor.FindAsync(id).Result;
                if (doctorDbObj == null)
                    return Result.ErrorResult($"Врач не найден по ID {id}.");
                ctx.tblDoctor.Remove(doctorDbObj);
                await ctx.SaveChangesAsync();
                return Result.SuccessResult();
            }
        }
    }
}