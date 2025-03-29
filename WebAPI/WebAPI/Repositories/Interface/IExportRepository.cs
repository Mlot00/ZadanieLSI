using WebAPI.Models.Domain;
using WebAPI.Models.DTO;
using WebAPI.Models.Helper;

namespace WebAPI.Repositories.Interface
{
	public interface IExportRepository
	{
		Task<PagedResult<Export>> GetFilteredExportsAsync(ExportFilterRequestDto filter);
		Task AddExportAsync(Export export);
		Task<Export?> GetByIdAsync(int id);
	}
}
