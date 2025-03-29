using WebAPI.Models.Domain;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Helper
{
	public static class ExportMappingExtensions
	{
		public static ExportResponseDto ToResponseDto(this Export export)
		{
			return new ExportResponseDto
			{
				Id = export.Id,
				ExportName = export.ExportName,
				ExportDateTime = export.ExportDateTime,
				Username = export.Username,
				Location = export.Location
			};
		}

		public static Export ToDomain(this ExportRequestDto request)
		{
			return new Export
			{
				ExportName = request.ExportName,
				ExportDateTime = request.ExportDateTime,
				Username = request.Username,
				Location = request.Location
			};
		}

		public static IEnumerable<ExportResponseDto> ToResponseDtos(this IEnumerable<Export> exports)
		{
			return exports.Select(e => e.ToResponseDto());
		}
	}
}

