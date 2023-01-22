using Castle.Core.Logging;
using Insurance.Api.Controllers;
using Insurance.Api.Models;
using Insurance.Api.Repositories;
using Insurance.Api.Services;
using Insurance.Api.Settings;
using Insurance.Tests.Fixtures;
using Insurance.Tests.Mocks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.Core.Arguments;
using System;
using System.Linq;
using Xunit;

namespace Insurance.Tests
{
    public class InsuranceTests : IClassFixture<ControllerTestFixture>
    {
        private const int ProductIdForLaptopTest = 99;

        private readonly ControllerTestFixture _fixture;

        public InsuranceTests(ControllerTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceBetween500And2000Euros_ShouldAddThousandEurosToInsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue = 1000;

            var dto = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdWithSalesPricesBetween500And2000,
            };
            var sut = CreatSut();

            //Act
            var result = sut.Sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceBetween500And2000EurosNotInsuranced_ShouldMakeInsuranceCostZero()
        {
            //Arrange
            const float expectedInsuranceValue = 0;

            var dto = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdWithSalesPricesBetween500And2000NotInsured,
            };
            var sut = CreatSut();

            //Act
            var result = sut.Sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceOver2000Euros_ShouldAddTwoThousandEurosToInsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue = 2000;

            var dto = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdSalesPricesHigherThan2000,
            };
            var sut = CreatSut();

            //Act
            var result = sut.Sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceOver2000EurosNoInsurance_ShouldMakeInsuranceCostZero()
        {
            //Arrange
            const float expectedInsuranceValue = 0;

            var dto = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdSalesPricesHigherThan2000NotInsured,
            };
            var sut = CreatSut();

            //Act
            var result = sut.Sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenLaptopWithSalesPriceLessThan500_ShouldMakeInsuranceCostFiveHundredEuros()
        {
            //Arrange
            const float expectedInsuranceValue = 500;

            var dto = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdLaptopWithSalesPricesLowerThan500
            };
            var sut = CreatSut();

            //Act
            var result = sut.Sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenLaptopWithSalesPriceLessThan500NoInsurance_ShouldMakeInsuranceCostZero()
        {
            //Arrange
            const float expectedInsuranceValue = 0;

            var dto = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdLaptopWithSalesPricesLowerThan500NotInsured
            };
            var sut = CreatSut();

