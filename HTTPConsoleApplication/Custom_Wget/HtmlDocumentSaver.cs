using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Custom_Wget
{
    public static class HtmlDocumentSaver
    {
        private const string HtmlMediaType = "text/html";

        public static async Task<string> SaveHtmlDocument(HttpContent content, string uri, string path, string[] fileExtensions)
        {
            string filePath = CreateValidFilePath(content, path, uri);
            var fileExtension = Path.GetExtension(filePath);
            if (fileExtensions != null && fileExtensions.All(x => x != fileExtension))
                return String.Empty;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await content.CopyToAsync(fileStream).ContinueWith(
                        (copyTask) =>
                        {
                            fileStream.Close();
                        });
                return filePath;
            }
        }

        private static string CreateValidFilePath(HttpContent content, string directoryPath, string uri)
        {
            string fileName;
 
            if (content.Headers.ContentType.MediaType == HtmlMediaType)
            {
                var createdDate = DateTime.Now;
                fileName = $"{createdDate.Month}-{createdDate.Day}-{createdDate.Year}_{uri}.html";
            }
            else
            {
                fileName = uri.Split('/').Last();
            }

            fileName = RemoveInvalidFileNameChars(fileName);
            
            string filePath = $"{directoryPath}\\{fileName}";

            return filePath;
        }

        private static string RemoveInvalidFileNameChars(string filename)
        {
            var invalidSymbols = Path.GetInvalidFileNameChars();
            return new string(filename.Where(c => !invalidSymbols.Contains(c)).ToArray());
        }
    }
}
