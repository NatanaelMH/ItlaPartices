using SeniorCare.Application.Contract;
using SeniorCare.Application.Core;
using SeniorCare.Application.Dtos.Contactos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorCare.Application.Service
{
    public class ContactoService : IContactoService
    {
        private readonly List<ContactoDto> _contactos = new List<ContactoDto>();

        public async Task<ServiceResult<ContactoDto>> Create(ContactoDto dto)
        {
            dto.Id = Guid.NewGuid();
            _contactos.Add(dto);

            return await Task.FromResult(new ServiceResult<ContactoDto>
            {
                Success = true,
                Message = "Contacto creado exitosamente.",
                Data = dto
            });
        }

        public async Task<ServiceResult<bool>> Delete(Guid id)
        {
            var contacto = _contactos.FirstOrDefault(c => c.Id == id);
            if (contacto == null)
            {
                return await Task.FromResult(new ServiceResult<bool>
                {
                    Success = false,
                    Message = "Contacto no encontrado.",
                    Data = false
                });
            }

            _contactos.Remove(contacto);
            return await Task.FromResult(new ServiceResult<bool>
            {
                Success = true,
                Message = "Contacto eliminado.",
                Data = true
            });
        }

        public async Task<ServiceResult<IEnumerable<ContactoDto>>> GetAll()
        {
            return await Task.FromResult(new ServiceResult<IEnumerable<ContactoDto>>
            {
                Success = true,
                Message = "Lista de contactos obtenida.",
                Data = _contactos
            });
        }

        public async Task<ServiceResult<ContactoDto>> GetById(Guid id)
        {
            var contacto = _contactos.FirstOrDefault(c => c.Id == id);
            if (contacto == null)
            {
                return await Task.FromResult(new ServiceResult<ContactoDto>
                {
                    Success = false,
                    Message = "Contacto no encontrado.",
                    Data = null
                });
            }

            return await Task.FromResult(new ServiceResult<ContactoDto>
            {
                Success = true,
                Message = "Contacto encontrado.",
                Data = contacto
            });
        }

        public async Task<ServiceResult<ContactoDto>> Update(Guid id, ContactoDto dto)
        {
            var contacto = _contactos.FirstOrDefault(c => c.Id == id);
            if (contacto == null)
            {
                return await Task.FromResult(new ServiceResult<ContactoDto>
                {
                    Success = false,
                    Message = "Contacto no encontrado.",
                    Data = null
                });
            }

            contacto.Nombre = dto.Nombre;
            contacto.Relacion = dto.Relacion;
            contacto.Telefono = dto.Telefono;
            contacto.Direccion = dto.Direccion;

            return await Task.FromResult(new ServiceResult<ContactoDto>
            {
                Success = true,
                Message = "Contacto actualizado.",
                Data = contacto
            });
        }

        public async Task<ServiceResult<ContactoDto>> AgregarContactoImportanteAsync(ContactoDto dto)
        {
            dto.Id = Guid.NewGuid();
            _contactos.Add(dto);

            return await Task.FromResult(new ServiceResult<ContactoDto>
            {
                Success = true,
                Message = "Contacto importante agregado.",
                Data = dto
            });
        }

        public async Task<ServiceResult<bool>> EliminarContactoAsync(Guid id)
        {
            var contacto = _contactos.FirstOrDefault(c => c.Id == id);
            if (contacto == null)
            {
                return await Task.FromResult(new ServiceResult<bool>
                {
                    Success = false,
                    Message = "Contacto no encontrado.",
                    Data = false
                });
            }

            _contactos.Remove(contacto);
            return await Task.FromResult(new ServiceResult<bool>
            {
                Success = true,
                Message = "Contacto eliminado.",
                Data = true
            });
        }
    }
}


