using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TitleParsingTask.Models;

namespace TitleParsingTask.Controllers
{
    public class HomeController : Controller
    {
        
        public HomeController()
        {
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost("api/title")]
        public async Task<IActionResult> GetTitle([FromForm]string url)
        {
            url = url.ToLower();
            url = url.Contains(":/") ? url : $"https://{url}";
            var requestData = new ResponseData()
            {
                Url = url
            };
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(url);
                var pageContents = await response.Content.ReadAsStringAsync();

                var pageDocument = new HtmlDocument();
                pageDocument.LoadHtml(pageContents);
                var titleText = pageDocument.DocumentNode.SelectSingleNode("//title")?.InnerText ?? "<EMPTY>";

                requestData.ResponseCode = ((int)response.StatusCode).ToString();
                requestData.Title = titleText;
            }
            catch (HttpRequestException)
            {
                requestData.ResponseCode = "404";
                requestData.Title = "<NOT_FOUND>";
                return BadRequest(requestData);
            }
            catch(Exception)
            {
                requestData.ResponseCode = "Error";
                requestData.Title = "Invalid error!";
                return BadRequest(requestData);
            }


            return Ok(requestData);
        }
    }
}
