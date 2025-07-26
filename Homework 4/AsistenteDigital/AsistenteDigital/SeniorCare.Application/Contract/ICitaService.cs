using SeniorCare.Application.Core;
using SeniorCare.Application.Dtos.Citas;
using SeniorCare.Application.Dtos.Medicamentos;
using System.Threading.Tasks;

namespace SeniorCare.Application.Contract
{
    public interface ICitaService : IBaseService<CitaDto>
    {
        Task<ServiceResult<MedicamentoDto>> AgendarCitaMedicaAsync(MedicamentoDto dto);
    }
}
