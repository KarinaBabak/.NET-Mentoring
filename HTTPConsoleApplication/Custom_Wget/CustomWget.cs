using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Custom_Wget.Enums;
using System.Text.RegularExpressions;
using NLog;

namespace Custom_Wget
{
    public class CustomWget
    {
        private readonly HttpClient httpClient;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string directoryPath;
        private readonly int maxDepth;
        private readonly string[] fileExtensions;

        public CustomWget(string directoryPath, int maxDepth, string[] fileExtensions)
        {
            httpClient = new HttpClient();
            this.directoryPath = directoryPath;
            this.maxDepth = maxDepth;
            this.fileExtensions = fileExtensions;
        }

        public void LoadUrl(string url, DomainRestriction domainRestriction)
        {
            if (IsValidInitialUri(url))
            {
                httpClient.BaseAddress = new Uri(url);
                ParseUrl(httpClient.BaseAddress, domainRestriction);
            }
            else return;
        }

        private void ParseUrl(Uri uri,
            DomainRestriction domainRestriction = DomainRestriction.NoRestriction,
            int currentDepth = 0)
        {
            if (currentDepth > maxDepth || (uri.Scheme != "http" && uri.Scheme != "https"))
            {
                return;
            }

            using (var response = httpClient.GetAsync(uri).Result)
            {
                try
                {
                    response.EnsureSuccessStatusCode();
                    logger.Info($"The result of the response - {response.ToString()}.");

                    string filePath = HtmlDocumentSaver.SaveHtmlDocument(response.Content, uri.OriginalString, directoryPath, fileExtensions).Result;
                    if(String.IsNullOrEmpty(filePath))
                    {
                        logger.Info($"The file extension is not supported - {uri.OriginalString}.");
                        return;
                    }
                    var childrenHtmlNodes = FindChildrenHtmlNodes(filePath);
                    if(childrenHtmlNodes == null)
                    {
                        return;
                    }

                    foreach (var childHtmlNode in childrenHtmlNodes)
                    {
                        var reference = childHtmlNode.GetAttributeValue("href", string.Empty);
                        reference = String.IsNullOrEmpty(reference) ? childHtmlNode.GetAttributeValue("src", string.Empty) : reference;

                        Uri newUri = new Uri(httpClient.BaseAddress, reference);
                        logger.Info($"The child element found - {newUri}");

                        if (domainRestriction == DomainRestriction.InInitialURLOnly && newUri.Host != httpClient.BaseAddress.Host)
                        {
                            logger.Info($"The url isn't in initial url: {reference}");
                            return;
                        }

                        if (reference != string.Empty)
                        {
                            ParseUrl(newUri, domainRestriction, currentDepth + 1);
                        }
                    }
                }
                catch (HttpRequestException httpRequestException)
                {
                    logger.Error($"Unsuccessful response: { httpRequestException.Message }");
                    return;
                }
                catch (Exception e)
                {
                    logger.Error($"Error: { e.Message }");
                    return;
                }
            }
        }

        private List<HtmlNode> FindChildrenHtmlNodes(string filePath)
        {
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.Load(filePath);
            var nodes = htmlSnippet.DocumentNode.SelectNodes("//@href | //@src")?.ToList();

            return nodes;
        }

        private bool IsValidInitialUri(string uri)
        {
            var regex = new Regex(@"^((https?|ftp|file):\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$", RegexOptions.IgnoreCase);

            if (!regex.IsMatch(uri))
            {
                logger.Error($"Incorrect uri: {uri}.");
                return false;
            }

            return true;
        }
    }
}
