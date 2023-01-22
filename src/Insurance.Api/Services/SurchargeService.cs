using Insurance.Api.Models;
using Insurance.Api.Repositories;
using Microsoft.Extensions.Logging;

namespace Insurance.Api.Services
{
    public class SurchargeService : ISurchargeService
    {
        private readonly ILogger<SurchargeService> _logger;
        private readonly ISurchargeRepository _surchargeRepository;

        public SurchargeService(ILogger<SurchargeService> logger,
            ISurchargeRepository surchargeRepository)
        {
            _logger = logger;
            _surchargeRepository = surchargeRepository;
        }

        /// <inheritdoc/>
        public virtual void AddSurcharge(SurchargeDto surchargeDto)
        {
            _surchargeRepository.AddSurcharge(surchargeDto);
        }

        /// <inheritdoc/>
        public virtual SurchargeDto GetSurcharge(int productTypeId)
        {
            return _surchargeRepository.GetSurcharge(productTypeId);
        }
    }
}
