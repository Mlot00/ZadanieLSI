using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Domain;
using WebAPI.Models.DTO;
using WebAPI.Models.Helper;
using WebAPI.Repositories.Interface;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExportsController : ControllerBase
	{
		private readonly IExportRepository _exportRepository;

		public ExportsController(IExportRepository exportRepository)
		{
			_exportRepository = exportRepository;
		}

		/// <summary>
		/// Pobiera listę eksportów z możliwością filtrowania i paginacji.
		/// </summary>
		/// <param name="filter">Parametry filtrów i paginacji.</param>
		/// <returns>Strona wyników z eksportami.</returns>
		[HttpGet]
		public async Task<ActionResult<PagedResult<ExportResponseDto>>> GetExports([FromQuery] ExportFilterRequestDto filter)
		{
			PagedResult<Export> pagedResult = await _exportRepository.GetFilteredExportsAsync(filter);

			List<ExportResponseDto> responseDtos = pagedResult.Items.ToResponseDtos().ToList();

			var result = new PagedResult<ExportResponseDto>
			{
				Items = responseDtos,
				TotalCount = pagedResult.TotalCount
			};

			return Ok(result);
		}

		/// <summary>
		/// Tworzy nowy obiekt export.
		/// </summary>
		/// <param name="exportRequest">Nowy obiekt Export.</param>
		/// <returns>Zwraca nowy obiekt export.</returns>
		[HttpPost]
		public async Task<ActionResult> CreateExport([FromBody] ExportRequestDto exportRequest)
		{
			Export newExport = exportRequest.ToDomain();

			await _exportRepository.AddExportAsync(newExport);

			ExportResponseDto response = newExport.ToResponseDto();

			return Ok(response);
		}

	}
}
