namespace Sasw.ExcelTools.UnitTests.Converters.ExcelConverterTests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using ExcelTools.Converters;
    using FluentAssertions;
    using TestSupport;
    using Xunit;

    public static class ToCsvStreamTests
    {
        public class Given_An_Excel_Stream_When_Converting_To_Csv_Stream
            : Given_WhenAsync_Then_Test
        {
            private ExcelConverter _sut;
            private Stream _sourceStream;
            private Exception _exception;
            private Stream _result;

            protected override void Given()
            {
                _sut = new ExcelConverter();
                var pathToFile = Path.Combine("TestSupport", "Samples", "test.xls");
                _sourceStream = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }

            protected override async Task WhenAsync()
            {
                try
                {
                    _result = await _sut.ToCsvStream(_sourceStream);
                }
                catch (Exception exception)
                {
                    _exception = exception;
                }
            }

            [Fact]
            public void Then_It_Should_Not_Throw_Any_Exception()
            {
                _exception.Should().BeNull();
            }

            [Fact]
            public void Then_It_Return_A_Valid_Csv_Stream()
            {
                _result.Should().NotBeNull();
            }

            [Fact]
            public void Then_It_Should_Have_Readable_Result()
            {
                _result.CanRead.Should().BeTrue();
            }

            [Fact]
            public void Then_It_Should_Not_Have_Readable_Source_Stream()
            {
                _sourceStream.CanRead.Should().BeFalse();
            }

            [Fact]
            public void Then_It_Should_Not_Have_Writable_Source_Stream()
            {
                _sourceStream.CanWrite.Should().BeFalse();
            }

            [Fact]
            public void Then_It_Should_Not_Have_Seekable_Source_Stream()
            {
                _sourceStream.CanSeek.Should().BeFalse();
            }

            protected override void Cleanup()
            {
                base.Cleanup();
                _result.Close();
            }
        }
    }
}