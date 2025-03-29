namespace WebAPI.Models.Domain
{
	public class Export
	{
		public int Id { get; set; } 
		public string ExportName { get; set; } = string.Empty;
		public DateTime ExportDateTime { get; set; }
		public string Username { get; set; } = string.Empty;
		public string Location { get; set; } = string.Empty;
	}
}
