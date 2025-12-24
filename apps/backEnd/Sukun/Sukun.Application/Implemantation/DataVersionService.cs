using Microsoft.Extensions.Logging;
using Sukun.Application.Interfaces;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class DataVersionService : IDataVersionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataVersionRepository _dataVersionRepository;
        private readonly ILogger<DataVersionService> _logger;

        public DataVersionService(
            IUnitOfWork unitOfWork,
            IDataVersionRepository dataVersionRepository,
            ILogger<DataVersionService> logger)
        {
            _unitOfWork = unitOfWork;
            _dataVersionRepository = dataVersionRepository;
            _logger = logger;
        }

        public async Task<Dictionary<string, long>> GetAllVersionsAsync()
        {
            try
            {
                return await _dataVersionRepository.GetAllVersionsDictionaryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching data versions");
                return new Dictionary<string, long>();
            }
        }
    }
}