namespace WebAPI.Models.Helper
{
	public class PagedResult<T>
	{
		public List<T> Items { get; set; } = new();
		public int TotalCount { get; set; }
	}
}
