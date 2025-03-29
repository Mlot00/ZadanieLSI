using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.Domain;
using WebAPI.Models.DTO;
using WebAPI.Models.Helper;
using WebAPI.Repositories.Interface;

namespace WebAPI.Repositories.Implementation
{
	public class ExportRepository : IExportRepository
	{
		private readonly ApplicationDbContext _context;

		public ExportRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<PagedResult<Export>> GetFilteredExportsAsync(ExportFilterRequestDto filter)
		{
			var query = _context.Exports.AsQueryable();

			if (filter.FromDate.HasValue)
			{
				query = query.Where(e => e.ExportDateTime >= filter.FromDate.Value);
			}

			if (filter.ToDate.HasValue)
			{
				query = query.Where(e => e.ExportDateTime <= filter.ToDate.Value);
			}

			if (!string.IsNullOrWhiteSpace(filter.Location))
			{
				query = query.Where(e => e.Location.Contains(filter.Location));
			}

			var totalCount = await query.CountAsync();

			var items = await query
				.OrderByDescending(e => e.ExportDateTime)
				.Skip((filter.PageNumber - 1) * filter.PageSize)
				.Take(filter.PageSize)
				.ToListAsync();

			return new PagedResult<Export>
			{
				Items = items,
				TotalCount = totalCount
			};
		}

		public async Task AddExportAsync(Export export)
		{
			await _context.Exports.AddAsync(export);
			await _context.SaveChangesAsync();
		}

		public async Task<Export?> GetByIdAsync(int id)
		{
			return await _context.Exports.FindAsync(id);
		}
	}
}
