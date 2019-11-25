namespace Sasw.ExcelTools
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;
    using ExcelDataReader;
    using Exceptions;

    public class ExcelConverter
        : IExcelConverter
    {
        public ExcelConverter()
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public async Task<Stream> ToCsvStream(Stream sourceStream, bool isLegacyXls = false)
        {
            try
            {
                var reader = isLegacyXls
                    ? ExcelReaderFactory.CreateBinaryReader(sourceStream)
                    : ExcelReaderFactory.CreateOpenXmlReader(sourceStream);

                if (reader is null)
                {
                    throw new ArgumentException("Unsupported source stream");
                }

                var excelDataTableConfiguration =
                    new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = false
                    };

                var excelDataSetConfiguration =
                    new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = (tableReader) => excelDataTableConfiguration
                    };

                var ds = reader.AsDataSet(excelDataSetConfiguration);

                var csvContent = string.Empty;
                int rowNumber = 0;
                while (rowNumber < ds.Tables[0].Rows.Count)
                {
                    var arr = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        arr.Add(ds.Tables[0].Rows[rowNumber][i].ToString());
                    }

                    rowNumber++;
                    csvContent += string.Join(",", arr) + "\n";
                }

                sourceStream.Close();

                var destinationStream = new MemoryStream();
                var writer = new StreamWriter(destinationStream, Encoding.UTF8);
                await writer.WriteAsync(csvContent);
                await writer.FlushAsync();
                destinationStream.Position = 0;

                return destinationStream;
            }
            catch (Exception exception)
            {
                throw new CouldNotConvertException("Unable to convert the given Excel stream to a CSV stream", exception);
            }
        }

        public async Task ToCsvFile(Stream sourceStream, string destinationFilePath, bool isLegacyXls = false)
        {
            await using var csvStream = await ToCsvStream(sourceStream, isLegacyXls);
            await using var fileStream = File.Create(destinationFilePath);
            csvStream.Seek(0, SeekOrigin.Begin);
            csvStream.CopyTo(fileStream);
        }
    }
}
