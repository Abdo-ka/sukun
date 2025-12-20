using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public abstract class BaseService
    {
        protected readonly ILogger _logger;
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected PagedResponseDto<T> CreatePagedResponse<T>(
            List<T> items, int pageNumber, int pageSize, int totalCount)
        {
            return new PagedResponseDto<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                HasNextPage = pageNumber < (int)Math.Ceiling(totalCount / (double)pageSize),
                HasPreviousPage = pageNumber > 1,
                Items = items
            };
        }
    }
}