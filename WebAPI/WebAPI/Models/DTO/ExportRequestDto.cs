namespace WebAPI.Models.DTO
{
	public class ExportRequestDto
	{
		public string ExportName { get; set; } = string.Empty;
		public DateTime ExportDateTime { get; set; }
		public string Username { get; set; } = string.Empty;
		public string Location { get; set; } = string.Empty;
	}
}
