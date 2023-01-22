using Azure.Data.Tables;
using Insurance.Api.Models;
using Insurance.Api.Repositories.TableEntities;
using Insurance.Api.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Insurance.Api.Repositories
{
    public class SurchargeRepository : ISurchargeRepository
    {
        /// <inheritdoc/>
        public string ContainerName { get; set; } = "Surcharge";

        private readonly ILogger<SurchargeRepository> _logger;
        private readonly TableClient _tableClient;
        public SurchargeRepository(ILogger<SurchargeRepository> logger, 
            IOptions<ConnectionStringsSettings> connectionStringsSettings)
        {
            _logger = logger;
            _tableClient = new TableClient(connectionStringsSettings.Value.Default, ContainerName);
            _tableClient.CreateIfNotExists(); //Perhaps better to create these tables beforehand than runtime in production code ;-)
        }

        /// <inheritdoc/>
        public virtual void AddSurcharge(SurchargeDto surchargeDto)
        {
            var response = _tableClient.UpsertEntity(new SurchargeEntity(surchargeDto)); //The default table mode merge is fine because there's only 1 value that changes: Surcharge.
            if (response.IsError) _logger.LogError("Failed to upsert surcharge for product type id {0}", surchargeDto.ProductTypeId);
        }

        /// <inheritdoc/>
        public virtual SurchargeDto GetSurcharge(int productTypeId)
        {
            var response = _tableClient.GetEntityIfExists<SurchargeEntity>(SurchargeEntity.CreatePartitionKey(productTypeId),
                SurchargeEntity.CreateRowKey(productTypeId));
            return response.HasValue ? response.Value.ToSurchargeDto() : null;
        }
    }
}
