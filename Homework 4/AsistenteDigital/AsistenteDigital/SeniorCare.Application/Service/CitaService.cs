using SeniorCare.Application.Contract;
using SeniorCare.Application.Core;
using SeniorCare.Application.Dtos.Citas;
using SeniorCare.Application.Dtos.Medicamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorCare.Application.Service
{
    public class CitaService : ICitaService
    {
        private readonly List<CitaDto> _citas = new List<CitaDto>();


        public async Task<ServiceResult<CitaDto>> Create(CitaDto dto)
        {
            dto.Id = Guid.NewGuid();
            _citas.Add(dto);

            return await Task.FromResult(new ServiceResult<CitaDto>
            {
                Success = true,
                Message = "Cita creada.",
                Data = dto
            });
        }

        public async Task<ServiceResult<bool>> Delete(Guid id)
        {
            var cita = _citas.FirstOrDefault(c => c.Id == id);
            if (cita == null)
            {
                return await Task.FromResult(new ServiceResult<bool>
                {
                    Success = false,
                    Message = "Cita no encontrada.",
                    Data = false
                });
            }

            _citas.Remove(cita);
            return await Task.FromResult(new ServiceResult<bool>
            {
                Success = true,
                Message = "Cita eliminada.",
                Data = true
            });
        }

        public async Task<ServiceResult<IEnumerable<CitaDto>>> GetAll()
        {
            return await Task.FromResult(new ServiceResult<IEnumerable<CitaDto>>
            {
                Success = true,
                Message = "Listado de citas obtenido.",
                Data = _citas
            });
        }

        public async Task<ServiceResult<CitaDto>> GetById(Guid id)
        {
            var cita = _citas.FirstOrDefault(c => c.Id == id);
            if (cita == null)
            {
                return await Task.FromResult(new ServiceResult<CitaDto>
                {
                    Success = false,
                    Message = "Cita no encontrada.",
                    Data = null
                });
            }

            return await Task.FromResult(new ServiceResult<CitaDto>
            {
                Success = true,
                Message = "Cita encontrada.",
                Data = cita
            });
        }

        public async Task<ServiceResult<CitaDto>> Update(Guid id, CitaDto dto)
        {
            var cita = _citas.FirstOrDefault(c => c.Id == id);
            if (cita == null)
            {
                return await Task.FromResult(new ServiceResult<CitaDto>
                {
                    Success = false,
                    Message = "Cita no encontrada.",
                    Data = null
                });
            }

            cita.Fecha = dto.Fecha;
            cita.Motivo = dto.Motivo;
            cita.Medico = dto.Medico;

            return await Task.FromResult(new ServiceResult<CitaDto>
            {
                Success = true,
                Message = "Cita actualizada.",
                Data = cita
            });
        }

        public async Task<ServiceResult<MedicamentoDto>> AgendarCitaMedicaAsync(MedicamentoDto dto)
        {
            return await Task.FromResult(new ServiceResult<MedicamentoDto>
            {
                Success = true,
                Message = "Cita médica agendada correctamente.",
                Data = dto
            });
        }
    }
}


