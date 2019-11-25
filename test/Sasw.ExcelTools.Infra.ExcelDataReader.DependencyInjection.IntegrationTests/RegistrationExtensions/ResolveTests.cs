namespace Sasw.ExcelTools.Infra.ExcelDataReader.DependencyInjection.IntegrationTests.RegistrationExtensions
{
    using Converters;
    using ExcelTools.Converters;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using TestSupport;
    using Xunit;

    public static class ResolveTests
    {
        public class Given_A_Service_Collection_With_Excel_Tools_When_Resolving_IExcelConverter
            : Given_When_Then_Test
        {
            private IServiceProvider _sut;
            private IExcelConverter _result;

            protected override void Given()
            {
                _sut = 
                    new ServiceCollection()
                        .AddExcelTools()
                        .BuildServiceProvider();
            }

            protected override void When()
            {
                _result = _sut.GetService<IExcelConverter>();
            }

            [Fact]
            public void Then_It_Should_Be_An_ExcelConverter()
            {
                _result.Should().BeAssignableTo<ExcelConverter>();
            }
        }

        public class Given_A_Service_Collection_With_Excel_Tools_And_An_IExcelConverter_Resolved_When_Resolving_A_Second_IExcelConverter
            : Given_When_Then_Test
        {
            private IServiceProvider _sut;
            private IExcelConverter _resultOne;
            private IExcelConverter _resultTwo;

            protected override void Given()
            {
                _sut =
                    new ServiceCollection()
                        .AddExcelTools()
                        .BuildServiceProvider();

                _resultOne = _sut.GetService<IExcelConverter>();
            }

            protected override void When()
            {
                _resultTwo = _sut.GetService<IExcelConverter>();
            }

            [Fact]
            public void Then_It_Should_Be_The_Same_Instance()
            {
                _resultOne.Should().BeSameAs(_resultTwo);
            }
        }

        public class Given_An_Empty_Service_Collection_When_Resolving_IExcelConverter
            : Given_When_Then_Test
        {
            private IServiceProvider _sut;
            private IExcelConverter _result;

            protected override void Given()
            {
                _sut =
                    new ServiceCollection()
                        .BuildServiceProvider();
            }

            protected override void When()
            {
                _result = _sut.GetService<IExcelConverter>();
            }

            [Fact]
            public void Then_It_Should_Be_Null()
            {
                _result.Should().BeNull();
            }
        }
    }
}