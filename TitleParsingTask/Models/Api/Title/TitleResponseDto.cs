using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TitleParsingTask.Models.Api.Title
{
    public class TitleResponseDto
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string ResponseCode { get; set; }
    }
}
