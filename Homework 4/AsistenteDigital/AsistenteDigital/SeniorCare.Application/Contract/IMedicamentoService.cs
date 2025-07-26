using SeniorCare.Application.Core;
using SeniorCare.Application.Dtos.Medicamentos;
using System.Threading.Tasks;

namespace SeniorCare.Application.Contract
{
    public interface IMedicamentoService : IBaseService<MedicamentoDto>
    {
        Task<ServiceResult<MedicamentoDto>> RecordarMedicamentoAsync(int id);
        Task<ServiceResult<bool>> MarcarComoTomadoAsync(int id);
    }
}

