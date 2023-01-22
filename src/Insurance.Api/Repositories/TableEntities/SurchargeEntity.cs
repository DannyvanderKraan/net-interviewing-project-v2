using Azure;
using Azure.Data.Tables;
using Insurance.Api.Models;
using System;

namespace Insurance.Api.Repositories.TableEntities
{
    public class SurchargeEntity : ITableEntity
    {
        public SurchargeEntity() { } //Obligated empty constructor   
        public SurchargeEntity(SurchargeDto surchargeDto)
        {
            PartitionKey = CreatePartitionKey(surchargeDto.ProductTypeId);
            RowKey = CreateRowKey(surchargeDto.ProductTypeId);
            Surcharge = surchargeDto.Surcharge;
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public int Surcharge { get; set; }

        public static string CreatePartitionKey(int productTypeId)
        {
            return productTypeId.ToString();
        }

        public static string CreateRowKey(int productTypeId)
        {
            return productTypeId.ToString();
        }

        public SurchargeDto ToSurchargeDto()
        {
            return new SurchargeDto()
            {
                ProductTypeId = Convert.ToInt32(RowKey),
                Surcharge = Surcharge
            };
        }

    }
}
