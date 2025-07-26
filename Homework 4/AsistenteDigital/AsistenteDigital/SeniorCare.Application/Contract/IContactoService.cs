using SeniorCare.Application.Core;
using SeniorCare.Application.Dtos.Contactos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorCare.Application.Contract
{
    public interface IContactoService : IBaseService<ContactoDto>
    {
        Task<ServiceResult<ContactoDto>> AgregarContactoImportanteAsync(ContactoDto dto);
        Task<ServiceResult<bool>> EliminarContactoAsync(Guid id);
    }
}
