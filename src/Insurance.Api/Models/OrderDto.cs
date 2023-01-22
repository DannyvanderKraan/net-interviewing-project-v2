using System.Collections.Generic;

namespace Insurance.Api.Models
{
    public class OrderDto
    {
        public List<InsuranceDto> ProductsToInsure { get; set; } = new List<InsuranceDto>();
        public float InsuranceValue { get; set; }
    }
}