            //Act
            var result = sut.Sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }


        [Fact]
        public void CalculateInsurance_GivenSmartphoneWithSalesPriceLessThan500_ShouldMakeInsuranceCostZeroEuros()
        {
            //Arrange
            const float expectedInsuranceValue = 0;

            var dto = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdSmartphoneWithSalesPricesLowerThan500
            };
            var sut = CreatSut();

            //Act
            var result = sut.Sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenSmartphoneWithSalesPriceLessThan500NoInsurance_ShouldMakeInsuranceCostZero()
        {
            //Arrange
            const float expectedInsuranceValue = 0;

            var dto = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdSmartphoneWithSalesPricesLowerThan500NotInsured
            };
            var sut = CreatSut();

            //Act
            var result = sut.Sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenLaptopWithSalesPriceHigherThan500_ShouldAddFiveHundredEurosToInsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue = 1500;

            var dto = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdLaptopWithSalesPricesHigherThan500
            };
            var sut = CreatSut();

            //Act
            var result = sut.Sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenLaptopWithSalesPriceHigherThan500NoInsurance_ShouldMakeInsuranceCostZero()
        {
            //Arrange
            const float expectedInsuranceValue = 0;

            var dto = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdLaptopWithSalesPricesHigherThan500NotInsured
            };
            var sut = CreatSut();

            //Act
            var result = sut.Sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenSmartphoneWithSalesPriceHigherThan500_ShouldAddFiveHundredEurosToInsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue = 1500;

            var dto = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdSmartphoneWithSalesPricesHigherThan500
            };
            var sut = CreatSut();

            //Act
            var result = sut.Sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenSmartphoneWithSalesPriceHigherThan500NoInsurance_ShouldMakeInsuranceCostZero()
        {
            //Arrange
            const float expectedInsuranceValue = 0;

            var dto = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdSmartphoneWithSalesPricesHigherThan500NotInsured
            };
            var sut = CreatSut();

            //Act
            var result = sut.Sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsuranceOrder_GivenOrder_ShouldCalculateCorrectInsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue1 = 1500;
            const float expectedInsuranceValue2 = 500;
            const float expectedInsuranceValue = 2000;

            var dto1 = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdSmartphoneWithSalesPricesHigherThan500
            };
            var dto2 = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdLaptopWithSalesPricesLowerThan500
            };
            var orderDto = new OrderDto() { ProductsToInsure = new System.Collections.Generic.List<InsuranceDto> { dto1, dto2 } };
            var sut = CreatSut();

            //Act
            var result = sut.Sut.CalculateInsuranceOrder(orderDto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue1,
                actual: result.ProductsToInsure.Single(p => p.ProductId == ControllerTestStartup.ProductIdSmartphoneWithSalesPricesHigherThan500).InsuranceValue
            );
            Assert.Equal(
                expected: expectedInsuranceValue2,
                actual: result.ProductsToInsure.Single(p => p.ProductId == ControllerTestStartup.ProductIdLaptopWithSalesPricesLowerThan500).InsuranceValue
                );
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
                );
        }

        [Fact]
        public void CalculateInsuranceOrder_GivenOrderWithDigitalCamera_ShouldAdd500EurosToTotalInsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue1 = 1500;
            const float expectedInsuranceValue2 = 0;
            const float expectedInsuranceValue = 2000;

            var dto1 = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdSmartphoneWithSalesPricesHigherThan500
            };
            var dto2 = new InsuranceDto
            {
                ProductId = ControllerTestStartup.ProductIdDigitalCamerasWithSalesPricesLowerThan500
            };
            var orderDto = new OrderDto() { ProductsToInsure = new System.Collections.Generic.List<InsuranceDto> { dto1, dto2 } };
            var sut = CreatSut();

            //Act
            var result = sut.Sut.CalculateInsuranceOrder(orderDto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue1,
                actual: result.ProductsToInsure.Single(p => p.ProductId == ControllerTestStartup.ProductIdSmartphoneWithSalesPricesHigherThan500).InsuranceValue
            );
            Assert.Equal(
                expected: expectedInsuranceValue2,
                actual: result.ProductsToInsure.Single(p => p.ProductId == ControllerTestStartup.ProductIdDigitalCamerasWithSalesPricesLowerThan500).InsuranceValue
                );
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
                );
        }

        [Fact]
        public void AddSurcharge_GivenProductTypeIdAnd500Euros_ShouldAdd500EurosSurchargeToProvidedProductTypeId()
        {
            //Arrange
            var surchargeDto = new SurchargeDto()
            {
                ProductTypeId = ControllerTestStartup.ProductTypeIdGeneric,
                Surcharge = 500
            };
            var sut = CreatSut();

            //Act
            sut.Sut.AddSurcharge(surchargeDto);

            //Assert
            sut.SurchargeRepository.ReceivedWithAnyArgs(1).AddSurcharge(surchargeDto);
        }

        [Fact]
        public void GetSurcharge_GivenProductTypeId_ShouldGetSurchargeForProvidedProductTypeId()
        {
            //Arrange
            var productTypeId = ControllerTestStartup.ProductTypeIdGeneric;
            var surcharge = 500;
            var expected = new SurchargeDto()
            {
                ProductTypeId = productTypeId,
                Surcharge = surcharge
            };
            var sut = CreatSut();
            sut.SurchargeRepository.GetSurcharge(Arg.Any<int>()).ReturnsForAnyArgs(expected);

            //Act
            var actual = sut.SurchargeService.GetSurcharge(productTypeId);

            //Assert
            Assert.Equal(expected: expected.Surcharge, actual: actual.Surcharge);
        }

        private static CalculateInsuranceTestsSut CreatSut()
        {
            //Loggers
            var insuranceControllerLogger = Substitute.For<ILogger<InsuranceController>>();
            var productRepositoryLogger = Substitute.For<ILogger<ProductRepository>>();
            var productTypeRepositoryLogger = Substitute.For<ILogger<ProductTypeRepository>>();
            var surchargeServiceLogger = Substitute.For<ILogger<SurchargeService>>();

            //Settings
            var productAPISettings = Substitute.For<IOptions<ProductAPISettings>>();
            productAPISettings.Value.Returns(new ProductAPISettings() { Url = "http://localhost:5002" });

            //Repos
            var productRepo = new ProductRepository(productRepositoryLogger, productAPISettings);
            var productTypeRepo = new ProductTypeRepository(productTypeRepositoryLogger, productAPISettings);
            var surchargeRepo = Substitute.For<ISurchargeRepository>();

            //Services
            var surchargeService = new SurchargeService(surchargeServiceLogger, surchargeRepo);
            var insuranceCalculator = new InsuranceCalculator(productRepo, productTypeRepo, surchargeService);

            //SUT
            var sut = new InsuranceController(insuranceControllerLogger, insuranceCalculator, surchargeService);

            return new CalculateInsuranceTestsSut()
            {
                ProductAPISettings = productAPISettings,
                ProductTypeRepo = productTypeRepo,
                SurchargeRepository = surchargeRepo,
                InsuranceCalculator = insuranceCalculator,
                ProductRepo = productRepo,
                SurchargeService = surchargeService,
                Sut = sut
            };
        }

        private class CalculateInsuranceTestsSut
        {
            public IOptions<ProductAPISettings> ProductAPISettings { get; set; }
            public ProductRepository ProductRepo { get; set; }
            public ProductTypeRepository ProductTypeRepo { get; set; }
            public ISurchargeRepository SurchargeRepository { get; set; }
            public ISurchargeService SurchargeService { get; set; }
            public InsuranceCalculator InsuranceCalculator { get; set; }
            public InsuranceController Sut { get; set; }
        }
    }
}