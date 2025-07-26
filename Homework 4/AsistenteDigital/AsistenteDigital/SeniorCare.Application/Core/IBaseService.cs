using SeniorCare.Application.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorCare.Application.Core
{
    public interface IBaseService<T>
    {
        Task<ServiceResult<T>> Create(T dto);
        Task<ServiceResult<bool>> Delete(Guid id);
        Task<ServiceResult<T>> Update(Guid id, T dto);
        Task<ServiceResult<T>> GetById(Guid id);
        Task<ServiceResult<IEnumerable<T>>> GetAll();
    }
}
