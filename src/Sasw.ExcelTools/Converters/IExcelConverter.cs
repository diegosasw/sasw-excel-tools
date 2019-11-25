namespace Sasw.ExcelTools.Converters
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IExcelConverter
    {
        /// <summary>
        /// Converts an Excel stream into a CSV stream.
        /// </summary>
        /// <param name="sourceStream">the Excel stream</param>
        /// <param name="isLegacyXls">flag to determine whether the Excel stream is from a legacy format</param>
        /// <returns>a CSV stream promise that should be disposed when used</returns>
        Task<Stream> ToCsvStream(Stream sourceStream, bool isLegacyXls = false);

        /// <summary>
        /// Converts an Excel stream into a CSV file.
        /// </summary>
        /// <param name="sourceStream">the Excel stream</param>
        /// <param name="destinationFilePath">the path to the destination CSV file</param>
        /// <param name="isLegacyXls">flag to determine whether the Excel stream is from a legacy format</param>
        /// <returns>a promise</returns>
        Task ToCsvFile(Stream sourceStream, string destinationFilePath, bool isLegacyXls = false);
    }
}