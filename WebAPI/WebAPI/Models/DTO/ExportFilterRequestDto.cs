namespace WebAPI.Models.DTO
{
	public class ExportFilterRequestDto
	{
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
		public string? Location { get; set; }
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
	}
}
