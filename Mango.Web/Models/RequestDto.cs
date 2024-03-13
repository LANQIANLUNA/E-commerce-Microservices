﻿using static Mango.Web.Utility.SD;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Mango.Web.Models
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }

        public Utility.SD.ContentType ContentType { get; set; } = Utility.SD.ContentType.Json;
    }
}
