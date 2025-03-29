using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Models.Domain;
using WebAPI.Models.DTO;
using WebAPI.Models.Helper;
using WebAPI.Repositories.Interface;

namespace TestProject
{
	public class UnitTest1
	{

		private readonly Fixture _fixture;
		private readonly Mock<IExportRepository> _exportRepositoryMock;
		private readonly ExportsController _controller;

        public UnitTest1()
        {
			_fixture = new Fixture();
			_exportRepositoryMock = new Mock<IExportRepository>();
			_controller = new ExportsController(_exportRepositoryMock.Object);
		}



		[Fact]
		public async Task GetExports_ReturnsOkResult_WithPagedResultOfExportResponseDto()
		{
			// Arrange
			var exports = _fixture.CreateMany<Export>(5).ToList();

			foreach (var export in exports)
			{
				export.ExportDateTime = DateTime.Now;
			}

			var pagedResult = new PagedResult<Export>
			{
				Items = exports,
				TotalCount = exports.Count
			};

			var filter = new ExportFilterRequestDto
			{
				PageNumber = 3,
				PageSize = 5
			};

			_exportRepositoryMock
				.Setup(repo => repo.GetFilteredExportsAsync(filter))
				.ReturnsAsync(pagedResult);

			// Act
			var actionResult = await _controller.GetExports(filter);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
			var resultPaged = Assert.IsAssignableFrom<PagedResult<ExportResponseDto>>(okResult.Value);
			Assert.Equal(exports.Count, resultPaged.TotalCount);
			Assert.Equal(exports.Select(e => e.ExportName), resultPaged.Items.Select(dto => dto.ExportName));
		}

		[Fact]
		public async Task GetExports_WhenRepositoryThrowsException_ShouldThrowException()
		{
			// Arrange
			var filter = new ExportFilterRequestDto
			{
				PageNumber = 1,
				PageSize = 10
			};

			_exportRepositoryMock
				.Setup(repo => repo.GetFilteredExportsAsync(filter))
				.ThrowsAsync(new Exception("Test exception"));

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _controller.GetExports(filter));
		}

		[Fact]
		public async Task CreateExport_ReturnsOkResult_WithExportResponseDto()
		{
			// Arrange
			var exportRequest = _fixture.Create<ExportRequestDto>();
			exportRequest.ExportDateTime = DateTime.Now; 

			_exportRepositoryMock
				.Setup(repo => repo.AddExportAsync(It.IsAny<Export>()))
				.Returns(Task.CompletedTask);

			// Act
			var actionResult = await _controller.CreateExport(exportRequest);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(actionResult);
			var responseDto = Assert.IsType<ExportResponseDto>(okResult.Value);
			Assert.Equal(exportRequest.ExportName, responseDto.ExportName);
			Assert.Equal(exportRequest.Username, responseDto.Username);
			Assert.Equal(exportRequest.Location, responseDto.Location);

			_exportRepositoryMock.Verify(repo => repo.AddExportAsync(It.Is<Export>(e =>
				e.ExportName == exportRequest.ExportName &&
				e.Username == exportRequest.Username &&
				e.Location == exportRequest.Location &&
				e.ExportDateTime == exportRequest.ExportDateTime
			)), Times.Once);
		}

		[Fact]
		public async Task CreateExport_WhenRepositoryThrowsException_ShouldThrowException()
		{
			// Arrange
			var exportRequest = _fixture.Create<ExportRequestDto>();
			exportRequest.ExportDateTime = DateTime.Now;

			_exportRepositoryMock
				.Setup(repo => repo.AddExportAsync(It.IsAny<Export>()))
				.ThrowsAsync(new Exception("Test exception"));

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _controller.CreateExport(exportRequest));
		}
	}
}