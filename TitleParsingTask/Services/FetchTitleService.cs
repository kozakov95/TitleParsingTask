using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TitleParsingTask.Models.Api.Title;

namespace TitleParsingTask.Services
{
    public class FetchTitleService
    {
        public FetchTitleService()
        {

        }

        public async Task<TitleResponseDto> FetchSiteDataAsync(string url)
        {

            try
            {
                using var httpClient = new HttpClient();

                url = url.Contains(":/") ? url : $"https://{url}";

                var response = await httpClient.GetAsync(url);
                var pageContents = await response.Content.ReadAsStringAsync();

                var pageDocument = new HtmlDocument();
                pageDocument.LoadHtml(pageContents);
                var titleText = pageDocument.DocumentNode.SelectSingleNode("//title")?.InnerText;

                return new TitleResponseDto()
                {
                    Title = titleText == null || titleText.Length == 0 ? "&ltEMPTY&gt" : titleText,
                    ResponseCode = ((int)response.StatusCode).ToString(),
                    Url = url
                };
            }
            catch (HttpRequestException)
            {
                return new TitleResponseDto()
                {
                    Url = url,
                    ResponseCode = "404",
                    Title = "&ltNOT_FOUND&gt"
                };
            }
            catch (Exception)
            {
                return new TitleResponseDto()
                {
                    Url = url,
                    ResponseCode = "Error",
                    Title = "Invalid error!"
                };
            }
        }
    }
}
