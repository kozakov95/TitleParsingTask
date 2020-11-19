using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TitleParsingTask.Models.Api.Title;
using TitleParsingTask.Services;

namespace TitleParsingTask.Controllers.Api
{
    public class TitleController:Controller
    {
        public TitleController()
        {

        }

        [HttpPost("api/title")]
        public async Task<IActionResult> GetTitle([FromBody] TitleRequestDto request, [FromServices]FetchTitleService titleService)
        {
            var responce = await titleService.FetchSiteDataAsync(request.Url);
            return Ok(responce);
        }
    }
}
